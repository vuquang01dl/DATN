<template>
  <div class="container mt-5">
    <h2>Quản lý tài khoản</h2>
    <table class="table table-bordered mt-3">
      <thead>
        <tr>
          <th>Email</th>
          <th>Role</th>
          <th>Trạng thái</th>
          <th>Hành động</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="user in users" :key="user.id">
          <td>{{ user.email }}</td>
          <td>{{ user.role }}</td>
          <td>
            <span :class="user.isActive ? 'text-success' : 'text-danger'">
              {{ user.isActive ? 'Hoạt động' : 'Bị khóa' }}
            </span>
          </td>
          <td>
            <button class="btn btn-sm btn-warning" @click="toggleActive(user)">
              {{ user.isActive ? 'Khóa' : 'Mở khóa' }}
            </button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
export default {
  name: 'AccountManagementView',
  data() {
    return {
      users: []
    }
  },
  mounted() {
    // Dữ liệu giả
    this.users = [
      { id: '1', email: 'admin@gmail.com', role: 'Admin', isActive: true },
      { id: '2', email: 'user@gmail.com', role: 'User', isActive: false }
    ]

    // Khi backend sẵn sàng, bạn dùng:
    /*
    const res = await axios.get('http://localhost:5017/api/account');
    this.users = res.data;
    */
  },
  methods: {
    toggleActive(user) {
      user.isActive = !user.isActive
      // Gửi API nếu backend đã có
      // await axios.put(`http://localhost:5017/api/account/${user.id}`, { isActive: user.isActive })
    }
  }
}
</script>
