<template>
  <div class="container py-5 account-bg">
    <div class="card shadow-lg border-0 mx-auto animate__animated animate__fadeInDown" style="max-width:900px">
      <div class="card-header bg-gradient-primary text-white d-flex align-items-center" style="height:70px;">
        <h3 class="mb-0"><i class="bi bi-person-badge me-2"></i>Quản lý tài khoản</h3>
      </div>
      <div class="card-body">
        <table class="table table-hover align-middle rounded shadow-sm overflow-hidden">
          <thead class="table-light">
            <tr>
              <th>Email</th>
              <th>Role</th>
              <th>Trạng thái</th>
              <th>Hành động</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="user in users" :key="user.accountID" :class="user.isActive ? 'table-success' : 'table-danger'">
              <td>{{ user.email }}</td>
              <td>
                <span class="badge bg-info text-dark px-3 py-2">{{ user.role }}</span>
              </td>
              <td>
                <span :class="user.isActive ? 'text-success fw-bold pulse-active' : 'text-danger fw-bold pulse-block'">
                  {{ user.isActive ? 'Hoạt động' : 'Bị khóa' }}
                </span>
              </td>
              <td>
                <button
                  class="btn btn-outline-warning btn-sm wave-btn"
                  @click="toggleStatus(user)"
                  :disabled="loading"
                >
                  <i class="bi bi-shield-lock"></i> {{ user.isActive ? 'Khóa' : 'Mở' }}
                </button>
                <button
                  class="btn btn-outline-danger btn-sm ms-2 wave-btn"
                  @click="deleteUser(user)"
                  :disabled="loading"
                >
                  <i class="bi bi-trash"></i> Xóa
                </button>
              </td>
            </tr>
          </tbody>
        </table>
        <!-- Toast Thông báo -->
        <div
          v-if="toastMsg"
          :class="['toast-container position-fixed bottom-0 end-0 p-3', showToast ? 'show' : '']"
          style="z-index:9999;"
        >
          <div class="toast align-items-center text-white" :class="toastType" style="opacity:1;" role="alert">
            <div class="d-flex">
              <div class="toast-body">
                {{ toastMsg }}
              </div>
              <button type="button" class="btn-close btn-close-white me-2 m-auto" @click="showToast=false"></button>
            </div>
          </div>
        </div>
      </div>
    </div>
    <!-- Hiệu ứng sóng nền -->
    <div class="wave-effect"></div>
  </div>
</template>

<script>
import axios from 'axios'

export default {
  name: 'AccountManagementView',
  data() {
    return {
      users: [],
      toastMsg: '',
      showToast: false,
      toastType: 'bg-success',
      loading: false
    }
  },
  created() {
    this.fetchUsers()
  },
  methods: {
    showToastMsg(msg, type = 'bg-success') {
      this.toastMsg = msg
      this.toastType = type
      this.showToast = true
      setTimeout(() => (this.showToast = false), 2500)
    },
    async fetchUsers() {
      try {
        this.loading = true
        const res = await axios.get('https://localhost:7046/api/Account')
        this.users = res.data
      } catch (err) {
        this.showToastMsg('Không thể tải danh sách tài khoản', 'bg-danger')
      } finally {
        this.loading = false
      }
    },
    async toggleStatus(user) {
      try {
        this.loading = true
        await axios.put(`https://localhost:7046/api/Account/${user.accountID}/toggle`)
        user.isActive = !user.isActive
        this.showToastMsg(`Đã ${user.isActive ? 'mở khóa' : 'khóa'} tài khoản thành công!`)
      } catch (err) {
        this.showToastMsg('Lỗi khi cập nhật trạng thái', 'bg-danger')
      } finally {
        this.loading = false
      }
    },
    async deleteUser(user) {
      if (confirm(`Bạn có chắc muốn xóa tài khoản ${user.email}?`)) {
        try {
          this.loading = true
          await axios.delete(`https://localhost:7046/api/Account/${user.accountID}`)
          this.fetchUsers()
          this.showToastMsg('Đã xóa tài khoản thành công!')
        } catch (err) {
          this.showToastMsg('Không thể xóa tài khoản', 'bg-danger')
        } finally {
          this.loading = false
        }
      }
    }
  }
}
</script>

<style scoped>
/* Sóng biển background */
.account-bg {
  background: linear-gradient(135deg, #e0f7fa 0%, #8fd3f4 100%);
  min-height: 100vh;
  position: relative;
}
.card-header.bg-gradient-primary {
  background: linear-gradient(90deg, #36d1c4 0%, #5b86e5 100%)!important;
  box-shadow: 0 6px 24px 0 rgba(44, 170, 226, 0.1);
}
.wave-effect {
  position: absolute;
  left: 0; right: 0; bottom: 0;
  height: 140px;
  background: url('https://svgshare.com/i/13BQ.svg') repeat-x;
  background-size: contain;
  opacity: 0.4;
  pointer-events: none;
  z-index: 0;
}
.table th, .table td {
  vertical-align: middle !important;
}
.table-hover tbody tr:hover {
  background: #fffde4 !important;
  transform: scale(1.01);
  transition: all 0.2s;
}
.badge.bg-info {
  font-size: 1em;
  letter-spacing: 1px;
}
/* Hiệu ứng nút sóng */
.wave-btn {
  position: relative;
  overflow: hidden;
  transition: color 0.15s;
}
.wave-btn:active::after {
  content: '';
  position: absolute;
  left: 50%; top: 50%;
  width: 200%; height: 200%;
  background: rgba(88, 198, 243, 0.18);
  border-radius: 50%;
  transform: translate(-50%, -50%) scale(1.1);
  animation: waveEffect 0.5s;
  z-index: 0;
}
@keyframes waveEffect {
  from { opacity: 0.8; transform: translate(-50%, -50%) scale(0); }
  to   { opacity: 0;   transform: translate(-50%, -50%) scale(1.1);}
}
.pulse-active {
  animation: pulseGreen 1s infinite alternate;
}
.pulse-block {
  animation: pulseRed 1s infinite alternate;
}
@keyframes pulseGreen {
  from { text-shadow: 0 0 0 #63e6be; }
  to   { text-shadow: 0 0 8px #63e6be; }
}
@keyframes pulseRed {
  from { text-shadow: 0 0 0 #f43f5e; }
  to   { text-shadow: 0 0 10px #f43f5e; }
}
/* Toast */
.toast-container .toast {
  border-radius: 12px;
  min-width: 260px;
  box-shadow: 0 4px 18px 0 rgba(44, 170, 226, 0.13);
  font-weight: 500;
}
</style>
