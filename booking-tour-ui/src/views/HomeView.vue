<template>
  <div class="hero-banner text-white text-center py-5">
    <h1>Khám phá thế giới với chúng tôi</h1>
    <p>Đặt tour nhanh chóng - Trải nghiệm tuyệt vời</p>
  </div>

  <div class="container mt-5">
    <h2 class="text-center mb-4">Tour nổi bật</h2>
    <div class="row">
      <div class="col-md-4 mb-4" v-for="tour in tours" :key="tour.id">
        <div class="card h-100">
          <div class="card-body">
            <h5 class="card-title">{{ tour.name }}</h5>
            <p class="card-text">{{ tour.description }}</p>
            <p><strong>Giá:</strong> {{ tour.price }} VND</p>
                <router-link :to="`/tours/${tour.id}`" class="btn btn-primary">Chi tiết</router-link>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
//import axios from 'axios'

export default {
  name: 'HomeView',
  data() {
    return { tours: [] }
  },
async mounted() {
  // 👇 Khi backend chưa chạy, dùng dữ liệu giả:
  this.tours = [
    {
      id: '1',
      name: 'Tour Đà Lạt 3N2Đ',
      description: 'Khám phá xứ sở ngàn hoa',
      price: 2500000,
      location: 'Đà Lạt',
      startDate: '2025-06-01',
      endDate: '2025-06-03'
    },
    {
      id: '2',
      name: 'Tour Phú Quốc 4N3Đ',
      description: 'Biển xanh - cát trắng - nắng vàng',
      price: 3500000,
      location: 'Phú Quốc',
      startDate: '2025-06-10',
      endDate: '2025-06-13'
    }
  ];

  // ❗️Khi backend đã sẵn sàng, bạn bỏ comment bên dưới ra:
  /*
  const res = await axios.get('http://localhost:5017/api/tour');
  this.tours = res.data;
  */
}

}
</script>

<style scoped>
.hero-banner {
  background: url('@/assets/banner.png') no-repeat center center;
  background-size: cover;
  height: 300px;
  padding-top: 100px;
}
</style>
