import home from "./Home/Index.vue";
import {VueConstructor} from "vue/types/vue";
import Transactions from "./Transaction/Transactions.vue";
import Accounts from "./Account/Accounts.vue";
import Categories from "./Category/Categories.vue";
import EditAccount from "./Account/EditAccount.vue";

export interface IRoute {
  path: string;
  component: VueConstructor
  props?: boolean
}


export const Routes: IRoute[] = [{
  path: '', component: home,
}, {
  path: '/transactions', component: Transactions,
},  {
  path: '/accounts', component: Accounts
}, {
  path: '/accounts/edit/:Id?', component: EditAccount, props: true
},{
  path: '/categories', component: Categories
}
];
