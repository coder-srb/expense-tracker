// mapping with PostTransactionDto class of Backend
// this interface is used to create a transaction

export interface PostTransaction 
{
    type: string;
    amount: number;
    category: string;
}
