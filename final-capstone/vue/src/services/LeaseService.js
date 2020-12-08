import axios from 'axios';

const http = axios.create({
    baseURL: "https://localhost:44315"
  });

  export default {
    submitLeaseForm(form) {
        return http.post('/lease', form);
    }
  }