<template>
  <div class="container mt-5">
    <h2>Đăng ký tài khoản</h2>
    <form @submit.prevent="register">
      <div class="mb-3">
        <label>Email</label>
        <input v-model="form.email" type="email" class="form-control" required>
      </div>
      <div class="mb-3">
        <label>Mật khẩu</label>
        <input v-model="form.password" type="password" class="form-control" required>
      </div>
      <div class="mb-3">
        <label>Vai trò</label>
        <select v-model="form.role" class="form-select" required>
          <option value="">-- Chọn vai trò --</option>
          <option value="user">Người dùng</option>
          <option value="admin">Quản trị viên</option>
        </select>
      </div>
      <button type="submit" class="btn btn-success">Đăng ký</button>
    </form>

    <div v-if="success" class="alert alert-success mt-3">
      ✅ Đăng ký thành công!
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
        email: '',
        password: '',
        role: ''
      },
      success: false
    }
  },
  methods: {
    async register() {
      if (!this.form.email || !this.form.password || !this.form.role) {
        alert('Vui lòng nhập đầy đủ thông tin!');
        return;
      }

      try {
        await axios.post('https://localhost:7046/api/account/register', this.form);
        this.success = true;
      } catch (error) {
        console.error(error);
        alert('❌ Đăng ký thất bại: ' + (error.response?.data?.message || error.message));
      }
    }
  }
}
</script>
