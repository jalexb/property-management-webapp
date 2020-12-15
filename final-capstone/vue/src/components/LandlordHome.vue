<template>
    <v-container
    fluid
    >
            <v-flex 
            xs12 
            wrap
            flex-direction: row
            justify-space-around
            >
            <router-link :to="{name: 'landlord-properties'}">
              <v-card
              class="ma-6"
              max-width="400"
              hover>
                <v-img src="https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/suburban-house-royalty-free-image-1584972559.jpg"> </v-img>
                <v-card-title> 
                  View and Add Properties
                </v-card-title>
              </v-card>
            </router-link>
            <v-card 
              class="ma-6"
              max-width="400"
              hover>
              <!--<router-link :to="{ name: 'property-details', params: {id: property.propertyId} }">-->
              <v-img src="https://static6.depositphotos.com/1155356/651/i/600/depositphotos_6516733-stock-photo-air-conditioning-repair.jpg"> </v-img>
              <v-card-title> 
                Assign Maintenance Tickets
              </v-card-title>
              <!--</router-link>-->
            </v-card>
            <v-card
              class="ma-6"
              max-width="400"
              hover>
              <!--<router-link :to="{ name: 'property-details', params: {id: property.propertyId} }">-->
              <v-img src="https://media.istockphoto.com/photos/pen-being-used-to-write-a-check-picture-id173602091?k=6&m=173602091&s=612x612&w=0&h=jGHuet6blVWXjTgaAwpRnHPqBvMQMv8w3kWUfvR01jo="> </v-img>
              <v-card-title> 
                View All Rents
              </v-card-title>
              <!--</router-link>-->
            </v-card>
            <router-link :to="{ name: 'pending-leases'}">
            <v-card 
              class="ma-6"
              max-width="400"
              hover>
              <v-img src="https://st.depositphotos.com/1194063/4024/i/600/depositphotos_40243649-stock-photo-lease-agreement.jpg"> </v-img>
              <v-card-title> 
                View Pending Applications
              </v-card-title>
            </v-card>
            </router-link>
            </v-flex>
            
    </v-container>
</template>

<script>
export default {

}
</script>

<style>

</style>