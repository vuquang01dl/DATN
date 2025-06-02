<template>
  <div class="container mt-5" style="max-width: 400px">
    <h2 class="text-center mb-4">Đăng nhập</h2>

    <div v-if="error" class="alert alert-danger text-center">{{ error }}</div>
    <div v-if="success" class="alert alert-success text-center">✅ Đăng nhập thành công!</div>

    <form @submit.prevent="handleLogin" class="card p-4 shadow">
      <div class="mb-3">
        <label class="form-label">Email</label>
        <input v-model="username" type="text" class="form-control" required />
      </div>

      <div class="mb-3">
        <label class="form-label">Mật khẩu</label>
        <input v-model="password" type="password" class="form-control" required />
      </div>

      <button type="submit" class="btn btn-primary w-100">Đăng nhập</button>
    </form>
  </div>
</template>

<script>
import axios from 'axios'

export default {
  name: "LoginView",
  data() {
    return {
      username: "",
      password: "",
      error: "",
      success: false,
    }
  },
  methods: {
    async handleLogin() {
      this.error = "";
      this.success = false;

      try {
        // Gửi yêu cầu đăng nhập
        const response = await axios.post("https://localhost:7046/api/account/login", {
          email: this.username,
          password: this.password,
        });

        const token = response.data.token;
        localStorage.setItem("token", token);

        // Gọi API /me để lấy thông tin người dùng
        const meResponse = await axios.get("https://localhost:7046/api/account/me", {
          headers: { Authorization: `Bearer ${token}` }
        });

        const userInfo = meResponse.data;
        localStorage.setItem("user", JSON.stringify(userInfo));
        this.success = true;

        // Chuyển hướng dựa trên vai trò (không phân biệt hoa thường)
        const role = userInfo.role?.toLowerCase();
        if (role === "admin") {
          this.$router.push("/admin/accounts");
        } else {
          this.$router.push("/");
        }

        // Reload lại để cập nhật navbar hoặc các phần giao diện khác
        setTimeout(() => {
          location.reload();
        }, 100);

      } catch (err) {
        console.error(err);
        this.error = "❌ Đăng nhập thất bại: " + (err.response?.data?.message || "Sai thông tin hoặc lỗi máy chủ.");
      }
    }
  }
}
</script>
