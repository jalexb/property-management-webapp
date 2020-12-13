import axios from 'axios';

const http = axios.create({
    baseURL: "https://localhost:44315"
  });

  export default {
    saveRenter(form) {
        return http.post('/renter', form);
    },
    getUsersRenterInformation(userId) {
      return http.get('renter/' + userId);
      
      //return http.get(`/renter/${userId}`);
    },
    addMaintenanceTicket(serviceDetails) {
      return http.post('/submit/ticket/', serviceDetails);
    },
    getRenterTransactions(userId) {
      return http.get(`/transaction/${userId}`);
    },
    rentPayment(transactionId, transaction) {
      return http.put(`/transaction/${transactionId}`, transaction);
    },
    updateRole_UserToRenter(userId) {
        return http.put('/renter/userRole/' + userId)
    }
  }