<template>
  <v-form
    ref="form"
    v-model="valid"
    lazy-validation
  >
    <v-text-field
      v-model="Item.Title"
      :counter="10"
      :rules="titleRules"
      label="Title"
      required
    ></v-text-field>

    <v-menu
      ref="menu1"
      v-model="menu1"
      :close-on-content-click="false"
      transition="scale-transition"
      offset-y
      full-width
      max-width="290px"
      min-width="290px"
    >
      <template v-slot:activator="{ on }">
        <v-text-field
          v-model="Item.ExecutedAt"
          label="Date"
          hint="MM/DD/YYYY format"
          persistent-hint
          _prepend-icon="event"
          @blur="date = parseDate(Item.ExecutedAt)"
          v-on="on"
        ></v-text-field>
      </template>
      <v-date-picker v-model="Item.ExecutedAt" no-title @input="menu1 = false"></v-date-picker>
    </v-menu>
    <p>Date in ISO format: <strong>{{ Item.ExecutedAt }}</strong></p>
    
    <v-select
      v-model="Item.AccountFrom"
      :items="Accounts"
      :rules="[v => !!v || 'Item is required']"
      label="From account"
      required
    ></v-select>

  </v-form>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import {IExpenseModel} from "../../../../@types/IExpenseModel";
    import {IEntityReference} from "../../../../@types/IEntityReference";

    @Component
    export default class AddExpenseVue extends Vue {
        valid: boolean = true;

        formatDate (date:string | null | undefined) {
            if (!date) return null

            const [year, month, day] = date.split('-')
            return `${month}/${day}/${year}`
        };
        
        parseDate (date:string | null | undefined) {
            if (!date) return null

            const [month, day, year] = date.split('/')
            return `${year}-${month.padStart(2, '0')}-${day.padStart(2, '0')}`
        }
        
        Item: IExpenseModel = {
            Title: "",
            ExecutedAt:  new Date().toISOString().substr(0, 10),
            ValueDate: null,
            Amount: 0,
            AccountFrom: null,
            Category: null,
            Notes: null,
            Tags: []
        };

        Accounts : IEntityReference[] = [];
        Categories : IEntityReference[] = [];
        
        titleRules: any[] = [
            (v: any) => !!v || 'Title is required',
            (v: any) => (v && v.length <= 10) || 'Title must be less than 10 characters',
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