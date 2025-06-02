<template>
  <div class="hotel-form">
    <h2>{{ isEdit ? "Cập nhật khách sạn" : "Thêm khách sạn mới" }}</h2>
    <form @submit.prevent="handleSubmit">
      <input v-model="hotel.name" placeholder="Tên khách sạn" required />
      <input v-model.number="hotel.starRating" type="number" min="1" max="5" placeholder="Số sao" required />
      <textarea v-model="hotel.description" placeholder="Mô tả" required></textarea>
      <input v-model="hotel.image" placeholder="Đường dẫn ảnh" />
      <input v-model="hotel.address" placeholder="Địa chỉ" required />
      <input v-model="hotel.city" placeholder="Thành phố" required />
      <input v-model="hotel.district" placeholder="Quận/Huyện" required />
      <input v-model="hotel.ward" placeholder="Phường/Xã" required />
      <input v-model="hotel.phone" placeholder="Số điện thoại" required />
      <button type="submit">{{ isEdit ? "Cập nhật" : "Tạo mới" }}</button>
    </form>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  props: {
    existingHotel: {
      type: Object,
      default: null,
    },
  },
  data() {
    return {
      hotel: {
        hotelID: null,
        name: '',
        starRating: 1,
        description: '',
        image: '',
        address: '',
        city: '',
        district: '',
        ward: '',
        phone: '',
      },
    };
  },
  computed: {
    isEdit() {
      return this.existingHotel !== null;
    },
  },
  created() {
    if (this.isEdit) {
      this.hotel = { ...this.existingHotel };
    }
  },
  methods: {
    async handleSubmit() {
      try {
        if (this.isEdit) {
          await axios.put(`https://localhost:7046/api/Hotel/${this.hotel.hotelID}`, this.hotel);
          alert('Cập nhật thành công!');
        } else {
          await axios.post('https://localhost:7046/api/Hotel', this.hotel);
          alert('Tạo mới thành công!');
        }
        this.$emit('refresh');
      } catch (error) {
        console.error(error);
        alert('Lỗi khi gửi dữ liệu');
      }
    },
  },
};
</script>
