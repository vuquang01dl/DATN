<template>
  <div class="container py-5">
    <h2 class="text-center mb-4">Quản lý Tour - Khách sạn</h2>

    <form @submit.prevent="addTourHotel" class="row g-2 mb-4">
      <div class="col-md-5">
        <input v-model="form.tourId" class="form-control" placeholder="ID Tour (GUID)" required />
      </div>
      <div class="col-md-5">
        <input v-model="form.hotelId" class="form-control" placeholder="ID Khách sạn (GUID)" required />
      </div>
      <div class="col-md-2">
        <button class="btn btn-primary w-100">Thêm</button>
      </div>
    </form>

    <table class="table table-bordered">
      <thead>
        <tr>
          <th>ID Tour</th>
          <th>ID Khách sạn</th>
          <th>Hành động</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="item in tourHotels" :key="item.tourId + '-' + item.hotelId">
          <td>{{ item.tourId }}</td>
          <td>{{ item.hotelId }}</td>
          <td>
            <button class="btn btn-sm btn-outline-danger" @click="deleteTourHotel(item.tourId, item.hotelId)">Xoá</button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
//import axios from 'axios';

export default {
  data() {
    return {
      tourHotels: [],
      form: {
        tourId: '',
        hotelId: ''
      }
    };
  },
  methods: {
    async fetchTourHotels() {
      // const res = await axios.get('http://localhost:5017/api/tourhotel');
      // this.tourHotels = res.data;
      this.tourHotels = [
        { tourId: '11111111-1111-1111-1111-111111111111', hotelId: 'aaaaaaa1-aaaa-aaaa-aaaa-aaaaaaaaaaaa' },
        { tourId: '22222222-2222-2222-2222-222222222222', hotelId: 'bbbbbbb2-bbbb-bbbb-bbbb-bbbbbbbbbbbb' }
      ];
    },
    async addTourHotel() {
      const dto = { ...this.form };
      console.log('🆕 Tạo Tour-Hotel:', dto);
      // await axios.post('http://localhost:5017/api/tourhotel', dto);
      this.tourHotels.push({ ...dto });
      this.form = { tourId: '', hotelId: '' };
    },
    async deleteTourHotel(tourId, hotelId) {
      console.log('🗑 Xoá Tour-Hotel:', tourId, hotelId);
      // await axios.delete(`http://localhost:5017/api/tourhotel/${tourId}/${hotelId}`);
      this.tourHotels = this.tourHotels.filter(item =>
        !(item.tourId === tourId && item.hotelId === hotelId)
      );
    }
  },
  mounted() {
    this.fetchTourHotels();
  }
};
</script>

<style scoped>
.table td, .table th {
  vertical-align: middle;
}
</style>
