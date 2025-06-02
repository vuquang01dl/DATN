<template>
  <div class="hotel-review-section py-5">
    <h2 class="section-title mb-4">Đánh giá khách sạn</h2>
    <div class="review-marquee-wrapper">
      <div class="review-marquee" :style="{ animationDuration: marqueeDuration + 's' }">
        <div
          class="review-card"
          v-for="review in repeatedReviews"
          :key="review.marqueeKey"
        >
          <div class="stars mb-2">
            <span v-for="n in 5" :key="n" class="star">★</span>
          </div>
          <div class="hotel-name mb-2">{{ review.hotel }}</div>
          <p class="review-comment">“{{ review.comment }}”</p>
          <p class="review-author">– {{ review.author }}</p>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'HotelReviewSection',
  data() {
    return {
      reviews: [
        { id: 1, comment: 'Khách sạn sạch sẽ, dịch vụ tốt!', author: 'Nguyễn Văn A', hotel: 'Grand Sun Hotel' },
        { id: 2, comment: 'Vị trí thuận tiện, nhân viên thân thiện.', author: 'Lê Thị B', hotel: 'Seaview Resort' },
        { id: 3, comment: 'Giá cả hợp lý, view đẹp.', author: 'Trần Văn C', hotel: 'Central Park Hotel' },
        { id: 4, comment: 'Bữa sáng ngon, phòng rộng rãi.', author: 'Phạm Minh D', hotel: 'Rose Villa' },
        { id: 5, comment: 'Thiết kế hiện đại, tiện nghi.', author: 'Đỗ Thị E', hotel: 'Skyline Hotel' },
        { id: 6, comment: 'Khu vực hồ bơi tuyệt vời!', author: 'Trịnh Văn F', hotel: 'Riverside Hotel' },
        { id: 7, comment: 'Dịch vụ chu đáo, có xe đưa đón sân bay.', author: 'Ngô Quang G', hotel: 'Luxury Suites' },
        { id: 8, comment: 'Wifi mạnh, không gian yên tĩnh.', author: 'Phan Thanh H', hotel: 'Peaceful Garden Hotel' }
      ],
      repeatCount: 3, // Lặp lại review để tạo hiệu ứng cuộn dài
      marqueeDuration: 36 // Thời gian chạy hết 1 vòng (giây)
    }
  },
  computed: {
    repeatedReviews() {
      // Lặp lại reviews để cuộn liên tục không bị trống
      let result = []
      for (let i = 0; i < this.repeatCount; i++) {
        this.reviews.forEach(item => {
          result.push({ ...item, marqueeKey: `${i}-${item.id}` })
        })
      }
      return result
    }
  }
}
</script>

<style scoped>
.hotel-review-section {
  background: linear-gradient(120deg, #fefcea 0%, #e0eafc 100%);
  min-height: 380px;
}
.section-title {
  text-align: center;
  font-size: 2.1rem;
  font-weight: bold;
  color: #198d8d;
  letter-spacing: 1px;
  text-shadow: 0 2px 12px #bdbdbd15;
}
.review-marquee-wrapper {
  width: 100%;
  overflow: hidden;
  position: relative;
  padding: 25px 0 15px 0;
}
.review-marquee {
  display: flex;
  align-items: stretch;
  gap: 32px;
  animation: marquee-left linear infinite;
}
@keyframes marquee-left {
  0%   { transform: translateX(0); }
  100% { transform: translateX(-66.666%); }
}
.review-card {
  min-width: 320px;
  max-width: 340px;
  margin: 0 4px;
  background: #fff;
  border-radius: 2rem;
  box-shadow: 0 8px 32px #00bcd442, 0 2px 12px #0000000f;
  padding: 30px 24px 24px 24px;
  transition: transform 0.35s cubic-bezier(.23,1.12,.55,1), box-shadow 0.2s;
  border: none;
  text-align: center;
  position: relative;
  opacity: 0.95;
  cursor: pointer;
  animation: card-fadein 1s both;
}
@keyframes card-fadein {
  from { opacity: 0; transform: scale(0.8);}
  to   { opacity: 1; transform: scale(1);}
}
.review-card:hover {
  transform: scale(1.08) rotate(-2deg);
  z-index: 3;
  box-shadow: 0 16px 48px #ffc1077a, 0 2px 24px #00968855;
  opacity: 1;
}
.stars {
  font-size: 1.4rem;
  letter-spacing: 2px;
}
.star {
  color: #ffbe2d;
  text-shadow: 0 2px 6px #ffd95f88;
  animation: star-glow 2s infinite alternate;
  filter: drop-shadow(0 0 6px #ffe082);
}
@keyframes star-glow {
  from { transform: scale(1); filter: drop-shadow(0 0 6px #ffd70066); }
  to   { transform: scale(1.12); filter: drop-shadow(0 0 12px #ffd70099);}
}
.hotel-name {
  font-weight: 600;
  color: #c57d1c;
  font-size: 1.08rem;
  letter-spacing: 0.5px;
  margin-bottom: 0.3rem;
  text-shadow: 0 2px 6px #ffe08288;
}
.review-comment {
  font-style: italic;
  font-size: 1.18rem;
  color: #111;
  margin-bottom: 1rem;
}
.review-author {
  font-weight: bold;
  color: #178f8f;
  margin-top: 1.1rem;
  font-size: 1.05rem;
  letter-spacing: 0.5px;
}
@media (max-width: 600px) {
  .review-card {
    min-width: 80vw;
    padding: 20px 6vw 20px 6vw;
  }
}
</style>
