<template>
  <div>
    <HeroBannerSection />
    <IntroHighlightSection />
    <TourHighlightSection :tours="tours" />
    <HotelReviewSection />
    <FeedbackSection />
    <BlogSection />
    <ContactSection />
    <FooterSection />
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
        }
      ]
    }
  }
}
</script>
