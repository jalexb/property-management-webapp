<template>
  <div :property="property" class="details">
    <h1>{{ property.street }}</h1>
    <p v-if="property.street2 != ''">{{ property.street2 }}</p>
    <img :src="property.photo" />
    <p>{{ property.city }}, {{ property.region }} {{ property.zip }}</p>
    <p><span class="bold-text">Rent:</span> ${{ property.price }}</p>
    <p>
      <span class="bold-text">Bedrooms:</span> {{ property.bedrooms }} |
      <span class="bold-text">Bathrooms:</span> {{ property.bathrooms }}
    </p>
    <div class="description">
      <p>{{ property.description }}</p>
    </div>
    <div v-if="userRole === 'user' && !userHasLease" class="button">
      <router-link :to="{ path: '/lease-form/' + this.property.propertyId }">
        <v-btn outlined raised rounded color="#BA3F1D">
          Apply For Lease
        </v-btn>
      </router-link>
    </div>
    <div
      v-if="
        (userRole != 'user') &
          (userRole != 'landlord') &
          (userRole != 'maintenance')
      "
      class="button"
    >
      <router-link :to="{ name: 'login' }">
        <v-btn outlined raised rounded color="#BA3F1D">
          Login to Apply
        </v-btn>
      </router-link>

      <v-btn outlined raised rounded color="#BA3F1D" @click="home">
        Back to Home
      </v-btn>
    </div>
  </div>
</template>

<script>
import propertyService from "../services/PropertyService";

export default {
  data() {
    return {
      userRole: this.$store.state.user.role,
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
        zip: "",
      },

      userHasLease: true,
    };
  },

  created() {
    const propId = this.$route.params.id;
    propertyService
      .getProperty(propId)
      .then((response) => {
        if (response.status === 200) {
          this.property = response.data;
        }
      })
      .catch((error) => {
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

    this.checkIfUserAppliedForThisProperty();
  },
  methods: {
    home() {
      this.$router.push("/");
    },

    checkIfUserAppliedForThisProperty() {
      propertyService
        .checkIfUserAppliedForProperty(
          this.$route.params.id,
          this.$store.state.user.userId
        )
        .then((response) => {
          if (response.status === 200) {
            this.userHasLease = response.data;
          }
        });
    },
  },
};
</script>

<style scoped>
a {
  text-decoration: none;
}

.details {
  margin-left: auto;
  margin-right: auto;
  background-color: white;
  max-width: 800px;
  padding-bottom: 10px;
}

div > img {
  max-width: 40%;
  display: block;
  margin-left: auto;
  margin-right: auto;
  padding-bottom: 5px;
}

h1 {
  text-align: center;
  color: #ba3f1d;
}

p {
  text-align: center;
  font-size: 1.1em;
}

.description {
  max-width: 60%;
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

img {
  max-height: 400px;
}
</style>
