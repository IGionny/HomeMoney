import home from "./Home/Index.vue";
import {VueConstructor} from "vue/types/vue";

import Accounts from "./Account/Accounts.vue";
import AccountEdit from "./Account/AccountEdit.vue";
//-----
import Categories from "./Category/Categories.vue";
import CategoryEdit from "./Category/CategoryEdit.vue";
//-----
import Transactions from "./Transaction/Transactions.vue";

export interface IRoute {
  path: string;
  component: VueConstructor
  props?: boolean
}


export const Routes: IRoute[] = [{
  path: '', component: home,
}, {
  path: '/transaction', component: Transactions,
}, {
  path: '/account', component: Accounts
}, {
  path: '/account/edit/:Id?', component: AccountEdit, props: true
}, {
  path: '/category', component: Categories
},
  {
    path: '/category/edit/:Id?', component: CategoryEdit, props: true
  }
];
