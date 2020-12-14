<template>
  <div class="settings-page">
    <v-form>
      <h1>User Settings</h1>
      <v-text-field
        v-model="renterInfo.email"
        label="Email"
        required
        prepend-icon="mdi-email"
      >
      </v-text-field>

      <v-text-field
        v-model="renterInfo.phoneNumber"
        label="Phone Number"
        required
        prepend-icon="mdi-phone-classic"
      >
      </v-text-field>

      <v-btn type="submit" outlined rounded color="primary"
        >Update Settings</v-btn
      >
    </v-form>
  </div>
</template>

<script>
import RenterService from "../services/RenterService.js";

export default {
  data() {
    return {
      renterInfo: {
        renterId: 0,
        userId: 0,
        firstName: null,
        lastName: null,
        phoneNumber: null,
        email: null,
        leaseType: null,
        salary: 0.0,
      },
    };
  },
  name: "renter-settings",
  created() {
    RenterService.getRenterInfo(this.$store.state.user.userId).then(
      (response) => {
        if (response.status === 200) {
          this.renterInfo = response.data;
        }
      }
    );
  },
  methods: {
    updateRenterInfo() {
      RenterService.updateRenterInfo(this.renterInfo)
        .then((response) => {
          if (response.status === 200) {
            alert("Updated Renter infomation successfully.");
            this.$router.push("/");
          }
        })
        .catch((error) => {
          alert("An error occurred on updating the renter information.", error);
        });
    },
  },
};
</script>

<style scoped>
.settings-page {
  width: 80%;
  height: 100%;
  margin-left: 10%;
  background-color: white;
  padding: 10px;
}
h1 {
  text-align: center;
}

.input {
  margin-bottom: 10px;
}
</style>
