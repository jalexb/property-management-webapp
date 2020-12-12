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
      /*
      return [
          {
            userId: userId,
            transactionId: 1,
            dueDate: '12-01-2020',
            amountDue: 800,
            lateFees: 0,
            amountPaid: 800,
            amountLeft: 0
          },
          {
            userId: userId,
            transactionId: 2,
            dueDate: '01-01-2021',
            amountDue: 800,
            lateFees: 0,
            amountPaid: 0,
            amountLeft: 800
          }
        ]
        */
    }
  }