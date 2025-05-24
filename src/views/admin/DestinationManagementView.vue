<template>
  <div class="container mt-5">
    <h2>Quản lý địa điểm</h2>
    <button class="btn btn-primary mb-3" @click="openAddModal">+ Thêm địa điểm</button>

    <table class="table table-bordered">
      <thead class="table-dark">
        <tr>
          <th>#</th>
          <th>Tên địa điểm</th>
          <th>Miêu tả</th>
          <th>Thao tác</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(d, index) in destinations" :key="d.id">
          <td>{{ index + 1 }}</td>
          <td>{{ d.name }}</td>
          <td>{{ d.description }}</td>
          <td>
            <button class="btn btn-sm btn-warning me-2" @click="openEditModal(d)">Sửa</button>
            <button class="btn btn-sm btn-danger" @click="deleteDestination(d.id)">Xóa</button>
          </td>
        </tr>
      </tbody>
    </table>

    <!-- Modal thêm/sửa -->
    <div v-if="showModal" class="modal-backdrop">
      <div class="modal-dialog">
        <div class="modal-content p-4">
          <h5>{{ isEditing ? 'Sửa địa điểm' : 'Thêm địa điểm' }}</h5>
          <input v-model="form.name" class="form-control mb-2" placeholder="Tên địa điểm" />
          <textarea v-model="form.description" class="form-control mb-3" placeholder="Miêu tả"></textarea>
          <div class="text-end">
            <button class="btn btn-secondary me-2" @click="showModal = false">Hủy</button>
            <button class="btn btn-success" @click="saveDestination">{{ isEditing ? 'Lưu' : 'Thêm' }}</button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
// import axios from 'axios'

export default {
  name: 'DestinationManagementView',
  data() {
    return {
      destinations: [],
      showModal: false,
      isEditing: false,
      form: { id: '', name: '', description: '' }
    }
  },
  mounted() {
    this.loadDestinations()

    // Gọi API nếu đã có backend:
    /*
    axios.get('http://localhost:5017/api/destination')
      .then(res => this.destinations = res.data)
    */
  },
  methods: {
    loadDestinations() {
      this.destinations = [
        { id: 'd1', name: 'Hà Nội', description: 'Thủ đô ngàn năm văn hiến' },
        { id: 'd2', name: 'Đà Nẵng', description: 'Thành phố đáng sống' }
      ]
    },
    openAddModal() {
      this.form = { id: '', name: '', description: '' }
      this.isEditing = false
      this.showModal = true
    },
    openEditModal(destination) {
      this.form = { ...destination }
      this.isEditing = true
      this.showModal = true
    },
    saveDestination() {
      if (!this.form.name || !this.form.description) return alert("Vui lòng nhập đầy đủ thông tin")

      if (this.isEditing) {
        const index = this.destinations.findIndex(d => d.id === this.form.id)
        if (index !== -1) this.destinations[index] = { ...this.form }
        // axios.put(`http://localhost:5017/api/destination/${this.form.id}`, this.form)
      } else {
        const newItem = { ...this.form, id: 'd' + Date.now() }
        this.destinations.push(newItem)
        // axios.post('http://localhost:5017/api/destination', newItem)
      }

      this.showModal = false
    },
    deleteDestination(id) {
      if (!confirm("Bạn có chắc chắn muốn xóa?")) return
      this.destinations = this.destinations.filter(d => d.id !== id)
      // axios.delete(`http://localhost:5017/api/destination/${id}`)
    }
  }
}
</script>

<style scoped>
.modal-backdrop {
  position: fixed;
  inset: 0;
  background: rgba(0, 0, 0, 0.5);
  z-index: 1050;
  display: flex;
  align-items: center;
  justify-content: center;
}
</style>
