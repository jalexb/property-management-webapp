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
          <th>Approve / Reject</th>
        </tr>
        <tr
        v-for="data in datas" 
        :key="data.lease.lease_Id"
        hover>
          <td>{{data.renterInfo.fullName}}</td>
          <td>{{data.renterInfo.email}}</td>
          <td>{{data.renterInfo.phoneNumber}}</td>
          <td>{{new Date(data.lease.from_Date).toISOString().substring(0,10)}}</td>
          <td>{{new Date(data.lease.to_Date).toISOString().substring(0,10)}}</td>
          <td>{{data.renterInfo.address != null ? data.renterInfo.address.split(',')[0] : ""}}</td>
          
          
          <td >
            <button v-show="data.lease.lease_Type === 'pending'" v-on:click="approveLease(data)" class="approve"> Approve </button> &nbsp;
            <button v-show="data.lease.lease_Type === 'pending'" v-on:click="rejectLease(data)" class="reject"> Reject </button>
            {{data.lease.lease_Type === 'pending' ? "" : data.lease.lease_Type}}
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
import TransactionService from '@/services/TransactionService';
import PropertyService from '@/services/PropertyService'
import RenterService from '@/services/RenterService';

export default {
  name: 'pending-leases',
  data() {
    return {
      datas:[{
        lease: {
          lease_Id: null, 
          from_Date: null, 
          to_Date: null, 
          user_Id: null,
          property_Id: null,
          lease_Type: null,
        },
        renterInfo: {
          address: null,
          email: null,
          fullName: null,
          phoneNumber: null,
          property_Id: null,
          user_Id: null,
        },
        rent_Price: null,
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
      LandlordService.getLeases(this.userId).then(response => {
        this.datas = response.data;
      });
    },
    //approve lease
    approveLease(data) {
      LandlordService.approveLease(data.lease.lease_Id, data.renterInfo.user_Id).then(response => {
        if(response.status === 204) {
          alert('Accepted');
          
          //populate transaction table with 12 months of rent due.
          data = this.addRentPriceToDate(data);
          data = this.populateTransactions(data);
          this.updateUserRole(data.renterInfo.user_Id);

          this.getInformation();
        }
      });
    },

    addRentPriceToDate(data) {
      PropertyService.getPrice(data.lease.property_Id)
      .then(response => 
      { 
          data.rent_Price = response.data
      })

      return data;
    },
    //reject lease
    rejectLease(data) {
      LandlordService.rejectLease(data.lease.lease_Id).then(response => {
        if(response.status === 200) {
          alert('Rejected');
          this.getInformation();
        }
      });
    },
    //Populate transaction table with 12 months of rent, 1 at a time.
    //throws from dateand to date to the backend, let the back end handle it.
    populateTransactions(data) {
      //Lease_Id, Property_Id, Payment_Due_Date

      let toDate = new Date(data.lease.to_Date).toISOString();
      let fromDate = new Date(data.lease.from_Date).toISOString();
      let initialTransaction = {
        Lease_Id: data.lease.lease_Id,
        Property_Id: data.lease.property_Id,
        From_Date: fromDate.substring(0,10),
        To_Date: toDate.substring(0,10),
        Rent_Price: data.rent_Price,
      };

      

      //console.log(initialTransaction);

      TransactionService.leaseAccepted_PopulateTransactions( initialTransaction )
      .catch(error => {
          console.log(error);
      })

      return data;
    },

    updateUserRole(userId) {
      console.log(userId);
      RenterService.updateRole_UserToRenter(userId).catch(error => console.log(error));
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