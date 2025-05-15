import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BudgetStatementReportComponent } from './budget-statement-report.component';

describe('BudgetStatementReportComponent', () => {
  let component: BudgetStatementReportComponent;
  let fixture: ComponentFixture<BudgetStatementReportComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BudgetStatementReportComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BudgetStatementReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
