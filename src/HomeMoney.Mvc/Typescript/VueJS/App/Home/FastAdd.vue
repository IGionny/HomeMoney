<template>
  <v-card min-width="500">
    <v-tabs
      background-color=" accent-4"
      centered
      dark
    >
      <v-tab>
        <span class="red--text">
        Expense
          </span>
      </v-tab>
      <v-tab>Transfer

      </v-tab>
      <v-tab>Income
      </v-tab>

      <v-tab-item>
        <AddTransaction class="m-2  pa-3 mt-4" Mode="Expense"></AddTransaction>
      </v-tab-item>
      <v-tab-item>
        <AddTransfer  class="m-2  pa-3 mt-4"></AddTransfer>
      </v-tab-item>
      <v-tab-item>
        <AddTransaction class="m-2  pa-3 mt-4" Mode="Income"></AddTransaction>
      </v-tab-item>
    </v-tabs>


  </v-card>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import AddTransaction from "./components/AddTransaction.vue";
    import AddTransfer from "./components/AddTransfer.vue";

    @Component({
        components: {AddTransfer, AddTransaction}
    })
    export default class FastAddVue extends Vue {
        tab: number = 0;

    }
</script>

<style scoped>

</style>