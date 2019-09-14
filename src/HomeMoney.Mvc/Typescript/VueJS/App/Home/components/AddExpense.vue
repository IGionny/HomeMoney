<template>
  <v-form

    ref="form"
    v-model="valid"
    lazy-validation
  >
    
    <v-row>
      <v-col cols="12" md="3">
        <v-menu
          ref="menu1"

          :close-on-content-click="false"
          transition="scale-transition"
          offset-y

          max-width="290px"
          min-width="290px"
        >
          <template v-slot:activator="{ on }">
            <!--  __@blur="date = parseDate(Item.ExecutedAt)"-->
            <v-text-field
              v-model="Item.ExecutedAt"
              label="Date"

              hint="YYYY/MM/DD"
              persistent-hint
              _prepend-icon="event"

              v-on="on"
            ></v-text-field>
          </template>
          <v-date-picker v-model="Item.ExecutedAt" no-title @input="menu1 = false"></v-date-picker>
        </v-menu>
      </v-col>
      <v-col cols="12" md="9">
        <v-text-field
          v-model="Item.Title"
          :counter="255"
          :rules="titleRules"
          label="Title"
          required
        ></v-text-field>

      </v-col>
    </v-row>


    <v-row>
      <v-col cols="12" md="3">
        <v-text-field

          prefix="€"
          type="number"
          v-model="Item.Amount"
          :counter="10"
          :rules="amountRules"
          label="Amount"
          required
          hide-details
          min="0"
          style="text-align: right;"
        ></v-text-field>
      </v-col>
      <v-col cols="12" md="9">
        <v-select
          v-model="Item.AccountFrom"
          :items="Accounts"
          :rules="[v => !!v || 'Item is required']"
          label="From account"
          required
        ></v-select>
      </v-col>

    </v-row>

    <v-combobox
      v-model="item.Tags"
      :items="Tags"
      :search-input.sync="null"
      :hide-selected="false"
      label="Add some tags"
      :multiple="multiple"
      persistent-hint
      :small-chips="true"
      :clearable="true"
    >
      <template v-slot:no-data>
      <v-list-item>
        <v-list-item-content>
          <v-list-item-title>
            No results matching "<strong>{{ search }}</strong>". Press <kbd>enter</kbd> to create a new one
          </v-list-item-title>
        </v-list-item-content>
      </v-list-item>
    </template>
    </v-combobox>

    <v-row>
      <v-col cols="12" md="8">
        <v-text-field
          v-model="Item.Notes"
          label="Notes"
          required
        ></v-text-field>
      </v-col>
      <v-col cols="12" md="4">
        <v-btn color="primary" dark>Add Expense</v-btn>
      </v-col>
    </v-row>

  </v-form>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import {IExpenseModel} from "../../../../@types/IExpenseModel";
    import {IEntityReference} from "../../../../@types/IEntityReference";
    import * as moment from 'moment';

    @Component
    export default class AddExpenseVue extends Vue {
        valid: boolean = true;

        formatDate(date: string | null | undefined) {
            if (!date) return null

            const [year, month, day] = date.split('-')
            return `${month}/${day}/${year}`
        };

        parseDate(date: string | null | undefined) {
            if (!date) return null
            return moment(date).format("yyyy-MM-DD");
            /*const [month, day, year] = date.split('/')
            /*return `${year}-${month.padStart(2, '0')}-${day.padStart(2, '0')}`*/
        }
        
        Tags: string[] = [];

        Item: IExpenseModel = {
            Title: "",
            ExecutedAt: new Date().toISOString().substr(0, 10),
            ValueDate: null,
            Amount: 0,
            AccountFrom: null,
            Category: null,
            Notes: null,
            Tags: []
        };

        Accounts: IEntityReference[] = [{Id: "xx", Name: "Fineco"}];
        Categories: IEntityReference[] = [];

        titleRules: any[] = [
            (v: any) => !!v || 'Title is required',
            (v: any) => (v && v.length <= 10) || 'Title must be less than 10 characters',
        ];

        amountRules: any[] = [
            (v: any) => !!v || 'Amount is required',
            (v: any) => (v && v >= 0) || 'Amount must be greater or equal than 0'
        ];

        validate() {
            //@ts-ignore
            if (this.$refs.form.validate()) {

            }
        }

        reset() {
            //@ts-ignore
            this.$refs.form.reset()
        }

        resetValidation() {
            //@ts-ignore
            this.$refs.form.resetValidation()
        }

    }
</script>

<style scoped>

</style>