<template>
  <v-app>
    <v-parallax src="@/assets/Background.jpg"> </v-parallax>
    <nav-bar />
    <v-main>
      <router-view />
    </v-main>
    <page-footer />
  </v-app>
</template>

<script>
import PageFooter from "./components/PageFooter.vue";
import NavBar from "./components/NavBar.vue";

export default {
  name: "App",

  components: {
    NavBar,
    PageFooter,
  },

  data: () => ({
    //
  }),
};
</script>
