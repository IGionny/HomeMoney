<template>
  <div>
    <v-btn to="/category/edit/" color="success">Add new Category
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
      <template v-slot:item.Color="{ item }">
        <v-chip :color="item.Color"></v-chip>
      </template>
      <template v-slot:item.Icon="{ item }">
        <i class="fas" :class="item.Icon"></i>
      </template>
      
    </v-data-table>

  </div>
</template>

<script lang="ts">
    import Component from "vue-class-component";
    import CommonGrid from "../../SharedComponents/CommonGrid";
    import {ICategory} from "../../../@types/ICategory";

    @Component
    export default class CategoriesVue extends CommonGrid<ICategory> {

        entityName: string = "category";
        
        headers: object[] = [
            {text: 'Name', value: 'Name'},
            {text: 'Color', value: 'Color'},
            {text: 'Icon', value: 'Icon'}
        ];
        
    }
</script>
