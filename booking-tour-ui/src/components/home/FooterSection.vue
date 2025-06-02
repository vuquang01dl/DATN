<template>
  <footer class="main-footer mt-auto py-4">
    <div class="container">
      <div class="row gy-3 align-items-center justify-content-between">
        <!-- Logo & Info -->
        <div class="col-lg-4 col-md-6 text-lg-start text-center mb-2 mb-lg-0">
          <div class="footer-brand fw-bold mb-2">BookingTour</div>
          <div>
            <i class="bi bi-geo-alt-fill me-1"></i>123 Trần Phú, Đà Nẵng<br />
            <i class="bi bi-telephone me-1"></i>0123 456 789<br />
            <i class="bi bi-envelope me-1"></i>support@bookingtour.vn<br />
            <i class="bi bi-clock me-1"></i>8h - 17h (Thứ 2 - Thứ 7)
          </div>
        </div>

        <!-- Quick links -->
        <div class="col-lg-4 col-md-6 text-center mb-2 mb-lg-0">
          <div class="footer-title fw-semibold mb-2">Liên kết nhanh</div>
          <ul class="footer-links list-unstyled d-flex flex-wrap justify-content-center gap-3 mb-0">
            <li><router-link to="/" class="footer-link">Trang chủ</router-link></li>
            <li><router-link to="/tours" class="footer-link">Tour</router-link></li>
            <li><router-link to="/hotels" class="footer-link">Khách sạn</router-link></li>
            <li><router-link to="/blogs" class="footer-link">Tin tức</router-link></li>
            <li><router-link to="/contact" class="footer-link">Liên hệ</router-link></li>
          </ul>
        </div>

        <!-- Social -->
        <div class="col-lg-4 text-lg-end text-center">
          <div class="footer-title fw-semibold mb-2">Kết nối với chúng tôi</div>
          <div class="footer-social d-inline-flex gap-3">
            <a href="https://facebook.com" target="_blank" rel="noopener" aria-label="Facebook" class="footer-social-link">
              <i class="bi bi-facebook"></i>
            </a>
            <a href="https://instagram.com" target="_blank" rel="noopener" aria-label="Instagram" class="footer-social-link">
              <i class="bi bi-instagram"></i>
            </a>
            <a href="https://twitter.com" target="_blank" rel="noopener" aria-label="Twitter" class="footer-social-link">
              <i class="bi bi-twitter"></i>
            </a>
            <a href="https://youtube.com" target="_blank" rel="noopener" aria-label="YouTube" class="footer-social-link">
              <i class="bi bi-youtube"></i>
            </a>
            <a href="https://tiktok.com" target="_blank" rel="noopener" aria-label="TikTok" class="footer-social-link">
              <i class="bi bi-tiktok"></i>
            </a>
          </div>
        </div>
      </div>

      <hr class="footer-hr mt-4 mb-2" />
      <div class="text-center small text-muted opacity-75">
        &copy; 2025 BookingTour. All rights reserved.
      </div>
    </div>
  </footer>
</template>

<style scoped>
.main-footer {
  background: linear-gradient(90deg, #f7fbff 0%, #e0f7fa 100%);
  color: #344055;
  font-size: 15px;
  border-top: 2px solid #e0e0e0;
  box-shadow: 0 2px 24px #00bcd420;
}
.footer-brand {
  font-size: 1.5rem;
  letter-spacing: 1.2px;
  color: #007991;
  text-shadow: 0 2px 8px #7de2fc28;
}
.footer-title {
  color: #1976d2;
  font-size: 1.08rem;
  letter-spacing: 0.5px;
}
.footer-links {
  padding-left: 0;
  margin-bottom: 0;
}
.footer-link {
  color: #1183c6;
  text-decoration: none;
  font-weight: 500;
  transition: color 0.2s, text-shadow 0.2s;
}
.footer-link:hover {
  color: #fbc02d;
  text-shadow: 0 2px 12px #ffe08266;
}
.footer-social-link {
  color: #00bcd4;
  font-size: 1.45rem;
  transition: color 0.18s, transform 0.18s;
  background: #fff;
  border-radius: 50%;
  padding: 7px;
  box-shadow: 0 2px 8px #b2f7ef1a;
  display: inline-block;
}
.footer-social-link:hover {
  color: #f44336;
  background: #fffbe7;
  transform: translateY(-4px) scale(1.13) rotate(-6deg);
}
.footer-hr {
  border: none;
  border-top: 1.5px solid #a9b9c3;
  opacity: 0.35;
}
@media (max-width: 992px) {
  .main-footer .row > div {
    text-align: center !important;
  }
  .footer-links {
    flex-direction: column;
    gap: 0.5rem !important;
  }
}
</style>
