<template>
     <form v-on:submit.prevent="saveApplication()">
    <div class="field">
     <h1>Lease Application</h1>
    </div>
    <div>
        <div class="input-fields">
    <h3>First Name:  </h3> 
        <input type="text" 
                id="firstname" 
                class=lease-form 
                placeholder="First Name" 
                required 
                autofocus
                v-model="renter.firstName" /></div>
    <h3>Last Name:  </h3> 
        <input type="text" 
                id="lastname" 
                class=lease-form 
                placeholder="Last Name" 
                required 
                autofocus
                v-model="renter.lastName" />
    
    <h3>Phone Number:  </h3> 
        <input type="text" 
                id="phone-number" 
                class=lease-form 
                placeholder="phone-number" 
                required 
                autofocus
                v-model="renter.phoneNumber" />
    <h3>email:  </h3> 
        <input type="text" 
                id="email" 
                class=lease-form 
                placeholder="email Address" 
                required 
                autofocus
                v-model="renter.email" />
    <h3>from-date:  </h3> 
        <input type="text" 
                id="from-date" 
                class=from-date 
                placeholder="from-date" 
                required 
                autofocus
                v-model="pendingLease.fromDate" />
    <h3>to-date:  </h3> 
        <input type="text" 
                id="to-date" 
                class=to-date 
                placeholder="to-date" 
                required 
                autofocus
                v-model="pendingLease.toDate" />
     </div>

    <div class="actions">
      <button type="submit" class="btn btn-primary">Save Application</button>
    </div>
  </form>
</template>

<script>
import LeaseService from "../services/LeaseService.js"
import RenterService from "../services/RenterService.js"

export default {
   name:"lease-form",
   data(){
       return {
           pendingLease:{
  "userId": 0,
  "propertyId": 0,
  "fromDate": null,
  "toDate": null,
  "isApproved": null
},
renter:{
  "userId": 0,
  "firstName": null,
  "lastName": null,
  "currentAddress": null,
  "phoneNumber": null,
  "email": null,
  "leaseType": null,
  "salary": 0.0
}
        
       }
   },
   created(){
       this.pendingLease.propertyId = parseInt(this.$route.params.id);
   },
       methods: {
           saveRenter(){
               this.renter.userId = this.$store.state.user.userId;
               RenterService.saveRenter(this.renter).then (response => {
                   if (response.status == 200) {
                       alert ("Renter information has been saved");
                       this.$router.push('/');
                   } else {
                       alert ("There was problem saving the Renter information, not saved");
                   }
               })
               .catch(error =>{
                    console.log(error);
                 alert("There was a problem with the renter info, not saved!");
               })
           },
        saveApplication() {
            
            this.pendingLease.userId = this.$store.state.user.userId;
            console.log(this.$route.params.id);
            LeaseService.submitLeaseForm(this.pendingLease) .then (response =>{
                if (response.status === 200){
                    this.saveRenter();
                    alert("Application has been saved");
                }
                else {
                    alert("There was a problem with the application, not saved!");
                }
            }
            )
            .catch(error=>{
                console.log(error);
                 alert("There was a problem with the application, not saved!");
            })
        }
    }
}
</script>