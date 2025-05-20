<template>
  <div class="container mt-5">
    <h2 class="mb-4 text-center">Đặt tour</h2>

    <!-- ✅ Thông báo lỗi -->
    <div v-if="error" class="alert alert-danger">
      {{ error }}
    </div>

    <!-- ✅ Thông báo thành công -->
    <div v-if="success" class="alert alert-success">
      ✅ Đặt tour thành công!
    </div>

    <form @submit.prevent="submitBooking" class="card p-4 shadow">
      <div class="mb-3">
        <label class="form-label">Họ tên</label>
        <input v-model="form.customerName" type="text" class="form-control" required />
      </div>

      <div class="mb-3">
        <label class="form-label">Số điện thoại</label>
        <input v-model="form.phone" type="tel" class="form-control" required />
      </div>

      <div class="mb-3">
        <label class="form-label">Chọn tour</label>
        <select v-model="form.tourId" class="form-select" required>
          <option disabled value="">-- Chọn một tour --</option>
          <option v-for="tour in tours" :key="tour.id" :value="tour.id">
            {{ tour.name }} - {{ tour.price }} VND
          </option>
        </select>
      </div>

      <button type="submit" class="btn btn-success w-100">Đặt tour</button>
    </form>
  </div>
</template>

<script>
export default {
  name: "BookingForm",
  data() {
    return {
      form: {
        customerName: "",
        phone: "",
        tourId: ""
      },
      tours: [
        { id: "1", name: "Tour Đà Lạt 3N2Đ", price: 2500000 },
        { id: "2", name: "Tour Phú Quốc 4N3Đ", price: 3500000 }
      ],
      error: "",
      success: false
    };
  },
  methods: {
    submitBooking() {
      this.error = "";
      this.success = false;

      // ✅ Kiểm tra dữ liệu có bị thiếu không
      if (!this.form.customerName || !this.form.phone || !this.form.tourId) {
        this.error = "Vui lòng điền đầy đủ thông tin trước khi đặt tour.";
        return;
      }

      // ✅ Giả lập đặt tour thành công
      console.log("Đặt tour:", this.form);
      this.success = true;

      // ✅ Reset form nếu muốn
      this.form = {
        customerName: "",
        phone: "",
        tourId: ""
      };
    }
  }
};
</script>
