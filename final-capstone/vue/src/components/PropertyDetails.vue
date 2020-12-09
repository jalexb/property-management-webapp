<template>
  <div :property="property">
    <h1>{{property.street}}</h1>
    <p v-if="property.street2 != ''">{{ property.street2 }}</p>
    <img :src="property.photo" />
    <p>{{ property.city }}, {{ property.region }} {{ property.zip }}</p>
    <p><span class="bold-text">Rent:</span> ${{ property.price }}</p>
    <p><span class="bold-text">Bedrooms:</span> {{ property.bedrooms }} | <span class="bold-text">Bathrooms:</span> {{ property.bathrooms }}</p>
    <div class="description">
    <p>{{ property.description }}</p>
    </div>
    <div v-if="this.$store.state.user.role==='user'" class="button">
    <v-btn
            outlined
            raised 
            rounded
            color="#BA3F1D"
            >
            Apply For Lease
      </v-btn>
    </div>
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
  max-width: 30%;
  display: block;
  margin-left: auto;
  margin-right: auto;
}

h1 {
  text-align: center;
  color: #BA3F1D;
}

p {
  text-align: center;
}

.description {
  max-width: 35%;
  display: block;
  margin-left: auto;
  margin-right: auto;
}

.description > p {
  text-align: left;
}

.bold-text {
  font-weight: 700;
}

.button {
  max-width: 15%;
  display: flex;
  justify-content: center;
  margin-left: auto;
  margin-right: auto;
  margin-bottom: 5px;
  align-content: center;
}

</style>