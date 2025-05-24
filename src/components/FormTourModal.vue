<template>
  <div v-if="visible" class="modal-backdrop">
    <div class="modal-dialog">
      <div class="modal-content p-4">
        <h5 class="modal-title mb-3">{{ form.id ? 'Sửa' : 'Thêm' }} Tour</h5>

        <form @submit.prevent="submitForm">
          <input v-model="form.name" placeholder="Tên tour" class="form-control mb-2" />
          <input v-model.number="form.price" placeholder="Giá (VND)" type="number" class="form-control mb-2" />
          <textarea v-model="form.description" placeholder="Mô tả" class="form-control mb-2" />

          <input v-model="form.startDate" type="date" class="form-control mb-2" />
          <input v-model="form.endDate" type="date" class="form-control mb-2" />

          <div class="mb-2">
            <label>Địa điểm</label>
            <select v-model="form.destinationId" class="form-select">
              <option disabled value="">-- Chọn địa điểm --</option>
              <option v-for="d in destinations" :key="d.id" :value="d.id">
                {{ d.name }}
              </option>
            </select>
          </div>

          <div class="mb-3">
            <label>Khách sạn</label>
            <select v-model="form.hotelIds" class="form-select" multiple>
              <option v-for="hotel in hotels" :key="hotel.id" :value="hotel.id">
                {{ hotel.name }} - {{ hotel.location }}
              </option>
            </select>
          </div>

          <div class="text-end">
            <button type="button" class="btn btn-secondary me-2" @click="$emit('close')">Hủy</button>
            <button type="submit" class="btn btn-primary">Lưu</button>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<script>
// import axios from 'axios'

export default {
  name: 'FormTourModal',
  props: {
    visible: Boolean,
    tour: Object
  },
  data() {
    return {
      hotels: [],
      destinations: [],
      form: {
        id: '',
        name: '',
        price: '',
        description: '',
        startDate: '',
        endDate: '',
        destinationId: '',
        hotelIds: []
      }
    }
  },
  watch: {
    tour: {
      immediate: true,
      handler(newVal) {
        this.form = newVal
          ? {
              ...newVal,
              hotelIds: newVal.hotelIds || [],
              destinationId: newVal.destinationId || ''
            }
          : {
              id: '',
              name: '',
              price: '',
              description: '',
              startDate: '',
              endDate: '',
              destinationId: '',
              hotelIds: []
            };
      }
    }
  },
  mounted() {
    this.loadHotels();
    this.loadDestinations();
  },
  methods: {
    loadHotels() {
      this.hotels = [
        { id: 'h1', name: 'Paradise', location: 'Đà Nẵng' },
        { id: 'h2', name: 'Sunrise', location: 'Phú Quốc' }
      ];
      // axios.get('http://localhost:5017/api/hotel').then(res => this.hotels = res.data)
    },
    loadDestinations() {
      this.destinations = [
        { id: 'd1', name: 'Đà Nẵng' },
        { id: 'd2', name: 'Hà Nội' }
      ];
      // axios.get('http://localhost:5017/api/destination').then(res => this.destinations = res.data)
    },
    submitForm() {
      const { name, price, description, startDate, endDate, destinationId } = this.form;
      if (!name || !price || !description || !startDate || !endDate || !destinationId) {
        alert("Vui lòng nhập đầy đủ thông tin.");
        return;
      }

      this.$emit('save', this.form);
    }
  }
}
</script>

<style scoped>
.modal-backdrop {
  position: fixed;
  inset: 0;
  background: rgba(0, 0, 0, 0.4);
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 1050;
}
</style>
