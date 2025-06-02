<template>
  <div class="container mt-4">
    <h3>👤 Quản lý tài khoản</h3>

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
        <tr v-for="user in users" :key="user.accountID">
          <td>{{ user.email }}</td>
          <td>{{ user.role }}</td>
          <td>
            <span :class="user.isActive ? 'text-success fw-bold' : 'text-danger fw-bold'">
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
import axios from 'axios'

export default {
  name: 'AccountManagementView',
  data() {
    return {
      users: []
    }
  },
  created() {
    this.fetchUsers()
  },
  methods: {
    async fetchUsers() {
      try {
        const res = await axios.get('https://localhost:7046/api/Account')
        this.users = res.data
      } catch (err) {
        console.error(err)
        alert('Không thể tải danh sách tài khoản')
      }
    },
    async toggleStatus(user) {
      try {
        await axios.put(`https://localhost:7046/api/Account/${user.accountID}/toggle`)
        user.isActive = !user.isActive
      } catch (err) {
        console.error(err)
        alert('Lỗi khi cập nhật trạng thái')
      }
    },
    async deleteUser(user) {
      if (confirm(`Bạn có chắc muốn xóa tài khoản ${user.email}?`)) {
        try {
          await axios.delete(`https://localhost:7046/api/Account/${user.accountID}`)
          this.fetchUsers()
        } catch (err) {
          console.error(err)
          alert('Không thể xóa tài khoản')
        }
      }
    }
  }
}
</script>
