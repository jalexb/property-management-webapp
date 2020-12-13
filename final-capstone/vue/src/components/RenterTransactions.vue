<template>
<div>
  <h1>Rent Transactions</h1>
  <v-card
  v-for="transaction in transactions"
  :key="transaction.transactionId"
  max-width="600"
  class="mx-auto px-2 my-4 py-1"
  hover
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
    <hr v-if="!transaction.paid">

    <v-row justify="center">
    <v-btn v-if="!transaction.paid && !onPayScreen"
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
        <input type="number" name="amount" id="amount" v-model="amount_Paid">
      </span>
  <v-btn v-if="!transaction.paid && onPayScreen"
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
        amount_Paid: 0,
        onPayScreen: false
    }
},
created () {
    this.getTransactions(this.currentUserId);
},
methods: {
    getTransactions(userId) {
        RenterService.getRenterTransactions(userId).then(response => {
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
    payRent(transactionId, transaction) {
        if(this.amount_Paid > 0) {
            let payment_Due_Date = new Date(transaction.payment_Due_Date).toISOString();
            transaction.amount_Paid = parseInt(this.amount_Paid);
            transaction.payment_Due_Date = payment_Due_Date.substring(0, 10);
            RenterService.rentPayment(transactionId, transaction).then(response => {
                if(response.status===200) {
                    alert("Your payment has been received!");
                    this.getTransactions(this.currentUserId);
                }
            }).catch(error => {
                if (error.response) {
                    alert(
                    "Error making payment. Response from server was " +
                     error.response.statusText +
                     "."
            );
                } else if (error.request) {
                alert("Error making payment. Could not connect to server.");
                } else {
                 alert("Error making payment. Request could not be created.");
                }
            });
        }
        else {
            alert("Please enter a valid amount of payment.");
        }
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