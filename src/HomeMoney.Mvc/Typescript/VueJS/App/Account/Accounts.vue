<template>
  <div>

    <v-btn to="/account/edit/" color="success">Add new Account
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
      <template v-slot:item.IsArchived="{ item }">
        <i class="fas fa-archive" v-if="item.IsArchived"></i>
      </template>
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
    import Component from "vue-class-component";
    import {IAccount} from "../../../@types/IAccount";
    import CommonGrid from "../../SharedComponents/CommonGrid";

    @Component
    export default class AccountsVue extends CommonGrid<IAccount> {

        entityName: string = "account";
        
        headers: object[] = [
            {text: 'Name', value: 'Name'},
            {text: 'First Balance', value: 'FirstBalance'},
            {text: 'Archived', value: 'IsArchived'},
            {text: 'From', value: 'FromAt'},
            {text: 'To', value: 'ToAt'},
            {text: 'Created At', value: 'CreatedAt'}
        ];
    }
</script>
