<template>
  <v-footer padless>
    <v-row justify="center" no-gutters>
      <v-col class="brown lighten-3 py-4 text-center white--text" cols="12">
        {{ new Date().getFullYear() }} —
        <strong
          >Vuetify |
          <router-link class="routerlink" :to="{ name: 'home' }">
            Help
          </router-link>
          |
          <router-link class="routerlink" :to="{ name: 'home' }">
            Contact
          </router-link>
        </strong>
      </v-col>
    </v-row>
  </v-footer>
</template>

<script>
export default {
  name: "page-footer",
};
</script>

<style>
.routerlink {
  text-decoration: none;
  color: red;
}
</style>
