import Vue from 'vue';
import Component from 'vue-class-component';
import {Watch} from "vue-property-decorator";
import {IPagedRequest} from "../../@types/IPagedRequest";
import {IPagedResponse} from "../../@types/IPagedResponse";
import {VuetifyDataPagination} from "../../@types/VuetifyTypes";
import {GetPaged} from "../Utilities/AxiosHelpers";
import {IEntity} from "../../@types/IEntity";

@Component({})
export default class CommonGrid<T extends IEntity> extends Vue {

  entityName: string = "";

  options: any = {
    itemsPerPage: 15,
    page: 1,
    sortBy: ["CreatedAt"],
    sortDesc: [true]
  };
  pagedRequest: IPagedRequest = {
    Page: 0,
    PageSize: 15,
    Filters: [],
    Orders: [{Field: "CreatedAt", Ascending: false}]
  }

  lastPagedResponse: IPagedResponse<T> | null = null;

  @Watch("options", {deep: true})
  onOptionsChange() {
    this.pagedRequest.Page = (this.options.page as number) - 1;
    this.pagedRequest.PageSize = this.options.itemsPerPage as number;
    if (this.options.sortBy.length > 0) {
      this.pagedRequest.Orders = [{
        Field: this.options.sortBy[0] as string,
        Ascending: !this.options.sortDesc[0]
      }]
    } else {
      this.pagedRequest.Orders = [];
    }
    this.FetchDataSetAsync();
  }


  get Items(): T[] {
    if (this.lastPagedResponse === null) return [];
    return this.lastPagedResponse.Value;
  }

  get totalItems(): number {
    if (this.lastPagedResponse === null) return 0;
    return this.lastPagedResponse.Total;
  }

  mounted() {
    this.FetchDataSetAsync();
  }


  PaginationChange(pagination: VuetifyDataPagination) {
    this.pagedRequest.PageSize = pagination.itemsPerPage;
    this.pagedRequest.Page = pagination.page - 1;
  }

  async FetchDataSetAsync() {
    const result = await GetPaged<T>(this.pagedRequest, this.entityName);
    this.lastPagedResponse = result;
  }

  SelectRow(item: T) {
    this.$router.push({path: "/" + this.entityName + "/edit/" + item.Id});
  }

}

