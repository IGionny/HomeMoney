<template>
  <v-layout column align-center>
    <FastAdd></FastAdd>
    <LastTransactions></LastTransactions>
  </v-layout>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import FastAdd from "./FastAdd.vue";
    @Component({
        components: {FastAdd}
    })
    export default class HomeIndexVue extends Vue {
        
    }
</script>
