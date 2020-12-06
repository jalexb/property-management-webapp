<template>
  <v-app-bar app color="#fadadd" dark>
      <div class="d-flex align-center">
        
        <router-link :to="{name: 'home'}">
          <v-btn text>
            <v-avatar size='125'>
              <img src="@/assets/CapstoneLogo.png" alt="logo"/>
            </v-avatar>        
          </v-btn>
        </router-link>
      </div>

      <v-spacer></v-spacer>

      <router-link v-bind:to="{name: 'login'}">
        <v-btn
          outlined
          raised 
          rounded
          color="#87ceeb"
          >
          Login
        </v-btn>
      </router-link>
    </v-app-bar>
</template>

<script>
export default {
    name: "nav-bar",

    data() {
      return {}
    }

}
</script>

<style>

</style>