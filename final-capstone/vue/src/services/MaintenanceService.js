import axios from 'axios';

const http = axios.create({
    baseURL: "https://localhost:44315"
  });

  export default {
  getAssignedTickets(userId) {
      //return http.get(`/maintenance/assigned/${userId}`);
      return [
          {
            request_Id: 1,
            renter_Id: 3,
            worker_Id: userId,
            request_Info: "The heat isn't working.",
            property_Id: 4,
            is_Assigned: true,
            is_Fixed: false,
            post_Fix_Report: "",
            street: "2800 Madison Rd",
            street2: "Suite 802"
          },
          {
            request_Id: 2,
            renter_Id: 3,
            worker_Id: userId,
            request_Info: "The garbage disposal in the sink isn't working.",
            property_Id: 4,
            is_Assigned: true,
            is_Fixed: false,
            post_Fix_Report: "",
            street: "2800 Madison Rd",
            street2: "Suite 802"
          }
      ]
  },
  markTicketComplete(request_Id, ticket) {
      return http.put(`maintenance/tickets/${request_Id}`, ticket);
  }
}