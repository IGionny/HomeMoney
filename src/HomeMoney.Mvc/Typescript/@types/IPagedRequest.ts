export interface IPagedRequest {
  Page: number; //start by 1
  PageSize: number; //commonly 15
  Orders: IOrderRequest[];
  Filters: IFilterRequest[];
}

export interface IFilterRequest {
  FilterName: string;
  Value: string;
  Op?: string | null;
}

export interface IOrderRequest {
  Field: string;
  Ascending: boolean;
}
