import axios from 'axios';

const http = axios.create({
    baseURL: "https://localhost:44315"
  });

  export default {
  getAssignedTickets(userId) {
      return http.get(`/maintenance/assigned/${userId}`);
  },
  markTicketComplete(request_Id, ticket) {
      return http.put(`maintenance/tickets/${request_Id}`, ticket);
  }
}