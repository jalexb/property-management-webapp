<template>
<div>
  <h1>Rent Transactions</h1>
  <v-simple-table
  fixed-header
  height="400"
  >
  <table>
      <thead>
          <tr>
              <th>Due Date</th>
              <th>Amount</th>
              <th>Status</th>
          </tr>
      </thead>
      <tbody>
          <tr v-for="transaction in transactions" v-bind:key="transaction.transactionId">
              <td> {{transaction.dueDate}} </td>
              <td> {{transaction.price}} </td>
              <td> {{transaction.status}} </td>
          </tr>
      </tbody>
  </table>
  </v-simple-table>
</div>
</template>

<script>
import RenterService from '../services/RenterService';

export default {
data () {
    return {
        currentUser: this.$store.state.user,
        transactions: []
    }
},
created () {
    RenterService.getRenterTransactions(this.currentUser).then(response => {
        if(response.status===200){
            this.transactions = response.data;
        }
    }).catch(error => {
        if (error.response) {
          alert(
            "Error retrieving transactions. Response from server was " +
              error.response.statusText +
              "."
          );
        } else if (error.request) {
          alert("Error retrieving transactions. Could not connect to server.");
        } else {
          alert("Error retrieving transactions. Request could not be created.");
        }
    });
}
}
</script>

<style>

</style>