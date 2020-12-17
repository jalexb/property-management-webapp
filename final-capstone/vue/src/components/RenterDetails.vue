<template>
<v-card>
    <v-card-title @click="showing = !showing">{{property.renter.info.fullName}}</v-card-title>
    <table style="width: 100%" v-show="showing">
        
        <tr>
            <th>Amount Due</th>
            <th>Amount Paid</th>
            <th>Late Fees</th>
            <th>Due Date</th>
            <th>Paid</th>
        </tr>
        <tr v-for="transaction in property.renter.transactionHistory" :key="transaction.transaction_Id" style="text-align: center">
            <td>{{transaction.amount_Due}}</td>
            <td>{{transaction.amount_Paid}}</td>
            <td>{{transaction.late_Fees}}</td>
            <td>{{new Date(transaction.payment_Due_Date).toISOString().substring(0,10)}}</td>
            <td>{{transaction.paid === true ? "Yes" : "No"}}</td>
        </tr>
    </table>
</v-card>
</template>

<script>
export default {
    props:['property'],

    data() {
        return {
            showing: false,
        }
    }

}
</script>

<style>

</style>