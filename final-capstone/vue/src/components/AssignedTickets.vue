<template>
  <div>
      <v-card
      v-for="ticket in tickets"
      :key="ticket.request_Id"
      max-width="600"
      class="mx-auto my-6 pa-3"
      >
      <p><span class="bold-text">Address: </span>{{ticket.street}}</p>
      <hr>
      <p v-show="ticket.street2 != 'N/A'"><span class="bold-text">Unit: </span>{{ticket.street2}}</p>
      <hr>
      <p><span class="bold-text">Request Information: </span>{{ticket.request_Info}}</p>
      <hr>
      <div v-if="ticket.is_Fixed">
          <p><span class="bold-text">Post-Fix Report: </span>{{ticket.post_Fix_Report}}</p>
      </div>
      <div v-else>
        <label for="report"><span class="bold-text">Post-Fix Report: </span></label>
        <textarea id="report" name="report" rows="3" cols="50" v-model="ticket.post_Fix_Report" />
        <v-row justify="center">
        <v-btn
        outlined
        raised 
        rounded
        color="#BA3F1D"
        v-on:click="markTicketComplete(ticket.request_Id, ticket)"
        class="ma-2"
        >
            Mark Completed
        </v-btn>
        </v-row>
      </div>
      </v-card>
  </div>
</template>

<script>
import maintenanceService from '../services/MaintenanceService'

export default {
data() {
    return {
        tickets: [
            {
                ticket: {
                    request_Id: 0,
                    renter_Id: 0,
                    worker_Id: 0,
                    request_Info: "",
                    property_Id: 0,
                    is_Assigned: false,
                    is_Fixed: false,
                    post_Fix_Report: "",
                    street: "",
                    street2: "N/A",
                }
            }
        ]
    }
},
created() {
    this.getAssignedTickets(this.$store.state.user.userId);
},
methods: {
    getAssignedTickets(userId) {
        maintenanceService.getAssignedTickets(userId).then(response => {
            if(response.status===200) {
                this.tickets = response.data;
            }
        }).catch(error => {
            if (error.response) {
                alert(
                "Error retrieving tickets. Response from server was " +
                    error.response.statusText +
                    "."
                );
                } else if (error.request) {
                    alert("Error retrieving tickets. Could not connect to server.");
                } else {
                    alert("Error retrieving tickets. Request could not be created.");
                }
        });
    },
    markTicketComplete(request_Id, ticket) {
        maintenanceService.markTicketComplete(request_Id, ticket).then(response => {
            if(response.status===200) {
                ticket.is_Fixed = true;
                alert("Ticket has successfully been marked completed!");
            }
        }).catch(error => {
            if (error.response) {
                alert(
                "Error updating ticket. Response from server was " +
                    error.response.statusText +
                    "."
                );
                } else if (error.request) {
                    alert("Error updating ticket. Could not connect to server.");
                } else {
                    alert("Error updating ticket. Request could not be created.");
                }
        });
    }
}
}
</script>

<style scoped>
.bold-text {
  font-weight: 700;
}

hr {
    margin-bottom: 10px;
}
</style>