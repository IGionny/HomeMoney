<template>
    <v-card min-width="500">
      <div v-for="transaction in Transactions">
        {{transaction.ExecutedAt}} 
      </div>
    </v-card>
</template>


<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import * as moment from 'moment';
    import axios, {AxiosResponse} from 'axios';
    import Tags from "../../components/Tags.vue";
    import {IEntityReference} from "../../../../@types/IEntityReference";
    import {ITransferModel} from "../../../../@types/ITransferModel";
    import {IRecentTransactionModel} from "../../../../@types/IRecentTransactionModel";

    @Component({
        components: {Tags}
    })
    export default class RecentTransactionsVue extends Vue {
        
        Transactions: IRecentTransactionModel[] = [];
        
        
        
    }
</script>

<style scoped>

</style>