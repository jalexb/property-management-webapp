import Vue from 'vue'
import Vuetify from 'vuetify/lib'
import VuePhoneNumberInput from 'vue-phone-number-input';
import 'vue-phone-number-input/dist/vue-phone-number-input.css';
 
Vue.component('vue-phone-number-input', VuePhoneNumberInput);

Vue.use(Vuetify)

const vuetify = new Vuetify({
  theme: {
    themes: {
      light: {
        primary: '#26495c',
        secondary: '#c4a35a',
        accent: 'c66b3d',
        error: '',
        info: '#26495c',
        success: '#1B5E20',
        warning: '#FFB300'
      },
    },
  },
})

export default vuetify