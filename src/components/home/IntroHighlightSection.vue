<template>
  <div class="container mt-4">
    <div class="row text-center">
      <div class="col-md-4 mb-4" v-for="(item, i) in highlights" :key="i">
        <img :src="item.image" class="img-fluid rounded mb-2" :alt="item.title">
        <h5>{{ item.title }}</h5>
        <p>{{ item.desc }}</p>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'IntroHighlightSection',
  data() {
    return {
      highlights: [
        { image: require('@/assets/banahill.jpg'), title: 'Bà Nà Hills', desc: 'Phong cảnh thiên nhiên, kiến trúc độc đáo.' },
        { image: require('@/assets/caurong.jpg'), title: 'Cầu Rồng', desc: 'Biểu tượng hiện đại của Đà Nẵng.' },
        { image: require('@/assets/hot1.jpg'), title: 'TTHC Đà Nẵng', desc: 'Công trình điểm nhấn của thành phố.' }
      ]
    }
  }
}
</script>