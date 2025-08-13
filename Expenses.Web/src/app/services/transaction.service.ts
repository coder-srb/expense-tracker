import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Transaction } from '../models/transaction';
import { PostTransaction } from '../models/post-transaction';
import { PutTransaction } from '../models/put-transaction';

@Injectable({
  providedIn: 'root'
})
export class TransactionService 
{
  private apiUrl = 'https://localhost:5027/api/Transaction';

  constructor(private http: HttpClient) { }

  getAll(): Observable<Transaction[]>
  {
    return this.http.get<Transaction[]>(`${this.apiUrl}/All`)
  }

  getById(id: number): Observable<Transaction>
  {
    return this.http.get<Transaction>(`${this.apiUrl}/Details/${id}`)
  }

  create(transaction: PostTransaction): Observable<Transaction>   // in payload we should send only the properties that are mentioned in PostTransactionDto of backend
  {
    return this.http.post<Transaction>(`${this.apiUrl}/Create`, transaction);
  }

  update(id: number, transaction: PutTransaction): Observable<Transaction>  // in payload we should send only the properties that are mentioned in PutTransactionDto of backend
  {
    return this.http.put<Transaction>(`${this.apiUrl}/Update/${id}`, transaction);
  }

  delete(id: number): Observable<void>
  {
    return this.http.delete<void>(`${this.apiUrl}/Delete/${id}`);
  }
}


