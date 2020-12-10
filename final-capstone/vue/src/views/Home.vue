<template>
  <div class="home">
    <div v-if="userRole == 'user' || (userRole != 'user' & userRole != 'landlord' & userRole != 'maintenance' & userRole != 'renter')">
    <h1>Property Listings</h1>
    <property-list />
    </div>
    <div v-if="userRole == 'landlord'">
      <h1>Landlord Services</h1>
      <landlord-home />
    </div>
  </div>
</template>

<script>
import LandlordHome from '../components/LandlordHome.vue';
import PropertyList from "../components/PropertyList.vue";

export default {
  data() {
    return {
      userRole: this.$store.state.user.role
    }
  },
  name: "home",
  components: {
    PropertyList,
    LandlordHome
  },
};
</script>

<style scoped>
h1 {
  text-align: center;
}
</style>
