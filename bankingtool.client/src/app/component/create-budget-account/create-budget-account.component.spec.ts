import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateBudgetAccountComponent } from './create-budget-account.component';

describe('CreateBudgetAccountComponent', () => {
  let component: CreateBudgetAccountComponent;
  let fixture: ComponentFixture<CreateBudgetAccountComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateBudgetAccountComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateBudgetAccountComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
