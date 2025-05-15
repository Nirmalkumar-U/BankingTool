import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BudgetTransactionListComponent } from './budget-transaction-list.component';

describe('BudgetTransactionListComponent', () => {
  let component: BudgetTransactionListComponent;
  let fixture: ComponentFixture<BudgetTransactionListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BudgetTransactionListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BudgetTransactionListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
