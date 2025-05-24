<template>
  <div class="container py-5">
    <div class="card shadow-sm mx-auto" style="max-width: 700px">
      <div class="card-body">
        <h4 class="text-center mb-4">📋 Đặt Tour</h4>
        <form @submit.prevent="submitBooking">
          <div class="mb-3">
            <label class="form-label fw-semibold">Chọn tour</label>
            <select v-model="form.tourID" class="form-select" required @change="filterHotelsByTour">
              <option disabled value="">-- Chọn tour --</option>
              <option v-for="t in tours" :key="t.id" :value="t.id">{{ t.name }}</option>
            </select>
          </div>

          <div class="mb-3">
            <label class="form-label fw-semibold">Chọn khách sạn</label>
            <select v-model="form.hotelId" class="form-select" required>
              <option disabled value="">-- Chọn khách sạn --</option>
              <option v-for="h in filteredHotels" :key="h.id" :value="h.id">{{ h.name }} - {{ h.location }}</option>
            </select>
          </div>

          <div class="row">
            <div class="col-md-6 mb-3">
              <label class="form-label fw-semibold">Người lớn</label>
              <input v-model.number="form.adult" @input="calculateTotal" type="number" min="0" class="form-control" required />
            </div>
            <div class="col-md-6 mb-3">
              <label class="form-label fw-semibold">Trẻ em</label>
              <input v-model.number="form.child" @input="calculateTotal" type="number" min="0" class="form-control" required />
            </div>
          </div>

          <div class="mb-3">
            <label class="form-label fw-semibold">Tổng giá (VND)</label>
            <input :value="form.totalPrice.toLocaleString()" class="form-control" readonly />
          </div>

          <div class="row">
            <div class="col-md-6 mb-3">
              <label class="form-label fw-semibold">Ngày tạo</label>
              <input v-model="form.createAt" type="date" class="form-control" required />
            </div>
            <div class="col-md-6 mb-3">
              <label class="form-label fw-semibold">Ngày cập nhật</label>
              <input v-model="form.modifyAt" type="date" class="form-control" required />
            </div>
          </div>

          <button type="submit" class="btn btn-primary w-100">🚀 Đặt tour</button>
        </form>
      </div>
    </div>
  </div>
</template>

<script>
// import axios from "axios";

export default {
  data() {
    const today = new Date().toISOString().substr(0, 10);
    return {
      form: {
        bookingID: '',
        tourID: '',
        customerID: '',
        paymentID: null,
        hotelId: '',
        adult: 1,
        child: 0,
        totalPrice: 0,
        createAt: today,
        modifyAt: today
      },
      tours: [],
      hotels: [],
      filteredHotels: [],
      tourHotelMap: {},
      pricePerAdult: 2000000,
      pricePerChild: 1000000
    };
  },
  methods: {
    calculateTotal() {
      this.form.totalPrice = (this.form.adult * this.pricePerAdult) + (this.form.child * this.pricePerChild);
    },
    async submitBooking() {
      const bookingData = { ...this.form };
      console.log("📦 Booking payload:", bookingData);
      // await axios.post("http://localhost:5017/api/booking", bookingData)
      //   .then(res => console.log("✅ Booking created:", res.data))
      //   .catch(err => console.error("❌ Error creating booking:", err));
      alert("✔️ Đặt tour thành công (giả lập)");
    },
    filterHotelsByTour() {
      this.filteredHotels = this.hotels.filter(h => this.tourHotelMap[this.form.tourID]?.includes(h.id));
      this.form.hotelId = ''; // reset hotel selection
    },
    async fetchOptions() {
      // const tourRes = await axios.get("http://localhost:5017/api/tour");
      // this.tours = tourRes.data;
      // const hotelRes = await axios.get("http://localhost:5017/api/hotel");
      // this.hotels = hotelRes.data;
      // const tourHotelRes = await axios.get("http://localhost:5017/api/tourhotel");
      // tourHotelRes.data.forEach(item => {
      //   if (!this.tourHotelMap[item.tourId]) this.tourHotelMap[item.tourId] = [];
      //   this.tourHotelMap[item.tourId].push(item.hotelId);
      // });

      this.tours = [
        { id: 't1', name: 'Tour Hà Nội 3N2Đ' },
        { id: 't2', name: 'Tour Đà Nẵng 4N3Đ' }
      ];
      this.hotels = [
        { id: 'h1', name: 'Khách sạn A', location: 'Hà Nội' },
        { id: 'h2', name: 'Khách sạn B', location: 'Đà Nẵng' },
        { id: 'h3', name: 'Khách sạn C', location: 'Nha Trang' }
      ];
      this.tourHotelMap = {
        't1': ['h1'],
        't2': ['h2', 'h3']
      };
    }
  },
  mounted() {
    this.fetchOptions();
    this.calculateTotal();
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
.form-label {
  font-weight: 600;
}
</style>