<template>
  <div class="container mt-5">
    <h2 class="text-center mb-4">Lịch sử đặt tour</h2>
    <div v-if="bookings.length > 0" class="table-responsive">
      <table class="table table-striped">
        <thead class="table-dark">
          <tr>
            <th>#</th>
            <th>Tên tour</th>
            <th>Ngày bắt đầu</th>
            <th>Ngày kết thúc</th>
            <th>Số người</th>
            <th>Trạng thái</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(booking, index) in bookings" :key="booking.id">
            <td>{{ index + 1 }}</td>
            <td>{{ booking.tourName }}</td>
            <td>{{ booking.startDate }}</td>
            <td>{{ booking.endDate }}</td>
            <td>{{ booking.peopleCount }}</td>
            <td>
              <span :class="statusClass(booking.status)">
                {{ statusLabel(booking.status) }}
              </span>
            </td>
          </tr>
        </tbody>
      </table>
    </div>

    <div v-else class="text-center text-muted">
      <p>Bạn chưa đặt tour nào.</p>
    </div>
  </div>
</template>

<script>
// import axios from 'axios'

export default {
  name: 'BookingView',
  data() {
    return {
      bookings: []
    }
  },
  async mounted() {
    // ✅ Dữ liệu đặt tour giả
    this.bookings = [
      {
        id: 1,
        tourName: 'Tour Nha Trang 3N2Đ',
        startDate: '2025-07-01',
        endDate: '2025-07-03',
        peopleCount: 2,
        status: 'confirmed' // or 'pending' | 'canceled'
      },
      {
        id: 2,
        tourName: 'Tour Hạ Long 2N1Đ',
        startDate: '2025-08-15',
        endDate: '2025-08-16',
        peopleCount: 4,
        status: 'pending'
      }
    ]

    // ❗ Khi có backend:
    /*
    const res = await axios.get('http://localhost:5017/api/booking/user');
    this.bookings = res.data;
    */
  },
  methods: {
    statusLabel(status) {
      switch (status) {
        case 'confirmed': return 'Đã xác nhận'
        case 'pending': return 'Chờ xử lý'
        case 'canceled': return 'Đã hủy'
        default: return 'Không rõ'
      }
    },
    statusClass(status) {
      return {
        'badge bg-success': status === 'confirmed',
        'badge bg-warning text-dark': status === 'pending',
        'badge bg-danger': status === 'canceled'
      }
    }
  }
}
</script>
