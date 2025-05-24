<template>
  <div class="container mt-5">
    <h2>Quản lý đơn đặt tour</h2>
    <table class="table table-bordered mt-3">
      <thead>
        <tr>
          <th>Khách hàng</th>
          <th>Tour</th>
          <th>Ngày đặt</th>
          <th>Trạng thái</th>
          <th>Thao tác</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="booking in bookings" :key="booking.id">
          <td>{{ booking.customerName }}</td>
          <td>{{ booking.tourName }}</td>
          <td>{{ booking.bookingDate }}</td>
          <td>
            <span :class="booking.status === 'Đã xác nhận' ? 'text-success' : 'text-warning'">
              {{ booking.status }}
            </span>
          </td>
          <td>
            <button class="btn btn-info btn-sm me-2" @click="showDetail(booking)">Chi tiết</button>
            <button v-if="booking.status !== 'Đã xác nhận'" class="btn btn-sm btn-primary me-2" @click="confirmBooking(booking)">Xác nhận</button>
            <button class="btn btn-sm btn-danger" @click="deleteBooking(booking)">Xóa</button>
          </td>
        </tr>
      </tbody>
    </table>

    <!-- ✅ Gọi modal -->
    <BookingDetailModal
      :visible="showDetailModal"
      :booking="selectedBooking"
      @close="showDetailModal = false"
    />
  </div>
</template>

<script>
import BookingDetailModal from '../../components/BookingDetailModal.vue'

export default {
  name: "BookingManagementView",
  components: {
    BookingDetailModal
  },
  data() {
    return {
      bookings: [],
      showDetailModal: false,
      selectedBooking: null
    }
  },
  mounted() {
    // Dữ liệu giả
    this.bookings = [
      {
        id: 'b1',
        customerName: 'Nguyễn Văn A',
        email: 'vana@example.com',
        phone: '0123456789',
        tourName: 'Tour Đà Lạt',
        bookingDate: '2025-06-01',
        people: 2,
        note: 'Ăn chay',
        status: 'Chờ xác nhận'
      },
      {
        id: 'b2',
        customerName: 'Trần Thị B',
        email: 'tranb@example.com',
        phone: '0987654321',
        tourName: 'Tour Phú Quốc',
        bookingDate: '2025-06-03',
        people: 4,
        note: '',
        status: 'Đã xác nhận'
      }
    ]
  },
  methods: {
    confirmBooking(booking) {
      booking.status = 'Đã xác nhận'
      alert('✅ Đã xác nhận đơn đặt tour!')
    },
    deleteBooking(booking) {
      this.bookings = this.bookings.filter(b => b.id !== booking.id)
      alert('🗑 Đã xóa đơn đặt tour!')
    },
    showDetail(booking) {
      this.selectedBooking = booking
      this.showDetailModal = true
    }
  }
}
</script>
