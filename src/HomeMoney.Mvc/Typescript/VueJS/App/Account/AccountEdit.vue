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
      <v-col cols="12" md="6">
        <v-text-field

          prefix="€"
          type="number"
          v-model.number="Item.FirstBalance"
          :counter="10"
          label="First balance"
          required
          hide-details
          min="0"
          style="text-align: right;"
        ></v-text-field>
      </v-col>
    </v-row>
    <v-row>
      <v-col cols="12" md="6">
        <v-date-picker v-model="Item.FromAt" color="green lighten-1"></v-date-picker>
      </v-col>
      <v-col cols="12" md="6">
        <v-date-picker v-model="Item.ToAt" color="green lighten-1" header-color="primary"></v-date-picker>
      </v-col>
    </v-row>
    <v-row>
      <v-col cols="12" md="6">
        <v-switch
          v-model="Item.IsArchived"
          label="Archived"
        ></v-switch>
      </v-col>
      <v-col cols="12" md="6" align="right">

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
    import {IAccount} from "../../../@types/IAccount";
    import CommonEdit from "../../SharedComponents/CommonEdit";

    @Component
    export default class AccountEditVue extends CommonEdit<IAccount> {

        entityName: string = "account";
        
        Item: IAccount = {
            Name: "",
            FirstBalance: 0,
            FromAt: null,
            ToAt: null,
            IsArchived: false,
            Owner: null
        };

        nameRules: any[] = [
            (v: any) => !!v || 'Name is required',
            (v: any) => (v && v.length > 3) || 'Name must be greater than 3 characters',
        ];

    }
</script>