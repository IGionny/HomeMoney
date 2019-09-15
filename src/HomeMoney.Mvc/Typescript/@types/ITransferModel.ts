export interface ITransferModel {
  Title: string | null;
  Amount: number;
  Notes: string | null;
  ExecutedAt: string;
  ValueDate: string | null;
  CategoryId: string | null;
  AccountFromId: string | null;
  AccountToId: string | null;
  Tags: string[];
}

