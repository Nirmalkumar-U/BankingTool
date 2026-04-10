import { CreateIndexedDb } from "../core/indexedDBService/indexed-db-service.service";
import { YesOrNoDto } from "../dto/internal-dto/drop-down-dto";

export class Constant {
  public static true: string = 'true';
  public static actionDBName: string = 'actionDBName';
  public static actionTableName: string = 'action';
  public static dbVersion: number = 1.0
  public static actionTablePrimaryKey: string = 'actionId';
  public static actionCoumns: CreateIndexedDb[] = [{ columnName: 'actionId', isUnique: true }, { columnName: 'actionName', isUnique: false }, { columnName: 'actionPath', isUnique: false },
  { columnName: 'actionType', isUnique: false }, { columnName: 'menuLevel', isUnique: false }, { columnName: 'parrentMenuId', isUnique: false }, { columnName: 'sequence', isUnique: false }];

  public static yesOrNo: YesOrNoDto[] = [{ key: true, value: 'Yes' },{ key: false, value: 'No' }]
}
export class AppPaths {
  public static home: string = 'home';
  public static login: string = 'login';

  //role
  public static addEditRole: string = 'addEditRole';
  public static roleList: string = 'roleList';

  //user
  public static userList: string = 'userList';
  public static addEditUser: string = 'addEditUser';

  //Account
  public static createAccount: string = 'createAccount';
  public static transactions: string = 'transactions';
  public static transfer: string = 'transfer';
  public static selfTransfer: string = 'selfTransfer';
  public static depositAmount: string = 'depositAmount';
  public static cashWithdraw: string = 'cashWithdraw';

  //Budget
  public static createBudgetAccount: string = 'createBudgetAccount';
  public static budgetTransactionList: string = 'budgetTransactionList/:id';
  public static budgetList: string = 'budgetList';
  public static budgetTransfer: string = 'budgetTransfer';

  //Notification
  public static budgetNotification: string = 'budgetNotification';
  public static accountNotification: string = 'accountNotification';

  //Report
  public static accountStatementReport: string = 'accountStatementReport';
  public static budgetStatementReport: string = 'budgetStatementReport';

  //Calender
  public static calender: string = 'calender';

  //Wallet
  public static createWallet: string = 'createWallet';
  public static walletTransactionList: string = 'walletTransactionList/:id';
  public static walletList: string = 'walletList';
  public static walletTransfer: string = 'walletTransfer';
  public static walletDeposit: string = 'depositWallet';

  //Bulk Transaction
  public static bulkTransaction: string = 'bulkTransaction';
}

export class AppRoute {
  public static home: string = '/home';
  public static login: string = '/login';

  //role
  public static addEditRole: string = '/addEditRole';
  public static roleList: string = '/roleList';

  //user
  public static userList: string = '/userList';
  public static addEditUser: string = '/addEditUser';

  //Account
  public static createAccount: string = '/createAccount';
  public static transactions: string = '/transactions';
  public static transfer: string = '/transfer';
  public static selfTransfer: string = '/selfTransfer';
  public static depositAmount: string = '/depositAmount';
  public static cashWithdraw: string = '/cashWithdraw';

  // Budget
  public static createBudgetAccount: string = '/createBudgetAccount';
  public static budgetList: string = '/budgetList';
  public static budgetTransfer: string = '/budgetTransfer';
  public static budgetTransactionList = (id: number): string => { return `/budgetTransactionList/${id}`; }

  //Notification
  public static budgetNotification: string = '/budgetNotification';
  public static accountNotification: string = '/accountNotification';

  //Report
  public static accountStatementReport: string = '/accountStatementReport';
  public static budgetStatementReport: string = '/budgetStatementReport';

  //Calender
  public static calender: string = '/calender';

  //Wallet
  public static createWallet: string = '/createWallet';
  public static walletTransactionList = (id: number): string => { return `/walletTransactionList/${id}`; }
  public static walletList: string = '/walletList';
  public static walletTransfer: string = '/walletTransfer';
  public static walletDeposit: string = '/depositWallet';

  //Bulk Transaction
  public static bulkTransaction: string = '/bulkTransaction';
}
export class PageTitle {
  public static home: string = 'Home';
  public static login: string = 'Login';

  //role
  public static addEditRole: string = 'Add Edit Role';
  public static roleList: string = 'Role List';

  //user
  public static userList: string = 'User List';
  public static addEditUser: string = 'Add Edit User';

  //Account
  public static createAccount: string = 'Create Account';
  public static transactions: string = 'Transactions List';
  public static transfer: string = 'Transfer Amount';
  public static selfTransfer: string = 'Self Transfer';
  public static depositAmount: string = 'Deposit Amount';
  public static cashWithdraw: string = 'Cash Withdraw';

  //Budget
  public static createBudgetAccount: string = 'Create Budget Account';
  public static budgetTransactionList: string = 'Budget Transaction List';
  public static budgetList: string = 'Budget List';
  public static budgetTransfer: string = 'Budget Transfer';

  // Notification
  public static budgetNotification: string = 'Budget Notification';
  public static accountNotification: string = 'Account Notification';

  //Report
  public static accountStatementReport: string = 'Account Statement Report';
  public static budgetStatementReport: string = 'Budget Statement Report';

  //Calender
  public static calender: string = 'Calender';

  //Wallet
  public static createWallet: string = 'Create Wallet';
  public static walletTransactionList: string = 'Wallet Transaction List';
  public static walletList: string = 'Wallet List';
  public static walletTransfer: string = 'Wallet Transfer';
  public static walletDeposit: string = 'Wallet Deposit';

  //Bulk Transaction
  public static bulkTransaction: string = 'Bulk Transaction';
}
export class ClaimKey {
  public static accessToken: string = 'accessToken';
  public static isLoggedIn: string = 'isLoggedIn';
  public static userName: string = 'UserName';
  public static idb: string = 'idb';
  public static customerId: string = 'CustomerId';
  public static staffId: string = 'StaffId';
}
