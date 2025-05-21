<template>
  <div class="container mt-5 mb-5">
    <h2 class="text-center mb-4">Liên hệ với chúng tôi</h2>
    <div class="row">
      <div class="col-md-6 offset-md-3">
        <form @submit.prevent="submitContact">
          <input v-model="contact.name" class="form-control mb-2" placeholder="Tên">
          <input v-model="contact.email" class="form-control mb-2" placeholder="Email">
          <textarea v-model="contact.message" class="form-control mb-2" rows="4" placeholder="Tin nhắn"></textarea>
          <button class="btn btn-primary w-100">Gửi</button>
        </form>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'ContactSection',
  data() {
    return {
      contact: { name: '', email: '', message: '' }
    }
  },
  methods: {
    submitContact() {
      // ❗ Gửi dữ liệu qua API nếu cần
      alert('📬 Đã gửi liên hệ, cảm ơn bạn!')
      this.contact = { name: '', email: '', message: '' }
    }
  }
}
</script>