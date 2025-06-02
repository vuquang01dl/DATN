<template>
  <div class="shopee-bg py-5 min-vh-100">
    <div class="container">
      <h2 class="text-center shopee-title mb-4">✨ Quản lý Tour & Điểm đến</h2>
      <form @submit.prevent="addTourDestination" class="card shadow-sm px-4 py-4 rounded-4 mb-5 border-0">
        <div class="row g-3 align-items-end">
          <div class="col-md-5">
            <label class="form-label fw-semibold mb-1">Chọn Tour</label>
            <select v-model="form.tourName" class="form-select form-select-lg rounded-pill shadow-none" required>
              <option value="" disabled>-- Chọn tour --</option>
              <option v-for="t in tourList" :key="t" :value="t">{{ t }}</option>
            </select>
          </div>
          <div class="col-md-5">
            <label class="form-label fw-semibold mb-1">Chọn điểm đến</label>
            <select v-model="form.destinationName" class="form-select form-select-lg rounded-pill shadow-none" required>
              <option value="" disabled>-- Chọn điểm đến --</option>
              <option v-for="d in destinationList" :key="d" :value="d">{{ d }}</option>
            </select>
          </div>
          <div class="col-md-2 d-grid">
            <button class="btn btn-shopee-lg rounded-pill">Thêm</button>
          </div>
        </div>
      </form>

      <div class="card border-0 shadow-sm rounded-4">
        <div class="table-responsive">
          <table class="table table-hover align-middle">
            <thead class="table-light">
              <tr>
                <th class="text-center">Tên Tour</th>
                <th class="text-center">Tên Điểm đến</th>
                <th class="text-center">Hành động</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="item in tourDestinations" :key="item.tourName + '-' + item.destinationName">
                <td class="text-center">{{ item.tourName }}</td>
                <td class="text-center">{{ item.destinationName }}</td>
                <td class="text-center">
                  <button class="btn btn-outline-danger btn-sm rounded-pill px-3"
                          @click="deleteTourDestination(item.tourName, item.destinationName)">
                    <i class="bi bi-trash"></i> Xóa
                  </button>
                </td>
              </tr>
              <tr v-if="!tourDestinations.length">
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
import axios from 'axios'

export default {
  data() {
    return {
      tourList: [],          // danh sách tên tour
      destinationList: [],   // danh sách tên điểm đến
      tourDestinations: [],  // các cặp tour - điểm đến
      form: {
        tourName: '',
        destinationName: ''
      }
    }
  },
  methods: {
    async fetchTourList() {
      const res = await axios.get('https://localhost:7046/api/tour');
      // Nếu res.data là array các object { name: string, ... } thì map như dưới
      this.tourList = res.data.map(x => x.name);
    },
    async fetchDestinationList() {
      const res = await axios.get('https://localhost:7046/api/destination');
      this.destinationList = res.data.map(x => x.name);
    },
    async fetchTourDestinations() {
      const res = await axios.get('https://localhost:7046/api/tourdestination');
      // Nếu trả về { tourName, destinationName } thì giữ nguyên
      this.tourDestinations = res.data;
    },
    async addTourDestination() {
      if (!this.form.tourName || !this.form.destinationName) return;
      try {
        await axios.post('https://localhost:7046/api/tourdestination', {
          tourName: this.form.tourName,
          destinationName: this.form.destinationName
        });
        await this.fetchTourDestinations();
        this.form = { tourName: '', destinationName: '' };
      } catch (err) {
        alert('Lỗi khi thêm Tour-Điểm đến!');
      }
    },
    async deleteTourDestination(tourName, destinationName) {
      if (!confirm('Xác nhận xóa?')) return;
      try {
        await axios.delete(`https://localhost:7046/api/tourdestination/${encodeURIComponent(tourName)}/${encodeURIComponent(destinationName)}`);
        await this.fetchTourDestinations();
      } catch (err) {
        alert('Lỗi khi xóa!');
      }
    }
  },
  mounted() {
    this.fetchTourList();
    this.fetchDestinationList();
    this.fetchTourDestinations();
  }
}
</script>

<style scoped>
.shopee-bg {
  background: linear-gradient(120deg, #ffe2ce 0%, #fff4e3 100%);
  min-height: 100vh;
}
.shopee-title {
  font-size: 2.1rem;
  font-weight: bold;
  color: #ee4d2d;
  letter-spacing: 1px;
  text-shadow: 0 2px 8px #ffede3;
}
.card {
  border-radius: 1.3rem !important;
  border: none !important;
}
.btn-shopee-lg {
  background: #ee4d2d;
  color: #fff;
  font-weight: 600;
  font-size: 1.2rem;
  box-shadow: 0 2px 8px #ffd3bc80;
  transition: background .18s;
}
.btn-shopee-lg:hover { background: #ff7337; color: #fff; }
.form-select, .form-control {
  min-height: 2.6rem;
  border-radius: 2rem !important;
  font-size: 1.08rem;
  box-shadow: 0 2px 8px #ffd3bc20;
  border: 2px solid #ee4d2d22 !important;
}
.table {
  border-radius: 1.3rem;
  overflow: hidden;
}
.table th, .table td {
  vertical-align: middle;
}
.table th {
  background: #fff7f3;
  color: #ee4d2d;
  font-weight: 600;
  letter-spacing: 0.5px;
}
.btn-outline-danger {
  border-radius: 2rem !important;
}
</style>

<!--
Chèn link bootstrap icons nếu dùng icon:
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.1/font/bootstrap-icons.css">
-->
