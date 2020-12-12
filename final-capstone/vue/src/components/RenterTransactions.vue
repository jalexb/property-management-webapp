<template>
<div>
  <h1>Rent Transactions</h1>
  <v-card
  v-for="transaction in transactions"
  :key="transaction.transactionId"
  max-width="600"
  class="mx-auto px-2 my-4 py-1"
  >
  <p> <span>Due Date</span> <span>{{transaction.payment_Due_Date}}</span> </p>
  <hr>
  <p> <span>Rent Price</span> <span>${{transaction.rent_Price}}</span> </p>
  <hr>
  <p> <span>Late Fees</span> <span>${{transaction.late_Fees}}</span> </p>
  <hr>
  <p> <span>Amount Paid</span> <span>${{transaction.amount_Paid}}</span> </p>
  <hr>
  <p> <span>Amount Due</span> <span>${{transaction.amount_Due}}</span> </p>
  </v-card>
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
p {
    margin: 5px 0 5px 0;
    display: flex;
    justify-content: space-between;
}

</style>