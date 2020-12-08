<template>
  <div :property="property">
    <p>{{property.street}}</p>
    <p v-if="property.street2 != ''">{{ property.street2 }}</p>
    <img :src="property.photo" />
    <p>{{ property.city }}, {{ property.region }}</p>
    <p>{{ property.zip }}</p>
    <p>${{ property.price }}</p>
    <p>{{ property.bedrooms }} Bedroom(s) | {{ property.bathrooms }} Bathroom(s)</p>
    <p>{{ property.description }}</p>
  </div>
</template>

<script>
import propertyService from "../services/PropertyService";

export default {
data() {
  return {
    property: {
      addressId: 0,
      bathrooms: 0,
      bedrooms: 0,
      city: "",
      description: "",
      photo: "",
      price: 0,
      propertyId: 0,
      property_Type: "",
      region: "",
      street: "",
      street2: "",
      userId: 0,
      zip: ""
    }
  }
},

created() {
  const propId = this.$route.params.id;
  propertyService.getProperty(propId).then(response => {
    if(response.status===200) {
      this.property = response.data;
    }
  }).catch(error => {
    if (error.response) {
          alert(
            "Error retrieving property. Response from server was " +
              error.response.statusText +
              "."
          );
        } else if (error.request) {
          alert("Error retrieving property. Could not connect to server.");
        } else {
          alert("Error retrieving property. Request could not be created.");
        }
  });
}
}
</script>

<style scoped>
div > img {
  max-width: 200px;
}
</style>