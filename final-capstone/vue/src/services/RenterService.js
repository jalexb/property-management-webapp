import axios from 'axios';

const http = axios.create({
    baseURL: "https://localhost:44315"
  });

  export default {
    saveRenter(form) {
        return http.post('/renter', form);
    },
    getUsersRenterInformation(userId) {
      return {
          FullName: "Jacob Barnett",
          Email: "jabarnett97@gmail.com",
          PhoneNumber: "5677125972",
          Address: "808 East Wooster Street, Bowling Green, Ohio, 4584923",
          User_Id: userId,
      }
      
      //return http.get(`/renter/${userId}`);
    },
    addMaintenanceTicket(serviceDetails) {
      if(serviceDetails.length > 0) {
        return true;
      }
      return false;
    },
    getRenterTransactions(userId) {
      //return http.get(`/renter/transactions/${userId}`);
      return [
          {
            userId: userId,
            transactionId: 1,
            dueDate: '12-01-2020',
            price: 800,
            status: 'Paid'
          },
          {
            userId: userId,
            transactionId: 2,
            dueDate: '01-01-2021',
            price: 800,
            status: 'Owed'
          }
        ]

    }
  }