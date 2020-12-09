<template>
     <form v-on:submit.prevent>
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
                v-model="application.firstname" /></div>
    <h3>Last Name:  </h3> 
        <input type="text" 
                id="lastname" 
                class=lease-form 
                placeholder="Last Name" 
                required 
                autofocus
                v-model="application.lastname" />
    <h3>Date of Birth:  </h3> 
        <input type="text" 
                id="dob" 
                class=lease-form 
                placeholder="Date of Birth" 
                required 
                autofocus
                v-model="application.dob" />
    <h3>Phone Number:  </h3> 
        <input type="text" 
                id="phone-number" 
                class=lease-form 
                placeholder="phone-number" 
                required 
                autofocus
                v-model="application.phonenumber" />
    <h3>email:  </h3> 
        <input type="text" 
                id="email" 
                class=lease-form 
                placeholder="email Address" 
                required 
                autofocus
                v-model="application.email" />
    <h3>lease-property:  </h3> 
        <select v-model="application.propertyid">
            <option value="0">--Select--</option>
            <option v-for="property in properties" v-bind:key="property.propertyId" :value="property.street">{{ property.street }}</option>
        </select>
    <h3>from-date:  </h3> 
        <input type="text" 
                id="from-date" 
                class=from-date 
                placeholder="from-date" 
                required 
                autofocus
                v-model="application.fromdate" />
    <h3>to-date:  </h3> 
        <input type="text" 
                id="to-date" 
                class=to-date 
                placeholder="to-date" 
                required 
                autofocus
                v-model="application.todate" />
     </div>

    <div class="actions">
      <button type="submit" class="btn btn-primary" v-on:click="saveApplication()">Save Application</button>
    </div>
  </form>
</template>

<script>
import LeaseService from "../services/LeaseService.js"
import PropertyService from "../services/PropertyService.js"

export default {
   name:"lease-form",
   data(){
       return {
           application:{
               firstname:"",
               lastname:"",
               dob:"",
               phonenumber:"", 
               email:"",
               propertyid:0,
               fromdate:"",
               todate:"",
               userid:0,
               isapproved:false
           },
           properties:[]
       }
   },
   created() {
       PropertyService.getProperties().then(response=>{
           if (response.status === 200){
               this.properties = response.data;
           }
       })
   },
    methods: {
        saveApplication() {
            this.application.userid = this.$store.state.user.userid;
            console.log(this.$store.state.user.userid);
            LeaseService.submitLeaseForm(this.application) .then (response =>{
                if (response.status === 201){
                    alert("Application has been saved");
                }
                else {
                    alert("There was a problem with the application, not saved!");
                }
            }
            )
        }
    }
}
</script>