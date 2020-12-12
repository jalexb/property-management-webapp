<template>
<div style="width: 80%; margin-left: 10%; text-align: center;">
  <h1>Pending Leases</h1>
  <v-flex>
  <v-card >
    <v-card-text>
      <table style="width: 100%">
        <tr>
          <th>Applicant Name</th>
          <th>Email</th>
          <th>Phone Number</th>
          <th>From Date</th>
          <th>To Date</th>
          <th>Location</th>
          <th>Approve / Reject Application</th>
        </tr>
        <tr
        v-for="pendingLease in pendingLeases" 
        :key="pendingLease.Lease_Id"
        :property_id="pendingLease.Property_Id"
        hover>
          <td>{{pendingLease.User_Info.fullName}}</td>
          <td>{{pendingLease.User_Info.email}}</td>
          <td>{{pendingLease.User_Info.phoneNumber}}</td>
          <td>{{pendingLease.From_Date}}</td>
          <td>{{pendingLease.To_Date}}</td>
          <td>{{pendingLease.User_Info.address}}</td>
          <td>
            <v-button v-on:click="approveLease(pendingLease.Lease_Id)" class="approve"> Approve </v-button> &nbsp;
            <v-button v-on:click="rejectLease(pendingLease.Lease_Id)" class="reject"> Reject </v-button>
          </td>
        </tr>
      </table>
    </v-card-text>
    
  </v-card>
  </v-flex>
</div>
</template>

<script>
import LandlordService from '@/services/LandlordService';

export default {
  name: 'pending-leases',
  data() {
    return {
      pendingLeases:[{
          Lease_Id: null, 
          From_Date: null, 
          To_Date: null, 
          User_Id: null,
          userInfo: /*{address, email, fullName, phoneNumber, property_Id, user_Id}*/ null,
          Property_Id: null,
          }],
      userId: this.$store.state.userId,
      
    }
  },
  created() {
    //returns and object with pendingLeases with that lease's userInformation.
      this.pendingLeases = LandlordService.getPendingLeases();
  },
  methods: {
    //get renter information
    
    //approve lease
    approveLease(leaseId) {
      LandlordService.approveLease(leaseId);
    },
    //reject lease
    rejectLease(leaseId) {
      LandlordService.rejectLease(leaseId);
    }
  }
}
</script>

<style>

th {
  border-style: outset;
}
button {
  padding: 0px 10px;
}

.approve:hover {
  background-color: green;
  cursor: pointer;
}

.reject:hover{
  background-color: red;
  cursor: pointer;
}

</style>