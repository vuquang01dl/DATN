<template>
  <div class="role-bg min-vh-100 py-5">
    <div class="container">
      <h2 class="role-title text-center mb-4">
        <i class="bi bi-shield-lock"></i> Quản lý vai trò
      </h2>

      <!-- Form thêm -->
      <transition name="fade">
        <form @submit.prevent="addRole"
              class="card shadow-sm px-4 py-4 mb-5 border-0 rounded-4 form-card animate__animated animate__fadeInDown">
          <div class="row g-2">
            <div class="col-md-5">
              <input v-model="form.name" class="form-control form-control-lg rounded-pill" placeholder="Tên vai trò" required />
            </div>
            <div class="col-md-5">
              <input v-model="form.description" class="form-control form-control-lg rounded-pill" placeholder="Mô tả" required />
            </div>
            <div class="col-md-2 d-grid">
              <button class="btn btn-blue rounded-pill btn-lg">Thêm vai trò</button>
            </div>
          </div>
        </form>
      </transition>

      <!-- Bảng vai trò -->
      <div class="card shadow-sm border-0 rounded-4 mb-5 animate__animated animate__fadeInUp">
        <div class="table-responsive">
          <table class="table table-hover align-middle mb-0">
            <thead class="table-blue">
              <tr>
                <th class="text-center">Tên</th>
                <th class="text-center">Mô tả</th>
                <th class="text-center">Hành động</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="r in roles" :key="r.roleID" :class="{ 'table-active': editingRole && editingRole.roleID === r.roleID }">
                <td class="text-center">{{ r.name }}</td>
                <td class="text-center">{{ r.description }}</td>
                <td class="text-center">
                  <button class="btn btn-outline-blue btn-sm me-2 rounded-pill px-3"
                          @click="startEdit(r)">
                    <i class="bi bi-pencil"></i> Sửa
                  </button>
                  <button class="btn btn-outline-danger btn-sm rounded-pill px-3"
                          @click="deleteRole(r.roleID)">
                    <i class="bi bi-trash"></i> Xoá
                  </button>
                </td>
              </tr>
              <tr v-if="!roles.length">
                <td colspan="3" class="text-center text-secondary">Chưa có dữ liệu</td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>

      <!-- Form sửa -->
      <transition name="fade">
        <div v-if="editingRole" class="card px-4 py-4 mb-5 border-0 rounded-4 shadow-sm animate__animated animate__zoomIn">
          <h5 class="mb-3"><i class="bi bi-pencil-square"></i> Sửa vai trò</h5>
          <div class="row g-2 mb-3">
            <div class="col-md-6">
              <input v-model="editForm.name" class="form-control form-control-lg rounded-pill" placeholder="Tên" required />
            </div>
            <div class="col-md-6">
              <input v-model="editForm.description" class="form-control form-control-lg rounded-pill" placeholder="Mô tả" required />
            </div>
          </div>
          <div class="d-flex gap-3 justify-content-end">
            <button class="btn btn-success rounded-pill px-4" @click="confirmEdit">
              <i class="bi bi-check-lg"></i> Lưu
            </button>
            <button class="btn btn-secondary rounded-pill px-4" @click="cancelEdit">
              <i class="bi bi-x-lg"></i> Huỷ
            </button>
          </div>
        </div>
      </transition>
    </div>
  </div>
</template>

<script>
import axios from 'axios'

export default {
  data() {
    return {
      roles: [],
      form: {
        name: '',
        description: ''
      },
      editingRole: null,
      editForm: {
        roleID: '',
        name: '',
        description: ''
      }
    };
  },
  methods: {
    async fetchRoles() {
      const res = await axios.get('https://localhost:7046/api/role');
      this.roles = res.data;
    },
    async addRole() {
      try {
        const newRole = { ...this.form };
        await axios.post('https://localhost:7046/api/role', newRole);
        this.form = { name: '', description: '' };
        await this.fetchRoles();
      } catch (err) {
        alert('Thêm vai trò thất bại!');
      }
    },
    startEdit(role) {
      this.editingRole = role;
      this.editForm = {
        roleID: role.roleID,
        name: role.name,
        description: role.description
      };
    },
    async confirmEdit() {
      try {
        const updated = { ...this.editForm };
        await axios.put(`https://localhost:7046/api/role/${updated.roleID}`, updated);
        this.editingRole = null;
        await this.fetchRoles();
      } catch (err) {
        alert('Cập nhật thất bại!');
      }
    },
    cancelEdit() {
      this.editingRole = null;
    },
    async deleteRole(id) {
      if (!confirm('Bạn chắc chắn muốn xóa?')) return;
      try {
        await axios.delete(`https://localhost:7046/api/role/${id}`);
        await this.fetchRoles();
      } catch (err) {
        alert('Xóa thất bại!');
      }
    }
  },
  mounted() {
    this.fetchRoles();
  }
};
</script>

<style scoped>
@import 'https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css';

.role-bg {
  background: linear-gradient(120deg, #e5f3ff 0%, #f8fdff 100%);
  min-height: 100vh;
}
.role-title {
  font-size: 2.2rem;
  font-weight: bold;
  color: #246bb2;
  letter-spacing: 1px;
  text-shadow: 0 2px 8px #d6e8fa;
}
.form-card {
  background: #f5faff;
  border: none !important;
}
.btn-blue, .btn-outline-blue {
  background: #246bb2 !important;
  color: #fff !important;
  font-weight: 600;
  border: none;
}
.btn-outline-blue {
  background: transparent !important;
  color: #246bb2 !important;
  border: 2px solid #246bb2 !important;
  transition: background 0.15s;
}
.btn-outline-blue:hover {
  background: #246bb2 !important;
  color: #fff !important;
}
.form-control, .form-select {
  border-radius: 2rem !important;
  border: 2px solid #246bb222 !important;
  font-size: 1.09rem;
  min-height: 2.6rem;
  box-shadow: 0 2px 8px #b6d5fa24;
}
.card {
  border-radius: 1.3rem !important;
  border: none !important;
}
.table {
  border-radius: 1.3rem;
  overflow: hidden;
}
.table th, .table td {
  vertical-align: middle;
}
.table-blue {
  background: #e5f3ff;
  color: #246bb2;
  font-weight: 600;
  letter-spacing: 0.5px;
}
.table-active td {
  background: #f1f8ff !important;
  font-weight: 600;
}
</style>

<!--
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.1/font/bootstrap-icons.css">
-->
