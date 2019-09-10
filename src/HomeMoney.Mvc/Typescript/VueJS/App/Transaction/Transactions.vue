<template>
  <div>
    <v-data-table
      :headers="headers"
      :items="transactions"
      :items-per-page="15"
      class="elevation-1"
    ></v-data-table>

  </div>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";

    export interface ITransactionRow {
        Date: string;
        Title: string;
        Expense: number;
        Income: number;
        Category: string;
        Tags: string;
        CreatedAt: string;
    }

    @Component
    export default class TransactionsVue extends Vue {
        headers: object[] = [
            {text: 'Date', value: 'Date'},
            {text: 'Title', value: 'Title'},
            {text: 'Expense', value: 'Expense'},
            {text: 'Income', value: 'Income'},
            {text: 'Category', value: 'Category'},
            {text: 'Tags', value: 'Tags'},
            {text: 'Created At', value: 'CreatedAt'},
        ];


        transactions: ITransactionRow[] = [
            {
                Date: "",
                Title: "",
                Expense: 100,
                Income: 0,
                Category: "Grocery",
                Tags: "x",
                CreatedAt: ""
            }
        ]
    }
</script>

<style scoped>

</style>