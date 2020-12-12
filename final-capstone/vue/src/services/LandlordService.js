import axios from 'axios'

const http = axios.create({
    baseURL: "https://localhost:44315"
  });

export default {
    //get pending leases
    getPendingLeases(landlordId) {
      return http.get('/lease/pending/' + landlordId)
      /*return [
        {
          Lease_Id: 2, 
          From_Date: '2020-12-17', 
          To_Date: '2021-12-17', 
          User_Id: 6, 
          Property_Id: 11,
          User_Info: {
            address:"37942 Eagle DrGatlinburgTennessee37738",
            email:"j",
            fullName:"f j",
            phoneNumber:"j",
            property_Id:11,
            user_Id:6,
          }
        },
        {
          Lease_Id: 3, 
          From_Date: '2020-12-17', 
          To_Date: '2021-12-17', 
          User_Id: 6, 
          Property_Id: 12,
          User_Info: {
            address:"37942 Eagle DrGatlinburgTennessee37738",
            email:"j",
            fullName:"f j",
            phoneNumber:"j",
            property_Id:11,
            user_Id:6,
          }
        },
        {
          Lease_Id: 4, 
          From_Date: '2020-12-17', 
          To_Date: '2021-12-17', 
          User_Id: 6, 
          Property_Id: 13,
          User_Info: {
            address:"37942 Eagle DrGatlinburgTennessee37738",
            email:"j",
            fullName:"f j",
            phoneNumber:"j",
            property_Id:11,
            user_Id:6,
          }
        }
      ]*/
    },
    approveLease(leaseId) {
      return http.post('lease/approve/' + leaseId);
    },
    rejectLease(leaseId) {
      return http.post('lease/reject/' + leaseId);
    }
}