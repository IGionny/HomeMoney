<template>
  <div>
    <v-card>
      <v-tabs
        background-color=" accent-4"
        center-active
        dark
      >
        <v-tab>
          Expense

        </v-tab>
        <v-tab>Transfer

        </v-tab>
        <v-tab>Income
        </v-tab>

        <v-tab-item>
          <AddExpense></AddExpense>
        </v-tab-item>
        <v-tab-item>
          2
        </v-tab-item>
        <v-tab-item>
          3
        </v-tab-item>
      </v-tabs>


    </v-card>
  </div>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import AddExpense from "./components/AddExpense.vue";

    @Component({
        components: {AddExpense}
    })
    export default class FastAddVue extends Vue {
        tab: number = 0;

    }
</script>

<style scoped>

</style>