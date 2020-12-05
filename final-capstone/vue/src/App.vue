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
  </v-app>
</template>

<script>
import HelloWorld from "./components/HelloWorld";

export default {
  name: "App",

  components: {
    HelloWorld,
  },

  data: () => ({
    //
  }),
};
</script>
