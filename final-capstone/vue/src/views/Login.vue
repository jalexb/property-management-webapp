<template>
  <div id="login" class="text-center">
    <v-container fluid fill-height>
      <v-layout align-center justify-center>
        <v-flex xs12 sm8 lg4>
          <v-card class="elevation-12">
            <v-toolbar color="primary lighten-3">
              <v-toolbar-title>Login form</v-toolbar-title>
            </v-toolbar>
            <v-card-text>
              <v-form class="form-signin" @submit.prevent="login">
                <div
                  class="alert alert-danger"
                  role="alert"
                  v-if="invalidCredentials"
                >
                  Invalid username and password!
                </div>
                <div
                  class="alert alert-success"
                  role="alert"
                  v-if="this.$route.query.registration"
                >
                  Thank you for registering, please sign in.
                </div>
                <v-text-field
                  prepend-icon="person"
                  name="login"
                  label="Login"
                  type="text"
                  v-model="user.username"
                  required
                ></v-text-field>
                <v-text-field
                  id="password"
                  prepend-icon="lock"
                  name="password"
                  label="Password"
                  type="password"
                  v-model="user.password"
                  required
                ></v-text-field>
                <v-btn type="submit" color="secondary">Login</v-btn>
                <v-spacer></v-spacer>
                <router-link :to="{ name: 'register' }"
                  >Need an account?</router-link
                >
              </v-form>
            </v-card-text>
          </v-card>
        </v-flex>
      </v-layout>
    </v-container>
  </div>
</template>

<script>
import authService from "../services/AuthService";

export default {
  name: "login",
  components: {},
  data() {
    return {
      user: {
        username: "",
        password: "",
      },
      invalidCredentials: false,
    };
  },
  methods: {
    login() {
      authService
        .login(this.user)
        .then((response) => {
          if (response.status == 200) {
            this.$store.commit("SET_AUTH_TOKEN", response.data.token);
            this.$store.commit("SET_USER", response.data.user);
            this.$router.push("/");
          }
        })
        .catch((error) => {
          const response = error.response;

          if (response.status === 401) {
            this.invalidCredentials = true;
          }
        });
    },
  },
  dialog: true,
  tab: 0,
  tabs: [
    { name: "Login", icon: "mdi-account" },
    { name: "Register", icon: "mdi-account-outline" },
  ],
};
</script>

<style scoped></style>
