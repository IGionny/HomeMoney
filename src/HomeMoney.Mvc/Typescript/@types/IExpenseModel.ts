import {IEntityReference} from "./IEntityReference";

export interface IExpenseModel {
  Title: string | null;
  Amount: number;
  Notes: string | null;
  ExecutedAt: string;
  ValueDate: string | null;
  Category: IEntityReference | null;
  AccountFrom: IEntityReference | null;
  Tags: string[];
}
