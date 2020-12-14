<template>
  <v-form v-model="valid">
    <div class="field">
      <h1>Lease Application</h1>
    </div>
    <div class="form">
      <v-text-field
        v-model="renter.firstName"
        :rules="nameRules"
        :counter="20"
        label="First Name"
        required
      >
      </v-text-field>

      <v-text-field
        v-model="renter.lastName"
        :rules="nameRules"
        :counter="20"
        label="Last Name"
        required
      >
      </v-text-field>

      <v-text-field
        v-model="renter.phoneNumber"
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

      <v-text-field
        v-model="renter.email"
        :rules="emailRules"
        label="E-mail"
        required
      >
      </v-text-field>

      <v-menu
        ref="menu"
        v-model="menu"
        :close-on-content-click="false"
        :return-value.sync="date"
        transition="scale-transition"
        offset-y
        min-width="290px"
      >
        <template v-slot:activator="{ on, attrs }">
          <v-text-field
            v-model="pendingLease.fromDate"
            label="From Date"
            prepend-icon="mdi-calendar"
            readonly
            v-bind="attrs"
            v-on="on"
          >
          </v-text-field>
        </template>

        <v-date-picker v-model="pendingLease.fromDate" no-title scrollable>
          <v-spacer></v-spacer>
          <v-btn text color="primary" @click="menu = false">
            Cancel
          </v-btn>
          <v-btn text color="primary" @click="$refs.menu.save(date)">
            OK
          </v-btn>
        </v-date-picker>
      </v-menu>

      <v-spacer></v-spacer>
      <h3>To Date:</h3>
      <div>
        &nbsp;
        {{ setToDate() }}
      </div>

      <v-spacer></v-spacer>
      <div class="actions">
        <v-btn class="btn btn-primary" type="submit" outlined rounded>
          Save Application
        </v-btn>
        <!-- <button type="submit" class="btn btn-primary">Save Application</button> -->
      </div>
    </div>
  </v-form>
</template>

<script>
import LeaseService from "../services/LeaseService.js";
import RenterService from "../services/RenterService.js";

export default {
  name: "lease-form",
  data() {
    return {
      pendingLease: {
        userId: 0,
        propertyId: 0,
        fromDate: null,
        toDate: null,
        isApproved: null,
      },
      renter: {
        user_Id: 0,
        firstName: null,
        lastName: null,
        currentAddress: null,
        phoneNumber: null,
        email: null,
        leaseType: null,
        salary: 0.0,
      },
    };
  },
  created() {
    this.pendingLease.propertyId = parseInt(this.$route.params.id);
    this.getRenterInformation();
  },
  newData: () => ({
    date: new Date().toISOString().substr(0, 10),
    menu: false,
    modal: false,
    menu2: false,
  }),
  methods: {
    setToDate() {
      //don't look at it, just know that it works :,)
      if (this.pendingLease.fromDate !== null) {
        let fromDate = new Date(this.pendingLease.fromDate);
        let nextYear = fromDate.getFullYear() + 1;
        let thisMonth = fromDate.getUTCMonth() + 1;
        let thisDay = fromDate.getUTCDate();
        let toDate =
          nextYear +
          "-" +
          thisMonth +
          "-" +
          (thisDay < 10 ? "0" + thisDay : thisDay);
        this.pendingLease.toDate = toDate;
        return thisMonth + "/" + thisDay + "/" + nextYear; //12/11/2020
      }
    },
    getRenterInformation() {
      return RenterService.getUsersRenterInformation(
        this.$store.state.user.userId
      ).then((response) => {
        this.renter = response.data;
        let firstAndLast = response.data.fullName.split(" ");
        this.renter.firstName = firstAndLast[0];
        this.renter.lastName = firstAndLast[1];
      });
    },
    saveRenter() {
      this.renter.user_Id = this.$store.state.user.userId;
      RenterService.saveRenter(this.renter)
        .then((response) => {
          if (response.status == 200) {
            alert("Renter information has been saved");
            this.$router.push("/");
          } else {
            alert("There was problem saving the Renter information, not saved");
          }
        })
        .catch((error) => {
          console.log(error);
          alert("There was a problem with the renter info, not saved!");
        });
    },
    saveApplication() {
      this.pendingLease.userId = this.$store.state.user.userId;
      console.log(this.$route.params.id);
      LeaseService.submitLeaseForm(this.pendingLease)
        .then((response) => {
          if (response.status === 200) {
            this.saveRenter();
            alert("Application has been saved");
          } else {
            alert("There was a problem with the application, not saved!");
          }
        })
        .catch((error) => {
          console.log(error);
          alert("There was a problem with the application, not saved!");
        });
    },
  },
};
</script>

<style>
.form {
  background-color: white;
}
</style>
