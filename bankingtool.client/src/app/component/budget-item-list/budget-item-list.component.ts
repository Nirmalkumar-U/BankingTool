import { Component, Input, Output, EventEmitter } from '@angular/core';
import { BudgetItemResponse } from '../../dto/response/get-budget-planner-response';

@Component({
  selector: 'app-budget-item-list',
  templateUrl: './budget-item-list.component.html',
  styleUrls: ['./budget-item-list.component.css']
})
export class BudgetItemListComponent {
  @Input() budgetItems: BudgetItemResponse[] = [];
  @Input() totalBudget: number = 0;
  @Output() itemDeleted = new EventEmitter<number>();
  @Output() itemEdited = new EventEmitter<BudgetItemResponse>();

  getTotalAllocated(): number {
    return this.budgetItems.reduce((sum, item) => sum + item.AllocatedAmount, 0);
  }

  getRemainingBudget(): number {
    return this.totalBudget - this.getTotalAllocated();
  }

  getPercentageAllocated(item: BudgetItemResponse): number {
    if (this.totalBudget === 0) return 0;
    return (item.AllocatedAmount / this.totalBudget) * 100;
  }

  deleteItem(itemId: number) {
    if (confirm('Are you sure you want to delete this budget item?')) {
      this.itemDeleted.emit(itemId);
    }
  }

  editItem(item: BudgetItemResponse) {
    this.itemEdited.emit(item);
  }
}
