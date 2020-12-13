<template>
  <div class="completed-applications">
    <table>
      <tr>
        <th>Applicant Name</th>
        <th>Email</th>
        <th>Phone Number</th>
        <th>From Date</th>
        <th>To Date</th>
        <th>Location</th>
        <th>Status</th>
      </tr>
      <tr v-for="completedApplication in completedApplications" v-bind:key="completedApplication.leaseId">
        <td>{{ completedApplication.renterInformation.firstName }} {{ completedApplication.renterInformation.lastName }}</td>
        <td>{{ completedApplication.renterInformation.email }}</td>
        <td>{{ completedApplication.renterInformation.phoneNumber }}</td>
        <td>{{ completedApplication.fromDate }}</td>
        <td>{{ completedApplication.toDate }}</td>
        <td>{{ completedApplication.property.address.street }}</td>
        <td>{{ completedApplication.currentStatus }}</td>
      </tr>
    </table>
  </div>
</template>

<script>
import LeaseService from "../services/LeaseService.js"


export default {
  data() {
    return {
      completedApplications: {
      
      }
    }
  },
  name: "completed-applications",
  created(){
    LeaseService.getCompletedApplications().then(response=>{
      if(response.status === 200){
        this.completedApplications = response.data;
      }
    })
  }
  
  
};
</script>

<style scoped>
h1 {
  text-align: center;
}

.form-group {
  padding: 20px;
}
label {
  padding: 20px;
  font-weight: bold;
}
button {
  padding: 20px;
  font-weight: bold;
}
</style>
