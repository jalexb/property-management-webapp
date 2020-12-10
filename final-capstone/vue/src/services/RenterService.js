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
          fullName: "Jacob Barnett",
          email: "jabarnett97@gmail.com",
          phoneNumber: "5677125972",
          address: "808 East Wooster Street, Bowling Green, Ohio, 4584923",
          userId: userId,
      }
      
      //return http.get(`/renter/${userId}`);
    },
    addMaintenanceTicket(serviceDetails) {
      if(serviceDetails.length > 0) {
        return true;
      }
      return false;
    }
  }