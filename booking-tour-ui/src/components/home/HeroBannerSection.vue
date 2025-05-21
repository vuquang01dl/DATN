<template>
  <div id="heroCarousel" class="carousel slide carousel-fade" data-bs-ride="carousel">
    <div class="carousel-inner">
      <div
        class="carousel-item"
        :class="{ active: index === 0 }"
        v-for="(banner, index) in banners"
        :key="index"
      >
        <img :src="banner.image" class="d-block w-100 hero-img" :alt="banner.title" />
        <div class="carousel-caption d-none d-md-block">
          <h5>{{ banner.title }}</h5>
          <p>{{ banner.subtitle }}</p>
        </div>
      </div>
    </div>

    <!-- Nút chuyển -->
    <button class="carousel-control-prev" type="button" data-bs-target="#heroCarousel" data-bs-slide="prev">
      <span class="carousel-control-prev-icon"></span>
    </button>
    <button class="carousel-control-next" type="button" data-bs-target="#heroCarousel" data-bs-slide="next">
      <span class="carousel-control-next-icon"></span>
    </button>
  </div>
</template>

<script>
export default {
  name: 'HeroBannerSection',
  data() {
    return {
      banners: [
        {
          image: require('@/assets/banner1.jpg'),
          title: 'Bà Nà Hills',
          subtitle: 'Khám phá vẻ đẹp thiên nhiên và kiến trúc'
        },
        {
          image: require('@/assets/banner2.jpg'),
          title: 'Cầu Rồng',
          subtitle: 'Biểu tượng sống động của Đà Nẵng'
        },
        {
          image: require('@/assets/banner4.jpg'),
          title: 'Trung tâm hành chính',
          subtitle: 'Biểu tượng phát triển thành phố'
        }
      ]
    }
  }
}
</script>

<style scoped>
.hero-img {
  height: 400px;
  object-fit: cover;
}
.carousel-caption {
  background-color: rgba(0, 0, 0, 0.4);
  border-radius: 8px;
  padding: 1rem;
}
</style>
