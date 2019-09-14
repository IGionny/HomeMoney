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
          v-model="Item.AccountFrom"
          :items="Accounts"
          :rules="[v => !!v || 'Item is required']"
          label="From account"
          required
        ></v-select>
      </v-col>

    </v-row>


    <v-combobox
      v-model="model"
      :filter="filter"
      :hide-no-data="!search"
      :items="items"
      :search-input.sync="search"
      hide-selected
      label="Add some #tags"
      multiple
      small-chips
      solo
    >
      <template v-slot:no-data>
        <v-list-item>
          <span class="subheading">Create</span>
          <v-chip
            :color="`${colors[nonce - 1]} `"
            label
            small
          >
            {{ search }}
          </v-chip>
        </v-list-item>
      </template>
      <template v-slot:selection="{ attrs, item, parent, selected }">
        <v-chip
          v-if="item === Object(item)"
          v-bind="attrs"
          :color="`${item.color}`"
          :input-value="selected"
          label
          small
        >
        <span class="pr-2">
          {{ item.text }}
        </span>
          <v-icon
            small
            @click="parent.selectItem(item)"
          >fa-times
          </v-icon>
        </v-chip>
      </template>
      <template v-slot:item="{ index, item }">
        <v-chip
          dark
          label
          small
        >
          {{ item.text }}
        </v-chip>
        <div class="flex-grow-1"></div>
        
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
      <v-col cols="12" md="4" class="mt-3">
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
    import {Watch} from "vue-property-decorator";

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

        /*------------*/

        activator: any = null;
        attach: any = null;
        colors: string[] = ['green', 'purple', 'indigo', 'cyan', 'teal', 'orange'];
        editing: any = null;
        index: number = -1;
        items: any[] = [
            {header: 'Select a tag or create one'},
            {
                text: 'Foo',
                color: 'blue',
            },
            {
                text: 'Bar',
                color: 'red',
            },
        ];
        nonce: number = 1;
        menu: boolean = false;
        model: any[] = [
            {
                text: 'Foo',
                color: 'blue',
            },
        ];

        x: number = 0;
        search: any = null;
        y: number = 0;

        @Watch("model")
        onChangeModel(val: any, prev: any) {
            if (val.length === prev.length) return;

            this.model = val.map((v:unknown) => {
                if (typeof v === 'string') {
                    v = {
                        text: v,
                        color: this.colors[this.nonce - 1],
                    }

                    this.items.push(v)

                    this.nonce++
                }

                return v
            })
        }


        edit(index: number, item: any) {
            if (!this.editing) {
                this.editing = item
                this.index = index
            } else {
                this.editing = null
                this.index = -1
            }
        };

        filter(item: any, queryText: string, itemText: string) {
            if (item.header) return false

            const hasValue = (val:any) => val != null ? val : '';

            const text = hasValue(itemText)
            const query = hasValue(queryText)

            return text.toString()
                .toLowerCase()
                .indexOf(query.toString().toLowerCase()) > -1
        }

    }
</script>

<style scoped>

</style>