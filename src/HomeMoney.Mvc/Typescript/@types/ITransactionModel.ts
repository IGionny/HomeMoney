export interface ITransactionModel {
  Title: string | null;
  Amount: number;
  Notes: string | null;
  ExecutedAt: string;
  ValueDate: string | null;
  CategoryId: string | null;
  AccountFromId: string | null;
  Tags: string[];
}

