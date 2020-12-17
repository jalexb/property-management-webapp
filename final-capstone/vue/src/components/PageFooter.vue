<template>
  <v-footer padless>
    <v-row justify="center" no-gutters>
      <v-col class="primary lighten-3 py-4 text-center white--text" cols="12">
        {{ new Date().getFullYear() }} —
        <strong>
          <router-link
            class="routerlink white--text"
            :to="{ name: 'about-us' }"
          >
            About Us |
          </router-link>
          <router-link class="routerlink white--text" :to="{ name: 'contact-us' }">
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
