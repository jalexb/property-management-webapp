<template>
<div>
  <h1>Rent Transactions</h1>
  <v-card
  v-for="transaction in transactions"
  :key="transaction.transactionId"
  max-width="600"
  class="ma-6"
  >
  <p> Due Date {{transaction.payment_Due_Date}} </p>
  <p> Rent Price ${{transaction.rent_Price}} </p>
  <p> Late Fees ${{transaction.late_Fees}} </p>
  <p> Amount Paid ${{transaction.amount_Paid}} </p>
  <p> Amount Due ${{transaction.amount_Due}} </p>
  </v-card>
  <!--<v-simple-table
  fixed-header
  height="400"
  >
  <div class="table-div">
  <table>
      <thead>
          <tr>
              <th>Due Date</th>
              <th>Amount</th>
              <th>Status</th>
          </tr>
      </thead>
      <tbody v-for="transaction in transactions" v-bind:key="transaction.transactionId">
          <tr>
              <td> {{transaction.dueDate}} </td>
              <td> ${{transaction.price}} </td>
              <td> {{transaction.status}} </td>
              <td v-if="transaction.status === 'Owed'">
                  <button class="pay-btn">Pay Now</button>
              </td>
              <td v-else>&nbsp;</td>
          </tr>
      </tbody>
  </table>
  </div>
  </v-simple-table>-->
</div>
</template>

<script>
import RenterService from '../services/RenterService';

export default {
data () {
    return {
        currentUserId: this.$store.state.user.userId,
        transactions: []
    }
},
created () {
    RenterService.getRenterTransactions(this.currentUserId).then(response => {
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

<style scoped>

.table-div {
    margin-right: auto;
    margin-left: auto;
    background-color: white;
    max-width: 60%;
}

table {
    margin-left: auto;
    margin-right: auto;
    max-width: 100%;
}

tbody:nth-child(2n) {
    background-color: lightgray;
}

td {
    padding-left: 50px;
    padding-right: 50px;
}

.pay-btn {
    color: blue;
}

.pay-btn:hover {
    text-decoration: underline;
}
</style>