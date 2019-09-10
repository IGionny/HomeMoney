<template>
  <div>
    <v-data-table
      :headers="headers"
      :items="categories"
      :items-per-page="15"
      class="elevation-1"
    >
      <template v-slot:item.Color="{ item }">
        <v-chip :color="item.Color" dark></v-chip>
      </template>
      <template v-slot:item.Icon="{ item }">
        <i class="fas" :class="item.Icon"></i>
      </template>
      
    </v-data-table>

  </div>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";

    export interface ICategoryRow {
        Name: string | null;
        Color: string | null;
        Icon: string | null;
    }

    @Component
    export default class CategoriesVue extends Vue {
        headers: object[] = [
            {text: 'Name', value: 'Name'},
            {text: 'Color', value: 'Color'},
            {text: 'Icon', value: 'Icon'}
        ];


        categories: ICategoryRow[] = [
            {
                Name: "Grocery",
                Color: "red",
                Icon: "fa-rocket"
            }
        ]
    }
</script>

<style scoped>

</style>