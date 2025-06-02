<template>
  <div class="feedback-section py-5">
    <h2 class="section-title mb-4">Góp ý từ khách hàng</h2>
    <div class="row justify-content-center">
      <div class="col-lg-6 col-md-8">
        <form class="feedback-form p-4 shadow" @submit.prevent="submitFeedback">
          <div class="mb-3">
            <input v-model="feedback.name" type="text" class="form-control custom-input" placeholder="Tên bạn" required>
          </div>
          <div class="mb-3">
            <textarea v-model="feedback.comment" class="form-control custom-input" rows="4" placeholder="Góp ý của bạn" required></textarea>
          </div>
          <button type="submit" class="btn send-btn w-100">Gửi góp ý</button>
        </form>
        <transition name="fade">
          <div v-if="sent" class="alert alert-success text-center mt-4 animate__animated animate__fadeInDown">
            ✅ Cảm ơn bạn đã góp ý!
          </div>
        </transition>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'FeedbackSection',
  data() {
    return {
      feedback: { name: '', comment: '' },
      sent: false
    }
  },
  methods: {
    async submitFeedback() {
      // await axios.post('http://localhost:5017/api/feedback', this.feedback)
      this.sent = true
      this.feedback = { name: '', comment: '' }
      setTimeout(() => this.sent = false, 3000)
    }
  }
}
</script>

<style scoped>
.feedback-section {
  background: linear-gradient(120deg, #e0f7fa 0%, #fffbe7 100%);
  min-height: 450px;
}
.section-title {
  text-align: center;
  font-size: 2rem;
  font-weight: bold;
  color: #209a88;
  margin-bottom: 2rem;
  letter-spacing: 1px;
}
.feedback-form {
  border-radius: 2rem;
  background: rgba(255, 255, 255, 0.82);
  box-shadow: 0 8px 32px #00bcd444, 0 1px 8px #fbc02d22;
  backdrop-filter: blur(8px);
  border: 1px solid #fbc02d22;
  animation: card-fadein 1.2s;
}
@keyframes card-fadein {
  from { opacity: 0; transform: translateY(50px);}
  to   { opacity: 1; transform: translateY(0);}
}
.custom-input {
  border-radius: 1.1rem;
  border: 1.5px solid #e0e0e0;
  font-size: 1.12rem;
  padding: 0.9rem 1.1rem;
  background: #fff8e1b8;
  box-shadow: 0 2px 8px #ffe0821f;
  transition: border 0.2s, box-shadow 0.2s;
}
.custom-input:focus {
  border: 2px solid #ffc107;
  box-shadow: 0 0 0 0.2rem #ffe0824d;
  outline: none;
  background: #fffde7;
}
.send-btn {
  background: linear-gradient(90deg, #ffb300, #009688 80%);
  color: #fff;
  font-weight: bold;
  padding: 0.9rem;
  border: none;
  border-radius: 2rem;
  font-size: 1.12rem;
  box-shadow: 0 2px 16px #ffb30030;
  letter-spacing: 1px;
  transition: background 0.3s, transform 0.2s;
}
.send-btn:hover {
  background: linear-gradient(90deg, #009688, #ffc107 80%);
  transform: scale(1.05);
}
.fade-enter-active, .fade-leave-active {
  transition: opacity 0.5s;
}
.fade-enter, .fade-leave-to {
  opacity: 0;
}
@media (max-width: 700px) {
  .feedback-form {
    padding: 2rem 1rem;
  }
}
</style>
