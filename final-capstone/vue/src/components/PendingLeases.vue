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
        v-for="data in datas" 
        :key="data.lease_Id"
        hover>
          <td>{{data.renter_Info.fullName}}</td>
          <td>{{data.renter_Info.email}}</td>
          <td>{{data.renter_Info.phoneNumber}}</td>
          <td>{{data.pending_Lease.from_Date}}</td>
          <td>{{data.pending_Lease.to_Date}}</td>
          <td>{{data.renter_Info.address}}</td>
          <td>
            <button v-on:click="approveLease(data.pending_Lease.lease_Id)" class="approve"> Approve </button> &nbsp;
            <button v-on:click="rejectLease(data.pending_Lease.lease_Id)" class="reject"> Reject </button>
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
      datas:[{
        pending_Lease: {
            lease_Id: null, 
            from_Date: null, 
            to_Date: null, 
            user_Id: null,
            property_Id: null,
        },
        renter_Info: {
          address: null,
          email: null,
          fullName: null,
          phoneNumber: null,
          property_Id: null,
          user_Id: null,
        }
      }],
        
      userId: null,
      
    }
  },
  created() {
    //returns and object with pendingLeases with that lease's userInformation.
      this.userId = this.$store.state.user.userId;
      this.getInformation();
      
  },
  methods: {
    //get renter information
    getInformation() {
      LandlordService.getPendingLeases(this.userId).then(response => {
        this.datas = response.data;
      });
    },
    //approve lease
    approveLease(leaseId) {
      LandlordService.approveLease(leaseId).then(response => {
        if(response.status === 200) {
          alert('Accepted');
          this.getInformation();
        }
      });
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