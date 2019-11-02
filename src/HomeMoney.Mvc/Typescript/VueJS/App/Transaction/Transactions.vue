<template>
  <div>
    <v-data-table
      :headers="headers"
      :items="transactions"
      @pagination="PaginationChange"
      :items-per-page="15"
      class="elevation-1"
    ></v-data-table>
  </div>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import {IPagedRequest} from "../../../@types/IPagedRequest";
    import {GetPaged} from "../../Utilities/AxiosHelpers";
    import {ITransaction, ITransactionRow} from "../../../@types/ITransaction";
    import {Watch} from "vue-property-decorator";
    import {VuetifyDataPagination} from "../../../@types/VuetifyTypes";



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

        transactions: ITransactionRow[] = [];

        pagedRequest: IPagedRequest = {
            Page: 0,
            PageSize: 15,
            Filters: [],
            Orders: [{Field: "CreatedAt", Ascending: false}]
        }

        mounted() {
            this.FetchDataSetAsync();
        }

        @Watch("pagedRequest", {deep: true})
        onPagedChange() {
            this.FetchDataSetAsync();
        }

        PaginationChange(pagination: VuetifyDataPagination) {
            this.pagedRequest.PageSize = pagination.itemsPerPage;
            this.pagedRequest.Page = pagination.page - 1;
        }

        async FetchDataSetAsync() {
            const result = await GetPaged<ITransaction>(this.pagedRequest, "Transaction");
            this.transactions = result.Value.map(this.ConvertEntityToRow);
        }

        ConvertEntityToRow(item: ITransaction): ITransactionRow {
            let categoryName = item.Category === null ? "" : item.Category.Name;
            return {
                Title: item.Title,
                Date: item.ExecutedAt,
                Expense: item.AmountExpense,
                Income: item.AmountIncome,
                Category: categoryName,
                Tags: item.Tags || "",
                CreatedAt: item.CreatedAt
            } as ITransactionRow;
        }

    }
</script>