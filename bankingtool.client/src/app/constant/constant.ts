import { CreateIndexedDb } from "../core/indexedDBService/indexed-db-service.service";

export class Constant {
  public static true: string = 'true';
  public static actionDBName: string = 'actionDBName';
  public static actionTableName: string = 'action';
  public static dbVersion: number = 1.0
  public static actionTablePrimaryKey: string = 'actionId';
  public static actionCoumns: CreateIndexedDb[] = [{ columnName: 'actionId', isUnique: true }, { columnName: 'actionName', isUnique: false }, { columnName: 'actionPath', isUnique: false },
    { columnName: 'actionType', isUnique: false }, { columnName: 'menuLevel', isUnique: false }, { columnName: 'parrentMenuId', isUnique: false }, { columnName: 'sequence', isUnique: false }];
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
}
export class ClaimKey {
  public static accessToken: string = 'accessToken';
  public static isLoggedIn: string = 'isLoggedIn';
  public static firstName: string = 'FirstName';
  public static lastName: string = 'LastName';
  public static idb: string = 'idb';
}
