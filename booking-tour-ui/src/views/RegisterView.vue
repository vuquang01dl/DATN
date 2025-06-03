<template>
  <div class="container mt-5" style="max-width: 420px;">
    <div class="card shadow-lg p-4">
      <h2 class="text-center mb-4" style="color: #27ae60;">📝 Đăng ký tài khoản</h2>
      <form @submit.prevent="register" autocomplete="off">
        <div class="mb-3">
          <label class="form-label fw-semibold">Họ tên</label>
          <input v-model="form.fullName" type="text" class="form-control" placeholder="Nhập họ tên" required />
        </div>
        <div class="mb-3">
          <label class="form-label fw-semibold">Địa chỉ</label>
          <input v-model="form.address" type="text" class="form-control" placeholder="Nhập địa chỉ" required />
        </div>
        <div class="mb-3">
          <label class="form-label fw-semibold">Số điện thoại</label>
          <input v-model="form.phone" type="tel" class="form-control" placeholder="Nhập số điện thoại" pattern="^0\d{9,10}$" required />
        </div>
        <div class="mb-3">
          <label class="form-label fw-semibold">Email</label>
          <input v-model="form.email"
                 type="email"
                 class="form-control"
                 placeholder="Nhập email"
                 required
                 @blur="checkEmailExist" />
          <div v-if="emailExists" class="text-danger mt-1" style="font-size: 0.93rem;">
            ❌ Email đã tồn tại, vui lòng chọn email khác!
          </div>
        </div>
        <div class="mb-3">
          <label class="form-label fw-semibold">Mật khẩu</label>
          <input v-model="form.password" type="password" class="form-control" placeholder="Nhập mật khẩu" required />
        </div>
        <button type="submit" class="btn btn-success w-100 fw-bold" style="font-size: 1.1rem;"
          :disabled="emailExists">
          Đăng ký
        </button>
      </form>
      <div v-if="success" class="alert alert-success text-center mt-3">
        ✅ Đăng ký thành công!
      </div>
    </div>
  </div>
</template>

<script>
import axios from 'axios'

export default {
  name: 'RegisterView',
  data() {
    return {
      form: {
        fullName: '',
        address: '',
        phone: '',
        email: '',
        password: '',
        role: 'user'
      },
      success: false,
      emailExists: false
    }
  },
  methods: {
    async checkEmailExist() {
      if (!this.form.email) {
        this.emailExists = false;
        return;
      }
      try {
        // API kiểm tra email tồn tại (ví dụ)
        // Nếu BE trả về null/404 nghĩa là chưa có, ngược lại là đã tồn tại
        const res = await axios.get(`https://localhost:7046/api/account/getByEmail?email=${encodeURIComponent(this.form.email)}`);
        // Nếu tìm thấy thì báo lỗi
        if (res && res.data && res.data.email) {
          this.emailExists = true;
        } else {
          this.emailExists = false;
        }
      } catch (err) {
        // Nếu trả về 404 nghĩa là không tồn tại (tùy backend)
        if (err.response && err.response.status === 404) {
          this.emailExists = false;
        } else {
          this.emailExists = false; // hoặc báo lỗi khác nếu cần
        }
      }
    },
    async register() {
      if (!this.form.email || !this.form.password) {
        alert('Vui lòng nhập email và mật khẩu!');
        return;
      }
      if (this.emailExists) {
        alert('Email đã tồn tại, vui lòng nhập email khác!');
        return;
      }
      try {
        await axios.post('https://localhost:7046/api/account/register', {
          email: this.form.email,
          password: this.form.password
        });
        this.success = true;
      } catch (error) {
        console.error(error);
        alert('❌ Đăng ký thất bại: ' + (error.response?.data?.message || error.message));
      }
    }
  }
}
</script>

<style scoped>
input.form-control:focus {
  border-color: #27ae60;
  box-shadow: 0 0 5px #27ae60;
  transition: 0.2s;
}
.card {
  border-radius: 18px;
  background: #f9fcf7;
}
</style>
