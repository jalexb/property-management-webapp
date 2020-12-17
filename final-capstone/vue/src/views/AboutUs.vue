<template>
  <v-container fluid>
    <v-flex xs12 flex-direction: row justify-space-around>
      <v-card class="pa-6 ma-4" max-width="350">
        <h1>Amy Scott</h1>
        <!-- v-img with user photo -->
        <v-img
          contain
          max-height="300"
          max-width="300"
          src="https://media-exp1.licdn.com/dms/image/C4D35AQEbyGrHCGvsAQ/profile-framedphoto-shrink_200_200/0/1608219005842?e=1608307200&v=beta&t=hsG9dLDWFpl1DiANVswaJq_HKW4BWSiwuBt4C8Wnz34"
        >
        </v-img>
        <!-- v-card-text with description and contact info -->
        <v-card-actions class="justify-center">
          <v-btn href="https://www.linkedin.com/in/amy-ackermann-scott/" target="_blank"
            >LinkedIn</v-btn
          >
        </v-card-actions>
        <v-card-text class=" primary--text">
          <strong> Email: </strong> amy.ack.scott@gmail.com
        </v-card-text>
        <v-card-text class=" primary--text">
          <strong> About: </strong> Amy has a bachelors degree in Computer Science and has worked many years in the insurance business.  She has experience in with xml, SQL, Oracle and mainframe data.  As a consultant, she help many customers migrate from one admin system to xPression suite of software. She is looking to forward her career using her .NET skills and current skill.
        </v-card-text>
      </v-card>

      <v-card class="pa-6 ma-4" max-width="350"  >
        <h1>Jacob Barnett</h1>
        <!-- v-img with user photo -->
        <v-img
          contain
          max-height="300"
          max-width="300"
          src="https://files.slack.com/files-pri/T0GNFLF6D-F01H57X351Q/img_0151.jpg"
        >
        </v-img>
        <!-- v-card-text with description and contact info -->
        <v-card-actions class="justify-center">
          <v-btn href="https://www.linkedin.com/in/jacob-barnett/" target="_blank"
            >LinkedIn</v-btn
          >
        </v-card-actions>
        <v-card-text class=" primary--text">
          <strong> Email: </strong> developer.jacob.barnett@gmail.com
        </v-card-text>
        <v-card-text class=" primary--text">
          <strong> About: </strong> Jacob first discovered the front end when he was taking some web design classes in highschool as well as participating on the competitive robotics team. Ever since then he has always wanted to be a developer!
        </v-card-text>
      </v-card>

      <v-card class="pa-6 ma-4" max-width="350">
        <h1>Evan Gemmer</h1>
        <!-- v-img with user photo -->
        <v-img
          contain
          max-height="300"
          max-width="300"
          src="https://files.slack.com/files-pri/T0GNFLF6D-F01HV5Q6Y5N/linkedin_picture.jpg"
        >
        </v-img>
        <!-- v-card-text with description and contact info -->
        <v-card-actions class="justify-center">
          <v-btn href="https://www.linkedin.com/in/evan-gemmer/" target="_blank"
            >LinkedIn</v-btn
          >
        </v-card-actions>

        <v-card-text class="  primary--text">
         <strong> Email: </strong>  evangemmer@gmail.com      
        </v-card-text>
        <v-card-text class=" primary--text">
        <strong> About: </strong>   Having experience in audio visual technology and proofreading, Evan came into software development after creating a website for his band and realizing that it had sparked an interest in delving deeper into the world of programming.
        </v-card-text>
      </v-card>

      <v-card class="pa-6 ma-4" max-width="350">
        <h1>Braydon Griffith</h1>
        <!-- v-img with user photo -->
        <v-img
          src="https://media-exp1.licdn.com/dms/image/C4E03AQFWc8G6rR8Upw/profile-displayphoto-shrink_200_200/0/1600147054429?e=1613606400&v=beta&t=E1O_cmmk1QABcKWyUhjDJjubwfiS1mDQ-Jr6Vq8dwgE"
        >
        </v-img>
        <!-- v-card-text with description and contact info -->
        <v-card-actions class="justify-center">
          <v-btn href="https://www.linkedin.com/in/braydon-griffith/" target="_blank"
            >LinkedIn</v-btn
          >
        </v-card-actions>
        <v-card-text class=" primary--text">
          <strong> Email: </strong> braydon0898@gmail.com 
        </v-card-text>
        <v-card-text class=" primary--text">
          <strong> About: </strong> Having graduated from UofL with an electrical engineering degree, Braydon came into software development through his courses and peers. This grew into an opportunity for a vast career in software development.
        </v-card-text>
      </v-card>
    </v-flex>
  </v-container>
</template>

<script>
export default {
  name: "about-us",
  data() {
    return {
      braydon_src: "../assets/Jacob.JPG",
    };
  },
};
</script>
