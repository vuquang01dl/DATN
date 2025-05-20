<template>
  <div class="container mt-5">
    <h2>Theo dõi trạng thái tour</h2>

    <table class="table table-bordered mt-3">
      <thead>
        <tr>
          <th>Tên tour</th>
          <th>Khách hàng</th>
          <th>Ngày đặt</th>
          <th>Trạng thái</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="status in tourStatuses" :key="status.id">
          <td>{{ status.tourName }}</td>
          <td>{{ status.customerName }}</td>
          <td>{{ status.bookingDate }}</td>
          <td>
            <span :class="status.status === 'Đang diễn ra' ? 'text-primary' : status.status === 'Đã kết thúc' ? 'text-success' : 'text-warning'">
              {{ status.status }}
            </span>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
// import axios from 'axios'

export default {
  name: 'TourStatusView',
  data() {
    return {
      tourStatuses: []
    }
  },
  async mounted() {
    // ❌ Khi chưa kết nối backend, dùng dữ liệu giả:
    this.tourStatuses = [
      {
        id: 1,
        tourName: 'Tour Đà Lạt',
        customerName: 'Nguyễn Văn A',
        bookingDate: '2025-06-01',
        status: 'Chưa bắt đầu'
      },
      {
        id: 2,
        tourName: 'Tour Phú Quốc',
        customerName: 'Trần Thị B',
        bookingDate: '2025-06-03',
        status: 'Đang diễn ra'
      },
      {
        id: 3,
        tourName: 'Tour Sapa',
        customerName: 'Lê Văn C',
        bookingDate: '2025-06-05',
        status: 'Đã kết thúc'
      }
    ]

    // ✅ Khi đã có backend, bạn chỉ cần bỏ comment:
    /*
    const res = await axios.get('http://localhost:5017/api/tourstatus');
    this.tourStatuses = res.data;
    */
  }
}
</script>
