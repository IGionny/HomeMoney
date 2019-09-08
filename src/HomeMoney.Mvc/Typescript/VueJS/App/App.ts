import Vue from 'vue';
import App from './App.vue';
import VueRouter from "vue-router"
import {Routes} from "./Routes";
import vuetify from '../../plugins/Vuetify'; // path to vuetify export
Vue.use(VueRouter);

const router = new VueRouter({ mode: 'history', routes: Routes});

new Vue({
  el: '#App',
  render: h => h(App),
  router,
  vuetify
});
