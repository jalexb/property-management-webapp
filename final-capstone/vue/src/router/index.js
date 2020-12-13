import Vue from 'vue'
import Router from 'vue-router'
import Home from '../views/Home.vue'
import Login from '../views/Login.vue'
import Logout from '../views/Logout.vue'
import Register from '../views/Register.vue'
import store from '../store/index'
import PropertyDetails from '../components/PropertyDetails.vue'
import LeaseForm from '../views/LeaseForm.vue'
import MaintenanceIssue from '@/views/MaintenenceTicket'
import RenterTransactions from '../components/RenterTransactions.vue'
import PendingLeases from '../components/PendingLeases.vue'
import RenterSettings from '../views/RenterSettings.vue'
import CompletedApplications from '../views/CompletedApplications.vue'

Vue.use(Router)

/**
 * The Vue Router is used to "direct" the browser to render a specific view component
 * inside of App.vue depending on the URL.
 *
 * It also is used to detect whether or not a route requires the user to have first authenticated.
 * If the user has not yet authenticated (and needs to) they are redirected to /login
 * If they have (or don't need to) they're allowed to go about their way.
 */

const router = new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: "/login",
      name: "login",
      component: Login,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: "/logout",
      name: "logout",
      component: Logout,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: "/register",
      name: "register",
      component: Register,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: "/property/:id",
      name: "property-details",
      component: PropertyDetails,
      meta: {
        requiresAuth: false
      }
    },
    {
      path: "/lease-form/:id",
      name: "lease-form",
      component: LeaseForm,
      meta: {
        requiresAuth: true
      }
    },
    {
      path: "/maintenance/:userId",
      name: "maintenance-ticket",
      component: MaintenanceIssue,
    },
    {
      path: "/renter-settings",
      name: "renter-settings",
      component: RenterSettings,
      meta: {
        requiresAuth: true
      }
    },
    {
    path: "/renter/transactions/:userId",
    name: "renter-transactions",
    component: RenterTransactions
    },
    {
      path: "/landlord/pending-leases",
      name: "pending-leases",
      component: PendingLeases
    },
    {
      path: "/completed-applications",
      name: "completed-applications",
      component: CompletedApplications
    }
  ]
})

router.beforeEach((to, from, next) => {
  // Determine if the route requires Authentication
  const requiresAuth = to.matched.some(x => x.meta.requiresAuth);

  // If it does and they are not logged in, send the user to "/login"
  if (requiresAuth && store.state.token === '') {
    next("/login");
  } else {
    // Else let them go to their next destination
    next();
  }
});

export default router;
