import axios from 'axios'

const http = axios.create({
    baseURL: "https://localhost:44315"
  });

export default {
    //get pending leases
    getPendingLeases(landlordId) {
      return http.get('/leases/' + landlordId)
    },
    approveLease(leaseId) {
      return http.post('lease/approve/' + leaseId);
    },
    rejectLease(leaseId) {
      return http.post('lease/reject/' + leaseId);
    },
    getLandlordProperties(landlord_id) {
      return http.get('/getAllProperties/' + landlord_id)
    },
    createNewProperty(newProperty) {
      return http.post('/addNewProperty', newProperty);
    }
}