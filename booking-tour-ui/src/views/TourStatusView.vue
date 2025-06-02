<template>
  <div class="shopee-bg min-vh-100 py-5">
    <div class="container">
      <h2 class="shopee-title mb-5">🚩 Theo dõi lịch trình tour</h2>
      <div class="row justify-content-center mb-4">
        <div class="col-md-7">
          <div class="input-group mb-2">
            <input
              v-model="tourName"
              class="form-control shopee-input"
              placeholder="Nhập tên tour cần tra cứu..."
              @keyup.enter="fetchTourStatuses"
            />
            <button class="btn shopee-btn" @click="fetchTourStatuses">
              Tra cứu
            </button>
          </div>
        </div>
      </div>

      <!-- Shopee Tab Navigation Style -->
      <div class="row justify-content-center mb-4">
        <div class="col-md-10 d-flex justify-content-between shopee-tab-nav">
          <div
            class="shopee-tab"
            :class="{active: activeTab===0}"
            @click="activeTab=0"
          >
            <div class="shopee-tab-icon bg-primary-subtle">
              <i class="bi bi-calendar-check"></i>
            </div>
            <div class="shopee-tab-label">Trước khi diễn ra</div>
          </div>
          <div
            class="shopee-tab"
            :class="{active: activeTab===1}"
            @click="activeTab=1"
          >
            <div class="shopee-tab-icon bg-warning-subtle">
              <i class="bi bi-geo-alt"></i>
            </div>
            <div class="shopee-tab-label">Lịch trình & trạng thái</div>
          </div>
          <div
            class="shopee-tab"
            :class="{active: activeTab===2}"
            @click="activeTab=2"
          >
            <div class="shopee-tab-icon bg-success-subtle">
              <i class="bi bi-emoji-smile"></i>
            </div>
            <div class="shopee-tab-label">Sau khi kết thúc</div>
          </div>
        </div>
      </div>

      <div class="row justify-content-center">
        <div class="col-md-10">
          <!-- TAB 0: Trước khi diễn ra -->
          <transition name="fade">
            <div v-if="activeTab === 0" class="tab-content-area">
              <h4 class="mb-3">🌅 Chuẩn bị trước khi đi tour</h4>
              <ul class="list-group mb-3 shadow-sm">
                <li class="list-group-item"><b>Họp mặt đoàn:</b> 07:00 sáng ngày khởi hành</li>
                <li class="list-group-item"><b>Nhận bộ quà tặng du lịch</b> (áo, nón, nước...)</li>
                <li class="list-group-item"><b>Kiểm tra giấy tờ tùy thân, vé tour</b></li>
                <li class="list-group-item"><b>Kiểm tra hành lý, sức khỏe trước chuyến đi</b></li>
              </ul>
              <div class="alert alert-info">
                Hãy chuẩn bị đầy đủ giấy tờ và đồ dùng cá nhân trước chuyến đi!
              </div>
            </div>
          </transition>

          <!-- TAB 1: Lịch trình & trạng thái -->
          <transition name="fade">
            <div v-if="activeTab === 1" class="tab-content-area">
              <h4 class="mb-3">🛣️ Lịch trình và trạng thái tour</h4>
              <!-- Nút cập nhật và form -->
              <div class="d-flex justify-content-end mb-3">
                <button class="btn btn-outline-warning fw-bold"
                        @click="showUpdateForm = !showUpdateForm">
                  <i class="bi bi-plus-circle"></i> Cập nhật trạng thái mới
                </button>
              </div>
              <transition name="fade">
                <form v-if="showUpdateForm"
                      class="bg-light rounded-4 p-4 mb-4 shadow-sm"
                      @submit.prevent="submitStatusUpdate">
                  <div class="row g-2">
                    <div class="col-md-4">
                      <label class="form-label fw-semibold">Trạng thái</label>
                      <input v-model="form.status" class="form-control" required placeholder="Nhập trạng thái mới..." />
                    </div>
                    <div class="col-md-4">
                      <label class="form-label fw-semibold">Ghi chú</label>
                      <input v-model="form.note" class="form-control" placeholder="Thêm ghi chú (nếu có)" />
                    </div>
                    <div class="col-md-4">
                      <label class="form-label fw-semibold">Mã nhân viên</label>
                      <input v-model="form.employeeId" class="form-control" placeholder="Nhập mã nhân viên" />
                    </div>
                  </div>
                  <div class="mt-3 d-flex justify-content-end gap-2">
                    <button type="submit" class="btn btn-success px-4">Lưu</button>
                    <button type="button" class="btn btn-outline-secondary px-4" @click="showUpdateForm = false">Đóng</button>
                  </div>
                </form>
              </transition>

              <div v-if="tourStatuses.length">
                <ol class="timeline">
                  <li v-for="status in tourStatuses" :key="status.id">
                    <span class="timeline-dot"></span>
                    <span class="timeline-content">
                      <b>{{ status.status }}</b>
                      <br>
                      <small class="text-muted">{{ new Date(status.time).toLocaleString() }}</small>
                      <div v-if="status.note" class="small text-secondary mt-1">
                        {{ status.note }}
                      </div>
                    </span>
                  </li>
                </ol>
                <div class="alert alert-success mt-4 text-center" v-if="getCurrentStatus()">
                  Trạng thái hiện tại: <b>{{ getCurrentStatus().status }}</b>
                </div>
              </div>
              <div v-else-if="loaded" class="alert alert-warning mt-4 text-center">
                Không tìm thấy lịch trình trạng thái cho tour này!
              </div>
              <div v-else class="alert alert-info mt-4 text-center">
                Vui lòng nhập tên tour và tra cứu trạng thái!
              </div>
            </div>
          </transition>

          <!-- TAB 2: Sau khi kết thúc -->
          <transition name="fade">
            <div v-if="activeTab === 2" class="tab-content-area">
              <h4 class="mb-3">🎉 Tổng kết & Đánh giá sau tour</h4>
              <div class="alert alert-success mb-3 shadow-sm">
                <b>Cảm ơn bạn đã tham gia tour cùng chúng tôi!</b><br>
                Chúc bạn có nhiều trải nghiệm tuyệt vời.
              </div>
              <div class="mb-3">
                <b>Gửi đánh giá của bạn:</b>
                <textarea class="form-control mt-2 mb-2" rows="3" placeholder="Cảm nhận về tour..."></textarea>
                <button class="btn btn-success rounded-pill px-4">Gửi đánh giá</button>
              </div>
              <div class="alert alert-secondary mt-4">
                <b>Những đánh giá nổi bật:</b>
                <ul class="mb-0">
                  <li>⭐⭐⭐⭐⭐ "Dịch vụ rất tốt, hướng dẫn viên thân thiện!"</li>
                  <li>⭐⭐⭐⭐ "Lịch trình hợp lý, đồ ăn ngon!"</li>
                </ul>
              </div>
            </div>
          </transition>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import axios from 'axios'

export default {
  name: 'TourShopeeStatusView',
  data() {
    return {
      tourName: '',
      tourStatuses: [],
      loaded: false,
      activeTab: 1,  // Tab 1 (lịch trình) là mặc định
      showUpdateForm: false,
      form: {
        status: '',
        note: '',
        employeeId: ''
      }
    }
  },
  methods: {
    async fetchTourStatuses() {
      if (!this.tourName) {
        alert('Vui lòng nhập TÊN TOUR!');
        return;
      }
      try {
        const res = await axios.get(
          `https://localhost:7046/api/tourstatuslog/by-name/${encodeURIComponent(this.tourName)}`
        );
        this.tourStatuses = res.data;
        this.loaded = true;
        this.activeTab = 1;
      } catch (err) {
        alert('Không lấy được dữ liệu từ server!');
        this.tourStatuses = [];
        this.loaded = true;
      }
    },
    getCurrentStatus() {
      return this.tourStatuses.length
        ? this.tourStatuses.slice().sort((a, b) => new Date(b.time) - new Date(a.time))[0]
        : null
    },
    async submitStatusUpdate() {
      if (!this.tourName) {
        alert("Chưa nhập tên tour!");
        return;
      }
      if (!this.form.status) {
        alert("Nhập trạng thái mới!");
        return;
      }
      // Lấy tourId từ lịch trình đang hiển thị
      let tourId = this.tourStatuses[0]?.tourId;
      if (!tourId) {
        alert("Không xác định được TourId để cập nhật!");
        return;
      }
      try {
        await axios.post('https://localhost:7046/api/tourstatuslog', {
          tourId: tourId,
          status: this.form.status,
          note: this.form.note,
          employeeId: this.form.employeeId,
          time: new Date().toISOString()
        });
        this.showUpdateForm = false;
        this.form.status = '';
        this.form.note = '';
        this.form.employeeId = '';
        await this.fetchTourStatuses();
      } catch (err) {
        alert("Có lỗi khi cập nhật trạng thái! " + (err.response?.data?.title || ''));
      }
    }
  }
}
</script>

<style scoped>
.shopee-bg {
  background: linear-gradient(120deg, #ffe2ce 0%, #fff4e3 100%);
  min-height: 100vh;
}
.shopee-title {
  font-size: 2.3rem;
  font-weight: bold;
  color: #ee4d2d;
  text-align: center;
  letter-spacing: 1px;
  margin-bottom: 2rem;
  text-shadow: 0 2px 8px #ffede3;
}
.shopee-input {
  border-radius: 2rem 0 0 2rem !important;
  border: 2px solid #ee4d2d !important;
  height: 2.7rem;
  font-size: 1.1rem;
  padding: 0.5rem 1rem;
}
.shopee-btn {
  border-radius: 0 2rem 2rem 0 !important;
  background-color: #ee4d2d !important;
  color: #fff !important;
  font-weight: 600;
  border: none;
  padding: 0 2rem;
  font-size: 1.1rem;
  transition: background 0.2s;
}
.shopee-btn:hover {
  background: #ff7337 !important;
  color: #fff;
}
.shopee-tab-nav { gap: 16px; }
.shopee-tab {
  flex: 1 1 0;
  text-align: center;
  cursor: pointer;
  user-select: none;
  transition: transform 0.18s;
  position: relative;
}
.shopee-tab .shopee-tab-icon {
  width: 60px;
  height: 60px;
  border-radius: 50%;
  font-size: 2rem;
  line-height: 60px;
  margin: 0 auto 0.6rem;
  display: flex;
  align-items: center;
  justify-content: center;
  background: #fff7f3;
  border: 2.5px solid #ffd3bc;
  box-shadow: 0 2px 8px rgba(238, 77, 45, 0.12);
  transition: box-shadow 0.18s, border 0.18s;
}
.shopee-tab.active .shopee-tab-icon {
  background: #ee4d2d;
  color: #fff;
  border: 2.5px solid #ee4d2d;
  box-shadow: 0 6px 20px rgba(238, 77, 45, 0.22);
  transform: scale(1.06);
}
.shopee-tab-label {
  font-size: 1.08rem;
  color: #ee4d2d;
  font-weight: 600;
  margin-bottom: 0;
}
.shopee-tab.active .shopee-tab-label {
  color: #ee4d2d;
  text-shadow: 0 1px 3px #ffd3bc;
}
.shopee-tab:hover { transform: translateY(-3px) scale(1.03);}
.tab-content-area {
  min-height: 250px;
  background: #fff;
  border-radius: 1.2rem;
  box-shadow: 0 2px 18px rgba(238, 77, 45, 0.08);
  padding: 2rem 1.5rem;
  margin-bottom: 2rem;
}
.timeline {
  list-style: none;
  padding-left: 0;
  position: relative;
  margin-left: 12px;
}
.timeline::before {
  content: '';
  position: absolute;
  left: 13px;
  top: 8px;
  height: calc(100% - 16px);
  width: 3px;
  background: #ffd3bc;
  border-radius: 2px;
}
.timeline li {
  position: relative;
  margin-bottom: 36px;
  min-height: 46px;
}
.timeline-dot {
  position: absolute;
  left: -1px;
  top: 0;
  width: 28px;
  height: 28px;
  background: #fff7f3;
  border: 3px solid #ee4d2d;
  border-radius: 50%;
  z-index: 1;
  box-shadow: 0 2px 8px #ffd3bc50;
}
.timeline-content {
  margin-left: 42px;
  font-size: 1.07rem;
}
.fade-enter-active, .fade-leave-active {
  transition: all 0.4s cubic-bezier(.87,.3,.18,1.1);
}
.fade-enter, .fade-leave-to {
  opacity: 0;
  transform: translateY(15px) scale(.96);
}
</style>

<!--
Yêu cầu: Cài Bootstrap-icons nếu chưa có: 
npm i bootstrap-icons
hoặc chèn vào public/index.html:
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.1/font/bootstrap-icons.css">
-->
