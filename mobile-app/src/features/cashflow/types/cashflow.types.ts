export type CashflowEntryType = 'income' | 'expense';

export type CashflowEntry = {
  id: string;
  entryType: CashflowEntryType;
  amount: number;
  entryDate: string;
};
