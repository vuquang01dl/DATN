<template>
  <div class="container py-5">
    <h2 class="text-center mb-4">Lịch sử đặt tour của bạn</h2>

    <div v-if="bookings.length > 0" class="row g-4">
      <div class="col-md-6" v-for="booking in bookings" :key="booking.id">
        <div class="card shadow-sm border-0 h-100">
          <div class="card-body">
            <h5 class="card-title">{{ booking.tourName }}</h5>
            <p class="mb-1"><i class="bi bi-building"></i> Khách sạn: <strong>{{ booking.hotelName }}</strong></p>
            <p class="mb-1"><i class="bi bi-calendar-event"></i> Từ: <strong>{{ booking.startDate }}</strong></p>
            <p class="mb-1"><i class="bi bi-calendar-check"></i> Đến: <strong>{{ booking.endDate }}</strong></p>
            <p class="mb-1"><i class="bi bi-person-fill"></i> Người lớn: <strong>{{ booking.adult }}</strong></p>
            <p class="mb-1"><i class="bi bi-person"></i> Trẻ em: <strong>{{ booking.child }}</strong></p>
            <p class="mb-1"><i class="bi bi-calendar2-week"></i> Ngày đặt: <strong>{{ booking.createdAt }}</strong></p>
            <p class="mb-1"><i class="bi bi-cash"></i> Tổng giá: <strong>{{ booking.totalPrice.toLocaleString() }} VND</strong></p>
            <span :class="['badge', statusClass(booking)]">{{ statusLabel(booking) }}</span>

            <!-- Nút thanh toán nếu chưa thanh toán -->
            <div class="mt-3">
              <p><strong>Thanh toán:</strong>
                <span v-if="booking.paid" class="text-success">Đã thanh toán</span>
                <span v-else class="text-danger">Chưa thanh toán</span>
              </p>
              <router-link
                v-if="!booking.paid"
                :to="`/invoice/${booking.invoiceId}`"
                class="btn btn-sm btn-success"
              >
                Thanh toán
              </router-link>
            </div>
          </div>
        </div>
      </div>
    </div>

    <div v-else class="text-center text-muted mt-5">
      <p>Bạn chưa đặt tour nào.</p>
    </div>
  </div>
</template>

<script>
// import axios from 'axios';

export default {
  name: 'MyBookingView',
  data() {
    return {
      bookings: []
    }
  },
  methods: {
    statusLabel(booking) {
      if (booking.paid) return 'Đã xác nhận';
      return 'Đang xử lý';
    },
    statusClass(booking) {
      if (booking.paid) return 'bg-success text-white';
      return 'bg-warning text-dark';
    },
    async fetchBookings() {
      // Gọi API backend để lấy danh sách booking của user
      // const res = await axios.get('http://localhost:5017/api/booking/user/00000000-0000-0000-0000-000000000001');
      // this.bookings = res.data;

      // Dữ liệu giả lập:
      this.bookings = [
        {
          id: 1,
          tourName: 'Tour Hà Nội 3N2Đ',
          hotelName: 'Khách sạn A - Hà Nội',
          startDate: '2025-07-01',
          endDate: '2025-07-03',
          adult: 2,
          child: 1,
          totalPrice: 5000000,
          createdAt: '2025-06-20',
          paid: false,
          invoiceId: 'INV001'
        },
        {
          id: 2,
          tourName: 'Tour Đà Nẵng 4N3Đ',
          hotelName: 'Khách sạn B - Đà Nẵng',
          startDate: '2025-08-10',
          endDate: '2025-08-13',
          adult: 3,
          child: 2,
          totalPrice: 7200000,
          createdAt: '2025-07-01',
          paid: true,
          invoiceId: 'INV002'
        }
      ];
    }
  },
  mounted() {
    this.fetchBookings();
  }
}
</script>

<style scoped>
.card-title {
  font-weight: bold;
}
.badge {
  padding: 0.5em 0.75em;
  font-size: 0.9rem;
  border-radius: 0.5rem;
}
</style>
