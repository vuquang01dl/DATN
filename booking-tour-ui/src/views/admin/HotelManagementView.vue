<template>
  <div class="hotel-bg">
    <div class="container">
      <h1 class="title mb-3">
        <i class="bi bi-building"></i> Danh sách khách sạn
      </h1>

      <button class="add-btn shadow-sm mb-4" @click="showForm(null)">
        <i class="bi bi-plus-circle"></i> Thêm khách sạn
      </button>

      <div class="card table-card mb-5 animate__animated animate__fadeInUp">
        <div class="table-responsive">
          <table class="hotel-table table align-middle mb-0">
            <thead>
              <tr>
                <th>Tên</th>
                <th>Thành phố</th>
                <th>Địa chỉ</th>
                <th>Hành động</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="hotel in hotels" :key="hotel.hotelID">
                <td><b>{{ hotel.name }}</b></td>
                <td>{{ hotel.city }}</td>
                <td>{{ hotel.address }}</td>
                <td>
                  <button class="edit-btn" @click="showForm(hotel)">
                    <i class="bi bi-pencil"></i> Sửa
                  </button>
                  <button class="delete-btn" @click="deleteHotel(hotel.hotelID)">
                    <i class="bi bi-trash"></i> Xóa
                  </button>
                </td>
              </tr>
              <tr v-if="!hotels.length">
                <td colspan="4" class="text-center text-secondary">Chưa có khách sạn nào</td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>

      <!-- Form Thêm / Sửa -->
      <transition name="fade">
        <div v-if="selectedHotel !== null" class="form-modal">
          <div class="form-card animate__animated animate__zoomIn">
            <h2 class="form-title">
              {{ isEdit ? '🛠 Cập nhật khách sạn' : '🆕 Thêm khách sạn' }}
            </h2>
            <form @submit.prevent="handleSubmit">
              <div class="form-grid">
                <input v-model="hotelForm.name" placeholder="Tên khách sạn" required />
                <input v-model.number="hotelForm.starRating" type="number" min="1" max="5" placeholder="Số sao" required />
                <textarea v-model="hotelForm.description" placeholder="Mô tả" required></textarea>
                <input v-model="hotelForm.image" placeholder="Đường dẫn ảnh" />
                <input v-model="hotelForm.address" placeholder="Địa chỉ" required />
                <input v-model="hotelForm.city" placeholder="Thành phố" required />
                <input v-model="hotelForm.district" placeholder="Quận/Huyện" required />
                <input v-model="hotelForm.ward" placeholder="Phường/Xã" required />
                <input v-model="hotelForm.phone" placeholder="Số điện thoại" required />
              </div>
              <div class="form-actions mt-3">
                <button type="submit" class="btn-pink">
                  {{ isEdit ? '✅ Cập nhật' : '✅ Tạo mới' }}
                </button>
                <button type="button" @click="cancelForm" class="btn-cancel">❌ Hủy</button>
              </div>
            </form>
          </div>
        </div>
      </transition>
    </div>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  data() {
    return {
      hotels: [],
      selectedHotel: null,
      hotelForm: {
        name: '',
        starRating: 1,
        description: '',
        image: '',
        address: '',
        city: '',
        district: '',
        ward: '',
        phone: '',
      },
    };
  },
  computed: {
    isEdit() {
      return this.selectedHotel && this.selectedHotel.hotelID;
    },
  },
  created() {
    this.loadHotels();
  },
  methods: {
    async loadHotels() {
      try {
        const res = await axios.get('https://localhost:7046/api/Hotel');
        this.hotels = res.data;
        this.cancelForm();
      } catch (err) {
        console.error(err);
        alert('Lỗi tải danh sách khách sạn');
      }
    },
    showForm(hotel) {
      this.selectedHotel = hotel || {};
      this.hotelForm = hotel
        ? { ...hotel }
        : {
            name: '',
            starRating: 1,
            description: '',
            image: '',
            address: '',
            city: '',
            district: '',
            ward: '',
            phone: '',
          };
    },
    async handleSubmit() {
      try {
        const data = { ...this.hotelForm };

        if (this.isEdit) {
          await axios.put(`https://localhost:7046/api/Hotel/${this.selectedHotel.hotelID}`, data);
          alert('Cập nhật thành công!');
        } else {
          delete data.hotelID;
          await axios.post('https://localhost:7046/api/Hotel', data);
          alert('Tạo mới thành công!');
        }

        this.loadHotels();
      } catch (err) {
        console.error(err);
        alert('Lỗi khi gửi dữ liệu');
      }
    },
    async deleteHotel(id) {
      if (confirm('Bạn có chắc muốn xóa khách sạn này?')) {
        try {
          await axios.delete(`https://localhost:7046/api/Hotel/${id}`);
          this.loadHotels();
        } catch (err) {
          console.error(err);
          alert('Lỗi khi xóa');
        }
      }
    },
    cancelForm() {
      this.selectedHotel = null;
      this.hotelForm = {
        name: '',
        starRating: 1,
        description: '',
        image: '',
        address: '',
        city: '',
        district: '',
        ward: '',
        phone: '',
      };
    },
  },
};
</script>

<style scoped>
@import 'https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css';

.hotel-bg {
  min-height: 100vh;
  background: linear-gradient(120deg, #ffe3fa 0%, #f5e4fb 100%);
  padding-bottom: 60px;
}

.container {
  max-width: 970px;
  margin: 36px auto;
  padding: 28px 28px 20px 28px;
  background: #fffafd;
  border-radius: 20px;
  box-shadow: 0 2px 16px #eeb6ed30, 0 4px 32px #b26ee822;
  position: relative;
}

.title {
  text-align: center;
  font-size: 2.1rem;
  font-weight: bold;
  color: #b26ee8;
  margin-bottom: 24px;
  letter-spacing: 1px;
  text-shadow: 0 2px 10px #ffe3fa;
}

.add-btn {
  background: linear-gradient(90deg, #b26ee8, #f476c6 80%);
  color: white;
  font-weight: 600;
  border: none;
  border-radius: 30px;
  font-size: 1.09rem;
  padding: 10px 28px;
  transition: box-shadow 0.15s, background 0.15s;
  margin-bottom: 18px;
  box-shadow: 0 4px 12px #f476c630;
}
.add-btn:hover {
  background: linear-gradient(90deg, #c493ec, #fb7ad3 80%);
  box-shadow: 0 2px 12px #b26ee855;
}

.table-card {
  border-radius: 18px !important;
  background: #fff3fb;
  box-shadow: 0 2px 16px #eeb6ed20;
}
.hotel-table {
  width: 100%;
  border-collapse: collapse;
  font-size: 1.04rem;
}
.hotel-table th, .hotel-table td {
  padding: 12px 16px;
  border: none;
  border-bottom: 2px solid #f7c8eb33;
}
.hotel-table th {
  background: #f5e4fb;
  color: #b26ee8;
  font-weight: 600;
  font-size: 1.07rem;
  letter-spacing: .5px;
}
.hotel-table tbody tr:hover {
  background: #ffe3fa66;
  transition: background 0.15s;
}
.edit-btn, .delete-btn {
  border: none;
  outline: none;
  border-radius: 18px;
  padding: 7px 15px;
  font-size: 1.02rem;
  font-weight: 500;
  margin-right: 6px;
  box-shadow: 0 1px 6px #d8a3f43c;
  transition: background .18s;
}
.edit-btn {
  background: #e7d3fa;
  color: #b26ee8;
}
.edit-btn:hover {
  background: #ffe3fa;
  color: #ae47d5;
}
.delete-btn {
  background: #fd7fae;
  color: white;
}
.delete-btn:hover {
  background: #b5275d;
  color: white;
}
.form-modal {
  position: fixed;
  top: 0; left: 0; right: 0; bottom: 0;
  background: rgba(190, 91, 222, 0.11);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 2000;
  animation: fadeIn .2s;
}
@keyframes fadeIn {
  from { opacity: 0;}
  to { opacity: 1;}
}
.form-card {
  background: #fff5fa;
  border-radius: 28px;
  box-shadow: 0 4px 26px #eeb6ed59;
  border: 2px solid #fbd8f7;
  width: 520px;
  max-width: 97vw;
  padding: 36px 28px 24px 28px;
}
.form-title {
  color: #b26ee8;
  margin-bottom: 18px;
  font-size: 1.22rem;
  font-weight: 700;
  text-align: center;
}
.form-grid {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 15px 18px;
  margin-bottom: 20px;
}
.form-grid textarea {
  grid-column: span 2;
  resize: vertical;
  min-height: 58px;
}
input, textarea {
  padding: 10px 16px;
  border: 1.5px solid #efb3f0;
  border-radius: 18px;
  font-size: 1.07rem;
  background: #fffafd;
  box-shadow: 0 1px 5px #f476c61a;
  transition: border 0.13s, box-shadow 0.18s;
}
input:focus, textarea:focus {
  outline: none;
  border: 2px solid #b26ee8;
  background: #ffe3fa;
  box-shadow: 0 2px 8px #f476c625;
}
.form-actions {
  display: flex;
  justify-content: flex-end;
  gap: 13px;
}
.btn-pink {
  background: linear-gradient(90deg, #e261c4, #b26ee8 70%);
  color: white;
  font-weight: 600;
  border: none;
  border-radius: 20px;
  padding: 8px 26px;
  font-size: 1.08rem;
  transition: background 0.14s;
  box-shadow: 0 2px 10px #dca5fa30;
}
.btn-pink:hover {
  background: linear-gradient(90deg, #f476c6, #ae47d5 70%);
}
.btn-cancel {
  background: #fff3fa;
  color: #e261c4;
  border: 2px solid #e261c444;
  border-radius: 20px;
  font-weight: 600;
  padding: 8px 22px;
  font-size: 1.08rem;
  box-shadow: 0 2px 8px #f476c633;
}
.btn-cancel:hover {
  background: #ffd6ee;
  color: #b26ee8;
}
</style>

<!--
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.1/font/bootstrap-icons.css">
-->
