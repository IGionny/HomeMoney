import Vue from 'vue';
import App from './Login.vue';
import Vuetify from 'vuetify/lib';

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
