<template>
  <div class="container mt-4">
    <h2>Quản lý Tour</h2>
    <button class="btn btn-primary mb-3" @click="openAddModal">➕ Thêm tour mới</button>

    <table class="table">
      <thead>
        <tr>
          <th>Tên</th>
          <th>Giá</th>
          <th>Mô tả</th>
          <th>Hành động</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="tour in tours" :key="tour.id">
          <td>{{ tour.name }}</td>
          <td>{{ tour.price }}</td>
          <td>{{ tour.description }}</td>
          <td>
            <button class="btn btn-sm btn-warning me-2" @click="editTour(tour)">Sửa</button>
            <button class="btn btn-sm btn-danger" @click="deleteTour(tour.id)">Xóa</button>
          </td>
        </tr>
      </tbody>
    </table>

    <FormTourModal :visible="showModal" :tour="selectedTour" @save="saveTour" @close="showModal = false" />
  </div>
</template>

<script>
import FormTourModal from '../components/FormTourModal.vue'

export default {
  name: 'TourManagementView',
  components: { FormTourModal },
  data() {
    return {
      tours: [
        { id: '1', name: 'Tour Đà Lạt', price: 2000000, description: 'Khám phá cao nguyên' }
      ],
      showModal: false,
      editingTour: null
    }
  },
  methods: {
    openAddModal() {
      this.editingTour = null
      this.showModal = true
    },
    editTour(tour) {
      this.editingTour = { ...tour }
      this.showModal = true
    },
    deleteTour(id) {
      this.tours = this.tours.filter(t => t.id !== id)
    },
    handleSaveTour(tour) {
      if (tour.id) {
        const idx = this.tours.findIndex(t => t.id === tour.id)
        if (idx !== -1) this.tours[idx] = { ...tour }
      } else {
        tour.id = String(Date.now())
        this.tours.push({ ...tour })
      }
      this.showModal = false
    }
  }
}
</script>
