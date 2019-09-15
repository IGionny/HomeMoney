<template>

  <v-combobox
    v-model="Value"
    :filter="filter"
    :hide-no-data="!searchTerm"
    :items="tags"
    :search-input.sync="searchTerm"
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
          label
          small
        >
          {{ searchTerm }}
        </v-chip>
      </v-list-item>
    </template>
    <template v-slot:selection="{ attrs, item, parent, selected }">
      <v-chip
        
        v-bind="attrs"
        :input-value="selected"
        label
        small
      >
        <span class="pr-2">
          {{ item }}
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
        {{ item }}
      </v-chip>
      <div class="flex-grow-1"></div>

    </template>
  </v-combobox>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import {Prop} from "vue-property-decorator";
    import {Watch} from "vue-property-decorator";
    import axios, {AxiosResponse} from 'axios';

    @Component
    export default class TagsVue extends Vue {

        @Prop() value !: string[];

        get Value(): string[] {
            return this.value;
        }

        set Value(values: string[]) {
            if (this.value.length === values.length) {
                let areEqual = true;
                for (let v of values) {
                    areEqual = this.value.find(x => x === v) !== undefined;
                }
                if (areEqual) {
                    return;
                }
            }

            this.$emit("input", values);
        }

        tags: string[] = [];
        model: string[] = [];
        searchTerm: string | null = null;

        @Watch("model")
        onChangeModel(val: any, prev: any) {
            if (val.length === prev.length) return;
            this.model = val.map((v: unknown) => {
                if (typeof v === 'string') {
                    this.tags.push(v)
                }
                return v
            })
        }


        filter(item: any, queryText: string, itemText: string) {
            if (item.header) return false

            const hasValue = (val: any) => val != null ? val : '';

            const text = hasValue(itemText)
            const query = hasValue(queryText)

            return text.toString()
                .toLowerCase()
                .indexOf(query.toString().toLowerCase()) > -1
        }


        @Watch("searchTerm")
        onSearch() {
            this.searchTag(this.searchTerm);
        }


        searchTag(searchTerm: string | null) {

            if (searchTerm === null || searchTerm.length === 0) {
                this.tags = [];
                return;
            }
            axios.request<string[]>({
                method: 'GET',
                url: '/Api/Tags/Search/' + searchTerm,
            }).then<string[]>((response: AxiosResponse<string[]>): PromiseLike<string[]> => {
                this.tags = response.data;
                return new Promise<string[]>(resolve => response.data);
            })
                .catch((error: any) => {
                    console.log(error)
                })

        }
    }
</script>
