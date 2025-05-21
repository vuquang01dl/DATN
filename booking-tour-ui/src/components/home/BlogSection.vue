<template>
  <div class="container mt-5">
    <h2 class="text-center mb-4">Tin tức du lịch</h2>
    <div class="row">
      <div class="col-md-4" v-for="post in blogs" :key="post.id">
        <div class="card h-100">
          <div class="card-body">
            <h5 class="card-title">{{ post.title }}</h5>
            <p class="card-text">{{ post.summary }}</p>
            <a :href="post.link" class="btn btn-outline-primary">Xem thêm</a>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'BlogSection',
  data() {
    return {
      blogs: [
        { id: 1, title: '5 địa điểm du lịch hot mùa hè 2025', summary: 'Gợi ý tour hấp dẫn cho bạn và gia đình', link: '#' },
        { id: 2, title: 'Kinh nghiệm du lịch tự túc', summary: 'Tự tin hơn khi khám phá thế giới một mình!', link: '#' },
        { id: 3, title: 'Ẩm thực miền Trung', summary: 'Khám phá hương vị đặc sản miền Trung Việt Nam.', link: '#' }
      ]
    }
  }
}
</script>