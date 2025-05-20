<template>
  <div>
    <MainNavbar />
    <router-view />
    <footer class="bg-dark text-white text-center py-3">
      © 2025 Booking Tour. All rights reserved.
    </footer>
  </div>
</template>

<script>
import MainNavbar from './Navbar.vue'

export default {
  components: { MainNavbar }
}
</script>
