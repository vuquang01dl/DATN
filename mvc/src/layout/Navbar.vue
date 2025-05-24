<template>
  <nav class="navbar navbar-expand-lg custom-navbar shadow-sm">
    <div class="container">
      <router-link to="/" class="navbar-brand fw-bold text-primary">Booking Tour</router-link>

      <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav">
        <span class="navbar-toggler-icon"></span>
      </button>

      <div class="collapse navbar-collapse" id="navbarNav">
        <ul class="navbar-nav ms-auto">
          <li class="nav-item">
            <router-link to="/" class="nav-link" exact-active-class="active-link">Trang chủ</router-link>
          </li>
          <li class="nav-item">
            <router-link to="/bookingform" class="nav-link" exact-active-class="active-link">Đặt tour</router-link>
          </li>

          <!-- Quản lý -->
          <li class="nav-item dropdown" v-if="user?.role === 'Admin'">
            <a class="nav-link dropdown-toggle" href="#" data-bs-toggle="dropdown">Quản lý (Admin)</a>
            <ul class="dropdown-menu">
              <li><router-link to="/admin/tours" class="dropdown-item">Quản lý tour</router-link></li>
              <li><router-link to="/admin/accounts" class="dropdown-item">Quản lý tài khoản</router-link></li>
              <li><router-link to="/admin/bookings" class="dropdown-item">Quản lý đặt tour</router-link></li>
              <li><router-link to="/tourstatus" class="dropdown-item">Trạng thái tour</router-link></li>
              <li><router-link to="/admin/assign" class="dropdown-item">Phân công nhân viên</router-link></li>
              <li><router-link to="/admin/destinations" class="dropdown-item">Quản lý địa điểm</router-link></li>
              <li><router-link to="/admin/hotels" class="dropdown-item">Quản lý khách sạn</router-link></li>

            </ul>
          </li>

          <!-- User -->
          <li class="nav-item dropdown" v-if="user">
            <a class="nav-link dropdown-toggle" href="#" data-bs-toggle="dropdown">{{ user.username }}</a>
            <ul class="dropdown-menu dropdown-menu-end">
              <li><router-link to="/my-bookings" class="dropdown-item">Lịch sử đặt tour</router-link></li>
              <li><hr class="dropdown-divider" /></li>
              <li><a href="#" class="dropdown-item" @click.prevent="logout">Đăng xuất</a></li>
            </ul>
          </li>

          <!-- Guest -->
          <li class="nav-item" v-else>
            <router-link to="/login" class="nav-link" exact-active-class="active-link">Đăng nhập</router-link>
          </li>
          <li class="nav-item" v-if="!user">
            <router-link to="/register" class="nav-link" exact-active-class="active-link">Đăng ký</router-link>
          </li>
        </ul>
      </div>
    </div>
  </nav>
</template>

<script>
export default {
  name: 'MainNavbar',
  data() {
    return {
      user: null
    }
  },
  mounted() {
    const userData = localStorage.getItem("user")
    if (userData) this.user = JSON.parse(userData)
  },
  methods: {
    logout() {
      localStorage.removeItem("user")
      this.$router.push("/login")
      location.reload()
    }
  }
}
</script>

<style scoped>
.custom-navbar {
  background-color: #f8f9fa;
  border-bottom: 1px solid #e2e2e2;
}

.navbar .nav-link {
  color: #333;
  padding: 8px 14px;
  transition: all 0.2s ease-in-out;
  position: relative;
}

.navbar .nav-link:hover {
  color: #0d6efd;
  text-decoration: underline;
}

.navbar .nav-link.active-link {
  color: #0d6efd;
  font-weight: 600;
  text-decoration: underline;
}

.dropdown-menu {
  box-shadow: 0 0.5rem 1rem rgba(0,0,0,0.1);
  border-radius: 8px;
}
</style>
