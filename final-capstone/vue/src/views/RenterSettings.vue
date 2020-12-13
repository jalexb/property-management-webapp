<template>
  <div class="renter-settings">
    <div class="form-group">
    <label>email</label>
    <input type="text" id="email" placeholder="email" v-model="renterInfo.email" />
    </div>
    <div class="form-group">
      <label>Phone Number</label>
    <input type="text" id="phoneNumber" placeholder="phone Number" v-model="renterInfo.phoneNumber" />
    </div>
    <br />
    <button type="submit" v-on:click="updateRenterInfo()" >Update Renter Info</button>
  </div>
</template>

<script>
import RenterService from "../services/RenterService.js"


export default {
  data() {
    return {
      renterInfo: {
      "renterId": 0,
      "userId": 0,
      "firstName": null,
      "lastName": null,
      "phoneNumber": null,
      "email": null,
      "leaseType": null,
      "salary": 0.0
      }
    }
  },
  name: "renter-settings",
  created(){
    RenterService.getRenterInfo(this.$store.state.user.userId).then(response=>{
      if(response.status === 200){
        this.renterInfo = response.data;
      }
    })
  },
  methods: {
    updateRenterInfo(){
      RenterService.updateRenterInfo(this.renterInfo).then(response=>{
        if(response.status === 200){
          alert("Updated Renter infomation successfully.");
          this.$router.push("/");
        }
      })
      .catch(error=>{
        alert("An error occurred on updating the renter information.", error);
      })
    }
  }

};
</script>

<style scoped>
h1 {
  text-align: center;
}

.form-group {
  padding: 20px;
}
label {
  padding: 20px;
  font-weight: bold;
}
button {
  padding: 20px;
  font-weight: bold;
}
</style>
