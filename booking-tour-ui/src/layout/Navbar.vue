<template>
    <nav class="navbar navbar-expand-lg navbar-dark bg-dark">
        <div class="container">
            <router-link to="/" class="navbar-brand">Booking Tour</router-link>

            <!-- Nút toggle cho mobile -->
            <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav"
                aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>

            <!-- Menu chính -->
            <div class="collapse navbar-collapse" id="navbarNav">
                <ul class="navbar-nav ms-auto">
                    <li class="nav-item">
                        <router-link to="/" class="nav-link">Trang chủ</router-link>
                    </li>

                    <li class="nav-item">
                        <router-link to="/bookingform" class="nav-link">Đặt tour</router-link>
                    </li>

                    <!-- 🔐 Chỉ hiển thị với Admin -->
                    <li class="nav-item" v-if="user?.role === 'Admin'">
                        <router-link to="/admin/accounts" class="nav-link">Quản lý tài khoản</router-link>
                        <router-link to="/admin/tours" class="nav-link">Quản lý tour</router-link>
                        <router-link to="/admin/accounts" class="nav-link">Quản lý tài khoản</router-link>
                        <router-link to="/admin/bookings" class="nav-link">Quản lý đặt tour</router-link>
                        <router-link to="/tourstatus" class="nav-link">Trạng thái tour</router-link>

                    </li>

                    <!-- ✅ Dropdown nếu đã đăng nhập -->
                    <li v-if="user" class="nav-item dropdown">
                        <a class="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown"
                            aria-expanded="false">
                            {{ user.username }}
                        </a>
                        <ul class="dropdown-menu dropdown-menu-end">
                            <li>
                                <router-link to="/my-bookings" class="dropdown-item">Lịch sử đặt tour</router-link>
                            </li>
                            <li>
                                <hr class="dropdown-divider" />
                            </li>
                            <li><a class="dropdown-item" href="#" @click.prevent="logout">Đăng xuất</a></li>

                        </ul>
                    </li>

                    <!-- 🔓 Nếu chưa đăng nhập -->
                    <li class="nav-item" v-else>
                        <router-link to="/login" class="nav-link">Đăng nhập</router-link>
                    </li>
                    <li class="nav-item" v-if="!user">
                        <router-link to="/register" class="nav-link">Đăng ký</router-link>
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
        const userData = localStorage.getItem("user");
        if (userData) {
            this.user = JSON.parse(userData);
        }
    },
    methods: {
        logout() {
            localStorage.removeItem("user");
            this.$router.push("/login");
            location.reload();
        }
    }
};
</script>
