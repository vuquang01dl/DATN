<template>
  <div class="container mt-5" v-if="tour">
    <h2 class="mb-3">{{ tour.name }}</h2>
    <p>{{ tour.description }}</p>
    <p><strong>Giá:</strong> {{ tour.price }} VND</p>
    <p><strong>Địa điểm:</strong> {{ tour.location }}</p>
    <p><strong>Thời gian:</strong> {{ tour.startDate }} - {{ tour.endDate }}</p>
    <router-link to="/" class="btn btn-secondary">← Quay lại</router-link>
  </div>

  <div v-else class="text-center mt-5">
    <p>Đang tải dữ liệu...</p>
  </div>
</template>

<script>
//import axios from 'axios'

export default {
  name: 'TourDetailView',
  data() {
    return {
      tour: null
    }
  },
  async mounted() {
    const id = this.$route.params.id

    // ✅ Dữ liệu giả (khi chưa chạy API)
    this.tour = {
      id,
      name: 'Tour Huế - Đà Nẵng 3N2Đ',
      description: 'Khám phá cố đô Huế và thành phố biển Đà Nẵng',
      price: 2990000,
      location: 'Huế, Đà Nẵng',
      startDate: '2025-07-10',
      endDate: '2025-07-12'
    }

    // ❗ Khi có backend thì bỏ comment đoạn dưới ra:
    /*
    try {
      const res = await axios.get(`http://localhost:5017/api/tour/${id}`)
      this.tour = res.data
    } catch (err) {
      console.error('Lỗi gọi API tour:', err)
    }
    */
  }
}
</script>
