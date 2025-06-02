<template>
  <div class="blue-bg py-5 min-vh-100">
    <div class="container">
      <h2 class="text-center blue-title mb-4">🏨 Quản lý Tour & Khách sạn</h2>
      <form @submit.prevent="addTourHotel" class="card shadow-sm px-4 py-4 rounded-4 mb-5 border-0 blue-form-card">
        <div class="row g-3 align-items-end">
          <div class="col-md-5">
            <label class="form-label fw-semibold mb-1">Chọn Tour</label>
            <select v-model="form.tourName" class="form-select form-select-lg rounded-pill shadow-none" required>
              <option value="" disabled>-- Chọn tour --</option>
              <option v-for="t in tourList" :key="t" :value="t">{{ t }}</option>
            </select>
          </div>
          <div class="col-md-5">
            <label class="form-label fw-semibold mb-1">Chọn khách sạn</label>
            <select v-model="form.hotelName" class="form-select form-select-lg rounded-pill shadow-none" required>
              <option value="" disabled>-- Chọn khách sạn --</option>
              <option v-for="h in hotelList" :key="h" :value="h">{{ h }}</option>
            </select>
          </div>
          <div class="col-md-2 d-grid">
            <button class="btn btn-blue-lg rounded-pill">Thêm</button>
          </div>
        </div>
      </form>

      <div class="card border-0 shadow-sm rounded-4 blue-form-card">
        <div class="table-responsive">
          <table class="table table-hover align-middle mb-0">
            <thead class="table-blue">
              <tr>
                <th class="text-center">Tên Tour</th>
                <th class="text-center">Tên Khách sạn</th>
                <th class="text-center">Hành động</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="item in tourHotels" :key="item.tourName + '-' + item.hotelName">
                <td class="text-center">{{ item.tourName }}</td>
                <td class="text-center">{{ item.hotelName }}</td>
                <td class="text-center">
                  <button class="btn btn-outline-danger btn-sm rounded-pill px-3"
                          @click="deleteTourHotel(item.tourName, item.hotelName)">
                    <i class="bi bi-trash"></i> Xóa
                  </button>
                </td>
              </tr>
              <tr v-if="!tourHotels.length">
                <td colspan="3" class="text-center text-secondary">Chưa có dữ liệu</td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  data() {
    return {
      tourList: [],
      hotelList: [],
      tourHotels: [],
      form: {
        tourName: '',
        hotelName: ''
      }
    }
  },
  methods: {
    async fetchTourList() {
      const res = await axios.get('https://localhost:7046/api/tour');
      this.tourList = res.data.map(x => x.name);
    },
    async fetchHotelList() {
      const res = await axios.get('https://localhost:7046/api/hotel');
      this.hotelList = res.data.map(x => x.name);
    },
    async fetchTourHotels() {
      const res = await axios.get('https://localhost:7046/api/tourhotel');
      this.tourHotels = res.data;
    },
    async addTourHotel() {
      if (!this.form.tourName || !this.form.hotelName) return;
      try {
        await axios.post('https://localhost:7046/api/tourhotel', {
          tourName: this.form.tourName,
          hotelName: this.form.hotelName
        });
        await this.fetchTourHotels();
        this.form = { tourName: '', hotelName: '' };
      } catch (err) {
        alert('Thêm thất bại!');
      }
    },
    async deleteTourHotel(tourName, hotelName) {
      if (!confirm('Xác nhận xóa?')) return;
      try {
        await axios.delete(`https://localhost:7046/api/tourhotel/${encodeURIComponent(tourName)}/${encodeURIComponent(hotelName)}`);
        await this.fetchTourHotels();
      } catch (err) {
        alert('Xóa thất bại!');
      }
    }
  },
  mounted() {
    this.fetchTourList();
    this.fetchHotelList();
    this.fetchTourHotels();
  }
}
</script>

<style scoped>
.blue-bg {
  background: linear-gradient(120deg, #e2f0ff 0%, #f0f7fa 100%);
  min-height: 100vh;
}
.blue-title {
  font-size: 2.1rem;
  font-weight: bold;
  color: #237edb;
  letter-spacing: 1px;
  text-shadow: 0 2px 8px #e3f1ff;
}
.blue-form-card {
  background: #f8fbff;
  border: none !important;
}
.btn-blue-lg {
  background: #237edb;
  color: #fff;
  font-weight: 600;
  font-size: 1.2rem;
  box-shadow: 0 2px 8px #aacbff80;
  transition: background .18s;
}
.btn-blue-lg:hover { background: #53a7f5; color: #fff; }
.form-select, .form-control {
  min-height: 2.6rem;
  border-radius: 2rem !important;
  font-size: 1.08rem;
  box-shadow: 0 2px 8px #aacbff20;
  border: 2px solid #237edb22 !important;
}
.table {
  border-radius: 1.3rem;
  overflow: hidden;
}
.table th, .table td {
  vertical-align: middle;
}
.table-blue {
  background: #e2f0ff;
  color: #237edb;
  font-weight: 600;
  letter-spacing: 0.5px;
}
.btn-outline-danger {
  border-radius: 2rem !important;
}
</style>

<!--
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.1/font/bootstrap-icons.css">
-->
