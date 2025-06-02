<template>
  <div class="tour-detail-section py-5" v-if="tour">
    <div class="container">
      <div class="row align-items-center g-5">
        <div class="col-md-6">
          <div class="img-wrap shadow-lg rounded-4 overflow-hidden mb-4 mb-md-0">
            <img :src="tour.image" class="img-fluid" alt="Ảnh tour" />
          </div>
        </div>
        <div class="col-md-6">
          <div class="tour-info p-4 rounded-4 shadow-sm bg-white h-100 d-flex flex-column">
            <h2 class="mb-3 fw-bold tour-title">{{ tour.name }}</h2>
            <p class="text-muted">{{ tour.description }}</p>
            <p><strong>📍 Địa điểm:</strong> {{ tour.location }}</p>
            <p><strong>🗓 Thời gian:</strong> {{ tour.startDate }} → {{ tour.endDate }}</p>
            <p>
              <strong>💰 Giá:</strong>
              <span class="price-tag">{{ tour.price.toLocaleString() }} VND</span>
            </p>
            <div class="mt-4 d-flex flex-wrap gap-3">
              <router-link to="/" class="btn back-btn px-4">
                ← Quay lại
              </router-link>
            <router-link to="/bookingform" class="btn book-btn px-4" exact-active-class="active-link">Đặt tour</router-link>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>

  <div v-else class="text-center mt-5">
    <div class="spinner-border text-primary" role="status">
      <span class="visually-hidden">Loading...</span>
    </div>
    <p class="mt-2">Đang tải dữ liệu tour...</p>
  </div>
</template>

<script>
// import axios from 'axios';

export default {
  name: 'TourDetailView',
  data() {
    return {
      tour: null,
      fakeTours: [
        {
          id: '1',
          name: 'Tour Đà Lạt 3N2Đ',
          description: 'Khám phá xứ sở ngàn hoa với những trải nghiệm tuyệt vời tại Đà Lạt.',
          price: 2500000,
          location: 'Đà Lạt',
          startDate: '2025-06-10',
          endDate: '2025-06-12',
          image: 'https://cdn3.ivivu.com/2023/09/du-lich-da-lat-ivivu.jpg'
        },
        {
          id: '2',
          name: 'Tour Phú Quốc 4N3Đ',
          description: 'Biển xanh - cát trắng - nắng vàng và hải sản tươi ngon tại Phú Quốc.',
          price: 3500000,
          location: 'Phú Quốc',
          startDate: '2025-07-01',
          endDate: '2025-07-04',
          image: 'https://cdn3.ivivu.com/2023/06/phu-quoc-tour.jpg'
        }
      ]
    };
  },
  async mounted() {
    const id = this.$route.params.id;
    // Dữ liệu giả theo ID
    this.tour = this.fakeTours.find(t => t.id === id);
    // Nếu dùng API thật, thay thế như bên dưới:
    /*
    try {
      const res = await axios.get(`http://localhost:5017/api/tour/${id}`);
      this.tour = res.data;
    } catch (err) {
      console.error('❌ Lỗi gọi API tour:', err);
    }
    */
  }
};
</script>

<style scoped>
.tour-detail-section {
  background: linear-gradient(120deg, #f7fbff 0%, #e0f7fa 100%);
  min-height: 550px;
}
.img-wrap {
  border-radius: 2.2rem !important;
  box-shadow: 0 12px 40px #00bcd433, 0 2px 16px #ffd95f33;
  max-height: 420px;
}
.img-wrap img {
  object-fit: cover;
  width: 100%;
  height: 100%;
  min-height: 320px;
  max-height: 420px;
  border-radius: 2.2rem;
  transition: transform 0.3s;
}
.img-wrap img:hover {
  transform: scale(1.04) rotate(-2deg);
  box-shadow: 0 8px 30px #b0f2ef66;
}
.tour-info {
  background: rgba(255,255,255,0.95);
  border-radius: 2.2rem;
  box-shadow: 0 6px 24px #bdbdbd15;
  min-height: 340px;
  animation: fadeIn 0.7s;
}
@keyframes fadeIn {
  from { opacity: 0; transform: translateY(40px);}
  to   { opacity: 1; transform: translateY(0);}
}
.tour-title {
  color: #1183c6;
  font-size: 2rem;
  letter-spacing: 1px;
  text-shadow: 0 2px 16px #cbefff33;
}
.price-tag {
  color: #e65100;
  font-size: 1.4rem;
  font-weight: bold;
  letter-spacing: 0.5px;
  text-shadow: 0 2px 8px #ffe08255;
  margin-left: 0.5rem;
}
.back-btn {
  background: linear-gradient(90deg, #b0b8c2 10%, #63c2ff 80%);
  color: #fff;
  border-radius: 2rem;
  font-weight: bold;
  font-size: 1.05rem;
  box-shadow: 0 2px 10px #b0b8c255;
  transition: background 0.3s, transform 0.18s;
  border: none;
}
.back-btn:hover {
  background: linear-gradient(90deg, #63c2ff, #b0b8c2 80%);
  color: #fff;
  transform: scale(1.04);
}
.book-btn {
  background: linear-gradient(90deg, #fbc02d 15%, #009688 80%);
  color: #fff;
  border-radius: 2rem;
  font-weight: bold;
  font-size: 1.08rem;
  box-shadow: 0 2px 12px #00bcd455;
  transition: background 0.3s, transform 0.18s;
  border: none;
  letter-spacing: 1px;
}
.book-btn:hover {
  background: linear-gradient(90deg, #009688 10%, #fbc02d 90%);
  color: #fff;
  transform: scale(1.06);
}
@media (max-width: 768px) {
  .img-wrap img, .img-wrap {
    min-height: 220px;
    max-height: 260px;
    border-radius: 1.2rem !important;
  }
  .tour-info {
    padding: 1.2rem !important;
    border-radius: 1.2rem;
  }
}
</style>
