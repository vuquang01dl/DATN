<template>
    <div class="container mt-5" style="max-width: 400px">
        <h2 class="text-center mb-4">Đăng nhập</h2>

        <div v-if="error" class="alert alert-danger text-center">{{ error }}</div>
        <div v-if="success" class="alert alert-success text-center">✅ Đăng nhập thành công!</div>

        <form @submit.prevent="handleLogin" class="card p-4 shadow">
            <div class="mb-3">
                <label class="form-label">Tên đăng nhập</label>
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
        handleLogin() {
            this.error = ""
            this.success = false

            const fakeUsers = [
                { username: "admin", password: "123456", role: "Admin" }, // 👈 Đúng role viết hoa
                { username: "user", password: "123456", role: "User" }
            ]

            const found = fakeUsers.find(
                (u) => u.username === this.username && u.password === this.password
            )

            if (found) {
                localStorage.setItem("user", JSON.stringify(found))
                this.success = true

                if (found.role === "Admin") {
                    this.$router.push("/admin/accounts")
                } else {
                    this.$router.push("/")
                }

                // ✅ Reload để navbar cập nhật
                setTimeout(() => {
                    location.reload()
                }, 100)

            } else {
                this.error = "Tên đăng nhập hoặc mật khẩu không đúng!"
            }
        }

    }
}
</script>
