<template>
  <div class="container py-5">
    <h2 class="text-center mb-4">Quản lý quyền truy cập</h2>

    <form @submit.prevent="addPermission" class="row g-2 mb-4">
      <div class="col-md-6">
        <input v-model="form.value" class="form-control" placeholder="Giá trị quyền (Value)" required />
      </div>
      <div class="col-md-6">
        <input v-model="form.description" class="form-control" placeholder="Mô tả quyền" required />
      </div>
      <div class="col-12">
        <button class="btn btn-primary w-100">Thêm quyền</button>
      </div>
    </form>

    <table class="table table-bordered">
      <thead>
        <tr>
          <th>Giá trị</th>
          <th>Mô tả</th>
          <th>Hành động</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="p in permissions" :key="p.value">
          <td>{{ p.value }}</td>
          <td>{{ p.description }}</td>
          <td>
            <button class="btn btn-sm btn-outline-primary me-2" @click="startEdit(p)">Sửa</button>
            <button class="btn btn-sm btn-outline-danger" @click="deletePermission(p.value)">Xoá</button>
          </td>
        </tr>
      </tbody>
    </table>

    <div v-if="editingPermission" class="mt-4">
      <h5>Sửa quyền</h5>
      <input v-model="editForm.value" class="form-control mb-2" placeholder="Giá trị quyền (Value)" disabled />
      <input v-model="editForm.description" class="form-control mb-2" placeholder="Mô tả quyền" />
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
      permissions: [],
      form: {
        value: '',
        description: ''
      },
      editingPermission: null,
      editForm: {
        value: '',
        description: ''
      }
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
    async addPermission() {
      const newPermission = { ...this.form };
      console.log('🆕 Tạo quyền:', newPermission);
      // await axios.post('http://localhost:5017/api/permission', newPermission);
      this.permissions.push({ ...newPermission });
      this.form = { value: '', description: '' };
    },
    startEdit(p) {
      this.editingPermission = p;
      this.editForm = { value: p.value, description: p.description };
    },
    async confirmEdit() {
      const updated = { ...this.editForm };
      console.log('✏️ Cập nhật quyền:', updated);
      // await axios.put(`http://localhost:5017/api/permission/${updated.value}`, updated);
      Object.assign(this.editingPermission, updated);
      this.editingPermission = null;
    },
    cancelEdit() {
      this.editingPermission = null;
    },
    async deletePermission(value) {
      console.log('🗑 Xoá quyền:', value);
      // await axios.delete(`http://localhost:5017/api/permission/${value}`);
      this.permissions = this.permissions.filter(p => p.value !== value);
    }
  },
  mounted() {
    this.fetchPermissions();
  }
};
</script>

<style scoped>
.table td, .table th {
  vertical-align: middle;
}
</style>
