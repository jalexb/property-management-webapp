<template>
  <div>
    <v-container
    fluid
    >
          <router-link :to="{ name: 'home' }">
            <!-- Change to like to further details instead of home-->
            <v-flex 
            xs12 
            flex-direction: row
            justify-space-around
            >
              <v-card 
              v-for="property in properties"
              :key="property.propertyId"
              max-width="450" 
              hover>
              <v-img :src="property.photo"> </v-img>
              <v-card-title> {{ property.street }} </v-card-title>
              <v-card-text>
                <p v-if="property.street2 != ''">{{ property.street2 }}</p>
                <p>{{ property.city }}, {{ property.region }}</p>
                <p>${{ property.price }}</p>
                <p>{{ property.bedrooms }} Bedroom(s)</p>
                <p>{{ property.bathrooms }} Bathroom(s)</p>
              </v-card-text>
            </v-card>
            </v-flex>
            
          </router-link>
    </v-container>
  </div>
</template>

<script>
import propertyService from "../services/PropertyService";

export default {
  data() {
    return {
      properties: [],
    };
  },
  created() {
    propertyService
      .getProperties()
      .then((response) => {
        if (response.status === 200) {
          this.properties = response.data;
        }
      })
      .catch((error) => {
        if (error.response) {
          alert(
            "Error retrieving properties. Response from server was " +
              error.response.statusText +
              "."
          );
        } else if (error.request) {
          alert("Error retrieving properties. Could not connect to server.");
        } else {
          alert("Error retrieving properties. Request could not be created.");
        }
      });
  },
};
</script>

<style>
/* 
* {
  border: solid red 1px
} */

.property {
  margin: 15px;
}

.property > img {
  max-width: 200px;
}

.propertyPhoto {
  width: 200px;
  height: 200px;
}
</style>
