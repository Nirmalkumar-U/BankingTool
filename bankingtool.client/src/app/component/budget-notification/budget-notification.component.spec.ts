import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BudgetNotificationComponent } from './budget-notification.component';

describe('BudgetNotificationComponent', () => {
  let component: BudgetNotificationComponent;
  let fixture: ComponentFixture<BudgetNotificationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BudgetNotificationComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BudgetNotificationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
