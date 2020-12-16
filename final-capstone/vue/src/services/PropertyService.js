import axios from 'axios';

const http = axios.create({
    baseURL: "https://localhost:44315"
  });

export default {
    getProperties() {
        return http.get('/property');
    },

    getProperty(id) {
        return http.get(`/property/${id}`);
    },

    getPrice(property_id) {
        return http.get('/property/price/' + property_id);
    },

    checkIfUserAppliedForProperty(property_id, user_Id) {
        return http.post(`lease/${user_Id}/${property_id}`);
    }
}