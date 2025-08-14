// mapping with PostTransactionDto class of Backend
// this interface is used to create a transaction

export interface PostTransaction 
{
    type: string;
    amount: number;
    category: string;
    createdAt: Date;
}

// even if we don't create this interface in the frontend, backend will automatically map the properties of PostTransactionDto to the properties of Transaction class