// mapping with PutTransactionDto class of Backend
// this interface is used to update a transaction

export interface PutTransaction {
    type: string;
    amount: number;
    category: string;
}
