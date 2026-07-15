import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AppRoute } from '../../constant/constant';
import { NotificationService } from '../../core/notificationService/notification-service';
import { BudgetPlannerService } from '../../core/service/budget-planner.service';
import { ResponseDto } from '../../dto/internal-dto/response-dto';
import { GetBudgetPlannerDetailResponse } from '../../dto/response/get-budget-planner-response';

@Component({
  selector: 'app-budget-planner-detail',
  templateUrl: './budget-planner-detail.component.html',
  styleUrls: ['./budget-planner-detail.component.css']
})
export class BudgetPlannerDetailComponent implements OnInit {
  budgetPlannerDetail: GetBudgetPlannerDetailResponse | null = null;
  isLoading = false;
  budgetPlannerId: number = 0;

  constructor(
    private budgetPlannerService: BudgetPlannerService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private notificationService: NotificationService
  ) { }

  ngOnInit() {
    const idParam = this.activatedRoute.snapshot.paramMap.get('id');
    this.budgetPlannerId = idParam ? +idParam : 0;

    if (this.budgetPlannerId > 0) {
      this.loadBudgetPlannerDetail();
    } else {
      this.notificationService.show('Invalid budget planner ID', 'error');
      this.goBack();
    }
  }

  loadBudgetPlannerDetail() {
    this.isLoading = true;
    this.budgetPlannerService.getBudgetPlannerDetail(this.budgetPlannerId).subscribe(
      (response: ResponseDto<GetBudgetPlannerDetailResponse>) => {
        this.isLoading = false;
        if (response.status === true && response.response) {
          this.budgetPlannerDetail = response.response;
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

  editBudgetPlanner() {
    this.router.navigate([AppRoute.editBudgetPlanner(this.budgetPlannerId)]);
  }

  deleteBudgetPlanner() {
    if (confirm('Are you sure you want to delete this budget planner?')) {
      this.budgetPlannerService.deleteBudgetPlanner(this.budgetPlannerId).subscribe(
        (response: ResponseDto<boolean>) => {
          if (response.status === true) {
            this.notificationService.show('Budget planner deleted successfully', 'success');
            this.goBack();
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

  getTotalAllocated(): number {
    if (!this.budgetPlannerDetail || !this.budgetPlannerDetail.BudgetItems) return 0;
    return this.budgetPlannerDetail.BudgetItems.reduce((sum, item) => sum + item.AllocatedAmount, 0);
  }

  getRemainingBudget(): number {
    if (!this.budgetPlannerDetail) return 0;
    return this.budgetPlannerDetail.TotalBudget - this.getTotalAllocated();
  }

  getPercentageAllocated(): number {
    if (!this.budgetPlannerDetail || this.budgetPlannerDetail.TotalBudget === 0) return 0;
    return (this.getTotalAllocated() / this.budgetPlannerDetail.TotalBudget) * 100;
  }

  goBack() {
    this.router.navigate([AppRoute.budgetPlannerList]);
  }
}
