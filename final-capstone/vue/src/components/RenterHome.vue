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
            <router-link :to="{name: 'renter-transactions', params: {id: this.$store.state.user.userId}}">
              <v-card
              class="ma-6"
              max-width="350"
              hover>
              <v-img max-height="200" src="https://www.investopedia.com/thmb/M-we6mdLUgezNP-XsAoli0Lf070=/2121x1414/filters:fill(auto,1)/GettyImages-974812152-705d0a6d517947fb8946a3c887d528be.jpg"> </v-img>
              <v-card-title> 
                Pay Rent
              </v-card-title>
            </v-card>
            </router-link>
            <router-link :to="{name: 'maintenance-ticket', params: {id: this.$store.state.user.userId}}">
              <v-card 
                class="ma-6"
                max-width="350"
                hover>
                <v-img max-height="200" src="https://media.noria.com/sites/Uploads/2019/1/29/6043ff94-3285-4abc-b169-6ab2793a0c24_Strategies%20to%20Reduce%20Maintenance%20Costs_extra_large.jpeg"/>          
                <v-card-title> 
                  Submit a Maintenance Request
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