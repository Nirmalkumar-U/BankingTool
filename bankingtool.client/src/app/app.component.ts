import { ChangeDetectorRef, Component, NgZone, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AppPaths, ClaimKey, Constant } from './constant/constant';
import { EmitterService } from './core/emitter.service';
import { IndexedDbServiceService } from './core/indexedDBService/indexed-db-service.service';
import { LocalStorageService } from './core/local-storage.service';
import { GetActionsByUserIdDto } from './dto/get-actions-by-user-id-dto';
import { faBell, faHandshake, faMoneyBill1, faArrowAltCircleRight } from '@fortawesome/free-regular-svg-icons';
import { faHouse } from '@fortawesome/free-solid-svg-icons';
import { AuthService } from './core/service/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  isLogined: boolean = false;
  appPaths = AppPaths;
  logggedInUserName: string = 'Logged In';

  isMobileNavVisible: boolean = false;
  isCompact: boolean = false;
  aBell = faBell;
  aHouse = faHouse;
  aHandshake = faHandshake;
  aMoneyBill1 = faMoneyBill1;
  aArrowAltCircleRight = faArrowAltCircleRight;

  Menus: Menu[] = [];
  notificationMenus: Menu[] = [];

  constructor(
    private emitService: EmitterService,
    private cdr: ChangeDetectorRef,
    private router: Router,
    private localStorageService: LocalStorageService,
    private indexedDbService: IndexedDbServiceService,
    private authService: AuthService,
    private ngZone: NgZone
  ) {
    this.emitService.loginEmitter.subscribe((us: boolean) => {
      this.ngZone.run(() => {
        this.isLogined = us;
        this.initIndexedDBAndLoadMenus();
        this.cdr.detectChanges();
      });
    });
  }

  ngOnInit(): void {
    this.authService.isLogined$.subscribe((state) => {
      this.isLogined = state;
    });

    const logined = this.localStorageService.getItem(ClaimKey.isLoggedIn) === Constant.true;
    this.isLogined = logined;

    if (!logined) {
      this.router.navigate([AppPaths.login]);
    }

    this.initIndexedDBAndLoadMenus();
  }

  logout(): void {
    this.authService.logout();
  }

  toggleDropdown(dropdown: Menu): void {
    this.Menus.forEach(d => {
      if (d !== dropdown) d.isOpen = false;
    });
    dropdown.isOpen = !dropdown.isOpen;
  }

  private initIndexedDBAndLoadMenus(): void {
    this.indexedDbService.initDb(
      Constant.actionDBName,
      Constant.dbVersion,
      Constant.actionTablePrimaryKey,
      Constant.actionTableName,
      Constant.actionCoumns
    ).then(() => this.loadMenus());
  }

  private loadMenus(): void {
    this.Menus = [];
    this.notificationMenus = [];
    let userName = this.localStorageService.getItem(ClaimKey.userName);
    this.logggedInUserName = userName ?? 'Logged In';
    this.indexedDbService.getAllData(Constant.actionTableName).then((data: GetActionsByUserIdDto[]) => {
      const mainMenu = data.filter(x => !x.actionPath && x.actionType === 'Menu');
      const subMenu = data.filter(x => x.actionPath);

      mainMenu.forEach((path: GetActionsByUserIdDto) => {
        const sMenu: MenuItem[] = subMenu
          .filter(x => x.parrentMenuId === path.actionId && x.actionType === 'Menu')
          .map(z => ({ menu: z.actionName, path: z.actionPath ?? '' }));

        this.Menus.push({
          label: path.actionName,
          icon: 'aMoneyBill1',
          isOpen: false,
          items: sMenu
        });
      });

      const notificationMainMenu = data.filter(x => !x.actionPath && x.actionType === 'ActionMenu');
      const notificationSubMenu = data.filter(x => x.actionPath);

      notificationMainMenu.forEach((path: GetActionsByUserIdDto) => {
        const sMenu: MenuItem[] = notificationSubMenu
          .filter(x => x.parrentMenuId === path.actionId)
          .map(z => ({ menu: z.actionName, path: z.actionPath ?? '' }));

        this.notificationMenus.push({
          label: path.actionName,
          icon: 'aMoneyBill1',
          isOpen: false,
          items: sMenu
        });
      });

      this.cdr.detectChanges();
    });
  }
}

interface MenuItem {
  menu: string;
  path: string;
}

interface Menu {
  label: string;
  icon: string;
  isOpen: boolean;
  items: MenuItem[];
}
