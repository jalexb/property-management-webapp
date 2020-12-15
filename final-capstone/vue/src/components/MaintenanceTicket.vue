git
<template>
  <div class="maintenance-page">
    <div v-show="!submitted">
      <v-card>
        <v-card-title>
          You're request has been submitted!
        </v-card-title>
        <v-card-text>
          We will process your request immediately and get a maintenance
          employee to handle this issue as soon as possible.
        </v-card-text>
      </v-card>
    </div>
    <div v-show="submitted">
      <h1>Maintenance Request</h1>
      <p>
        We're sorry for any inconveniences that led you here. We will let you
        know the date the maintenance staff will be coming. Please note that you
        are allowing our staff to enter your home to complete the service.
      </p>
      <p>
        Please verify that all this information is correct and then fill out the
        description of what is wrong.
      </p>

      <v-card></v-card>

      <v-form @submit.prevent="submit">
        <v-text-field
          v-model="User.fullName"
          label="Name"
          :counter="40"
          required
        >
        </v-text-field>

        <v-text-field v-model="User.email" label="Email" required>
        </v-text-field>

        <v-text-field
          v-model="User.phoneNumber"
          :counter="10"
          :rules="{
            required: true,
            digits: 10,
            regex: '^(71|72|74|76|81|82|84|85|86|87|88|89)\\d{5}$',
          }"
          label="Phone Number"
          required
        >
        </v-text-field>

        <v-text-field v-model="User.address" label="Address" required>
        </v-text-field>

        <v-textarea
          v-model="ticket.Request_Info"
          label="Maintenance Description"
        >
        </v-textarea>

        <v-btn type="submit" outlined rounded v-on:click="myFilter" target="">
          Submit
        </v-btn>
      </v-form>
    </div>
  </div>
</template>

<script>
import RenterService from "@/services/RenterService";

export default {
  name: "maintenance-ticket",

  data() {
    return {
      User: this.user(),
      requests: "",
      propertyId: 0,

      ticket: {
        Request_Info: "",
        UserId: null,
      },
      submitted: true,
    };
  },
  methods: {
    myFilter: function(){
      this.submitted = !this.submitted;
    },

    populateTicket() {
      this.ticket.Renter_Id = this.User.user_Id;
      this.ticket.Property_Id = this.User.property_Id;
    },
    user() {
      return RenterService.getUsersRenterInformation(
        this.$store.state.user.userId
      )
        .then((response) => {
          this.User = response.data;
        })
        .catch((error) => {
          alert(error);
        });
    },
    addMaintenanceTicket() {
      this.populateTicket();
      console.log(this.ticket);
      RenterService.addMaintenanceTicket(this.ticket)
        .then((response) => {
          if (response.status === 200) {
            alert("Ticket received. We will get back with you soon!");
            this.$router.push("/");
          }
        })
        .catch((error) => {
          alert(error);
        });
    },
  },
};
</script>

<style>
.maintenance-page {
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
