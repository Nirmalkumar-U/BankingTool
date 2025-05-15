import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccountStatementReportComponent } from './account-statement-report.component';

describe('AccountStatementReportComponent', () => {
  let component: AccountStatementReportComponent;
  let fixture: ComponentFixture<AccountStatementReportComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AccountStatementReportComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AccountStatementReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
