<template>
  <div>
      <h1>Manage Properties</h1>
      <v-card
      max-width="1170"
      class="ma-auto"
      >
        <v-card-title>Revenue Statistics</v-card-title>
        <v-card class="currentIncome">
            <v-card-title  @click="CurrentIncomeShowing = !CurrentIncomeShowing" justify="space-between"><v-col>Current Income</v-col><v-col>Current Monthly Rent Amount: ${{properties.occupiedRentSum}}</v-col></v-card-title>
            <div v-show="CurrentIncomeShowing">
                <p><span>Occupied Properties</span><span>{{properties.occupiedProperties.length}}</span></p>
                <p><span>Current Monthly Rent Amount</span><span>${{properties.occupiedRentSum}}</span></p>
                <renter-details :property="property" v-for="property in properties.occupiedProperties" :key="property.renter.info.user_Id"></renter-details>

            </div>
        </v-card>
        <v-card class="potentialIncome">
            <v-card-title @click="PotentialIncomeShowing = !PotentialIncomeShowing">Potential Income</v-card-title>
            <div v-show="PotentialIncomeShowing">
                <p><span>Vacant Properties</span><span>{{properties.vacantProperties.length}}</span></p>
                <p><span>Potential Additional Monthly Rent Amount</span><span>${{properties.vacantRentSum}}</span></p>
            </div>
        </v-card>
        <v-card class="occupiedPRoperties">
            <v-card-title @click="OccupiedShowing = !OccupiedShowing">Occupied Properties</v-card-title>
            <v-slide-group
            show-arrows="always"
            v-show="OccupiedShowing">
            <v-card
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
            </v-slide-group>

        </v-card>
        <v-card class="vacantProperties">
            <v-card-title @click="VacantShowing = !VacantShowing">Vacant Properties</v-card-title>
            <v-slide-group
                       show-arrows="always"
                       v-show="VacantShowing"
                       center-active="true">
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
            <v-card class="addNewProperty">
            <v-card-title @click="addingNewProperty = !addingNewProperty">Add New Property</v-card-title>
                <v-card
                v-show="addingNewProperty"
                >
                    <v-form v-show="addingNewProperty">
                    <label for="property-type">Property Type: </label>
                    <input type="text" name="property-type" id="property-type" v-model="newProperty.property_Type" style="display: block" />
                    <label for="street">Street: </label>
                    <input type="text" name="street" id="street" v-model="newProperty.street" style="display: block" />
                    <label for="street2">Street2: </label>
                    <input type="text" name="street2" id="street2" v-model="newProperty.street2" style="display: block" />
                    <label for="city">City: </label>
                    <input type="text" name="city" id="city" v-model="newProperty.city" style="display: block" />
                    <label for="region">Region: </label>
                    <input type="text" name="region" id="region" v-model="newProperty.region" style="display: block" />
                    <label for="zip">Zip: </label>
                    <input type="number" name="zip" id="zip" v-model="newProperty.zip" style="display: block" />
                    <label for="bedrooms">Bedrooms: </label>
                    <input type="number" name="bedrooms" id="bedrooms" v-model="newProperty.bedrooms" style="display: block" />
                    <label for="bathrooms">Bathrooms: </label>
                    <input type="number" name="bathrooms" id="bathrooms" v-model="newProperty.bathrooms" style="display: block" />
                    <label for="photo">Link to Photo: </label>
                    <input type="text" name="photo" id="photo" v-model="newProperty.photo" style="display: block" />
                    <label for="description">Description: </label>
                    <input type="text" name="description" id="description" v-model="newProperty.description" style="display: block" />
                    <label for="price">Price: </label>
                    <input type="number" name="price" id="price" v-model="newProperty.price" style="display: block" />

                    <v-btn
                    outlined
                    raised 
                    rounded
                    color="success"
                    v-on:click="addNewProperty(newProperty)"
                    class="mx-2 my-2"
                    >
                    Submit
                    </v-btn>
                    <v-btn
                    outlined
                    raised 
                    rounded
                    color="warning"
                    v-on:click="addingNewProperty=false"
                    class="mx-2 my-2"
                    >
                    Cancel
                    </v-btn>
                </v-form>
                </v-card>
        </v-card>
        </v-card>
  </div>
</template>

<script>
import LandlordService from '@/services/LandlordService'
import RenterDetails from './RenterDetails.vue'
  
export default {
    name: 'landlord-properties',
    components: { RenterDetails },
    data() {
        return {
            properties: [],
            VacantShowing: false,
            OccupiedShowing: false,
            CurrentIncomeShowing: false,
            PotentialIncomeShowing: false,
            addingNewProperty: false,
            newProperty: {
                    userId: this.$store.state.user.userId,
                    property_Type: '',
                    street: '',
                    street2: 'N/A',
                    city: '',
                    region: '',
                    zip: 0,
                    bedrooms: 0,
                    bathrooms: 0,
                    photo: '',
                    description: '',
                    price: 0
            }
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
        },
        addNewProperty(newProperty) {
            LandlordService.createNewProperty(newProperty).then(response => {
                if(response.status===204) {
                    alert("New property has successfully been added!");
                    this.resetNewProperty();
                    this.getAllProperties();
                }
            }).catch(error => {
                if (error.response) {
                alert(
                "Error creating property. Response from server was " +
                    error.response.statusText +
                    "."
                );
                } else if (error.request) {
                    alert("Error creating property. Could not connect to server.");
                } else {
                    alert("Error creating property. Request could not be created.");
                }
            });
        },
        resetNewProperty() {
            this.newProperty = {
                address: {
                    userId: this.$store.state.user.userId,
                    property_Type: '',
                    street: '',
                    street2: '',
                    city: '',
                    region: '',
                    zip: 0
                },
                property: {
                    userId: this.$store.state.user.userId,
                    bedrooms: 0,
                    bathrooms: 0,
                    photo: '',
                    description: '',
                    price: 0
                }
            }
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

input {
    margin: 5px 0 10px 20px;
}

label {
    margin: 5px 0 0 20px;
}

div {
    padding: 10px;
}

</style>