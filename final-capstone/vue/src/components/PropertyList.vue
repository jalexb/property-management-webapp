<template>
  <v-app>
      <div 
        class="property"
        v-for="property in properties"
        v-bind:key="property.id"
        >
      </div>
  </v-app>
</template>

<script>
import propertyService from '../services/PropertyService';

export default {
    data() {
        return {
            properties: []
        }
    },
    created() {
      propertyService.getProperties().then(response => {
          if(response.status===200){
              this.properties = response.data;
          }
      }).catch(error => {
          if(error.response){
              alert("Error retrieving properties. Response from server was " + error.response.statusText + ".");
          }
          else if(error.request){
              alert("Error retrieving properties. Could not connect to server.");
          }
          else {
              alert("Error retrieving properties. Request could not be created.");
          }
      });  
    }
}
</script>

<style>

</style>