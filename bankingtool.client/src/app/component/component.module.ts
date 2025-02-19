import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { AppPaths, PageTitle } from '../constant/constant';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { AddEditRoleComponent } from './add-edit-role/add-edit-role.component';
import { RoleListComponent } from './role-list/role-list.component';
import { AddEditUserComponent } from './add-edit-user/add-edit-user.component';
import { UserListComponent } from './user-list/user-list.component';
import { AddEditUserResolver } from '../core/resolver/add-edit-user.resolver';
import { CreateAccountComponent } from './create-account/create-account.component';
import { CreateAccountResolver } from '../core/resolver/create-account.resolver';
import { TransactionsListComponent } from './transactions-list/transactions-list.component';
import { TransactionListResolver } from '../core/resolver/transaction-list.resolver';

const routes: Routes = [
  {
    path: AppPaths.home,
    title: PageTitle.home,
    component: HomeComponent
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
    component: UserListComponent
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
    TransactionsListComponent
  ],
  imports: [
    RouterModule.forChild(routes),
    CommonModule,
    HttpClientModule,
    ReactiveFormsModule
  ]
})
export class ComponentModule { }
