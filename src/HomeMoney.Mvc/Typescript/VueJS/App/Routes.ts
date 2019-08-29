import home from "./Home/Index.vue";
import {VueConstructor} from "vue/types/vue";
import Transactions from "./Transaction/Transactions.vue";

export interface IRoute {
  path: string;
  component: VueConstructor
}


export const Routes: IRoute[] = [{
  path: '', component: home,
}, {
  path: '/transactions', component: Transactions
}
];
