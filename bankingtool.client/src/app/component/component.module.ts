import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { FullCalendarModule } from '@fullcalendar/angular';
import { AppPaths, PageTitle } from '../constant/constant';
import { SharedModule } from '../core/component/shared.module';
import { AccountNotificationResolver } from '../core/resolver/account-notification.resolver';
import { AccountStatementReportResolver } from '../core/resolver/account-statement-report.resolver';
import { AddEditUserResolver } from '../core/resolver/add-edit-user.resolver';
import { BudgetListResolver } from '../core/resolver/budget-list.resolver';
import { BudgetNotificationResolver } from '../core/resolver/budget-notification.resolver';
import { BudgetStatementReportResolver } from '../core/resolver/budget-statement-report.resolver';
import { BudgetTransactionListResolver } from '../core/resolver/budget-transaction-list.resolver';
import { BudgetTransferResolver } from '../core/resolver/budget-transfer.resolver';
import { CashWithdrawResolver } from '../core/resolver/cash-withdraw.resolver';
import { CreateAccountResolver } from '../core/resolver/create-account.resolver';
import { HomeResolver } from '../core/resolver/home.resolver';
import { SelfTransferResolver } from '../core/resolver/self-transfer.resolver';
import { TransactionListResolver } from '../core/resolver/transaction-list.resolver';
import { TransferAmountResolver } from '../core/resolver/transfer-amount.resolver';
import { UserListResolver } from '../core/resolver/user-list.resolver';
import { AccountNotificationComponent } from './account-notification/account-notification.component';
import { AccountStatementReportComponent } from './account-statement-report/account-statement-report.component';
import { AddEditRoleComponent } from './add-edit-role/add-edit-role.component';
import { AddEditUserComponent } from './add-edit-user/add-edit-user.component';
import { BudgetListComponent } from './budget-list/budget-list.component';
import { BudgetNotificationComponent } from './budget-notification/budget-notification.component';
import { BudgetStatementReportComponent } from './budget-statement-report/budget-statement-report.component';
import { BudgetTransactionListComponent } from './budget-transaction-list/budget-transaction-list.component';
import { BudgetTransferComponent } from './budget-transfer/budget-transfer.component';
import { CalenderComponent } from './calender/calender.component';
import { CashWithdrawComponent } from './cash-withdraw/cash-withdraw.component';
import { CreateAccountComponent } from './create-account/create-account.component';
import { CreateBudgetAccountComponent } from './create-budget-account/create-budget-account.component';
import { DepositAmountComponent } from './desposit-amount/deposit-amount.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { RoleListComponent } from './role-list/role-list.component';
import { TransactionsListComponent } from './transactions-list/transactions-list.component';
import { TransferAmountComponent } from './transfer-amount/transfer-amount.component';
import { UserListComponent } from './user-list/user-list.component';
import { AddEventDialogComponent } from './calender/add-event-dialog/add-event-dialog.component';

const routes: Routes = [
  {
    path: AppPaths.home,
    title: PageTitle.home,
    component: HomeComponent,
    resolve: { DataResolver: HomeResolver }
  },
  {
    path: AppPaths.login,
    title: PageTitle.login,
    component: LoginComponent
  },
  {
    path: AppPaths.addEditRole,
    title: PageTitle.addEditRole,
    component: AddEditRoleComponent,
  },
  {
    path: AppPaths.roleList,
    title: PageTitle.roleList,
    component: RoleListComponent
  },
  {
    path: AppPaths.addEditUser,
    title: PageTitle.addEditUser,
    component: AddEditUserComponent,
    resolve: { DataResolver: AddEditUserResolver },
  },
  {
    path: AppPaths.userList,
    title: PageTitle.userList,
    component: UserListComponent,
    resolve: { DataResolver: UserListResolver }
  },
  {
    path: AppPaths.createAccount,
    title: PageTitle.createAccount,
    component: CreateAccountComponent,
    resolve: { DataResolver: CreateAccountResolver },
  },
  {
    path: AppPaths.transactions,
    title: PageTitle.transactions,
    component: TransactionsListComponent,
    resolve: { DataResolver: TransactionListResolver }
  },
  {
    path: AppPaths.transfer,
    title: PageTitle.transfer,
    data: { reportName: PageTitle.transfer },
    component: TransferAmountComponent,
    resolve: { DataResolver: TransferAmountResolver }
  },
  {
    path: AppPaths.selfTransfer,
    title: PageTitle.selfTransfer,
    data: { reportName: PageTitle.selfTransfer },
    component: TransferAmountComponent,
    resolve: { DataResolver: SelfTransferResolver }
  },
  {
    path: AppPaths.depositAmount,
    title: PageTitle.depositAmount,
    component: DepositAmountComponent,
    resolve: { DataResolver: SelfTransferResolver }
  },
  {
    path: AppPaths.cashWithdraw,
    title: PageTitle.cashWithdraw,
    component: CashWithdrawComponent,
    resolve: { DataResolver: CashWithdrawResolver }
  },
  {
    path: AppPaths.createBudgetAccount,
    title: PageTitle.createBudgetAccount,
    component: CreateBudgetAccountComponent,
  },
  {
    path: AppPaths.budgetTransactionList,
    title: PageTitle.budgetTransactionList,
    component: BudgetTransactionListComponent,
    resolve: { DataResolver: BudgetTransactionListResolver }
  },
  {
    path: AppPaths.budgetNotification,
    title: PageTitle.budgetNotification,
    component: BudgetNotificationComponent,
    resolve: { DataResolver: BudgetNotificationResolver }
  },
  {
    path: AppPaths.accountNotification,
    title: PageTitle.accountNotification,
    component: AccountNotificationComponent,
    resolve: { DataResolver: AccountNotificationResolver }
  },
  {
    path: AppPaths.budgetStatementReport,
    title: PageTitle.budgetStatementReport,
    component: BudgetStatementReportComponent,
    resolve: { DataResolver: BudgetStatementReportResolver }
  },
  {
    path: AppPaths.accountStatementReport,
    title: PageTitle.accountStatementReport,
    component: AccountStatementReportComponent,
    resolve: { DataResolver: AccountStatementReportResolver }
  },
  {
    path: AppPaths.budgetList,
    title: PageTitle.budgetList,
    component: BudgetListComponent,
    resolve: { DataResolver: BudgetListResolver }
  },
  {
    path: AppPaths.budgetTransfer,
    title: PageTitle.budgetTransfer,
    component: BudgetTransferComponent,
    resolve: { DataResolver: BudgetTransferResolver }
  },
  {
    path: AppPaths.calender,
    title: PageTitle.calender,
    component: CalenderComponent
  }
];
@NgModule({
  declarations: [
    HomeComponent,
    LoginComponent,
    AddEditRoleComponent,
    RoleListComponent,
    AddEditUserComponent,
    UserListComponent,
    CreateAccountComponent,
    TransactionsListComponent,
    TransferAmountComponent,
    DepositAmountComponent,
    CashWithdrawComponent,
    CreateBudgetAccountComponent,
    BudgetTransactionListComponent,
    BudgetNotificationComponent,
    AccountNotificationComponent,
    AccountStatementReportComponent,
    BudgetStatementReportComponent,
    BudgetListComponent,
    BudgetTransferComponent,
    CalenderComponent,
    AddEventDialogComponent
  ],
  imports: [
    RouterModule.forChild(routes),
    CommonModule,
    HttpClientModule,
    ReactiveFormsModule,
    SharedModule,
    FullCalendarModule
  ]
})
export class ComponentModule { }
