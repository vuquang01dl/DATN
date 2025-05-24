<template>
  <div class="container mt-5" v-if="tour">
    <div class="row g-4">
      <div class="col-md-6">
        <img :src="tour.image" class="img-fluid rounded shadow-sm" alt="Ảnh tour" />
      </div>
      <div class="col-md-6">
        <h2 class="mb-3 fw-bold">{{ tour.name }}</h2>
        <p class="text-muted">{{ tour.description }}</p>
        <p><strong>📍 Địa điểm:</strong> {{ tour.location }}</p>
        <p><strong>🗓 Thời gian:</strong> {{ tour.startDate }} → {{ tour.endDate }}</p>
        <p><strong>💰 Giá:</strong> <span class="text-danger fw-bold">{{ tour.price.toLocaleString() }} VND</span></p>
        <router-link to="/" class="btn btn-secondary mt-3">← Quay lại</router-link>
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

    // ✅ Dữ liệu giả theo ID
    this.tour = this.fakeTours.find(t => t.id === id);

    // ❗ Dữ liệu thật khi có backend:
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
img {
  object-fit: cover;
  max-height: 400px;
}
h2 {
  color: #0056b3;
}
</style>
