import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AppPaths, AppRoute, ClaimKey, Constant } from './constant/constant';
import { EmitterService } from './core/emitter.service';
import { IndexedDbServiceService } from './core/indexedDBService/indexed-db-service.service';
import { LocalStorageService } from './core/local-storage.service';
import { GetActionsByUserIdDto } from './dto/get-actions-by-user-id-dto';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  isLoading: boolean = false;
  isLogined: boolean = false;
  appPaths = AppPaths;
  logggedInUserName: string = 'Logged In';

  isMobileNavVisible: boolean = false;
  isCompact: boolean = false;
  //Menus = [
  //  {
  //    label: 'User',
  //    icon: 'fas fa-photo-video',
  //    isOpen: false,
  //    items: [{ menu: 'User List', path: AppRoute.userList }, { menu: 'Add New User', path: AppRoute.addEditUser }],
  //  },
  //  {
  //    label: 'Role',
  //    icon: 'fas fa-photo-video',
  //    isOpen: false,
  //    items: [{ menu: 'Role List', path: AppRoute.roleList }, { menu: 'Add New Role', path: AppRoute.addEditRole }],
  //  }
  //];

  Menus: Menu[] = []

  constructor(private emitService: EmitterService, private cdr: ChangeDetectorRef, private router: Router,
    private localStorageService: LocalStorageService, private indexedDbService: IndexedDbServiceService) {
    this.emitService.loaderEmitter.subscribe(loader => {
      this.isLoading = loader;
      this.cdr.detectChanges();
    });
    this.emitService.loginEmitter.subscribe(us => {
      this.isLogined = us;
      this.logggedInUserName = this.localStorageService.getItem(ClaimKey.userName)!;
      this.cdr.detectChanges();
      this.indexedDbService.getAllData(Constant.actionTableName).then((data: GetActionsByUserIdDto[]) => {
        console.log(data);
        this.Menus = [];
        let mainMenu = data.filter(x => x.actionPath == null);
        let subMenu = data.filter(x => x.actionPath != null);
        mainMenu.forEach((path: GetActionsByUserIdDto) => {
          let sMenu: MenuItem[] = subMenu.filter(x => x.parrentMenuId == path.actionId).map(z => ({ menu: z.actionName, path: z.actionPath ?? '' }));
          this.Menus.push({
            label: path.actionName, icon: 'fas fa-photo-video', isOpen: false,
            items: [...sMenu]
          })
        });
      });
    });
    this.logggedInUserName = this.localStorageService.getItem(ClaimKey.userName)!;

    this.indexedDbService.initDb(Constant.actionDBName, Constant.dbVersion,
      Constant.actionTablePrimaryKey, Constant.actionTableName, Constant.actionCoumns).then(() => {
        this.indexedDbService.getAllData(Constant.actionTableName).then((data: GetActionsByUserIdDto[]) => {
          this.Menus = [];
          console.log(data);
          let mainMenu = data.filter(x => x.actionPath == null);
          let subMenu = data.filter(x => x.actionPath != null);
          mainMenu.forEach((path: GetActionsByUserIdDto) => {
            let sMenu: MenuItem[] = subMenu.filter(x => x.parrentMenuId == path.actionId).map(z => ({ menu: z.actionName, path: z.actionPath ?? '' }));
            this.Menus.push({
              label: path.actionName, icon: 'fas fa-photo-video', isOpen: false,
              items: [...sMenu]
            })
          });
        });
      });
  }

  ngOnInit() {
    let logined = this.localStorageService.getItem(ClaimKey.isLoggedIn) == Constant.true;
    this.isLogined = logined;
    let currentPage = this.router.url;
    if (!this.isLogined) {
      this.router.navigate([AppPaths.login]);
    }
    //else if (currentPage && currentPage == '/' || currentPage.includes(AppPaths.login)) {
    //  this.router.navigate(['/' + AppPaths.home]);
    //}
  }

  logout() {
    this.isLogined = false;
    this.localStorageService.clear();
    this.indexedDbService.initDb(Constant.actionDBName, Constant.dbVersion,
      Constant.actionTablePrimaryKey, Constant.actionTableName, Constant.actionCoumns).then(() => {
        this.indexedDbService.deleteDatabase(Constant.actionTableName).then(() => { });
        this.indexedDbService.clearData(Constant.actionTableName).then(() => { });
      });
  }

  toggleDropdown(dropdown: any): void {
    this.Menus.forEach((d) => {
      if (d !== dropdown) {
        d.isOpen = false;
      }
    });
    dropdown.isOpen = !dropdown.isOpen;
  }

}

// Define the MenuItem interface
interface MenuItem {
  menu: string;  // The name of the menu
  path: string;  // The path associated with the menu
}

// Define the Menu interface
interface Menu {
  label: string;         // The label for the menu
  icon: string;          // The icon for the menu
  isOpen: boolean;       // Indicates if the menu is open
  items: MenuItem[];     // List of MenuItems
}
