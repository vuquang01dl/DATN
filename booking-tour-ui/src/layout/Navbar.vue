<template>
  <div class="sidebar-layout">
    <!-- SIDEBAR -->
    <nav class="sidebar shadow">
      <div class="sidebar-header">
        <router-link to="/" class="brand">
          <i class="fa-solid fa-earth-asia me-2"></i>
          Booking Tour
        </router-link>
      </div>
      <ul class="sidebar-nav">

        <!-- Employee chỉ hiện trạng thái tour -->
        <template v-if="user && user.role === 'employee'">
          <li>
            <router-link to="/tourstatus" class="sidebar-link" exact-active-class="active">
              <i class="fa-solid fa-flag-checkered me-2"></i>
              Quản lý trạng thái tour
            </router-link>
          </li>
          <!-- Dropdown tài khoản -->
          <li class="sidebar-dropdown" :class="{open: openDropdown === 'user'}">
            <div class="sidebar-link sidebar-dropdown-toggle" @click="toggleDropdown('user')">
              <i class="fa-solid fa-user-circle me-2"></i> {{ user.username }}
              <i class="fa-solid" :class="openDropdown === 'user' ? 'fa-chevron-up' : 'fa-chevron-down'" style="float:right;"></i>
            </div>
            <ul class="sidebar-dropdown-list custom-dropdown" v-show="openDropdown === 'user'">
              <li>
                <a href="#" class="sidebar-link" @click.prevent="logout">
                  <i class="fa-solid fa-sign-out-alt me-2"></i>Đăng xuất
                </a>
              </li>
            </ul>
          </li>
        </template>

        <!-- Admin đầy đủ menu -->
        <template v-else-if="user && user.role === 'admin'">
          <li>
            <router-link to="/" class="sidebar-link" exact-active-class="active">
              <i class="fa-solid fa-house me-2"></i> Trang chủ
            </router-link>
          </li>
          <li>
            <router-link to="/bookingform" class="sidebar-link" exact-active-class="active">
              <i class="fa-solid fa-calendar-plus me-2"></i> Đặt tour
            </router-link>
          </li>
          <li class="sidebar-dropdown" :class="{open: openDropdown === 'admin'}">
            <div class="sidebar-link sidebar-dropdown-toggle" @click="toggleDropdown('admin')">
              <i class="fa-solid fa-gears me-2"></i> Quản lý (Admin)
              <i class="fa-solid" :class="openDropdown === 'admin' ? 'fa-chevron-up' : 'fa-chevron-down'" style="float:right;"></i>
            </div>
            <ul class="sidebar-dropdown-list custom-dropdown" v-show="openDropdown === 'admin'">
              <li><router-link to="/admin/tours" class="sidebar-link"><i class="fa-solid fa-map me-2"></i>Quản lý tour</router-link></li>
              <li><router-link to="/admin/accounts" class="sidebar-link"><i class="fa-solid fa-users me-2"></i>Quản lý tài khoản</router-link></li>
              <li><router-link to="/admin/bookings" class="sidebar-link"><i class="fa-solid fa-clipboard-list me-2"></i>Quản lý đặt tour</router-link></li>
              <li><router-link to="/tourstatus" class="sidebar-link"><i class="fa-solid fa-flag-checkered me-2"></i>Trạng thái tour</router-link></li>
              <li><router-link to="/admin/assign" class="sidebar-link"><i class="fa-solid fa-user-tie me-2"></i>Phân công nhân viên</router-link></li>
              <li><router-link to="/admin/destinations" class="sidebar-link"><i class="fa-solid fa-location-dot me-2"></i>Quản lý địa điểm</router-link></li>
              <li><router-link to="/admin/hotels" class="sidebar-link"><i class="fa-solid fa-hotel me-2"></i>Quản lý khách sạn</router-link></li>
              <li><router-link to="/admin/customers" class="sidebar-link"><i class="fa-solid fa-user-friends me-2"></i>Quản lý khách hàng</router-link></li>
              <li><router-link to="/admin/permissions" class="sidebar-link"><i class="fa-solid fa-key me-2"></i>Quản lý quyền</router-link></li>
              <li><router-link to="/admin/roles" class="sidebar-link"><i class="fa-solid fa-user-tag me-2"></i>Quản lý vai trò</router-link></li>
              <li><router-link to="/admin/tour-destinations" class="sidebar-link"><i class="fa-solid fa-route me-2"></i>Tour - Điểm đến</router-link></li>
              <li><router-link to="/admin/tour-hotels" class="sidebar-link"><i class="fa-solid fa-hotel me-2"></i>Tour - Khách sạn</router-link></li>
            </ul>
          </li>
          <!-- Dropdown tài khoản -->
          <li class="sidebar-dropdown" :class="{open: openDropdown === 'user'}">
            <div class="sidebar-link sidebar-dropdown-toggle" @click="toggleDropdown('user')">
              <i class="fa-solid fa-user-circle me-2"></i> {{ user.username }}
              <i class="fa-solid" :class="openDropdown === 'user' ? 'fa-chevron-up' : 'fa-chevron-down'" style="float:right;"></i>
            </div>
            <ul class="sidebar-dropdown-list custom-dropdown" v-show="openDropdown === 'user'">
              <li><router-link to="/my-bookings" class="sidebar-link"><i class="fa-solid fa-clock-rotate-left me-2"></i>Lịch sử đặt tour</router-link></li>
              <li><router-link to="/feedback" class="sidebar-link"><i class="fa-solid fa-comment-dots me-2"></i>Gửi phản hồi</router-link></li>
              <li><router-link to="/invoice/123456" class="sidebar-link"><i class="fa-solid fa-file-invoice-dollar me-2"></i>Thanh toán hóa đơn</router-link></li>
              <li><a href="#" class="sidebar-link" @click.prevent="logout"><i class="fa-solid fa-sign-out-alt me-2"></i>Đăng xuất</a></li>
            </ul>
          </li>
        </template>

        <!-- User thường -->
        <template v-else-if="user">
          <li>
            <router-link to="/" class="sidebar-link" exact-active-class="active">
              <i class="fa-solid fa-house me-2"></i> Trang chủ
            </router-link>
          </li>
          <li>
            <router-link to="/bookingform" class="sidebar-link" exact-active-class="active">
              <i class="fa-solid fa-calendar-plus me-2"></i> Đặt tour
            </router-link>
          </li>
          <!-- Dropdown tài khoản -->
          <li class="sidebar-dropdown" :class="{open: openDropdown === 'user'}">
            <div class="sidebar-link sidebar-dropdown-toggle" @click="toggleDropdown('user')">
              <i class="fa-solid fa-user-circle me-2"></i> {{ user.username }}
              <i class="fa-solid" :class="openDropdown === 'user' ? 'fa-chevron-up' : 'fa-chevron-down'" style="float:right;"></i>
            </div>
            <ul class="sidebar-dropdown-list custom-dropdown" v-show="openDropdown === 'user'">
              <li><router-link to="/my-bookings" class="sidebar-link"><i class="fa-solid fa-clock-rotate-left me-2"></i>Lịch sử đặt tour</router-link></li>
              <li><router-link to="/feedback" class="sidebar-link"><i class="fa-solid fa-comment-dots me-2"></i>Gửi phản hồi</router-link></li>
              <li><router-link to="/invoice/123456" class="sidebar-link"><i class="fa-solid fa-file-invoice-dollar me-2"></i>Thanh toán hóa đơn</router-link></li>
              <li><a href="#" class="sidebar-link" @click.prevent="logout"><i class="fa-solid fa-sign-out-alt me-2"></i>Đăng xuất</a></li>
            </ul>
          </li>
        </template>

        <!-- Guest -->
        <template v-else>
          <li>
            <router-link to="/login" class="sidebar-link" exact-active-class="active"><i class="fa-solid fa-sign-in-alt me-2"></i> Đăng nhập</router-link>
          </li>
          <li>
            <router-link to="/register" class="sidebar-link" exact-active-class="active"><i class="fa-solid fa-user-plus me-2"></i> Đăng ký</router-link>
          </li>
        </template>
      </ul>
    </nav>
    <!-- MAIN CONTENT -->
    <div class="main-content">
      <slot />
    </div>
  </div>
</template>

<script>
export default {
  name: "SidebarNavbar",
  data() {
    return {
      user: null,
      openDropdown: null
    };
  },
  mounted() {
    const userData = localStorage.getItem("user");
    if (userData) this.user = JSON.parse(userData);
  },
  methods: {
    logout() {
      localStorage.removeItem("user");
      this.$router.push("/login");
      location.reload();
    },
    toggleDropdown(name) {
      this.openDropdown = this.openDropdown === name ? null : name;
    }
  }
};
</script>

<style scoped>
.sidebar-layout {
  display: flex;
  min-height: 100vh;
}
.sidebar {
  width: 265px;
  background: #202245;
  color: #fff;
  display: flex;
  flex-direction: column;
  min-height: 100vh;
  box-shadow: 2px 0 10px #0002;
  padding-top: 20px;
  position: fixed;
  left: 0; top: 0; bottom: 0;
  z-index: 1000;
}
.sidebar-header {
  padding: 0 24px 20px 24px;
  font-size: 22px;
  font-weight: bold;
  color: #33d3ff;
  margin-bottom: 8px;
}
.brand {
  color: #33d3ff;
  text-decoration: none;
  font-size: 1.2rem;
}
.sidebar-nav {
  list-style: none;
  padding: 0;
  margin: 0;
  flex: 1;
}
.sidebar-link {
  display: flex;
  align-items: center;
  gap: 8px;
  color: #bfc8e2;
  text-decoration: none;
  padding: 12px 28px;
  border-left: 3px solid transparent;
  font-size: 1rem;
  transition: background 0.15s, color 0.2s, border-color 0.2s;
  cursor: pointer;
}
.sidebar-link.active,
.sidebar-link.router-link-exact-active,
.sidebar-link:hover {
  background: #262a4d;
  color: #33d3ff !important;
  border-left: 3px solid #33d3ff;
}
.sidebar-dropdown-toggle {
  cursor: pointer;
  user-select: none;
}
.sidebar-dropdown-list {
  margin-left: 0;
  border-radius: 0 8px 8px 0;
  overflow: hidden;
  transition: max-height 0.3s;
}
.sidebar-dropdown .fa-chevron-down,
.sidebar-dropdown .fa-chevron-up {
  margin-left: auto;
  font-size: 0.9em;
}
.main-content {
  flex: 1;
  margin-left: 265px;
  background: #f4f7fc;
  padding: 0 40px;
  min-height: 100vh;
  box-sizing: border-box;
}

/* --- MÀU DROPDOWN KHÁC BIỆT, DỄ NHÌN --- */
.custom-dropdown {
  background: #e2fbff !important; /* Xanh nhạt khác biệt */
  border-left: 3px solid #33d3ff;
}
.custom-dropdown .sidebar-link {
  color: #085e85 !important;
}
.custom-dropdown .sidebar-link:hover {
  background: #b8f1fa !important;
  color: #174f63 !important;
}

/* Thêm hiệu ứng đóng mở dropdown */
.sidebar-dropdown-list {
  max-height: 0;
  opacity: 0;
  pointer-events: none;
  transition: max-height 0.25s, opacity 0.18s;
}
.sidebar-dropdown.open .sidebar-dropdown-list {
  max-height: 1000px;
  opacity: 1;
  pointer-events: auto;
  transition: max-height 0.45s, opacity 0.22s;
}
.sidebar-dropdown-list .sidebar-link {
  padding-left: 48px;
  font-size: 0.96rem;
}
</style>
