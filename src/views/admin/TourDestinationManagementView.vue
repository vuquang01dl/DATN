<template>
  <div class="container py-5">
    <h2 class="text-center mb-4">Quản lý Tour - Điểm đến</h2>

    <form @submit.prevent="addTourDestination" class="row g-2 mb-4">
      <div class="col-md-5">
        <input v-model="form.tourId" class="form-control" placeholder="ID Tour (GUID)" required />
      </div>
      <div class="col-md-5">
        <input v-model="form.destinationId" class="form-control" placeholder="ID Điểm đến (GUID)" required />
      </div>
      <div class="col-md-2">
        <button class="btn btn-primary w-100">Thêm</button>
      </div>
    </form>

    <table class="table table-bordered">
      <thead>
        <tr>
          <th>ID Tour</th>
          <th>ID Điểm đến</th>
          <th>Hành động</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="item in tourDestinations" :key="item.tourId + '-' + item.destinationId">
          <td>{{ item.tourId }}</td>
          <td>{{ item.destinationId }}</td>
          <td>
            <button class="btn btn-sm btn-outline-danger" @click="deleteTourDestination(item.tourId, item.destinationId)">Xoá</button>
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
      tourDestinations: [],
      form: {
        tourId: '',
        destinationId: ''
      }
    }
  },
  methods: {
    async fetchTourDestinations() {
      // const res = await axios.get('http://localhost:5017/api/tourdestination');
      // this.tourDestinations = res.data;
      this.tourDestinations = [
        { tourId: '11111111-1111-1111-1111-111111111111', destinationId: 'aaaaaaa1-aaaa-aaaa-aaaa-aaaaaaaaaaaa' },
        { tourId: '22222222-2222-2222-2222-222222222222', destinationId: 'bbbbbbb2-bbbb-bbbb-bbbb-bbbbbbbbbbbb' }
      ];
    },
    async addTourDestination() {
      const dto = { ...this.form };
      console.log('🆕 Tạo Tour-Destination:', dto);
      // await axios.post('http://localhost:5017/api/tourdestination', dto);
      this.tourDestinations.push({ ...dto });
      this.form = { tourId: '', destinationId: '' };
    },
    async deleteTourDestination(tourId, destinationId) {
      console.log('🗑 Xoá Tour-Destination:', tourId, destinationId);
      // await axios.delete(`http://localhost:5017/api/tourdestination/${tourId}/${destinationId}`);
      this.tourDestinations = this.tourDestinations.filter(item =>
        !(item.tourId === tourId && item.destinationId === destinationId)
      );
    }
  },
  mounted() {
    this.fetchTourDestinations();
  }
}
</script>

<style scoped>
.table td, .table th {
  vertical-align: middle;
}
</style>
