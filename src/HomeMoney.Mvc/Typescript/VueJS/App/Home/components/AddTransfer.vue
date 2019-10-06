<template>
  <v-form ref="form" v-model="valid" lazy-validation>
    <v-row>
      <v-col cols="12" md="6">
        <v-select
          v-model="Item.AccountFromId"
          :items="Accounts"
          item-text="Name"
          item-value="Id"
          :rules="[v => !!v || 'From Account is required']"
          label="From Account"
          required
        ></v-select>
      </v-col>
      <v-col cols="12" md="6">
        <v-select
          v-model="Item.AccountToId"
          :items="Accounts"
          item-text="Name"
          item-value="Id"
          :rules="[v => !!v || 'To Account is required']"
          label="To Account"
          required
        ></v-select>
      </v-col>

    </v-row>

    <v-row>
      <v-col cols="12" md="6">
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
      <v-col cols="12" md="6">
        <v-text-field

          prefix="€"
          type="number"
          v-model="Item.Amount"
          :counter="10"
          :rules="amountRules"
          label=""
          required
          hide-details
          min="0"
          style="text-align: right;"
        ></v-text-field>

      </v-col>
    </v-row>

    <v-row>
      <v-col cols="12" md="12">
        <Tags v-model="Item.Tags"></Tags>
      </v-col>
    </v-row>


    <v-row>
      <v-col cols="12" md="8">
        <v-text-field
          v-model="Item.Title"
          label="Title"
          required
        ></v-text-field>
      </v-col>
      <v-col cols="12" md="4" class="mt-3">
        <v-btn color="info" dark>Add transfer</v-btn>
      </v-col>
    </v-row>

  </v-form>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import * as moment from 'moment';
    import axios, {AxiosResponse} from 'axios';
    import Tags from "../../components/Tags.vue";
    import {IEntityReference} from "../../../../@types/IEntityReference";
    import {ITransferModel} from "../../../../@types/ITransferModel";

    @Component({
        components: {Tags}
    })
    export default class AddTransferVue extends Vue {

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

        Item: ITransferModel = {
            Title: "",
            ExecutedAt: new Date().toISOString().substr(0, 10),
            ValueDate: null,
            Amount: 0,
            AccountFromId: null,
            AccountToId: null,
            CategoryId: null,
            Notes: null,
            Tags: []
        };

        Accounts: IEntityReference[] = [];
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

        LoadAccounts() {
            axios.request<IEntityReference[]>({
                method: 'GET',
                url: '/Api/Accounts/All',
            }).then<IEntityReference[]>((response: AxiosResponse<IEntityReference[]>): PromiseLike<IEntityReference[]> => {
                this.Accounts = response.data;
                return new Promise<IEntityReference[]>(resolve => response.data);
            })
                .catch((error: any) => {
                    console.log(error)
                });

            axios.request<IEntityReference[]>({
                method: 'GET',
                url: '/Api/Categories/All',
            }).then<IEntityReference[]>((response: AxiosResponse<IEntityReference[]>): PromiseLike<IEntityReference[]> => {
                this.Categories = response.data;
                return new Promise<IEntityReference[]>(resolve => response.data);
            })
                .catch((error: any) => {
                    console.log(error)
                })
        }

        mounted() {
            this.LoadAccounts();
        }
    }
</script>

<style scoped>

</style>