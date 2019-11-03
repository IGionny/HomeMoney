<template>
  <div>
    <v-btn to="/transaction/edit/" color="success">Add new Transaction
      <v-icon right dark>fa fa-plus</v-icon>
    </v-btn>

    <v-data-table
      :options.sync="options"
      :headers="headers"
      :items="Items"
      :server-items-length="totalItems"
      :items-per-page="15"
      class="elevation-1"
      @click:row="SelectRow"
    ></v-data-table>
  </div>
</template>

<script lang="ts">

    import Component from "vue-class-component";
    import {ITransaction} from "../../../@types/ITransaction";
    import CommonGrid from "../../SharedComponents/CommonGrid";

    @Component
    export default class TransactionsVue extends CommonGrid<ITransaction> {
        entityName: string = "transaction";
        
        headers: object[] = [
            {text: 'Date', value: 'Date'},
            {text: 'Title', value: 'Title'},
            {text: 'Expense', value: 'Expense'},
            {text: 'Income', value: 'Income'},
            {text: 'Category', value: 'Category'},
            {text: 'Tags', value: 'Tags'},
            {text: 'Created At', value: 'CreatedAt'},
        ];
    }
</script>