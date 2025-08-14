import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { TransactionService } from '../../services/transaction.service';

@Component({
  selector: 'app-transaction-form',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './transaction-form.component.html',
  styleUrl: './transaction-form.component.css'
})
export class TransactionFormComponent implements OnInit {

  transactionForm : FormGroup;

  incomeCategories = ['Salary', 'Freelance', 'Investment', 'Other'];
  expenseCategories = ['Food', 'Transport', 'Utilities', 'Entertainment', 'Other'];
  availableCategories: string[] = [];

  editMode: boolean = false;
  transactionId?: number;

  constructor(private fb: FormBuilder, private router: Router, private activatedRoute:ActivatedRoute, private transactionService: TransactionService) {
    this.transactionForm = this.fb.group({
      type: ['Expense', Validators.required],
      category: ['', Validators.required],
      amount: ['', [Validators.required, Validators.min(0)]],
      createdAt: [new Date(), Validators.required]
    })
  }

  ngOnInit(): void {
    const type = this.transactionForm.get('type')?.value;
    this.updateAvailableCategories(type);

    this.activatedRoute.params.subscribe( t => {
      this.transactionId = +t['id'];
      if(this.transactionId) {
        this.editMode = true;
        this.loadTransaction(this.transactionId);
      }
    })
  }

  loadTransaction(id: number): void {
    this.transactionService.getById(id).subscribe({
      next: (transaction) => {
        this.updateAvailableCategories(transaction.type);
        this.transactionForm.patchValue({
          type: transaction.type,
          category: transaction.category,
          amount: transaction.amount,
        });
      },
      error: (err) => {
        console.error('Error loading transaction:', err);
        // this.router.navigate(['/transactions']);
      }
    });
  } 

  onTypeChange(): void {
    const type = this.transactionForm.get('type')?.value;
    this.updateAvailableCategories(type);
  }

  updateAvailableCategories(type: string): void {
    this.availableCategories = type === 'Expense' ? this.expenseCategories : this.incomeCategories
    this.transactionForm.patchValue({category: '' }); // Reset category when type changes
  }

  onSubmit(){
    if(this.transactionForm.valid) {
      const transactionData = this.transactionForm.value;
      console.log('Transaction Data:', transactionData);
      
      // update
      if(this.editMode && this.transactionId) {
        this.transactionService.update(this.transactionId, transactionData).subscribe({
          next: () => {
            this.router.navigate(['/transactions']);
          },
          error: (err) =>{
            console.error('Error updating transaction:', err);
          }
        })
      }
      else{
        // create
        this.transactionService.create(transactionData).subscribe(() =>{
          this.router.navigate(['/transactions']);
        }, err => {
          console.error('Error creating transaction:', err);
        })
      }

    }
  }

  cancel(){
    this.router.navigate(['/transactions']);
  }

}
