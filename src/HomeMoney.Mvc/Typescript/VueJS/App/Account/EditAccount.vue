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
        <v-btn dark @click="Save">Save</v-btn>
      </v-col>
    </v-row>

  </v-form>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import {IAccount} from "../../../@types/IAccount";
    import {Prop} from "vue-property-decorator";
    import {SaveAsync} from "../../Utilities/AxiosHelpers";
    import {IResultModel} from "../../../@types/IResultModel";

    @Component
    export default class EditAccountVue extends Vue {
        valid: boolean = true;

        Item: IAccount = {
            Name: "",
            FirstBalance: 0,
            FromAt: null,
            ToAt: null,
            IsArchived: false,
            Owner: null
        };

        @Prop({required: true}) Id!: string;

        beforeRouteUpdate(to: any, from: object, next: Function) {
            this.Id = to.params.Id;
            next();
        }

        LoadAccount() {
            if (this.Id !== null && this.Id !== undefined) {
                //Load Account
            }
        }

        mounted() {
            this.LoadAccount();
        }

        nameRules: any[] = [
            (v: any) => !!v || 'Name is required',
            (v: any) => (v && v.length > 3) || 'Name must be greater than 3 characters',
        ];


        validate() {
            if ((<any>this.$refs.form).validate()) {

            }
        }

        reset() {
            (<any>this.$refs.form).reset()
        }

        resetValidation() {
            (<any>this.$refs.form).resetValidation();
        }

        Save() {
            const self = this;
            SaveAsync<IAccount>(this.Item, "Account").then((response: IResultModel<IAccount>) => {
                self.Item = response.Value;
                //todo: Message info ok;
                //todo: programmatically redirect to grid
                self.$router.push({path: "/accounts"});
            });
        }

        beforeRouteLeave(to: any, from: object, next: Function) {
            const answer = window.confirm('Do you really want to leave? you have unsaved changes!')
            if (answer) {
                next()
            } else {
                next(false)
            }
        }
    }
</script>

<style scoped>

</style>