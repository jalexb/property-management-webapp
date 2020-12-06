<template>
  <v-app>
    <v-app-bar app color="#fadadd" dark>
      <div class="d-flex align-center">

        <v-btn href="/" 
        text
        
        >
          <v-avatar size='125'>
            <img src="..\src\assets\CapstoneLogo.png" alt="logo"/>
            </v-avatar>        
        </v-btn>
      </div>

      <v-spacer></v-spacer>

      <v-btn
        href="/login"
        target="_blank"
        outlined
        raised 
        rounded
        color="#87ceeb"
        
      >
        Login
      </v-btn>
    </v-app-bar>

    <v-main>
      <Home />
    </v-main>

    <v-footer
    color="primary lighten-1"
    padless
  >
    <v-row
      justify="center"
      no-gutters
    >
      <v-btn
        v-for="link in links"
        :key="link"
        color="white"
        text
        rounded
        class="my-2"
      >
        {{ link }}
      </v-btn>
      <v-col
        class="primary lighten-2 py-4 text-center white--text"
        cols="12"
      >
        {{ new Date().getFullYear() }} — <strong>Vuetify</strong>
      </v-col>
    </v-row>
  </v-footer>

  </v-app>
</template>

<script>

export default {
  name: "App",

  components: {
    
  },

  data: () => ({
    //
  }),
};
</script>
