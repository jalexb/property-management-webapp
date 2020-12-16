import axios from 'axios';

const http = axios.create({
    baseURL: "https://localhost:44315"
  });

  export default {
  getAssignedTickets(userId) {
      return http.get(`/maintenance/assigned/${userId}`);
  }, 
//added back in getUnassignedTickets, submitTicket and assign
  getUnassignedTickets(){
      return http.get('/maintenance/unassigned');
  },
  markTicketComplete(request_Id, ticket) {
      return http.put(`maintenance/tickets/${request_Id}`, ticket);
  },
  submitTicket(ticket){
      return http.post('submit/ticket', ticket);
},
  assign(ticket){
      return http.put('maintenance/assign', ticket);
  }
}