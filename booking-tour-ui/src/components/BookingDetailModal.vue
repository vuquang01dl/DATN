<template>
  <div v-if="visible" class="modal-overlay">
    <div class="modal-container">
      <div class="modal-content p-4 bg-white rounded shadow">
        <h5 class="modal-title mb-3">Chi tiết đơn đặt tour</h5>

        <div v-if="booking">
          <p><strong>Tên khách hàng:</strong> {{ booking.customerName }}</p>
          <p><strong>Email:</strong> {{ booking.email }}</p>
          <p><strong>Số điện thoại:</strong> {{ booking.phone }}</p>
          <p><strong>Tên tour:</strong> {{ booking.tourName }}</p>
          <p><strong>Ngày đặt:</strong> {{ booking.bookingDate }}</p>
          <p><strong>Số người:</strong> {{ booking.people }}</p>
          <p><strong>Ghi chú:</strong> {{ booking.note || 'Không có' }}</p>
          <p><strong>Trạng thái:</strong>
            <span :class="booking.status === 'Đã xác nhận' ? 'text-success' : 'text-warning'">
              {{ booking.status }}
            </span>
          </p>
        </div>

        <div class="text-end mt-3">
          <button class="btn btn-secondary" @click="$emit('close')">Đóng</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'BookingDetailModal',
  props: {
    visible: Boolean,
    booking: Object
  }
}
</script>

<style scoped>
.modal-overlay {
  position: fixed;
  top: 0;
  left: 0;
  z-index: 1050;
  width: 100%;
  height: 100%;
  background: rgba(0, 0, 0, 0.6);
  display: flex;
  align-items: center;
  justify-content: center;
}

.modal-container {
  width: 100%;
  max-width: 600px;
  background: white;
  border-radius: 8px;
  padding: 1.5rem;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.3);
}
</style>
