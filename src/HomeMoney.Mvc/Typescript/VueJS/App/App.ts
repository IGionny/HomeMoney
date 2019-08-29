import Vue from 'vue';
import App from './App.vue';
import Vuetify from 'vuetify/lib';
import VueRouter from "vue-router"
import {Routes} from "./Routes";

Vue.use(Vuetify);
Vue.use(VueRouter);
const vuetifyOptions = {
  theme: {
    dark: true
  }
};

const router = new VueRouter({ mode: 'history', routes: Routes});

new Vue({
  el: '#App',
  render: h => h(App),
  router,
  vuetify: new Vuetify(vuetifyOptions)
});
