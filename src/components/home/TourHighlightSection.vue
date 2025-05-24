<template>
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
// import axios from 'axios'
export default {
  name: 'TourHighlightSection',
  props: ['tours'],
  /*
  async mounted() {
    const res = await axios.get('http://localhost:5017/api/tour')
    this.tours = res.data
  }
  */
}
</script>
