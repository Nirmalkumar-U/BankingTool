import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AppRoute } from '../../constant/constant';
import { NotificationService } from '../../core/notificationService/notification-service';
import { BudgetPlannerService } from '../../core/service/budget-planner.service';
import { ResponseDto } from '../../dto/internal-dto/response-dto';
import { GetBudgetPlannerList, GetBudgetPlannerListResponse } from '../../dto/response/get-budget-planner-response';

@Component({
  selector: 'app-budget-planner-list',
  templateUrl: './budget-planner-list.component.html',
  styleUrls: ['./budget-planner-list.component.css']
})
export class BudgetPlannerListComponent implements OnInit {
  budgetPlannerList: GetBudgetPlannerList[] = [];

  constructor(
    private budgetPlannerService: BudgetPlannerService,
    private readonly router: Router,
    private notificationService: NotificationService
  ) { }

  ngOnInit() {
    this.loadBudgetPlanners();
  }

  loadBudgetPlanners() {
    this.budgetPlannerService.getBudgetPlannerList().subscribe(
      (response: ResponseDto<GetBudgetPlannerListResponse>) => {
        if (response.status === true && response.response) {
          this.budgetPlannerList = response.response.budgetPlanners;
        } else {
          const messages = [
            ...(response?.errors?.map(x => x.errorMessage) || []),
            ...(response?.validationErrors?.map(x => x.errorMessage) || [])
          ];
          messages.push("Failed to load budget planners.");
          messages.forEach(msg => this.notificationService.show(msg, 'error'));
        }
      },
      error => {
        this.notificationService.show('An error occurred while loading budget planners', 'error');
      }
    );
  }

  createNewPlan() {
    this.router.navigate([AppRoute.createBudgetPlanner()]);
  }

  editPlan(budgetPlannerId: number) {
    this.router.navigate([AppRoute.editBudgetPlanner(budgetPlannerId)]);
  }

  viewDetails(budgetPlannerId: number) {
    this.router.navigate([AppRoute.budgetPlannerDetail(budgetPlannerId)]);
  }

  deletePlan(budgetPlannerId: number) {
    if (confirm('Are you sure you want to delete this budget planner?')) {
      this.budgetPlannerService.deleteBudgetPlanner(budgetPlannerId).subscribe(
        (response: ResponseDto<boolean>) => {
          if (response.status === true) {
            this.notificationService.show('Budget planner deleted successfully', 'success');
            this.loadBudgetPlanners();
          } else {
            const messages = [
              ...(response?.errors?.map(x => x.errorMessage) || []),
              ...(response?.validationErrors?.map(x => x.errorMessage) || [])
            ];
            messages.push("Failed to delete budget planner.");
            messages.forEach(msg => this.notificationService.show(msg, 'error'));
          }
        },
        error => {
          this.notificationService.show('An error occurred while deleting the budget planner', 'error');
        }
      );
    }
  }

  getPercentageAllocated(planner: GetBudgetPlannerList): number {
    if (planner.totalBudget === 0) return 0;
    return (planner.allocatedAmount / planner.totalBudget) * 100;
  }

  getRemainingBudget(planner: GetBudgetPlannerList): number {
    return planner.totalBudget - planner.allocatedAmount;
  }
}
