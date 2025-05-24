<template>
  <div class="container py-5">
    <div class="card shadow-sm mx-auto" style="max-width: 700px">
      <div class="card-body">
        <h4 class="mb-4 text-center">💳 Thanh toán hóa đơn</h4>

        <div v-if="invoice">
          <p><strong>Mã hóa đơn:</strong> {{ invoice.invoiceID }}</p>
          <p><strong>Khách hàng:</strong> {{ invoice.customerName }}</p>
          <p><strong>Tour:</strong> {{ invoice.tourName }}</p>
          <p><strong>Ngày đặt:</strong> {{ invoice.date }}</p>
          <p><strong>Tổng tiền:</strong> <span class="text-danger fw-bold">{{ invoice.amount.toLocaleString() }} VND</span></p>

          <div v-if="paymentStatus === null">
            <button class="btn btn-success w-100" @click="payVNPay">Thanh toán qua VNPay</button>
          </div>

          <div v-else-if="paymentStatus === 'success'" class="alert alert-success mt-3">
            ✅ Thanh toán thành công!
          </div>

          <div v-else-if="paymentStatus === 'failed'" class="alert alert-danger mt-3">
            ❌ Thanh toán thất bại. Vui lòng thử lại.
          </div>
        </div>

        <div v-else class="text-center">
          <div class="spinner-border text-primary" role="status"></div>
          <p class="mt-2">Đang tải hóa đơn...</p>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
// import axios from 'axios';
export default {
  name: 'PaymentInvoiceView',
  data() {
    return {
      invoice: null,
      paymentStatus: null
    };
  },
  methods: {
    async fetchInvoice() {
      const invoiceId = this.$route.params.id;

      // Gọi API thật:
      // const res = await axios.get(`http://localhost:5017/api/invoice/${invoiceId}`);
      // this.invoice = res.data;

      // Dữ liệu giả:
      this.invoice = {
        invoiceID: invoiceId,
        customerName: 'Nguyễn Văn A',
        tourName: 'Tour Đà Lạt 3N2Đ',
        date: '2025-07-01',
        amount: 3000000
      };
    },
    async payVNPay() {
      try {
        // Gọi API VNPay (giả lập):
        // const res = await axios.post('http://localhost:5017/api/payment/vnpay', { invoiceID: this.invoice.invoiceID });
        // this.paymentStatus = res.data.status; // 'success' hoặc 'failed'

        // Giả lập kết quả:
        this.paymentStatus = Math.random() > 0.5 ? 'success' : 'failed';
      } catch (error) {
        this.paymentStatus = 'failed';
      }
    }
  },
  mounted() {
    this.fetchInvoice();
  }
};
</script>

<style scoped>
.card {
  border-radius: 1rem;
}
.card-body {
  padding: 2rem;
}
</style>
