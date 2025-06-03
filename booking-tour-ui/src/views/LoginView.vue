<template>
  <div class="login-bg d-flex align-items-center justify-content-center min-vh-100">
    <div class="login-card glass shadow-lg animate__animated animate__fadeInDown">
      <h2 class="gradient-text text-center mb-4">
        <i class="bi bi-shield-lock"></i> Đăng nhập
      </h2>

      <transition name="shake">
        <div v-if="error" class="alert alert-danger text-center animate__animated animate__shakeX">
          {{ error }}
        </div>
      </transition>
      <transition name="fade">
        <div v-if="success" class="alert alert-success text-center animate__animated animate__fadeIn">
          ✅ Đăng nhập thành công!
        </div>
      </transition>

      <form @submit.prevent="handleLogin" autocomplete="off">
        <div class="mb-3 input-group">
          <span class="input-group-text bg-white border-end-0">
            <i class="bi bi-envelope-fill"></i>
          </span>
          <input v-model="username" type="text" class="form-control input-effect border-start-0"
            placeholder="Email đăng nhập" required />
        </div>

        <div class="mb-4 input-group">
          <span class="input-group-text bg-white border-end-0">
            <i class="bi bi-lock-fill"></i>
          </span>
          <input v-model="password" type="password" class="form-control input-effect border-start-0"
            placeholder="Mật khẩu" required />
        </div>

        <button type="submit" class="btn btn-gradient w-100 py-2 fw-bold">
          <i class="bi bi-box-arrow-in-right"></i> Đăng nhập
        </button>
      </form>
    </div>
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
        setTimeout(() => { location.reload(); }, 100);
      } catch (err) {
        console.error(err);
        this.error = "❌ Đăng nhập thất bại: " + (err.response?.data?.message || "Sai thông tin hoặc lỗi máy chủ.");
      }
    }
  }
}
</script>

<style scoped>
@import "https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css";
@import "https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.1/font/bootstrap-icons.css";

.login-bg {
  background: linear-gradient(135deg, #eef9ff 0%, #e7d9ff 100%);
  min-height: 100vh;
}

.login-card {
  max-width: 380px;
  width: 97vw;
  padding: 2.3rem 2rem 2.1rem 2rem;
  border-radius: 2.3rem;
  background: rgba(255, 255, 255, 0.98);
  box-shadow: 0 8px 40px #7161e955, 0 1.5px 20px #b5e6ff44;
  margin-top: 2.2rem;
}

.gradient-text {
  font-weight: 900;
  font-size: 2rem;
  background: linear-gradient(90deg,#7367F0,#32cfff 80%);
  -webkit-background-clip: text;
  -webkit-text-fill-color: transparent;
  margin-bottom: 2rem;
  letter-spacing: 1px;
}

.input-effect {
  border-radius: 1.3rem !important;
  border: 2px solid #a5f0ff;
  background: #f6fafe !important;
  font-size: 1.09rem;
  transition: border 0.21s, background 0.12s;
  box-shadow: 0 2px 8px #c2e8ff14;
  padding: 0.98rem 1.13rem;
}
.input-effect:focus {
  border: 2px solid #7367F0;
  background: #fff5ff !important;
  outline: none;
}

.input-group-text {
  border-radius: 1.3rem 0 0 1.3rem !important;
  border: 2px solid #a5f0ff;
  border-right: none;
  background: #fff !important;
}

.btn-gradient {
  background: linear-gradient(90deg, #7ee8fa 20%, #eec0c6 100%);
  color: #2e225f;
  border: none;
  border-radius: 1.7rem;
  transition: background 0.18s, color 0.12s, transform 0.12s;
}
.btn-gradient:hover, .btn-gradient:focus {
  background: linear-gradient(90deg, #eec0c6 10%, #7ee8fa 100%);
  color: #5b3cf0;
  transform: scale(1.04) translateY(-1px);
  box-shadow: 0 5px 16px #b7b6f733;
}

.alert {
  border-radius: 1rem;
  font-size: 1rem;
}

.fade-enter-active, .fade-leave-active {
  transition: opacity .7s cubic-bezier(0.23,1,0.32,1);
}
.fade-enter, .fade-leave-to {
  opacity: 0;
}

.shake-enter-active {
  animation: shakeX 1.1s;
}
@keyframes shakeX {
  10%, 90% { transform: translateX(-2px);}
  20%, 80% { transform: translateX(3px);}
  30%, 50%, 70% { transform: translateX(-6px);}
  40%, 60% { transform: translateX(6px);}
}

@media (max-width: 500px) {
  .login-card { padding: 1.1rem .6rem; border-radius: 1.2rem;}
  .gradient-text { font-size: 1.18rem; }
}
</style>
