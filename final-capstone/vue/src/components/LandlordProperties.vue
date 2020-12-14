<template>
  <div>
      <p>Hello</p>
      <v-card>
        <v-card-title>Revenue Statistics</v-card-title>
        <v-card>
            <v-card-title @click="CurrentIncomeShowing = !CurrentIncomeShowing">Current Income</v-card-title>
            <div v-show="CurrentIncomeShowing">
                <p><span>Occupied Properties</span><span>{{properties.occupiedProperties.length}}</span></p>
                <p><span>Current Monthly Rent Amount</span><span>${{properties.occupiedRentSum}}</span></p>
            </div>
        </v-card>
        <v-card>
            <v-card-title @click="PotentialIncomeShowing = !PotentialIncomeShowing">Potential Income</v-card-title>
            <div v-show="PotentialIncomeShowing">
                <p><span>Vacant Properties</span><span>{{properties.vacantProperties.length}}</span></p>
                <p><span>Potential Additional Monthly Rent Amount</span><span>${{properties.vacantRentSum}}</span></p>
            </div>
        </v-card>
        <v-card>
            <v-card-title @click="OccupiedShowing = !OccupiedShowing">Occupied Properties</v-card-title>
            <v-card
            v-show="OccupiedShowing"
            v-for="occupiedProperty in properties.occupiedProperties"
            :key="occupiedProperty.property.addressId"
            >
                <v-card-title>
                    {{ occupiedProperty.renter.info.fullName }} <br/>
                    {{ occupiedProperty.property.street}}
                </v-card-title>
                <v-img max-height="200" max-width="200" :src="occupiedProperty.property.photo"> </v-img>
                <v-card-text>
                    <p>${{ occupiedProperty.property.price}}</p>
                </v-card-text>
            </v-card>
        </v-card>
        <v-card>
            <v-card-title @click="VacantShowing = !VacantShowing">Vacant Properties</v-card-title>
            <v-slide-group
            v-show="VacantShowing">
                    <v-card
                    v-for="vacantProperty in properties.vacantProperties"
                    :key="vacantProperty.addressId"
                    max-width="250"
                    v-show="VacantShowing"
                    >
                        <v-card-title>
                            {{ vacantProperty.address }}
                        </v-card-title>
                        <v-img max-height="200" max-width="200" :src="vacantProperty.photo"> </v-img>
                        <v-card-text>
                            <p>${{ vacantProperty.price}}</p>
                        </v-card-text>
                    </v-card>
            </v-slide-group>
        </v-card>
      </v-card>
  </div>
</template>

<script>
import LandlordService from '@/services/LandlordService'

export default {
    name: 'landlord-properties',
    data() {
        return {
            properties: [],
            VacantShowing: false,
            OccupiedShowing: false,
            CurrentIncomeShowing: false,
            PotentialIncomeShowing: false,
        }
    },
    created () {
        this.getAllProperties();
    },
    methods: {
        getAllProperties() {
            LandlordService.getLandlordProperties(this.$store.state.user.userId).then(response => {
                console.log(response);
                this.properties = response.data;
            })
        }
    }
}
</script>

<style scoped>
p {
    margin: 5px 0 5px 0;
    display: flex;
    justify-content: space-between;
}
</style>