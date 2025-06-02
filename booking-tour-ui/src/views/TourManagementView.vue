<template>
  <div class="green-bg min-vh-100 py-5">
    <div class="container">
      <h2 class="tour-title text-center mb-4">
        <i class="bi bi-tree"></i> Quản lý Tour
      </h2>
      <button class="btn btn-green mb-3 rounded-pill shadow-sm px-4 py-2" @click="openAddModal">
        <i class="bi bi-plus-circle"></i> Thêm tour mới
      </button>

      <div class="card border-0 shadow-sm rounded-4">
        <div class="table-responsive">
          <table class="table table-hover align-middle mb-0">
            <thead class="table-green">
              <tr>
                <th class="text-center">Tên</th>
                <th class="text-center">Giá</th>
                <th class="text-center">Mô tả</th>
                <th class="text-center">Địa điểm</th>
                <th class="text-center">Ngày bắt đầu</th>
                <th class="text-center">Ngày kết thúc</th>
                <th class="text-center">Hành động</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="tour in tours" :key="tour.id">
                <td class="text-center">{{ tour.name }}</td>
                <td class="text-center">{{ tour.price.toLocaleString() }} ₫</td>
                <td class="text-center">{{ tour.description }}</td>
                <td class="text-center">{{ tour.location }}</td>
                <td class="text-center">{{ tour.startDate }}</td>
                <td class="text-center">{{ tour.endDate }}</td>
                <td class="text-center">
                  <button class="btn btn-outline-green btn-sm me-2 rounded-pill px-3" @click="editTour(tour)">
                    <i class="bi bi-pencil"></i> Sửa
                  </button>
                  <button class="btn btn-outline-danger btn-sm rounded-pill px-3" @click="deleteTour(tour.id)">
                    <i class="bi bi-trash"></i> Xóa
                  </button>
                </td>
              </tr>
              <tr v-if="!tours.length">
                <td colspan="7" class="text-center text-secondary">Chưa có dữ liệu</td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>

      <!-- Modal Form -->
      <transition name="fade">
        <div v-if="showModal" class="modal-backdrop">
          <div class="modal-content green-modal animate__animated animate__fadeInDown p-4">
            <h5 class="mb-3">{{ editingTour ? 'Cập nhật Tour' : 'Thêm Tour mới' }}</h5>
            <input v-model="form.name" class="form-control mb-2 rounded-pill" placeholder="Tên tour" />
            <input v-model="form.description" class="form-control mb-2 rounded-pill" placeholder="Mô tả" />
            <input v-model.number="form.price" type="number" class="form-control mb-2 rounded-pill" placeholder="Giá" />
            <input v-model="form.location" class="form-control mb-2 rounded-pill" placeholder="Địa điểm" />
            <input v-model="form.startDate" type="date" class="form-control mb-2 rounded-pill" placeholder="Ngày bắt đầu" />
            <input v-model="form.endDate" type="date" class="form-control mb-3 rounded-pill" placeholder="Ngày kết thúc" />

            <div class="d-flex gap-2 justify-content-end">
              <button class="btn btn-green rounded-pill px-4" @click="saveTour">
                <i class="bi bi-check-lg"></i> Lưu
              </button>
              <button class="btn btn-secondary rounded-pill px-4" @click="closeModal">
                <i class="bi bi-x-lg"></i> Huỷ
              </button>
            </div>
          </div>
        </div>
      </transition>
    </div>
  </div>
</template>

<script>
import axios from 'axios'

export default {
  name: 'TourManagementView',
  data() {
    return {
      tours: [],
      showModal: false,
      editingTour: null,
      form: {
        id: null,
        name: '',
        description: '',
        price: 0,
        location: '',
        startDate: '',
        endDate: ''
      }
    }
  },
  methods: {
    async fetchTours() {
      try {
        const res = await axios.get('https://localhost:7046/api/tour')
        this.tours = res.data
      } catch (err) {
        console.error('Lỗi khi tải danh sách tour:', err)
      }
    },
    openAddModal() {
      this.resetForm()
      this.editingTour = null
      this.showModal = true
    },
    editTour(tour) {
      this.form = { ...tour }
      this.editingTour = tour
      this.showModal = true
    },
    async saveTour() {
      try {
        if (!this.form.id) {
          this.form.id = crypto.randomUUID()
          await axios.post('https://localhost:7046/api/tour', this.form)
        } else {
          await axios.put(`https://localhost:7046/api/tour/${this.form.id}`, this.form)
        }
        await this.fetchTours()
        this.closeModal()
      } catch (err) {
        alert('Thêm/sửa tour thất bại.')
        console.error(err)
      }
    },
    async deleteTour(id) {
      if (!confirm('Bạn chắc chắn muốn xóa tour này?')) return
      try {
        await axios.delete(`https://localhost:7046/api/tour/${id}`)
        await this.fetchTours()
      } catch (err) {
        alert('Xóa tour thất bại.')
        console.error(err)
      }
    },
    resetForm() {
      this.form = {
        id: null,
        name: '',
        description: '',
        price: 0,
        location: '',
        startDate: '',
        endDate: ''
      }
    },
    closeModal() {
      this.showModal = false
      this.resetForm()
    }
  },
  mounted() {
    this.fetchTours()
  }
}
</script>

<style scoped>
@import 'https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css';

.green-bg {
  background: linear-gradient(120deg, #e7ffe7 0%, #f9fff7 100%);
  min-height: 100vh;
}
.tour-title {
  font-size: 2.2rem;
  font-weight: bold;
  color: #3cb371;
  letter-spacing: 1px;
  text-shadow: 0 2px 8px #e7ffe7;
}
.btn-green, .btn-outline-green {
  background: #4ccb54 !important;
  color: #fff !important;
  font-weight: 600;
  border: none;
}
.btn-green:hover, .btn-outline-green:hover {
  background: #51e067 !important;
  color: #fff !important;
}
.btn-outline-green {
  background: transparent !important;
  color: #37a847 !important;
  border: 2px solid #37a847 !important;
}
.form-control, .form-select {
  border-radius: 2rem !important;
  border: 2px solid #4ccb5422 !important;
  font-size: 1.09rem;
  min-height: 2.6rem;
  box-shadow: 0 2px 8px #a2ffc924;
}
.card {
  border-radius: 1.3rem !important;
  border: none !important;
  background: #fafff7;
}
.table {
  border-radius: 1.3rem;
  overflow: hidden;
}
.table th, .table td {
  vertical-align: middle;
}
.table-green {
  background: #e0ffe6;
  color: #37a847;
  font-weight: 600;
  letter-spacing: 0.5px;
}
.modal-backdrop {
  position: fixed;
  top: 0; left: 0; right: 0; bottom: 0;
  background: rgba(44, 167, 81, 0.13);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 1000;
}
.modal-content.green-modal {
  background: #f5fff5;
  width: 100%;
  max-width: 450px;
  border-radius: 1.5rem;
  box-shadow: 0 4px 24px #60d58a42;
  border: 2px solid #baf3c4;
  animation-duration: 0.34s;
}
.fade-enter-active, .fade-leave-active {
  transition: all 0.36s cubic-bezier(.87,.3,.18,1.1);
}
.fade-enter, .fade-leave-to {
  opacity: 0;
  transform: translateY(15px) scale(.96);
}
</style>

<!--
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.1/font/bootstrap-icons.css">
-->
