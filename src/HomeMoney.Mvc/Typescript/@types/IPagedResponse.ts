import {IResultModel} from "./IResultModel";

export interface IPagedResponse<T> extends IResultModel<T[]> {
  Total: number;
  Pages: number;
  PageSize: number;
  Current: number;
  HasMoreElement: boolean;
}
