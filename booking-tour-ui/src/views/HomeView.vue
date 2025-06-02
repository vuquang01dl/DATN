<template>
  <div class="home-container">
    <section class="home-section yellow-bg">
      <HeroBannerSection />
    </section>
    <section class="home-section blue-bg">
      <IntroHighlightSection />
    </section>
    <section class="home-section yellow-bg">
      <TourHighlightSection :tours="tours" />
    </section>
    <section class="home-section blue-bg">
      <HotelReviewSection />
    </section>
    <section class="home-section yellow-bg">
      <FeedbackSection />
    </section>
    <section class="home-section blue-bg">
      <BlogSection />
    </section>
    <section class="home-section yellow-bg">
      <ContactSection />
    </section>
    <section class="home-section blue-bg">
      <MapSection />
    </section>
    <section class="home-section yellow-bg footer-fix">
      <FooterSection />
    </section>
  </div>
</template>

<script>
import HeroBannerSection from '../components/home/HeroBannerSection.vue'
import IntroHighlightSection from '../components/home/IntroHighlightSection.vue'
import TourHighlightSection from '../components/home/TourHighlightSection.vue'
import HotelReviewSection from '../components/home/HotelReviewSection.vue'
import FeedbackSection from '../components/home/FeedbackSection.vue'
import BlogSection from '../components/home/BlogSection.vue'
import ContactSection from '../components/home/ContactSection.vue'
import MapSection from '../components/home/MapSection.vue'
import FooterSection from '../components/home/FooterSection.vue'

export default {
  name: 'HomeView',
  components: {
    HeroBannerSection,
    IntroHighlightSection,
    TourHighlightSection,
    HotelReviewSection,
    FeedbackSection,
    BlogSection,
    ContactSection,
    MapSection,
    FooterSection
  },
data() {
  return {
    tours: [
      {
        id: '1',
        name: 'Tour Đà Lạt 3N2Đ',
        description: 'Khám phá xứ sở ngàn hoa',
        price: 2500000
      },
      {
        id: '2',
        name: 'Tour Phú Quốc 4N3Đ',
        description: 'Biển xanh - cát trắng - nắng vàng',
        price: 3500000
      },
      {
        id: '3',
        name: 'Tour Đà Nẵng - Bà Nà Hills 3N2Đ',
        description: 'Trải nghiệm thành phố đáng sống, tham quan Bà Nà Hills',
        price: 2800000
      },
      {
        id: '4',
        name: 'Tour Nha Trang 4N3Đ',
        description: 'Tận hưởng biển xanh, đảo ngọc và đặc sản miền Trung',
        price: 3200000
      },
      {
        id: '5',
        name: 'Tour Sapa - Fansipan 3N2Đ',
        description: 'Chinh phục nóc nhà Đông Dương, ngắm ruộng bậc thang',
        price: 2950000
      },
      {
        id: '6',
        name: 'Tour Hà Nội - Hạ Long - Ninh Bình 5N4Đ',
        description: 'Khám phá thủ đô ngàn năm văn hiến, vịnh Hạ Long kỳ quan thế giới',
        price: 4200000
      },
      {
        id: '7',
        name: 'Tour Huế - Đà Nẵng - Hội An 4N3Đ',
        description: 'Di sản miền Trung: Cố đô Huế, phố cổ Hội An, biển Đà Nẵng',
        price: 3350000
      },
      {
        id: '8',
        name: 'Tour Côn Đảo 3N2Đ',
        description: 'Khám phá hòn đảo huyền thoại, biển xanh hoang sơ, lịch sử thiêng liêng',
        price: 3900000
      },
      {
        id: '9',
        name: 'Tour Miền Tây Sông Nước 3N2Đ',
        description: 'Trải nghiệm chợ nổi, miệt vườn, văn hóa sông nước Nam Bộ',
        price: 2700000
      },
    ]
  }
}

}
</script>

<style scoped>
.home-container {
  max-width: 1280px;
  margin: 0 auto;
  padding: 32px 0 48px 0;
  background: #f8fafc;
}
.home-section {
  border-radius: 18px;
  margin-bottom: 28px;
  box-shadow: 0 2px 12px 0 rgba(60, 60, 100, 0.06);
  padding: 0;
  position: relative;
  overflow: hidden;
}
/* Màu nền so le */
.yellow-bg { background: #fffbe7; }   /* vàng nhạt */
.blue-bg   { background: #f0f9ff; }   /* xanh nhạt */
.footer-fix {
  border-radius: 0 0 18px 18px;
  box-shadow: none;
}

/* Responsive */
@media (max-width: 900px) {
  .home-container {
    padding: 0 0 28px 0;
  }
  .home-section {
    margin-bottom: 14px;
    border-radius: 10px;
  }
}
</style>
