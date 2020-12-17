<template>
  <div class="home">
    <div v-if="userRole == 'user' || (userRole != 'user' & userRole != 'landlord' & userRole != 'maintenance' & userRole != 'renter')">
      <h1>Property Listings</h1>
      <property-list />
    </div>
    <div v-if="userRole == 'renter'">
      <h1>Renter Services</h1>
      <renter-home />
    </div>
    <div v-if="userRole == 'landlord'">
      <h1>Landlord Services</h1>
      <landlord-home />
    </div>
    <div v-if="userRole == 'maintenance'">
      <h1>Assigned Tickets</h1>
      <assigned-tickets />
    </div>
  </div>
</template>

<script>
import LandlordHome from '../components/LandlordHome.vue';
import RenterHome from "../components/RenterHome.vue";
import PropertyList from "../components/PropertyList.vue";
import AssignedTickets from '../components/AssignedTickets.vue';

export default {
  data() {
    return {
      userRole: this.$store.state.user.role
    }
  },
  name: "home",
  components: {
    PropertyList,
    RenterHome,
    LandlordHome,
    AssignedTickets
  },
};
</script>

<style scoped>
h1 {
  text-align: center;
}

a {
  text-decoration: none;
}
</style>
