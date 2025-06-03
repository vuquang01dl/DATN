<template>
  <div class="permission-bg min-vh-100 py-5">
    <div class="container">
      <div class="glass-card mx-auto animate__animated animate__fadeInDown">
        <h2 class="gradient-text text-center mb-4">
          <i class="bi bi-shield-lock-fill"></i> Quản lý quyền truy cập
        </h2>

        <!-- Thêm quyền -->
        <form @submit.prevent="addPermission" class="row g-2 mb-4">
          <div class="col-md-5">
            <input v-model="form.value" class="form-control input-effect" placeholder="Giá trị quyền (Value)" required />
          </div>
          <div class="col-md-5">
            <input v-model="form.description" class="form-control input-effect" placeholder="Mô tả quyền" required />
          </div>
          <div class="col-md-2 d-flex">
            <button class="btn btn-gradient flex-grow-1" :disabled="!form.value || !form.description">
              <i class="bi bi-plus-circle"></i> Thêm quyền
            </button>
          </div>
        </form>

        <!-- Hiệu ứng khi thêm quyền -->
        <transition name="fade">
          <div v-if="showAdded" class="alert alert-success animate__animated animate__fadeIn mb-3 text-center shadow">
            <i class="bi bi-check2-circle"></i> Đã thêm quyền thành công!
          </div>
        </transition>

        <!-- Bảng quyền -->
        <div class="table-responsive">
          <table class="table table-hover shadow-sm rounded-4 overflow-hidden">
            <thead class="table-light">
              <tr>
                <th class="w-25">Giá trị</th>
                <th>Mô tả</th>
                <th class="text-center" style="width:120px;">Hành động</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="p in permissions" :key="p.value" class="table-row-effect">
                <td><span class="badge bg-primary bg-gradient rounded-pill px-3 py-2">{{ p.value }}</span></td>
                <td>{{ p.description }}</td>
                <td class="text-center">
                  <button class="btn btn-sm btn-outline-info me-2" @click="startEdit(p)">
                    <i class="bi bi-pencil-square"></i>
                  </button>
                  <button class="btn btn-sm btn-outline-danger" @click="deletePermission(p.value)">
                    <i class="bi bi-trash"></i>
                  </button>
                </td>
              </tr>
              <tr v-if="permissions.length === 0">
                <td colspan="3" class="text-center text-muted py-3">
                  <i class="bi bi-emoji-frown"></i> Chưa có quyền nào!
                </td>
              </tr>
            </tbody>
          </table>
        </div>

        <!-- Popup sửa quyền -->
        <transition name="slide-down">
          <div v-if="editingPermission" class="edit-popup mt-4 animate__animated animate__fadeInUp">
            <div class="card shadow rounded-4 p-4 bg-light border-0">
              <h5 class="mb-3 gradient-text"><i class="bi bi-pencil-fill"></i> Sửa quyền</h5>
              <div class="mb-2">
                <input v-model="editForm.value" class="form-control" placeholder="Giá trị quyền (Value)" disabled />
              </div>
              <div class="mb-3">
                <input v-model="editForm.description" class="form-control" placeholder="Mô tả quyền" />
              </div>
              <div class="d-flex gap-2">
                <button class="btn btn-success flex-grow-1" @click="confirmEdit">
                  <i class="bi bi-check2-square"></i> Lưu
                </button>
                <button class="btn btn-secondary flex-grow-1" @click="cancelEdit">
                  <i class="bi bi-x-square"></i> Huỷ
                </button>
              </div>
            </div>
          </div>
        </transition>
      </div>
    </div>
  </div>
</template>

<script>
// import axios from 'axios';
export default {
  data() {
    return {
      permissions: [],
      form: { value: '', description: '' },
      editingPermission: null,
      editForm: { value: '', description: '' },
      showAdded: false,
      addedTimeout: null
    };
  },
  methods: {
    async fetchPermissions() {
      // const res = await axios.get('http://localhost:5017/api/permission');
      // this.permissions = res.data;
      this.permissions = [
        { value: 'ADMIN', description: 'Quản trị hệ thống' },
        { value: 'USER', description: 'Người dùng thường' }
      ];
    },
    addPermission() {
      const newPermission = { ...this.form };
      this.permissions.push(newPermission);
      this.form = { value: '', description: '' };

      // Hiệu ứng thông báo
      this.showAdded = true;
      clearTimeout(this.addedTimeout);
      this.addedTimeout = setTimeout(() => {
        this.showAdded = false;
      }, 1700);
    },
    startEdit(p) {
      this.editingPermission = p;
      this.editForm = { value: p.value, description: p.description };
    },
    confirmEdit() {
      Object.assign(this.editingPermission, this.editForm);
      this.editingPermission = null;
    },
    cancelEdit() {
      this.editingPermission = null;
    },
    deletePermission(value) {
      this.permissions = this.permissions.filter(p => p.value !== value);
    }
  },
  mounted() {
    this.fetchPermissions();
  }
};
</script>

<style scoped>
@import "https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css";
@import "https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.1/font/bootstrap-icons.css";

.permission-bg {
  background: linear-gradient(135deg, #e0f7fa 0%, #c7d2fe 100%);
  min-height: 100vh;
}

.glass-card {
  background: rgba(255, 255, 255, 0.95);
  border-radius: 2rem;
  padding: 2.4rem 2.1rem 2.2rem 2.1rem;
  box-shadow: 0 10px 38px #38b6ff22, 0 1.5px 20px #b5e6ff44;
  max-width: 680px;
}

.gradient-text {
  background: linear-gradient(90deg,#45c7fe,#735cff 80%);
  font-weight: 900;
  font-size: 2rem;
  -webkit-background-clip: text;
  -webkit-text-fill-color: transparent;
  letter-spacing: 1px;
  margin-bottom: 2rem;
  display: inline-block;
}

.input-effect {
  border-radius: 1.4rem;
  border: 2px solid #bce1fd;
  font-size: 1.08rem;
  background: #f7fbff;
  box-shadow: 0 2px 8px #e2f8ff16;
  padding: 0.84rem 1.1rem;
  transition: border 0.24s, box-shadow 0.22s;
}
.input-effect:focus {
  border: 2px solid #735cff;
  background: #f0e9ff;
  box-shadow: 0 0 0 0.13rem #ede3fd73;
  outline: none;
}

.btn-gradient {
  background: linear-gradient(90deg, #5ee7df 10%, #b490ca 100%);
  color: #252525;
  font-weight: 700;
  border: none;
  border-radius: 1.3rem;
  transition: background 0.2s, color 0.15s, transform 0.15s;
}
.btn-gradient:hover, .btn-gradient:focus {
  background: linear-gradient(90deg, #b490ca 10%, #5ee7df 100%);
  color: #171769;
  transform: translateY(-2px) scale(1.04);
  box-shadow: 0 5px 18px #735cff18;
}

.table {
  background: white;
  border-radius: 1.2rem;
  overflow: hidden;
  margin-bottom: 0;
}
.table thead {
  background: #f4f8fc;
}
.table-hover tbody tr:hover {
  background: #e3f0ff9c;
  transition: background 0.22s;
}
.table-row-effect {
  transition: box-shadow 0.23s, transform 0.16s;
}
.table-row-effect:hover {
  box-shadow: 0 6px 22px #b5e6ff55;
  transform: scale(1.02);
}

.badge.bg-primary {
  background: linear-gradient(90deg, #45c7fe, #735cff 70%);
  color: #fff;
  font-size: 1.09rem;
  box-shadow: 0 2px 6px #735cff16;
}

.edit-popup {
  max-width: 420px;
  margin-left: auto;
  margin-right: auto;
}

.fade-enter-active, .fade-leave-active {
  transition: opacity .6s cubic-bezier(0.23,1,0.32,1);
}
.fade-enter, .fade-leave-to {
  opacity: 0;
}

.slide-down-enter-active {
  animation: slideDownIn .7s;
}
.slide-down-leave-active {
  animation: slideDownOut .7s;
}
@keyframes slideDownIn {
  0% { transform: translateY(-40px); opacity: 0; }
  100% { transform: translateY(0); opacity: 1; }
}
@keyframes slideDownOut {
  0% { transform: translateY(0); opacity: 1; }
  100% { transform: translateY(-40px); opacity: 0; }
}

@media (max-width: 650px) {
  .glass-card { padding: 1rem 0.4rem; border-radius: 1.1rem; }
  .gradient-text { font-size: 1.3rem; }
  .edit-popup { padding: 0 2vw; }
}
</style>
