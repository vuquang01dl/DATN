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
    register() {
      if (!this.form.email || !this.form.password || !this.form.role) {
        alert('Vui lòng nhập đầy đủ thông tin!');
        return;
      }

      // ✅ Dữ liệu giả để kiểm thử
      console.log('Đã gửi thông tin đăng ký:', this.form);

      // ❗️Khi backend sẵn sàng thì bật đoạn axios sau:
      /*
      axios.post('http://localhost:5017/api/account/register', this.form)
        .then(() => this.success = true)
        .catch(err => alert('Lỗi: ' + err));
      */

      // ✅ Hiển thị thành công luôn khi chưa có backend
      this.success = true;
    }
  }
}
</script>
