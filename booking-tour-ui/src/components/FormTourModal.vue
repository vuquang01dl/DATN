<template>
  <div v-if="visible" class="modal-backdrop">
    <div class="modal-dialog">
      <div class="modal-content p-4">
        <h5 class="modal-title mb-3">{{ form.id ? 'Sửa' : 'Thêm' }} Tour</h5>

        <form @submit.prevent="submitForm">
          <input v-model="form.name" placeholder="Tên tour" class="form-control mb-2" />
          <input v-model="form.price" placeholder="Giá" type="number" class="form-control mb-2" />
          <textarea v-model="form.description" placeholder="Mô tả" class="form-control mb-2" />

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
export default {
  name: 'FormTourModal',
  props: {
    visible: Boolean,
    tour: Object
  },
  data() {
    return {
      form: {
        id: '',
        name: '',
        price: '',
        description: ''
      }
    }
  },
  watch: {
    tour: {
      immediate: true,
      handler(newVal) {
        this.form = newVal
          ? { ...newVal }
          : { id: '', name: '', price: '', description: '' }
      }
    }
  },
  methods: {
    submitForm() {
      if (!this.form.name || !this.form.price || !this.form.description) {
        alert("Vui lòng điền đầy đủ thông tin.");
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
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: rgba(0,0,0,0.4);
  display: flex;
  align-items: center;
  justify-content: center;
}
</style>
