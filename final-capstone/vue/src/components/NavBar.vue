<template>
  <v-app-bar app color="#D9CFC1" dark>
      <div class="d-flex align-center">
        
        <router-link class="routerlink" :to="{name: 'home'}">
          <v-btn text>
            <v-avatar size='125'>
              <img src="@/assets/CapstoneLogo.png" alt="logo"/>
            </v-avatar>        
          </v-btn>
        </router-link>
      </div>

      <v-spacer></v-spacer>
      <div v-if="this.$store.state.token === ''">
        <router-link v-bind:to="{name: 'login'}">
          <v-btn
            outlined
            raised 
            rounded
            color="#BA3F1D"
            >
            Login
          </v-btn>
        </router-link>
      </div>

      <div v-else>
        <router-link v-bind:to="{name: 'logout'}">
          <v-btn
            outlined
            raised 
            rounded
            color="#87ceeb"
            >
            Logout
          </v-btn>
        </router-link>
      </div>

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