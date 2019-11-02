<template>
  <v-form ref="form" v-model="valid" lazy-validation>
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
              label=""

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
          v-model="Item.AccountFromId"
          :items="Accounts"
          item-text="Name"
          item-value="Id"
          :rules="[v => !!v || 'Account is required']"
          :label="AccountLabel"
          required
        ></v-select>
      </v-col>

    </v-row>

    <v-select
      v-model="Item.CategoryId"
      :items="Categories"
      item-text="Name"
      item-value="Id"
      :rules="[v => !!v || 'Category is required']"
      label="Category"
      required
    ></v-select>

    <Tags v-model="Item.Tags"></Tags>

    <v-row>
      <v-col cols="12" md="8">
        <v-text-field
          v-model="Item.Notes"
          label="Notes"
          required
        ></v-text-field>
      </v-col>
      <v-col cols="12" md="4" class="mt-3">
        <v-btn :color="AddColor" dark>Add {{Mode}}</v-btn>
      </v-col>
    </v-row>

  </v-form>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import {ITransactionModel} from "../../../../@types/ITransactionModel";
    import * as moment from 'moment';
    import Tags from "../../components/Tags.vue";
    import {Prop} from "vue-property-decorator";
    import {IPagedRequest} from "../../../../@types/IPagedRequest";
    import {IAccount} from "../../../../@types/IAccount";
    import {ICategory} from "../../../../@types/ICategory";
    import {GetPaged} from "../../../Utilities/AxiosHelpers";

    @Component({
        components: {Tags}
    })
    export default class AddExpenseVue extends Vue {

        @Prop({required: true}) Mode !: string;

        valid: boolean = true;

        get AccountLabel(): string {
            if (this.Mode === "Expense") {
                return "From Account";
            }
            return "To Account";
        }

        get AddColor(): string {
            if (this.Mode === "Expense") {
                return "red--text";
            }
            return "primary";
        }

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

        Item: ITransactionModel = {
            Title: "",
            ExecutedAt: new Date().toISOString().substr(0, 10),
            ValueDate: null,
            Amount: 0,
            AccountFromId: null,
            CategoryId: null,
            Notes: null,
            Tags: []
        };

        Accounts: IAccount[] = [];
        Categories: ICategory[] = [];

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

        LoadAccounts() {
            let request: IPagedRequest = {
                Page: 0,
                PageSize: 100,
                Filters: [],
                Orders: [{Field: "Name", Ascending: true}]
            }

            GetPaged<IAccount>(request, "Account").then(response => this.Accounts = response.Value);

            GetPaged<ICategory>(request, "Category").then(response => this.Categories = response.Value);
        }

        mounted() {
            this.LoadAccounts();
        }

    }
</script>