import { Component, Input, Output, EventEmitter, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MutualFundsDto, InsertUserInvestmentRequest } from '../../../dto/mutual-funds-dto';

@Component({
  selector: 'app-invest-modal',
  templateUrl: './invest-modal.component.html',
  styleUrls: ['./invest-modal.component.css']
})
export class InvestModalComponent {
  @Input() mutualFundsList: MutualFundsDto[] = [];
  @Output() investmentSubmitted = new EventEmitter<InsertUserInvestmentRequest>();
  @ViewChild('investModalDialog') modalDialog: any;

  investmentForm: FormGroup;
  isSubmitting: boolean = false;

  constructor(private formBuilder: FormBuilder) {
    this.investmentForm = this.formBuilder.group({
      schemeCode: ['', Validators.required],
      installmentAmount: ['', [Validators.required, Validators.min(0.01)]]
    });
  }

  openModal() {
    this.investmentForm.reset();
    const modal = document.getElementById('investModal');
    if (modal) {
      const bootstrapModal = new (window as any).bootstrap.Modal(modal);
      bootstrapModal.show();
    }
  }

  closeModal() {
    const modal = document.getElementById('investModal');
    if (modal) {
      const bootstrapModal = (window as any).bootstrap.Modal.getInstance(modal);
      if (bootstrapModal) {
        bootstrapModal.hide();
      }
    }
  }

  onSubmit() {
    if (this.investmentForm.invalid) {
      return;
    }

    this.isSubmitting = true;

    const formValue = this.investmentForm.value;
    const request: InsertUserInvestmentRequest = {
      schemeCodes: [formValue.schemeCode],
      installmentAmount: parseFloat(formValue.installmentAmount)
    };

    this.investmentSubmitted.emit(request);

    // Reset form and close after a small delay
    setTimeout(() => {
      this.isSubmitting = false;
      this.closeModal();
      this.investmentForm.reset();
    }, 500);
  }

  get schemeCodeControl() {
    return this.investmentForm.get('schemeCode');
  }

  get installmentAmountControl() {
    return this.investmentForm.get('installmentAmount');
  }

  getSelectedSchemeDetails(): MutualFundsDto | undefined {
    const schemeCode = this.investmentForm.get('schemeCode')?.value;
    return this.mutualFundsList.find(x => x.schemeCode === schemeCode);
  }
}
