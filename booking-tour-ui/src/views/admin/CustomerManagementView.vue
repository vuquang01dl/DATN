<template>
  <div class="blue-bg min-vh-100 py-5">
    <div class="container">
      <h2 class="blue-title text-center mb-4">
        <i class="bi bi-people"></i> Quản lý khách hàng
      </h2>

      <!-- Tìm kiếm -->
      <div class="mb-4">
        <input
          v-model="search"
          class="form-control search-input rounded-pill shadow-sm"
          placeholder="🔍 Tìm kiếm theo tên, email hoặc số điện thoại..."
        />
      </div>

      <!-- Form thêm -->
      <form @submit.prevent="addCustomer" class="card form-card shadow-sm mb-4 border-0 rounded-4 px-4 py-3">
        <div class="row g-2">
          <div class="col-md-2">
            <input v-model="form.firstName" class="form-control rounded-pill" placeholder="Họ" required />
          </div>
          <div class="col-md-2">
            <input v-model="form.lastName" class="form-control rounded-pill" placeholder="Tên" required />
          </div>
          <div class="col-md-3">
            <input v-model="form.email" type="email" class="form-control rounded-pill" placeholder="Email" required />
          </div>
          <div class="col-md-3">
            <input v-model="form.address" class="form-control rounded-pill" placeholder="Địa chỉ" required />
          </div>
          <div class="col-md-2">
            <input v-model="form.phone" class="form-control rounded-pill" placeholder="Số điện thoại" required />
          </div>
          <div class="col-12 mt-3">
            <button class="btn btn-blue-lg rounded-pill w-100">
              <i class="bi bi-person-plus"></i> Thêm khách hàng
            </button>
          </div>
        </div>
      </form>

      <!-- Danh sách -->
      <div class="card border-0 shadow-sm rounded-4">
        <div class="table-responsive">
          <table class="table table-hover align-middle mb-0">
            <thead class="table-blue">
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
                <td><b>{{ c.firstName }} {{ c.lastName }}</b></td>
                <td>{{ c.email }}</td>
                <td>{{ c.address }}</td>
                <td>{{ c.phone }}</td>
                <td>
                  <button class="btn btn-outline-blue btn-sm me-2 rounded-pill px-3" @click="startEdit(c)">
                    <i class="bi bi-pencil"></i> Sửa
                  </button>
                  <button class="btn btn-outline-danger btn-sm rounded-pill px-3" @click="deleteCustomer(c.customerID)">
                    <i class="bi bi-trash"></i> Xoá
                  </button>
                </td>
              </tr>
              <tr v-if="!filteredCustomers.length">
                <td colspan="5" class="text-center text-secondary">Không có khách hàng phù hợp</td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>

      <!-- Form sửa -->
      <transition name="fade">
        <div v-if="editingCustomer" class="card px-4 py-4 mt-4 border-0 rounded-4 shadow-sm edit-form animate__animated animate__zoomIn">
          <h5 class="mb-3">
            <i class="bi bi-pencil-square"></i> Sửa khách hàng
          </h5>
          <div class="row g-2">
            <div class="col-md-2">
              <input v-model="editForm.firstName" class="form-control rounded-pill mb-2" placeholder="Họ" />
            </div>
            <div class="col-md-2">
              <input v-model="editForm.lastName" class="form-control rounded-pill mb-2" placeholder="Tên" />
            </div>
            <div class="col-md-3">
              <input v-model="editForm.email" class="form-control rounded-pill mb-2" placeholder="Email" />
            </div>
            <div class="col-md-3">
              <input v-model="editForm.address" class="form-control rounded-pill mb-2" placeholder="Địa chỉ" />
            </div>
            <div class="col-md-2">
              <input v-model="editForm.phone" class="form-control rounded-pill mb-2" placeholder="Số điện thoại" />
            </div>
          </div>
          <div class="d-flex gap-2 mt-3 justify-content-end">
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
import axios from 'axios';

export default {
  name: 'CustomerManagementView',
  data() {
    return {
      customers: [],
      search: '',
      form: {
        customerID: '',
        firstName: '',
        lastName: '',
        email: '',
        address: '',
        phone: '',
        image: 'abc'
      },
      editingCustomer: null,
      editForm: {
        customerID: '',
        firstName: '',
        lastName: '',
        email: '',
        address: '',
        phone: '',
        image: 'abc'
      }
    };
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
      try {
        const res = await axios.get('https://localhost:7046/api/customer');
        this.customers = res.data;
      } catch (error) {
        alert('❌ Không thể tải danh sách khách hàng.');
        console.error(error);
      }
    },
    async addCustomer() {
      try {
        this.form.email = this.form.email.trim().toLowerCase();
        this.form.customerID = crypto.randomUUID();
        const payload = { ...this.form };
        await axios.post('https://localhost:7046/api/customer', payload);
        this.resetForm();
        await this.fetchCustomers();
      } catch (error) {
        const msg = error.response?.data?.message || error.message;
        alert('❌ Thêm thất bại: ' + msg);
        console.error(error);
      }
    },
    startEdit(customer) {
      this.editingCustomer = customer;
      this.editForm = { ...customer };
    },
    async confirmEdit() {
      try {
        this.editForm.email = this.editForm.email.trim().toLowerCase();
        await axios.put(`https://localhost:7046/api/customer/${this.editForm.customerID}`, this.editForm);
        this.editingCustomer = null;
        await this.fetchCustomers();
      } catch (error) {
        const msg = error.response?.data?.message || error.message;
        alert('❌ Cập nhật thất bại: ' + msg);
        console.error(error);
      }
    },
    cancelEdit() {
      this.editingCustomer = null;
    },
    async deleteCustomer(id) {
      if (!confirm('Bạn chắc chắn muốn xoá khách hàng này?')) return;
      try {
        await axios.delete(`https://localhost:7046/api/customer/${id}`);
        await this.fetchCustomers();
      } catch (error) {
        alert('❌ Xoá thất bại.');
        console.error(error);
      }
    },
    resetForm() {
      this.form = {
        customerID: '',
        firstName: '',
        lastName: '',
        email: '',
        address: '',
        phone: '',
        image: 'abc'
      };
    }
  },
  mounted() {
    this.fetchCustomers();
  }
};
</script>

<style scoped>
@import 'https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css';

.blue-bg {
  min-height: 100vh;
  background: linear-gradient(120deg, #e2f0ff 0%, #f8fdff 100%);
}
.blue-title {
  font-size: 2.2rem;
  font-weight: bold;
  color: #237edb;
  letter-spacing: 1px;
  text-shadow: 0 2px 8px #d6e8fa;
}
.search-input {
  background: #f4faff;
  border: 2px solid #237edb22;
  font-size: 1.13rem;
  padding: 10px 22px;
  box-shadow: 0 2px 10px #aacbff22;
}
.search-input:focus {
  background: #e2f0ff;
  border: 2px solid #237edb66;
}
.form-card {
  background: #f4faff;
  border: none !important;
  border-radius: 1.3rem !important;
  box-shadow: 0 2px 10px #aacbff22;
}
.btn-blue-lg,
.btn-outline-blue {
  background: #237edb !important;
  color: #fff !important;
  font-weight: 600;
  font-size: 1.1rem;
  border: none;
  box-shadow: 0 2px 10px #aacbff44;
}
.btn-blue-lg:hover {
  background: #4593e6 !important;
}
.btn-outline-blue {
  background: transparent !important;
  color: #237edb !important;
  border: 2px solid #237edb !important;
  transition: background 0.15s;
}
.btn-outline-blue:hover {
  background: #237edb !important;
  color: #fff !important;
}
.table {
  border-radius: 1.3rem;
  overflow: hidden;
}
.table th, .table td {
  vertical-align: middle;
}
.table-blue {
  background: #e2f0ff;
  color: #237edb;
  font-weight: 600;
  letter-spacing: 0.5px;
}
.edit-form {
  background: #f2faff;
}
input, .form-control, .search-input {
  border-radius: 2rem !important;
  border: 2px solid #237edb22 !important;
  font-size: 1.09rem;
  min-height: 2.6rem;
  box-shadow: 0 2px 8px #aacbff22;
  transition: border 0.15s, background 0.15s;
}
input:focus, .form-control:focus, .search-input:focus {
  border: 2px solid #237edb77 !important;
  background: #e2f0ff !important;
}
</style>

<!--
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.1/font/bootstrap-icons.css">
-->
