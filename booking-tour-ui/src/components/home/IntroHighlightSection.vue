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
      { image: require('@/assets/hot1.jpg'), title: 'TTHC Đà Nẵng', desc: 'Công trình điểm nhấn của thành phố.' },
      { image: require('@/assets/mykhe.jpg'), title: 'Biển Mỹ Khê', desc: 'Một trong những bãi biển đẹp nhất hành tinh.' },
      { image: require('@/assets/sonttra.jpg'), title: 'Bán đảo Sơn Trà', desc: 'Khu bảo tồn thiên nhiên hoang sơ, phong cảnh hùng vĩ.' },
      { image: require('@/assets/nguahanhson.jpg'), title: 'Ngũ Hành Sơn', desc: 'Quần thể núi đá, hang động kỳ bí và chùa thiêng.' },
      { image: require('@/assets/cauhan.jpg'), title: 'Cầu Hàn', desc: 'Cây cầu quay nổi tiếng, biểu tượng giao thông hiện đại.' },
      { image: require('@/assets/chobacmyan.jpg'), title: 'Chợ Bắc Mỹ An', desc: 'Thiên đường ẩm thực đường phố Đà Nẵng.' },
      { image: require('@/assets/baotangchams.jpg'), title: 'Bảo tàng Chăm', desc: 'Nơi lưu giữ văn hóa Chăm Pa đặc sắc.' },
      { image: require('@/assets/helio.jpg'), title: 'Helio Center', desc: 'Khu vui chơi, giải trí và ẩm thực sôi động.' }
    ]

    }
  }
}
</script>