import { CreateIndexedDb } from "../core/indexedDBService/indexed-db-service.service";
import { DropDownDto, YesOrNoDto } from "../dto/drop-down-dto";

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
}
export class ClaimKey {
  public static accessToken: string = 'accessToken';
  public static isLoggedIn: string = 'isLoggedIn';
  public static userName: string = 'UserName';
  public static idb: string = 'idb';
  public static customerId: string = 'CustomerId';
  public static staffId: string = 'StaffId';
}
