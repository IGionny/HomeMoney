import Vue from 'vue';
import App from './App.vue';
import Vuetify from 'vuetify/lib';
//import vuetify from '@/plugins/vuetify'; // path to vuetify export

// @ts-ignore
//Vuetify.theme.dark = true;
Vue.use(Vuetify);
const vuetifyOptions = {
  theme: {
    dark: true
  }
};

new Vue({
  el: '#App',
  render: h => h(App),
  vuetify: new Vuetify(vuetifyOptions)
});
