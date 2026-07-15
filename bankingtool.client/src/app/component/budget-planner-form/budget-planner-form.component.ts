import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AppRoute } from '../../constant/constant';
import { NotificationService } from '../../core/notificationService/notification-service';
import { BudgetPlannerService } from '../../core/service/budget-planner.service';
import { ResponseDto } from '../../dto/internal-dto/response-dto';
import { GetBudgetPlannerDetailResponse } from '../../dto/response/get-budget-planner-response';

@Component({
  selector: 'app-budget-planner-form',
  templateUrl: './budget-planner-form.component.html',
  styleUrls: ['./budget-planner-form.component.css']
})
export class BudgetPlannerFormComponent implements OnInit {
  budgetPlannerForm: FormGroup;
  isEditMode = false;
  budgetPlannerId: number = 0;
  isLoading = false;
  isSaving = false;
  budgetDetails: GetBudgetPlannerDetailResponse | null = null;

  constructor(
    private formBuilder: FormBuilder,
    private budgetPlannerService: BudgetPlannerService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private notificationService: NotificationService
  ) {
    this.budgetPlannerForm = this.initializeForm();
  }

  ngOnInit() {
    const idParam = this.activatedRoute.snapshot.paramMap.get('id');
    this.budgetPlannerId = idParam ? +idParam : 0;
    this.isEditMode = this.budgetPlannerId > 0;

    if (this.isEditMode) {
      this.loadBudgetPlannerDetails();
    }
  }

  initializeForm(): FormGroup {
    return this.formBuilder.group({
      planName: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      description: ['', [Validators.required, Validators.minLength(10), Validators.maxLength(500)]],
      totalBudget: [0, [Validators.required, Validators.min(0.01)]],
      budgetItems: this.formBuilder.array([])
    });
  }

  get budgetItems(): FormArray {
    return this.budgetPlannerForm.get('budgetItems') as FormArray;
  }

  getBudgetItemGroup(index: number): any {
    return this.budgetItems.at(index);
  }

  loadBudgetPlannerDetails() {
    this.isLoading = true;
    this.budgetPlannerService.getBudgetPlannerDetail(this.budgetPlannerId).subscribe(
      (response: ResponseDto<GetBudgetPlannerDetailResponse>) => {
        this.isLoading = false;
        if (response.status === true && response.response) {
          this.budgetDetails = response.response;
          this.populateForm(response.response);
        } else {
          const messages = [
            ...(response?.errors?.map(x => x.errorMessage) || []),
            ...(response?.validationErrors?.map(x => x.errorMessage) || [])
          ];
          messages.push("Failed to load budget planner details.");
          messages.forEach(msg => this.notificationService.show(msg, 'error'));
          this.goBack();
        }
      },
      error => {
        this.isLoading = false;
        this.notificationService.show('An error occurred while loading budget planner details', 'error');
        this.goBack();
      }
    );
  }

  populateForm(data: GetBudgetPlannerDetailResponse) {
    this.budgetPlannerForm.patchValue({
      planName: data.PlanName,
      description: data.Description,
      totalBudget: data.TotalBudget
    });

    if (data.BudgetItems && data.BudgetItems.length > 0) {
      data.BudgetItems.forEach(item => {
        this.budgetItems.push(
          this.formBuilder.group({
            budgetName: [item.BudgetName, [Validators.required, Validators.minLength(3)]],
            allocatedAmount: [item.AllocatedAmount, [Validators.required, Validators.min(0.01)]]
          })
        );
      });
    } else {
      this.addBudgetItem();
    }
  }

  addBudgetItem() {
    this.budgetItems.push(
      this.formBuilder.group({
        budgetName: ['', [Validators.required, Validators.minLength(3)]],
        allocatedAmount: [0, [Validators.required, Validators.min(0.01)]]
      })
    );
  }

  removeBudgetItem(index: number) {
    if (this.budgetItems.length > 1) {
      this.budgetItems.removeAt(index);
      this.validateTotalAllocation();
    } else {
      this.notificationService.show('At least one budget item is required', 'warning');
    }
  }

  getTotalAllocated(): number {
    return this.budgetItems.value.reduce((sum: number, item: any) => sum + (parseFloat(item.allocatedAmount) || 0), 0);
  }

  getRemainingBudget(): number {
    const totalBudget = parseFloat(this.budgetPlannerForm.get('totalBudget')?.value) || 0;
    return totalBudget - this.getTotalAllocated();
  }

  validateTotalAllocation(): boolean {
    const totalBudget = parseFloat(this.budgetPlannerForm.get('totalBudget')?.value) || 0;
    const totalAllocated = this.getTotalAllocated();
    return totalAllocated <= totalBudget;
  }

  onTotalBudgetChange() {
    this.validateTotalAllocation();
  }

  onAllocationChange() {
    if (!this.validateTotalAllocation()) {
      this.notificationService.show('Total allocated amount exceeds budget', 'warning');
    }
  }

  saveBudgetPlanner() {
    if (!this.budgetPlannerForm.valid) {
      this.notificationService.show('Please fill all required fields correctly', 'warning');
      return;
    }

    if (!this.validateTotalAllocation()) {
      this.notificationService.show('Total allocated amount cannot exceed the total budget', 'error');
      return;
    }

    if (this.budgetItems.length === 0) {
      this.notificationService.show('At least one budget item is required', 'error');
      return;
    }

    this.isSaving = true;

    if (this.isEditMode) {
      this.updateBudgetPlanner();
    } else {
      this.createBudgetPlanner();
    }
  }

  createBudgetPlanner() {
    const formValue = this.budgetPlannerForm.value;
    const request = {
      request: {
        budgetPlanner: {
          planName: formValue.planName,
          description: formValue.description,
          totalBudget: formValue.totalBudget,
          budgetItems: formValue.budgetItems.map((item: any) => ({
            budgetName: item.budgetName,
            allocatedAmount: item.allocatedAmount
          }))
        }
      }
    };

    this.budgetPlannerService.createBudgetPlanner(request).subscribe(
      (response: ResponseDto<boolean>) => {
        this.isSaving = false;
        if (response.status === true) {
          this.notificationService.show('Budget planner created successfully', 'success');
          this.goBack();
        } else {
          const messages = [
            ...(response?.errors?.map(x => x.errorMessage) || []),
            ...(response?.validationErrors?.map(x => x.errorMessage) || [])
          ];
          messages.push("Failed to create budget planner.");
          messages.forEach(msg => this.notificationService.show(msg, 'error'));
        }
      },
      error => {
        this.isSaving = false;
        this.notificationService.show('An error occurred while creating the budget planner', 'error');
      }
    );
  }

  updateBudgetPlanner() {
    const formValue = this.budgetPlannerForm.value;
    const request = {
      request: {
        budgetPlanner: {
          budgetPlannerId: this.budgetPlannerId,
          planName: formValue.planName,
          description: formValue.description,
          totalBudget: formValue.totalBudget
        }
      }
    };

    this.budgetPlannerService.updateBudgetPlanner(request).subscribe(
      (response: ResponseDto<boolean>) => {
        this.isSaving = false;
        if (response.status === true) {
          this.notificationService.show('Budget planner updated successfully', 'success');
          this.goBack();
        } else {
          const messages = [
            ...(response?.errors?.map(x => x.errorMessage) || []),
            ...(response?.validationErrors?.map(x => x.errorMessage) || [])
          ];
          messages.push("Failed to update budget planner.");
          messages.forEach(msg => this.notificationService.show(msg, 'error'));
        }
      },
      error => {
        this.isSaving = false;
        this.notificationService.show('An error occurred while updating the budget planner', 'error');
      }
    );
  }

  goBack() {
    this.router.navigate([AppRoute.budgetPlannerList]);
  }

  get planNameControl() {
    return this.budgetPlannerForm.get('planName');
  }

  get descriptionControl() {
    return this.budgetPlannerForm.get('description');
  }

  get totalBudgetControl() {
    return this.budgetPlannerForm.get('totalBudget');
  }
}
