import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import TourDetailView from '../views/TourDetailView.vue'
import BookingView from '../views/BookingView.vue'
import BookingForm from '../views/BookingForm.vue'
import LoginView from '../views/LoginView.vue'
import RegisterView from '../views/RegisterView.vue'
//import AccountManagementView from '../views/AccountManagementView.vue'
import TourManagementView from '../views/TourManagementView.vue'
import AccountManagementView from '../views/admin/AccountManagementView.vue'
import BookingManagementView from '../views/admin/BookingManagementView.vue'
import TourStatusView from '../views/TourStatusView.vue'



const routes = [
    { path: '/', name: 'Home', component: HomeView },
    { path: '/tours/:id', name: 'TourDetail', component: TourDetailView },
    { path: '/booking', name: 'Booking', component: BookingView },
    { path: '/bookingform', name: 'BookingForm', component: BookingForm },
    { path: '/login', name: 'Login', component: LoginView },
    { path: '/register', name: 'Register', component: RegisterView },
    {
        path: '/admin/accounts',
        name: 'AccountManagement',
        component: AccountManagementView,
        meta: { requiresAdmin: true }
    },
    {
        path: '/admin/tours',
        name: 'TourManagement',
        component: TourManagementView,
        meta: { requiresAdmin: true }
    },
    {
        path: '/admin/accounts',
        name: 'AccountManagement',
        component: AccountManagementView
    },
    {
        path: '/admin/bookings',
        name: 'AdminBookings',
        component: BookingManagementView
    },
    {
        path: '/tourstatus',
        name: 'TourStatus',
        component: TourStatusView
    },



]

const router = createRouter({
    history: createWebHistory(),
    routes
})

export default router
