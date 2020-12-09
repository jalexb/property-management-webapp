<template>
  <v-app>
    <nav-bar />
    <v-main class="container">
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

<style lang="scss" scoped>

.container{
  background-image: url('~@/assets/Background.jpg');
}

</style>