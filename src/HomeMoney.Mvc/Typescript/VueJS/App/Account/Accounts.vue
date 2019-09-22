<template>
  <div>

    <v-btn to="/accounts/edit/" color="success">Add new Account <v-icon right dark>fa fa-plus</v-icon></v-btn>
    
    <v-data-table
      :headers="headers"
      :items="accounts"
      :items-per-page="15"
      class="elevation-1"
    ></v-data-table>

  </div>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";

    export interface IAccountRow {
        Name: string | null;
        IsArchived: boolean;
        FirstBalance: number;
        FromAt: string | null;
        ToAt: string | null;
    }

    @Component
    export default class AccountsVue extends Vue {
        headers: object[] = [
            {text: 'Name', value: 'Name'},
            {text: 'First Balance', value: 'FirstBalance'},
            {text: 'Archived', value: 'IsArchived'},
            {text: 'From', value: 'FromAt'},
            {text: 'To', value: 'ToAt'}
        ];


        accounts: IAccountRow[] = [
            {
                Name: "Fineco",
                IsArchived: false,
                FirstBalance: 0,
                FromAt: null,
                ToAt: null
            }
        ]
    }
</script>

<style scoped>

</style>