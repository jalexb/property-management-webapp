<template> 

<div style="width: 80%; margin-left: 10%; text-align: center;">
  <h1>Unassigned Tickets</h1>
  <v-flex>
  <v-card >
    <v-card-text>
      <table style="width: 100%">
        <tr>
          <th>Renter Name</th>
          <th>Email</th>
          <th>Phone Number</th>
          <th>Address</th>
          <th>Problem</th>
          <th>Assigned To</th>
          <th>Confirm Assignment</th>
        </tr>
        <tr v-for="data in datas" :key="data.requestId" hover>
        <td>{{data.renterInformation.firstName}} {{ data.renterInformation.lastName }}</td>  
        <td>{{data.renterInformation.email}}</td>
        <td>{{data.renterInformation.phoneNumber}}</td>
        <td>{{data.property.address.street}} {{data.property.address.street2}}</td>
        <td>{{data.requestInfo}}</td>
        <td>
          <select v-model="ticket.workerId">
            <option value=null> -- Select-- </option>
            <option v-for="worker in workers"
                    v-bind:key="worker.userId" 
                          :value="worker.userId"> {{ worker.firstName }} {{ worker.lastName }}
            </option>
          </select>
        </td>
        <td>
          <button v-on:click="confirmAssignment(data)" class="approve">
              Confirm Assignment
          </button>
          &nbsp;
        </td>
        </tr>
      </table>  
    </v-card-text>
  </v-card>
  </v-flex>    
</div>

</template>

<script>

import MaintenanceService from '@/services/MaintenanceService.js';
import WorkersService from '@/services/WorkersService.js';

export default {
  name: 'pending-leases',
  data() {
    return {
      datas:[],
      
      workers: [],
        ticket: {
          "requestId": 0,
          "workerId": null
        }
      
    }
  },
  created() {
    
      this.getUnassignedTickets();
      this.getWorkers();
      
  },
  methods: {
    //get renter information
    getUnassignedTickets() {
      MaintenanceService.getUnassignedTickets().then(response => {
        this.datas = response.data;
      });
    },
    getWorkers() {
      WorkersService.getWorkers().then(response => {
        this.workers = response.data;
      });
    },
    //approve lease
    confirmAssignment(data) {
      console.log (this.ticket.workerId);
      console.log (data);
      if(this.ticket.workerId == null){
        alert("Please select a worker");
      }
      else{
        this.ticket.requestId = data.requestId;
        MaintenanceService.assign(this.ticket).then(response => {
        if(response.status === 200) {
          alert('Accepted');  
          this.getUnassignedTickets();
        }
      }
     );
    }}
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