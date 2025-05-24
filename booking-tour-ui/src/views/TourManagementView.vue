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
          <th>Địa điểm</th>
          <th>Ngày bắt đầu</th>
          <th>Ngày kết thúc</th>
          <th>Hành động</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="tour in tours" :key="tour.id">
          <td>{{ tour.name }}</td>
          <td>{{ tour.price }}</td>
          <td>{{ tour.description }}</td>
          <td>{{ tour.destinationId }}</td>
          <td>{{ tour.startDate }}</td>
          <td>{{ tour.endDate }}</td>
          <td>
            <button class="btn btn-sm btn-warning me-2" @click="editTour(tour)">Sửa</button>
            <button class="btn btn-sm btn-danger" @click="deleteTour(tour.id)">Xóa</button>
          </td>
        </tr>
      </tbody>
    </table>

    <FormTourModal :visible="showModal" :tour="editingTour" @save="handleSaveTour" @close="showModal = false" />
  </div>
</template>

<script>
// import axios from 'axios'
import FormTourModal from '../components/FormTourModal.vue'

export default {
  name: 'TourManagementView',
  components: { FormTourModal },
  data() {
    return {
      tours: [],
      showModal: false,
      editingTour: null
    }
  },
  mounted() {
    this.fetchTours();
  },
  methods: {
    fetchTours() {
      this.tours = [
        {
          id: '1',
          name: 'Tour Đà Lạt',
          price: 2000000,
          description: 'Khám phá cao nguyên',
          destinationId: 'd1',
          startDate: '2025-06-01',
          endDate: '2025-06-05',
          hotelIds: ['h1']
        }
      ];
      // axios.get('http://localhost:5017/api/tour').then(res => this.tours = res.data)
    },
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
      // axios.delete(`http://localhost:5017/api/tour/${id}`).then(() => this.fetchTours())
    },
    handleSaveTour(tour) {
      if (tour.id) {
        const idx = this.tours.findIndex(t => t.id === tour.id)
        if (idx !== -1) this.tours[idx] = { ...tour }
        // axios.put(`http://localhost:5017/api/tour/${tour.id}`, tour).then(() => this.fetchTours())
      } else {
        tour.id = String(Date.now())
        this.tours.push({ ...tour })
        // axios.post('http://localhost:5017/api/tour', tour).then(() => this.fetchTours())
      }
      this.showModal = false
    }
  }
}
</script>