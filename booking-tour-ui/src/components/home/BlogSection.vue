<template>
  <div class="blog-section py-5">
    <h2 class="section-title mb-4">Tin tức du lịch</h2>
    <div class="container">
      <div class="row">
        <div class="col-lg-4 col-md-6 mb-4" v-for="post in blogs" :key="post.id">
          <div class="card blog-card h-100">
            <div class="card-body d-flex flex-column">
              <h5 class="card-title fw-bold">{{ post.title }}</h5>
              <p class="card-text flex-grow-1">{{ post.summary }}</p>
              <a
                :href="post.link"
                target="_blank"
                rel="noopener"
                class="btn btn-blog mt-auto"
              >
                Xem thêm
              </a>
            </div>
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
        {
          id: 1,
          title: 'Khám phá 5 địa điểm du lịch hot nhất Việt Nam 2025',
          summary: 'Danh sách các điểm đến cực kỳ “hot” mùa hè này mà bạn không thể bỏ lỡ.',
          link: 'https://www.bing.com/search?pglt=2083&q=Khám+phá+5+địa+điểm+du+lịch+hot+nhất+Việt+Nam+2025&cvid=01d3e028955947799e39e842cc560597&gs_lcrp=EgRlZGdlKgYIABBFGDkyBggAEEUYOTIICAEQ6QcY_FXSAQczMjBqMGoxqAIAsAIA&FORM=ANNAB1&PC=U531'
        },
        {
          id: 2,
          title: 'Kinh nghiệm du lịch tự túc tiết kiệm',
          summary: 'Chia sẻ bí quyết du lịch tiết kiệm, thoải mái, không lo phát sinh chi phí.',
          link: 'https://phuotvivu.com/blog/12-cach-tiet-kiem-tien-khi-di-du-lich-bui-phan-1/'
        },
        {
          id: 3,
          title: 'Ẩm thực miền Trung: Hành trình vị giác',
          summary: 'Khám phá những món ngon đặc sắc miền Trung khiến du khách mê mẩn.',
          link: 'https://ztravel.vn/blogs/du-lich-mien-trung-hanh-trinh-kham-pha-van-hoa-lich-su-va-am-thuc-dac-sac-231'
        },
        {
          id: 4,
          title: 'Những bãi biển đẹp nhất Việt Nam mùa hè 2025',
          summary: 'Biển xanh, cát trắng, nắng vàng – chọn ngay cho mình điểm đến lý tưởng!',
          link: 'https://top5hot.com/du-lich-bien-he-2025-top-5-bai-bien-dep-nhat-viet-nam/'
        },
        {
          id: 5,
          title: '10 mẹo hữu ích khi đi du lịch cùng gia đình',
          summary: 'Kinh nghiệm chuẩn bị, giữ an toàn và tận hưởng chuyến đi trọn vẹn.',
          link: 'https://thichvivu.vn/10-meo-huu-ich-khi-du-lich-gia-dinh-co-tre-nho/'
        },
        {
          id: 6,
          title: 'Check-in các resort mới khai trương ở Việt Nam',
          summary: 'Các khu nghỉ dưỡng mới, sang trọng, phù hợp cho mùa hè năm nay.',
          link: 'https://menandlife.com.vn/trong-nuoc/marriott-bonvoy-ra-mat-6-khach-san-va-resort-moi-tai-viet-nam-2991.html'
        }
      ]
    }
  }
}
</script>

<style scoped>
.blog-section {
  background: linear-gradient(120deg, #edeaff 0%, #d4fcf9 100%);
  min-height: 420px;
}
.section-title {
  text-align: center;
  font-size: 2rem;
  font-weight: bold;
  color: #674fa3;
  margin-bottom: 2rem;
  letter-spacing: 1px;
  text-shadow: 0 2px 10px #a9a9d566;
}
.blog-card {
  border: 1.5px solid #85e5f9;
  border-radius: 1.5rem;
  background: #fafaff;
  transition: transform 0.3s cubic-bezier(.4,2,.7,1), box-shadow 0.2s, border 0.2s;
  box-shadow: 0 4px 18px #bdbdf580;
  position: relative;
  overflow: hidden;
}
.blog-card:hover {
  transform: translateY(-10px) scale(1.04) rotate(-1.4deg);
  box-shadow: 0 16px 40px #7e82c077, 0 2px 16px #27ffd944;
  border-color: #b09cff;
  z-index: 2;
}
.card-title {
  color: #7a5be2;
  font-size: 1.18rem;
  font-weight: 600;
  margin-bottom: 1rem;
}
.btn-blog {
  background: linear-gradient(90deg, #63c2ff 20%, #bc6fff 90%);
  color: #fff;
  border: none;
  border-radius: 2rem;
  font-weight: bold;
  padding: 0.6rem 1.5rem;
  font-size: 1rem;
  box-shadow: 0 2px 8px #bc6fff33;
  transition: background 0.3s, transform 0.2s;
  letter-spacing: 0.5px;
}
.btn-blog:hover {
  background: linear-gradient(90deg, #bc6fff, #63c2ff 80%);
  color: #fff;
  transform: scale(1.09);
}
@media (max-width: 768px) {
  .col-lg-4, .col-md-6 {
    max-width: 100%;
    flex: 0 0 100%;
  }
}
</style>
