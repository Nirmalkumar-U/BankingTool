"use strict";
(self["webpackChunkbankingtool_client"] = self["webpackChunkbankingtool_client"] || []).push([["main"],{

/***/ 5041:
/*!**********************************!*\
  !*** ./src/app/app.component.ts ***!
  \**********************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   "AppComponent": () => (/* binding */ AppComponent)
/* harmony export */ });
/* harmony import */ var _constant_constant__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./constant/constant */ 4219);
/* harmony import */ var _fortawesome_free_regular_svg_icons__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! @fortawesome/free-regular-svg-icons */ 2898);
/* harmony import */ var _fortawesome_free_solid_svg_icons__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! @fortawesome/free-solid-svg-icons */ 9306);
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! @angular/core */ 2560);
/* harmony import */ var _core_emitter_service__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./core/emitter.service */ 9216);
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! @angular/router */ 124);
/* harmony import */ var _core_local_storage_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./core/local-storage.service */ 357);
/* harmony import */ var _core_indexedDBService_indexed_db_service_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./core/indexedDBService/indexed-db-service.service */ 3197);
/* harmony import */ var _core_service_auth_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./core/service/auth.service */ 1782);
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! @angular/common */ 4666);
/* harmony import */ var _fortawesome_angular_fontawesome__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! @fortawesome/angular-fontawesome */ 9200);
/* harmony import */ var _component_loader_loader_component__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ./component/loader/loader.component */ 1157);
/* harmony import */ var _core_component_notification_popup_notification_popup_component__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ./core/component/notification-popup/notification-popup.component */ 8398);
/* harmony import */ var _component_note_note_component__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! ./component/note/note.component */ 1475);














const _c0 = function (a0) {
  return [a0];
};
function AppComponent_div_0_div_13_a_6_Template(rf, ctx) {
  if (rf & 1) {
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelementStart"](0, "a", 32);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵtext"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelementEnd"]();
  }
  if (rf & 2) {
    const item_r7 = ctx.$implicit;
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵproperty"]("routerLink", _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵpureFunction1"](2, _c0, item_r7.path));
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵadvance"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵtextInterpolate1"](" ", item_r7.menu, " ");
  }
}
function AppComponent_div_0_div_13_Template(rf, ctx) {
  if (rf & 1) {
    const _r9 = _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵgetCurrentView"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelementStart"](0, "div", 28)(1, "a", 29);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵlistener"]("click", function AppComponent_div_0_div_13_Template_a_click_1_listener() {
      const restoredCtx = _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵrestoreView"](_r9);
      const dropdown_r5 = restoredCtx.$implicit;
      const ctx_r8 = _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵnextContext"](2);
      return _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵresetView"](ctx_r8.toggleDropdown(dropdown_r5));
    });
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelement"](2, "fa-icon", 6);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelementStart"](3, "span");
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵtext"](4);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelementEnd"]()();
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelementStart"](5, "div", 30);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵtemplate"](6, AppComponent_div_0_div_13_a_6_Template, 2, 4, "a", 31);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelementEnd"]()();
  }
  if (rf & 2) {
    const dropdown_r5 = ctx.$implicit;
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵclassProp"]("show", dropdown_r5.isOpen);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵadvance"](2);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵproperty"]("icon", dropdown_r5.icon);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵadvance"](2);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵtextInterpolate"](dropdown_r5.label);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵadvance"](2);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵproperty"]("ngForOf", dropdown_r5.items);
  }
}
function AppComponent_div_0_ng_container_33_li_1_Template(rf, ctx) {
  if (rf & 1) {
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelementStart"](0, "li")(1, "a", 33);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵtext"](2);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelementEnd"]()();
  }
  if (rf & 2) {
    const item_r12 = ctx.$implicit;
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵadvance"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵproperty"]("routerLink", _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵpureFunction1"](2, _c0, item_r12.path));
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵadvance"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵtextInterpolate"](item_r12.menu);
  }
}
function AppComponent_div_0_ng_container_33_Template(rf, ctx) {
  if (rf & 1) {
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelementContainerStart"](0);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵtemplate"](1, AppComponent_div_0_ng_container_33_li_1_Template, 3, 4, "li", 22);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelementContainerEnd"]();
  }
  if (rf & 2) {
    const menu_r10 = ctx.$implicit;
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵadvance"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵproperty"]("ngForOf", menu_r10.items);
  }
}
function AppComponent_div_0_Template(rf, ctx) {
  if (rf & 1) {
    const _r14 = _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵgetCurrentView"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelementStart"](0, "div", 2)(1, "aside", 3)(2, "header")(3, "a", 4)(4, "span", 5);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelement"](5, "fa-icon", 6);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelementStart"](6, "span");
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵtext"](7, "Banking Tool");
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelementEnd"]()()();
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelementStart"](8, "nav", 7)(9, "a", 8);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelement"](10, "fa-icon", 6);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelementStart"](11, "span");
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵtext"](12, "Home");
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelementEnd"]()();
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵtemplate"](13, AppComponent_div_0_div_13_Template, 7, 5, "div", 9);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelement"](14, "div", 10);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelementStart"](15, "a", 11);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵlistener"]("click", function AppComponent_div_0_Template_a_click_15_listener() {
      _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵrestoreView"](_r14);
      const ctx_r13 = _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵnextContext"]();
      return _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵresetView"](ctx_r13.logout());
    });
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelement"](16, "fa-icon", 12);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelementStart"](17, "span");
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵtext"](18, "Logout");
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelementEnd"]()()()();
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelementStart"](19, "div", 13)(20, "header", 14)(21, "div", 15)(22, "button", 16);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵlistener"]("click", function AppComponent_div_0_Template_button_click_22_listener() {
      _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵrestoreView"](_r14);
      const ctx_r15 = _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵnextContext"]();
      return _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵresetView"](ctx_r15.toggleMobileNav());
    });
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelement"](23, "fa-icon", 6);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelementStart"](24, "div", 17)(25, "div", 18);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelement"](26, "fa-icon", 6);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelementStart"](27, "span");
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵtext"](28, "Updates");
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelementEnd"]()();
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelementStart"](29, "div", 19)(30, "span", 20);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelement"](31, "fa-icon", 6);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelementStart"](32, "ul", 21);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵtemplate"](33, AppComponent_div_0_ng_container_33_Template, 2, 1, "ng-container", 22);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelementEnd"]()();
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelementStart"](34, "div", 23)(35, "span", 24);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵtext"](36);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelementStart"](37, "span");
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵtext"](38);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelementEnd"]()()()()();
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelementStart"](39, "main", 25);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelement"](40, "router-outlet");
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelementStart"](41, "footer", 26)(42, "span");
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵtext"](43, "\u00A9 2026 Banking Tool");
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelementStart"](44, "span", 27);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵtext"](45, "|");
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelementStart"](46, "span");
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵtext"](47, "Secure \u2022 Smart \u2022 Simple");
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelementEnd"]()()()();
  }
  if (rf & 2) {
    const ctx_r0 = _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵnextContext"]();
    let tmp_11_0;
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵadvance"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵclassProp"]("mobile-show", ctx_r0.isMobileNavVisible);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵadvance"](4);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵproperty"]("icon", ctx_r0.aHandshake);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵadvance"](4);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵproperty"]("routerLink", _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵpureFunction1"](15, _c0, ctx_r0.appPaths.home));
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵadvance"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵproperty"]("icon", ctx_r0.aHouse);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵadvance"](3);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵproperty"]("ngForOf", ctx_r0.Menus);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵadvance"](3);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵproperty"]("icon", ctx_r0.aArrowAltCircleRight);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵadvance"](3);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵclassProp"]("dashboard-compact", ctx_r0.isCompact);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵadvance"](4);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵproperty"]("icon", ctx_r0.aBars);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵadvance"](3);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵproperty"]("icon", ctx_r0.aBell);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵadvance"](5);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵproperty"]("icon", ctx_r0.aBell);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵadvance"](2);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵproperty"]("ngForOf", ctx_r0.notificationMenus);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵadvance"](3);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵtextInterpolate"]((ctx_r0.logggedInUserName == null ? null : (tmp_11_0 = ctx_r0.logggedInUserName.charAt(0)) == null ? null : tmp_11_0.toUpperCase()) || "U");
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵadvance"](2);
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵtextInterpolate"](ctx_r0.logggedInUserName);
  }
}
function AppComponent_div_1_Template(rf, ctx) {
  if (rf & 1) {
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelementStart"](0, "div");
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelement"](1, "router-outlet");
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelementEnd"]();
  }
}
function AppComponent_app_note_4_Template(rf, ctx) {
  if (rf & 1) {
    _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelement"](0, "app-note");
  }
}
class AppComponent {
  constructor(emitService, cdr, router, localStorageService, indexedDbService, authService, ngZone) {
    this.emitService = emitService;
    this.cdr = cdr;
    this.router = router;
    this.localStorageService = localStorageService;
    this.indexedDbService = indexedDbService;
    this.authService = authService;
    this.ngZone = ngZone;
    this.isLogined = false;
    this.appPaths = _constant_constant__WEBPACK_IMPORTED_MODULE_0__.AppPaths;
    this.logggedInUserName = 'Logged In';
    this.isMobileNavVisible = false;
    this.isCompact = false;
    this.aBell = _fortawesome_free_regular_svg_icons__WEBPACK_IMPORTED_MODULE_9__.faBell;
    this.aHouse = _fortawesome_free_solid_svg_icons__WEBPACK_IMPORTED_MODULE_10__.faHouse;
    this.aBars = _fortawesome_free_solid_svg_icons__WEBPACK_IMPORTED_MODULE_10__.faBars;
    this.aHandshake = _fortawesome_free_regular_svg_icons__WEBPACK_IMPORTED_MODULE_9__.faHandshake;
    this.aMoneyBill1 = _fortawesome_free_regular_svg_icons__WEBPACK_IMPORTED_MODULE_9__.faMoneyBill1;
    this.aArrowAltCircleRight = _fortawesome_free_regular_svg_icons__WEBPACK_IMPORTED_MODULE_9__.faArrowAltCircleRight;
    this.Menus = [];
    this.notificationMenus = [];
    this.emitService.loginEmitter.subscribe(us => {
      this.ngZone.run(() => {
        this.isLogined = us;
        this.initIndexedDBAndLoadMenus();
        this.cdr.detectChanges();
      });
    });
  }
  ngOnInit() {
    this.authService.isLogined$.subscribe(state => {
      this.isLogined = state;
    });
    const logined = this.localStorageService.getItem(_constant_constant__WEBPACK_IMPORTED_MODULE_0__.ClaimKey.isLoggedIn) === _constant_constant__WEBPACK_IMPORTED_MODULE_0__.Constant["true"];
    this.isLogined = logined;
    if (!logined) {
      this.router.navigate([_constant_constant__WEBPACK_IMPORTED_MODULE_0__.AppPaths.login]);
    }
    this.initIndexedDBAndLoadMenus();
  }
  logout() {
    this.authService.logout();
  }
  toggleMobileNav() {
    this.isMobileNavVisible = !this.isMobileNavVisible;
  }
  toggleDropdown(dropdown) {
    this.Menus.forEach(d => {
      if (d !== dropdown) d.isOpen = false;
    });
    dropdown.isOpen = !dropdown.isOpen;
  }
  initIndexedDBAndLoadMenus() {
    this.indexedDbService.initDb(_constant_constant__WEBPACK_IMPORTED_MODULE_0__.Constant.actionDBName, _constant_constant__WEBPACK_IMPORTED_MODULE_0__.Constant.dbVersion, _constant_constant__WEBPACK_IMPORTED_MODULE_0__.Constant.actionTablePrimaryKey, _constant_constant__WEBPACK_IMPORTED_MODULE_0__.Constant.actionTableName, _constant_constant__WEBPACK_IMPORTED_MODULE_0__.Constant.actionCoumns).then(() => this.loadMenus());
  }
  getMenuIcon(label) {
    const normalizedLabel = label?.toLowerCase() ?? '';
    if (normalizedLabel.includes('budget')) return _fortawesome_free_solid_svg_icons__WEBPACK_IMPORTED_MODULE_10__.faPiggyBank;
    if (normalizedLabel.includes('wallet')) return _fortawesome_free_solid_svg_icons__WEBPACK_IMPORTED_MODULE_10__.faWallet;
    if (normalizedLabel.includes('account') || normalizedLabel.includes('bank')) return _fortawesome_free_solid_svg_icons__WEBPACK_IMPORTED_MODULE_10__.faLandmark;
    if (normalizedLabel.includes('transaction') || normalizedLabel.includes('transfer')) return _fortawesome_free_solid_svg_icons__WEBPACK_IMPORTED_MODULE_10__.faMoneyCheckDollar;
    if (normalizedLabel.includes('report') || normalizedLabel.includes('statement')) return _fortawesome_free_solid_svg_icons__WEBPACK_IMPORTED_MODULE_10__.faChartLine;
    if (normalizedLabel.includes('calendar') || normalizedLabel.includes('event')) return _fortawesome_free_solid_svg_icons__WEBPACK_IMPORTED_MODULE_10__.faCalendar;
    if (normalizedLabel.includes('user') || normalizedLabel.includes('role')) return _fortawesome_free_solid_svg_icons__WEBPACK_IMPORTED_MODULE_10__.faUser;
    if (normalizedLabel.includes('note')) return _fortawesome_free_solid_svg_icons__WEBPACK_IMPORTED_MODULE_10__.faClipboard;
    if (normalizedLabel.includes('mutual') || normalizedLabel.includes('fund')) return _fortawesome_free_solid_svg_icons__WEBPACK_IMPORTED_MODULE_10__.faChartPie;
    if (normalizedLabel.includes('card') || normalizedLabel.includes('credit')) return _fortawesome_free_solid_svg_icons__WEBPACK_IMPORTED_MODULE_10__.faCreditCard;
    if (normalizedLabel.includes('setting') || normalizedLabel.includes('config')) return _fortawesome_free_solid_svg_icons__WEBPACK_IMPORTED_MODULE_10__.faGear;
    if (normalizedLabel.includes('home')) return _fortawesome_free_solid_svg_icons__WEBPACK_IMPORTED_MODULE_10__.faHouse;
    if (normalizedLabel.includes('invoice') || normalizedLabel.includes('payment')) return _fortawesome_free_solid_svg_icons__WEBPACK_IMPORTED_MODULE_10__.faFileInvoiceDollar;
    if (normalizedLabel.includes('team') || normalizedLabel.includes('employee')) return _fortawesome_free_solid_svg_icons__WEBPACK_IMPORTED_MODULE_10__.faUsers;
    if (normalizedLabel.includes('task') || normalizedLabel.includes('list')) return _fortawesome_free_solid_svg_icons__WEBPACK_IMPORTED_MODULE_10__.faListCheck;
    if (normalizedLabel.includes('plan')) return _fortawesome_free_solid_svg_icons__WEBPACK_IMPORTED_MODULE_10__.faSackDollar;
    return _fortawesome_free_regular_svg_icons__WEBPACK_IMPORTED_MODULE_9__.faMoneyBill1;
  }
  loadMenus() {
    this.Menus = [];
    this.notificationMenus = [];
    let userName = this.localStorageService.getItem(_constant_constant__WEBPACK_IMPORTED_MODULE_0__.ClaimKey.userName);
    this.logggedInUserName = userName ?? 'Logged In';
    this.indexedDbService.getAllData(_constant_constant__WEBPACK_IMPORTED_MODULE_0__.Constant.actionTableName).then(data => {
      const mainMenu = data.filter(x => !x.actionPath && x.actionType === 'Menu').sort((a, b) => a.sequence - b.sequence);
      const subMenu = data.filter(x => x.actionPath);
      mainMenu.forEach(path => {
        const sMenu = subMenu.filter(x => x.parrentMenuId === path.actionId && x.actionType === 'Menu').map(z => ({
          menu: z.actionName,
          path: z.actionPath ?? ''
        }));
        this.Menus.push({
          label: path.actionName,
          icon: this.getMenuIcon(path.actionName),
          isOpen: false,
          items: sMenu
        });
      });
      const notificationMainMenu = data.filter(x => !x.actionPath && x.actionType === 'ActionMenu');
      const notificationSubMenu = data.filter(x => x.actionPath);
      notificationMainMenu.forEach(path => {
        const sMenu = notificationSubMenu.filter(x => x.parrentMenuId === path.actionId).map(z => ({
          menu: z.actionName,
          path: z.actionPath ?? ''
        }));
        this.notificationMenus.push({
          label: path.actionName,
          icon: this.getMenuIcon(path.actionName),
          isOpen: false,
          items: sMenu
        });
      });
      this.cdr.detectChanges();
    });
  }
  openNotesPopup() {
    console.log("Open Notes Popup");
  }
  static {
    this.ɵfac = function AppComponent_Factory(t) {
      return new (t || AppComponent)(_angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵdirectiveInject"](_core_emitter_service__WEBPACK_IMPORTED_MODULE_1__.EmitterService), _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵdirectiveInject"](_angular_core__WEBPACK_IMPORTED_MODULE_8__.ChangeDetectorRef), _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵdirectiveInject"](_angular_router__WEBPACK_IMPORTED_MODULE_11__.Router), _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵdirectiveInject"](_core_local_storage_service__WEBPACK_IMPORTED_MODULE_2__.LocalStorageService), _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵdirectiveInject"](_core_indexedDBService_indexed_db_service_service__WEBPACK_IMPORTED_MODULE_3__.IndexedDbServiceService), _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵdirectiveInject"](_core_service_auth_service__WEBPACK_IMPORTED_MODULE_4__.AuthService), _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵdirectiveInject"](_angular_core__WEBPACK_IMPORTED_MODULE_8__.NgZone));
    };
  }
  static {
    this.ɵcmp = /*@__PURE__*/_angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵdefineComponent"]({
      type: AppComponent,
      selectors: [["app-root"]],
      decls: 5,
      vars: 3,
      consts: [["class", "app-root-shell", 4, "ngIf"], [4, "ngIf"], [1, "app-root-shell"], [1, "dashboard-nav"], ["href", "#", 1, "brand-logo"], [1, "brand-mark"], [3, "icon"], ["aria-label", "Main navigation", 1, "dashboard-nav-list"], [1, "dashboard-nav-item", 3, "routerLink"], ["class", "dashboard-nav-dropdown", 3, "show", 4, "ngFor", "ngForOf"], [1, "nav-item-divider"], ["href", "#", 1, "dashboard-nav-item", "logout-item", 3, "click"], ["size", "lg", 3, "icon"], [1, "dashboard-app"], [1, "dashboard-toolbar"], [1, "toolbar-row"], ["type", "button", "aria-label", "Toggle menu", 1, "mobile-menu-toggle", 3, "click"], [1, "toolbar-actions"], [1, "toolbar-badge"], [1, "dropdown"], ["type", "button", "id", "dropdownMenuButton1", "data-bs-toggle", "dropdown", "aria-expanded", "false", 1, "btn", "btn-light", "dropdown-toggle", "topbar-icon-btn"], ["aria-labelledby", "dropdownMenuButton1", 1, "dropdown-menu", "dropdown-menu-end", "shadow-sm", "border-0"], [4, "ngFor", "ngForOf"], [1, "user-chip"], [1, "user-avatar"], [1, "dashboard-content"], [1, "dashboard-footer"], [1, "footer-divider"], [1, "dashboard-nav-dropdown"], [1, "dashboard-nav-item", "dashboard-nav-dropdown-toggle", 3, "click"], [1, "dashboard-nav-dropdown-menu"], ["class", "dashboard-nav-dropdown-item", 3, "routerLink", 4, "ngFor", "ngForOf"], [1, "dashboard-nav-dropdown-item", 3, "routerLink"], [1, "dropdown-item", 3, "routerLink"]],
      template: function AppComponent_Template(rf, ctx) {
        if (rf & 1) {
          _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵtemplate"](0, AppComponent_div_0_Template, 48, 17, "div", 0);
          _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵtemplate"](1, AppComponent_div_1_Template, 2, 0, "div", 1);
          _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵelement"](2, "app-loader")(3, "app-notification-popup");
          _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵtemplate"](4, AppComponent_app_note_4_Template, 1, 0, "app-note", 1);
        }
        if (rf & 2) {
          _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵproperty"]("ngIf", ctx.isLogined);
          _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵadvance"](1);
          _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵproperty"]("ngIf", !ctx.isLogined);
          _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵadvance"](3);
          _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵproperty"]("ngIf", ctx.isLogined);
        }
      },
      dependencies: [_angular_common__WEBPACK_IMPORTED_MODULE_12__.NgForOf, _angular_common__WEBPACK_IMPORTED_MODULE_12__.NgIf, _angular_router__WEBPACK_IMPORTED_MODULE_11__.RouterOutlet, _angular_router__WEBPACK_IMPORTED_MODULE_11__.RouterLink, _fortawesome_angular_fontawesome__WEBPACK_IMPORTED_MODULE_13__.FaIconComponent, _component_loader_loader_component__WEBPACK_IMPORTED_MODULE_5__.LoaderComponent, _core_component_notification_popup_notification_popup_component__WEBPACK_IMPORTED_MODULE_6__.NotificationPopupComponent, _component_note_note_component__WEBPACK_IMPORTED_MODULE_7__.NoteComponent],
      styles: [".app-root-shell[_ngcontent-%COMP%] {\r\n  display: flex;\r\n  min-height: 100vh;\r\n  background: linear-gradient(180deg, rgba(237, 244, 255, 0.9), rgba(243, 247, 255, 0.96));\r\n}\r\n\r\n[_nghost-%COMP%] {\r\n  display: block;\r\n  width: 100%;\r\n  min-height: 100vh;\r\n}\r\n\r\nth[_ngcontent-%COMP%], td[_ngcontent-%COMP%] {\r\n  padding-left: 1rem;\r\n  padding-right: 1rem;\r\n}\r\n\r\ntable[_ngcontent-%COMP%] {\r\n  margin: 0 auto;\r\n}\r\n\r\n.dashboard-nav[_ngcontent-%COMP%] {\r\n  min-width: 250px;\r\n  width: 250px;\r\n  position: sticky;\r\n  top: 0;\r\n  height: 100vh;\r\n  overflow: auto;\r\n  background: linear-gradient(180deg, #1f2a4f 0%, #2d2a6e 38%, #443ea2 100%);\r\n  box-shadow: 18px 0 40px rgba(45, 42, 110, 0.18);\r\n  border-right: 1px solid rgba(255, 255, 255, 0.08);\r\n}\r\n\r\n.dashboard-nav[_ngcontent-%COMP%]   header[_ngcontent-%COMP%] {\r\n  min-height: 96px;\r\n  padding: 20px 22px 16px;\r\n  display: flex;\r\n  align-items: center;\r\n  justify-content: center;\r\n  border-bottom: 1px solid rgba(255, 255, 255, 0.08);\r\n  background: rgba(255, 255, 255, 0.02);\r\n}\r\n\r\n.brand-logo[_ngcontent-%COMP%] {\r\n  display: inline-flex;\r\n  align-items: center;\r\n  gap: 0.8rem;\r\n  font-size: 1.35rem;\r\n  font-weight: 700;\r\n  letter-spacing: 0.02em;\r\n  color: #fff;\r\n}\r\n\r\n.brand-mark[_ngcontent-%COMP%] {\r\n  width: 42px;\r\n  height: 42px;\r\n  display: inline-flex;\r\n  align-items: center;\r\n  justify-content: center;\r\n  border-radius: 14px;\r\n  background: rgba(255, 255, 255, 0.12);\r\n  box-shadow: inset 0 1px 0 rgba(255, 255, 255, 0.16);\r\n}\r\n\r\n.brand-mark[_ngcontent-%COMP%]   fa-icon[_ngcontent-%COMP%] {\r\n  color: #e7ebff;\r\n  font-size: 1.15rem;\r\n}\r\n\r\n.dashboard-nav-list[_ngcontent-%COMP%] {\r\n  display: flex;\r\n  flex-direction: column;\r\n  padding: 1.15rem 0.8rem 1.25rem;\r\n  gap: 0.2rem;\r\n}\r\n\r\n.dashboard-nav-item[_ngcontent-%COMP%] {\r\n  min-height: 52px;\r\n  padding: 0.8rem 1rem 0.8rem 1.1rem;\r\n  display: flex;\r\n  align-items: center;\r\n  gap: 0.8rem;\r\n  border-radius: 0.9rem;\r\n  color: rgba(255, 255, 255, 0.9);\r\n  font-weight: 600;\r\n  letter-spacing: 0.01em;\r\n  text-decoration: none;\r\n}\r\n\r\n.dashboard-nav-item[_ngcontent-%COMP%]:hover, .dashboard-nav-item[_ngcontent-%COMP%]:focus {\r\n  background: rgba(255, 255, 255, 0.08);\r\n  text-decoration: none;\r\n  transform: translateX(2px);\r\n}\r\n\r\n.dashboard-nav-item[_ngcontent-%COMP%]   fa-icon[_ngcontent-%COMP%] {\r\n  width: 18px;\r\n  text-align: center;\r\n}\r\n\r\n.dashboard-nav-dropdown[_ngcontent-%COMP%] {\r\n  display: flex;\r\n  flex-direction: column;\r\n  gap: 0.18rem;\r\n}\r\n\r\n.dashboard-nav-dropdown.show[_ngcontent-%COMP%] {\r\n  background: rgba(255, 255, 255, 0.03);\r\n  border-radius: 0.9rem;\r\n  padding: 0.2rem;\r\n}\r\n\r\n.dashboard-nav-dropdown-toggle[_ngcontent-%COMP%] {\r\n  position: relative;\r\n  cursor: pointer;\r\n}\r\n\r\n.dashboard-nav-dropdown-toggle[_ngcontent-%COMP%]::after {\r\n  content: \"\";\r\n  margin-left: auto;\r\n  display: inline-block;\r\n  width: 0;\r\n  height: 0;\r\n  border-left: 5px solid transparent;\r\n  border-right: 5px solid transparent;\r\n  border-top: 5px solid rgba(255, 255, 255, 0.7);\r\n  transform: rotate(90deg);\r\n}\r\n\r\n.dashboard-nav-dropdown.show[_ngcontent-%COMP%]    > .dashboard-nav-dropdown-toggle[_ngcontent-%COMP%]::after {\r\n  transform: none;\r\n}\r\n\r\n.dashboard-nav-dropdown-menu[_ngcontent-%COMP%] {\r\n  display: none;\r\n  flex-direction: column;\r\n  gap: 0.2rem;\r\n  padding: 0 0.2rem 0.15rem;\r\n}\r\n\r\n.dashboard-nav-dropdown.show[_ngcontent-%COMP%]    > .dashboard-nav-dropdown-menu[_ngcontent-%COMP%] {\r\n  display: flex;\r\n}\r\n\r\n.dashboard-nav-dropdown-item[_ngcontent-%COMP%] {\r\n  min-height: 38px;\r\n  padding: 0.7rem 0.8rem 0.7rem 1.15rem;\r\n  display: flex;\r\n  align-items: center;\r\n  border-radius: 0.7rem;\r\n  color: rgba(255, 255, 255, 0.82);\r\n  background: rgba(255, 255, 255, 0.02);\r\n  text-decoration: none;\r\n}\r\n\r\n.dashboard-nav-dropdown-item[_ngcontent-%COMP%]:hover, .dashboard-nav-dropdown-item[_ngcontent-%COMP%]:focus {\r\n  background: rgba(255, 255, 255, 0.08);\r\n  color: #fff;\r\n  text-decoration: none;\r\n}\r\n\r\n.logout-item[_ngcontent-%COMP%] {\r\n  margin-top: 0.25rem;\r\n}\r\n\r\n.dashboard-app[_ngcontent-%COMP%] {\r\n  display: flex;\r\n  flex-direction: column;\r\n  flex: 1;\r\n  min-width: 0;\r\n}\r\n\r\n.dashboard-toolbar[_ngcontent-%COMP%] {\r\n  min-height: 86px;\r\n  background: rgba(255, 255, 255, 0.7);\r\n  backdrop-filter: blur(14px);\r\n  -webkit-backdrop-filter: blur(14px);\r\n  display: flex;\r\n  align-items: center;\r\n  padding: 12px 26px;\r\n  position: sticky;\r\n  top: 0;\r\n  z-index: 10;\r\n  border-bottom: 1px solid rgba(148, 163, 184, 0.18);\r\n  box-shadow: 0 10px 30px rgba(15, 23, 42, 0.04);\r\n}\r\n\r\n.toolbar-row[_ngcontent-%COMP%] {\r\n  width: 100%;\r\n  display: flex;\r\n  align-items: center;\r\n  justify-content: space-between;\r\n  gap: 0.75rem;\r\n}\r\n\r\n.mobile-menu-toggle[_ngcontent-%COMP%] {\r\n  display: none;\r\n  width: 42px;\r\n  height: 42px;\r\n  border: 0;\r\n  border-radius: 12px;\r\n  background: rgba(68, 62, 162, 0.08);\r\n  color: #2d2a6e;\r\n  cursor: pointer;\r\n}\r\n\r\n.toolbar-actions[_ngcontent-%COMP%] {\r\n  margin-left: auto;\r\n  display: flex;\r\n  align-items: center;\r\n  gap: 0.9rem;\r\n}\r\n\r\n.toolbar-badge[_ngcontent-%COMP%] {\r\n  display: inline-flex;\r\n  align-items: center;\r\n  gap: 0.5rem;\r\n  padding: 0.58rem 0.9rem;\r\n  border-radius: 999px;\r\n  background: rgba(45, 93, 246, 0.08);\r\n  color: #2d5df6;\r\n  font-size: 0.8rem;\r\n  font-weight: 700;\r\n  letter-spacing: 0.04em;\r\n}\r\n\r\n.topbar-icon-btn[_ngcontent-%COMP%] {\r\n  width: 42px;\r\n  height: 42px;\r\n  border: none;\r\n  border-radius: 12px;\r\n  background: rgba(68, 62, 162, 0.08);\r\n  color: #2d2a6e;\r\n  display: inline-flex;\r\n  align-items: center;\r\n  justify-content: center;\r\n  box-shadow: none;\r\n}\r\n\r\n.topbar-icon-btn[_ngcontent-%COMP%]:hover, .topbar-icon-btn[_ngcontent-%COMP%]:focus {\r\n  background: rgba(68, 62, 162, 0.15);\r\n}\r\n\r\n.user-chip[_ngcontent-%COMP%] {\r\n  display: inline-flex;\r\n  align-items: center;\r\n  gap: 0.75rem;\r\n  padding: 0.45rem 0.85rem 0.45rem 0.45rem;\r\n  border-radius: 999px;\r\n  background: rgba(15, 23, 42, 0.04);\r\n  border: 1px solid rgba(148, 163, 184, 0.2);\r\n  color: #0f172a;\r\n  font-weight: 600;\r\n}\r\n\r\n.user-avatar[_ngcontent-%COMP%] {\r\n  width: 2rem;\r\n  height: 2rem;\r\n  border-radius: 50%;\r\n  display: inline-flex;\r\n  align-items: center;\r\n  justify-content: center;\r\n  background: linear-gradient(135deg, #443ea2, #4f7cff);\r\n  color: #fff;\r\n  font-size: 0.8rem;\r\n  font-weight: 700;\r\n}\r\n\r\n.dashboard-content[_ngcontent-%COMP%] {\r\n  flex: 1;\r\n  padding: 24px 22px 10px;\r\n  background: linear-gradient(180deg, rgba(255,255,255,0.08), rgba(233, 237, 244, 0.72));\r\n}\r\n\r\n.dashboard-footer[_ngcontent-%COMP%] {\r\n  display: flex;\r\n  justify-content: center;\r\n  align-items: center;\r\n  gap: 0.75rem;\r\n  min-height: 62px;\r\n  padding: 0.9rem 1.2rem 1.15rem;\r\n  background: rgba(255, 255, 255, 0.62);\r\n  border-top: 1px solid rgba(148, 163, 184, 0.18);\r\n  color: var(--text-soft);\r\n  font-size: 0.82rem;\r\n  letter-spacing: 0.02em;\r\n}\r\n\r\n.footer-divider[_ngcontent-%COMP%] {\r\n  opacity: 0.5;\r\n}\r\n\r\n@media (max-width: 992px) {\r\n  .dashboard-nav[_ngcontent-%COMP%] {\r\n    position: fixed;\r\n    inset: 0 auto 0 0;\r\n    width: min(280px, 85vw);\r\n    z-index: 1000;\r\n    transform: translateX(-105%);\r\n    transition: transform 0.25s ease;\r\n  }\r\n\r\n  .dashboard-nav.mobile-show[_ngcontent-%COMP%] {\r\n    transform: translateX(0);\r\n    display: block;\r\n  }\r\n\r\n  .mobile-menu-toggle[_ngcontent-%COMP%] {\r\n    display: inline-flex;\r\n    align-items: center;\r\n    justify-content: center;\r\n  }\r\n}\r\n\r\n@media (max-width: 768px) {\r\n  .dashboard-toolbar[_ngcontent-%COMP%] {\r\n    padding: 10px 14px;\r\n  }\r\n\r\n  .toolbar-row[_ngcontent-%COMP%] {\r\n    gap: 0.5rem;\r\n  }\r\n\r\n  .toolbar-actions[_ngcontent-%COMP%] {\r\n    gap: 0.5rem;\r\n    flex-wrap: wrap;\r\n    justify-content: flex-end;\r\n  }\r\n\r\n  .dashboard-content[_ngcontent-%COMP%] {\r\n    padding: 15px 12px 6px;\r\n  }\r\n\r\n  .dashboard-footer[_ngcontent-%COMP%] {\r\n    flex-direction: column;\r\n    gap: 0.2rem;\r\n    padding-bottom: 1.1rem;\r\n  }\r\n}\r\n\r\n@media (max-width: 576px) {\r\n  .user-chip[_ngcontent-%COMP%] {\r\n    width: 100%;\r\n    justify-content: center;\r\n  }\r\n\r\n  .toolbar-actions[_ngcontent-%COMP%] {\r\n    width: 100%;\r\n    justify-content: space-between;\r\n  }\r\n}\r\n\r\n@media (min-width: 992px) {\r\n  .dashboard-compact[_ngcontent-%COMP%]   .dashboard-nav[_ngcontent-%COMP%] {\r\n    display: none;\r\n  }\r\n  .dashboard-compact[_ngcontent-%COMP%]   .dashboard-toolbar[_ngcontent-%COMP%] {\r\n    left: 0;\r\n  }\r\n}\r\n\r\n@media (max-width: 992px) {\r\n  .dashboard-app[_ngcontent-%COMP%] {\r\n    width: 100%;\r\n  }\r\n}\r\n\r\n.dashboard-content[_ngcontent-%COMP%]   .card[_ngcontent-%COMP%], .dashboard-content[_ngcontent-%COMP%]   .panel[_ngcontent-%COMP%], .dashboard-content[_ngcontent-%COMP%]   .container-fluid[_ngcontent-%COMP%], .dashboard-content[_ngcontent-%COMP%]   .row[_ngcontent-%COMP%] {\r\n  border-radius: 20px;\r\n}\n/*# sourceMappingURL=data:application/json;charset=utf-8;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbIndlYnBhY2s6Ly8uL3NyYy9hcHAvYXBwLmNvbXBvbmVudC5jc3MiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IkFBQUE7RUFDRSxhQUFhO0VBQ2IsaUJBQWlCO0VBQ2pCLHdGQUF3RjtBQUMxRjs7QUFFQTtFQUNFLGNBQWM7RUFDZCxXQUFXO0VBQ1gsaUJBQWlCO0FBQ25COztBQUVBO0VBQ0Usa0JBQWtCO0VBQ2xCLG1CQUFtQjtBQUNyQjs7QUFFQTtFQUNFLGNBQWM7QUFDaEI7O0FBRUE7RUFDRSxnQkFBZ0I7RUFDaEIsWUFBWTtFQUNaLGdCQUFnQjtFQUNoQixNQUFNO0VBQ04sYUFBYTtFQUNiLGNBQWM7RUFDZCwwRUFBMEU7RUFDMUUsK0NBQStDO0VBQy9DLGlEQUFpRDtBQUNuRDs7QUFFQTtFQUNFLGdCQUFnQjtFQUNoQix1QkFBdUI7RUFDdkIsYUFBYTtFQUNiLG1CQUFtQjtFQUNuQix1QkFBdUI7RUFDdkIsa0RBQWtEO0VBQ2xELHFDQUFxQztBQUN2Qzs7QUFFQTtFQUNFLG9CQUFvQjtFQUNwQixtQkFBbUI7RUFDbkIsV0FBVztFQUNYLGtCQUFrQjtFQUNsQixnQkFBZ0I7RUFDaEIsc0JBQXNCO0VBQ3RCLFdBQVc7QUFDYjs7QUFFQTtFQUNFLFdBQVc7RUFDWCxZQUFZO0VBQ1osb0JBQW9CO0VBQ3BCLG1CQUFtQjtFQUNuQix1QkFBdUI7RUFDdkIsbUJBQW1CO0VBQ25CLHFDQUFxQztFQUNyQyxtREFBbUQ7QUFDckQ7O0FBRUE7RUFDRSxjQUFjO0VBQ2Qsa0JBQWtCO0FBQ3BCOztBQUVBO0VBQ0UsYUFBYTtFQUNiLHNCQUFzQjtFQUN0QiwrQkFBK0I7RUFDL0IsV0FBVztBQUNiOztBQUVBO0VBQ0UsZ0JBQWdCO0VBQ2hCLGtDQUFrQztFQUNsQyxhQUFhO0VBQ2IsbUJBQW1CO0VBQ25CLFdBQVc7RUFDWCxxQkFBcUI7RUFDckIsK0JBQStCO0VBQy9CLGdCQUFnQjtFQUNoQixzQkFBc0I7RUFDdEIscUJBQXFCO0FBQ3ZCOztBQUVBOztFQUVFLHFDQUFxQztFQUNyQyxxQkFBcUI7RUFDckIsMEJBQTBCO0FBQzVCOztBQUVBO0VBQ0UsV0FBVztFQUNYLGtCQUFrQjtBQUNwQjs7QUFFQTtFQUNFLGFBQWE7RUFDYixzQkFBc0I7RUFDdEIsWUFBWTtBQUNkOztBQUVBO0VBQ0UscUNBQXFDO0VBQ3JDLHFCQUFxQjtFQUNyQixlQUFlO0FBQ2pCOztBQUVBO0VBQ0Usa0JBQWtCO0VBQ2xCLGVBQWU7QUFDakI7O0FBRUE7RUFDRSxXQUFXO0VBQ1gsaUJBQWlCO0VBQ2pCLHFCQUFxQjtFQUNyQixRQUFRO0VBQ1IsU0FBUztFQUNULGtDQUFrQztFQUNsQyxtQ0FBbUM7RUFDbkMsOENBQThDO0VBQzlDLHdCQUF3QjtBQUMxQjs7QUFFQTtFQUNFLGVBQWU7QUFDakI7O0FBRUE7RUFDRSxhQUFhO0VBQ2Isc0JBQXNCO0VBQ3RCLFdBQVc7RUFDWCx5QkFBeUI7QUFDM0I7O0FBRUE7RUFDRSxhQUFhO0FBQ2Y7O0FBRUE7RUFDRSxnQkFBZ0I7RUFDaEIscUNBQXFDO0VBQ3JDLGFBQWE7RUFDYixtQkFBbUI7RUFDbkIscUJBQXFCO0VBQ3JCLGdDQUFnQztFQUNoQyxxQ0FBcUM7RUFDckMscUJBQXFCO0FBQ3ZCOztBQUVBOztFQUVFLHFDQUFxQztFQUNyQyxXQUFXO0VBQ1gscUJBQXFCO0FBQ3ZCOztBQUVBO0VBQ0UsbUJBQW1CO0FBQ3JCOztBQUVBO0VBQ0UsYUFBYTtFQUNiLHNCQUFzQjtFQUN0QixPQUFPO0VBQ1AsWUFBWTtBQUNkOztBQUVBO0VBQ0UsZ0JBQWdCO0VBQ2hCLG9DQUFvQztFQUNwQywyQkFBMkI7RUFDM0IsbUNBQW1DO0VBQ25DLGFBQWE7RUFDYixtQkFBbUI7RUFDbkIsa0JBQWtCO0VBQ2xCLGdCQUFnQjtFQUNoQixNQUFNO0VBQ04sV0FBVztFQUNYLGtEQUFrRDtFQUNsRCw4Q0FBOEM7QUFDaEQ7O0FBRUE7RUFDRSxXQUFXO0VBQ1gsYUFBYTtFQUNiLG1CQUFtQjtFQUNuQiw4QkFBOEI7RUFDOUIsWUFBWTtBQUNkOztBQUVBO0VBQ0UsYUFBYTtFQUNiLFdBQVc7RUFDWCxZQUFZO0VBQ1osU0FBUztFQUNULG1CQUFtQjtFQUNuQixtQ0FBbUM7RUFDbkMsY0FBYztFQUNkLGVBQWU7QUFDakI7O0FBRUE7RUFDRSxpQkFBaUI7RUFDakIsYUFBYTtFQUNiLG1CQUFtQjtFQUNuQixXQUFXO0FBQ2I7O0FBRUE7RUFDRSxvQkFBb0I7RUFDcEIsbUJBQW1CO0VBQ25CLFdBQVc7RUFDWCx1QkFBdUI7RUFDdkIsb0JBQW9CO0VBQ3BCLG1DQUFtQztFQUNuQyxjQUFjO0VBQ2QsaUJBQWlCO0VBQ2pCLGdCQUFnQjtFQUNoQixzQkFBc0I7QUFDeEI7O0FBRUE7RUFDRSxXQUFXO0VBQ1gsWUFBWTtFQUNaLFlBQVk7RUFDWixtQkFBbUI7RUFDbkIsbUNBQW1DO0VBQ25DLGNBQWM7RUFDZCxvQkFBb0I7RUFDcEIsbUJBQW1CO0VBQ25CLHVCQUF1QjtFQUN2QixnQkFBZ0I7QUFDbEI7O0FBRUE7O0VBRUUsbUNBQW1DO0FBQ3JDOztBQUVBO0VBQ0Usb0JBQW9CO0VBQ3BCLG1CQUFtQjtFQUNuQixZQUFZO0VBQ1osd0NBQXdDO0VBQ3hDLG9CQUFvQjtFQUNwQixrQ0FBa0M7RUFDbEMsMENBQTBDO0VBQzFDLGNBQWM7RUFDZCxnQkFBZ0I7QUFDbEI7O0FBRUE7RUFDRSxXQUFXO0VBQ1gsWUFBWTtFQUNaLGtCQUFrQjtFQUNsQixvQkFBb0I7RUFDcEIsbUJBQW1CO0VBQ25CLHVCQUF1QjtFQUN2QixxREFBcUQ7RUFDckQsV0FBVztFQUNYLGlCQUFpQjtFQUNqQixnQkFBZ0I7QUFDbEI7O0FBRUE7RUFDRSxPQUFPO0VBQ1AsdUJBQXVCO0VBQ3ZCLHNGQUFzRjtBQUN4Rjs7QUFFQTtFQUNFLGFBQWE7RUFDYix1QkFBdUI7RUFDdkIsbUJBQW1CO0VBQ25CLFlBQVk7RUFDWixnQkFBZ0I7RUFDaEIsOEJBQThCO0VBQzlCLHFDQUFxQztFQUNyQywrQ0FBK0M7RUFDL0MsdUJBQXVCO0VBQ3ZCLGtCQUFrQjtFQUNsQixzQkFBc0I7QUFDeEI7O0FBRUE7RUFDRSxZQUFZO0FBQ2Q7O0FBRUE7RUFDRTtJQUNFLGVBQWU7SUFDZixpQkFBaUI7SUFDakIsdUJBQXVCO0lBQ3ZCLGFBQWE7SUFDYiw0QkFBNEI7SUFDNUIsZ0NBQWdDO0VBQ2xDOztFQUVBO0lBQ0Usd0JBQXdCO0lBQ3hCLGNBQWM7RUFDaEI7O0VBRUE7SUFDRSxvQkFBb0I7SUFDcEIsbUJBQW1CO0lBQ25CLHVCQUF1QjtFQUN6QjtBQUNGOztBQUVBO0VBQ0U7SUFDRSxrQkFBa0I7RUFDcEI7O0VBRUE7SUFDRSxXQUFXO0VBQ2I7O0VBRUE7SUFDRSxXQUFXO0lBQ1gsZUFBZTtJQUNmLHlCQUF5QjtFQUMzQjs7RUFFQTtJQUNFLHNCQUFzQjtFQUN4Qjs7RUFFQTtJQUNFLHNCQUFzQjtJQUN0QixXQUFXO0lBQ1gsc0JBQXNCO0VBQ3hCO0FBQ0Y7O0FBRUE7RUFDRTtJQUNFLFdBQVc7SUFDWCx1QkFBdUI7RUFDekI7O0VBRUE7SUFDRSxXQUFXO0lBQ1gsOEJBQThCO0VBQ2hDO0FBQ0Y7O0FBRUE7RUFDRTtJQUNFLGFBQWE7RUFDZjtFQUNBO0lBQ0UsT0FBTztFQUNUO0FBQ0Y7O0FBRUE7RUFDRTtJQUNFLFdBQVc7RUFDYjtBQUNGOztBQUVBOzs7O0VBSUUsbUJBQW1CO0FBQ3JCIiwic291cmNlc0NvbnRlbnQiOlsiLmFwcC1yb290LXNoZWxsIHtcclxuICBkaXNwbGF5OiBmbGV4O1xyXG4gIG1pbi1oZWlnaHQ6IDEwMHZoO1xyXG4gIGJhY2tncm91bmQ6IGxpbmVhci1ncmFkaWVudCgxODBkZWcsIHJnYmEoMjM3LCAyNDQsIDI1NSwgMC45KSwgcmdiYSgyNDMsIDI0NywgMjU1LCAwLjk2KSk7XHJcbn1cclxuXHJcbjpob3N0IHtcclxuICBkaXNwbGF5OiBibG9jaztcclxuICB3aWR0aDogMTAwJTtcclxuICBtaW4taGVpZ2h0OiAxMDB2aDtcclxufVxyXG5cclxudGgsIHRkIHtcclxuICBwYWRkaW5nLWxlZnQ6IDFyZW07XHJcbiAgcGFkZGluZy1yaWdodDogMXJlbTtcclxufVxyXG5cclxudGFibGUge1xyXG4gIG1hcmdpbjogMCBhdXRvO1xyXG59XHJcblxyXG4uZGFzaGJvYXJkLW5hdiB7XHJcbiAgbWluLXdpZHRoOiAyNTBweDtcclxuICB3aWR0aDogMjUwcHg7XHJcbiAgcG9zaXRpb246IHN0aWNreTtcclxuICB0b3A6IDA7XHJcbiAgaGVpZ2h0OiAxMDB2aDtcclxuICBvdmVyZmxvdzogYXV0bztcclxuICBiYWNrZ3JvdW5kOiBsaW5lYXItZ3JhZGllbnQoMTgwZGVnLCAjMWYyYTRmIDAlLCAjMmQyYTZlIDM4JSwgIzQ0M2VhMiAxMDAlKTtcclxuICBib3gtc2hhZG93OiAxOHB4IDAgNDBweCByZ2JhKDQ1LCA0MiwgMTEwLCAwLjE4KTtcclxuICBib3JkZXItcmlnaHQ6IDFweCBzb2xpZCByZ2JhKDI1NSwgMjU1LCAyNTUsIDAuMDgpO1xyXG59XHJcblxyXG4uZGFzaGJvYXJkLW5hdiBoZWFkZXIge1xyXG4gIG1pbi1oZWlnaHQ6IDk2cHg7XHJcbiAgcGFkZGluZzogMjBweCAyMnB4IDE2cHg7XHJcbiAgZGlzcGxheTogZmxleDtcclxuICBhbGlnbi1pdGVtczogY2VudGVyO1xyXG4gIGp1c3RpZnktY29udGVudDogY2VudGVyO1xyXG4gIGJvcmRlci1ib3R0b206IDFweCBzb2xpZCByZ2JhKDI1NSwgMjU1LCAyNTUsIDAuMDgpO1xyXG4gIGJhY2tncm91bmQ6IHJnYmEoMjU1LCAyNTUsIDI1NSwgMC4wMik7XHJcbn1cclxuXHJcbi5icmFuZC1sb2dvIHtcclxuICBkaXNwbGF5OiBpbmxpbmUtZmxleDtcclxuICBhbGlnbi1pdGVtczogY2VudGVyO1xyXG4gIGdhcDogMC44cmVtO1xyXG4gIGZvbnQtc2l6ZTogMS4zNXJlbTtcclxuICBmb250LXdlaWdodDogNzAwO1xyXG4gIGxldHRlci1zcGFjaW5nOiAwLjAyZW07XHJcbiAgY29sb3I6ICNmZmY7XHJcbn1cclxuXHJcbi5icmFuZC1tYXJrIHtcclxuICB3aWR0aDogNDJweDtcclxuICBoZWlnaHQ6IDQycHg7XHJcbiAgZGlzcGxheTogaW5saW5lLWZsZXg7XHJcbiAgYWxpZ24taXRlbXM6IGNlbnRlcjtcclxuICBqdXN0aWZ5LWNvbnRlbnQ6IGNlbnRlcjtcclxuICBib3JkZXItcmFkaXVzOiAxNHB4O1xyXG4gIGJhY2tncm91bmQ6IHJnYmEoMjU1LCAyNTUsIDI1NSwgMC4xMik7XHJcbiAgYm94LXNoYWRvdzogaW5zZXQgMCAxcHggMCByZ2JhKDI1NSwgMjU1LCAyNTUsIDAuMTYpO1xyXG59XHJcblxyXG4uYnJhbmQtbWFyayBmYS1pY29uIHtcclxuICBjb2xvcjogI2U3ZWJmZjtcclxuICBmb250LXNpemU6IDEuMTVyZW07XHJcbn1cclxuXHJcbi5kYXNoYm9hcmQtbmF2LWxpc3Qge1xyXG4gIGRpc3BsYXk6IGZsZXg7XHJcbiAgZmxleC1kaXJlY3Rpb246IGNvbHVtbjtcclxuICBwYWRkaW5nOiAxLjE1cmVtIDAuOHJlbSAxLjI1cmVtO1xyXG4gIGdhcDogMC4ycmVtO1xyXG59XHJcblxyXG4uZGFzaGJvYXJkLW5hdi1pdGVtIHtcclxuICBtaW4taGVpZ2h0OiA1MnB4O1xyXG4gIHBhZGRpbmc6IDAuOHJlbSAxcmVtIDAuOHJlbSAxLjFyZW07XHJcbiAgZGlzcGxheTogZmxleDtcclxuICBhbGlnbi1pdGVtczogY2VudGVyO1xyXG4gIGdhcDogMC44cmVtO1xyXG4gIGJvcmRlci1yYWRpdXM6IDAuOXJlbTtcclxuICBjb2xvcjogcmdiYSgyNTUsIDI1NSwgMjU1LCAwLjkpO1xyXG4gIGZvbnQtd2VpZ2h0OiA2MDA7XHJcbiAgbGV0dGVyLXNwYWNpbmc6IDAuMDFlbTtcclxuICB0ZXh0LWRlY29yYXRpb246IG5vbmU7XHJcbn1cclxuXHJcbi5kYXNoYm9hcmQtbmF2LWl0ZW06aG92ZXIsXHJcbi5kYXNoYm9hcmQtbmF2LWl0ZW06Zm9jdXMge1xyXG4gIGJhY2tncm91bmQ6IHJnYmEoMjU1LCAyNTUsIDI1NSwgMC4wOCk7XHJcbiAgdGV4dC1kZWNvcmF0aW9uOiBub25lO1xyXG4gIHRyYW5zZm9ybTogdHJhbnNsYXRlWCgycHgpO1xyXG59XHJcblxyXG4uZGFzaGJvYXJkLW5hdi1pdGVtIGZhLWljb24ge1xyXG4gIHdpZHRoOiAxOHB4O1xyXG4gIHRleHQtYWxpZ246IGNlbnRlcjtcclxufVxyXG5cclxuLmRhc2hib2FyZC1uYXYtZHJvcGRvd24ge1xyXG4gIGRpc3BsYXk6IGZsZXg7XHJcbiAgZmxleC1kaXJlY3Rpb246IGNvbHVtbjtcclxuICBnYXA6IDAuMThyZW07XHJcbn1cclxuXHJcbi5kYXNoYm9hcmQtbmF2LWRyb3Bkb3duLnNob3cge1xyXG4gIGJhY2tncm91bmQ6IHJnYmEoMjU1LCAyNTUsIDI1NSwgMC4wMyk7XHJcbiAgYm9yZGVyLXJhZGl1czogMC45cmVtO1xyXG4gIHBhZGRpbmc6IDAuMnJlbTtcclxufVxyXG5cclxuLmRhc2hib2FyZC1uYXYtZHJvcGRvd24tdG9nZ2xlIHtcclxuICBwb3NpdGlvbjogcmVsYXRpdmU7XHJcbiAgY3Vyc29yOiBwb2ludGVyO1xyXG59XHJcblxyXG4uZGFzaGJvYXJkLW5hdi1kcm9wZG93bi10b2dnbGU6OmFmdGVyIHtcclxuICBjb250ZW50OiBcIlwiO1xyXG4gIG1hcmdpbi1sZWZ0OiBhdXRvO1xyXG4gIGRpc3BsYXk6IGlubGluZS1ibG9jaztcclxuICB3aWR0aDogMDtcclxuICBoZWlnaHQ6IDA7XHJcbiAgYm9yZGVyLWxlZnQ6IDVweCBzb2xpZCB0cmFuc3BhcmVudDtcclxuICBib3JkZXItcmlnaHQ6IDVweCBzb2xpZCB0cmFuc3BhcmVudDtcclxuICBib3JkZXItdG9wOiA1cHggc29saWQgcmdiYSgyNTUsIDI1NSwgMjU1LCAwLjcpO1xyXG4gIHRyYW5zZm9ybTogcm90YXRlKDkwZGVnKTtcclxufVxyXG5cclxuLmRhc2hib2FyZC1uYXYtZHJvcGRvd24uc2hvdyA+IC5kYXNoYm9hcmQtbmF2LWRyb3Bkb3duLXRvZ2dsZTo6YWZ0ZXIge1xyXG4gIHRyYW5zZm9ybTogbm9uZTtcclxufVxyXG5cclxuLmRhc2hib2FyZC1uYXYtZHJvcGRvd24tbWVudSB7XHJcbiAgZGlzcGxheTogbm9uZTtcclxuICBmbGV4LWRpcmVjdGlvbjogY29sdW1uO1xyXG4gIGdhcDogMC4ycmVtO1xyXG4gIHBhZGRpbmc6IDAgMC4ycmVtIDAuMTVyZW07XHJcbn1cclxuXHJcbi5kYXNoYm9hcmQtbmF2LWRyb3Bkb3duLnNob3cgPiAuZGFzaGJvYXJkLW5hdi1kcm9wZG93bi1tZW51IHtcclxuICBkaXNwbGF5OiBmbGV4O1xyXG59XHJcblxyXG4uZGFzaGJvYXJkLW5hdi1kcm9wZG93bi1pdGVtIHtcclxuICBtaW4taGVpZ2h0OiAzOHB4O1xyXG4gIHBhZGRpbmc6IDAuN3JlbSAwLjhyZW0gMC43cmVtIDEuMTVyZW07XHJcbiAgZGlzcGxheTogZmxleDtcclxuICBhbGlnbi1pdGVtczogY2VudGVyO1xyXG4gIGJvcmRlci1yYWRpdXM6IDAuN3JlbTtcclxuICBjb2xvcjogcmdiYSgyNTUsIDI1NSwgMjU1LCAwLjgyKTtcclxuICBiYWNrZ3JvdW5kOiByZ2JhKDI1NSwgMjU1LCAyNTUsIDAuMDIpO1xyXG4gIHRleHQtZGVjb3JhdGlvbjogbm9uZTtcclxufVxyXG5cclxuLmRhc2hib2FyZC1uYXYtZHJvcGRvd24taXRlbTpob3ZlcixcclxuLmRhc2hib2FyZC1uYXYtZHJvcGRvd24taXRlbTpmb2N1cyB7XHJcbiAgYmFja2dyb3VuZDogcmdiYSgyNTUsIDI1NSwgMjU1LCAwLjA4KTtcclxuICBjb2xvcjogI2ZmZjtcclxuICB0ZXh0LWRlY29yYXRpb246IG5vbmU7XHJcbn1cclxuXHJcbi5sb2dvdXQtaXRlbSB7XHJcbiAgbWFyZ2luLXRvcDogMC4yNXJlbTtcclxufVxyXG5cclxuLmRhc2hib2FyZC1hcHAge1xyXG4gIGRpc3BsYXk6IGZsZXg7XHJcbiAgZmxleC1kaXJlY3Rpb246IGNvbHVtbjtcclxuICBmbGV4OiAxO1xyXG4gIG1pbi13aWR0aDogMDtcclxufVxyXG5cclxuLmRhc2hib2FyZC10b29sYmFyIHtcclxuICBtaW4taGVpZ2h0OiA4NnB4O1xyXG4gIGJhY2tncm91bmQ6IHJnYmEoMjU1LCAyNTUsIDI1NSwgMC43KTtcclxuICBiYWNrZHJvcC1maWx0ZXI6IGJsdXIoMTRweCk7XHJcbiAgLXdlYmtpdC1iYWNrZHJvcC1maWx0ZXI6IGJsdXIoMTRweCk7XHJcbiAgZGlzcGxheTogZmxleDtcclxuICBhbGlnbi1pdGVtczogY2VudGVyO1xyXG4gIHBhZGRpbmc6IDEycHggMjZweDtcclxuICBwb3NpdGlvbjogc3RpY2t5O1xyXG4gIHRvcDogMDtcclxuICB6LWluZGV4OiAxMDtcclxuICBib3JkZXItYm90dG9tOiAxcHggc29saWQgcmdiYSgxNDgsIDE2MywgMTg0LCAwLjE4KTtcclxuICBib3gtc2hhZG93OiAwIDEwcHggMzBweCByZ2JhKDE1LCAyMywgNDIsIDAuMDQpO1xyXG59XHJcblxyXG4udG9vbGJhci1yb3cge1xyXG4gIHdpZHRoOiAxMDAlO1xyXG4gIGRpc3BsYXk6IGZsZXg7XHJcbiAgYWxpZ24taXRlbXM6IGNlbnRlcjtcclxuICBqdXN0aWZ5LWNvbnRlbnQ6IHNwYWNlLWJldHdlZW47XHJcbiAgZ2FwOiAwLjc1cmVtO1xyXG59XHJcblxyXG4ubW9iaWxlLW1lbnUtdG9nZ2xlIHtcclxuICBkaXNwbGF5OiBub25lO1xyXG4gIHdpZHRoOiA0MnB4O1xyXG4gIGhlaWdodDogNDJweDtcclxuICBib3JkZXI6IDA7XHJcbiAgYm9yZGVyLXJhZGl1czogMTJweDtcclxuICBiYWNrZ3JvdW5kOiByZ2JhKDY4LCA2MiwgMTYyLCAwLjA4KTtcclxuICBjb2xvcjogIzJkMmE2ZTtcclxuICBjdXJzb3I6IHBvaW50ZXI7XHJcbn1cclxuXHJcbi50b29sYmFyLWFjdGlvbnMge1xyXG4gIG1hcmdpbi1sZWZ0OiBhdXRvO1xyXG4gIGRpc3BsYXk6IGZsZXg7XHJcbiAgYWxpZ24taXRlbXM6IGNlbnRlcjtcclxuICBnYXA6IDAuOXJlbTtcclxufVxyXG5cclxuLnRvb2xiYXItYmFkZ2Uge1xyXG4gIGRpc3BsYXk6IGlubGluZS1mbGV4O1xyXG4gIGFsaWduLWl0ZW1zOiBjZW50ZXI7XHJcbiAgZ2FwOiAwLjVyZW07XHJcbiAgcGFkZGluZzogMC41OHJlbSAwLjlyZW07XHJcbiAgYm9yZGVyLXJhZGl1czogOTk5cHg7XHJcbiAgYmFja2dyb3VuZDogcmdiYSg0NSwgOTMsIDI0NiwgMC4wOCk7XHJcbiAgY29sb3I6ICMyZDVkZjY7XHJcbiAgZm9udC1zaXplOiAwLjhyZW07XHJcbiAgZm9udC13ZWlnaHQ6IDcwMDtcclxuICBsZXR0ZXItc3BhY2luZzogMC4wNGVtO1xyXG59XHJcblxyXG4udG9wYmFyLWljb24tYnRuIHtcclxuICB3aWR0aDogNDJweDtcclxuICBoZWlnaHQ6IDQycHg7XHJcbiAgYm9yZGVyOiBub25lO1xyXG4gIGJvcmRlci1yYWRpdXM6IDEycHg7XHJcbiAgYmFja2dyb3VuZDogcmdiYSg2OCwgNjIsIDE2MiwgMC4wOCk7XHJcbiAgY29sb3I6ICMyZDJhNmU7XHJcbiAgZGlzcGxheTogaW5saW5lLWZsZXg7XHJcbiAgYWxpZ24taXRlbXM6IGNlbnRlcjtcclxuICBqdXN0aWZ5LWNvbnRlbnQ6IGNlbnRlcjtcclxuICBib3gtc2hhZG93OiBub25lO1xyXG59XHJcblxyXG4udG9wYmFyLWljb24tYnRuOmhvdmVyLFxyXG4udG9wYmFyLWljb24tYnRuOmZvY3VzIHtcclxuICBiYWNrZ3JvdW5kOiByZ2JhKDY4LCA2MiwgMTYyLCAwLjE1KTtcclxufVxyXG5cclxuLnVzZXItY2hpcCB7XHJcbiAgZGlzcGxheTogaW5saW5lLWZsZXg7XHJcbiAgYWxpZ24taXRlbXM6IGNlbnRlcjtcclxuICBnYXA6IDAuNzVyZW07XHJcbiAgcGFkZGluZzogMC40NXJlbSAwLjg1cmVtIDAuNDVyZW0gMC40NXJlbTtcclxuICBib3JkZXItcmFkaXVzOiA5OTlweDtcclxuICBiYWNrZ3JvdW5kOiByZ2JhKDE1LCAyMywgNDIsIDAuMDQpO1xyXG4gIGJvcmRlcjogMXB4IHNvbGlkIHJnYmEoMTQ4LCAxNjMsIDE4NCwgMC4yKTtcclxuICBjb2xvcjogIzBmMTcyYTtcclxuICBmb250LXdlaWdodDogNjAwO1xyXG59XHJcblxyXG4udXNlci1hdmF0YXIge1xyXG4gIHdpZHRoOiAycmVtO1xyXG4gIGhlaWdodDogMnJlbTtcclxuICBib3JkZXItcmFkaXVzOiA1MCU7XHJcbiAgZGlzcGxheTogaW5saW5lLWZsZXg7XHJcbiAgYWxpZ24taXRlbXM6IGNlbnRlcjtcclxuICBqdXN0aWZ5LWNvbnRlbnQ6IGNlbnRlcjtcclxuICBiYWNrZ3JvdW5kOiBsaW5lYXItZ3JhZGllbnQoMTM1ZGVnLCAjNDQzZWEyLCAjNGY3Y2ZmKTtcclxuICBjb2xvcjogI2ZmZjtcclxuICBmb250LXNpemU6IDAuOHJlbTtcclxuICBmb250LXdlaWdodDogNzAwO1xyXG59XHJcblxyXG4uZGFzaGJvYXJkLWNvbnRlbnQge1xyXG4gIGZsZXg6IDE7XHJcbiAgcGFkZGluZzogMjRweCAyMnB4IDEwcHg7XHJcbiAgYmFja2dyb3VuZDogbGluZWFyLWdyYWRpZW50KDE4MGRlZywgcmdiYSgyNTUsMjU1LDI1NSwwLjA4KSwgcmdiYSgyMzMsIDIzNywgMjQ0LCAwLjcyKSk7XHJcbn1cclxuXHJcbi5kYXNoYm9hcmQtZm9vdGVyIHtcclxuICBkaXNwbGF5OiBmbGV4O1xyXG4gIGp1c3RpZnktY29udGVudDogY2VudGVyO1xyXG4gIGFsaWduLWl0ZW1zOiBjZW50ZXI7XHJcbiAgZ2FwOiAwLjc1cmVtO1xyXG4gIG1pbi1oZWlnaHQ6IDYycHg7XHJcbiAgcGFkZGluZzogMC45cmVtIDEuMnJlbSAxLjE1cmVtO1xyXG4gIGJhY2tncm91bmQ6IHJnYmEoMjU1LCAyNTUsIDI1NSwgMC42Mik7XHJcbiAgYm9yZGVyLXRvcDogMXB4IHNvbGlkIHJnYmEoMTQ4LCAxNjMsIDE4NCwgMC4xOCk7XHJcbiAgY29sb3I6IHZhcigtLXRleHQtc29mdCk7XHJcbiAgZm9udC1zaXplOiAwLjgycmVtO1xyXG4gIGxldHRlci1zcGFjaW5nOiAwLjAyZW07XHJcbn1cclxuXHJcbi5mb290ZXItZGl2aWRlciB7XHJcbiAgb3BhY2l0eTogMC41O1xyXG59XHJcblxyXG5AbWVkaWEgKG1heC13aWR0aDogOTkycHgpIHtcclxuICAuZGFzaGJvYXJkLW5hdiB7XHJcbiAgICBwb3NpdGlvbjogZml4ZWQ7XHJcbiAgICBpbnNldDogMCBhdXRvIDAgMDtcclxuICAgIHdpZHRoOiBtaW4oMjgwcHgsIDg1dncpO1xyXG4gICAgei1pbmRleDogMTAwMDtcclxuICAgIHRyYW5zZm9ybTogdHJhbnNsYXRlWCgtMTA1JSk7XHJcbiAgICB0cmFuc2l0aW9uOiB0cmFuc2Zvcm0gMC4yNXMgZWFzZTtcclxuICB9XHJcblxyXG4gIC5kYXNoYm9hcmQtbmF2Lm1vYmlsZS1zaG93IHtcclxuICAgIHRyYW5zZm9ybTogdHJhbnNsYXRlWCgwKTtcclxuICAgIGRpc3BsYXk6IGJsb2NrO1xyXG4gIH1cclxuXHJcbiAgLm1vYmlsZS1tZW51LXRvZ2dsZSB7XHJcbiAgICBkaXNwbGF5OiBpbmxpbmUtZmxleDtcclxuICAgIGFsaWduLWl0ZW1zOiBjZW50ZXI7XHJcbiAgICBqdXN0aWZ5LWNvbnRlbnQ6IGNlbnRlcjtcclxuICB9XHJcbn1cclxuXHJcbkBtZWRpYSAobWF4LXdpZHRoOiA3NjhweCkge1xyXG4gIC5kYXNoYm9hcmQtdG9vbGJhciB7XHJcbiAgICBwYWRkaW5nOiAxMHB4IDE0cHg7XHJcbiAgfVxyXG5cclxuICAudG9vbGJhci1yb3cge1xyXG4gICAgZ2FwOiAwLjVyZW07XHJcbiAgfVxyXG5cclxuICAudG9vbGJhci1hY3Rpb25zIHtcclxuICAgIGdhcDogMC41cmVtO1xyXG4gICAgZmxleC13cmFwOiB3cmFwO1xyXG4gICAganVzdGlmeS1jb250ZW50OiBmbGV4LWVuZDtcclxuICB9XHJcblxyXG4gIC5kYXNoYm9hcmQtY29udGVudCB7XHJcbiAgICBwYWRkaW5nOiAxNXB4IDEycHggNnB4O1xyXG4gIH1cclxuXHJcbiAgLmRhc2hib2FyZC1mb290ZXIge1xyXG4gICAgZmxleC1kaXJlY3Rpb246IGNvbHVtbjtcclxuICAgIGdhcDogMC4ycmVtO1xyXG4gICAgcGFkZGluZy1ib3R0b206IDEuMXJlbTtcclxuICB9XHJcbn1cclxuXHJcbkBtZWRpYSAobWF4LXdpZHRoOiA1NzZweCkge1xyXG4gIC51c2VyLWNoaXAge1xyXG4gICAgd2lkdGg6IDEwMCU7XHJcbiAgICBqdXN0aWZ5LWNvbnRlbnQ6IGNlbnRlcjtcclxuICB9XHJcblxyXG4gIC50b29sYmFyLWFjdGlvbnMge1xyXG4gICAgd2lkdGg6IDEwMCU7XHJcbiAgICBqdXN0aWZ5LWNvbnRlbnQ6IHNwYWNlLWJldHdlZW47XHJcbiAgfVxyXG59XHJcblxyXG5AbWVkaWEgKG1pbi13aWR0aDogOTkycHgpIHtcclxuICAuZGFzaGJvYXJkLWNvbXBhY3QgLmRhc2hib2FyZC1uYXYge1xyXG4gICAgZGlzcGxheTogbm9uZTtcclxuICB9XHJcbiAgLmRhc2hib2FyZC1jb21wYWN0IC5kYXNoYm9hcmQtdG9vbGJhciB7XHJcbiAgICBsZWZ0OiAwO1xyXG4gIH1cclxufVxyXG5cclxuQG1lZGlhIChtYXgtd2lkdGg6IDk5MnB4KSB7XHJcbiAgLmRhc2hib2FyZC1hcHAge1xyXG4gICAgd2lkdGg6IDEwMCU7XHJcbiAgfVxyXG59XHJcblxyXG4uZGFzaGJvYXJkLWNvbnRlbnQgLmNhcmQsXHJcbi5kYXNoYm9hcmQtY29udGVudCAucGFuZWwsXHJcbi5kYXNoYm9hcmQtY29udGVudCAuY29udGFpbmVyLWZsdWlkLFxyXG4uZGFzaGJvYXJkLWNvbnRlbnQgLnJvdyB7XHJcbiAgYm9yZGVyLXJhZGl1czogMjBweDtcclxufVxyXG5cclxuIl0sInNvdXJjZVJvb3QiOiIifQ== */"]
    });
  }
}

/***/ }),

/***/ 6747:
/*!*******************************!*\
  !*** ./src/app/app.module.ts ***!
  \*******************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   "AppModule": () => (/* binding */ AppModule)
/* harmony export */ });
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! @angular/common/http */ 8987);
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! @angular/forms */ 2508);
/* harmony import */ var _angular_platform_browser__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! @angular/platform-browser */ 4497);
/* harmony import */ var _angular_platform_browser_animations__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! @angular/platform-browser/animations */ 7146);
/* harmony import */ var _fortawesome_angular_fontawesome__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! @fortawesome/angular-fontawesome */ 9200);
/* harmony import */ var _app_component__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./app.component */ 5041);
/* harmony import */ var _app_routing_module__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./app.routing.module */ 3670);
/* harmony import */ var _component_loader_loader_component__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./component/loader/loader.component */ 1157);
/* harmony import */ var _component_note_note_component__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./component/note/note.component */ 1475);
/* harmony import */ var _core_component_notification_popup_notification_popup_component__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./core/component/notification-popup/notification-popup.component */ 8398);
/* harmony import */ var _core_component_shared_module__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ./core/component/shared.module */ 9004);
/* harmony import */ var _core_core_module__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ./core/core.module */ 294);
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! @angular/core */ 2560);














class AppModule {
  static {
    this.ɵfac = function AppModule_Factory(t) {
      return new (t || AppModule)();
    };
  }
  static {
    this.ɵmod = /*@__PURE__*/_angular_core__WEBPACK_IMPORTED_MODULE_7__["ɵɵdefineNgModule"]({
      type: AppModule,
      bootstrap: [_app_component__WEBPACK_IMPORTED_MODULE_0__.AppComponent]
    });
  }
  static {
    this.ɵinj = /*@__PURE__*/_angular_core__WEBPACK_IMPORTED_MODULE_7__["ɵɵdefineInjector"]({
      imports: [_angular_platform_browser__WEBPACK_IMPORTED_MODULE_8__.BrowserModule.withServerTransition({
        appId: 'ng-cli-universal'
      }), _angular_common_http__WEBPACK_IMPORTED_MODULE_9__.HttpClientModule, _angular_forms__WEBPACK_IMPORTED_MODULE_10__.FormsModule, _app_routing_module__WEBPACK_IMPORTED_MODULE_1__.AppRoutingModule, _core_core_module__WEBPACK_IMPORTED_MODULE_6__.CoreModule, _fortawesome_angular_fontawesome__WEBPACK_IMPORTED_MODULE_11__.FontAwesomeModule, _angular_platform_browser_animations__WEBPACK_IMPORTED_MODULE_12__.BrowserAnimationsModule, _core_component_shared_module__WEBPACK_IMPORTED_MODULE_5__.SharedModule // Add for shared components/modules
      ]
    });
  }
}

(function () {
  (typeof ngJitMode === "undefined" || ngJitMode) && _angular_core__WEBPACK_IMPORTED_MODULE_7__["ɵɵsetNgModuleScope"](AppModule, {
    declarations: [_app_component__WEBPACK_IMPORTED_MODULE_0__.AppComponent, _component_loader_loader_component__WEBPACK_IMPORTED_MODULE_2__.LoaderComponent, _core_component_notification_popup_notification_popup_component__WEBPACK_IMPORTED_MODULE_4__.NotificationPopupComponent, _component_note_note_component__WEBPACK_IMPORTED_MODULE_3__.NoteComponent],
    imports: [_angular_platform_browser__WEBPACK_IMPORTED_MODULE_8__.BrowserModule, _angular_common_http__WEBPACK_IMPORTED_MODULE_9__.HttpClientModule, _angular_forms__WEBPACK_IMPORTED_MODULE_10__.FormsModule, _app_routing_module__WEBPACK_IMPORTED_MODULE_1__.AppRoutingModule, _core_core_module__WEBPACK_IMPORTED_MODULE_6__.CoreModule, _fortawesome_angular_fontawesome__WEBPACK_IMPORTED_MODULE_11__.FontAwesomeModule, _angular_platform_browser_animations__WEBPACK_IMPORTED_MODULE_12__.BrowserAnimationsModule, _core_component_shared_module__WEBPACK_IMPORTED_MODULE_5__.SharedModule // Add for shared components/modules
    ]
  });
})();

/***/ }),

/***/ 3670:
/*!***************************************!*\
  !*** ./src/app/app.routing.module.ts ***!
  \***************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   "AppRoutingModule": () => (/* binding */ AppRoutingModule)
/* harmony export */ });
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/router */ 124);
/* harmony import */ var _constant_constant__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./constant/constant */ 4219);
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ 2560);




const routes = [{
  path: '',
  redirectTo: _constant_constant__WEBPACK_IMPORTED_MODULE_0__.AppPaths.home,
  pathMatch: 'full'
}, {
  path: '',
  loadChildren: () => __webpack_require__.e(/*! import() */ "src_app_component_component_module_ts").then(__webpack_require__.bind(__webpack_require__, /*! ../app/component/component.module */ 5051)).then(m => m.ComponentModule),
  pathMatch: 'prefix'
}];
class AppRoutingModule {
  static {
    this.ɵfac = function AppRoutingModule_Factory(t) {
      return new (t || AppRoutingModule)();
    };
  }
  static {
    this.ɵmod = /*@__PURE__*/_angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵdefineNgModule"]({
      type: AppRoutingModule
    });
  }
  static {
    this.ɵinj = /*@__PURE__*/_angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵdefineInjector"]({
      imports: [_angular_router__WEBPACK_IMPORTED_MODULE_2__.RouterModule.forRoot(routes, {
        preloadingStrategy: _angular_router__WEBPACK_IMPORTED_MODULE_2__.PreloadAllModules,
        scrollPositionRestoration: 'enabled',
        useHash: false,
        onSameUrlNavigation: 'reload',
        enableTracing: false
      }), _angular_router__WEBPACK_IMPORTED_MODULE_2__.RouterModule]
    });
  }
}
(function () {
  (typeof ngJitMode === "undefined" || ngJitMode) && _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵsetNgModuleScope"](AppRoutingModule, {
    imports: [_angular_router__WEBPACK_IMPORTED_MODULE_2__.RouterModule],
    exports: [_angular_router__WEBPACK_IMPORTED_MODULE_2__.RouterModule]
  });
})();

/***/ }),

/***/ 1157:
/*!******************************************************!*\
  !*** ./src/app/component/loader/loader.component.ts ***!
  \******************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   "LoaderComponent": () => (/* binding */ LoaderComponent)
/* harmony export */ });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ 2560);
/* harmony import */ var _core_service_loader_service__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ../../core/service/loader.service */ 5523);
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/common */ 4666);



function LoaderComponent_div_0_Template(rf, ctx) {
  if (rf & 1) {
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](0, "div", 1);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelement"](1, "div", 2);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
  }
}
class LoaderComponent {
  constructor(loaderService) {
    this.loaderService = loaderService;
    this.isLoading = false;
  }
  ngOnInit() {
    this.loaderService.loader$.subscribe(value => {
      this.isLoading = value;
    });
  }
  static {
    this.ɵfac = function LoaderComponent_Factory(t) {
      return new (t || LoaderComponent)(_angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵdirectiveInject"](_core_service_loader_service__WEBPACK_IMPORTED_MODULE_0__.LoaderService));
    };
  }
  static {
    this.ɵcmp = /*@__PURE__*/_angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵdefineComponent"]({
      type: LoaderComponent,
      selectors: [["app-loader"]],
      decls: 1,
      vars: 1,
      consts: [["class", "loader-container", 4, "ngIf"], [1, "loader-container"], [1, "loader"]],
      template: function LoaderComponent_Template(rf, ctx) {
        if (rf & 1) {
          _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtemplate"](0, LoaderComponent_div_0_Template, 2, 0, "div", 0);
        }
        if (rf & 2) {
          _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵproperty"]("ngIf", ctx.isLoading);
        }
      },
      dependencies: [_angular_common__WEBPACK_IMPORTED_MODULE_2__.NgIf],
      styles: [".loader-container[_ngcontent-%COMP%] {\r\n  position: fixed;\r\n  top: 0;\r\n  left: 0;\r\n  width: 100%;\r\n  height: 100%;\r\n  background: rgba(255, 255, 255, 0.6); \r\n  display: flex;\r\n  justify-content: center;\r\n  align-items: center;\r\n  z-index: 9999; \r\n  -webkit-backdrop-filter: blur(0.5%);\r\n          backdrop-filter: blur(0.5%); \r\n}\r\n\r\n.loader[_ngcontent-%COMP%] {\r\n  border: 8px solid #f3f3f3; \r\n  border-top: 8px solid #3498db; \r\n  border-radius: 50%;\r\n  width: 50px;\r\n  height: 50px;\r\n  animation: _ngcontent-%COMP%_spin 1s linear infinite; \r\n}\r\n\r\n@keyframes _ngcontent-%COMP%_spin {\r\n  0% {\r\n    transform: rotate(0deg);\r\n  }\r\n\r\n  100% {\r\n    transform: rotate(360deg);\r\n  }\r\n}\n/*# sourceMappingURL=data:application/json;charset=utf-8;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbIndlYnBhY2s6Ly8uL3NyYy9hcHAvY29tcG9uZW50L2xvYWRlci9sb2FkZXIuY29tcG9uZW50LmNzcyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiO0FBQ0E7RUFDRSxlQUFlO0VBQ2YsTUFBTTtFQUNOLE9BQU87RUFDUCxXQUFXO0VBQ1gsWUFBWTtFQUNaLG9DQUFvQyxFQUFFLHNDQUFzQztFQUM1RSxhQUFhO0VBQ2IsdUJBQXVCO0VBQ3ZCLG1CQUFtQjtFQUNuQixhQUFhLEVBQUUscURBQXFEO0VBQ3BFLG1DQUEyQjtVQUEzQiwyQkFBMkIsRUFBRSwwQ0FBMEM7QUFDekU7O0FBRUE7RUFDRSx5QkFBeUIsRUFBRSxlQUFlO0VBQzFDLDZCQUE2QixFQUFFLFNBQVM7RUFDeEMsa0JBQWtCO0VBQ2xCLFdBQVc7RUFDWCxZQUFZO0VBQ1osa0NBQWtDLEVBQUUsMkJBQTJCO0FBQ2pFOztBQUVBO0VBQ0U7SUFDRSx1QkFBdUI7RUFDekI7O0VBRUE7SUFDRSx5QkFBeUI7RUFDM0I7QUFDRiIsInNvdXJjZXNDb250ZW50IjpbIlxyXG4ubG9hZGVyLWNvbnRhaW5lciB7XHJcbiAgcG9zaXRpb246IGZpeGVkO1xyXG4gIHRvcDogMDtcclxuICBsZWZ0OiAwO1xyXG4gIHdpZHRoOiAxMDAlO1xyXG4gIGhlaWdodDogMTAwJTtcclxuICBiYWNrZ3JvdW5kOiByZ2JhKDI1NSwgMjU1LCAyNTUsIDAuNik7IC8qIFNlbWktdHJhbnNwYXJlbnQgd2hpdGUgYmFja2dyb3VuZCAqL1xyXG4gIGRpc3BsYXk6IGZsZXg7XHJcbiAganVzdGlmeS1jb250ZW50OiBjZW50ZXI7XHJcbiAgYWxpZ24taXRlbXM6IGNlbnRlcjtcclxuICB6LWluZGV4OiA5OTk5OyAvKiBNYWtlIHN1cmUgdGhlIGxvYWRlciBpcyBvbiB0b3Agb2Ygb3RoZXIgZWxlbWVudHMgKi9cclxuICBiYWNrZHJvcC1maWx0ZXI6IGJsdXIoMC41JSk7IC8qIEFwcGx5IGEgYmx1ciBlZmZlY3QgdG8gdGhlIGJhY2tncm91bmQgKi9cclxufVxyXG5cclxuLmxvYWRlciB7XHJcbiAgYm9yZGVyOiA4cHggc29saWQgI2YzZjNmMzsgLyogTGlnaHQgZ3JleSAqL1xyXG4gIGJvcmRlci10b3A6IDhweCBzb2xpZCAjMzQ5OGRiOyAvKiBCbHVlICovXHJcbiAgYm9yZGVyLXJhZGl1czogNTAlO1xyXG4gIHdpZHRoOiA1MHB4O1xyXG4gIGhlaWdodDogNTBweDtcclxuICBhbmltYXRpb246IHNwaW4gMXMgbGluZWFyIGluZmluaXRlOyAvKiBBcHBseSBhIHNwaW4gYW5pbWF0aW9uICovXHJcbn1cclxuXHJcbkBrZXlmcmFtZXMgc3BpbiB7XHJcbiAgMCUge1xyXG4gICAgdHJhbnNmb3JtOiByb3RhdGUoMGRlZyk7XHJcbiAgfVxyXG5cclxuICAxMDAlIHtcclxuICAgIHRyYW5zZm9ybTogcm90YXRlKDM2MGRlZyk7XHJcbiAgfVxyXG59XHJcbiJdLCJzb3VyY2VSb290IjoiIn0= */"]
    });
  }
}

/***/ }),

/***/ 1475:
/*!**************************************************!*\
  !*** ./src/app/component/note/note.component.ts ***!
  \**************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   "NoteComponent": () => (/* binding */ NoteComponent)
/* harmony export */ });
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! rxjs */ 228);
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! rxjs */ 1989);
/* harmony import */ var _dto_request_note_delete_note_request__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ../../dto/request/note/delete-note-request */ 6168);
/* harmony import */ var _dto_request_note_save_note_request__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../../dto/request/note/save-note-request */ 7882);
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @angular/core */ 2560);
/* harmony import */ var _core_service_note_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../../core/service/note.service */ 6429);
/* harmony import */ var _core_notificationService_notification_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../../core/notificationService/notification-service */ 1971);
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! @angular/common */ 4666);
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! @angular/forms */ 2508);








function NoteComponent_div_2_div_8_li_4_Template(rf, ctx) {
  if (rf & 1) {
    const _r6 = _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵgetCurrentView"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵelementStart"](0, "li", 11)(1, "div", 12);
    _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵlistener"]("click", function NoteComponent_div_2_div_8_li_4_Template_div_click_1_listener() {
      const restoredCtx = _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵrestoreView"](_r6);
      const note_r4 = restoredCtx.$implicit;
      const ctx_r5 = _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵnextContext"](3);
      return _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵresetView"](ctx_r5.openNote(note_r4));
    });
    _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵelementStart"](2, "div", 13);
    _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵtext"](3);
    _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵelementStart"](4, "div", 14);
    _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵtext"](5);
    _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵpipe"](6, "slice");
    _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵelementEnd"]()();
    _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵelementStart"](7, "button", 15);
    _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵlistener"]("click", function NoteComponent_div_2_div_8_li_4_Template_button_click_7_listener() {
      const restoredCtx = _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵrestoreView"](_r6);
      const note_r4 = restoredCtx.$implicit;
      const ctx_r7 = _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵnextContext"](3);
      return _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵresetView"](ctx_r7.deleteNote(note_r4.noteId));
    });
    _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵtext"](8, "\uD83D\uDDD1");
    _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵelementEnd"]()();
  }
  if (rf & 2) {
    const note_r4 = ctx.$implicit;
    _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵadvance"](3);
    _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵtextInterpolate"](note_r4.title);
    _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵadvance"](2);
    _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵtextInterpolate1"]("", _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵpipeBind3"](6, 2, note_r4.content, 0, 60), "...");
  }
}
function NoteComponent_div_2_div_8_Template(rf, ctx) {
  if (rf & 1) {
    const _r9 = _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵgetCurrentView"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵelementStart"](0, "div")(1, "button", 8);
    _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵlistener"]("click", function NoteComponent_div_2_div_8_Template_button_click_1_listener() {
      _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵrestoreView"](_r9);
      const ctx_r8 = _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵnextContext"](2);
      return _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵresetView"](ctx_r8.addNewNote());
    });
    _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵtext"](2, "\u2795 Add Note");
    _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵelementStart"](3, "ul", 9);
    _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵtemplate"](4, NoteComponent_div_2_div_8_li_4_Template, 9, 6, "li", 10);
    _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵelementEnd"]()();
  }
  if (rf & 2) {
    const ctx_r1 = _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵnextContext"](2);
    _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵadvance"](4);
    _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵproperty"]("ngForOf", ctx_r1.notes);
  }
}
function NoteComponent_div_2_div_9_Template(rf, ctx) {
  if (rf & 1) {
    const _r11 = _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵgetCurrentView"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵelementStart"](0, "div")(1, "form")(2, "input", 16);
    _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵlistener"]("ngModelChange", function NoteComponent_div_2_div_9_Template_input_ngModelChange_2_listener($event) {
      _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵrestoreView"](_r11);
      const ctx_r10 = _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵnextContext"](2);
      return _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵresetView"](ctx_r10.selectedNote.title = $event);
    })("ngModelChange", function NoteComponent_div_2_div_9_Template_input_ngModelChange_2_listener() {
      _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵrestoreView"](_r11);
      const ctx_r12 = _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵnextContext"](2);
      return _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵresetView"](ctx_r12.triggerAutoSave());
    });
    _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵelementStart"](3, "textarea", 17);
    _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵlistener"]("ngModelChange", function NoteComponent_div_2_div_9_Template_textarea_ngModelChange_3_listener($event) {
      _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵrestoreView"](_r11);
      const ctx_r13 = _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵnextContext"](2);
      return _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵresetView"](ctx_r13.selectedNote.content = $event);
    })("ngModelChange", function NoteComponent_div_2_div_9_Template_textarea_ngModelChange_3_listener() {
      _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵrestoreView"](_r11);
      const ctx_r14 = _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵnextContext"](2);
      return _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵresetView"](ctx_r14.triggerAutoSave());
    });
    _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵelementEnd"]()()();
  }
  if (rf & 2) {
    const ctx_r2 = _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵnextContext"](2);
    _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵadvance"](2);
    _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵproperty"]("ngModel", ctx_r2.selectedNote.title);
    _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵadvance"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵproperty"]("ngModel", ctx_r2.selectedNote.content);
  }
}
function NoteComponent_div_2_Template(rf, ctx) {
  if (rf & 1) {
    const _r16 = _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵgetCurrentView"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵelementStart"](0, "div", 2)(1, "div", 3)(2, "div", 4)(3, "h5");
    _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵtext"](4, "\uD83D\uDCDD My Notes");
    _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵelementStart"](5, "button", 5);
    _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵlistener"]("click", function NoteComponent_div_2_Template_button_click_5_listener() {
      _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵrestoreView"](_r16);
      const ctx_r15 = _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵnextContext"]();
      return _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵresetView"](ctx_r15.togglePopup());
    });
    _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵtext"](6, "\u2716");
    _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵelementEnd"]()();
    _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵelementStart"](7, "div", 6);
    _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵtemplate"](8, NoteComponent_div_2_div_8_Template, 5, 1, "div", 7);
    _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵtemplate"](9, NoteComponent_div_2_div_9_Template, 4, 2, "div", 7);
    _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵelementEnd"]()()();
  }
  if (rf & 2) {
    const ctx_r0 = _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵnextContext"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵadvance"](8);
    _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵproperty"]("ngIf", !ctx_r0.selectedNote);
    _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵadvance"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵproperty"]("ngIf", ctx_r0.selectedNote);
  }
}
class NoteComponent {
  constructor(noteService, notificationService) {
    this.noteService = noteService;
    this.notificationService = notificationService;
    this.isPopupOpen = false;
    this.notes = [];
    this.selectedNote = null;
    this.newNoteMode = false;
    this.autoSave$ = new rxjs__WEBPACK_IMPORTED_MODULE_5__.Subject();
    this.debounceTimeMs = 1000; // 1 second delay
    this.noteService.getAllNotes().subscribe(response => {
      if (response.status == true) {
        this.notes = response.response;
      }
    });
  }
  ngOnInit() {
    this.autoSave$.pipe((0,rxjs__WEBPACK_IMPORTED_MODULE_6__.debounceTime)(this.debounceTimeMs)).subscribe(() => this.saveNote(false));
  }
  handleEscape(event) {
    if (this.selectedNote) {
      this.selectedNote = null;
      this.newNoteMode = false;
      return;
    }
    if (this.isPopupOpen || this.selectedNote) {
      this.isPopupOpen = false;
    }
  }
  togglePopup() {
    if (this.selectedNote) {
      // If editing a note, go back to list
      this.selectedNote = null;
      this.newNoteMode = false;
    } else {
      // If already at list view, toggle the popup
      this.isPopupOpen = !this.isPopupOpen;
    }
  }
  openNote(note) {
    this.selectedNote = {
      ...note
    };
    this.newNoteMode = false;
  }
  addNewNote() {
    let model = (0,_dto_request_note_save_note_request__WEBPACK_IMPORTED_MODULE_1__.CreateSaveNoteRequestObject)(null, '', '');
    this.noteService.saveNote(model).subscribe(response => {
      if (response.status == true) {
        const newNote = {
          noteId: response.response,
          title: '',
          content: '',
          modifiedDate: new Date(),
          isEditing: true
        };
        this.selectedNote = newNote;
        this.newNoteMode = true;
        this.notes.unshift(newNote);
      } else {
        let messageList = [];
        if (Array.isArray(response?.errors) && response?.errors) messageList = [...response?.errors?.map(x => x.errorMessage), ...response?.validationErrors?.map(x => x.errorMessage)];
        messageList.push("Note save failed.");
        messageList.forEach(msg => {
          this.notificationService.show(msg, 'error');
        });
      }
    });
  }
  saveNote(closeAfterSave = true) {
    if (!this.selectedNote) return;
    const model = (0,_dto_request_note_save_note_request__WEBPACK_IMPORTED_MODULE_1__.CreateSaveNoteRequestObject)(this.selectedNote.noteId, this.selectedNote.title, this.selectedNote.content);
    this.noteService.saveNote(model).subscribe(response => {
      if (response.status == true) {
        const index = this.notes.findIndex(n => n.noteId === this.selectedNote?.noteId);
        if (index > -1) {
          this.notes[index] = {
            ...this.selectedNote,
            modifiedDate: new Date()
          };
        }
        if (closeAfterSave) {
          this.selectedNote = null;
          this.newNoteMode = false;
        }
      } else {
        let messageList = [];
        if (Array.isArray(response?.errors) && response?.errors) messageList = [...response?.errors?.map(x => x.errorMessage), ...response?.validationErrors?.map(x => x.errorMessage)];
        messageList.push("Note save failed.");
        messageList.forEach(msg => {
          this.notificationService.show(msg, 'error');
        });
      }
    });
  }
  triggerAutoSave() {
    if (this.selectedNote) {
      this.autoSave$.next();
    }
  }
  closeEditor() {
    this.selectedNote = null;
    this.newNoteMode = false;
  }
  deleteNote(noteId) {
    let model = (0,_dto_request_note_delete_note_request__WEBPACK_IMPORTED_MODULE_0__.CreateDeleteNoteRequestObject)(noteId);
    this.noteService.deleteNote(model).subscribe(response => {
      if (response.status == true) {
        this.notes = this.notes.filter(n => n.noteId !== noteId);
        if (this.selectedNote?.noteId === noteId) {
          this.selectedNote = null;
          this.newNoteMode = false;
        }
        this.notificationService.show("Note Deleted successfully.", 'success');
      } else {
        let messageList = [];
        if (Array.isArray(response?.errors) && response?.errors) messageList = [...response?.errors?.map(x => x.errorMessage), ...response?.validationErrors?.map(x => x.errorMessage)];
        messageList.push("Failed to delete note.");
        messageList.forEach(msg => {
          this.notificationService.show(msg, 'error');
        });
      }
    });
  }
  static {
    this.ɵfac = function NoteComponent_Factory(t) {
      return new (t || NoteComponent)(_angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵdirectiveInject"](_core_service_note_service__WEBPACK_IMPORTED_MODULE_2__.NoteService), _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵdirectiveInject"](_core_notificationService_notification_service__WEBPACK_IMPORTED_MODULE_3__.NotificationService));
    };
  }
  static {
    this.ɵcmp = /*@__PURE__*/_angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵdefineComponent"]({
      type: NoteComponent,
      selectors: [["app-note"]],
      hostBindings: function NoteComponent_HostBindings(rf, ctx) {
        if (rf & 1) {
          _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵlistener"]("keydown.escape", function NoteComponent_keydown_escape_HostBindingHandler($event) {
            return ctx.handleEscape($event);
          }, false, _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵresolveDocument"]);
        }
      },
      decls: 3,
      vars: 1,
      consts: [[1, "floating-button", 3, "click"], ["class", "popup-backdrop", 4, "ngIf"], [1, "popup-backdrop"], [1, "notes-popup", "shadow"], [1, "popup-header"], [1, "icon-button", 3, "click"], [1, "popup-content"], [4, "ngIf"], [1, "btn", "btn-sm", "btn-success", "mb-2", "w-100", 3, "click"], [1, "list-group"], ["class", "note-item", 4, "ngFor", "ngForOf"], [1, "note-item"], [1, "note-preview", 3, "click"], [1, "note-title"], [1, "note-snippet"], [1, "delete-icon", 3, "click"], ["type", "text", "placeholder", "Title", "name", "title", 1, "form-control", "note-input", "mb-2", 3, "ngModel", "ngModelChange"], ["rows", "6", "placeholder", "Write your note here...", "name", "content", 1, "form-control", 2, "min-height", "calc(1.5em + 30.75rem + calc(var(--bs-border-width) * 2))", 3, "ngModel", "ngModelChange"]],
      template: function NoteComponent_Template(rf, ctx) {
        if (rf & 1) {
          _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵelementStart"](0, "div", 0);
          _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵlistener"]("click", function NoteComponent_Template_div_click_0_listener() {
            return ctx.togglePopup();
          });
          _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵtext"](1, " \uD83D\uDCDD\n");
          _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵelementEnd"]();
          _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵtemplate"](2, NoteComponent_div_2_Template, 10, 2, "div", 1);
        }
        if (rf & 2) {
          _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵadvance"](2);
          _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵproperty"]("ngIf", ctx.isPopupOpen);
        }
      },
      dependencies: [_angular_common__WEBPACK_IMPORTED_MODULE_7__.NgForOf, _angular_common__WEBPACK_IMPORTED_MODULE_7__.NgIf, _angular_forms__WEBPACK_IMPORTED_MODULE_8__["ɵNgNoValidate"], _angular_forms__WEBPACK_IMPORTED_MODULE_8__.DefaultValueAccessor, _angular_forms__WEBPACK_IMPORTED_MODULE_8__.NgControlStatus, _angular_forms__WEBPACK_IMPORTED_MODULE_8__.NgControlStatusGroup, _angular_forms__WEBPACK_IMPORTED_MODULE_8__.NgModel, _angular_forms__WEBPACK_IMPORTED_MODULE_8__.NgForm, _angular_common__WEBPACK_IMPORTED_MODULE_7__.SlicePipe],
      styles: [".floating-button[_ngcontent-%COMP%] {\r\n  position: fixed;\r\n  bottom: 25px;\r\n  right: 25px;\r\n  background-color: #0d6efd;\r\n  color: white;\r\n  border-radius: 50%;\r\n  width: 55px;\r\n  height: 55px;\r\n  font-size: 28px;\r\n  text-align: center;\r\n  line-height: 55px;\r\n  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.3);\r\n  cursor: pointer;\r\n  z-index: 1000;\r\n  transition: background-color 0.3s ease;\r\n}\r\n\r\n  .floating-button[_ngcontent-%COMP%]:hover {\r\n    background-color: #0b5ed7;\r\n  }\r\n\r\n.popup-backdrop[_ngcontent-%COMP%] {\r\n  position: fixed;\r\n  bottom: 100px;\r\n  right: 30px;\r\n  width: 460px;\r\n  max-height: 75vh;\r\n  background-color: #fff;\r\n  border-radius: 16px;\r\n  box-shadow: 0 10px 30px rgba(0, 0, 0, 0.2);\r\n  overflow: hidden;\r\n  z-index: 1001;\r\n  animation: _ngcontent-%COMP%_fadeInUp 0.3s ease;\r\n}\r\n\r\n@keyframes _ngcontent-%COMP%_fadeInUp {\r\n  from {\r\n    opacity: 0;\r\n    transform: translateY(20px);\r\n  }\r\n\r\n  to {\r\n    opacity: 1;\r\n    transform: translateY(0);\r\n  }\r\n}\r\n\r\n.notes-popup[_ngcontent-%COMP%] {\r\n  display: flex;\r\n  flex-direction: column;\r\n  height: 100%;\r\n  padding: 16px;\r\n}\r\n\r\n.popup-header[_ngcontent-%COMP%] {\r\n  display: flex;\r\n  justify-content: space-between;\r\n  align-items: center;\r\n  padding-bottom: 8px;\r\n  border-bottom: 1px solid #eaeaea;\r\n  margin-bottom: 10px;\r\n}\r\n\r\n  .popup-header[_ngcontent-%COMP%]   h5[_ngcontent-%COMP%] {\r\n    margin: 0;\r\n    color: #0d6efd;\r\n    font-size: 18px;\r\n    font-weight: 600;\r\n  }\r\n\r\n.icon-button[_ngcontent-%COMP%] {\r\n  background: transparent;\r\n  border: none;\r\n  font-size: 18px;\r\n  color: #666;\r\n  cursor: pointer;\r\n}\r\n\r\n.popup-content[_ngcontent-%COMP%] {\r\n  overflow-y: auto;\r\n  flex: 1;\r\n  max-height: 55vh;\r\n  padding-right: 6px;\r\n}\r\n\r\n.add-note-btn[_ngcontent-%COMP%] {\r\n  background-color: #198754;\r\n  color: white;\r\n  padding: 4px 10px;\r\n  font-size: 14px;\r\n}\r\n\r\n.note-list[_ngcontent-%COMP%] {\r\n  list-style: none;\r\n  padding: 0;\r\n  margin-top: 10px;\r\n}\r\n\r\n.note-item[_ngcontent-%COMP%] {\r\n  background-color: #f8f9fa;\r\n  border: 1px solid #0d6efd;\r\n  border-radius: 10px;\r\n  padding: 12px 14px;\r\n  margin-bottom: 12px;\r\n  cursor: pointer;\r\n  display: flex;\r\n  justify-content: space-between;\r\n  align-items: center;\r\n  transition: background-color 0.2s ease;\r\n  font-size: 15px;\r\n}\r\n\r\n  .note-item[_ngcontent-%COMP%]:hover {\r\n    background-color: #e7f1ff;\r\n  }\r\n\r\n.note-title[_ngcontent-%COMP%] {\r\n  font-weight: 600;\r\n  color: #0d6efd;\r\n}\r\n\r\n.note-snippet[_ngcontent-%COMP%] {\r\n  font-size: 13px;\r\n  color: #555;\r\n  margin-top: 2px;\r\n}\r\n\r\n.note-input[_ngcontent-%COMP%], .note-textarea[_ngcontent-%COMP%] {\r\n  border-radius: 8px;\r\n  border: 1px solid #ced4da;\r\n  font-size: 14px;\r\n}\r\n\r\n  .note-input[_ngcontent-%COMP%]:focus, .note-textarea[_ngcontent-%COMP%]:focus {\r\n    border-color: #0d6efd;\r\n    box-shadow: 0 0 0 0.2rem rgba(13, 110, 253, 0.25);\r\n  }\r\n.list-group-item[_ngcontent-%COMP%] {\r\n  cursor: pointer;\r\n  border: 1px solid #0d6efd;\r\n  border-radius: 8px;\r\n  margin-bottom: 12px;\r\n  padding: 16px;\r\n  transition: background-color 0.2s ease;\r\n  font-size: 16px;\r\n}\r\n\r\n  .list-group-item[_ngcontent-%COMP%]:hover {\r\n    background-color: #e7f1ff;\r\n  }\r\n\r\n.note-preview[_ngcontent-%COMP%] {\r\n  flex-grow: 1;\r\n  padding-right: 10px;\r\n  overflow: hidden;\r\n}\r\n\r\n.delete-icon[_ngcontent-%COMP%] {\r\n  font-size: 18px;\r\n  color: #dc3545;\r\n  padding: 2px 6px;\r\n  background: transparent;\r\n  border: none;\r\n  cursor: pointer;\r\n  transition: color 0.2s ease;\r\n}\r\n  .delete-icon[_ngcontent-%COMP%]:hover {\r\n    color: #bb2d3b;\r\n  }\n/*# sourceMappingURL=data:application/json;charset=utf-8;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbIndlYnBhY2s6Ly8uL3NyYy9hcHAvY29tcG9uZW50L25vdGUvbm90ZS5jb21wb25lbnQuY3NzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiJBQUFBO0VBQ0UsZUFBZTtFQUNmLFlBQVk7RUFDWixXQUFXO0VBQ1gseUJBQXlCO0VBQ3pCLFlBQVk7RUFDWixrQkFBa0I7RUFDbEIsV0FBVztFQUNYLFlBQVk7RUFDWixlQUFlO0VBQ2Ysa0JBQWtCO0VBQ2xCLGlCQUFpQjtFQUNqQix5Q0FBeUM7RUFDekMsZUFBZTtFQUNmLGFBQWE7RUFDYixzQ0FBc0M7QUFDeEM7O0VBRUU7SUFDRSx5QkFBeUI7RUFDM0I7O0FBRUY7RUFDRSxlQUFlO0VBQ2YsYUFBYTtFQUNiLFdBQVc7RUFDWCxZQUFZO0VBQ1osZ0JBQWdCO0VBQ2hCLHNCQUFzQjtFQUN0QixtQkFBbUI7RUFDbkIsMENBQTBDO0VBQzFDLGdCQUFnQjtFQUNoQixhQUFhO0VBQ2IsNkJBQTZCO0FBQy9COztBQUVBO0VBQ0U7SUFDRSxVQUFVO0lBQ1YsMkJBQTJCO0VBQzdCOztFQUVBO0lBQ0UsVUFBVTtJQUNWLHdCQUF3QjtFQUMxQjtBQUNGOztBQUVBO0VBQ0UsYUFBYTtFQUNiLHNCQUFzQjtFQUN0QixZQUFZO0VBQ1osYUFBYTtBQUNmOztBQUVBO0VBQ0UsYUFBYTtFQUNiLDhCQUE4QjtFQUM5QixtQkFBbUI7RUFDbkIsbUJBQW1CO0VBQ25CLGdDQUFnQztFQUNoQyxtQkFBbUI7QUFDckI7O0VBRUU7SUFDRSxTQUFTO0lBQ1QsY0FBYztJQUNkLGVBQWU7SUFDZixnQkFBZ0I7RUFDbEI7O0FBRUY7RUFDRSx1QkFBdUI7RUFDdkIsWUFBWTtFQUNaLGVBQWU7RUFDZixXQUFXO0VBQ1gsZUFBZTtBQUNqQjs7QUFFQTtFQUNFLGdCQUFnQjtFQUNoQixPQUFPO0VBQ1AsZ0JBQWdCO0VBQ2hCLGtCQUFrQjtBQUNwQjs7QUFFQTtFQUNFLHlCQUF5QjtFQUN6QixZQUFZO0VBQ1osaUJBQWlCO0VBQ2pCLGVBQWU7QUFDakI7O0FBRUE7RUFDRSxnQkFBZ0I7RUFDaEIsVUFBVTtFQUNWLGdCQUFnQjtBQUNsQjs7QUFFQTtFQUNFLHlCQUF5QjtFQUN6Qix5QkFBeUI7RUFDekIsbUJBQW1CO0VBQ25CLGtCQUFrQjtFQUNsQixtQkFBbUI7RUFDbkIsZUFBZTtFQUNmLGFBQWE7RUFDYiw4QkFBOEI7RUFDOUIsbUJBQW1CO0VBQ25CLHNDQUFzQztFQUN0QyxlQUFlO0FBQ2pCOztFQUVFO0lBQ0UseUJBQXlCO0VBQzNCOztBQUVGO0VBQ0UsZ0JBQWdCO0VBQ2hCLGNBQWM7QUFDaEI7O0FBRUE7RUFDRSxlQUFlO0VBQ2YsV0FBVztFQUNYLGVBQWU7QUFDakI7O0FBRUE7O0VBRUUsa0JBQWtCO0VBQ2xCLHlCQUF5QjtFQUN6QixlQUFlO0FBQ2pCOztFQUVFOztJQUVFLHFCQUFxQjtJQUNyQixpREFBaUQ7RUFDbkQ7QUFDRjtFQUNFLGVBQWU7RUFDZix5QkFBeUI7RUFDekIsa0JBQWtCO0VBQ2xCLG1CQUFtQjtFQUNuQixhQUFhO0VBQ2Isc0NBQXNDO0VBQ3RDLGVBQWU7QUFDakI7O0VBRUU7SUFDRSx5QkFBeUI7RUFDM0I7O0FBRUY7RUFDRSxZQUFZO0VBQ1osbUJBQW1CO0VBQ25CLGdCQUFnQjtBQUNsQjs7QUFFQTtFQUNFLGVBQWU7RUFDZixjQUFjO0VBQ2QsZ0JBQWdCO0VBQ2hCLHVCQUF1QjtFQUN2QixZQUFZO0VBQ1osZUFBZTtFQUNmLDJCQUEyQjtBQUM3QjtFQUNFO0lBQ0UsY0FBYztFQUNoQiIsInNvdXJjZXNDb250ZW50IjpbIi5mbG9hdGluZy1idXR0b24ge1xyXG4gIHBvc2l0aW9uOiBmaXhlZDtcclxuICBib3R0b206IDI1cHg7XHJcbiAgcmlnaHQ6IDI1cHg7XHJcbiAgYmFja2dyb3VuZC1jb2xvcjogIzBkNmVmZDtcclxuICBjb2xvcjogd2hpdGU7XHJcbiAgYm9yZGVyLXJhZGl1czogNTAlO1xyXG4gIHdpZHRoOiA1NXB4O1xyXG4gIGhlaWdodDogNTVweDtcclxuICBmb250LXNpemU6IDI4cHg7XHJcbiAgdGV4dC1hbGlnbjogY2VudGVyO1xyXG4gIGxpbmUtaGVpZ2h0OiA1NXB4O1xyXG4gIGJveC1zaGFkb3c6IDAgNHB4IDEycHggcmdiYSgwLCAwLCAwLCAwLjMpO1xyXG4gIGN1cnNvcjogcG9pbnRlcjtcclxuICB6LWluZGV4OiAxMDAwO1xyXG4gIHRyYW5zaXRpb246IGJhY2tncm91bmQtY29sb3IgMC4zcyBlYXNlO1xyXG59XHJcblxyXG4gIC5mbG9hdGluZy1idXR0b246aG92ZXIge1xyXG4gICAgYmFja2dyb3VuZC1jb2xvcjogIzBiNWVkNztcclxuICB9XHJcblxyXG4ucG9wdXAtYmFja2Ryb3Age1xyXG4gIHBvc2l0aW9uOiBmaXhlZDtcclxuICBib3R0b206IDEwMHB4O1xyXG4gIHJpZ2h0OiAzMHB4O1xyXG4gIHdpZHRoOiA0NjBweDtcclxuICBtYXgtaGVpZ2h0OiA3NXZoO1xyXG4gIGJhY2tncm91bmQtY29sb3I6ICNmZmY7XHJcbiAgYm9yZGVyLXJhZGl1czogMTZweDtcclxuICBib3gtc2hhZG93OiAwIDEwcHggMzBweCByZ2JhKDAsIDAsIDAsIDAuMik7XHJcbiAgb3ZlcmZsb3c6IGhpZGRlbjtcclxuICB6LWluZGV4OiAxMDAxO1xyXG4gIGFuaW1hdGlvbjogZmFkZUluVXAgMC4zcyBlYXNlO1xyXG59XHJcblxyXG5Aa2V5ZnJhbWVzIGZhZGVJblVwIHtcclxuICBmcm9tIHtcclxuICAgIG9wYWNpdHk6IDA7XHJcbiAgICB0cmFuc2Zvcm06IHRyYW5zbGF0ZVkoMjBweCk7XHJcbiAgfVxyXG5cclxuICB0byB7XHJcbiAgICBvcGFjaXR5OiAxO1xyXG4gICAgdHJhbnNmb3JtOiB0cmFuc2xhdGVZKDApO1xyXG4gIH1cclxufVxyXG5cclxuLm5vdGVzLXBvcHVwIHtcclxuICBkaXNwbGF5OiBmbGV4O1xyXG4gIGZsZXgtZGlyZWN0aW9uOiBjb2x1bW47XHJcbiAgaGVpZ2h0OiAxMDAlO1xyXG4gIHBhZGRpbmc6IDE2cHg7XHJcbn1cclxuXHJcbi5wb3B1cC1oZWFkZXIge1xyXG4gIGRpc3BsYXk6IGZsZXg7XHJcbiAganVzdGlmeS1jb250ZW50OiBzcGFjZS1iZXR3ZWVuO1xyXG4gIGFsaWduLWl0ZW1zOiBjZW50ZXI7XHJcbiAgcGFkZGluZy1ib3R0b206IDhweDtcclxuICBib3JkZXItYm90dG9tOiAxcHggc29saWQgI2VhZWFlYTtcclxuICBtYXJnaW4tYm90dG9tOiAxMHB4O1xyXG59XHJcblxyXG4gIC5wb3B1cC1oZWFkZXIgaDUge1xyXG4gICAgbWFyZ2luOiAwO1xyXG4gICAgY29sb3I6ICMwZDZlZmQ7XHJcbiAgICBmb250LXNpemU6IDE4cHg7XHJcbiAgICBmb250LXdlaWdodDogNjAwO1xyXG4gIH1cclxuXHJcbi5pY29uLWJ1dHRvbiB7XHJcbiAgYmFja2dyb3VuZDogdHJhbnNwYXJlbnQ7XHJcbiAgYm9yZGVyOiBub25lO1xyXG4gIGZvbnQtc2l6ZTogMThweDtcclxuICBjb2xvcjogIzY2NjtcclxuICBjdXJzb3I6IHBvaW50ZXI7XHJcbn1cclxuXHJcbi5wb3B1cC1jb250ZW50IHtcclxuICBvdmVyZmxvdy15OiBhdXRvO1xyXG4gIGZsZXg6IDE7XHJcbiAgbWF4LWhlaWdodDogNTV2aDtcclxuICBwYWRkaW5nLXJpZ2h0OiA2cHg7XHJcbn1cclxuXHJcbi5hZGQtbm90ZS1idG4ge1xyXG4gIGJhY2tncm91bmQtY29sb3I6ICMxOTg3NTQ7XHJcbiAgY29sb3I6IHdoaXRlO1xyXG4gIHBhZGRpbmc6IDRweCAxMHB4O1xyXG4gIGZvbnQtc2l6ZTogMTRweDtcclxufVxyXG5cclxuLm5vdGUtbGlzdCB7XHJcbiAgbGlzdC1zdHlsZTogbm9uZTtcclxuICBwYWRkaW5nOiAwO1xyXG4gIG1hcmdpbi10b3A6IDEwcHg7XHJcbn1cclxuXHJcbi5ub3RlLWl0ZW0ge1xyXG4gIGJhY2tncm91bmQtY29sb3I6ICNmOGY5ZmE7XHJcbiAgYm9yZGVyOiAxcHggc29saWQgIzBkNmVmZDtcclxuICBib3JkZXItcmFkaXVzOiAxMHB4O1xyXG4gIHBhZGRpbmc6IDEycHggMTRweDtcclxuICBtYXJnaW4tYm90dG9tOiAxMnB4O1xyXG4gIGN1cnNvcjogcG9pbnRlcjtcclxuICBkaXNwbGF5OiBmbGV4O1xyXG4gIGp1c3RpZnktY29udGVudDogc3BhY2UtYmV0d2VlbjtcclxuICBhbGlnbi1pdGVtczogY2VudGVyO1xyXG4gIHRyYW5zaXRpb246IGJhY2tncm91bmQtY29sb3IgMC4ycyBlYXNlO1xyXG4gIGZvbnQtc2l6ZTogMTVweDtcclxufVxyXG5cclxuICAubm90ZS1pdGVtOmhvdmVyIHtcclxuICAgIGJhY2tncm91bmQtY29sb3I6ICNlN2YxZmY7XHJcbiAgfVxyXG5cclxuLm5vdGUtdGl0bGUge1xyXG4gIGZvbnQtd2VpZ2h0OiA2MDA7XHJcbiAgY29sb3I6ICMwZDZlZmQ7XHJcbn1cclxuXHJcbi5ub3RlLXNuaXBwZXQge1xyXG4gIGZvbnQtc2l6ZTogMTNweDtcclxuICBjb2xvcjogIzU1NTtcclxuICBtYXJnaW4tdG9wOiAycHg7XHJcbn1cclxuXHJcbi5ub3RlLWlucHV0LFxyXG4ubm90ZS10ZXh0YXJlYSB7XHJcbiAgYm9yZGVyLXJhZGl1czogOHB4O1xyXG4gIGJvcmRlcjogMXB4IHNvbGlkICNjZWQ0ZGE7XHJcbiAgZm9udC1zaXplOiAxNHB4O1xyXG59XHJcblxyXG4gIC5ub3RlLWlucHV0OmZvY3VzLFxyXG4gIC5ub3RlLXRleHRhcmVhOmZvY3VzIHtcclxuICAgIGJvcmRlci1jb2xvcjogIzBkNmVmZDtcclxuICAgIGJveC1zaGFkb3c6IDAgMCAwIDAuMnJlbSByZ2JhKDEzLCAxMTAsIDI1MywgMC4yNSk7XHJcbiAgfVxyXG4ubGlzdC1ncm91cC1pdGVtIHtcclxuICBjdXJzb3I6IHBvaW50ZXI7XHJcbiAgYm9yZGVyOiAxcHggc29saWQgIzBkNmVmZDtcclxuICBib3JkZXItcmFkaXVzOiA4cHg7XHJcbiAgbWFyZ2luLWJvdHRvbTogMTJweDtcclxuICBwYWRkaW5nOiAxNnB4O1xyXG4gIHRyYW5zaXRpb246IGJhY2tncm91bmQtY29sb3IgMC4ycyBlYXNlO1xyXG4gIGZvbnQtc2l6ZTogMTZweDtcclxufVxyXG5cclxuICAubGlzdC1ncm91cC1pdGVtOmhvdmVyIHtcclxuICAgIGJhY2tncm91bmQtY29sb3I6ICNlN2YxZmY7XHJcbiAgfVxyXG5cclxuLm5vdGUtcHJldmlldyB7XHJcbiAgZmxleC1ncm93OiAxO1xyXG4gIHBhZGRpbmctcmlnaHQ6IDEwcHg7XHJcbiAgb3ZlcmZsb3c6IGhpZGRlbjtcclxufVxyXG5cclxuLmRlbGV0ZS1pY29uIHtcclxuICBmb250LXNpemU6IDE4cHg7XHJcbiAgY29sb3I6ICNkYzM1NDU7XHJcbiAgcGFkZGluZzogMnB4IDZweDtcclxuICBiYWNrZ3JvdW5kOiB0cmFuc3BhcmVudDtcclxuICBib3JkZXI6IG5vbmU7XHJcbiAgY3Vyc29yOiBwb2ludGVyO1xyXG4gIHRyYW5zaXRpb246IGNvbG9yIDAuMnMgZWFzZTtcclxufVxyXG4gIC5kZWxldGUtaWNvbjpob3ZlciB7XHJcbiAgICBjb2xvcjogI2JiMmQzYjtcclxuICB9XHJcbiJdLCJzb3VyY2VSb290IjoiIn0= */"]
    });
  }
}

/***/ }),

/***/ 4219:
/*!**************************************!*\
  !*** ./src/app/constant/constant.ts ***!
  \**************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   "AppPaths": () => (/* binding */ AppPaths),
/* harmony export */   "AppRoute": () => (/* binding */ AppRoute),
/* harmony export */   "ClaimKey": () => (/* binding */ ClaimKey),
/* harmony export */   "Constant": () => (/* binding */ Constant),
/* harmony export */   "PageTitle": () => (/* binding */ PageTitle)
/* harmony export */ });
class Constant {
  static {
    this.true = 'true';
  }
  static {
    this.actionDBName = 'actionDBName';
  }
  static {
    this.actionTableName = 'action';
  }
  static {
    this.dbVersion = 1.0;
  }
  static {
    this.actionTablePrimaryKey = 'actionId';
  }
  static {
    this.actionCoumns = [{
      columnName: 'actionId',
      isUnique: true
    }, {
      columnName: 'actionName',
      isUnique: false
    }, {
      columnName: 'actionPath',
      isUnique: false
    }, {
      columnName: 'actionType',
      isUnique: false
    }, {
      columnName: 'menuLevel',
      isUnique: false
    }, {
      columnName: 'parrentMenuId',
      isUnique: false
    }, {
      columnName: 'sequence',
      isUnique: false
    }];
  }
  static {
    this.yesOrNo = [{
      key: true,
      value: 'Yes'
    }, {
      key: false,
      value: 'No'
    }];
  }
}
class AppPaths {
  static {
    this.home = 'home';
  }
  static {
    this.login = 'login';
  }
  //role
  static {
    this.addEditRole = 'addEditRole';
  }
  static {
    this.roleList = 'roleList';
  }
  //user
  static {
    this.userList = 'userList';
  }
  static {
    this.addEditUser = 'addEditUser';
  }
  //Account
  static {
    this.createAccount = 'createAccount';
  }
  static {
    this.transactions = 'transactions';
  }
  static {
    this.transfer = 'transfer';
  }
  static {
    this.selfTransfer = 'selfTransfer';
  }
  static {
    this.depositAmount = 'depositAmount';
  }
  static {
    this.cashWithdraw = 'cashWithdraw';
  }
  //Budget
  static {
    this.createBudgetAccount = 'createBudgetAccount';
  }
  static {
    this.budgetTransactionList = 'budgetTransactionList/:id';
  }
  static {
    this.budgetList = 'budgetList';
  }
  static {
    this.budgetTransfer = 'budgetTransfer';
  }
  static {
    this.budgetPlannerList = 'budgetPlannerList';
  }
  static {
    this.createBudgetPlanner = 'budgetPlanner/create';
  }
  static {
    this.editBudgetPlanner = 'budgetPlanner/edit/:id';
  }
  static {
    this.budgetPlannerDetail = 'budgetPlanner/detail/:id';
  }
  //Notification
  static {
    this.budgetNotification = 'budgetNotification';
  }
  static {
    this.accountNotification = 'accountNotification';
  }
  //Report
  static {
    this.accountStatementReport = 'accountStatementReport';
  }
  static {
    this.budgetStatementReport = 'budgetStatementReport';
  }
  //Calender
  static {
    this.calender = 'calender';
  }
  //Wallet
  static {
    this.createWallet = 'createWallet';
  }
  static {
    this.walletTransactionList = 'walletTransactionList/:id';
  }
  static {
    this.walletList = 'walletList';
  }
  static {
    this.walletTransfer = 'walletTransfer';
  }
  static {
    this.walletDeposit = 'depositWallet';
  }
  //Bulk Transaction
  static {
    this.bulkTransaction = 'bulkTransaction';
  }
  //Mutual Funds
  static {
    this.mutualFundsList = 'mutualFundsList';
  }
  static {
    this.investment = 'investment';
  }
  static {
    this.investmentProfile = 'userInvestmentProflie';
  }
  //Credit Card
  static {
    this.creditCardApplication = 'creditCardApplication';
  }
  static {
    this.creditCardDashboard = 'creditCardDashboard';
  }
  static {
    this.creditCardTransactions = 'creditCardTransactions/:cardId';
  }
  static {
    this.creditCardPayment = 'creditCardPayment';
  }
  static {
    this.creditCardStatement = 'creditCardStatement/:cardId';
  }
  static {
    this.creditCardManagement = 'creditCardManagement';
  }
}
class AppRoute {
  static {
    this.home = '/home';
  }
  static {
    this.login = '/login';
  }
  //role
  static {
    this.addEditRole = '/addEditRole';
  }
  static {
    this.roleList = '/roleList';
  }
  //user
  static {
    this.userList = '/userList';
  }
  static {
    this.addEditUser = '/addEditUser';
  }
  //Account
  static {
    this.createAccount = '/createAccount';
  }
  static {
    this.transactions = '/transactions';
  }
  static {
    this.transfer = '/transfer';
  }
  static {
    this.selfTransfer = '/selfTransfer';
  }
  static {
    this.depositAmount = '/depositAmount';
  }
  static {
    this.cashWithdraw = '/cashWithdraw';
  }
  // Budget
  static {
    this.createBudgetAccount = '/createBudgetAccount';
  }
  static {
    this.budgetList = '/budgetList';
  }
  static {
    this.budgetTransfer = '/budgetTransfer';
  }
  static {
    this.budgetTransactionList = id => {
      return `/budgetTransactionList/${id}`;
    };
  }
  static {
    this.budgetPlannerList = '/budgetPlannerList';
  }
  static {
    this.createBudgetPlanner = () => {
      return '/budgetPlanner/create';
    };
  }
  static {
    this.editBudgetPlanner = id => {
      return `/budgetPlanner/edit/${id}`;
    };
  }
  static {
    this.budgetPlannerDetail = id => {
      return `/budgetPlanner/detail/${id}`;
    };
  }
  //Notification
  static {
    this.budgetNotification = '/budgetNotification';
  }
  static {
    this.accountNotification = '/accountNotification';
  }
  //Report
  static {
    this.accountStatementReport = '/accountStatementReport';
  }
  static {
    this.budgetStatementReport = '/budgetStatementReport';
  }
  //Calender
  static {
    this.calender = '/calender';
  }
  //Wallet
  static {
    this.createWallet = '/createWallet';
  }
  static {
    this.walletTransactionList = id => {
      return `/walletTransactionList/${id}`;
    };
  }
  static {
    this.walletList = '/walletList';
  }
  static {
    this.walletTransfer = '/walletTransfer';
  }
  static {
    this.walletDeposit = '/depositWallet';
  }
  //Bulk Transaction
  static {
    this.bulkTransaction = '/bulkTransaction';
  }
  //Mutual Funds
  static {
    this.mutualFundsList = '/mutualFundsList';
  }
  //Credit Card
  static {
    this.creditCardApplication = '/creditCardApplication';
  }
  static {
    this.creditCardDashboard = '/creditCardDashboard';
  }
  static {
    this.creditCardTransactions = cardId => {
      return `/creditCardTransactions/${cardId}`;
    };
  }
  static {
    this.creditCardPayment = '/creditCardPayment';
  }
  static {
    this.creditCardStatement = cardId => {
      return `/creditCardStatement/${cardId}`;
    };
  }
  static {
    this.creditCardManagement = '/creditCardManagement';
  }
}
class PageTitle {
  static {
    this.home = 'Home';
  }
  static {
    this.login = 'Login';
  }
  //role
  static {
    this.addEditRole = 'Add Edit Role';
  }
  static {
    this.roleList = 'Role List';
  }
  //user
  static {
    this.userList = 'User List';
  }
  static {
    this.addEditUser = 'Add Edit User';
  }
  //Account
  static {
    this.createAccount = 'Create Account';
  }
  static {
    this.transactions = 'Transactions List';
  }
  static {
    this.transfer = 'Transfer Amount';
  }
  static {
    this.selfTransfer = 'Self Transfer';
  }
  static {
    this.depositAmount = 'Deposit Amount';
  }
  static {
    this.cashWithdraw = 'Cash Withdraw';
  }
  //Budget
  static {
    this.createBudgetAccount = 'Create Budget Account';
  }
  static {
    this.budgetTransactionList = 'Budget Transaction List';
  }
  static {
    this.budgetList = 'Budget List';
  }
  static {
    this.budgetTransfer = 'Budget Transfer';
  }
  static {
    this.budgetPlannerList = 'Budget Planner List';
  }
  static {
    this.budgetPlannerForm = 'Budget Planner';
  }
  static {
    this.budgetPlannerDetail = 'Budget Planner Detail';
  }
  // Notification
  static {
    this.budgetNotification = 'Budget Notification';
  }
  static {
    this.accountNotification = 'Account Notification';
  }
  //Report
  static {
    this.accountStatementReport = 'Account Statement Report';
  }
  static {
    this.budgetStatementReport = 'Budget Statement Report';
  }
  //Calender
  static {
    this.calender = 'Calender';
  }
  //Wallet
  static {
    this.createWallet = 'Create Wallet';
  }
  static {
    this.walletTransactionList = 'Wallet Transaction List';
  }
  static {
    this.walletList = 'Wallet List';
  }
  static {
    this.walletTransfer = 'Wallet Transfer';
  }
  static {
    this.walletDeposit = 'Wallet Deposit';
  }
  //Bulk Transaction
  static {
    this.bulkTransaction = 'Bulk Transaction';
  }
  //Mutual Funds
  static {
    this.mutualFundsList = 'Mutual Funds List';
  }
  static {
    this.investment = 'Investment';
  }
  static {
    this.investmentProfile = 'Investment Profile';
  }
  //Credit Card
  static {
    this.creditCardApplication = 'Credit Card Application';
  }
  static {
    this.creditCardDashboard = 'Credit Card Dashboard';
  }
  static {
    this.creditCardTransactions = 'Credit Card Transactions';
  }
  static {
    this.creditCardPayment = 'Credit Card Payment';
  }
  static {
    this.creditCardStatement = 'Credit Card Statement';
  }
  static {
    this.creditCardManagement = 'Credit Card Management';
  }
}
class ClaimKey {
  static {
    this.accessToken = 'accessToken';
  }
  static {
    this.isLoggedIn = 'isLoggedIn';
  }
  static {
    this.userName = 'UserName';
  }
  static {
    this.idb = 'idb';
  }
  static {
    this.customerId = 'CustomerId';
  }
  static {
    this.accountId = 'AccountId';
  }
  static {
    this.staffId = 'StaffId';
  }
}

/***/ }),

/***/ 5381:
/*!*******************************************************************!*\
  !*** ./src/app/core/component/data-table/data-table.component.ts ***!
  \*******************************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   "DataTableComponent": () => (/* binding */ DataTableComponent)
/* harmony export */ });
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/common */ 4666);
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ 2560);
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/forms */ 2508);





function DataTableComponent_div_0_Template(rf, ctx) {
  if (rf & 1) {
    const _r7 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵgetCurrentView"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "div", 8)(1, "button", 9);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("click", function DataTableComponent_div_0_Template_button_click_1_listener() {
      _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵrestoreView"](_r7);
      const ctx_r6 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"]();
      return _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵresetView"](ctx_r6.exportToCSV());
    });
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelement"](2, "i", 10);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](3, " Export to Excel ");
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](4, "div", 11)(5, "input", 12);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("ngModelChange", function DataTableComponent_div_0_Template_input_ngModelChange_5_listener($event) {
      _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵrestoreView"](_r7);
      const ctx_r8 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"]();
      return _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵresetView"](ctx_r8.searchTerm = $event);
    })("input", function DataTableComponent_div_0_Template_input_input_5_listener() {
      _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵrestoreView"](_r7);
      const ctx_r9 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"]();
      return _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵresetView"](ctx_r9.onSearchChange());
    });
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](6, "span", 13);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelement"](7, "i", 14);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]()()();
  }
  if (rf & 2) {
    const ctx_r0 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](5);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngModel", ctx_r0.searchTerm);
  }
}
function DataTableComponent_th_5_span_3_i_1_Template(rf, ctx) {
  if (rf & 1) {
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelement"](0, "i", 20);
  }
}
function DataTableComponent_th_5_span_3_i_2_Template(rf, ctx) {
  if (rf & 1) {
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelement"](0, "i", 21);
  }
}
function DataTableComponent_th_5_span_3_Template(rf, ctx) {
  if (rf & 1) {
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "span");
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtemplate"](1, DataTableComponent_th_5_span_3_i_1_Template, 1, 0, "i", 18);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtemplate"](2, DataTableComponent_th_5_span_3_i_2_Template, 1, 0, "i", 19);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
  }
  if (rf & 2) {
    const col_r10 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"]().$implicit;
    const ctx_r11 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngIf", ctx_r11.sortColumn === col_r10.field && ctx_r11.sortDirection === "asc");
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngIf", ctx_r11.sortColumn === col_r10.field && ctx_r11.sortDirection === "desc");
  }
}
function DataTableComponent_th_5_Template(rf, ctx) {
  if (rf & 1) {
    const _r16 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵgetCurrentView"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "th", 15)(1, "div", 16);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("click", function DataTableComponent_th_5_Template_div_click_1_listener() {
      const restoredCtx = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵrestoreView"](_r16);
      const col_r10 = restoredCtx.$implicit;
      const ctx_r15 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"]();
      return _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵresetView"](ctx_r15.changeSort(col_r10));
    });
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](2);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtemplate"](3, DataTableComponent_th_5_span_3_Template, 3, 2, "span", 17);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]()();
  }
  if (rf & 2) {
    const col_r10 = ctx.$implicit;
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵstyleProp"]("width", col_r10.width);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵstyleProp"]("cursor", col_r10.sortable ? "pointer" : "default");
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtextInterpolate1"](" ", col_r10.header, " ");
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngIf", col_r10.sortable);
  }
}
function DataTableComponent_tbody_6_tr_1_td_1_Template(rf, ctx) {
  if (rf & 1) {
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "td", 15);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
  }
  if (rf & 2) {
    const col_r20 = ctx.$implicit;
    const row_r18 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"]().$implicit;
    const ctx_r19 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"](2);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtextInterpolate1"](" ", ctx_r19.formatCell(row_r18[col_r20.field], col_r20), " ");
  }
}
function DataTableComponent_tbody_6_tr_1_Template(rf, ctx) {
  if (rf & 1) {
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "tr");
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtemplate"](1, DataTableComponent_tbody_6_tr_1_td_1_Template, 2, 1, "td", 23);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
  }
  if (rf & 2) {
    const ctx_r17 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"](2);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngForOf", ctx_r17.columns);
  }
}
function DataTableComponent_tbody_6_Template(rf, ctx) {
  if (rf & 1) {
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "tbody");
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtemplate"](1, DataTableComponent_tbody_6_tr_1_Template, 2, 1, "tr", 22);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
  }
  if (rf & 2) {
    const ctx_r2 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngForOf", ctx_r2.visibleData);
  }
}
function DataTableComponent_ng_template_7_Template(rf, ctx) {
  if (rf & 1) {
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "tr")(1, "td", 24);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](2, "No records found");
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]()();
  }
  if (rf & 2) {
    const ctx_r4 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵattribute"]("colspan", ctx_r4.columns.length);
  }
}
function DataTableComponent_div_9_option_9_Template(rf, ctx) {
  if (rf & 1) {
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "option", 34);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
  }
  if (rf & 2) {
    const size_r24 = ctx.$implicit;
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("value", size_r24);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtextInterpolate"](size_r24);
  }
}
const _c0 = function (a0, a1, a2) {
  return {
    "btn-primary": a0,
    "btn-outline-light": a1,
    "btn-outline-secondary": a2
  };
};
function DataTableComponent_div_9_ng_container_15_Template(rf, ctx) {
  if (rf & 1) {
    const _r28 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵgetCurrentView"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementContainerStart"](0);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](1, "button", 35);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("click", function DataTableComponent_div_9_ng_container_15_Template_button_click_1_listener() {
      const restoredCtx = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵrestoreView"](_r28);
      const i_r26 = restoredCtx.index;
      const ctx_r27 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"](2);
      return _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵresetView"](ctx_r27.changePage(i_r26 + 1));
    });
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](2);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementContainerEnd"]();
  }
  if (rf & 2) {
    const i_r26 = ctx.index;
    const ctx_r23 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"](2);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngClass", _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpureFunction3"](2, _c0, ctx_r23.currentPage === i_r26 + 1, ctx_r23.darkMode && ctx_r23.currentPage !== i_r26 + 1, !ctx_r23.darkMode && ctx_r23.currentPage !== i_r26 + 1));
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtextInterpolate1"](" ", i_r26 + 1, " ");
  }
}
const _c1 = function (a0) {
  return {
    "bg-dark text-light border-secondary": a0
  };
};
const _c2 = function (a0, a1) {
  return {
    "btn-outline-light": a0,
    "btn-outline-secondary": a1
  };
};
const _c3 = function () {
  return [];
};
function DataTableComponent_div_9_Template(rf, ctx) {
  if (rf & 1) {
    const _r30 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵgetCurrentView"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "div", 25)(1, "div", 26)(2, "div", 27);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](3);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](4, "div", 8)(5, "div", 28)(6, "label", 29);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](7, "Show");
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](8, "select", 30);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("ngModelChange", function DataTableComponent_div_9_Template_select_ngModelChange_8_listener($event) {
      _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵrestoreView"](_r30);
      const ctx_r29 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"]();
      return _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵresetView"](ctx_r29.pageSize = $event);
    })("change", function DataTableComponent_div_9_Template_select_change_8_listener() {
      _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵrestoreView"](_r30);
      const ctx_r31 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"]();
      return _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵresetView"](ctx_r31.changePageSize());
    });
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtemplate"](9, DataTableComponent_div_9_option_9_Template, 2, 2, "option", 31);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](10, "span", 27);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](11, "entries");
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]()()()();
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](12, "div", 32)(13, "button", 33);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("click", function DataTableComponent_div_9_Template_button_click_13_listener() {
      _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵrestoreView"](_r30);
      const ctx_r32 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"]();
      return _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵresetView"](ctx_r32.changePage(ctx_r32.currentPage - 1));
    });
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](14, " \u2039 Prev ");
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtemplate"](15, DataTableComponent_div_9_ng_container_15_Template, 3, 6, "ng-container", 22);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](16, "button", 33);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("click", function DataTableComponent_div_9_Template_button_click_16_listener() {
      _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵrestoreView"](_r30);
      const ctx_r33 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"]();
      return _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵresetView"](ctx_r33.changePage(ctx_r33.currentPage + 1));
    });
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](17, " Next \u203A ");
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]()()();
  }
  if (rf & 2) {
    const ctx_r5 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngClass", ctx_r5.darkMode ? "text-light" : "text-dark");
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](3);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtextInterpolate3"](" Showing ", (ctx_r5.currentPage - 1) * ctx_r5.pageSize + 1, " to ", ctx_r5.math.min(ctx_r5.currentPage * ctx_r5.pageSize, ctx_r5.data.length), " of ", ctx_r5.data.length, " entries ");
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](5);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngClass", _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpureFunction1"](12, _c1, ctx_r5.darkMode))("ngModel", ctx_r5.pageSize);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngForOf", ctx_r5.pageSizeOptions);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](4);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngClass", _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpureFunction2"](14, _c2, ctx_r5.darkMode, !ctx_r5.darkMode))("disabled", ctx_r5.currentPage === 1);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](2);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngForOf", _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpureFunction0"](17, _c3).constructor(ctx_r5.totalPages()));
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngClass", _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpureFunction2"](18, _c2, ctx_r5.darkMode, !ctx_r5.darkMode))("disabled", ctx_r5.currentPage === ctx_r5.totalPages());
  }
}
const _c4 = function (a0, a1) {
  return {
    "bg-dark text-light": a0,
    "bg-white text-dark": a1
  };
};
const _c5 = function (a0, a1) {
  return {
    "table-dark table-striped-columns": a0,
    "table-striped": a1
  };
};
const _c6 = function (a0, a1) {
  return {
    "table-light": a0,
    "table-dark text-white": a1
  };
};
class DataTableComponent {
  constructor() {
    this.columns = [];
    this.data = [];
    this.darkMode = false;
    this.pageChange = new _angular_core__WEBPACK_IMPORTED_MODULE_0__.EventEmitter();
    this.currentPage = 1;
    this.sortColumn = '';
    this.sortDirection = 'asc';
    this.math = Math;
    this.pageSizeOptions = [10, 25, 50, 100];
    this.pageSize = 10;
    this.searchTerm = '';
    this.filteredData = [];
  }
  changePageSize() {
    this.currentPage = 1;
  }
  get visibleData() {
    const start = (this.currentPage - 1) * this.pageSize;
    const end = start + this.pageSize;
    return this.sortedData.slice(start, end);
  }
  ngOnChanges() {
    this.applyFilter(); // when @Input() data changes
  }

  ngOnInit() {
    this.filteredData = [...this.data];
  }
  onSearchChange() {
    this.currentPage = 1;
    this.applyFilter();
  }
  applyFilter() {
    if (!this.searchTerm || this.searchTerm.trim() === '') {
      this.filteredData = [...this.data];
    } else {
      const lowerTerm = this.searchTerm.toLowerCase();
      this.filteredData = this.data.filter(row => this.columns.some(col => {
        const cellValue = row[col.field];
        return cellValue && cellValue.toString().toLowerCase().includes(lowerTerm);
      }));
    }
  }
  get sortedData() {
    let sorted = [...this.filteredData];
    if (this.sortColumn) {
      sorted.sort((a, b) => {
        const valueA = a[this.sortColumn];
        const valueB = b[this.sortColumn];
        return (valueA > valueB ? 1 : -1) * (this.sortDirection === 'asc' ? 1 : -1);
      });
    }
    return sorted;
  }
  changeSort(col) {
    if (!col.sortable) return;
    if (this.sortColumn === col.field) {
      this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
    } else {
      this.sortColumn = col.field;
      this.sortDirection = 'asc';
    }
  }
  totalPages() {
    return Math.ceil(this.data.length / this.pageSize);
  }
  changePage(page) {
    this.currentPage = page;
    this.pageChange.emit(this.currentPage);
  }
  formatCell(value, col) {
    if (col.format === 'date' && value) {
      return (0,_angular_common__WEBPACK_IMPORTED_MODULE_1__.formatDate)(value, col.dateFormat || 'dd/MM/yyyy', 'en-US');
    }
    if (col.format === 'currency' && value) {
      return (0,_angular_common__WEBPACK_IMPORTED_MODULE_1__.formatCurrency)(value, 'en-US', '', 'symbol', '1.2-2');
    }
    return value;
  }
  exportToCSV() {
    const rows = [this.columns.map(col => col.header)]; // headers
    for (const row of this.data) {
      const rowData = this.columns.map(col => {
        const raw = row[col.field];
        const formatted = this.formatCell(raw, col);
        return `"${(formatted ?? '').toString().replace(/"/g, '""')}"`; // Escape quotes
      });

      rows.push(rowData);
    }
    const csvContent = rows.map(e => e.join(',')).join('\n');
    const blob = new Blob([csvContent], {
      type: 'text/csv;charset=utf-8;'
    });
    const link = document.createElement('a');
    const url = URL.createObjectURL(blob);
    link.href = url;
    link.download = 'transactions.csv';
    link.click();
    URL.revokeObjectURL(url);
  }
  static {
    this.ɵfac = function DataTableComponent_Factory(t) {
      return new (t || DataTableComponent)();
    };
  }
  static {
    this.ɵcmp = /*@__PURE__*/_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineComponent"]({
      type: DataTableComponent,
      selectors: [["app-data-table"]],
      inputs: {
        columns: "columns",
        data: "data",
        darkMode: "darkMode"
      },
      outputs: {
        pageChange: "pageChange"
      },
      features: [_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵNgOnChangesFeature"]],
      decls: 10,
      vars: 17,
      consts: [["class", "d-flex justify-content-between align-items-center mb-2 flex-wrap gap-2", 4, "ngIf"], [1, "table-responsive", "rounded", "shadow-sm", 3, "ngClass"], [1, "table", "table-hover", "align-middle", "mb-0", 3, "ngClass"], [1, "sticky-top", 3, "ngClass"], ["class", "text-nowrap", 3, "width", 4, "ngFor", "ngForOf"], [4, "ngIf", "ngIfElse"], ["noData", ""], ["class", "d-flex justify-content-between align-items-center mt-3 flex-wrap gap-2", 3, "ngClass", 4, "ngIf"], [1, "d-flex", "justify-content-between", "align-items-center", "mb-2", "flex-wrap", "gap-2"], [1, "btn", "btn-sm", "btn-outline-success", 3, "click"], [1, "bi", "bi-file-earmark-excel", "me-1"], [1, "input-group", "input-group-sm", 2, "max-width", "300px"], ["type", "text", "placeholder", "Search...", 1, "form-control", 3, "ngModel", "ngModelChange", "input"], [1, "input-group-text"], [1, "bi", "bi-search"], [1, "text-nowrap"], [1, "d-flex", "align-items-center", "gap-1", 3, "click"], [4, "ngIf"], ["class", "bi bi-caret-up-fill text-primary", 4, "ngIf"], ["class", "bi bi-caret-down-fill text-primary", 4, "ngIf"], [1, "bi", "bi-caret-up-fill", "text-primary"], [1, "bi", "bi-caret-down-fill", "text-primary"], [4, "ngFor", "ngForOf"], ["class", "text-nowrap", 4, "ngFor", "ngForOf"], [1, "text-center"], [1, "d-flex", "justify-content-between", "align-items-center", "mt-3", "flex-wrap", "gap-2", 3, "ngClass"], [1, "d-flex", "align-items-center", "gap-3", "flex-wrap"], [1, "small"], [1, "d-flex", "align-items-center", "gap-1"], ["for", "pageSizeSelect", 1, "form-label", "m-0", "small"], ["id", "pageSizeSelect", 1, "form-select", "form-select-sm", 2, "width", "auto", 3, "ngClass", "ngModel", "ngModelChange", "change"], [3, "value", 4, "ngFor", "ngForOf"], [1, "d-flex", "gap-2", "flex-wrap"], [1, "btn", "btn-sm", 3, "ngClass", "disabled", "click"], [3, "value"], [1, "btn", "btn-sm", 3, "ngClass", "click"]],
      template: function DataTableComponent_Template(rf, ctx) {
        if (rf & 1) {
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtemplate"](0, DataTableComponent_div_0_Template, 8, 1, "div", 0);
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](1, "div", 1)(2, "table", 2)(3, "thead", 3)(4, "tr");
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtemplate"](5, DataTableComponent_th_5_Template, 4, 6, "th", 4);
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]()();
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtemplate"](6, DataTableComponent_tbody_6_Template, 2, 1, "tbody", 5);
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtemplate"](7, DataTableComponent_ng_template_7_Template, 3, 1, "ng-template", null, 6, _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtemplateRefExtractor"]);
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtemplate"](9, DataTableComponent_div_9_Template, 18, 21, "div", 7);
        }
        if (rf & 2) {
          const _r3 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵreference"](8);
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngIf", ctx.data.length > 0);
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngClass", _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpureFunction2"](8, _c4, ctx.darkMode, !ctx.darkMode));
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngClass", _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpureFunction2"](11, _c5, ctx.darkMode, !ctx.darkMode));
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngClass", _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵpureFunction2"](14, _c6, !ctx.darkMode, ctx.darkMode));
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](2);
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngForOf", ctx.columns);
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngIf", ctx.visibleData && ctx.visibleData.length)("ngIfElse", _r3);
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](3);
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngIf", ctx.data.length > 0);
        }
      },
      dependencies: [_angular_common__WEBPACK_IMPORTED_MODULE_1__.NgClass, _angular_common__WEBPACK_IMPORTED_MODULE_1__.NgForOf, _angular_common__WEBPACK_IMPORTED_MODULE_1__.NgIf, _angular_forms__WEBPACK_IMPORTED_MODULE_2__.NgSelectOption, _angular_forms__WEBPACK_IMPORTED_MODULE_2__["ɵNgSelectMultipleOption"], _angular_forms__WEBPACK_IMPORTED_MODULE_2__.DefaultValueAccessor, _angular_forms__WEBPACK_IMPORTED_MODULE_2__.SelectControlValueAccessor, _angular_forms__WEBPACK_IMPORTED_MODULE_2__.NgControlStatus, _angular_forms__WEBPACK_IMPORTED_MODULE_2__.NgModel],
      styles: [".table[_ngcontent-%COMP%] {\r\n  border-collapse: separate;\r\n  border-spacing: 0;\r\n}\r\n\r\n  .table[_ngcontent-%COMP%]   td[_ngcontent-%COMP%], .table[_ngcontent-%COMP%]   th[_ngcontent-%COMP%] {\r\n    vertical-align: middle;\r\n    white-space: nowrap;\r\n  }\r\n\r\n  .table[_ngcontent-%COMP%]   thead[_ngcontent-%COMP%]   th[_ngcontent-%COMP%] {\r\n    position: sticky;\r\n    top: 0;\r\n    z-index: 1;\r\n    box-shadow: 0 1px 0 #dee2e6;\r\n  }\r\n\r\n  \r\n  .table[_ngcontent-%COMP%]:not(.table-dark)   thead[_ngcontent-%COMP%]   th[_ngcontent-%COMP%] {\r\n    background-color: #f8f9fa;\r\n  }\r\n\r\n\r\n.table-dark[_ngcontent-%COMP%]   thead[_ngcontent-%COMP%]   th[_ngcontent-%COMP%] {\r\n  background-color: #212529; \r\n  color: #fff !important;\r\n}\r\n\r\nth[_ngcontent-%COMP%] {\r\n  position: relative;\r\n  white-space: nowrap;\r\n}\r\n\r\n  th[_ngcontent-%COMP%]   span[_ngcontent-%COMP%] {\r\n    -webkit-user-select: none;\r\n            user-select: none;\r\n  }\r\n.table-dark[_ngcontent-%COMP%]   thead[_ngcontent-%COMP%]   th[_ngcontent-%COMP%] {\r\n  color: white !important;\r\n}\r\n.pagination.bg-dark[_ngcontent-%COMP%]   .page-link[_ngcontent-%COMP%] {\r\n  background-color: #212529;\r\n  color: #f8f9fa;\r\n  border-color: #343a40;\r\n}\r\n\r\n  .pagination.bg-dark[_ngcontent-%COMP%]   .page-link[_ngcontent-%COMP%]:hover {\r\n    background-color: #343a40;\r\n    color: #ffffff;\r\n  }\r\n\r\n.pagination.bg-dark[_ngcontent-%COMP%]   .page-item.active[_ngcontent-%COMP%]   .page-link[_ngcontent-%COMP%] {\r\n  background-color: #0d6efd; \r\n  border-color: #0d6efd;\r\n  color: #ffffff;\r\n}\r\n\r\n.pagination.bg-dark[_ngcontent-%COMP%]   .page-item.disabled[_ngcontent-%COMP%]   .page-link[_ngcontent-%COMP%] {\r\n  background-color: #212529;\r\n  color: #6c757d;\r\n  border-color: #343a40;\r\n}\r\n.pagination.bg-white[_ngcontent-%COMP%]   .page-link[_ngcontent-%COMP%] {\r\n  color: #212529;\r\n}\r\n\r\n.pagination.bg-white[_ngcontent-%COMP%]   .page-item.active[_ngcontent-%COMP%]   .page-link[_ngcontent-%COMP%] {\r\n  background-color: #0d6efd;\r\n  border-color: #0d6efd;\r\n  color: #fff;\r\n}\r\nbutton.btn-sm[_ngcontent-%COMP%] {\r\n  min-width: 36px;\r\n}\r\n\r\n\r\n.btn-outline-light[_ngcontent-%COMP%] {\r\n  color: #f8f9fa;\r\n  border-color: #6c757d;\r\n}\r\n\r\n  .btn-outline-light[_ngcontent-%COMP%]:hover {\r\n    background-color: #343a40;\r\n    border-color: #adb5bd;\r\n    color: #ffffff;\r\n  }\r\n\r\n\r\n.btn.btn-primary[_ngcontent-%COMP%] {\r\n  background-color: #0d6efd;\r\n  border-color: #0d6efd;\r\n  color: #fff;\r\n}\r\n\r\n\r\n.btn-outline-light[_ngcontent-%COMP%]:disabled {\r\n  background-color: transparent;\r\n  color: #6c757d;\r\n  border-color: #6c757d;\r\n  opacity: 0.6;\r\n}\n/*# sourceMappingURL=data:application/json;charset=utf-8;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbIndlYnBhY2s6Ly8uL3NyYy9hcHAvY29yZS9jb21wb25lbnQvZGF0YS10YWJsZS9kYXRhLXRhYmxlLmNvbXBvbmVudC5jc3MiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IkFBQUE7RUFDRSx5QkFBeUI7RUFDekIsaUJBQWlCO0FBQ25COztFQUVFOztJQUVFLHNCQUFzQjtJQUN0QixtQkFBbUI7RUFDckI7O0VBRUE7SUFDRSxnQkFBZ0I7SUFDaEIsTUFBTTtJQUNOLFVBQVU7SUFDViwyQkFBMkI7RUFDN0I7O0VBRUEsZ0NBQWdDO0VBQ2hDO0lBQ0UseUJBQXlCO0VBQzNCOztBQUVGLDhCQUE4QjtBQUM5QjtFQUNFLHlCQUF5QixFQUFFLHNCQUFzQjtFQUNqRCxzQkFBc0I7QUFDeEI7O0FBRUE7RUFDRSxrQkFBa0I7RUFDbEIsbUJBQW1CO0FBQ3JCOztFQUVFO0lBQ0UseUJBQWlCO1lBQWpCLGlCQUFpQjtFQUNuQjtBQUNGO0VBQ0UsdUJBQXVCO0FBQ3pCO0FBQ0E7RUFDRSx5QkFBeUI7RUFDekIsY0FBYztFQUNkLHFCQUFxQjtBQUN2Qjs7RUFFRTtJQUNFLHlCQUF5QjtJQUN6QixjQUFjO0VBQ2hCOztBQUVGO0VBQ0UseUJBQXlCLEVBQUUsc0JBQXNCO0VBQ2pELHFCQUFxQjtFQUNyQixjQUFjO0FBQ2hCOztBQUVBO0VBQ0UseUJBQXlCO0VBQ3pCLGNBQWM7RUFDZCxxQkFBcUI7QUFDdkI7QUFDQTtFQUNFLGNBQWM7QUFDaEI7O0FBRUE7RUFDRSx5QkFBeUI7RUFDekIscUJBQXFCO0VBQ3JCLFdBQVc7QUFDYjtBQUNBO0VBQ0UsZUFBZTtBQUNqQjs7QUFFQSwyQkFBMkI7QUFDM0I7RUFDRSxjQUFjO0VBQ2QscUJBQXFCO0FBQ3ZCOztFQUVFO0lBQ0UseUJBQXlCO0lBQ3pCLHFCQUFxQjtJQUNyQixjQUFjO0VBQ2hCOztBQUVGLDZCQUE2QjtBQUM3QjtFQUNFLHlCQUF5QjtFQUN6QixxQkFBcUI7RUFDckIsV0FBVztBQUNiOztBQUVBLGlDQUFpQztBQUNqQztFQUNFLDZCQUE2QjtFQUM3QixjQUFjO0VBQ2QscUJBQXFCO0VBQ3JCLFlBQVk7QUFDZCIsInNvdXJjZXNDb250ZW50IjpbIi50YWJsZSB7XHJcbiAgYm9yZGVyLWNvbGxhcHNlOiBzZXBhcmF0ZTtcclxuICBib3JkZXItc3BhY2luZzogMDtcclxufVxyXG5cclxuICAudGFibGUgdGQsXHJcbiAgLnRhYmxlIHRoIHtcclxuICAgIHZlcnRpY2FsLWFsaWduOiBtaWRkbGU7XHJcbiAgICB3aGl0ZS1zcGFjZTogbm93cmFwO1xyXG4gIH1cclxuXHJcbiAgLnRhYmxlIHRoZWFkIHRoIHtcclxuICAgIHBvc2l0aW9uOiBzdGlja3k7XHJcbiAgICB0b3A6IDA7XHJcbiAgICB6LWluZGV4OiAxO1xyXG4gICAgYm94LXNoYWRvdzogMCAxcHggMCAjZGVlMmU2O1xyXG4gIH1cclxuXHJcbiAgLyogTGlnaHQgbW9kZSBoZWFkZXIgKGRlZmF1bHQpICovXHJcbiAgLnRhYmxlOm5vdCgudGFibGUtZGFyaykgdGhlYWQgdGgge1xyXG4gICAgYmFja2dyb3VuZC1jb2xvcjogI2Y4ZjlmYTtcclxuICB9XHJcblxyXG4vKiBEYXJrIG1vZGUgaGVhZGVyIG92ZXJyaWRlICovXHJcbi50YWJsZS1kYXJrIHRoZWFkIHRoIHtcclxuICBiYWNrZ3JvdW5kLWNvbG9yOiAjMjEyNTI5OyAvKiBCb290c3RyYXAgZGFyayBiZyAqL1xyXG4gIGNvbG9yOiAjZmZmICFpbXBvcnRhbnQ7XHJcbn1cclxuXHJcbnRoIHtcclxuICBwb3NpdGlvbjogcmVsYXRpdmU7XHJcbiAgd2hpdGUtc3BhY2U6IG5vd3JhcDtcclxufVxyXG5cclxuICB0aCBzcGFuIHtcclxuICAgIHVzZXItc2VsZWN0OiBub25lO1xyXG4gIH1cclxuLnRhYmxlLWRhcmsgdGhlYWQgdGgge1xyXG4gIGNvbG9yOiB3aGl0ZSAhaW1wb3J0YW50O1xyXG59XHJcbi5wYWdpbmF0aW9uLmJnLWRhcmsgLnBhZ2UtbGluayB7XHJcbiAgYmFja2dyb3VuZC1jb2xvcjogIzIxMjUyOTtcclxuICBjb2xvcjogI2Y4ZjlmYTtcclxuICBib3JkZXItY29sb3I6ICMzNDNhNDA7XHJcbn1cclxuXHJcbiAgLnBhZ2luYXRpb24uYmctZGFyayAucGFnZS1saW5rOmhvdmVyIHtcclxuICAgIGJhY2tncm91bmQtY29sb3I6ICMzNDNhNDA7XHJcbiAgICBjb2xvcjogI2ZmZmZmZjtcclxuICB9XHJcblxyXG4ucGFnaW5hdGlvbi5iZy1kYXJrIC5wYWdlLWl0ZW0uYWN0aXZlIC5wYWdlLWxpbmsge1xyXG4gIGJhY2tncm91bmQtY29sb3I6ICMwZDZlZmQ7IC8qIEJvb3RzdHJhcCBwcmltYXJ5ICovXHJcbiAgYm9yZGVyLWNvbG9yOiAjMGQ2ZWZkO1xyXG4gIGNvbG9yOiAjZmZmZmZmO1xyXG59XHJcblxyXG4ucGFnaW5hdGlvbi5iZy1kYXJrIC5wYWdlLWl0ZW0uZGlzYWJsZWQgLnBhZ2UtbGluayB7XHJcbiAgYmFja2dyb3VuZC1jb2xvcjogIzIxMjUyOTtcclxuICBjb2xvcjogIzZjNzU3ZDtcclxuICBib3JkZXItY29sb3I6ICMzNDNhNDA7XHJcbn1cclxuLnBhZ2luYXRpb24uYmctd2hpdGUgLnBhZ2UtbGluayB7XHJcbiAgY29sb3I6ICMyMTI1Mjk7XHJcbn1cclxuXHJcbi5wYWdpbmF0aW9uLmJnLXdoaXRlIC5wYWdlLWl0ZW0uYWN0aXZlIC5wYWdlLWxpbmsge1xyXG4gIGJhY2tncm91bmQtY29sb3I6ICMwZDZlZmQ7XHJcbiAgYm9yZGVyLWNvbG9yOiAjMGQ2ZWZkO1xyXG4gIGNvbG9yOiAjZmZmO1xyXG59XHJcbmJ1dHRvbi5idG4tc20ge1xyXG4gIG1pbi13aWR0aDogMzZweDtcclxufVxyXG5cclxuLyogQmFzZSBkYXJrIG1vZGUgYnV0dG9ucyAqL1xyXG4uYnRuLW91dGxpbmUtbGlnaHQge1xyXG4gIGNvbG9yOiAjZjhmOWZhO1xyXG4gIGJvcmRlci1jb2xvcjogIzZjNzU3ZDtcclxufVxyXG5cclxuICAuYnRuLW91dGxpbmUtbGlnaHQ6aG92ZXIge1xyXG4gICAgYmFja2dyb3VuZC1jb2xvcjogIzM0M2E0MDtcclxuICAgIGJvcmRlci1jb2xvcjogI2FkYjViZDtcclxuICAgIGNvbG9yOiAjZmZmZmZmO1xyXG4gIH1cclxuXHJcbi8qIEFjdGl2ZSBwYWdlIGluIGRhcmsgbW9kZSAqL1xyXG4uYnRuLmJ0bi1wcmltYXJ5IHtcclxuICBiYWNrZ3JvdW5kLWNvbG9yOiAjMGQ2ZWZkO1xyXG4gIGJvcmRlci1jb2xvcjogIzBkNmVmZDtcclxuICBjb2xvcjogI2ZmZjtcclxufVxyXG5cclxuLyogRGlzYWJsZWQgYnV0dG9uIGluIGRhcmsgbW9kZSAqL1xyXG4uYnRuLW91dGxpbmUtbGlnaHQ6ZGlzYWJsZWQge1xyXG4gIGJhY2tncm91bmQtY29sb3I6IHRyYW5zcGFyZW50O1xyXG4gIGNvbG9yOiAjNmM3NTdkO1xyXG4gIGJvcmRlci1jb2xvcjogIzZjNzU3ZDtcclxuICBvcGFjaXR5OiAwLjY7XHJcbn1cclxuIl0sInNvdXJjZVJvb3QiOiIifQ== */"]
    });
  }
}

/***/ }),

/***/ 3937:
/*!************************************************************************!*\
  !*** ./src/app/core/component/directive/resizable-column.directive.ts ***!
  \************************************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   "ResizableColumnDirective": () => (/* binding */ ResizableColumnDirective)
/* harmony export */ });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ 2560);

class ResizableColumnDirective {
  constructor(el, renderer) {
    this.el = el;
    this.renderer = renderer;
  }
  ngAfterViewInit() {
    const nativeEl = this.el.nativeElement;
    const resizer = this.renderer.createElement('span');
    this.resizer = resizer;
    this.renderer.setStyle(resizer, 'width', '5px');
    this.renderer.setStyle(resizer, 'height', '100%');
    this.renderer.setStyle(resizer, 'position', 'absolute');
    this.renderer.setStyle(resizer, 'top', '0');
    this.renderer.setStyle(resizer, 'right', '0');
    this.renderer.setStyle(resizer, 'cursor', 'col-resize');
    this.renderer.setStyle(resizer, 'user-select', 'none');
    this.renderer.setStyle(nativeEl, 'position', 'relative');
    this.renderer.appendChild(nativeEl, resizer);
    resizer.addEventListener('mousedown', this.onMouseDown.bind(this));
  }
  onMouseDown(event) {
    this.startX = event.pageX;
    this.startWidth = this.el.nativeElement.offsetWidth;
    const mouseMove = this.onMouseMove.bind(this);
    const mouseUp = this.onMouseUp.bind(this);
    document.addEventListener('mousemove', mouseMove);
    document.addEventListener('mouseup', mouseUp);
  }
  onMouseMove(event) {
    const offset = event.pageX - this.startX;
    const newWidth = this.startWidth + offset;
    this.renderer.setStyle(this.el.nativeElement, 'width', `${newWidth}px`);
  }
  onMouseUp() {
    document.removeEventListener('mousemove', this.onMouseMove);
    document.removeEventListener('mouseup', this.onMouseUp);
  }
  static {
    this.ɵfac = function ResizableColumnDirective_Factory(t) {
      return new (t || ResizableColumnDirective)(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_core__WEBPACK_IMPORTED_MODULE_0__.ElementRef), _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_core__WEBPACK_IMPORTED_MODULE_0__.Renderer2));
    };
  }
  static {
    this.ɵdir = /*@__PURE__*/_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineDirective"]({
      type: ResizableColumnDirective,
      selectors: [["", "appResizableColumn", ""]]
    });
  }
}

/***/ }),

/***/ 8398:
/*!***********************************************************************************!*\
  !*** ./src/app/core/component/notification-popup/notification-popup.component.ts ***!
  \***********************************************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   "NotificationPopupComponent": () => (/* binding */ NotificationPopupComponent)
/* harmony export */ });
/* harmony import */ var _angular_animations__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/animations */ 4851);
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ 2560);
/* harmony import */ var _notificationService_notification_service__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ../../notificationService/notification-service */ 1971);
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/common */ 4666);




function NotificationPopupComponent_div_1_Template(rf, ctx) {
  if (rf & 1) {
    const _r3 = _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵgetCurrentView"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](0, "div", 2);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](2, "button", 3);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵlistener"]("click", function NotificationPopupComponent_div_1_Template_button_click_2_listener() {
      const restoredCtx = _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵrestoreView"](_r3);
      const notif_r1 = restoredCtx.$implicit;
      const ctx_r2 = _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵnextContext"]();
      return _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵresetView"](ctx_r2.dismiss(notif_r1.id));
    });
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]()();
  }
  if (rf & 2) {
    const notif_r1 = ctx.$implicit;
    const ctx_r0 = _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵnextContext"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵclassMapInterpolate1"]("alert ", ctx_r0.getAlertClass(notif_r1.status), " alert-dismissible fade show shadow mb-2");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵproperty"]("@toastAnimation", undefined);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵadvance"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtextInterpolate1"](" ", notif_r1.message, " ");
  }
}
class NotificationPopupComponent {
  constructor(notificationService) {
    this.notificationService = notificationService;
    this.notifications = [];
  }
  ngOnInit() {
    this.notificationService.notification$.subscribe(data => {
      const notification = {
        ...data,
        id: data.id ?? Date.now()
      };
      this.notifications.push(notification);
      setTimeout(() => {
        this.notifications = this.notifications.filter(n => n.id !== notification.id);
      }, data.duration || 5000);
    });
  }
  getAlertClass(status) {
    switch (status) {
      case 'success':
        return 'alert-success';
      case 'error':
        return 'alert-danger';
      case 'info':
        return 'alert-info';
      case 'warning':
        return 'alert-warning';
      default:
        return 'alert-secondary';
    }
  }
  dismiss(id) {
    this.notifications = this.notifications.filter(n => n.id !== id);
  }
  static {
    this.ɵfac = function NotificationPopupComponent_Factory(t) {
      return new (t || NotificationPopupComponent)(_angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵdirectiveInject"](_notificationService_notification_service__WEBPACK_IMPORTED_MODULE_0__.NotificationService));
    };
  }
  static {
    this.ɵcmp = /*@__PURE__*/_angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵdefineComponent"]({
      type: NotificationPopupComponent,
      selectors: [["app-notification-popup"]],
      decls: 2,
      vars: 1,
      consts: [[1, "position-fixed", "bottom-0", "end-0", "p-3", 2, "z-index", "1050"], ["role", "alert", 3, "class", 4, "ngFor", "ngForOf"], ["role", "alert"], ["type", "button", "aria-label", "Close", 1, "btn-close", 3, "click"]],
      template: function NotificationPopupComponent_Template(rf, ctx) {
        if (rf & 1) {
          _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](0, "div", 0);
          _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtemplate"](1, NotificationPopupComponent_div_1_Template, 3, 5, "div", 1);
          _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
        }
        if (rf & 2) {
          _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵadvance"](1);
          _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵproperty"]("ngForOf", ctx.notifications);
        }
      },
      dependencies: [_angular_common__WEBPACK_IMPORTED_MODULE_2__.NgForOf],
      styles: ["\n/*# sourceMappingURL=data:application/json;charset=utf-8;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsInNvdXJjZVJvb3QiOiIifQ== */"],
      data: {
        animation: [(0,_angular_animations__WEBPACK_IMPORTED_MODULE_3__.trigger)('toastAnimation', [(0,_angular_animations__WEBPACK_IMPORTED_MODULE_3__.transition)(':enter', [(0,_angular_animations__WEBPACK_IMPORTED_MODULE_3__.style)({
          opacity: 0,
          transform: 'translateY(20px)'
        }), (0,_angular_animations__WEBPACK_IMPORTED_MODULE_3__.animate)('300ms ease-out', (0,_angular_animations__WEBPACK_IMPORTED_MODULE_3__.style)({
          opacity: 1,
          transform: 'translateY(0)'
        }))]), (0,_angular_animations__WEBPACK_IMPORTED_MODULE_3__.transition)(':leave', [(0,_angular_animations__WEBPACK_IMPORTED_MODULE_3__.animate)('300ms ease-in', (0,_angular_animations__WEBPACK_IMPORTED_MODULE_3__.style)({
          opacity: 0,
          transform: 'translateY(20px)'
        }))])])]
      }
    });
  }
}

/***/ }),

/***/ 5054:
/*!***************************************************************************!*\
  !*** ./src/app/core/component/select-control/select-control.component.ts ***!
  \***************************************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   "SelectControlComponent": () => (/* binding */ SelectControlComponent)
/* harmony export */ });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ 2560);
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/common */ 4666);
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/forms */ 2508);



function SelectControlComponent_option_6_Template(rf, ctx) {
  if (rf & 1) {
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "option", 6);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
  }
  if (rf & 2) {
    const option_r2 = ctx.$implicit;
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngValue", option_r2.key);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtextInterpolate1"](" ", option_r2.value, " ");
  }
}
function SelectControlComponent_div_7_Template(rf, ctx) {
  if (rf & 1) {
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "div", 7);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
  }
  if (rf & 2) {
    const ctx_r1 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtextInterpolate1"](" Please select ", ctx_r1.label.toLowerCase(), "... ");
  }
}
class SelectControlComponent {
  constructor() {
    this.label = '';
    this.options = [];
    this.required = false;
  }
  static {
    this.ɵfac = function SelectControlComponent_Factory(t) {
      return new (t || SelectControlComponent)();
    };
  }
  static {
    this.ɵcmp = /*@__PURE__*/_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineComponent"]({
      type: SelectControlComponent,
      selectors: [["app-select-control"]],
      inputs: {
        label: "label",
        options: "options",
        control: "control",
        required: "required"
      },
      decls: 8,
      vars: 5,
      consts: [[1, "mb-3"], [1, "form-label"], [1, "form-select", 3, "formControl", "required"], ["value", ""], [3, "ngValue", 4, "ngFor", "ngForOf"], ["class", "invalid-feedback", 4, "ngIf"], [3, "ngValue"], [1, "invalid-feedback"]],
      template: function SelectControlComponent_Template(rf, ctx) {
        if (rf & 1) {
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "div", 0)(1, "label", 1);
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](2);
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](3, "select", 2)(4, "option", 3);
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](5, "Select");
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtemplate"](6, SelectControlComponent_option_6_Template, 2, 2, "option", 4);
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtemplate"](7, SelectControlComponent_div_7_Template, 2, 1, "div", 5);
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        }
        if (rf & 2) {
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](2);
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtextInterpolate"](ctx.label);
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("formControl", ctx.control)("required", ctx.required);
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](3);
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngForOf", ctx.options);
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngIf", ctx.required);
        }
      },
      dependencies: [_angular_common__WEBPACK_IMPORTED_MODULE_1__.NgForOf, _angular_common__WEBPACK_IMPORTED_MODULE_1__.NgIf, _angular_forms__WEBPACK_IMPORTED_MODULE_2__.NgSelectOption, _angular_forms__WEBPACK_IMPORTED_MODULE_2__["ɵNgSelectMultipleOption"], _angular_forms__WEBPACK_IMPORTED_MODULE_2__.SelectControlValueAccessor, _angular_forms__WEBPACK_IMPORTED_MODULE_2__.NgControlStatus, _angular_forms__WEBPACK_IMPORTED_MODULE_2__.RequiredValidator, _angular_forms__WEBPACK_IMPORTED_MODULE_2__.FormControlDirective],
      styles: ["\n/*# sourceMappingURL=data:application/json;charset=utf-8;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsInNvdXJjZVJvb3QiOiIifQ== */"]
    });
  }
}

/***/ }),

/***/ 9004:
/*!*************************************************!*\
  !*** ./src/app/core/component/shared.module.ts ***!
  \*************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   "SharedModule": () => (/* binding */ SharedModule)
/* harmony export */ });
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @angular/common */ 4666);
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! @angular/forms */ 2508);
/* harmony import */ var _angular_material_button__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! @angular/material/button */ 4522);
/* harmony import */ var _angular_material_checkbox__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! @angular/material/checkbox */ 4792);
/* harmony import */ var _angular_material_dialog__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! @angular/material/dialog */ 1484);
/* harmony import */ var _angular_material_form_field__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! @angular/material/form-field */ 5074);
/* harmony import */ var _angular_material_input__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! @angular/material/input */ 8562);
/* harmony import */ var _angular_material_select__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! @angular/material/select */ 7371);
/* harmony import */ var _data_table_data_table_component__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./data-table/data-table.component */ 5381);
/* harmony import */ var _directive_resizable_column_directive__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./directive/resizable-column.directive */ 3937);
/* harmony import */ var _select_control_select_control_component__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./select-control/select-control.component */ 5054);
/* harmony import */ var _textbox_control_textbox_control_component__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./textbox-control/textbox-control.component */ 4077);
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @angular/core */ 2560);













class SharedModule {
  static {
    this.ɵfac = function SharedModule_Factory(t) {
      return new (t || SharedModule)();
    };
  }
  static {
    this.ɵmod = /*@__PURE__*/_angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵdefineNgModule"]({
      type: SharedModule
    });
  }
  static {
    this.ɵinj = /*@__PURE__*/_angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵdefineInjector"]({
      imports: [_angular_common__WEBPACK_IMPORTED_MODULE_5__.CommonModule, _angular_forms__WEBPACK_IMPORTED_MODULE_6__.ReactiveFormsModule, _angular_forms__WEBPACK_IMPORTED_MODULE_6__.FormsModule, _angular_material_dialog__WEBPACK_IMPORTED_MODULE_7__.MatDialogModule, _angular_material_form_field__WEBPACK_IMPORTED_MODULE_8__.MatFormFieldModule, _angular_material_input__WEBPACK_IMPORTED_MODULE_9__.MatInputModule, _angular_material_select__WEBPACK_IMPORTED_MODULE_10__.MatSelectModule, _angular_material_checkbox__WEBPACK_IMPORTED_MODULE_11__.MatCheckboxModule, _angular_material_button__WEBPACK_IMPORTED_MODULE_12__.MatButtonModule, _angular_common__WEBPACK_IMPORTED_MODULE_5__.CommonModule, _angular_forms__WEBPACK_IMPORTED_MODULE_6__.ReactiveFormsModule, _angular_forms__WEBPACK_IMPORTED_MODULE_6__.FormsModule, _angular_material_dialog__WEBPACK_IMPORTED_MODULE_7__.MatDialogModule, _angular_material_form_field__WEBPACK_IMPORTED_MODULE_8__.MatFormFieldModule, _angular_material_input__WEBPACK_IMPORTED_MODULE_9__.MatInputModule, _angular_material_select__WEBPACK_IMPORTED_MODULE_10__.MatSelectModule, _angular_material_checkbox__WEBPACK_IMPORTED_MODULE_11__.MatCheckboxModule, _angular_material_button__WEBPACK_IMPORTED_MODULE_12__.MatButtonModule]
    });
  }
}
(function () {
  (typeof ngJitMode === "undefined" || ngJitMode) && _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵsetNgModuleScope"](SharedModule, {
    declarations: [_textbox_control_textbox_control_component__WEBPACK_IMPORTED_MODULE_3__.TextboxControlComponent, _select_control_select_control_component__WEBPACK_IMPORTED_MODULE_2__.SelectControlComponent, _data_table_data_table_component__WEBPACK_IMPORTED_MODULE_0__.DataTableComponent, _directive_resizable_column_directive__WEBPACK_IMPORTED_MODULE_1__.ResizableColumnDirective],
    imports: [_angular_common__WEBPACK_IMPORTED_MODULE_5__.CommonModule, _angular_forms__WEBPACK_IMPORTED_MODULE_6__.ReactiveFormsModule, _angular_forms__WEBPACK_IMPORTED_MODULE_6__.FormsModule, _angular_material_dialog__WEBPACK_IMPORTED_MODULE_7__.MatDialogModule, _angular_material_form_field__WEBPACK_IMPORTED_MODULE_8__.MatFormFieldModule, _angular_material_input__WEBPACK_IMPORTED_MODULE_9__.MatInputModule, _angular_material_select__WEBPACK_IMPORTED_MODULE_10__.MatSelectModule, _angular_material_checkbox__WEBPACK_IMPORTED_MODULE_11__.MatCheckboxModule, _angular_material_button__WEBPACK_IMPORTED_MODULE_12__.MatButtonModule],
    exports: [_angular_common__WEBPACK_IMPORTED_MODULE_5__.CommonModule, _angular_forms__WEBPACK_IMPORTED_MODULE_6__.ReactiveFormsModule, _angular_forms__WEBPACK_IMPORTED_MODULE_6__.FormsModule, _angular_material_dialog__WEBPACK_IMPORTED_MODULE_7__.MatDialogModule, _angular_material_form_field__WEBPACK_IMPORTED_MODULE_8__.MatFormFieldModule, _angular_material_input__WEBPACK_IMPORTED_MODULE_9__.MatInputModule, _angular_material_select__WEBPACK_IMPORTED_MODULE_10__.MatSelectModule, _angular_material_checkbox__WEBPACK_IMPORTED_MODULE_11__.MatCheckboxModule, _angular_material_button__WEBPACK_IMPORTED_MODULE_12__.MatButtonModule, _textbox_control_textbox_control_component__WEBPACK_IMPORTED_MODULE_3__.TextboxControlComponent, _select_control_select_control_component__WEBPACK_IMPORTED_MODULE_2__.SelectControlComponent, _data_table_data_table_component__WEBPACK_IMPORTED_MODULE_0__.DataTableComponent, _directive_resizable_column_directive__WEBPACK_IMPORTED_MODULE_1__.ResizableColumnDirective]
  });
})();

/***/ }),

/***/ 4077:
/*!*****************************************************************************!*\
  !*** ./src/app/core/component/textbox-control/textbox-control.component.ts ***!
  \*****************************************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   "TextboxControlComponent": () => (/* binding */ TextboxControlComponent)
/* harmony export */ });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ 2560);
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/common */ 4666);
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/forms */ 2508);



function TextboxControlComponent_div_4_Template(rf, ctx) {
  if (rf & 1) {
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "div", 4);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
  }
  if (rf & 2) {
    const ctx_r0 = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵnextContext"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtextInterpolate1"](" ", ctx_r0.label, " is required. ");
  }
}
class TextboxControlComponent {
  constructor() {
    this.label = '';
    this.type = 'text'; // 'text', 'number', 'date', etc.
    this.required = false;
  }
  get formControl() {
    return this.control;
  }
  static {
    this.ɵfac = function TextboxControlComponent_Factory(t) {
      return new (t || TextboxControlComponent)();
    };
  }
  static {
    this.ɵcmp = /*@__PURE__*/_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineComponent"]({
      type: TextboxControlComponent,
      selectors: [["app-textbox-control"]],
      inputs: {
        label: "label",
        type: "type",
        control: "control",
        required: "required"
      },
      decls: 5,
      vars: 5,
      consts: [[1, "mb-3"], [1, "form-label"], [1, "form-control", 3, "type", "formControl"], ["class", "invalid-feedback", 4, "ngIf"], [1, "invalid-feedback"]],
      template: function TextboxControlComponent_Template(rf, ctx) {
        if (rf & 1) {
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "div", 0)(1, "label", 1);
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](2);
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelement"](3, "input", 2);
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtemplate"](4, TextboxControlComponent_div_4_Template, 2, 1, "div", 3);
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
        }
        if (rf & 2) {
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](2);
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtextInterpolate"](ctx.label);
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("type", ctx.type)("formControl", ctx.formControl);
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵattribute"]("required", ctx.required ? true : null);
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵadvance"](1);
          _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵproperty"]("ngIf", (ctx.formControl == null ? null : ctx.formControl.invalid) && ((ctx.formControl == null ? null : ctx.formControl.touched) || (ctx.formControl == null ? null : ctx.formControl.dirty)));
        }
      },
      dependencies: [_angular_common__WEBPACK_IMPORTED_MODULE_1__.NgIf, _angular_forms__WEBPACK_IMPORTED_MODULE_2__.DefaultValueAccessor, _angular_forms__WEBPACK_IMPORTED_MODULE_2__.NgControlStatus, _angular_forms__WEBPACK_IMPORTED_MODULE_2__.RequiredValidator, _angular_forms__WEBPACK_IMPORTED_MODULE_2__.FormControlDirective],
      styles: ["\n/*# sourceMappingURL=data:application/json;charset=utf-8;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsInNvdXJjZVJvb3QiOiIifQ== */"]
    });
  }
}

/***/ }),

/***/ 294:
/*!*************************************!*\
  !*** ./src/app/core/core.module.ts ***!
  \*************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   "CoreModule": () => (/* binding */ CoreModule)
/* harmony export */ });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ 2560);
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/common */ 4666);
/* harmony import */ var _core_initailizerService_initializer_service__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ../core/initailizerService/initializer.service */ 3778);




class CoreModule {
  constructor(parentModule) {
    // Do not allow multiple injections
    if (parentModule) {
      throw new Error('CoreModule has already been loaded. Import this module in the AppModule only.');
    }
  }
  static {
    this.ɵfac = function CoreModule_Factory(t) {
      return new (t || CoreModule)(_angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵinject"](CoreModule, 12));
    };
  }
  static {
    this.ɵmod = /*@__PURE__*/_angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵdefineNgModule"]({
      type: CoreModule
    });
  }
  static {
    this.ɵinj = /*@__PURE__*/_angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵdefineInjector"]({
      providers: [{
        provide: _angular_core__WEBPACK_IMPORTED_MODULE_1__.APP_INITIALIZER,
        useFactory: appInitializer => appInitializer.init(),
        deps: [_core_initailizerService_initializer_service__WEBPACK_IMPORTED_MODULE_0__.InitializerService],
        multi: true
      }],
      imports: [_angular_common__WEBPACK_IMPORTED_MODULE_2__.CommonModule]
    });
  }
}
(function () {
  (typeof ngJitMode === "undefined" || ngJitMode) && _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵsetNgModuleScope"](CoreModule, {
    imports: [_angular_common__WEBPACK_IMPORTED_MODULE_2__.CommonModule]
  });
})();

/***/ }),

/***/ 9216:
/*!*****************************************!*\
  !*** ./src/app/core/emitter.service.ts ***!
  \*****************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   "EmitterService": () => (/* binding */ EmitterService),
/* harmony export */   "LoginEmitter": () => (/* binding */ LoginEmitter)
/* harmony export */ });
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! rxjs */ 228);
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ 2560);


class EmitterService {
  constructor() {
    this.loginEmitter = new LoginEmitter();
  }
  static {
    this.ɵfac = function EmitterService_Factory(t) {
      return new (t || EmitterService)();
    };
  }
  static {
    this.ɵprov = /*@__PURE__*/_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineInjectable"]({
      token: EmitterService,
      factory: EmitterService.ɵfac,
      providedIn: 'root'
    });
  }
}
class LoginEmitter extends rxjs__WEBPACK_IMPORTED_MODULE_1__.Subject {
  constructor() {
    super();
  }
  emit(value) {
    super.next(value);
  }
}

/***/ }),

/***/ 8776:
/*!*******************************************!*\
  !*** ./src/app/core/http/http.service.ts ***!
  \*******************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   "HttpService": () => (/* binding */ HttpService),
/* harmony export */   "methods": () => (/* binding */ methods)
/* harmony export */ });
/* harmony import */ var D_Nirmal_Learning_Projects_BankingTool_bankingtool_client_node_modules_babel_runtime_helpers_esm_asyncToGenerator_js__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./node_modules/@babel/runtime/helpers/esm/asyncToGenerator.js */ 1670);
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! rxjs */ 833);
/* harmony import */ var _dto_internal_dto_app_settings_dto__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../../dto/internal-dto/app-settings-dto */ 9176);
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! @angular/core */ 2560);
/* harmony import */ var _local_storage_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../local-storage.service */ 357);
/* harmony import */ var _notificationService_notification_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../notificationService/notification-service */ 1971);
/* harmony import */ var _service_auth_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../service/auth.service */ 1782);
/* harmony import */ var _service_loader_service__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ../service/loader.service */ 5523);
/* harmony import */ var _initailizerService_integrate_service__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ../initailizerService/integrate.service */ 2423);









class HttpService {
  constructor(localStoreService, notificationService, authService, loaderService, integrateService) {
    this.localStoreService = localStoreService;
    this.notificationService = notificationService;
    this.authService = authService;
    this.loaderService = loaderService;
    this.integrateService = integrateService;
    this.baseUrl = _dto_internal_dto_app_settings_dto__WEBPACK_IMPORTED_MODULE_1__.AppSettingsDto.baseUrl;
    this.isEncryptionEnabled = _dto_internal_dto_app_settings_dto__WEBPACK_IMPORTED_MODULE_1__.AppSettingsDto.isEncryptionEnabled;
    this.fieldsToEncrypt = _dto_internal_dto_app_settings_dto__WEBPACK_IMPORTED_MODULE_1__.AppSettingsDto.fieldsToEncrypt;
    this.requestObj = (method = 'GET', headerType = 'DEFAULT', body) => {
      return {
        method,
        headerType,
        body
      };
    };
  }
  get(url, isLoading = true) {
    return this.executeRequest(url, this.requestObj(methods.GET, methods.GET), isLoading);
  }
  post(url, body, isLoading = true) {
    return this.executeRequest(url, this.requestObj(methods.POST, methods.POST, JSON.stringify(body)), isLoading);
  }
  executeRequest(url, requestObj, isLoading) {
    var _this = this;
    return new rxjs__WEBPACK_IMPORTED_MODULE_7__.Observable(observer => {
      const _makeFetch = /*#__PURE__*/function () {
        var _ref = (0,D_Nirmal_Learning_Projects_BankingTool_bankingtool_client_node_modules_babel_runtime_helpers_esm_asyncToGenerator_js__WEBPACK_IMPORTED_MODULE_0__["default"])(function* (retry = false) {
          try {
            let requestInit = _this.getFetchObject(requestObj);
            if (isLoading) {
              _this.loaderService.show();
            }
            const response = yield fetch(_this.baseUrl + url, requestInit);
            if (response.status === 498 && !retry) {
              const success = yield _this.refreshToken();
              if (success) {
                yield _makeFetch(true);
                return;
              } else {
                throw new Error("Token refresh failed");
              }
            }
            const result = yield _this.handelResponse(response);
            if (isLoading) {
              _this.loaderService.hide();
            }
            if (result === null || result === undefined) {
              _this.notificationService.show("Internal server error", 'error');
            }
            observer.next(result);
            observer.complete();
          } catch (error) {
            if (isLoading) {
              _this.loaderService.hide();
            }
            observer.error(error);
            observer.complete();
          }
        });
        return function makeFetch() {
          return _ref.apply(this, arguments);
        };
      }();
      _makeFetch();
    });
  }
  getFetchObject(requestObj) {
    let headers;
    switch (requestObj.headerType) {
      case methods.POST:
        {
          headers = this.postHeaders();
          break;
        }
      case methods.GET:
        {
          headers = this.getHeaders();
          break;
        }
      default:
        {
          headers = this.getHeaders();
        }
    }
    let request = {
      method: requestObj.method,
      headers,
      redirect: 'follow',
      referrerPolicy: 'no-referrer'
    };
    if (requestObj.body) {
      request.body = requestObj.body;
    }
    return request;
  }
  getBaseHeaders() {
    const token = this.localStoreService.getItem('accessToken');
    let base = {};
    if (token) {
      base['Authorization'] = `Bearer ${token}`;
    }
    if (_dto_internal_dto_app_settings_dto__WEBPACK_IMPORTED_MODULE_1__.AppSettingsDto.apiKey) {
      base['ApiKey'] = _dto_internal_dto_app_settings_dto__WEBPACK_IMPORTED_MODULE_1__.AppSettingsDto.apiKey;
    }
    return base;
  }
  getHeaders() {
    let base = this.getBaseHeaders();
    base.Accept = 'application/json';
    base.Pragma = 'no-cache';
    base['Cache-Control'] = 'no-cache';
    return base;
  }
  postHeaders() {
    let base = this.getBaseHeaders();
    base.Accept = 'application/json';
    base['Content-Type'] = 'application/json';
    return base;
  }
  handelResponse(response) {
    var _this2 = this;
    return (0,D_Nirmal_Learning_Projects_BankingTool_bankingtool_client_node_modules_babel_runtime_helpers_esm_asyncToGenerator_js__WEBPACK_IMPORTED_MODULE_0__["default"])(function* () {
      if (response.status == 403) {
        return response.json();
      } else if (!response || response.status === 204) {
        return;
      } else if (response && response.status >= 500) {
        _this2.loaderService.hide();
        _this2.notificationService.show("Internal server error " + response.status.toString(), 'error');
        throw response;
      }
      if (response && response.status == 500) {
        _this2.loaderService.hide();
        _this2.notificationService.show("Internal server error " + response.status.toString(), 'error');
        throw response;
      } else if (response && response.status >= 400) {
        _this2.loaderService.hide();
        _this2.notificationService.show("Internal server error " + response.status.toString(), 'error');
        throw response;
      } else if (response.status >= 200 && response.status <= 210) {
        if (_this2.isEncryptionEnabled === 'true') {
          const result = yield response.json();
          if (_this2.hasEncryptField(result)) {
            return new Promise((resolve, reject) => {
              _this2.integrateService.getResponse(result).subscribe({
                next: integratedRes => resolve(integratedRes),
                error: err => reject(err)
              });
            });
          } else {
            return result;
          }
        } else {
          return response.json();
        }
      } else {
        return response.json();
      }
    })();
  }
  hasEncryptField(obj) {
    if (!obj || typeof obj !== 'object') {
      return false;
    }
    const encryptFieldKeys = this.fieldsToEncrypt.split(',') ?? ['accountNumber'];
    if (Object.keys(obj).some(key => encryptFieldKeys.some(field => key.toLowerCase() === field.toLowerCase()))) {
      return true;
    }
    return Object.values(obj).some(value => typeof value === 'object' && value !== null && this.hasEncryptField(value));
  }
  refreshToken() {
    let roleId = localStorage.getItem('RoleId');
    let emailId = localStorage.getItem('EmailId');
    let userId = localStorage.getItem('UserId');
    let userName = localStorage.getItem('UserName');
    let name = userName?.split(' ');
    let firstName = name[0];
    let lastName = name[1];
    let request = {
      request: {
        role: {
          id: Number(roleId)
        },
        user: {
          id: Number(userId),
          email: emailId,
          firstName: firstName,
          lastName: lastName
        }
      }
    };
    const requestObj = this.requestObj(methods.POST, methods.POST, JSON.stringify(request));
    const requestInit = this.getFetchObject(requestObj);
    return fetch(this.baseUrl + 'User/CreateToken', requestInit).then(res => res.json()).then(data => {
      if (data && data.response.accessToken) {
        this.localStoreService.setItem('accessToken', data.response.accessToken);
        return true;
      }
      return false;
    }).catch(() => false);
  }
  static {
    this.ɵfac = function HttpService_Factory(t) {
      return new (t || HttpService)(_angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵinject"](_local_storage_service__WEBPACK_IMPORTED_MODULE_2__.LocalStorageService), _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵinject"](_notificationService_notification_service__WEBPACK_IMPORTED_MODULE_3__.NotificationService), _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵinject"](_service_auth_service__WEBPACK_IMPORTED_MODULE_4__.AuthService), _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵinject"](_service_loader_service__WEBPACK_IMPORTED_MODULE_5__.LoaderService), _angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵinject"](_initailizerService_integrate_service__WEBPACK_IMPORTED_MODULE_6__.IntegrateService));
    };
  }
  static {
    this.ɵprov = /*@__PURE__*/_angular_core__WEBPACK_IMPORTED_MODULE_8__["ɵɵdefineInjectable"]({
      token: HttpService,
      factory: HttpService.ɵfac,
      providedIn: 'root'
    });
  }
}
class methods {
  static {
    this.GET = "GET";
  }
  static {
    this.POST = "POST";
  }
  static {
    this.PUT = "PUT";
  }
  static {
    this.DELETE = "DELETE";
  }
}

/***/ }),

/***/ 3197:
/*!*********************************************************************!*\
  !*** ./src/app/core/indexedDBService/indexed-db-service.service.ts ***!
  \*********************************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   "IndexedDbServiceService": () => (/* binding */ IndexedDbServiceService)
/* harmony export */ });
/* harmony import */ var _constant_constant__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ../../constant/constant */ 4219);
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/core */ 2560);
/* harmony import */ var _local_storage_service__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../local-storage.service */ 357);



class IndexedDbServiceService {
  constructor(localstorage) {
    this.localstorage = localstorage;
    let idb = this.localstorage.getItem(_constant_constant__WEBPACK_IMPORTED_MODULE_0__.ClaimKey.idb);
    if (idb) {
      this.db = JSON.parse(idb);
    }
  }
  initDb(dbName, dbVersion, primaryKey, tableName, columns) {
    return new Promise((resolve, reject) => {
      const request = indexedDB.open(dbName, dbVersion);
      request.onerror = event => {
        reject(`Createing ${dbName}: IndexedDB error: ${event}`);
      };
      request.onupgradeneeded = event => {
        const db = event.target.result;
        const objectStore = db.createObjectStore(tableName, {
          keyPath: primaryKey,
          autoIncrement: true
        });
        columns.forEach(({
          columnName,
          isUnique
        }) => objectStore.createIndex(columnName, columnName, {
          unique: isUnique
        }));
      };
      request.onsuccess = event => {
        this.db = event.target.result;
        this.localstorage.setItem(_constant_constant__WEBPACK_IMPORTED_MODULE_0__.ClaimKey.idb, JSON.stringify(this.db));
        resolve();
      };
    });
  }
  getAllData(tableName) {
    return new Promise((resolve, reject) => {
      const transaction = this.db.transaction([tableName], 'readonly');
      const objectStore = transaction.objectStore(tableName);
      const request = objectStore.getAll();
      request.onerror = event => {
        reject(`Get ${tableName}: IndexedDB error: ${event}`);
      };
      request.onsuccess = event => {
        resolve(request.result);
      };
    });
  }
  addData(tableName, data) {
    return new Promise((resolve, reject) => {
      const transaction = this.db.transaction([tableName], 'readwrite');
      const objectStore = transaction.objectStore(tableName);
      const request = objectStore.add(data);
      request.onerror = event => {
        reject(`Add ${tableName}: IndexedDB error: ${event}`);
      };
      request.onsuccess = event => {
        resolve();
      };
    });
  }
  deleteData(tableName, id) {
    return new Promise((resolve, reject) => {
      const transaction = this.db.transaction([tableName], 'readwrite');
      const objectStore = transaction.objectStore(tableName);
      const request = objectStore.delete(id);
      request.onerror = event => {
        reject(`Delete ${tableName}: IndexedDB error: ${event}`);
      };
      request.onsuccess = event => {
        resolve();
      };
    });
  }
  updateData(tableName, data) {
    return new Promise((resolve, reject) => {
      const transaction = this.db.transaction([tableName], 'readwrite');
      const objectStore = transaction.objectStore(tableName);
      const request = objectStore.put(data);
      request.onerror = event => {
        reject(`Update ${tableName}: IndexedDB error: ${event}`);
      };
      request.onsuccess = event => {
        resolve();
      };
    });
  }
  clearData(tableName) {
    return new Promise((resolve, reject) => {
      const transaction = this.db.transaction([tableName], 'readwrite');
      const objectStore = transaction.objectStore(tableName);
      const request = objectStore.clear();
      request.onerror = event => {
        reject(`Clear ${tableName}: IndexedDB error: ${event}`);
      };
      request.onsuccess = event => {
        resolve();
      };
    });
  }
  deleteDatabase(databaseName) {
    return new Promise((resolve, reject) => {
      const deleteRequest = indexedDB.deleteDatabase(databaseName);
      deleteRequest.onsuccess = () => {
        console.log('Database deleted successfully');
        resolve();
      };
      deleteRequest.onerror = event => {
        console.error('Error deleting database:');
        reject(`Error deleting database: `);
      };
    });
  }
  static {
    this.ɵfac = function IndexedDbServiceService_Factory(t) {
      return new (t || IndexedDbServiceService)(_angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵinject"](_local_storage_service__WEBPACK_IMPORTED_MODULE_1__.LocalStorageService));
    };
  }
  static {
    this.ɵprov = /*@__PURE__*/_angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵdefineInjectable"]({
      token: IndexedDbServiceService,
      factory: IndexedDbServiceService.ɵfac,
      providedIn: 'root'
    });
  }
}

/***/ }),

/***/ 3778:
/*!****************************************************************!*\
  !*** ./src/app/core/initailizerService/initializer.service.ts ***!
  \****************************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   "InitializerService": () => (/* binding */ InitializerService)
/* harmony export */ });
/* harmony import */ var lodash__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! lodash */ 2938);
/* harmony import */ var lodash__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(lodash__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _dto_internal_dto_app_settings_dto__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../../dto/internal-dto/app-settings-dto */ 9176);
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/core */ 2560);
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @angular/common */ 4666);
/* harmony import */ var _integrate_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./integrate.service */ 2423);





class InitializerService {
  constructor(_platformLocation, _appConfig) {
    this._platformLocation = _platformLocation;
    this._appConfig = _appConfig;
  }
  stripTrailingSlash(value) {
    return value.replace(/\/$/, "");
  }
  init() {
    return () => {
      return new Promise((resolve, reject) => {
        let appBaseUrl = this.stripTrailingSlash(this.getDocumentOrigin() + this.getBaseHref());
        console.log('appBaseUrl: ' + appBaseUrl);
        this._appConfig.getAppConfiguration().subscribe(response => {
          lodash__WEBPACK_IMPORTED_MODULE_0__.merge(_dto_internal_dto_app_settings_dto__WEBPACK_IMPORTED_MODULE_1__.AppSettingsDto, response);
          resolve(true);
        });
      });
    };
  }
  getBaseHref() {
    let baseUrl = this._platformLocation.getBaseHrefFromDOM();
    if (baseUrl && baseUrl == '/') {
      baseUrl = '';
    }
    return baseUrl;
  }
  getDocumentOrigin() {
    if (!document.location.origin) {
      const port = document.location.port ? ':' + document.location.port : '';
      return document.location.protocol + '//' + document.location.hostname + port;
    }
    return document.location.origin;
  }
  static {
    this.ɵfac = function InitializerService_Factory(t) {
      return new (t || InitializerService)(_angular_core__WEBPACK_IMPORTED_MODULE_3__["ɵɵinject"](_angular_common__WEBPACK_IMPORTED_MODULE_4__.PlatformLocation), _angular_core__WEBPACK_IMPORTED_MODULE_3__["ɵɵinject"](_integrate_service__WEBPACK_IMPORTED_MODULE_2__.IntegrateService));
    };
  }
  static {
    this.ɵprov = /*@__PURE__*/_angular_core__WEBPACK_IMPORTED_MODULE_3__["ɵɵdefineInjectable"]({
      token: InitializerService,
      factory: InitializerService.ɵfac,
      providedIn: 'root'
    });
  }
}

/***/ }),

/***/ 2423:
/*!**************************************************************!*\
  !*** ./src/app/core/initailizerService/integrate.service.ts ***!
  \**************************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   "IntegrateService": () => (/* binding */ IntegrateService)
/* harmony export */ });
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/common/http */ 8987);
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ 2560);



class IntegrateService {
  constructor(http) {
    this.http = http;
    this.apiBaseUrl = 'https://localhost:7113';
  }
  getAppConfiguration() {
    return this.http.get(`${this.apiBaseUrl}/AppSettings`, {
      headers: this.getWebAppApiHeaders(),
      withCredentials: false
    });
  }
  getResponse(data) {
    return this.http.post(`${this.apiBaseUrl}/AppSettings`, data, {
      headers: this.getWebAppApiHeaders(),
      withCredentials: false
    });
  }
  getWebAppApiHeaders() {
    let headers = new _angular_common_http__WEBPACK_IMPORTED_MODULE_0__.HttpHeaders();
    headers = headers.append('skip', 'true');
    headers = headers.append('Content-Type', 'application/json');
    return headers;
  }
  static {
    this.ɵfac = function IntegrateService_Factory(t) {
      return new (t || IntegrateService)(_angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵinject"](_angular_common_http__WEBPACK_IMPORTED_MODULE_0__.HttpClient));
    };
  }
  static {
    this.ɵprov = /*@__PURE__*/_angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵdefineInjectable"]({
      token: IntegrateService,
      factory: IntegrateService.ɵfac,
      providedIn: 'root'
    });
  }
}

/***/ }),

/***/ 357:
/*!***********************************************!*\
  !*** ./src/app/core/local-storage.service.ts ***!
  \***********************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   "LocalStorageService": () => (/* binding */ LocalStorageService)
/* harmony export */ });
/* harmony import */ var _constant_constant__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ../constant/constant */ 4219);
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ 2560);


class LocalStorageService {
  constructor() {
    if (!this.isLocalStorageAvailable()) {
      console.error("LocalStorage is not available in this environment.");
    }
  }
  isLocalStorageAvailable() {
    try {
      const testKey = "__test__";
      localStorage.setItem(testKey, testKey);
      localStorage.removeItem(testKey);
      return true;
    } catch (e) {
      return false;
    }
  }
  getItem(key) {
    return localStorage.getItem(key);
  }
  setItem(key, value) {
    localStorage.setItem(key, value);
  }
  removeItem(key) {
    localStorage.removeItem(key);
  }
  clear() {
    localStorage.clear();
  }
  getIsLoggedIn() {
    return localStorage.getItem(_constant_constant__WEBPACK_IMPORTED_MODULE_0__.ClaimKey.isLoggedIn) == _constant_constant__WEBPACK_IMPORTED_MODULE_0__.Constant["true"];
  }
  setIsLoggedIn(value) {
    localStorage.setItem(_constant_constant__WEBPACK_IMPORTED_MODULE_0__.ClaimKey.isLoggedIn, value.toString().toLocaleLowerCase());
  }
  setAllData(data) {
    console.log(data);
    data.forEach(item => {
      this.setItem(item.key, item.value);
    });
  }
  static {
    this.ɵfac = function LocalStorageService_Factory(t) {
      return new (t || LocalStorageService)();
    };
  }
  static {
    this.ɵprov = /*@__PURE__*/_angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵdefineInjectable"]({
      token: LocalStorageService,
      factory: LocalStorageService.ɵfac,
      providedIn: 'root'
    });
  }
}

/***/ }),

/***/ 1971:
/*!******************************************************************!*\
  !*** ./src/app/core/notificationService/notification-service.ts ***!
  \******************************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   "NotificationService": () => (/* binding */ NotificationService)
/* harmony export */ });
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! rxjs */ 228);
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ 2560);


class NotificationService {
  constructor() {
    this.notificationSubject = new rxjs__WEBPACK_IMPORTED_MODULE_0__.Subject();
    this.notification$ = this.notificationSubject.asObservable();
    this.counter = 0;
  }
  show(message, status = 'info', duration = 5000) {
    this.notificationSubject.next({
      id: ++this.counter,
      message,
      status,
      duration
    });
  }
  static {
    this.ɵfac = function NotificationService_Factory(t) {
      return new (t || NotificationService)();
    };
  }
  static {
    this.ɵprov = /*@__PURE__*/_angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵdefineInjectable"]({
      token: NotificationService,
      factory: NotificationService.ɵfac,
      providedIn: 'root'
    });
  }
}

/***/ }),

/***/ 1782:
/*!**********************************************!*\
  !*** ./src/app/core/service/auth.service.ts ***!
  \**********************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   "AuthService": () => (/* binding */ AuthService)
/* harmony export */ });
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! rxjs */ 6317);
/* harmony import */ var _constant_constant__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ../../constant/constant */ 4219);
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @angular/core */ 2560);
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @angular/router */ 124);
/* harmony import */ var _local_storage_service__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../local-storage.service */ 357);
/* harmony import */ var _indexedDBService_indexed_db_service_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../indexedDBService/indexed-db-service.service */ 3197);






class AuthService {
  constructor(router, localStorageService, indexedDbService) {
    this.router = router;
    this.localStorageService = localStorageService;
    this.indexedDbService = indexedDbService;
    this.loginState = new rxjs__WEBPACK_IMPORTED_MODULE_3__.BehaviorSubject(true);
    this.isLogined$ = this.loginState.asObservable();
  }
  setLoginState(state) {
    this.loginState.next(state);
  }
  logout() {
    this.setLoginState(false);
    this.localStorageService.clear();
    this.indexedDbService.initDb(_constant_constant__WEBPACK_IMPORTED_MODULE_0__.Constant.actionDBName, _constant_constant__WEBPACK_IMPORTED_MODULE_0__.Constant.dbVersion, _constant_constant__WEBPACK_IMPORTED_MODULE_0__.Constant.actionTablePrimaryKey, _constant_constant__WEBPACK_IMPORTED_MODULE_0__.Constant.actionTableName, _constant_constant__WEBPACK_IMPORTED_MODULE_0__.Constant.actionCoumns).then(() => {
      this.indexedDbService.deleteDatabase(_constant_constant__WEBPACK_IMPORTED_MODULE_0__.Constant.actionTableName).then(() => {});
      this.indexedDbService.clearData(_constant_constant__WEBPACK_IMPORTED_MODULE_0__.Constant.actionTableName).then(() => {});
    });
    this.router.navigate([_constant_constant__WEBPACK_IMPORTED_MODULE_0__.AppPaths.login]);
  }
  static {
    this.ɵfac = function AuthService_Factory(t) {
      return new (t || AuthService)(_angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵinject"](_angular_router__WEBPACK_IMPORTED_MODULE_5__.Router), _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵinject"](_local_storage_service__WEBPACK_IMPORTED_MODULE_1__.LocalStorageService), _angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵinject"](_indexedDBService_indexed_db_service_service__WEBPACK_IMPORTED_MODULE_2__.IndexedDbServiceService));
    };
  }
  static {
    this.ɵprov = /*@__PURE__*/_angular_core__WEBPACK_IMPORTED_MODULE_4__["ɵɵdefineInjectable"]({
      token: AuthService,
      factory: AuthService.ɵfac,
      providedIn: 'root'
    });
  }
}

/***/ }),

/***/ 5523:
/*!************************************************!*\
  !*** ./src/app/core/service/loader.service.ts ***!
  \************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   "LoaderService": () => (/* binding */ LoaderService)
/* harmony export */ });
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! rxjs */ 6317);
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! rxjs */ 1989);
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/core */ 2560);


class LoaderService {
  constructor() {
    this.loaderSubject = new rxjs__WEBPACK_IMPORTED_MODULE_0__.BehaviorSubject(false);
    this.loader$ = this.loaderSubject.asObservable().pipe((0,rxjs__WEBPACK_IMPORTED_MODULE_1__.debounceTime)(100));
  }
  show() {
    this.loaderSubject.next(true);
  }
  hide() {
    this.loaderSubject.next(false);
  }
  static {
    this.ɵfac = function LoaderService_Factory(t) {
      return new (t || LoaderService)();
    };
  }
  static {
    this.ɵprov = /*@__PURE__*/_angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵdefineInjectable"]({
      token: LoaderService,
      factory: LoaderService.ɵfac,
      providedIn: 'root'
    });
  }
}

/***/ }),

/***/ 6429:
/*!**********************************************!*\
  !*** ./src/app/core/service/note.service.ts ***!
  \**********************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   "NoteService": () => (/* binding */ NoteService)
/* harmony export */ });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ 2560);
/* harmony import */ var _http_http_service__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ../http/http.service */ 8776);


class NoteService {
  constructor(httpService) {
    this.httpService = httpService;
  }
  getAllNotes() {
    return this.httpService.get('Note/GetAllNotes');
  }
  saveNote(model) {
    return this.httpService.post('Note/SaveNote', model, false);
  }
  deleteNote(model) {
    return this.httpService.post('Note/DeleteNote', model);
  }
  static {
    this.ɵfac = function NoteService_Factory(t) {
      return new (t || NoteService)(_angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵinject"](_http_http_service__WEBPACK_IMPORTED_MODULE_0__.HttpService));
    };
  }
  static {
    this.ɵprov = /*@__PURE__*/_angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵdefineInjectable"]({
      token: NoteService,
      factory: NoteService.ɵfac,
      providedIn: 'root'
    });
  }
}

/***/ }),

/***/ 9176:
/*!******************************************************!*\
  !*** ./src/app/dto/internal-dto/app-settings-dto.ts ***!
  \******************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   "AppSettingsDto": () => (/* binding */ AppSettingsDto)
/* harmony export */ });
class AppSettingsDto {}

/***/ }),

/***/ 6168:
/*!*********************************************************!*\
  !*** ./src/app/dto/request/note/delete-note-request.ts ***!
  \*********************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   "CreateDeleteNoteRequestObject": () => (/* binding */ CreateDeleteNoteRequestObject)
/* harmony export */ });
const CreateDeleteNoteRequestObject = noteId => {
  let model = {
    request: {
      note: {
        id: noteId
      }
    }
  };
  return model;
};

/***/ }),

/***/ 7882:
/*!*******************************************************!*\
  !*** ./src/app/dto/request/note/save-note-request.ts ***!
  \*******************************************************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony export */ __webpack_require__.d(__webpack_exports__, {
/* harmony export */   "CreateSaveNoteRequestObject": () => (/* binding */ CreateSaveNoteRequestObject)
/* harmony export */ });
const CreateSaveNoteRequestObject = (noteId, title, content) => {
  let model = {
    request: {
      note: {
        id: noteId,
        content: content,
        title: title
      }
    }
  };
  return model;
};

/***/ }),

/***/ 4431:
/*!*********************!*\
  !*** ./src/main.ts ***!
  \*********************/
/***/ ((__unused_webpack_module, __webpack_exports__, __webpack_require__) => {

__webpack_require__.r(__webpack_exports__);
/* harmony import */ var _angular_platform_browser__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/platform-browser */ 4497);
/* harmony import */ var _app_app_module__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./app/app.module */ 6747);


_angular_platform_browser__WEBPACK_IMPORTED_MODULE_1__.platformBrowser().bootstrapModule(_app_app_module__WEBPACK_IMPORTED_MODULE_0__.AppModule).catch(err => console.error(err));

/***/ })

},
/******/ __webpack_require__ => { // webpackRuntimeModules
/******/ var __webpack_exec__ = (moduleId) => (__webpack_require__(__webpack_require__.s = moduleId))
/******/ __webpack_require__.O(0, ["vendor"], () => (__webpack_exec__(4431)));
/******/ var __webpack_exports__ = __webpack_require__.O();
/******/ }
]);
//# sourceMappingURL=main.js.map