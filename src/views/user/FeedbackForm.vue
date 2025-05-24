<template>
  <div class="container py-5">
    <div class="card shadow-sm mx-auto" style="max-width: 600px">
      <div class="card-body">
        <h4 class="mb-4 text-center">✍️ Gửi phản hồi của bạn</h4>
        <form @submit.prevent="submitFeedback">
          <div class="mb-3">
            <label class="form-label">Chọn tour</label>
            <select v-model="form.tourID" class="form-select" required>
              <option disabled value="">-- Chọn tour --</option>
              <option v-for="t in tours" :key="t.id" :value="t.id">{{ t.name }}</option>
            </select>
          </div>

          <div class="mb-3">
            <label class="form-label">Nội dung phản hồi</label>
            <textarea v-model="form.content" class="form-control" rows="4" required></textarea>
          </div>

          <button type="submit" class="btn btn-primary w-100">Gửi phản hồi</button>
        </form>
      </div>
    </div>
  </div>
</template>

<script>
// import axios from 'axios';

export default {
  name: 'FeedbackForm',
  data() {
    return {
      form: {
        feedbackID: '',
        content: '',
        date: new Date().toISOString(),
        customerID: '00000000-0000-0000-0000-000000000001',
        tourID: ''
      },
      tours: [
        { id: '1', name: 'Tour Đà Lạt 3N2Đ' },
        { id: '2', name: 'Tour Phú Quốc 4N3Đ' },
        { id: '3', name: 'Tour Nha Trang 2N1Đ' }
      ]
    };
  },
  methods: {
    submitFeedback() {
      console.log("📩 Gửi phản hồi:", this.form);

      // Gọi API khi backend sẵn sàng:
      // await axios.post("http://localhost:5017/api/feedback", this.form)
      //   .then(res => alert("✔️ Gửi phản hồi thành công!"))
      //   .catch(err => alert("❌ Lỗi gửi phản hồi!"));

      alert("✔️ Gửi phản hồi thành công (giả lập)");
      this.form.content = '';
      this.form.tourID = '';
    }
  }
};
</script>

<style scoped>
.card {
  border-radius: 1rem;
}
.card-body {
  padding: 2rem;
}
.form-label {
  font-weight: 600;
}
</style>
