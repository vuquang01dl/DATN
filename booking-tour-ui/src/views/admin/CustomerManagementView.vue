<template>
  <div class="container py-5">
    <h2 class="text-center mb-4">Quản lý khách hàng</h2>

    <div class="mb-3">
      <input v-model="search" class="form-control" placeholder="Tìm kiếm theo tên, email hoặc số điện thoại..." />
    </div>

    <form @submit.prevent="addCustomer" class="row g-2 mb-4">
      <div class="col-md-3">
        <input v-model="form.firstName" class="form-control" placeholder="Họ" required />
      </div>
      <div class="col-md-3">
        <input v-model="form.lastName" class="form-control" placeholder="Tên" required />
      </div>
      <div class="col-md-3">
        <input v-model="form.email" type="email" class="form-control" placeholder="Email" required />
      </div>
      <div class="col-md-3">
        <button class="btn btn-primary w-100">Thêm khách hàng</button>
      </div>
    </form>

    <table class="table table-bordered">
      <thead>
        <tr>
          <th>Họ tên</th>
          <th>Email</th>
          <th>Địa chỉ</th>
          <th>Điện thoại</th>
          <th>Hành động</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="c in filteredCustomers" :key="c.customerID">
          <td>{{ c.firstName }} {{ c.lastName }}</td>
          <td>{{ c.email }}</td>
          <td>{{ c.address }}</td>
          <td>{{ c.phone }}</td>
          <td>
            <button class="btn btn-sm btn-outline-primary me-2" @click="startEdit(c)">Sửa</button>
            <button class="btn btn-sm btn-outline-danger" @click="deleteCustomer(c.customerID)">Xoá</button>
          </td>
        </tr>
      </tbody>
    </table>

    <div v-if="editingCustomer" class="mt-4">
      <h5>Sửa khách hàng</h5>
      <input v-model="editForm.firstName" class="form-control mb-2" placeholder="Họ" />
      <input v-model="editForm.lastName" class="form-control mb-2" placeholder="Tên" />
      <input v-model="editForm.email" class="form-control mb-2" placeholder="Email" />
      <input v-model="editForm.address" class="form-control mb-2" placeholder="Địa chỉ" />
      <input v-model="editForm.phone" class="form-control mb-2" placeholder="Điện thoại" />
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
      customers: [],
      search: '',
      form: {
        firstName: '',
        lastName: '',
        email: '',
        address: '',
        phone: ''
      },
      editingCustomer: null,
      editForm: {
        firstName: '',
        lastName: '',
        email: '',
        address: '',
        phone: ''
      }
    }
  },
  computed: {
    filteredCustomers() {
      const s = this.search.toLowerCase();
      return this.customers.filter(c =>
        c.firstName.toLowerCase().includes(s) ||
        c.lastName.toLowerCase().includes(s) ||
        c.email.toLowerCase().includes(s) ||
        c.phone.includes(s)
      );
    }
  },
  methods: {
    async fetchCustomers() {
      // const res = await axios.get('http://localhost:5017/api/customer');
      // this.customers = res.data;

      this.customers = [
        {
          customerID: '1',
          firstName: 'Nguyễn',
          lastName: 'Văn A',
          email: 'a@gmail.com',
          address: 'Hà Nội',
          phone: '0123456789'
        },
        {
          customerID: '2',
          firstName: 'Trần',
          lastName: 'Thị B',
          email: 'b@gmail.com',
          address: 'TP.HCM',
          phone: '0987654321'
        }
      ];
    },
    async addCustomer() {
      const newCustomer = { ...this.form };
      console.log('🆕 Gửi dữ liệu tạo:', newCustomer);
      // await axios.post('http://localhost:5017/api/customer', newCustomer);
      this.customers.push({ ...newCustomer, customerID: Date.now().toString() });
      this.form = { firstName: '', lastName: '', email: '', address: '', phone: '' };
    },
    startEdit(c) {
      this.editingCustomer = c;
      this.editForm = {
        firstName: c.firstName,
        lastName: c.lastName,
        email: c.email,
        address: c.address,
        phone: c.phone
      };
    },
    async confirmEdit() {
      const updated = { ...this.editForm, customerID: this.editingCustomer.customerID };
      console.log('✏️ Gửi dữ liệu cập nhật:', updated);
      // await axios.put(`http://localhost:5017/api/customer/${updated.customerID}`, updated);
      Object.assign(this.editingCustomer, updated);
      this.editingCustomer = null;
    },
    cancelEdit() {
      this.editingCustomer = null;
    },
    async deleteCustomer(id) {
      console.log('🗑 Xoá khách hàng ID:', id);
      // await axios.delete(`http://localhost:5017/api/customer/${id}`);
      this.customers = this.customers.filter(c => c.customerID !== id);
    }
  },
  mounted() {
    this.fetchCustomers();
  }
}
</script>

<style scoped>
.table td, .table th {
  vertical-align: middle;
}
</style>
