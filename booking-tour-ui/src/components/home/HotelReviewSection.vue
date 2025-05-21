<template>
  <div class="container mt-5">
    <h2 class="text-center mb-4">Đánh giá khách sạn</h2>
    <div class="row justify-content-center">
      <div class="col-md-4 mb-4" v-for="review in reviews" :key="review.id">
        <div class="card shadow-sm review-card h-100 text-center p-4">
          <div class="mb-2 text-warning">
            <span v-for="n in 5" :key="n">★</span>
          </div>
          <p class="fst-italic">“{{ review.comment }}”</p>
          <p class="fw-bold mt-3 mb-0 text-muted">– {{ review.author }}</p>
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
        { id: 1, comment: 'Khách sạn sạch sẽ, dịch vụ tốt!', author: 'Nguyễn Văn A' },
        { id: 2, comment: 'Vị trí thuận tiện, nhân viên thân thiện.', author: 'Lê Thị B' },
        { id: 3, comment: 'Giá cả hợp lý, view đẹp.', author: 'Trần Văn C' }
      ]
    }
  }
}
</script>

<style scoped>
.review-card {
  border: 1px solid #e0e0e0;
  border-radius: 12px;
  transition: transform 0.3s ease, box-shadow 0.3s ease;
  background-color: #fff;
}
.review-card:hover {
  transform: translateY(-5px);
  box-shadow: 0 10px 20px rgba(0,0,0,0.1);
}
</style>
