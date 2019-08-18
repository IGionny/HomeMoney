<template>
  <v-layout column align-center>
    <v-layout wrap justify-space-around>
      <v-switch v-model="valid" class="ma-4" label="Valid" readonly></v-switch>
      <v-switch v-model="lazy" class="ma-4" label="Lazy"></v-switch>
    </v-layout>
    <v-form
      ref="form"
      v-model="valid"
      :lazy-validation="lazy"
    >
      <v-text-field
        v-model="name"
        :counter="10"
        :rules="nameRules"
        label="Name"
        required
      ></v-text-field>

      <v-text-field
        v-model="email"
        :rules="emailRules"
        label="E-mail"
        required
      ></v-text-field>

      <v-select
        v-model="select"
        :items="items"
        :rules="[v => !!v || 'Item is required']"
        label="Item"
        required
      ></v-select>

      <v-checkbox
        v-model="checkbox"
        :rules="[v => !!v || 'You must agree to continue!']"
        label="Do you agree?"
        required
      ></v-checkbox>

      <v-btn
        :disabled="!valid"
        color="success"
        class="mr-4"
        @click="validate"
      >
        Validate
      </v-btn>

      <v-btn
        color="error"
        class="mr-4"
        @click="reset"
      >
        Reset Form
      </v-btn>

      <v-btn
        color="warning"
        @click="resetValidation"
      >
        Reset Validation
      </v-btn>
    </v-form>
  </v-layout>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";

    @Component
    export default class PlaygroundVue extends Vue {

        valid: boolean = true;
        name: string = '';
        nameRules: any[] = [
            (v: any) => !!v || 'Name is required',
            (v: any) => (v && v.length <= 10) || 'Name must be less than 10 characters',
        ];

        email: string = '';
        emailRules: any[] = [
            (v: any) => !!v || 'E-mail is required',
            (v: any) => /.+@.+\..+/.test(v) || 'E-mail must be valid'];

        select: string | null = null;

        items: string[] = [
            'Item 1',
            'Item 2',
            'Item 3',
            'Item 4',
        ];

        checkbox: boolean = false;
        lazy: boolean = false;
        snackbar: boolean = false;

        validate() {
            if ((this.$refs.form as HTMLFormElement).validate()) {
                this.snackbar = true
            }
        }

        reset() {

            (this.$refs.form as HTMLFormElement).reset()
        }

        resetValidation() {
            (this.$refs.form as HTMLFormElement).resetValidation()
        }

    }
</script>

<style scoped>

</style>