<template>
  <div>

    <v-btn to="/accounts/edit/" color="success">Add new Account
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
    >
      <template v-slot:item.FromAt="{ item }">
        {{item.FromAt | FormatDate}}
      </template>
      <template v-slot:item.ToAt="{ item }">
        {{item.ToAt | FormatDate}}
      </template>
      <template v-slot:item.CreatedAt="{ item }">
        {{item.CreatedAt | FormatDateTime}}
      </template>
    </v-data-table>

  </div>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import {IAccount} from "../../../@types/IAccount";
    import {GetPaged} from "../../Utilities/AxiosHelpers";
    import {VuetifyDataPagination} from "../../../@types/VuetifyTypes";
    import {Watch} from "vue-property-decorator";
    import {IPagedRequest} from "../../../@types/IPagedRequest";
    import {IPagedResponse} from "../../../@types/IPagedResponse";
    import {typeIsOrHasBaseType} from "tslint/lib/language/typeUtils";

    @Component
    export default class AccountsVue extends Vue {
        headers: object[] = [
            {text: 'Name', value: 'Name'},
            {text: 'First Balance', value: 'FirstBalance'},
            {text: 'Archived', value: 'IsArchived'},
            {text: 'From', value: 'FromAt'},
            {text: 'To', value: 'ToAt'},
            {text: 'Created At', value: 'CreatedAt'}
        ];

        options: any = {
            itemsPerPage: 15,
            page: 1,
            sortBy: ["CreatedAt"],
            sortDesc: [true]
        };

        @Watch("options", {deep: true})
        onOptionsChange() {
            this.pagedRequest.Page = (this.options.page as number) - 1;
            this.pagedRequest.PageSize = this.options.itemsPerPage as number;
            if (this.options.sortBy.length > 0) {
                this.pagedRequest.Orders = [{
                    Field: this.options.sortBy[0] as string,
                    Ascending: !this.options.sortDesc[0]
                }]
            } else {
                this.pagedRequest.Orders = [];
            }
            this.FetchDataSetAsync();
        }

        accounts: IAccount[] = [];

        pagedRequest: IPagedRequest = {
            Page: 0,
            PageSize: 15,
            Filters: [],
            Orders: [{Field: "CreatedAt", Ascending: false}]
        }

        lastPagedResponse: IPagedResponse<IAccount> | null = null;

        get Items(): IAccount[] {
            if (this.lastPagedResponse === null) return [];
            return this.lastPagedResponse.Value;
        }

        get totalItems(): number {
            if (this.lastPagedResponse === null) return 0;
            return this.lastPagedResponse.Total;
        }

        mounted() {
            this.FetchDataSetAsync();
        }


        PaginationChange(pagination: VuetifyDataPagination) {
            this.pagedRequest.PageSize = pagination.itemsPerPage;
            this.pagedRequest.Page = pagination.page - 1;
        }

        async FetchDataSetAsync() {

            const result = await GetPaged<IAccount>(this.pagedRequest, "Account");
            this.lastPagedResponse = result;
        }

        SelectRow(item: IAccount) {
            this.$router.push({path: "/accounts/edit/" + item.Id});
        }

    }
</script>
