import {IUserEntity} from "./IUserEntity";
import {IEntityReference} from "./IEntityReference";
import {TransactionType} from "./TransactionType";

export interface ITransaction extends IUserEntity {
  Title: string | null;
  AmountExpense: number;
  AmountIncome: number;
  Type: TransactionType;
  Notes: string | null;
  ExecutedAt: string;
  ValueDate: string | null;
  Category: IEntityReference | null;
  AccountFrom: IEntityReference | null;
  AccountTo: IEntityReference | null;
  Tags: string | null;
  TagList: string[];
}

export interface ITransactionRow {
  Date: string;
  Title: string;
  Expense: number;
  Income: number;
  Category: string;
  Tags: string;
  CreatedAt: string;
}
