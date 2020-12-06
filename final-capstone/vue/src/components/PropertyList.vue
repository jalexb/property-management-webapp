<template>
  <div>
      <div 
        class="property"
        v-for="property in properties"
        v-bind:key="property.propertyId"
        >
        <img :src="property.photo" />
        {{property.description}}

      </div>
  </div>
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
.property {
    margin: 15px;
}

.property > img {
    display: block;
    max-width: 200px;
}
</style>