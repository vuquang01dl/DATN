<template>
  <div class="booking-bg-sea">
    <div class="sea-waves">
      <svg viewBox="0 0 1200 120" preserveAspectRatio="none">
        <path
          d="M0,64L40,69.3C80,75,160,85,240,80C320,75,400,53,480,58.7C560,64,640,96,720,101.3C800,107,880,85,960,74.7C1040,64,1120,64,1200,58.7V120H0V64Z"
          fill="#8fe3ff" fill-opacity="1">
          <animate attributeName="d" dur="8s" repeatCount="indefinite"
            values="M0,64L40,69.3C80,75,160,85,240,80C320,75,400,53,480,58.7C560,64,640,96,720,101.3C800,107,880,85,960,74.7C1040,64,1120,64,1200,58.7V120H0V64Z;
                    M0,74L40,74C80,74,160,90,240,100C320,110,400,105,480,96C560,87,640,65,720,63C800,61,880,83,960,98C1040,113,1120,109,1200,100V120H0V74Z;
                    M0,64L40,69.3C80,75,160,85,240,80C320,75,400,53,480,58.7C560,64,640,96,720,101.3C800,107,880,85,960,74.7C1040,64,1120,64,1200,58.7V120H0V64Z"/>
        </path>
      </svg>
    </div>
    <div class="booking-form-wrapper">
      <div class="glass-card animate__animated animate__fadeInDown">
        <h2 class="gradient-text text-center mb-4">
          <i class="bi bi-umbrella-fill"></i> Đặt Tour Nghỉ Dưỡng
        </h2>
        <form @submit.prevent="submitBooking" autocomplete="off">
          <div class="form-group mb-3">
            <label class="form-label">Email khách hàng</label>
            <input v-model.trim="email"
              type="email"
              class="form-control input-effect"
              required
              placeholder="Nhập email khách hàng (đã đăng ký)" />
          </div>
          <div class="form-group mb-3">
            <label class="form-label">Chọn tour</label>
            <select v-model="form.tourName" class="form-select input-effect" required @change="filterHotelsByTour">
              <option disabled value="">-- Chọn tour --</option>
              <option v-for="t in tours" :key="t.id" :value="t.name">
                {{ t.name }} ({{ t.location }})
              </option>
            </select>
          </div>
          <div class="form-group mb-3">
            <label class="form-label">Chọn khách sạn</label>
            <select v-model="form.hotelName" class="form-select input-effect" required>
              <option disabled value="">-- Chọn khách sạn --</option>
              <option v-for="h in filteredHotels" :key="h.hotelID" :value="h.name">
                {{ h.name }} - {{ h.city }}
              </option>
            </select>
          </div>
          <div class="row mb-3">
            <div class="col-md-6 mb-2">
              <label class="form-label">Người lớn</label>
              <input v-model.number="form.adult" @input="calculateTotal" type="number" min="1"
                     class="form-control input-effect" required />
            </div>
            <div class="col-md-6 mb-2">
              <label class="form-label">Trẻ em</label>
              <input v-model.number="form.child" @input="calculateTotal" type="number" min="0"
                     class="form-control input-effect" required />
            </div>
          </div>
          <div class="form-group mb-3">
            <label class="form-label">Ghi chú</label>
            <textarea v-model="form.note" class="form-control input-effect" rows="2"
              placeholder="Yêu cầu đặc biệt hoặc ghi chú cho tour..."></textarea>
          </div>
          <div class="form-group mb-4">
            <label class="form-label">Tổng giá (VND)</label>
            <div class="total-price-box">
              <span>{{ form.totalPrice.toLocaleString() }}</span>
            </div>
          </div>
          <button type="submit" class="book-btn w-100">
            <i class="bi bi-send-check"></i> Đặt tour ngay
          </button>
        </form>
      </div>
    </div>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  name: 'BookingFormView',
  data() {
    return {
      email: "",
      tours: [],
      hotels: [],
      tourHotels: [],
      form: {
        tourName: "",
        hotelName: "",
        customerID: "",
        adult: 1,
        child: 0,
        totalPrice: 0,
        note: ""
      },
    };
  },
  computed: {
    filteredHotels() {
      if (!this.form.tourName || this.tourHotels.length === 0) return [];
      const matched = this.tourHotels
        .filter(th => th.tourName === this.form.tourName)
        .map(th => th.hotelName);

      return this.hotels.filter(h => matched.includes(h.name));
    }
  },
  watch: {
    'form.tourName': 'calculateTotal',
    'form.adult': 'calculateTotal',
    'form.child': 'calculateTotal'
  },
  methods: {
    async fetchInitialData() {
      const tourRes = await axios.get("https://localhost:7046/api/tour");
      this.tours = tourRes.data;
      const hotelRes = await axios.get("https://localhost:7046/api/hotel");
      this.hotels = hotelRes.data;
      const tourHotelRes = await axios.get("https://localhost:7046/api/tourhotel");
      this.tourHotels = tourHotelRes.data;
    },
    filterHotelsByTour() {
      this.form.hotelName = ""; // reset khi đổi tour
    },
    async submitBooking() {
      let customerID = "";
      try {
        const res = await axios.get(
          `https://localhost:7046/api/customer/by-email/${encodeURIComponent(this.email.trim())}`
        );
        customerID = res.data.customerID || res.data.customerId;
      } catch {
        alert("❌ Email khách hàng không tồn tại hoặc sai.");
        return;
      }
      const selectedTour = this.tours.find(t => t.name === this.form.tourName);
      const selectedHotel = this.hotels.find(h => h.name === this.form.hotelName);

      if (!selectedTour || !selectedHotel) {
        alert("Chưa chọn đủ tour và khách sạn.");
        return;
      }
      this.calculateTotal();
      const payload = {
        bookingId: crypto.randomUUID(),
        tourID: selectedTour.id || selectedTour.tourID,
        hotelID: selectedHotel.hotelID || selectedHotel.id,
        customerID: customerID,
        adult: this.form.adult,
        child: this.form.child,
        totalPrice: this.form.totalPrice,
        note: this.form.note // GHI CHÚ!
      };

      try {
        await axios.post("https://localhost:7046/api/booking", payload);
        alert("✅ Đặt tour thành công!");
        this.resetForm();
      } catch (err) {
        alert("❌ Đặt tour thất bại. Vui lòng kiểm tra thông tin.");
        console.error(err);
      }
    },
    calculateTotal() {
      const selectedTour = this.tours.find(t => t.name === this.form.tourName);
      if (!selectedTour) {
        this.form.totalPrice = 0;
        return 0;
      }
      const priceAdult = +selectedTour.price || 0;
      const priceChild = Math.round(priceAdult * 0.6);
      this.form.totalPrice = (this.form.adult || 0) * priceAdult + (this.form.child || 0) * priceChild;
      return this.form.totalPrice;
    },
    resetForm() {
      this.email = "";
      this.form = {
        tourName: "",
        hotelName: "",
        customerID: "",
        adult: 1,
        child: 0,
        totalPrice: 0,
        note: ""
      };
    }
  },
  mounted() {
    this.fetchInitialData();
  }
};
</script>

<style scoped>
@import "https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css";
@import "https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.1/font/bootstrap-icons.css";

.booking-bg-sea {
  min-height: 100vh;
  background: radial-gradient(ellipse 120% 70% at 50% 30%, #c2f8ff 0%, #f8fcff 100%);
  position: relative;
  overflow-x: hidden;
  display: flex;
  align-items: center;
  justify-content: center;
}

.sea-waves {
  position: absolute;
  bottom: 0; left: 0; right: 0;
  width: 100vw;
  z-index: 1;
  pointer-events: none;
  opacity: 0.94;
}

.booking-form-wrapper {
  width: 100%;
  min-height: 95vh;
  display: flex;
  align-items: center;
  justify-content: center;
}

.glass-card {
  width: 430px;
  max-width: 97vw;
  background: rgba(255, 255, 255, 0.92);
  border-radius: 2rem;
  box-shadow: 0 16px 64px #38b6ff33, 0 2px 32px #e1faff99;
  padding: 2.6rem 2.2rem 2.2rem 2.2rem;
  margin-bottom: 32px;
  position: relative;
  z-index: 2;
  animation: card-appear 1.15s;
  backdrop-filter: blur(6px);
}

.gradient-text {
  font-size: 2.2rem;
  font-weight: 900;
  background: linear-gradient(90deg,#38b6ff,#f642e4 70%);
  -webkit-background-clip: text;
  -webkit-text-fill-color: transparent;
  text-shadow: 0 3px 24px #e4c1ffb3;
  margin-bottom: 2.3rem;
  letter-spacing: 1.5px;
  display: flex;
  justify-content: center;
  align-items: center;
  gap: .4rem;
}

.form-label {
  font-weight: 700;
  color: #4ac8ff;
  font-size: 1.09rem;
  letter-spacing: 0.4px;
}

.input-effect {
  border-radius: 1.7rem;
  border: 2px solid #a5f0ff;
  font-size: 1.09rem;
  background: #f6fafe;
  box-shadow: 0 2px 10px #e2f8ff15;
  padding: 0.92rem 1.18rem;
  transition: border 0.24s, box-shadow 0.22s, background 0.13s;
}
.input-effect:focus {
  border: 2px solid #7e4fff;
  background: #fff0fe;
  box-shadow: 0 0 0 0.17rem #f1d2f355;
  outline: none;
}

.total-price-box {
  border: 2px dashed #6ff6ff;
  background: #eafffb;
  border-radius: 1.3rem;
  color: #4f59f8;
  font-weight: bold;
  font-size: 1.27rem;
  padding: .9rem 1.2rem;
  letter-spacing: 1.05px;
  margin-top: 4px;
  text-align: right;
  box-shadow: 0 2px 10px #e9dbff46;
}

.book-btn {
  background: linear-gradient(90deg, #3eb9ff 10%, #f88bee 100%);
  color: #fff;
  font-weight: bold;
  font-size: 1.16rem;
  padding: 1.1rem 0;
  border-radius: 2.3rem;
  letter-spacing: 1.07px;
  box-shadow: 0 10px 28px #bffcff36, 0 2px 16px #efb7fa99;
  border: none;
  transition: background 0.27s, transform 0.17s, color 0.15s;
}
.book-btn:hover {
  background: linear-gradient(90deg, #f88bee 10%, #3eb9ff 100%);
  color: #5128aa;
  transform: scale(1.07) translateY(-2px);
  box-shadow: 0 14px 36px #befcffb8, 0 4px 22px #efb7fa54;
}

@media (max-width: 620px) {
  .glass-card {
    padding: 1.2rem .4rem;
    border-radius: 1.1rem;
  }
  .gradient-text {
    font-size: 1.33rem;
  }
  .total-price-box {
    font-size: 1.07rem;
    padding: .6rem .7rem;
  }
}
</style>
