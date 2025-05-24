<template>
  <div class="container py-5">
    <h2 class="text-center mb-4">Quản lý vai trò</h2>

    <form @submit.prevent="addRole" class="row g-2 mb-4">
      <div class="col-md-6">
        <input v-model="form.name" class="form-control" placeholder="Tên vai trò" required />
      </div>
      <div class="col-md-6">
        <input v-model="form.description" class="form-control" placeholder="Mô tả" required />
      </div>
      <div class="col-12">
        <button class="btn btn-primary w-100">Thêm vai trò</button>
      </div>
    </form>

    <table class="table table-bordered">
      <thead>
        <tr>
          <th>Tên</th>
          <th>Mô tả</th>
          <th>Hành động</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="r in roles" :key="r.roleID">
          <td>{{ r.name }}</td>
          <td>{{ r.description }}</td>
          <td>
            <button class="btn btn-sm btn-outline-primary me-2" @click="startEdit(r)">Sửa</button>
            <button class="btn btn-sm btn-outline-danger" @click="deleteRole(r.roleID)">Xoá</button>
          </td>
        </tr>
      </tbody>
    </table>

    <div v-if="editingRole" class="mt-4">
      <h5>Sửa vai trò</h5>
      <input v-model="editForm.name" class="form-control mb-2" placeholder="Tên" />
      <input v-model="editForm.description" class="form-control mb-2" placeholder="Mô tả" />
      <button class="btn btn-success btn-sm" @click="confirmEdit">Lưu</button>
      <button class="btn btn-secondary btn-sm" @click="cancelEdit">Huỷ</button>
    </div>
  </div>
</template>

<script>
//import axios from 'axios';

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
      // const res = await axios.get('http://localhost:5017/api/role');
      // this.roles = res.data;
      this.roles = [
        { roleID: '1', name: 'Admin', description: 'Quản trị viên' },
        { roleID: '2', name: 'User', description: 'Người dùng' }
      ];
    },
    async addRole() {
      const newRole = { ...this.form };
      console.log('🆕 Tạo vai trò:', newRole);
      // await axios.post('http://localhost:5017/api/role', newRole);
      this.roles.push({ ...newRole, roleID: Date.now().toString() });
      this.form = { name: '', description: '' };
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
      const updated = { ...this.editForm };
      console.log('✏️ Cập nhật vai trò:', updated);
      // await axios.put(`http://localhost:5017/api/role/${updated.roleID}`, updated);
      Object.assign(this.editingRole, updated);
      this.editingRole = null;
    },
    cancelEdit() {
      this.editingRole = null;
    },
    async deleteRole(id) {
      console.log('🗑 Xoá vai trò:', id);
      // await axios.delete(`http://localhost:5017/api/role/${id}`);
      this.roles = this.roles.filter(r => r.roleID !== id);
    }
  },
  mounted() {
    this.fetchRoles();
  }
};
</script>

<style scoped>
.table td, .table th {
  vertical-align: middle;
}
</style>
