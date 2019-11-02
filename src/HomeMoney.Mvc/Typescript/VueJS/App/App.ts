import Vue from 'vue';
import App from './App.vue';
import VueRouter from "vue-router"
import {Routes} from "./Routes";
import vuetify from '../../plugins/Vuetify';
import {FormatDateFilter, FormatDateTimeFilter} from "../Utilities/DateFilter"; // path to vuetify export
Vue.use(VueRouter);

const router = new VueRouter({ mode: 'history', routes: Routes});

Vue.filter("FormatDate", (value: any) => FormatDateFilter(value));
Vue.filter("FormatDateTime", (value: any) => FormatDateTimeFilter(value));


new Vue({
  el: '#App',
  render: h => h(App),
  router,
  vuetify
});
