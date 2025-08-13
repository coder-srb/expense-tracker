// mapping with Transaction class of Backend
// this interface is used to represent a transaction while listing or viewing details

export interface Transaction 
{
    id: number;
    type: string;
    amount: number;
    category: string;
    createdAt: Date;
    updatedAt: Date;
}
