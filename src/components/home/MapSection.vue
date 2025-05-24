<template>
  <div class="container my-5">
    <h4 class="text-center mb-4">Địa chỉ: Đại học Bách Khoa Hà Nội</h4>
    <div class="ratio ratio-16x9">
      <iframe
        src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3724.6248693038245!2d105.84117111533061!3d21.005176493894226!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x3135ab3ec6f9bfb3%3A0x9e4c3139330e7d8f!2zSOG6o2kgdGjhuqV0IELDoWNoIEtob2EgLSBIQU5V!5e0!3m2!1svi!2s!4v1620812239183!5m2!1svi!2s"
        width="600"
        height="450"
        style="border:0;"
        allowfullscreen=""
        loading="lazy"
        referrerpolicy="no-referrer-when-downgrade"
      ></iframe>
    </div>
  </div>
</template>

<script>
export default {
  name: 'MapSection'
}
</script>

<style scoped>
.ratio {
  border-radius: 10px;
  overflow: hidden;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
}
</style>
