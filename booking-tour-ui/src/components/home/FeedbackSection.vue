<template>
  <div class="container mt-5">
    <h2 class="text-center mb-4">Góp ý từ khách hàng</h2>
    <div class="row">
      <div class="col-md-6 offset-md-3">
        <form @submit.prevent="submitFeedback">
          <div class="mb-3">
            <input v-model="feedback.name" type="text" class="form-control" placeholder="Tên bạn" required>
          </div>
          <div class="mb-3">
            <textarea v-model="feedback.comment" class="form-control" rows="4" placeholder="Góp ý của bạn" required></textarea>
          </div>
          <button type="submit" class="btn btn-success w-100">Gửi góp ý</button>
        </form>
      </div>
    </div>
  </div>
</template>

<script>
// import axios from 'axios'
export default {
  name: 'FeedbackSection',
  data() {
    return {
      feedback: { name: '', comment: '' }
    }
  },
  methods: {
    async submitFeedback() {
      // await axios.post('http://localhost:5017/api/feedback', this.feedback)
      alert('✅ Cảm ơn bạn đã góp ý!')
      this.feedback = { name: '', comment: '' }
    }
  }
}
</script>