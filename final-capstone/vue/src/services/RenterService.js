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
    }
  }