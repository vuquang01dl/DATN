<template>
  <div class="container mt-5">
    <h2>Quản lý khách sạn</h2>
    <button class="btn btn-primary mb-3" @click="openAddModal">+ Thêm khách sạn</button>

    <table class="table table-bordered">
      <thead class="table-dark">
        <tr>
          <th>#</th>
          <th>Tên khách sạn</th>
          <th>Địa điểm</th>
          <th>Số sao</th>
          <th>Thao tác</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(h, index) in hotels" :key="h.id">
          <td>{{ index + 1 }}</td>
          <td>{{ h.name }}</td>
          <td>{{ h.location }}</td>
          <td>{{ h.star }}</td>
          <td>
            <button class="btn btn-sm btn-warning me-2" @click="openEditModal(h)">Sửa</button>
            <button class="btn btn-sm btn-danger" @click="deleteHotel(h.id)">Xóa</button>
          </td>
        </tr>
      </tbody>
    </table>

    <!-- Modal thêm/sửa -->
    <div v-if="showModal" class="modal-backdrop">
      <div class="modal-dialog">
        <div class="modal-content p-4">
          <h5>{{ isEditing ? 'Sửa khách sạn' : 'Thêm khách sạn' }}</h5>
          <input v-model="form.name" class="form-control mb-2" placeholder="Tên khách sạn" />
          <input v-model="form.location" class="form-control mb-2" placeholder="Địa điểm" />
          <input v-model.number="form.star" type="number" class="form-control mb-3" placeholder="Số sao (1-5)" />
          <div class="text-end">
            <button class="btn btn-secondary me-2" @click="showModal = false">Hủy</button>
            <button class="btn btn-success" @click="saveHotel">{{ isEditing ? 'Lưu' : 'Thêm' }}</button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
// import axios from 'axios'

export default {
  name: 'HotelManagementView',
  data() {
    return {
      hotels: [],
      showModal: false,
      isEditing: false,
      form: { id: '', name: '', location: '', star: 3 }
    }
  },
  mounted() {
    this.loadHotels()

    // Gọi API nếu backend đã sẵn sàng
    /*
    axios.get('http://localhost:5017/api/hotel')
      .then(res => this.hotels = res.data)
    */
  },
  methods: {
    loadHotels() {
      this.hotels = [
        { id: 'h1', name: 'Khách sạn Paradise', location: 'Đà Nẵng', star: 4 },
        { id: 'h2', name: 'Sunrise Hotel', location: 'Phú Quốc', star: 5 }
      ]
    },
    openAddModal() {
      this.form = { id: '', name: '', location: '', star: 3 }
      this.isEditing = false
      this.showModal = true
    },
    openEditModal(hotel) {
      this.form = { ...hotel }
      this.isEditing = true
      this.showModal = true
    },
    saveHotel() {
      if (!this.form.name || !this.form.location || this.form.star < 1 || this.form.star > 5) {
        alert("Vui lòng nhập đúng và đủ thông tin");
        return;
      }

      if (this.isEditing) {
        const index = this.hotels.findIndex(h => h.id === this.form.id)
        if (index !== -1) this.hotels[index] = { ...this.form }
        // axios.put(`http://localhost:5017/api/hotel/${this.form.id}`, this.form)
      } else {
        const newHotel = { ...this.form, id: 'h' + Date.now() }
        this.hotels.push(newHotel)
        // axios.post('http://localhost:5017/api/hotel', newHotel)
      }

      this.showModal = false
    },
    deleteHotel(id) {
      if (!confirm("Xác nhận xóa khách sạn?")) return
      this.hotels = this.hotels.filter(h => h.id !== id)
      // axios.delete(`http://localhost:5017/api/hotel/${id}`)
    }
  }
}
</script>

<style scoped>
.modal-backdrop {
  position: fixed;
  inset: 0;
  background: rgba(0, 0, 0, 0.4);
  z-index: 1050;
  display: flex;
  align-items: center;
  justify-content: center;
}
</style>
