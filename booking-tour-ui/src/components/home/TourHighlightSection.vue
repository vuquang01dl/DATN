<template>
  <div class="tour-highlight-section py-5">
    <h2 class="section-title mb-5">Tour nổi bật</h2>
    <div class="container">
      <div class="row justify-content-center">
        <div
          class="col-lg-6 col-md-6 mb-4"
          v-for="tour in tours"
          :key="tour.id"
        >
          <div class="tour-card card h-100 shadow-sm animate__animated animate__fadeInUp">
            <img
              :src="tour.image || require('@/assets/caurong.jpg')"
              class="tour-image mb-3"
              alt="Ảnh tour"
            />
            <div class="card-body d-flex flex-column">
              <h5 class="card-title fw-bold mb-3">{{ tour.name }}</h5>
              <p class="card-text flex-grow-1">{{ tour.description }}</p>
              <div class="price mb-3">
                <span>Giá:</span>
                <span class="price-value">{{ tour.price.toLocaleString() }} VND</span>
              </div>
              <router-link
                :to="`/tours/${tour.id}`"
                class="btn tour-btn mt-auto"
              >
                Chi tiết
              </router-link>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'TourHighlightSection',
  props: ['tours']
}
</script>

<style scoped>
.tour-highlight-section {
  background: linear-gradient(120deg, #fffbe7 0%, #e0f7fa 100%);
  min-height: 100vh;
}
.section-title {
  text-align: center;
  font-size: 2.2rem;
  font-weight: bold;
  letter-spacing: 1px;
  color: #188c8c;
  margin-bottom: 2rem;
  text-shadow: 0 2px 12px #bdbdbd20;
}
.tour-card {
  border: none;
  border-radius: 1.5rem;
  background: #fff;
  transition: transform 0.3s cubic-bezier(.4,2,.7,1), box-shadow 0.2s;
  box-shadow: 0 4px 20px #bdbdbd2a;
  position: relative;
  overflow: hidden;
  padding-top: 8px;
}
.tour-card:hover {
  transform: translateY(-12px) scale(1.03);
  box-shadow: 0 12px 32px #00bcd44a;
  z-index: 2;
}
.tour-image {
  width: 100%;
  height: 190px;
  object-fit: cover;
  border-radius: 1.3rem 1.3rem 0 0;
}
.card-title {
  color: #c57d1c;
  font-size: 1.3rem;
  font-weight: 600;
}
.price {
  font-size: 1.08rem;
  color: #009688;
}
.price-value {
  font-size: 1.2rem;
  font-weight: 700;
  color: #e65100;
  margin-left: 0.5rem;
}
.tour-btn {
  background: linear-gradient(90deg, #fbc02d, #009688 80%);
  color: #fff;
  border: none;
  border-radius: 2rem;
  font-weight: bold;
  padding: 0.6rem 1.5rem;
  font-size: 1rem;
  box-shadow: 0 2px 8px #00968833;
  transition: background 0.3s, transform 0.2s;
}
.tour-btn:hover {
  background: linear-gradient(90deg, #009688, #fbc02d 80%);
  color: #fff;
  transform: scale(1.08);
}
@media (max-width: 992px) {
  .col-lg-6, .col-md-6 {
    max-width: 100%;
    flex: 0 0 100%;
  }
}
</style>
