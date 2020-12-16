import axios from 'axios';

const http = axios.create({
    baseURL: "https://localhost:44315"
  });

  export default {
    submitLeaseForm(form) {
      console.log(form);
        return http.post('/lease', form);
    },
    getCompletedApplications(){
      return http.get('/completedApplications');
    }

  }