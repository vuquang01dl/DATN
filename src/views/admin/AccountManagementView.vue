<template>
  <div class="container mt-4">
    <h3>Quản lý tài khoản</h3>

    <table class="table table-bordered mt-3">
      <thead class="table-dark">
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
            <button class="btn btn-sm btn-warning" @click="toggleStatus(user)">Khóa/Mở</button>
            <button class="btn btn-sm btn-danger ms-2" @click="deleteUser(user)">Xóa</button>
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
      users: [
        { id: '1', email: 'admin@gmail.com', role: 'admin', isActive: true },
        { id: '2', email: 'user1@gmail.com', role: 'user', isActive: true },
        { id: '3', email: 'user2@gmail.com', role: 'user', isActive: false }
      ]
    }
  },
  methods: {
    toggleStatus(user) {
      user.isActive = !user.isActive
    },
    deleteUser(user) {
      if (confirm(`Bạn có chắc muốn xóa tài khoản ${user.email}?`)) {
        this.users = this.users.filter(u => u.id !== user.id)
      }
    }
  }
}
</script>
