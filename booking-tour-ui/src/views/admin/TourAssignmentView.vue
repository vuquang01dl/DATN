<template>
  <div class="container mt-5">
    <h2>Phân công nhân viên dẫn tour</h2>

    <table class="table table-bordered mt-3">
      <thead>
        <tr>
          <th>Tên tour</th>
          <th>Nhân viên</th>
          <th>Trưởng đoàn</th>
          <th>Hành động</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="assign in assignments" :key="assign.id">
          <td>{{ assign.tourName }}</td>
          <td>{{ assign.employeeName }}</td>
          <td>
            <span :class="assign.isLeader ? 'text-success' : 'text-muted'">
              {{ assign.isLeader ? 'Có' : 'Không' }}
            </span>
          </td>
          <td>
            <button class="btn btn-sm btn-danger" @click="removeAssignment(assign.id)">Xóa</button>
          </td>
        </tr>
      </tbody>
    </table>

    <!-- Nút thêm -->
    <button class="btn btn-primary mt-3" @click="showAddModal = true">+ Phân công mới</button>

    <!-- Modal thêm mới -->
    <div v-if="showAddModal" class="modal-backdrop">
      <div class="modal-dialog">
        <div class="modal-content p-4">
          <h5 class="modal-title">Phân công mới</h5>

          <div class="form-group mb-2">
            <label>Tour</label>
            <select class="form-select" v-model="newAssign.tourId">
              <option v-for="tour in tours" :key="tour.id" :value="tour.id">
                {{ tour.name }}
              </option>
            </select>
          </div>

          <div class="form-group mb-2">
            <label>Nhân viên</label>
            <select class="form-select" v-model="newAssign.employeeId">
              <option v-for="emp in employees" :key="emp.id" :value="emp.id">
                {{ emp.name }}
              </option>
            </select>
          </div>

          <div class="form-check mb-3">
            <input class="form-check-input" type="checkbox" v-model="newAssign.isLeader" id="leaderCheck">
            <label class="form-check-label" for="leaderCheck">Là trưởng đoàn</label>
          </div>

          <div class="text-end">
            <button class="btn btn-secondary me-2" @click="showAddModal = false">Hủy</button>
            <button class="btn btn-success" @click="addAssignment">Thêm</button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
// import axios from 'axios'

export default {
  name: 'TourAssignmentView',
  data() {
    return {
      showAddModal: false,
      assignments: [],
      tours: [],
      employees: [],
      newAssign: {
        tourId: '',
        employeeId: '',
        isLeader: false
      }
    }
  },
  mounted() {
    this.loadAssignments()

    // Gọi API nếu backend chạy:
    /*
    axios.get('http://localhost:5017/api/touremployee')
      .then(res => this.assignments = res.data)
    axios.get('http://localhost:5017/api/tour')
      .then(res => this.tours = res.data)
    axios.get('http://localhost:5017/api/employee')
      .then(res => this.employees = res.data)
    */
  },
  methods: {
    loadAssignments() {
      // Dữ liệu giả
      this.tours = [
        { id: 't1', name: 'Tour Đà Lạt 3N2Đ' },
        { id: 't2', name: 'Tour Phú Quốc 4N3Đ' }
      ]
      this.employees = [
        { id: 'e1', name: 'Nguyễn Văn A' },
        { id: 'e2', name: 'Trần Thị B' }
      ]
      this.assignments = [
        { id: 'a1', tourId: 't1', employeeId: 'e1', tourName: 'Tour Đà Lạt 3N2Đ', employeeName: 'Nguyễn Văn A', isLeader: true },
        { id: 'a2', tourId: 't2', employeeId: 'e2', tourName: 'Tour Phú Quốc 4N3Đ', employeeName: 'Trần Thị B', isLeader: false }
      ]
    },
    addAssignment() {
      const tour = this.tours.find(t => t.id === this.newAssign.tourId)
      const emp = this.employees.find(e => e.id === this.newAssign.employeeId)

      if (!tour || !emp) {
        alert("Vui lòng chọn tour và nhân viên.")
        return
      }

      const newItem = {
        id: 'a' + (this.assignments.length + 1),
        ...this.newAssign,
        tourName: tour.name,
        employeeName: emp.name
      }

      this.assignments.push(newItem)
      this.newAssign = { tourId: '', employeeId: '', isLeader: false }
      this.showAddModal = false

      // Gửi API nếu có backend:
      /*
      axios.post('http://localhost:5017/api/touremployee', newItem)
        .then(() => alert("Phân công thành công"))
        .catch(() => alert("Lỗi phân công"))
      */
    },
    removeAssignment(id) {
      this.assignments = this.assignments.filter(a => a.id !== id)

      // Gọi API xóa nếu có backend:
      /*
      axios.delete(`http://localhost:5017/api/touremployee/${id}`)
        .then(() => alert("Đã xóa"))
        .catch(() => alert("Lỗi xóa phân công"))
      */
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
