<template>
<div>
  <h1>Rent Transactions</h1>
  <v-card
  v-for="transaction in transactions"
  :key="transaction.transactionId"
  max-width="600"
  class="mx-auto px-2 my-4 py-1"
  >
  <p> <span>Due Date</span> <span>{{new Date(transaction.payment_Due_Date).toDateString()}}</span> </p>
  <hr>
  <p> <span>Rent Price</span> <span>${{transaction.rent_Price}}</span> </p>
  <hr>
  <p> <span>Late Fees</span> <span>${{transaction.late_Fees}}</span> </p>
  <hr>
  <p> <span>Amount Paid</span> <span>${{transaction.amount_Paid}}</span> </p>
  <hr>
  <p> <span>Amount Due</span> <span>${{transaction.amount_Due}}</span> </p>
  <hr>
  <v-row justify="center">
  <v-btn v-if="transaction.paid===false && onPayScreen===false"
  v-on:click="onPayScreen = true" 
  outlined
  raised 
  rounded
  color="#BA3F1D">
  Pay Now
  </v-btn>
  </v-row>

  <div v-if="onPayScreen">
  <p>
      <span>
        <label for="amount">Amount: $</label>
        <input type="number" name="amount" id="amount" v-model="transaction.amount_Paid">
      </span>
  <v-btn v-if="transaction.paid===false && onPayScreen"
  v-on:click="payRent(transaction.transaction_Id, transaction)"
  outlined
  raised 
  rounded
  color="#BA3F1D">Pay
  </v-btn>
  </p>
  </div>
  </v-card>
</div>
</template>

<script>
import RenterService from '../services/RenterService';

export default {
data () {
    return {
        currentUserId: this.$store.state.user.userId,
        transactions: [
            {
                transaction: {
                    amount_Due: 0,
                    amount_Paid: 0,
                    late_Fees: 0,
                    lease_Id: 0,
                    paid: false,
                    payment_Due_Date: null,
                    property_Id: 0,
                    rent_Price: 0,
                    transaction_Id: 0
                }
            }
        ],
        onPayScreen: false
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
},
methods: {
    payRent(transactionId, transaction) {
        RenterService.rentPayment(transactionId, transaction);
    }
}
}
</script>

<style scoped>
p {
    margin: 5px 0 5px 0;
    display: flex;
    justify-content: space-between;
}

</style>