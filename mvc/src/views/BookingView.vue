<template>
  <div class="container py-5">
    <h2 class="text-center mb-4">Lịch sử đặt tour của bạn</h2>

    <div v-if="bookings.length > 0" class="row g-4">
      <div class="col-md-6" v-for="booking in bookings" :key="booking.id">
        <div class="card shadow-sm border-0 h-100">
          <div class="card-body">
            <h5 class="card-title">{{ booking.tourName }}</h5>
            <p class="mb-1"><i class="bi bi-calendar-event"></i> Từ: <strong>{{ booking.startDate }}</strong></p>
            <p class="mb-1"><i class="bi bi-calendar-check"></i> Đến: <strong>{{ booking.endDate }}</strong></p>
            <p class="mb-1"><i class="bi bi-people"></i> Số người: <strong>{{ booking.peopleCount }}</strong></p>
            <span :class="['badge', statusClass(booking.status)]">{{ statusLabel(booking.status) }}</span>
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
export default {
  name: 'MyBookingView',
  data() {
    return {
      bookings: [
        {
          id: 1,
          tourName: 'Tour Nha Trang 3N2Đ',
          startDate: '2025-07-01',
          endDate: '2025-07-03',
          peopleCount: 2,
          status: 'confirmed'
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
    }
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
        confirmed: 'bg-success text-white',
        pending: 'bg-warning text-dark',
        canceled: 'bg-danger text-white'
      }[status]
    }
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
