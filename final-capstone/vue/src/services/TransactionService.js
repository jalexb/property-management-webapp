import axios from 'axios'

const http = axios.create({
    baseURL: "https://localhost:44315"
  });

export default {
    //populate transaction table with 12 months of rent due.
    leaseAccepted_PopulateTransactions(transaction) {
      return http.put('/transaction/add', transaction);
    }

    //Transaction_Id, Lease_Id, Property_Id, Payment_Due_Date, Late_Fees, Paid, Amount_Paid, Rent_Price

}