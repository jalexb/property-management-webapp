<template>
  <div class="maintenance-page">
    <h1> Maintenance Request </h1>
    <p>We're sorry for any inconveniences that led you here. 
        We will let you know the date the maintenance staff will be coming. 
        Please note that you are allowing our staff to enter your home to complete the service.</p>
            <p>Please verify that all this information is correct and then fill out the description of what is wrong.</p>

    <form v-on:submit.prevent="addMaintenanceTicket">
        <div class="input">
            <label for="Name">Name</label> &nbsp;
            <input type="text" v-model="User.fullName" value="Name"/>
        </div>
        <div class="input">
            <label for="Email">Email</label> &nbsp;
            <input type="email" v-model="User.email" value="Email"/>
        </div>
        <div class="input">
            <label for="PhoneNumber">Phone Number</label> &nbsp;
            <input type="text" v-model="User.phoneNumber" value="PhoneNumber"/>
        </div>
        <div class="input">
            <label for="Address">Address</label> &nbsp;
            <input type="text" v-model="User.address" value="Address"/>
        </div>
        <div class="input">
            <label for="Requests">Maintenance Description</label> &nbsp;
            <input type="text" v-model="requests" value="Requests"/>
        </div>
        <div class="submit">
            <input type="submit" />
        </div>
    </form>
  </div>
</template>

<script>
import RenterService from '@/services/RenterService'

export default {
    name: "maintenance-ticket",

    data() { 
        return {
            User: this.user(),
            requests: "",
            propertyId: 0,
        } 
    },
    methods: {
        user() {
            return RenterService.getUsersRenterInformation(this.$store.state.user.userId)
            .then(response => {
                this.User = response.data;
            }).catch(error => {
                alert(error);
            })
        },
        addMaintenanceTicket() {
            RenterService.addMaintenanceTicket(this.requests);
            
            /*.then(response => {
                if(response.data === true) {
                    alert(response.data)
                }
            })*/
        }
    }
}
</script>


<style>
.maintenance-page {
    width: 80%;
    height: 100%;
    margin-left: 10%;
    background-color: white;
    padding: 10px;
}
h1 {
    text-align: center;
}

.input {
    margin-bottom: 10px;
}
</style>