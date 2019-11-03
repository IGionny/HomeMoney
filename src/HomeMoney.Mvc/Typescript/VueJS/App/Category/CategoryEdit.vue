<template>
  <v-form ref="form" v-model="valid" lazy-validation>
    <v-row>
      <v-col cols="12" md="6">
        <v-text-field
          v-model="Item.Name"
          :counter="255"
          :rules="nameRules"
          label="Name"
          required
        ></v-text-field>
      </v-col>
    </v-row>
    <v-row>
      <v-col cols="6" md="6">
        <v-text-field
          v-model="Item.Color"
          :counter="255"
          label="Color"
          required
        ></v-text-field>
      </v-col>
      <v-col cols="6" md="6">
        <v-text-field
          v-model="Item.Icon"
          :counter="255"
          label="Icon"
          required
        ></v-text-field>
      </v-col>
    </v-row>
    <v-row>
      <v-col cols="12" md="6">
        <v-btn class="secondary" @click="Cancel">Cancel</v-btn>
      </v-col>
      <v-col cols="12" md="6" align="right">
        <v-btn @click="Save">Save</v-btn>
      </v-col>
    </v-row>
  </v-form>
</template>

<script lang="ts">
    import Component from "vue-class-component";
    import CommonEdit from "../../SharedComponents/CommonEdit";
    import {ICategory} from "../../../@types/ICategory";

    @Component
    export default class CategoryEditVue extends CommonEdit<ICategory> {

        entityName: string = "category";
        
        Item: ICategory = {
            Name: "",
            Color: null,
            Icon: null,
            Owner: null
        };

        nameRules: any[] = [
            (v: any) => !!v || 'Name is required',
            (v: any) => (v && v.length > 3) || 'Name must be greater than 3 characters',
        ];

    }
</script>