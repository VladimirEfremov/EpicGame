(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["main"],{

/***/ "./$$_lazy_route_resource lazy recursive":
/*!******************************************************!*\
  !*** ./$$_lazy_route_resource lazy namespace object ***!
  \******************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

function webpackEmptyAsyncContext(req) {
	// Here Promise.resolve().then() is used instead of new Promise() to prevent
	// uncaught exception popping up in devtools
	return Promise.resolve().then(function() {
		var e = new Error("Cannot find module '" + req + "'");
		e.code = 'MODULE_NOT_FOUND';
		throw e;
	});
}
webpackEmptyAsyncContext.keys = function() { return []; };
webpackEmptyAsyncContext.resolve = webpackEmptyAsyncContext;
module.exports = webpackEmptyAsyncContext;
webpackEmptyAsyncContext.id = "./$$_lazy_route_resource lazy recursive";

/***/ }),

/***/ "./node_modules/raw-loader/dist/cjs.js!./src/app/app.component.html":
/*!**************************************************************************!*\
  !*** ./node_modules/raw-loader/dist/cjs.js!./src/app/app.component.html ***!
  \**************************************************************************/
/*! exports provided: default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony default export */ __webpack_exports__["default"] = ("\n<router-outlet></router-outlet>");

/***/ }),

/***/ "./node_modules/raw-loader/dist/cjs.js!./src/app/game/game-menu/game-menu.component.html":
/*!***********************************************************************************************!*\
  !*** ./node_modules/raw-loader/dist/cjs.js!./src/app/game/game-menu/game-menu.component.html ***!
  \***********************************************************************************************/
/*! exports provided: default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony default export */ __webpack_exports__["default"] = ("<p>\n  game-menu works!\n</p>\n\n<div>\n  <button name=\"btn_play\" (click)=\"OnBtnPlayClick()\">\n    Play  \n  </button>\n  <button name=\"btn_account\" (click)=\"OnBtnAccountClick()\">\n    Account\n  </button>\n  <button name=\"btn_people\" (click)=\"OnBtnPeopleClick()\">\n    People\n  </button>\n</div>");

/***/ }),

/***/ "./node_modules/raw-loader/dist/cjs.js!./src/app/game/game.component.html":
/*!********************************************************************************!*\
  !*** ./node_modules/raw-loader/dist/cjs.js!./src/app/game/game.component.html ***!
  \********************************************************************************/
/*! exports provided: default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony default export */ __webpack_exports__["default"] = ("\n<table class=\"globalTable\">\n  \n<tr>\n    <td>\n      <div class=\"GameTxt\" > \n        Ogame2.0\n      </div>\n    </td>\n    <td>\n    Nickname:{{accountData.Nickname}}\n    </td>\n</tr>\n\n<tr>\n    <button>ToCore</button>\n    <button>ToMap</button>\n</tr>\n\n<tr>\n  <td>\n  <table class=\"CoreTable\" align=\"left\"> \n    <tr>\n      <td></td>\n      <td>\n        <h3> Money: {{money}} gold Defence power: {{defencePower}} </h3>\n        <button (click)=\"OnCoreBtnClick()\">Core</button>\n\n        <ng-template [ngIf]=\"IsCasernActive\">\n          <button (click)=\"OnCasernBtnClick()\">Casern</button> \n        </ng-template>\n        <ng-template [ngIf]=\"IsDefenceTowerActive\">\n          <button (click)=\"OnGoldMiningBtnClick()\">GoldMining</button> \n        </ng-template>\n        <ng-template [ngIf]=\"IsGoldMiningActive\">\n          <button (click)=\"OnDefenceTowerBtnClick()\">DefenceTower</button> \n        </ng-template>\n\n        <ng-template [ngIf]=\"!IsCasernActive\">\n            <button class=\"DiactivatedButton\" title=\"Необходимо купить в Core\" disabled=\"true\">Casern</button> \n        </ng-template>\n        <ng-template [ngIf]=\"!IsDefenceTowerActive\">\n            <button class=\"DiactivatedButton\" title=\"Необходимо купить в Core\" disabled=\"true\">GoldMining</button> \n        </ng-template>\n        <ng-template [ngIf]=\"!IsGoldMiningActive\">\n            <button class=\"DiactivatedButton\" title=\"Необходимо купить в Core\" disabled=\"true\">DefenceTower</button> \n        </ng-template>\n\n      </td>\n    </tr>      \n    \n    <tr>\n      <td></td>\n      <td>\n        <ng-template [ngIf]=\"IsCoreInfoActivated\">\n          <td>\n            <div>Hp: {{coreInfo.CoreHp}}</div> \n            <div>Attack: {{coreInfo.CoreAttack}}</div> \n            <div>Defence: {{coreInfo.CoreDefence}}</div>\n            <div>Workers: {{coreInfo.CoreWorkersCount}}/{{coreInfo.CoreCapacity}}</div>\n            <div>Type: {{coreInfo.CoreType}}</div>\n          </td>\n          <td>\n              <div>Income: {{coreInfo.CoreIncome}}</div> \n              <div>Outcome: {{coreInfo.CoreOutcome}}</div>\n          </td>\n          <td>\n            <tr><button title=\"Стоимость: 1000 gold\" (click)=\"OnBuildCasernBtnClick()\">Build Casern</button></tr>\n            <tr><button title=\"Стоимость: 1000 gold\" (click)=\"OnBuildGoldMiningBtnClick()\">Build GoldMining</button></tr>\n            <tr><button title=\"Стоимость: 1000 gold\" (click)=\"OnBuildDefenceTowerBtnClick()\">Build DefenceTower</button></tr>\n          </td>\n          <td>\n            <button title=\"Стоимость: 1000 gold\" (click)=\"OnProduceWorkerBtnClick()\">Produce worker</button>\n          </td>\n        </ng-template>\n        \n        <ng-template [ngIf]=\"IsCasernInfoActivated\">\n          <td>  \n            <div>Hp: {{casernInfo.CasernHp}}</div> \n            <div>Attack: {{casernInfo.CasernAttack}}</div> \n            <div>Defence: {{casernInfo.CasernDefence}}</div>\n            <div>Warriors: {{casernInfo.CasernWarriorsCount}}</div>\n            <div>AttackAircraft: {{casernInfo.CasernAttackAircraftsCount}}</div>\n            <div>Type: {{casernInfo.CasernType}}</div>\n          </td>\n          <td>\n              <div>Income: {{casernInfo.CasernIncome}}</div> \n              <div>Outcome: {{casernInfo.CasernOutcome}}</div>\n          </td>\n          <td>\n            <button title=\"Стоимость: 1000 gold\" (click)=\"OnProduceWarriorBtnClick()\">Produce warrior</button> <br/>\n            <button title=\"Стоимость: 1000 gold\" (click)=\"OnProduceAttackAircraftBtnClick()\">Produce attackAircraft</button>\n          </td>\n        </ng-template>\n        \n        <ng-template [ngIf]=\"IsDefenceTowerInfoActivated\">\n            <td>  \n              <div>Hp: {{defenceTowerInfo.DefenceTowerHp}}</div> \n              <div>Attack: {{defenceTowerInfo.DefenceTowerAttack}}</div> \n              <div>Defence: {{defenceTowerInfo.DefenceTowerDefence}}</div>\n              <div>Type: {{defenceTowerInfo.DefenceTowerType}}</div>\n            </td>\n            <td>\n                <div>Income: {{goldMiningInfo.GoldMiningIncome}}</div> \n                <div>Outcome: {{goldMiningInfo.GoldMiningOutcome}}</div>\n            </td>\n          </ng-template>\n        \n          <ng-template [ngIf]=\"IsGoldMiningInfoActivated\">\n              <td>  \n                  <div>Hp: {{goldMiningInfo.GoldMiningHp}}</div> \n                  <div>Attack: {{goldMiningInfo.GoldMiningAttack}}</div> \n                  <div>Defence: {{goldMiningInfo.GoldMiningDefence}}</div>\n                  <div>Type: {{goldMiningInfo.GoldMiningType}}</div>\n                </td>\n                <td>\n                    <div>Income: {{goldMiningInfo.GoldMiningIncome}}</div> \n                    <div>Outcome: {{goldMiningInfo.GoldMiningOutcome}}</div>\n                </td>\n              <td>\n                <button title=\"Добавит нового рабочего на производственную линию\"\n                        (click)=\"OnAddWorkerToMineBtnClick()\">Add worker</button> <br/>\n              </td>\n            </ng-template>\n      </td>\n    </tr>\n    \n  </table>\n  </td>\n  \n  <td>\n    <table class=\"CommunicationTable\">\n      <tr align=\"center\">FriendsList</tr>\n      <tr>\n        <td>\n          <button (click)=\"OnLessBtnClick()\">\n            &lt;\n          </button>\n          <button (click)=\"OnGreaterBtnClick()\">\n            &gt;\n          </button>\n        </td>\n      </tr>\n      <tr>\n        <td>\n          <tr>\n            <button class=\"cmnBtn\" (click)=\"OnAllBtnClick()\">All</button>\n          </tr>\n          <tr>\n            <button class=\"cmnBtn\" (click)=\"OnFriendsBtnClick()\">Friends</button>\n          </tr>\n          <tr>\n            <button class=\"cmnBtn\" (click)=\"OnFollowersBtnClick()\">Followers</button>\n          </tr>\n          <tr>\n            <button class=\"cmnBtn\" (click)=\"OnFollowingsBtnClick()\">Followings</button>\n          </tr>\n        </td>\n        \n        <td >\n\n          <div [ngSwitch]=\"numberOfSelectedList\">\n              <ng-template ngSwitchCase=\"0\">\n                  <div *ngFor=\n                    \"let user of selectedAll\">\n                    <button class=\"btnList\" \n                    (click)=\"OnUserBtnClick(user)\">\n                      {{user.Nickname}}\n                    </button>\n                  </div>\n              </ng-template>\n\n              <ng-template ngSwitchCase=\"2\"> \n                  <div *ngFor=\n                  \"let user of friendsUsers\">\n                  <button class=\"btnList\" \n                  (click)=\"OnUserBtnClick(user)\">\n                    {{user.Nickname}}\n                  </button>\n                </div>\n              </ng-template>\n\n              <ng-template ngSwitchCase=\"3\">\n                  <div *ngFor=\n                  \"let user of followersUsers\">\n                  <button class=\"btnList\" \n                  (click)=\"OnUserBtnClick(user)\">\n                    {{user.Nickname}}\n                  </button>\n                </div>\n              </ng-template>\n\n              <ng-template ngSwitchDefault>\n                  <div *ngFor=\n                  \"let user of followingsUsers\">\n                  <button class=\"btnList\" \n                  (click)=\"OnUserBtnClick(user)\">\n                    {{user.Nickname}}\n                  </button>\n                </div>\n              </ng-template>\n\n          </div>\n\n        </td>\n      </tr>\n\n    </table>\n  </td>\n  \n</tr>\n\n</table>\n\n<div class=\"UsersActionWindowBox\">\n  <ng-template [ngIf]=\"isUserActionsWindowVisible\">\n    \n    <div class=\"UsersActionWindow\" \n      [ngSwitch]=\"numberOfSelectedList\">\n      <div class=\"RoundedExitBtnBox\">\n        <button class=\"RoundedExitBtn\"\n                (click)=\"OnExitBtnClick()\">&#215;</button>\n      </div>\n      \n      <br/> \n      <br/>\n\n      <div class=\"UserInfo\">\n        User info\n        <br/> \n        Nickname: {{selectedUser.Nickname}}\n        <br/> \n        <br/> \n        Core info \n        <br/> \n      </div>\n      \n      \n\n      <br/> \n       <ng-template ngSwitchCase=\"0\">\n         <ng-template *ngIf=\"IsUserFriend(); else not\">\n           <button class=\"btnUserAction\" \n           (click)=\"RemoveUserFromFriends()\">\n           Удалить из друзей\n          </button>\n        </ng-template>\n        <ng-template #not>\n            <button class=\"btnUserAction\" \n            (click)=\"AddUserToFriends()\">\n            Добавить в друзья\n           </button>\n        </ng-template>\n       </ng-template>\n\n       <ng-template ngSwitchCase=\"1\"> \n          <button class=\"btnUserAction\" \n          (click)=\"RemoveUserFromFriends()\">\n            Удалить из друзей\n          </button>\n       </ng-template>\n\n       <ng-template ngSwitchCase=\"3\">\n          <button class=\"btnUserAction\" \n          (click)=\"RemoveUserFromFriends()\">\n          Отменить подписку\n          </button>\n       </ng-template>\n\n       <ng-template ngSwitchDefault>\n        <button class=\"btnUserAction\">\n          Default\n        </button>\n       </ng-template>\n      \n       {{selectedUser.Nickname}}\n    </div>\n  </ng-template>\n</div>\n\n\n<div class=\"Logger\" onload=\"this.scrollTop = 9999;\"> \n    <div class=\"LoggerContent\"\n      *ngFor=\"let log of loggedData?.Data\"\n      >\n      <div [ngSwitch]=\"log.color\">\n          <ng-template ngSwitchCase=\"1\"><div class=\"LoggerBuildContent\">{{log.message}}</div></ng-template>\n          <ng-template ngSwitchCase=\"2\"> <div class=\"LoggerWarContent\">{{log.message}}</div></ng-template>\n          <ng-template ngSwitchCase=\"3\"><div class=\"LoggerCommunicationContent\">{{log.message}}</div></ng-template>\n          <ng-template ngSwitchDefault><div class=\"LoggerDefaultContent\">{{log.message}}</div></ng-template>\n      </div>\n    </div>\n  </div>\n\n<div class=\"CanvasDiv\">\n  <button>\n    CLICK ON ME\n  </button>\n\n</div>\n\n <canvas id=\"map\" width=600 height=600>\n    Canvas not supported\n </canvas>");

/***/ }),

/***/ "./node_modules/raw-loader/dist/cjs.js!./src/app/login/login.component.html":
/*!**********************************************************************************!*\
  !*** ./node_modules/raw-loader/dist/cjs.js!./src/app/login/login.component.html ***!
  \**********************************************************************************/
/*! exports provided: default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony default export */ __webpack_exports__["default"] = ("\n<div class=\"main_login\">\n<div class=\"background\">\n        \n        <form>\n        <h1 class=\"txt_login\">\n        LOGIN FORM\n        </h1>\n        \n        <div class=\"nickname\">\n        <input  name=\"nickname\" type=\"text\" \n                maxlength=30\n                placeholder=\"Введите nickname *\" \n                [(ngModel)]=\"LoginPostData.Nickname\"\n                required\n                />\n        </div> \n        <br />\n        \n        <div class=\"password\">\n\n        <input name=\"PasswordHash\" type=\"password\" \n                maxlength=30\n                placeholder=\"Введите пароль *\" \n                [(ngModel)]=\"LoginPostData.PasswordHash\"\n                required/>\n        </div>\n             \n        <br />\n                \n        <div class=\"link_registration\">\n                <a \n                title=\"Нажми, чтобы создать аккаунт\"\n                routerLink=\"/registration\">\n                        Еще не зарегистрированы? \n                </a>\n        </div>\n        \n        <br /> <br />\n\n        <div>\n        <button \n                name=\"btn_login\" \n                (click)=\"onClick()\">\n                Войти\n        </button>\n        </div>\n                \n        </form>\n        \n</div>\n</div>");

/***/ }),

/***/ "./node_modules/raw-loader/dist/cjs.js!./src/app/main/main.component.html":
/*!********************************************************************************!*\
  !*** ./node_modules/raw-loader/dist/cjs.js!./src/app/main/main.component.html ***!
  \********************************************************************************/
/*! exports provided: default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony default export */ __webpack_exports__["default"] = ("\n<div class=\"logo\">\n  OGAME 2.0\n</div>\n  \n<div class=\"box\">\n\n  \n  <button class=\"btn_try_it_now\"\n          (click)=\"onClick()\">\n    <span>\n      TRY IT NOW\n    </span>\n  </button>\n\n</div>");

/***/ }),

/***/ "./node_modules/raw-loader/dist/cjs.js!./src/app/not-found-page/not-found-page.component.html":
/*!****************************************************************************************************!*\
  !*** ./node_modules/raw-loader/dist/cjs.js!./src/app/not-found-page/not-found-page.component.html ***!
  \****************************************************************************************************/
/*! exports provided: default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony default export */ __webpack_exports__["default"] = ("\n\n<div>\n  <p> Page not found ! </p>\n</div>\n\n<div class=\"time\">\n  <h1>[You already wasted {{wastedSeconds}} seconds on this page]</h1>\n</div>\n\n<div class=\"error\">\n    <h1> вернитесь на главную страницу </h1>\n</div>\n\n<div class=\"error_2\">\n    <h1> здесь могла быть ваша реклама </h1>\n</div>\n");

/***/ }),

/***/ "./node_modules/raw-loader/dist/cjs.js!./src/app/registration/registration.component.html":
/*!************************************************************************************************!*\
  !*** ./node_modules/raw-loader/dist/cjs.js!./src/app/registration/registration.component.html ***!
  \************************************************************************************************/
/*! exports provided: default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony default export */ __webpack_exports__["default"] = ("\n\n<div class=\"main_registration\">\n<div class=\"background\">\n\n<form>\n        <h1 class=\"txt_registration\">\n        REGISTRATION FORM\n        </h1>\n\n        <div class=\"name\">\n        <input name=\"firstName\" type=\"text\" \n                maxlength=30\n                placeholder=\"Введите имя *\" \n                [(ngModel)]=\"PostData.FirstName\"\n                required\n                />\n        </div>\n\n        <br />\n\n        <div class=\"sunname\">\n        <input  name=\"login\" type=\"text\" \n                maxlength=30\n                placeholder=\"Введите фамилию *\" \n                [(ngModel)]=\"PostData.SecondName\"\n                required\n                />\n        </div>\n   \n        <br />\n\n        <div class=\"password\">\n        <input name=\"pass\" type=\"password\" \n                maxlength=30\n                placeholder=\"Введите пароль *\" \n                [(ngModel)]=\"PostData.PasswordHash\"\n                required/>\n        </div>\n   \n        <br />\n\n        <div class=\"nickname\">\n        <input  name=\"login\" type=\"text\" \n                maxlength=30\n                placeholder=\"Введите никнейм *\" \n                [(ngModel)]=\"PostData.Nickname\"\n                required\n                />\n        </div>\n   \n        <br />\n\n        <div class=\"email\">\n        <input  name=\"login\" type=\"text\" \n                placeholder=\"Введите email *\" \n                [(ngModel)]=\"PostData.Email\"\n                required\n                />\n        </div>\n   \n        <br />\n\n        <div>\n        <button  name=\"btn_Registration\" \n                (click)=\"onClick()\">\n                Зарегестрироваться \n        </button>\n        </div>\n\n</form>\n\n</div>\n</div>");

/***/ }),

/***/ "./node_modules/tslib/tslib.es6.js":
/*!*****************************************!*\
  !*** ./node_modules/tslib/tslib.es6.js ***!
  \*****************************************/
/*! exports provided: __extends, __assign, __rest, __decorate, __param, __metadata, __awaiter, __generator, __exportStar, __values, __read, __spread, __spreadArrays, __await, __asyncGenerator, __asyncDelegator, __asyncValues, __makeTemplateObject, __importStar, __importDefault */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "__extends", function() { return __extends; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "__assign", function() { return __assign; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "__rest", function() { return __rest; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "__decorate", function() { return __decorate; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "__param", function() { return __param; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "__metadata", function() { return __metadata; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "__awaiter", function() { return __awaiter; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "__generator", function() { return __generator; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "__exportStar", function() { return __exportStar; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "__values", function() { return __values; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "__read", function() { return __read; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "__spread", function() { return __spread; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "__spreadArrays", function() { return __spreadArrays; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "__await", function() { return __await; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "__asyncGenerator", function() { return __asyncGenerator; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "__asyncDelegator", function() { return __asyncDelegator; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "__asyncValues", function() { return __asyncValues; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "__makeTemplateObject", function() { return __makeTemplateObject; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "__importStar", function() { return __importStar; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "__importDefault", function() { return __importDefault; });
/*! *****************************************************************************
Copyright (c) Microsoft Corporation. All rights reserved.
Licensed under the Apache License, Version 2.0 (the "License"); you may not use
this file except in compliance with the License. You may obtain a copy of the
License at http://www.apache.org/licenses/LICENSE-2.0

THIS CODE IS PROVIDED ON AN *AS IS* BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
KIND, EITHER EXPRESS OR IMPLIED, INCLUDING WITHOUT LIMITATION ANY IMPLIED
WARRANTIES OR CONDITIONS OF TITLE, FITNESS FOR A PARTICULAR PURPOSE,
MERCHANTABLITY OR NON-INFRINGEMENT.

See the Apache Version 2.0 License for specific language governing permissions
and limitations under the License.
***************************************************************************** */
/* global Reflect, Promise */

var extendStatics = function(d, b) {
    extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return extendStatics(d, b);
};

function __extends(d, b) {
    extendStatics(d, b);
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
}

var __assign = function() {
    __assign = Object.assign || function __assign(t) {
        for (var s, i = 1, n = arguments.length; i < n; i++) {
            s = arguments[i];
            for (var p in s) if (Object.prototype.hasOwnProperty.call(s, p)) t[p] = s[p];
        }
        return t;
    }
    return __assign.apply(this, arguments);
}

function __rest(s, e) {
    var t = {};
    for (var p in s) if (Object.prototype.hasOwnProperty.call(s, p) && e.indexOf(p) < 0)
        t[p] = s[p];
    if (s != null && typeof Object.getOwnPropertySymbols === "function")
        for (var i = 0, p = Object.getOwnPropertySymbols(s); i < p.length; i++) {
            if (e.indexOf(p[i]) < 0 && Object.prototype.propertyIsEnumerable.call(s, p[i]))
                t[p[i]] = s[p[i]];
        }
    return t;
}

function __decorate(decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
}

function __param(paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
}

function __metadata(metadataKey, metadataValue) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(metadataKey, metadataValue);
}

function __awaiter(thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
}

function __generator(thisArg, body) {
    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t, g;
    return g = { next: verb(0), "throw": verb(1), "return": verb(2) }, typeof Symbol === "function" && (g[Symbol.iterator] = function() { return this; }), g;
    function verb(n) { return function (v) { return step([n, v]); }; }
    function step(op) {
        if (f) throw new TypeError("Generator is already executing.");
        while (_) try {
            if (f = 1, y && (t = op[0] & 2 ? y["return"] : op[0] ? y["throw"] || ((t = y["return"]) && t.call(y), 0) : y.next) && !(t = t.call(y, op[1])).done) return t;
            if (y = 0, t) op = [op[0] & 2, t.value];
            switch (op[0]) {
                case 0: case 1: t = op; break;
                case 4: _.label++; return { value: op[1], done: false };
                case 5: _.label++; y = op[1]; op = [0]; continue;
                case 7: op = _.ops.pop(); _.trys.pop(); continue;
                default:
                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
                    if (t[2]) _.ops.pop();
                    _.trys.pop(); continue;
            }
            op = body.call(thisArg, _);
        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
    }
}

function __exportStar(m, exports) {
    for (var p in m) if (!exports.hasOwnProperty(p)) exports[p] = m[p];
}

function __values(o) {
    var m = typeof Symbol === "function" && o[Symbol.iterator], i = 0;
    if (m) return m.call(o);
    return {
        next: function () {
            if (o && i >= o.length) o = void 0;
            return { value: o && o[i++], done: !o };
        }
    };
}

function __read(o, n) {
    var m = typeof Symbol === "function" && o[Symbol.iterator];
    if (!m) return o;
    var i = m.call(o), r, ar = [], e;
    try {
        while ((n === void 0 || n-- > 0) && !(r = i.next()).done) ar.push(r.value);
    }
    catch (error) { e = { error: error }; }
    finally {
        try {
            if (r && !r.done && (m = i["return"])) m.call(i);
        }
        finally { if (e) throw e.error; }
    }
    return ar;
}

function __spread() {
    for (var ar = [], i = 0; i < arguments.length; i++)
        ar = ar.concat(__read(arguments[i]));
    return ar;
}

function __spreadArrays() {
    for (var s = 0, i = 0, il = arguments.length; i < il; i++) s += arguments[i].length;
    for (var r = Array(s), k = 0, i = 0; i < il; i++)
        for (var a = arguments[i], j = 0, jl = a.length; j < jl; j++, k++)
            r[k] = a[j];
    return r;
};

function __await(v) {
    return this instanceof __await ? (this.v = v, this) : new __await(v);
}

function __asyncGenerator(thisArg, _arguments, generator) {
    if (!Symbol.asyncIterator) throw new TypeError("Symbol.asyncIterator is not defined.");
    var g = generator.apply(thisArg, _arguments || []), i, q = [];
    return i = {}, verb("next"), verb("throw"), verb("return"), i[Symbol.asyncIterator] = function () { return this; }, i;
    function verb(n) { if (g[n]) i[n] = function (v) { return new Promise(function (a, b) { q.push([n, v, a, b]) > 1 || resume(n, v); }); }; }
    function resume(n, v) { try { step(g[n](v)); } catch (e) { settle(q[0][3], e); } }
    function step(r) { r.value instanceof __await ? Promise.resolve(r.value.v).then(fulfill, reject) : settle(q[0][2], r); }
    function fulfill(value) { resume("next", value); }
    function reject(value) { resume("throw", value); }
    function settle(f, v) { if (f(v), q.shift(), q.length) resume(q[0][0], q[0][1]); }
}

function __asyncDelegator(o) {
    var i, p;
    return i = {}, verb("next"), verb("throw", function (e) { throw e; }), verb("return"), i[Symbol.iterator] = function () { return this; }, i;
    function verb(n, f) { i[n] = o[n] ? function (v) { return (p = !p) ? { value: __await(o[n](v)), done: n === "return" } : f ? f(v) : v; } : f; }
}

function __asyncValues(o) {
    if (!Symbol.asyncIterator) throw new TypeError("Symbol.asyncIterator is not defined.");
    var m = o[Symbol.asyncIterator], i;
    return m ? m.call(o) : (o = typeof __values === "function" ? __values(o) : o[Symbol.iterator](), i = {}, verb("next"), verb("throw"), verb("return"), i[Symbol.asyncIterator] = function () { return this; }, i);
    function verb(n) { i[n] = o[n] && function (v) { return new Promise(function (resolve, reject) { v = o[n](v), settle(resolve, reject, v.done, v.value); }); }; }
    function settle(resolve, reject, d, v) { Promise.resolve(v).then(function(v) { resolve({ value: v, done: d }); }, reject); }
}

function __makeTemplateObject(cooked, raw) {
    if (Object.defineProperty) { Object.defineProperty(cooked, "raw", { value: raw }); } else { cooked.raw = raw; }
    return cooked;
};

function __importStar(mod) {
    if (mod && mod.__esModule) return mod;
    var result = {};
    if (mod != null) for (var k in mod) if (Object.hasOwnProperty.call(mod, k)) result[k] = mod[k];
    result.default = mod;
    return result;
}

function __importDefault(mod) {
    return (mod && mod.__esModule) ? mod : { default: mod };
}


/***/ }),

/***/ "./src/app/app.component.css":
/*!***********************************!*\
  !*** ./src/app/app.component.css ***!
  \***********************************/
/*! exports provided: default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony default export */ __webpack_exports__["default"] = ("\r\nhtml\r\n{\r\n    background-image: linear-gradient(\r\n        90deg, #13bfdd 0%, \r\n      #4213dd 40%, #6013dd 85%);\r\n}\r\n\r\nbutton \r\n{\r\n    background-color: rgb(4, 121, 255);\r\n    opacity: 0.6;\r\n    color: white;\r\n    cursor: pointer;\r\n\r\n    box-shadow: 0 4px 16px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0,0,0,0.19);\r\n    transition: all 0.5s;\r\n}\r\n\r\nbutton span {\r\n  cursor: pointer;\r\n  transition: 0.5s;\r\n}\r\n\r\nbutton:hover {\r\n  opacity: 1.0;\r\n}\r\n\r\nbutton:active {\r\n  background-color: #6000fb;\r\n  box-shadow: 0 5px #666;\r\n  transform: translateY(8px);\r\n}\r\n\r\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNyYy9hcHAvYXBwLmNvbXBvbmVudC5jc3MiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IjtBQUNBOztJQUVJOzsrQkFBZ0c7QUFDcEc7O0FBRUE7O0lBRUksa0NBQWtDO0lBQ2xDLFlBQVk7SUFDWixZQUFZO0lBQ1osZUFBZTs7SUFFZiwwRUFBMEU7SUFDMUUsb0JBQW9CO0FBQ3hCOztBQUVBO0VBQ0UsZUFBZTtFQUNmLGdCQUFnQjtBQUNsQjs7QUFFQTtFQUNFLFlBQVk7QUFDZDs7QUFFQTtFQUNFLHlCQUF5QjtFQUN6QixzQkFBc0I7RUFDdEIsMEJBQTBCO0FBQzVCIiwiZmlsZSI6InNyYy9hcHAvYXBwLmNvbXBvbmVudC5jc3MiLCJzb3VyY2VzQ29udGVudCI6WyJcclxuaHRtbFxyXG57XHJcbiAgICBiYWNrZ3JvdW5kLWltYWdlOiBsaW5lYXItZ3JhZGllbnQoXHJcbiAgICAgICAgOTBkZWcsICMxM2JmZGQgMCUsIFxyXG4gICAgICAjNDIxM2RkIDQwJSwgIzYwMTNkZCA4NSUpO1xyXG59XHJcblxyXG5idXR0b24gXHJcbntcclxuICAgIGJhY2tncm91bmQtY29sb3I6IHJnYig0LCAxMjEsIDI1NSk7XHJcbiAgICBvcGFjaXR5OiAwLjY7XHJcbiAgICBjb2xvcjogd2hpdGU7XHJcbiAgICBjdXJzb3I6IHBvaW50ZXI7XHJcblxyXG4gICAgYm94LXNoYWRvdzogMCA0cHggMTZweCAwIHJnYmEoMCwgMCwgMCwgMC4yKSwgMCA2cHggMjBweCAwIHJnYmEoMCwwLDAsMC4xOSk7XHJcbiAgICB0cmFuc2l0aW9uOiBhbGwgMC41cztcclxufVxyXG5cclxuYnV0dG9uIHNwYW4ge1xyXG4gIGN1cnNvcjogcG9pbnRlcjtcclxuICB0cmFuc2l0aW9uOiAwLjVzO1xyXG59XHJcblxyXG5idXR0b246aG92ZXIge1xyXG4gIG9wYWNpdHk6IDEuMDtcclxufVxyXG5cclxuYnV0dG9uOmFjdGl2ZSB7XHJcbiAgYmFja2dyb3VuZC1jb2xvcjogIzYwMDBmYjtcclxuICBib3gtc2hhZG93OiAwIDVweCAjNjY2O1xyXG4gIHRyYW5zZm9ybTogdHJhbnNsYXRlWSg4cHgpO1xyXG59XHJcbiJdfQ== */");

/***/ }),

/***/ "./src/app/app.component.ts":
/*!**********************************!*\
  !*** ./src/app/app.component.ts ***!
  \**********************************/
/*! exports provided: AppComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AppComponent", function() { return AppComponent; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "./node_modules/tslib/tslib.es6.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm2015/core.js");


let AppComponent = class AppComponent {
    constructor() {
        this.title = 'Angular works correctly with nodejs';
        this.isRegistered = false;
    }
};
AppComponent = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([
    Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Component"])({
        selector: 'app-root',
        template: tslib__WEBPACK_IMPORTED_MODULE_0__["__importDefault"](__webpack_require__(/*! raw-loader!./app.component.html */ "./node_modules/raw-loader/dist/cjs.js!./src/app/app.component.html")).default,
        encapsulation: _angular_core__WEBPACK_IMPORTED_MODULE_1__["ViewEncapsulation"].None,
        styles: [tslib__WEBPACK_IMPORTED_MODULE_0__["__importDefault"](__webpack_require__(/*! ./app.component.css */ "./src/app/app.component.css")).default]
    })
], AppComponent);



/***/ }),

/***/ "./src/app/app.module.ts":
/*!*******************************!*\
  !*** ./src/app/app.module.ts ***!
  \*******************************/
/*! exports provided: AppModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AppModule", function() { return AppModule; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "./node_modules/tslib/tslib.es6.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm2015/core.js");
/* harmony import */ var _angular_platform_browser__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/platform-browser */ "./node_modules/@angular/platform-browser/fesm2015/platform-browser.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm2015/router.js");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm2015/http.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm2015/forms.js");
/* harmony import */ var _app_component__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ./app.component */ "./src/app/app.component.ts");
/* harmony import */ var _login_login_component__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! ./login/login.component */ "./src/app/login/login.component.ts");
/* harmony import */ var _registration_registration_component__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! ./registration/registration.component */ "./src/app/registration/registration.component.ts");
/* harmony import */ var _main_main_component__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! ./main/main.component */ "./src/app/main/main.component.ts");
/* harmony import */ var _not_found_page_not_found_page_component__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! ./not-found-page/not-found-page.component */ "./src/app/not-found-page/not-found-page.component.ts");
/* harmony import */ var _game_game_component__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! ./game/game.component */ "./src/app/game/game.component.ts");
/* harmony import */ var _shared_GameGuard__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! ./shared/GameGuard */ "./src/app/shared/GameGuard.ts");
/* harmony import */ var _game_game_menu_game_menu_component__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! ./game/game-menu/game-menu.component */ "./src/app/game/game-menu/game-menu.component.ts");














const appRoutes = [
    { path: '', component: _main_main_component__WEBPACK_IMPORTED_MODULE_9__["MainComponent"] },
    { path: 'login', component: _login_login_component__WEBPACK_IMPORTED_MODULE_7__["LoginComponent"] },
    { path: 'registration', component: _registration_registration_component__WEBPACK_IMPORTED_MODULE_8__["RegistrationComponent"] },
    { path: 'game', component: _game_game_component__WEBPACK_IMPORTED_MODULE_11__["GameComponent"], canActivate: [_shared_GameGuard__WEBPACK_IMPORTED_MODULE_12__["GameGuard"]] },
    { path: 'game-menu', component: _game_game_menu_game_menu_component__WEBPACK_IMPORTED_MODULE_13__["GameMenuComponent"], canActivate: [_shared_GameGuard__WEBPACK_IMPORTED_MODULE_12__["GameGuard"]] },
    { path: '**', component: _not_found_page_not_found_page_component__WEBPACK_IMPORTED_MODULE_10__["NotFoundPageComponent"] }
];
let AppModule = class AppModule {
};
AppModule = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([
    Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["NgModule"])({
        declarations: [
            _app_component__WEBPACK_IMPORTED_MODULE_6__["AppComponent"],
            _main_main_component__WEBPACK_IMPORTED_MODULE_9__["MainComponent"],
            _login_login_component__WEBPACK_IMPORTED_MODULE_7__["LoginComponent"], _registration_registration_component__WEBPACK_IMPORTED_MODULE_8__["RegistrationComponent"],
            _not_found_page_not_found_page_component__WEBPACK_IMPORTED_MODULE_10__["NotFoundPageComponent"],
            _game_game_menu_game_menu_component__WEBPACK_IMPORTED_MODULE_13__["GameMenuComponent"], _game_game_component__WEBPACK_IMPORTED_MODULE_11__["GameComponent"]
        ],
        imports: [
            _angular_platform_browser__WEBPACK_IMPORTED_MODULE_2__["BrowserModule"],
            _angular_router__WEBPACK_IMPORTED_MODULE_3__["RouterModule"].forRoot(appRoutes),
            _angular_common_http__WEBPACK_IMPORTED_MODULE_4__["HttpClientModule"],
            _angular_forms__WEBPACK_IMPORTED_MODULE_5__["FormsModule"]
        ],
        providers: [
            _shared_GameGuard__WEBPACK_IMPORTED_MODULE_12__["GameGuard"]
        ],
        bootstrap: [
            _app_component__WEBPACK_IMPORTED_MODULE_6__["AppComponent"]
        ]
    })
], AppModule);



/***/ }),

/***/ "./src/app/game/DataToLog.ts":
/*!***********************************!*\
  !*** ./src/app/game/DataToLog.ts ***!
  \***********************************/
/*! exports provided: DataToLog */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "DataToLog", function() { return DataToLog; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "./node_modules/tslib/tslib.es6.js");

class DataToLog {
}


/***/ }),

/***/ "./src/app/game/Logger.ts":
/*!********************************!*\
  !*** ./src/app/game/Logger.ts ***!
  \********************************/
/*! exports provided: Logger */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "Logger", function() { return Logger; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "./node_modules/tslib/tslib.es6.js");
/* harmony import */ var _DataToLog__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./DataToLog */ "./src/app/game/DataToLog.ts");


class Logger {
    constructor() {
        this.Data = [];
    }
    PushDefaultMsg(data) {
        let date = new Date();
        let dataToLog = new _DataToLog__WEBPACK_IMPORTED_MODULE_1__["DataToLog"]();
        dataToLog.message =
            "[Default] " +
                data + " " +
                date.getUTCHours() + ":" +
                date.getMinutes() + ":" +
                date.getSeconds() +
                "  [" +
                date.getUTCDate() + "." +
                date.getMonth() + "." +
                date.getFullYear() % 100
                + "]";
        dataToLog.color = 0;
        this.Data.push(dataToLog);
    }
    PushBuildingMsg(data) {
        let date = new Date();
        let dataToLog = new _DataToLog__WEBPACK_IMPORTED_MODULE_1__["DataToLog"]();
        dataToLog.message =
            "[Build] " +
                data + " " +
                date.getUTCHours() + ":" +
                date.getMinutes() + ":" +
                date.getSeconds() +
                "  [" +
                date.getUTCDate() + "." +
                date.getMonth() + "." +
                date.getFullYear() % 100
                + "]";
        dataToLog.color = 1;
        this.Data.push(dataToLog);
    }
    PushWarMsg(data) {
        let date = new Date();
        let dataToLog = new _DataToLog__WEBPACK_IMPORTED_MODULE_1__["DataToLog"]();
        dataToLog.message =
            "[War] " +
                data + " " +
                date.getUTCHours() + ":" +
                date.getMinutes() + ":" +
                date.getSeconds() +
                "  [" +
                date.getUTCDate() + "." +
                date.getMonth() + "." +
                date.getFullYear() % 100
                + "]";
        dataToLog.color = 2;
        this.Data.push(dataToLog);
    }
    PushCommunicationMsg(data) {
        let date = new Date();
        let dataToLog = new _DataToLog__WEBPACK_IMPORTED_MODULE_1__["DataToLog"]();
        dataToLog.message =
            "[Communication] " +
                data + " " +
                date.getUTCHours() + ":" +
                date.getMinutes() + ":" +
                date.getSeconds() +
                "  [" +
                date.getUTCDate() + "." +
                date.getMonth() + "." +
                date.getFullYear() % 100
                + "]";
        dataToLog.color = 3;
        this.Data.push(dataToLog);
    }
}


/***/ }),

/***/ "./src/app/game/Map.ts":
/*!*****************************!*\
  !*** ./src/app/game/Map.ts ***!
  \*****************************/
/*! exports provided: Map */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "Map", function() { return Map; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "./node_modules/tslib/tslib.es6.js");

class Map {
    constructor() {
        this.x = 0;
        this.y = 0;
        this.xoff = 0;
        this.yoff = 0;
    }
    Init() {
        this.canvas = document
            .getElementById('map');
        this.context = this.canvas.getContext('2d');
        this.width = this.canvas.width;
        this.height = this.canvas.height;
        //нужно качать изображение с сервера
        //this.spaceShipSprite = new Image();
        //this.spaceShipSprite.src = "spaceship1.png";
        //this.spaceShipSprite.onload = 
        //    function(e) {this.context.drawImage(this.spaceShipSprite, 0, 0);;};
    }
    Clear() {
        this.context.clearRect(0, 0, this.width, this.height);
    }
    DrawCircle(x, y, radius, color) {
        this.context.save();
        if (color) {
            this.context.fillStyle = color;
        }
        this.context.beginPath();
        this.context.arc(x, y, radius, 0, 2 * Math.PI, true);
        this.context.closePath();
        this.context.fill();
        this.context.restore();
    }
    DrawWorld() {
        this.Clear();
        console.log("Draw");
        this.context.fillStyle = "red";
        this.DrawCircle(200, 200, 50, "yellow");
        if (this.x > 100) {
            this.xoff = -0.1;
        }
        else {
            this.xoff = +0.1;
        }
        this.context.fillRect(0, 0, 100 + this.x, 100);
        this.x += this.xoff;
        window.requestAnimationFrame(this.DrawWorld);
    }
}


/***/ }),

/***/ "./src/app/game/game-menu/game-menu.component.css":
/*!********************************************************!*\
  !*** ./src/app/game/game-menu/game-menu.component.css ***!
  \********************************************************/
/*! exports provided: default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony default export */ __webpack_exports__["default"] = ("\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL2dhbWUvZ2FtZS1tZW51L2dhbWUtbWVudS5jb21wb25lbnQuY3NzIn0= */");

/***/ }),

/***/ "./src/app/game/game-menu/game-menu.component.ts":
/*!*******************************************************!*\
  !*** ./src/app/game/game-menu/game-menu.component.ts ***!
  \*******************************************************/
/*! exports provided: GameMenuComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "GameMenuComponent", function() { return GameMenuComponent; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "./node_modules/tslib/tslib.es6.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm2015/core.js");
/* harmony import */ var _shared_Game_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../../shared/Game.service */ "./src/app/shared/Game.service.ts");



let GameMenuComponent = class GameMenuComponent {
    constructor(gameService) {
        this.gameService = gameService;
    }
    ngOnInit() {
    }
    OnBtnPlayClick() {
        this.gameService.SwitchToGame();
    }
    OnBtnAccountClick() {
        //
    }
    OnBtnPeopleClick() {
        //
    }
};
GameMenuComponent.ctorParameters = () => [
    { type: _shared_Game_service__WEBPACK_IMPORTED_MODULE_2__["GameService"] }
];
GameMenuComponent = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([
    Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Component"])({
        selector: 'app-game-menu',
        template: tslib__WEBPACK_IMPORTED_MODULE_0__["__importDefault"](__webpack_require__(/*! raw-loader!./game-menu.component.html */ "./node_modules/raw-loader/dist/cjs.js!./src/app/game/game-menu/game-menu.component.html")).default,
        providers: [_shared_Game_service__WEBPACK_IMPORTED_MODULE_2__["GameService"]],
        styles: [tslib__WEBPACK_IMPORTED_MODULE_0__["__importDefault"](__webpack_require__(/*! ./game-menu.component.css */ "./src/app/game/game-menu/game-menu.component.css")).default]
    })
], GameMenuComponent);



/***/ }),

/***/ "./src/app/game/game.component.css":
/*!*****************************************!*\
  !*** ./src/app/game/game.component.css ***!
  \*****************************************/
/*! exports provided: default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony default export */ __webpack_exports__["default"] = ("\r\n\r\ntd\r\n{\r\n    text-align: left;\r\n    padding: 0.5rem;\r\n}\r\n\r\n\r\n.globalTable\r\n{\r\n    width: 100%;\r\n    height: 100%;\r\n}\r\n\r\n\r\n.CoreTable\r\n{\r\n    width: 70rem;\r\n    border: 1px solid black;\r\n    border-left: 1px solid black;\r\n}\r\n\r\n\r\n.GameTxt\r\n{\r\n    text-align: center;\r\n    font-size: 34px;\r\n    vertical-align: text-top;\r\n}\r\n\r\n\r\n.CommunicationTable\r\n{\r\n    width: 250px;\r\n    height:200px;\r\n    border: 1px solid black;\r\n    margin-right: 5%;\r\n}\r\n\r\n\r\n.DiactivatedButton\r\n{\r\n    background-color: gray;\r\n}\r\n\r\n\r\n.cmnBtn\r\n{\r\n    width: 100px;\r\n}\r\n\r\n\r\n.btnList\r\n{\r\n    width: 100px;\r\n}\r\n\r\n\r\n.Logger {\r\n    margin-left:25px;\r\n    background-color: #0f0d53;\r\n    height:350px;\r\n    width:650px;\r\n    padding:0.1rem;\r\n    padding-left:1.1rem;\r\n    overflow-y: scroll;\r\n}\r\n\r\n\r\n.LoggerContent\r\n{\r\n    margin-left:1px;\r\n    margin-top:5px;\r\n    font-size:14px;\r\n}\r\n\r\n\r\n.LoggerDefaultContent\r\n{\r\n    color: #ffffff;\r\n}\r\n\r\n\r\n.LoggerBuildContent\r\n{\r\n    color: #f1ee12;\r\n}\r\n\r\n\r\n.LoggerWarContent\r\n{\r\n    color: #ff0000;\r\n}\r\n\r\n\r\n.LoggerCommunicationContent\r\n{\r\n    color: #09ff00;\r\n}\r\n\r\n\r\n.RoundedExitBtnBox{\r\n    display: flex;\r\n}\r\n\r\n\r\n.RoundedExitBtn{\r\n    position: absolute; \r\n    right: 2.5%;\r\n    top: 2.5%;\r\n    width:25px;\r\n    height:25px;\r\n    color:red;\r\n    background-color: rgb(250, 250, 0, 0);\r\n    font:30px;\r\n    border-radius: 50%;\r\n    border:1px solid red;\r\n}\r\n\r\n\r\n.UsersActionWindowBox{\r\n    display: flex;\r\n    justify-content: center;\r\n    align-items: center;\r\n}\r\n\r\n\r\n.UsersActionWindow {\r\n    position: absolute;\r\n    width: 500px;\r\n    height: 500px;\r\n    background-color: rgb(0,0,0);\r\n    color:white;\r\n    font-size: 30px;\r\n}\r\n\r\n\r\n.UserInfo {\r\n    position:absolute;\r\n    left: 5%;\r\n}\r\n\r\n\r\n.btnUserAction {\r\n    position: absolute;\r\n    left:40%;\r\n    bottom: 10%;\r\n    width: 100px;\r\n    align-self:center;\r\n}\r\n\r\n\r\n.CanvasDiv{\r\n    position:absolute;\r\n    left: 0px;\r\n    right: 0px;\r\n}\r\n\r\n\r\n#map{\r\n\r\n    border:1px solid black;\r\n}\r\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNyYy9hcHAvZ2FtZS9nYW1lLmNvbXBvbmVudC5jc3MiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6Ijs7QUFFQTs7SUFFSSxnQkFBZ0I7SUFDaEIsZUFBZTtBQUNuQjs7O0FBR0E7O0lBRUksV0FBVztJQUNYLFlBQVk7QUFDaEI7OztBQUVBOztJQUVJLFlBQVk7SUFDWix1QkFBdUI7SUFDdkIsNEJBQTRCO0FBQ2hDOzs7QUFFQTs7SUFFSSxrQkFBa0I7SUFDbEIsZUFBZTtJQUNmLHdCQUF3QjtBQUM1Qjs7O0FBRUE7O0lBRUksWUFBWTtJQUNaLFlBQVk7SUFDWix1QkFBdUI7SUFDdkIsZ0JBQWdCO0FBQ3BCOzs7QUFFQTs7SUFFSSxzQkFBc0I7QUFDMUI7OztBQUVBOztJQUVJLFlBQVk7QUFDaEI7OztBQUVBOztJQUVJLFlBQVk7QUFDaEI7OztBQUVBO0lBQ0ksZ0JBQWdCO0lBQ2hCLHlCQUF5QjtJQUN6QixZQUFZO0lBQ1osV0FBVztJQUNYLGNBQWM7SUFDZCxtQkFBbUI7SUFDbkIsa0JBQWtCO0FBQ3RCOzs7QUFFQTs7SUFFSSxlQUFlO0lBQ2YsY0FBYztJQUNkLGNBQWM7QUFDbEI7OztBQUVBOztJQUVJLGNBQWM7QUFDbEI7OztBQUVBOztJQUVJLGNBQWM7QUFDbEI7OztBQUVBOztJQUVJLGNBQWM7QUFDbEI7OztBQUVBOztJQUVJLGNBQWM7QUFDbEI7OztBQUVBO0lBQ0ksYUFBYTtBQUNqQjs7O0FBRUE7SUFDSSxrQkFBa0I7SUFDbEIsV0FBVztJQUNYLFNBQVM7SUFDVCxVQUFVO0lBQ1YsV0FBVztJQUNYLFNBQVM7SUFDVCxxQ0FBcUM7SUFDckMsU0FBUztJQUNULGtCQUFrQjtJQUNsQixvQkFBb0I7QUFDeEI7OztBQUVBO0lBQ0ksYUFBYTtJQUNiLHVCQUF1QjtJQUN2QixtQkFBbUI7QUFDdkI7OztBQUVBO0lBQ0ksa0JBQWtCO0lBQ2xCLFlBQVk7SUFDWixhQUFhO0lBQ2IsNEJBQTRCO0lBQzVCLFdBQVc7SUFDWCxlQUFlO0FBQ25COzs7QUFFQTtJQUNJLGlCQUFpQjtJQUNqQixRQUFRO0FBQ1o7OztBQUVBO0lBQ0ksa0JBQWtCO0lBQ2xCLFFBQVE7SUFDUixXQUFXO0lBQ1gsWUFBWTtJQUNaLGlCQUFpQjtBQUNyQjs7O0FBRUE7SUFDSSxpQkFBaUI7SUFDakIsU0FBUztJQUNULFVBQVU7QUFDZDs7O0FBRUE7O0lBRUksc0JBQXNCO0FBQzFCIiwiZmlsZSI6InNyYy9hcHAvZ2FtZS9nYW1lLmNvbXBvbmVudC5jc3MiLCJzb3VyY2VzQ29udGVudCI6WyJcclxuXHJcbnRkXHJcbntcclxuICAgIHRleHQtYWxpZ246IGxlZnQ7XHJcbiAgICBwYWRkaW5nOiAwLjVyZW07XHJcbn1cclxuXHJcblxyXG4uZ2xvYmFsVGFibGVcclxue1xyXG4gICAgd2lkdGg6IDEwMCU7XHJcbiAgICBoZWlnaHQ6IDEwMCU7XHJcbn1cclxuXHJcbi5Db3JlVGFibGVcclxue1xyXG4gICAgd2lkdGg6IDcwcmVtO1xyXG4gICAgYm9yZGVyOiAxcHggc29saWQgYmxhY2s7XHJcbiAgICBib3JkZXItbGVmdDogMXB4IHNvbGlkIGJsYWNrO1xyXG59XHJcblxyXG4uR2FtZVR4dFxyXG57XHJcbiAgICB0ZXh0LWFsaWduOiBjZW50ZXI7XHJcbiAgICBmb250LXNpemU6IDM0cHg7XHJcbiAgICB2ZXJ0aWNhbC1hbGlnbjogdGV4dC10b3A7XHJcbn1cclxuXHJcbi5Db21tdW5pY2F0aW9uVGFibGVcclxue1xyXG4gICAgd2lkdGg6IDI1MHB4O1xyXG4gICAgaGVpZ2h0OjIwMHB4O1xyXG4gICAgYm9yZGVyOiAxcHggc29saWQgYmxhY2s7XHJcbiAgICBtYXJnaW4tcmlnaHQ6IDUlO1xyXG59XHJcblxyXG4uRGlhY3RpdmF0ZWRCdXR0b25cclxue1xyXG4gICAgYmFja2dyb3VuZC1jb2xvcjogZ3JheTtcclxufVxyXG5cclxuLmNtbkJ0blxyXG57XHJcbiAgICB3aWR0aDogMTAwcHg7XHJcbn1cclxuXHJcbi5idG5MaXN0XHJcbntcclxuICAgIHdpZHRoOiAxMDBweDtcclxufVxyXG5cclxuLkxvZ2dlciB7XHJcbiAgICBtYXJnaW4tbGVmdDoyNXB4O1xyXG4gICAgYmFja2dyb3VuZC1jb2xvcjogIzBmMGQ1MztcclxuICAgIGhlaWdodDozNTBweDtcclxuICAgIHdpZHRoOjY1MHB4O1xyXG4gICAgcGFkZGluZzowLjFyZW07XHJcbiAgICBwYWRkaW5nLWxlZnQ6MS4xcmVtO1xyXG4gICAgb3ZlcmZsb3cteTogc2Nyb2xsO1xyXG59XHJcblxyXG4uTG9nZ2VyQ29udGVudFxyXG57XHJcbiAgICBtYXJnaW4tbGVmdDoxcHg7XHJcbiAgICBtYXJnaW4tdG9wOjVweDtcclxuICAgIGZvbnQtc2l6ZToxNHB4O1xyXG59XHJcblxyXG4uTG9nZ2VyRGVmYXVsdENvbnRlbnRcclxue1xyXG4gICAgY29sb3I6ICNmZmZmZmY7XHJcbn1cclxuXHJcbi5Mb2dnZXJCdWlsZENvbnRlbnRcclxue1xyXG4gICAgY29sb3I6ICNmMWVlMTI7XHJcbn1cclxuXHJcbi5Mb2dnZXJXYXJDb250ZW50XHJcbntcclxuICAgIGNvbG9yOiAjZmYwMDAwO1xyXG59XHJcblxyXG4uTG9nZ2VyQ29tbXVuaWNhdGlvbkNvbnRlbnRcclxue1xyXG4gICAgY29sb3I6ICMwOWZmMDA7XHJcbn1cclxuXHJcbi5Sb3VuZGVkRXhpdEJ0bkJveHtcclxuICAgIGRpc3BsYXk6IGZsZXg7XHJcbn1cclxuXHJcbi5Sb3VuZGVkRXhpdEJ0bntcclxuICAgIHBvc2l0aW9uOiBhYnNvbHV0ZTsgXHJcbiAgICByaWdodDogMi41JTtcclxuICAgIHRvcDogMi41JTtcclxuICAgIHdpZHRoOjI1cHg7XHJcbiAgICBoZWlnaHQ6MjVweDtcclxuICAgIGNvbG9yOnJlZDtcclxuICAgIGJhY2tncm91bmQtY29sb3I6IHJnYigyNTAsIDI1MCwgMCwgMCk7XHJcbiAgICBmb250OjMwcHg7XHJcbiAgICBib3JkZXItcmFkaXVzOiA1MCU7XHJcbiAgICBib3JkZXI6MXB4IHNvbGlkIHJlZDtcclxufVxyXG5cclxuLlVzZXJzQWN0aW9uV2luZG93Qm94e1xyXG4gICAgZGlzcGxheTogZmxleDtcclxuICAgIGp1c3RpZnktY29udGVudDogY2VudGVyO1xyXG4gICAgYWxpZ24taXRlbXM6IGNlbnRlcjtcclxufVxyXG5cclxuLlVzZXJzQWN0aW9uV2luZG93IHtcclxuICAgIHBvc2l0aW9uOiBhYnNvbHV0ZTtcclxuICAgIHdpZHRoOiA1MDBweDtcclxuICAgIGhlaWdodDogNTAwcHg7XHJcbiAgICBiYWNrZ3JvdW5kLWNvbG9yOiByZ2IoMCwwLDApO1xyXG4gICAgY29sb3I6d2hpdGU7XHJcbiAgICBmb250LXNpemU6IDMwcHg7XHJcbn1cclxuXHJcbi5Vc2VySW5mbyB7XHJcbiAgICBwb3NpdGlvbjphYnNvbHV0ZTtcclxuICAgIGxlZnQ6IDUlO1xyXG59XHJcblxyXG4uYnRuVXNlckFjdGlvbiB7XHJcbiAgICBwb3NpdGlvbjogYWJzb2x1dGU7XHJcbiAgICBsZWZ0OjQwJTtcclxuICAgIGJvdHRvbTogMTAlO1xyXG4gICAgd2lkdGg6IDEwMHB4O1xyXG4gICAgYWxpZ24tc2VsZjpjZW50ZXI7XHJcbn1cclxuXHJcbi5DYW52YXNEaXZ7XHJcbiAgICBwb3NpdGlvbjphYnNvbHV0ZTtcclxuICAgIGxlZnQ6IDBweDtcclxuICAgIHJpZ2h0OiAwcHg7XHJcbn1cclxuXHJcbiNtYXB7XHJcblxyXG4gICAgYm9yZGVyOjFweCBzb2xpZCBibGFjaztcclxufSJdfQ== */");

/***/ }),

/***/ "./src/app/game/game.component.ts":
/*!****************************************!*\
  !*** ./src/app/game/game.component.ts ***!
  \****************************************/
/*! exports provided: GameComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "GameComponent", function() { return GameComponent; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "./node_modules/tslib/tslib.es6.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm2015/core.js");
/* harmony import */ var _shared_HttpAuth_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../shared/HttpAuth.service */ "./src/app/shared/HttpAuth.service.ts");
/* harmony import */ var _shared_Game_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../shared/Game.service */ "./src/app/shared/Game.service.ts");
/* harmony import */ var _Logger__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./Logger */ "./src/app/game/Logger.ts");
/* harmony import */ var _Map__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ./Map */ "./src/app/game/Map.ts");






let GameComponent = class GameComponent {
    constructor(gameService, httpAuthService) {
        this.gameService = gameService;
        this.httpAuthService = httpAuthService;
        //Info account
        this.accountData = {
            UserId: -1,
            Nickname: "",
            FriendsList: [],
            FollowersList: [],
            FollowingsList: []
        };
        //Flags info core
        this.IsCoreInfoActivated = true;
        this.IsCasernActive = true;
        this.IsDefenceTowerActive = true;
        this.IsGoldMiningActive = true;
        //Base info
        this.money = 0;
        this.defencePower = 0;
        //Info core
        this.coreInfo = {
            CoreHp: 10000,
            CoreAttack: 0,
            CoreDefence: 20,
            CoreWorkersCount: 3,
            CoreCapacity: 10,
            CoreIncome: 0,
            CoreOutcome: 10,
            CoreType: "Core"
        };
        this.casernInfo = {
            CasernHp: 7500,
            CasernAttack: 0,
            CasernDefence: 15,
            CasernIncome: 0,
            CasernOutcome: 10,
            CasernType: "ProductionAttack",
            CasernWarriorsCount: 0,
            CasernAttackAircraftsCount: 0,
            CasernCapacity: 0
        };
        //Info DefenceTower
        this.defenceTowerInfo = {
            DefenceTowerHp: 12500,
            DefenceTowerAttack: 200,
            DefenceTowerDefence: 15,
            DefenceTowerIncome: 0,
            DefenceTowerOutcome: 10,
            DefenceTowerType: "Defence",
            DefenceTowerWorkersCount: 0
        };
        //Info GoldMining
        this.goldMiningInfo = {
            GoldMiningHp: 2500,
            GoldMiningAttack: 0,
            GoldMiningDefence: 0,
            GoldMiningIncome: 0,
            GoldMiningOutcome: 0,
            GoldMiningType: "Production",
            GoldMiningWorkersCount: 0,
            GoldMiningCapacity: 0
        };
        //Communication 
        //0-All, 1-Friends, 2-Followers, 3-Followings
        this.numberOfSelectedList = -1;
        this.numberOfPage = 0;
        this.pageStep = 4;
        //list [All, Friends, Followers, Followings]
        this.selectedNicknames = [];
        this.all = ["none1", "none2"];
        this.friends = ["Friend1", "Friend2"];
        this.followers = [
            "Fol0", "Fol1", "Fol2", "Fol3", "Fol4",
            "Fol5", "Fol6", "Fol7", "Fol8", "Fol9",
            "Fol10", "Fol11", "Fol12"
        ];
        this.followings = ["Followings1", "Followings2"];
        //Users stuff
        this.allUsers = [];
        this.friendsUsers = [];
        this.followersUsers = [];
        this.followingsUsers = [];
        this.selectedAll = [];
        this.selectedFriends = [];
        this.selectedFollowers = [];
        this.selectedFollowings = [];
        this.isUserActionsWindowVisible = true;
        //Log
        this.loggedData = new _Logger__WEBPACK_IMPORTED_MODULE_4__["Logger"]();
        //Map
        this.map = new _Map__WEBPACK_IMPORTED_MODULE_5__["Map"]();
    }
    ngOnInit() {
        console.log("NG on init()");
        this.map.Init();
        this.map.DrawWorld();
        window.requestAnimationFrame(this.map.DrawWorld);
        this.GetAccountData();
    }
    GetAccountData() {
        this.httpAuthService.GetAccountData()
            .subscribe(res => {
            this.accountData = res;
            console.log("Get account data " +
                "[Nickname: " + this.accountData.Nickname +
                " Friends list: " + this.accountData.FriendsList + "]");
        }, err => {
            console.log("GetAccountData error: " + err);
            this.accountData.Nickname = "null";
            this.accountData.FriendsList = ["null"];
        });
    }
    GetAllUsers() {
        this.httpAuthService.GetAllUsers()
            .subscribe(res => {
            console.log("get all users");
            this.allUsers = res;
        }, err => {
            console.log("[error] get all users");
        });
    }
    GetUsersFriendsTable(userId) {
        this.httpAuthService.GetUsersFriendsInfo(userId)
            .subscribe(res => {
            console.log("get user friends");
            this.friendsUsers = res;
        }, err => {
            console.log("[error] get user friends");
        });
    }
    GetUsersFollowersTable(userId) {
        this.httpAuthService.GetUsersFollowersInfo(userId)
            .subscribe(res => {
            console.log("get user followers");
            this.followersUsers = res;
        }, err => {
            console.log("[error] get user followers");
        });
    }
    GetUsersFollowingsTable(userId) {
        this.httpAuthService.GetUsersFollowingsInfo(userId)
            .subscribe(res => {
            console.log("get user followings");
            this.followingsUsers = res;
        }, err => {
            console.log("[error] get user followings");
        });
    }
    OnCoreBtnClick() {
        this.IsCoreInfoActivated = true;
        this.IsCasernInfoActivated = false;
        this.IsDefenceTowerInfoActivated = false;
        this.IsGoldMiningInfoActivated = false;
    }
    OnBuildCasernBtnClick() {
        this.loggedData.PushBuildingMsg("Строится казарма");
    }
    OnBuildGoldMiningBtnClick() {
        this.loggedData.PushBuildingMsg("Строится шахта для добывания золото");
    }
    OnBuildDefenceTowerBtnClick() {
        this.loggedData.PushBuildingMsg("Строится защитное сооружение");
    }
    OnProduceWorkerBtnClick() {
        this.loggedData.PushBuildingMsg("Создается unit рабочий");
    }
    OnCasernBtnClick() {
        this.IsCoreInfoActivated = false;
        this.IsCasernInfoActivated = true;
        this.IsDefenceTowerInfoActivated = false;
        this.IsGoldMiningInfoActivated = false;
    }
    OnProduceWarriorBtnClick() {
        this.loggedData.PushBuildingMsg("Создается unit воин");
    }
    OnProduceAttackAircraftBtnClick() {
        this.loggedData.PushBuildingMsg("Создается unit AttackAircraft");
    }
    OnGoldMiningBtnClick() {
        this.IsCoreInfoActivated = false;
        this.IsCasernInfoActivated = false;
        this.IsDefenceTowerInfoActivated = false;
        this.IsGoldMiningInfoActivated = true;
    }
    OnAddWorkerToMineBtnClick() {
        this.loggedData.PushBuildingMsg("Добавлен рабочий к руднику");
    }
    OnDefenceTowerBtnClick() {
        this.IsCasernInfoActivated = false;
        this.IsCoreInfoActivated = false;
        this.IsDefenceTowerInfoActivated = true;
        this.IsGoldMiningInfoActivated = false;
    }
    OnLessBtnClick() {
        if (this.numberOfPage > 0) {
            if (this.numberOfSelectedList == 0) {
                --this.numberOfPage;
                this.selectedAll =
                    this.allUsers
                        .slice(this.pageStep * this.numberOfPage, this.pageStep * this.numberOfPage + this.pageStep);
            }
            else if (this.numberOfSelectedList == 1) {
                --this.numberOfPage;
                this.selectedFriends =
                    this.friendsUsers
                        .slice(this.pageStep * this.numberOfPage, this.pageStep * this.numberOfPage + this.pageStep);
            }
            else if (this.numberOfSelectedList == 2) {
                --this.numberOfPage;
                this.selectedFollowers =
                    this.followersUsers
                        .slice(this.pageStep * this.numberOfPage, this.pageStep * this.numberOfPage + this.pageStep);
            }
            else if (this.numberOfSelectedList == 3) {
                --this.numberOfPage;
                this.selectedFollowings =
                    this.followingsUsers
                        .slice(this.pageStep * this.numberOfPage, this.pageStep * this.numberOfPage + this.pageStep);
            }
        }
    }
    OnGreaterBtnClick() {
        if (this.numberOfSelectedList == 0) {
            if (this.pageStep * (this.numberOfPage + 1) < this.allUsers.length) {
                ++this.numberOfPage;
                this.selectedAll =
                    this.allUsers
                        .slice(this.pageStep * this.numberOfPage, this.pageStep * this.numberOfPage + this.pageStep - 1);
            }
        }
        else if (this.numberOfSelectedList == 1) {
            if (this.pageStep * (this.numberOfPage + 1) < this.friendsUsers.length) {
                ++this.numberOfPage;
                this.selectedFriends =
                    this.friendsUsers
                        .slice(this.pageStep * this.numberOfPage, this.pageStep * this.numberOfPage + this.pageStep - 1);
            }
        }
        else if (this.numberOfSelectedList == 2) {
            if (this.pageStep * (this.numberOfPage + 1) < this.followersUsers.length) {
                ++this.numberOfPage;
                this.selectedFollowers =
                    this.followersUsers
                        .slice(this.pageStep * this.numberOfPage, this.pageStep * this.numberOfPage + this.pageStep);
            }
        }
        else if (this.numberOfSelectedList == 3) {
            if (this.pageStep * (this.numberOfPage + 1) < this.followingsUsers.length) {
                ++this.numberOfPage;
                this.selectedFollowings =
                    this.followingsUsers
                        .slice(this.pageStep * this.numberOfPage, this.pageStep * this.numberOfPage + this.pageStep - 1);
            }
        }
    }
    OnAllBtnClick() {
        //GetAllUsers
        this.GetAllUsers();
        this.selectedAll =
            this.allUsers.slice(0, this.pageStep);
        this.numberOfPage = 0;
        this.numberOfSelectedList = 0;
    }
    OnFriendsBtnClick() {
        //GetAllFriends
        this.selectedNicknames = this.friends.slice(0, this.pageStep);
        this.numberOfPage = 0;
        this.numberOfSelectedList = 1;
    }
    OnFollowersBtnClick() {
        //GetAllFollowers
        this.selectedNicknames = this.followers.slice(0, this.pageStep);
        this.numberOfPage = 0;
        this.numberOfSelectedList = 2;
    }
    OnFollowingsBtnClick() {
        //GetAllFollowings
        this.selectedNicknames = this.followings.slice(0, this.pageStep);
        this.numberOfPage = 0;
        this.numberOfSelectedList = 3;
    }
    OnUserBtnClick(userInfo) {
        this.isUserActionsWindowVisible = true;
        console.log("OnUserBtnClick: " + userInfo.Nickname);
        this.selectedUser = userInfo;
    }
    IsUserFriend(nick) {
        for (var i = 0; i < this.friendsUsers.length; i++) {
            if (this.friendsUsers[i].Nickname == nick) {
                return true;
            }
        }
        return false;
    }
    AddUserToFriends() {
        let users = {
            FirstId: this.accountData.UserId,
            SecondId: this.selectedUser.UserId
        };
        this.httpAuthService.AddUserToFriends(users);
    }
    RemoveUserFromFriends() {
        let users = {
            FirstId: this.accountData.UserId,
            SecondId: this.selectedUser.UserId
        };
        this.httpAuthService.RemoveUserFromFriends(users);
    }
    OnExitBtnClick() {
        this.isUserActionsWindowVisible = false;
    }
};
GameComponent.ctorParameters = () => [
    { type: _shared_Game_service__WEBPACK_IMPORTED_MODULE_3__["GameService"] },
    { type: _shared_HttpAuth_service__WEBPACK_IMPORTED_MODULE_2__["HttpAuthService"] }
];
GameComponent = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([
    Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Component"])({
        selector: 'app-game',
        template: tslib__WEBPACK_IMPORTED_MODULE_0__["__importDefault"](__webpack_require__(/*! raw-loader!./game.component.html */ "./node_modules/raw-loader/dist/cjs.js!./src/app/game/game.component.html")).default,
        providers: [_shared_Game_service__WEBPACK_IMPORTED_MODULE_3__["GameService"], _shared_HttpAuth_service__WEBPACK_IMPORTED_MODULE_2__["HttpAuthService"]],
        styles: [tslib__WEBPACK_IMPORTED_MODULE_0__["__importDefault"](__webpack_require__(/*! ./game.component.css */ "./src/app/game/game.component.css")).default]
    })
], GameComponent);



/***/ }),

/***/ "./src/app/login/login.component.css":
/*!*******************************************!*\
  !*** ./src/app/login/login.component.css ***!
  \*******************************************/
/*! exports provided: default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony default export */ __webpack_exports__["default"] = ("\r\n.main_login {\r\n    display: flex;\r\n    margin-top:200px;\r\n    align-items: center;\r\n    justify-content: center;\r\n}\r\n\r\n.background {\r\n    background-color: rgb(93, 72, 255);\r\n    border-radius: 20%;\r\n    padding: 60px;\r\n}\r\n\r\n.txt_login {\r\n    color: white;\r\n    display: flex;\r\n    justify-content: center;\r\n    align-items: center;\r\n}\r\n\r\ninput {\r\n    padding: 5px;\r\n    font-size: 30px;\r\n}\r\n\r\nbutton {\r\n    font-size: 40px;\r\n    display: flex;\r\n    justify-content: center;\r\n    align-items: center;\r\n}\r\n\r\n.link_registration { \r\n    color:white;\r\n    margin-top:3px; \r\n    margin-bottom:25px; \r\n    font-weight:bold; \r\n    float:left; \r\n}\r\n\r\na:link { \r\n    color:white;\r\n}\r\n\r\na:visited { \r\n    color:white;\r\n}\r\n\r\na:hover { \r\n    color:rgb(0, 47, 255);\r\n}\r\n\r\na:active { \r\n    color:rgb(111, 0, 255);\r\n}\r\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNyYy9hcHAvbG9naW4vbG9naW4uY29tcG9uZW50LmNzcyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiO0FBQ0E7SUFDSSxhQUFhO0lBQ2IsZ0JBQWdCO0lBQ2hCLG1CQUFtQjtJQUNuQix1QkFBdUI7QUFDM0I7O0FBRUE7SUFDSSxrQ0FBa0M7SUFDbEMsa0JBQWtCO0lBQ2xCLGFBQWE7QUFDakI7O0FBRUE7SUFDSSxZQUFZO0lBQ1osYUFBYTtJQUNiLHVCQUF1QjtJQUN2QixtQkFBbUI7QUFDdkI7O0FBR0E7SUFDSSxZQUFZO0lBQ1osZUFBZTtBQUNuQjs7QUFFQTtJQUNJLGVBQWU7SUFDZixhQUFhO0lBQ2IsdUJBQXVCO0lBQ3ZCLG1CQUFtQjtBQUN2Qjs7QUFFQTtJQUNJLFdBQVc7SUFDWCxjQUFjO0lBQ2Qsa0JBQWtCO0lBQ2xCLGdCQUFnQjtJQUNoQixVQUFVO0FBQ2Q7O0FBQ0E7SUFDSSxXQUFXO0FBQ2Y7O0FBQ0E7SUFDSSxXQUFXO0FBQ2Y7O0FBQ0E7SUFDSSxxQkFBcUI7QUFDekI7O0FBQ0E7SUFDSSxzQkFBc0I7QUFDMUIiLCJmaWxlIjoic3JjL2FwcC9sb2dpbi9sb2dpbi5jb21wb25lbnQuY3NzIiwic291cmNlc0NvbnRlbnQiOlsiXHJcbi5tYWluX2xvZ2luIHtcclxuICAgIGRpc3BsYXk6IGZsZXg7XHJcbiAgICBtYXJnaW4tdG9wOjIwMHB4O1xyXG4gICAgYWxpZ24taXRlbXM6IGNlbnRlcjtcclxuICAgIGp1c3RpZnktY29udGVudDogY2VudGVyO1xyXG59XHJcblxyXG4uYmFja2dyb3VuZCB7XHJcbiAgICBiYWNrZ3JvdW5kLWNvbG9yOiByZ2IoOTMsIDcyLCAyNTUpO1xyXG4gICAgYm9yZGVyLXJhZGl1czogMjAlO1xyXG4gICAgcGFkZGluZzogNjBweDtcclxufVxyXG5cclxuLnR4dF9sb2dpbiB7XHJcbiAgICBjb2xvcjogd2hpdGU7XHJcbiAgICBkaXNwbGF5OiBmbGV4O1xyXG4gICAganVzdGlmeS1jb250ZW50OiBjZW50ZXI7XHJcbiAgICBhbGlnbi1pdGVtczogY2VudGVyO1xyXG59XHJcblxyXG5cclxuaW5wdXQge1xyXG4gICAgcGFkZGluZzogNXB4O1xyXG4gICAgZm9udC1zaXplOiAzMHB4O1xyXG59XHJcblxyXG5idXR0b24ge1xyXG4gICAgZm9udC1zaXplOiA0MHB4O1xyXG4gICAgZGlzcGxheTogZmxleDtcclxuICAgIGp1c3RpZnktY29udGVudDogY2VudGVyO1xyXG4gICAgYWxpZ24taXRlbXM6IGNlbnRlcjtcclxufVxyXG5cclxuLmxpbmtfcmVnaXN0cmF0aW9uIHsgXHJcbiAgICBjb2xvcjp3aGl0ZTtcclxuICAgIG1hcmdpbi10b3A6M3B4OyBcclxuICAgIG1hcmdpbi1ib3R0b206MjVweDsgXHJcbiAgICBmb250LXdlaWdodDpib2xkOyBcclxuICAgIGZsb2F0OmxlZnQ7IFxyXG59XHJcbmE6bGluayB7IFxyXG4gICAgY29sb3I6d2hpdGU7XHJcbn1cclxuYTp2aXNpdGVkIHsgXHJcbiAgICBjb2xvcjp3aGl0ZTtcclxufVxyXG5hOmhvdmVyIHsgXHJcbiAgICBjb2xvcjpyZ2IoMCwgNDcsIDI1NSk7XHJcbn1cclxuYTphY3RpdmUgeyBcclxuICAgIGNvbG9yOnJnYigxMTEsIDAsIDI1NSk7XHJcbn0iXX0= */");

/***/ }),

/***/ "./src/app/login/login.component.ts":
/*!******************************************!*\
  !*** ./src/app/login/login.component.ts ***!
  \******************************************/
/*! exports provided: LoginComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "LoginComponent", function() { return LoginComponent; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "./node_modules/tslib/tslib.es6.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm2015/core.js");
/* harmony import */ var _shared_HttpAuth_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../shared/HttpAuth.service */ "./src/app/shared/HttpAuth.service.ts");



let LoginComponent = class LoginComponent {
    constructor(httpAuth) {
        this.httpAuth = httpAuth;
        this.LoginPostData = {
            Nickname: '',
            PasswordHash: ''
        };
    }
    ngOnInit() {
    }
    onClick() {
        console.log("LOGIN COMPONENT: " +
            "nickname: " + this.LoginPostData.Nickname +
            " pass: " + this.LoginPostData.PasswordHash);
        this.httpAuth.login(this.LoginPostData);
    }
};
LoginComponent.ctorParameters = () => [
    { type: _shared_HttpAuth_service__WEBPACK_IMPORTED_MODULE_2__["HttpAuthService"] }
];
LoginComponent = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([
    Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Component"])({
        selector: 'app-login',
        template: tslib__WEBPACK_IMPORTED_MODULE_0__["__importDefault"](__webpack_require__(/*! raw-loader!./login.component.html */ "./node_modules/raw-loader/dist/cjs.js!./src/app/login/login.component.html")).default,
        providers: [_shared_HttpAuth_service__WEBPACK_IMPORTED_MODULE_2__["HttpAuthService"]],
        styles: [tslib__WEBPACK_IMPORTED_MODULE_0__["__importDefault"](__webpack_require__(/*! ./login.component.css */ "./src/app/login/login.component.css")).default]
    })
], LoginComponent);



/***/ }),

/***/ "./src/app/main/main.component.css":
/*!*****************************************!*\
  !*** ./src/app/main/main.component.css ***!
  \*****************************************/
/*! exports provided: default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony default export */ __webpack_exports__["default"] = ("\r\n.logo {\r\n    display: flex;\r\n    margin-top:200px;\r\n    align-items: center;\r\n    justify-content: center;\r\n    font-size: 190px;\r\n    color: white;\r\n}\r\n\r\n.box{\r\n    display: flex;\r\n    align-items: center;\r\n    justify-content: center;\r\n    height: 100%;\r\n}\r\n\r\n.btn_try_it_now {\r\n    font-size: 40px;\r\n    text-align: center;\r\n    padding-top: 15px;\r\n    padding-left: 35px;\r\n    padding-bottom: 15px;\r\n    width: 350px;\r\n    \r\n    background-color: rgb(4, 121, 255);\r\n    opacity: 0.6;\r\n    color: white;\r\n    cursor: pointer;\r\n\r\n    box-shadow: 0 4px 16px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0,0,0,0.19);\r\n    transition: all 0.5s;\r\n}\r\n\r\n.btn_try_it_now span:after {\r\n    content: '\\00bb';\r\n    position: relative;\r\n    top: 0;\r\n    right: -20px;\r\n    transition: 0.5s;\r\n    opacity: 0;\r\n}\r\n\r\n.btn_try_it_now:hover {\r\n    opacity: 1.0;\r\n}\r\n\r\n.btn_try_it_now:hover span{\r\n    padding-right: 20px;\r\n}\r\n\r\n.btn_try_it_now:hover span:after {\r\n    right: 0;\r\n    opacity: 1;\r\n}\r\n\r\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNyYy9hcHAvbWFpbi9tYWluLmNvbXBvbmVudC5jc3MiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IjtBQUNBO0lBQ0ksYUFBYTtJQUNiLGdCQUFnQjtJQUNoQixtQkFBbUI7SUFDbkIsdUJBQXVCO0lBQ3ZCLGdCQUFnQjtJQUNoQixZQUFZO0FBQ2hCOztBQUVBO0lBQ0ksYUFBYTtJQUNiLG1CQUFtQjtJQUNuQix1QkFBdUI7SUFDdkIsWUFBWTtBQUNoQjs7QUFFQTtJQUNJLGVBQWU7SUFDZixrQkFBa0I7SUFDbEIsaUJBQWlCO0lBQ2pCLGtCQUFrQjtJQUNsQixvQkFBb0I7SUFDcEIsWUFBWTs7SUFFWixrQ0FBa0M7SUFDbEMsWUFBWTtJQUNaLFlBQVk7SUFDWixlQUFlOztJQUVmLDBFQUEwRTtJQUMxRSxvQkFBb0I7QUFDeEI7O0FBRUE7SUFDSSxnQkFBZ0I7SUFDaEIsa0JBQWtCO0lBQ2xCLE1BQU07SUFDTixZQUFZO0lBQ1osZ0JBQWdCO0lBQ2hCLFVBQVU7QUFDZDs7QUFFQTtJQUNJLFlBQVk7QUFDaEI7O0FBRUE7SUFDSSxtQkFBbUI7QUFDdkI7O0FBRUE7SUFDSSxRQUFRO0lBQ1IsVUFBVTtBQUNkIiwiZmlsZSI6InNyYy9hcHAvbWFpbi9tYWluLmNvbXBvbmVudC5jc3MiLCJzb3VyY2VzQ29udGVudCI6WyJcclxuLmxvZ28ge1xyXG4gICAgZGlzcGxheTogZmxleDtcclxuICAgIG1hcmdpbi10b3A6MjAwcHg7XHJcbiAgICBhbGlnbi1pdGVtczogY2VudGVyO1xyXG4gICAganVzdGlmeS1jb250ZW50OiBjZW50ZXI7XHJcbiAgICBmb250LXNpemU6IDE5MHB4O1xyXG4gICAgY29sb3I6IHdoaXRlO1xyXG59XHJcblxyXG4uYm94e1xyXG4gICAgZGlzcGxheTogZmxleDtcclxuICAgIGFsaWduLWl0ZW1zOiBjZW50ZXI7XHJcbiAgICBqdXN0aWZ5LWNvbnRlbnQ6IGNlbnRlcjtcclxuICAgIGhlaWdodDogMTAwJTtcclxufVxyXG5cclxuLmJ0bl90cnlfaXRfbm93IHtcclxuICAgIGZvbnQtc2l6ZTogNDBweDtcclxuICAgIHRleHQtYWxpZ246IGNlbnRlcjtcclxuICAgIHBhZGRpbmctdG9wOiAxNXB4O1xyXG4gICAgcGFkZGluZy1sZWZ0OiAzNXB4O1xyXG4gICAgcGFkZGluZy1ib3R0b206IDE1cHg7XHJcbiAgICB3aWR0aDogMzUwcHg7XHJcbiAgICBcclxuICAgIGJhY2tncm91bmQtY29sb3I6IHJnYig0LCAxMjEsIDI1NSk7XHJcbiAgICBvcGFjaXR5OiAwLjY7XHJcbiAgICBjb2xvcjogd2hpdGU7XHJcbiAgICBjdXJzb3I6IHBvaW50ZXI7XHJcblxyXG4gICAgYm94LXNoYWRvdzogMCA0cHggMTZweCAwIHJnYmEoMCwgMCwgMCwgMC4yKSwgMCA2cHggMjBweCAwIHJnYmEoMCwwLDAsMC4xOSk7XHJcbiAgICB0cmFuc2l0aW9uOiBhbGwgMC41cztcclxufVxyXG5cclxuLmJ0bl90cnlfaXRfbm93IHNwYW46YWZ0ZXIge1xyXG4gICAgY29udGVudDogJ1xcMDBiYic7XHJcbiAgICBwb3NpdGlvbjogcmVsYXRpdmU7XHJcbiAgICB0b3A6IDA7XHJcbiAgICByaWdodDogLTIwcHg7XHJcbiAgICB0cmFuc2l0aW9uOiAwLjVzO1xyXG4gICAgb3BhY2l0eTogMDtcclxufVxyXG5cclxuLmJ0bl90cnlfaXRfbm93OmhvdmVyIHtcclxuICAgIG9wYWNpdHk6IDEuMDtcclxufVxyXG5cclxuLmJ0bl90cnlfaXRfbm93OmhvdmVyIHNwYW57XHJcbiAgICBwYWRkaW5nLXJpZ2h0OiAyMHB4O1xyXG59XHJcblxyXG4uYnRuX3RyeV9pdF9ub3c6aG92ZXIgc3BhbjphZnRlciB7XHJcbiAgICByaWdodDogMDtcclxuICAgIG9wYWNpdHk6IDE7XHJcbn1cclxuIl19 */");

/***/ }),

/***/ "./src/app/main/main.component.ts":
/*!****************************************!*\
  !*** ./src/app/main/main.component.ts ***!
  \****************************************/
/*! exports provided: MainComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "MainComponent", function() { return MainComponent; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "./node_modules/tslib/tslib.es6.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm2015/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm2015/router.js");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm2015/http.js");




let MainComponent = class MainComponent {
    constructor(http, router) {
        this.http = http;
        this.router = router;
        this.base_url = '';
    }
    ngOnInit() {
    }
    onClick() {
        this.router.navigate(['/login']);
    }
};
MainComponent.ctorParameters = () => [
    { type: _angular_common_http__WEBPACK_IMPORTED_MODULE_3__["HttpClient"] },
    { type: _angular_router__WEBPACK_IMPORTED_MODULE_2__["Router"] }
];
MainComponent = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([
    Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Component"])({
        selector: 'app-main',
        template: tslib__WEBPACK_IMPORTED_MODULE_0__["__importDefault"](__webpack_require__(/*! raw-loader!./main.component.html */ "./node_modules/raw-loader/dist/cjs.js!./src/app/main/main.component.html")).default,
        styles: [tslib__WEBPACK_IMPORTED_MODULE_0__["__importDefault"](__webpack_require__(/*! ./main.component.css */ "./src/app/main/main.component.css")).default]
    })
], MainComponent);



/***/ }),

/***/ "./src/app/not-found-page/not-found-page.component.css":
/*!*************************************************************!*\
  !*** ./src/app/not-found-page/not-found-page.component.css ***!
  \*************************************************************/
/*! exports provided: default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony default export */ __webpack_exports__["default"] = ("\r\np {\r\n    color: red;\r\n    font-size: 80px;\r\n    text-align: center;\r\n    margin-top: 2%;\r\n}\r\n\r\n.time {\r\n    color: rgb(255, 255, 255);\r\n    font-size: 10px;\r\n    text-align: center;\r\n}\r\n\r\n.error {\r\n    color: rgb(0, 255, 255);\r\n    font-size: 10px;\r\n    -webkit-animation-name: errorAnimation;\r\n            animation-name: errorAnimation;\r\n    -webkit-animation-duration: 1s;\r\n            animation-duration: 1s;\r\n    -webkit-animation-iteration-count: infinite;\r\n            animation-iteration-count: infinite;\r\n    animation-direction: alternate-reverse;\r\n}\r\n\r\n@-webkit-keyframes errorAnimation {\r\n    from \r\n    { \r\n        color: rgb(0, 255, 255);\r\n        font-size: 10px; \r\n        opacity: 0.1;\r\n    }\r\n    to \r\n    { \r\n        transform: rotate(6deg);\r\n        color: rgb(255, 251, 0);\r\n        font-size: 15px; \r\n        opacity: 1.0;\r\n    }\r\n}\r\n\r\n@keyframes errorAnimation {\r\n    from \r\n    { \r\n        color: rgb(0, 255, 255);\r\n        font-size: 10px; \r\n        opacity: 0.1;\r\n    }\r\n    to \r\n    { \r\n        transform: rotate(6deg);\r\n        color: rgb(255, 251, 0);\r\n        font-size: 15px; \r\n        opacity: 1.0;\r\n    }\r\n}\r\n\r\n.error_2 {\r\n    color:white;\r\n    font-size: 10px;\r\n    text-align: right;\r\n    -webkit-animation-name: error2Animation;\r\n            animation-name: error2Animation;\r\n    -webkit-animation-duration: 1s;\r\n            animation-duration: 1s;\r\n    -webkit-animation-iteration-count: infinite;\r\n            animation-iteration-count: infinite;\r\n    animation-direction: alternate-reverse;\r\n}\r\n\r\n@-webkit-keyframes error2Animation {\r\n    from \r\n    { \r\n        color: rgb(0, 255, 255);\r\n        font-size: 10px; \r\n        opacity: 0.1;\r\n    }\r\n    to \r\n    { \r\n        transform: rotate(6deg);\r\n        color: rgb(255, 251, 0);\r\n        font-size: 15px; \r\n        opacity: 1.0;\r\n    }\r\n}\r\n\r\n@keyframes error2Animation {\r\n    from \r\n    { \r\n        color: rgb(0, 255, 255);\r\n        font-size: 10px; \r\n        opacity: 0.1;\r\n    }\r\n    to \r\n    { \r\n        transform: rotate(6deg);\r\n        color: rgb(255, 251, 0);\r\n        font-size: 15px; \r\n        opacity: 1.0;\r\n    }\r\n}\r\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNyYy9hcHAvbm90LWZvdW5kLXBhZ2Uvbm90LWZvdW5kLXBhZ2UuY29tcG9uZW50LmNzcyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiO0FBQ0E7SUFDSSxVQUFVO0lBQ1YsZUFBZTtJQUNmLGtCQUFrQjtJQUNsQixjQUFjO0FBQ2xCOztBQUVBO0lBQ0kseUJBQXlCO0lBQ3pCLGVBQWU7SUFDZixrQkFBa0I7QUFDdEI7O0FBRUE7SUFDSSx1QkFBdUI7SUFDdkIsZUFBZTtJQUNmLHNDQUE4QjtZQUE5Qiw4QkFBOEI7SUFDOUIsOEJBQXNCO1lBQXRCLHNCQUFzQjtJQUN0QiwyQ0FBbUM7WUFBbkMsbUNBQW1DO0lBQ25DLHNDQUFzQztBQUMxQzs7QUFFQTtJQUNJOztRQUVJLHVCQUF1QjtRQUN2QixlQUFlO1FBQ2YsWUFBWTtJQUNoQjtJQUNBOztRQUVJLHVCQUF1QjtRQUN2Qix1QkFBdUI7UUFDdkIsZUFBZTtRQUNmLFlBQVk7SUFDaEI7QUFDSjs7QUFkQTtJQUNJOztRQUVJLHVCQUF1QjtRQUN2QixlQUFlO1FBQ2YsWUFBWTtJQUNoQjtJQUNBOztRQUVJLHVCQUF1QjtRQUN2Qix1QkFBdUI7UUFDdkIsZUFBZTtRQUNmLFlBQVk7SUFDaEI7QUFDSjs7QUFFQTtJQUNJLFdBQVc7SUFDWCxlQUFlO0lBQ2YsaUJBQWlCO0lBQ2pCLHVDQUErQjtZQUEvQiwrQkFBK0I7SUFDL0IsOEJBQXNCO1lBQXRCLHNCQUFzQjtJQUN0QiwyQ0FBbUM7WUFBbkMsbUNBQW1DO0lBQ25DLHNDQUFzQztBQUMxQzs7QUFFQTtJQUNJOztRQUVJLHVCQUF1QjtRQUN2QixlQUFlO1FBQ2YsWUFBWTtJQUNoQjtJQUNBOztRQUVJLHVCQUF1QjtRQUN2Qix1QkFBdUI7UUFDdkIsZUFBZTtRQUNmLFlBQVk7SUFDaEI7QUFDSjs7QUFkQTtJQUNJOztRQUVJLHVCQUF1QjtRQUN2QixlQUFlO1FBQ2YsWUFBWTtJQUNoQjtJQUNBOztRQUVJLHVCQUF1QjtRQUN2Qix1QkFBdUI7UUFDdkIsZUFBZTtRQUNmLFlBQVk7SUFDaEI7QUFDSiIsImZpbGUiOiJzcmMvYXBwL25vdC1mb3VuZC1wYWdlL25vdC1mb3VuZC1wYWdlLmNvbXBvbmVudC5jc3MiLCJzb3VyY2VzQ29udGVudCI6WyJcclxucCB7XHJcbiAgICBjb2xvcjogcmVkO1xyXG4gICAgZm9udC1zaXplOiA4MHB4O1xyXG4gICAgdGV4dC1hbGlnbjogY2VudGVyO1xyXG4gICAgbWFyZ2luLXRvcDogMiU7XHJcbn1cclxuXHJcbi50aW1lIHtcclxuICAgIGNvbG9yOiByZ2IoMjU1LCAyNTUsIDI1NSk7XHJcbiAgICBmb250LXNpemU6IDEwcHg7XHJcbiAgICB0ZXh0LWFsaWduOiBjZW50ZXI7XHJcbn1cclxuXHJcbi5lcnJvciB7XHJcbiAgICBjb2xvcjogcmdiKDAsIDI1NSwgMjU1KTtcclxuICAgIGZvbnQtc2l6ZTogMTBweDtcclxuICAgIGFuaW1hdGlvbi1uYW1lOiBlcnJvckFuaW1hdGlvbjtcclxuICAgIGFuaW1hdGlvbi1kdXJhdGlvbjogMXM7XHJcbiAgICBhbmltYXRpb24taXRlcmF0aW9uLWNvdW50OiBpbmZpbml0ZTtcclxuICAgIGFuaW1hdGlvbi1kaXJlY3Rpb246IGFsdGVybmF0ZS1yZXZlcnNlO1xyXG59XHJcblxyXG5Aa2V5ZnJhbWVzIGVycm9yQW5pbWF0aW9uIHtcclxuICAgIGZyb20gXHJcbiAgICB7IFxyXG4gICAgICAgIGNvbG9yOiByZ2IoMCwgMjU1LCAyNTUpO1xyXG4gICAgICAgIGZvbnQtc2l6ZTogMTBweDsgXHJcbiAgICAgICAgb3BhY2l0eTogMC4xO1xyXG4gICAgfVxyXG4gICAgdG8gXHJcbiAgICB7IFxyXG4gICAgICAgIHRyYW5zZm9ybTogcm90YXRlKDZkZWcpO1xyXG4gICAgICAgIGNvbG9yOiByZ2IoMjU1LCAyNTEsIDApO1xyXG4gICAgICAgIGZvbnQtc2l6ZTogMTVweDsgXHJcbiAgICAgICAgb3BhY2l0eTogMS4wO1xyXG4gICAgfVxyXG59XHJcblxyXG4uZXJyb3JfMiB7XHJcbiAgICBjb2xvcjp3aGl0ZTtcclxuICAgIGZvbnQtc2l6ZTogMTBweDtcclxuICAgIHRleHQtYWxpZ246IHJpZ2h0O1xyXG4gICAgYW5pbWF0aW9uLW5hbWU6IGVycm9yMkFuaW1hdGlvbjtcclxuICAgIGFuaW1hdGlvbi1kdXJhdGlvbjogMXM7XHJcbiAgICBhbmltYXRpb24taXRlcmF0aW9uLWNvdW50OiBpbmZpbml0ZTtcclxuICAgIGFuaW1hdGlvbi1kaXJlY3Rpb246IGFsdGVybmF0ZS1yZXZlcnNlO1xyXG59XHJcblxyXG5Aa2V5ZnJhbWVzIGVycm9yMkFuaW1hdGlvbiB7XHJcbiAgICBmcm9tIFxyXG4gICAgeyBcclxuICAgICAgICBjb2xvcjogcmdiKDAsIDI1NSwgMjU1KTtcclxuICAgICAgICBmb250LXNpemU6IDEwcHg7IFxyXG4gICAgICAgIG9wYWNpdHk6IDAuMTtcclxuICAgIH1cclxuICAgIHRvIFxyXG4gICAgeyBcclxuICAgICAgICB0cmFuc2Zvcm06IHJvdGF0ZSg2ZGVnKTtcclxuICAgICAgICBjb2xvcjogcmdiKDI1NSwgMjUxLCAwKTtcclxuICAgICAgICBmb250LXNpemU6IDE1cHg7IFxyXG4gICAgICAgIG9wYWNpdHk6IDEuMDtcclxuICAgIH1cclxufSJdfQ== */");

/***/ }),

/***/ "./src/app/not-found-page/not-found-page.component.ts":
/*!************************************************************!*\
  !*** ./src/app/not-found-page/not-found-page.component.ts ***!
  \************************************************************/
/*! exports provided: NotFoundPageComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "NotFoundPageComponent", function() { return NotFoundPageComponent; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "./node_modules/tslib/tslib.es6.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm2015/core.js");


let NotFoundPageComponent = class NotFoundPageComponent {
    constructor() {
        setInterval(() => { this.incriment(); }, 1000);
    }
    ngOnInit() {
        this.wastedSeconds = 0;
    }
    incriment() {
        this.wastedSeconds++;
    }
};
NotFoundPageComponent = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([
    Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Component"])({
        selector: 'app-not-found-page',
        template: tslib__WEBPACK_IMPORTED_MODULE_0__["__importDefault"](__webpack_require__(/*! raw-loader!./not-found-page.component.html */ "./node_modules/raw-loader/dist/cjs.js!./src/app/not-found-page/not-found-page.component.html")).default,
        styles: [tslib__WEBPACK_IMPORTED_MODULE_0__["__importDefault"](__webpack_require__(/*! ./not-found-page.component.css */ "./src/app/not-found-page/not-found-page.component.css")).default]
    })
], NotFoundPageComponent);



/***/ }),

/***/ "./src/app/registration/registration.component.css":
/*!*********************************************************!*\
  !*** ./src/app/registration/registration.component.css ***!
  \*********************************************************/
/*! exports provided: default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony default export */ __webpack_exports__["default"] = ("\r\n.main_registration {\r\n    display: flex;\r\n    margin-top:200px;\r\n    align-items: center;\r\n    justify-content: center;\r\n}\r\n\r\n.background {\r\n    background-color: rgb(93, 72, 255);\r\n    border-radius: 20%;\r\n    padding: 60px;\r\n}\r\n\r\n.txt_registration {\r\n    color: white;\r\n    display: flex;\r\n    justify-content: center;\r\n    align-items: center;\r\n}\r\n\r\ninput {\r\n    padding: 5px;\r\n    font-size: 30px;\r\n    margin-left: 30px;\r\n}\r\n\r\nbutton {\r\n    font-size: 30px;\r\n    margin-left: 65px;\r\n}\r\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbInNyYy9hcHAvcmVnaXN0cmF0aW9uL3JlZ2lzdHJhdGlvbi5jb21wb25lbnQuY3NzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiI7QUFDQTtJQUNJLGFBQWE7SUFDYixnQkFBZ0I7SUFDaEIsbUJBQW1CO0lBQ25CLHVCQUF1QjtBQUMzQjs7QUFFQTtJQUNJLGtDQUFrQztJQUNsQyxrQkFBa0I7SUFDbEIsYUFBYTtBQUNqQjs7QUFFQTtJQUNJLFlBQVk7SUFDWixhQUFhO0lBQ2IsdUJBQXVCO0lBQ3ZCLG1CQUFtQjtBQUN2Qjs7QUFFQTtJQUNJLFlBQVk7SUFDWixlQUFlO0lBQ2YsaUJBQWlCO0FBQ3JCOztBQUVBO0lBQ0ksZUFBZTtJQUNmLGlCQUFpQjtBQUNyQiIsImZpbGUiOiJzcmMvYXBwL3JlZ2lzdHJhdGlvbi9yZWdpc3RyYXRpb24uY29tcG9uZW50LmNzcyIsInNvdXJjZXNDb250ZW50IjpbIlxyXG4ubWFpbl9yZWdpc3RyYXRpb24ge1xyXG4gICAgZGlzcGxheTogZmxleDtcclxuICAgIG1hcmdpbi10b3A6MjAwcHg7XHJcbiAgICBhbGlnbi1pdGVtczogY2VudGVyO1xyXG4gICAganVzdGlmeS1jb250ZW50OiBjZW50ZXI7XHJcbn1cclxuXHJcbi5iYWNrZ3JvdW5kIHtcclxuICAgIGJhY2tncm91bmQtY29sb3I6IHJnYig5MywgNzIsIDI1NSk7XHJcbiAgICBib3JkZXItcmFkaXVzOiAyMCU7XHJcbiAgICBwYWRkaW5nOiA2MHB4O1xyXG59XHJcblxyXG4udHh0X3JlZ2lzdHJhdGlvbiB7XHJcbiAgICBjb2xvcjogd2hpdGU7XHJcbiAgICBkaXNwbGF5OiBmbGV4O1xyXG4gICAganVzdGlmeS1jb250ZW50OiBjZW50ZXI7XHJcbiAgICBhbGlnbi1pdGVtczogY2VudGVyO1xyXG59XHJcblxyXG5pbnB1dCB7XHJcbiAgICBwYWRkaW5nOiA1cHg7XHJcbiAgICBmb250LXNpemU6IDMwcHg7XHJcbiAgICBtYXJnaW4tbGVmdDogMzBweDtcclxufVxyXG5cclxuYnV0dG9uIHtcclxuICAgIGZvbnQtc2l6ZTogMzBweDtcclxuICAgIG1hcmdpbi1sZWZ0OiA2NXB4O1xyXG59Il19 */");

/***/ }),

/***/ "./src/app/registration/registration.component.ts":
/*!********************************************************!*\
  !*** ./src/app/registration/registration.component.ts ***!
  \********************************************************/
/*! exports provided: RegistrationComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "RegistrationComponent", function() { return RegistrationComponent; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "./node_modules/tslib/tslib.es6.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm2015/core.js");
/* harmony import */ var _shared_HttpAuth_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../shared/HttpAuth.service */ "./src/app/shared/HttpAuth.service.ts");



let RegistrationComponent = class RegistrationComponent {
    constructor(httpAuth) {
        this.httpAuth = httpAuth;
        this.PostData = {
            FirstName: '',
            SecondName: '',
            PasswordHash: '',
            Nickname: '',
            Email: ''
        };
    }
    ngOnInit() {
    }
    onClick() {
        this.httpAuth.registration(this.PostData);
    }
};
RegistrationComponent.ctorParameters = () => [
    { type: _shared_HttpAuth_service__WEBPACK_IMPORTED_MODULE_2__["HttpAuthService"] }
];
RegistrationComponent = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([
    Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Component"])({
        selector: 'app-registration',
        template: tslib__WEBPACK_IMPORTED_MODULE_0__["__importDefault"](__webpack_require__(/*! raw-loader!./registration.component.html */ "./node_modules/raw-loader/dist/cjs.js!./src/app/registration/registration.component.html")).default,
        providers: [_shared_HttpAuth_service__WEBPACK_IMPORTED_MODULE_2__["HttpAuthService"]],
        styles: [tslib__WEBPACK_IMPORTED_MODULE_0__["__importDefault"](__webpack_require__(/*! ./registration.component.css */ "./src/app/registration/registration.component.css")).default]
    })
], RegistrationComponent);



/***/ }),

/***/ "./src/app/shared/Game.service.ts":
/*!****************************************!*\
  !*** ./src/app/shared/Game.service.ts ***!
  \****************************************/
/*! exports provided: GameService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "GameService", function() { return GameService; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "./node_modules/tslib/tslib.es6.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm2015/core.js");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm2015/http.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm2015/router.js");




let GameService = class GameService {
    constructor(httpClient, router) {
        this.httpClient = httpClient;
        this.router = router;
    }
    SwitchToGame() {
        this.router.navigate(["/game"]);
    }
    GetCoreById(coreId) {
        return null;
    }
    CasernGetNumberOfWarriors(coreId) {
        return 0;
    }
    CasernGetNumberOfAttackAircraft(coreId) {
        return 0;
    }
    CoreBuildCasern(coreId) {
        //post
    }
    CoreBuildGoldMining(coreId) {
        //post
    }
    CoreBuildDefenceTower(coreId) {
        //post
    }
    BaseProduceWorker(coreId) {
        //post
    }
    CasernProduceWarrior(coreId) {
        //post
    }
    CasernProduceAttackAircraft(coreId) {
        //post
    }
    GoldMiningAddWorker(coreId) {
        //post
    }
};
GameService.ctorParameters = () => [
    { type: _angular_common_http__WEBPACK_IMPORTED_MODULE_2__["HttpClient"] },
    { type: _angular_router__WEBPACK_IMPORTED_MODULE_3__["Router"] }
];
GameService = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([
    Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Injectable"])()
], GameService);



/***/ }),

/***/ "./src/app/shared/GameGuard.ts":
/*!*************************************!*\
  !*** ./src/app/shared/GameGuard.ts ***!
  \*************************************/
/*! exports provided: GameGuard */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "GameGuard", function() { return GameGuard; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "./node_modules/tslib/tslib.es6.js");

class GameGuard {
    constructor() { }
    canActivate(route, state) {
        return true; //confirm('Wanna go?');
    }
}


/***/ }),

/***/ "./src/app/shared/HttpAuth.service.ts":
/*!********************************************!*\
  !*** ./src/app/shared/HttpAuth.service.ts ***!
  \********************************************/
/*! exports provided: HttpAuthService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "HttpAuthService", function() { return HttpAuthService; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "./node_modules/tslib/tslib.es6.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm2015/core.js");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm2015/http.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm2015/router.js");
/* harmony import */ var ts_md5_dist_md5__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ts-md5/dist/md5 */ "./node_modules/ts-md5/dist/md5.js");
/* harmony import */ var ts_md5_dist_md5__WEBPACK_IMPORTED_MODULE_4___default = /*#__PURE__*/__webpack_require__.n(ts_md5_dist_md5__WEBPACK_IMPORTED_MODULE_4__);





let HttpAuthService = class HttpAuthService {
    constructor(httpClient, router) {
        this.httpClient = httpClient;
        this.router = router;
        this.loginUrl = 
        //"http://httpbin.org/post"; 
        "http://localhost:6430/Auth/Login";
        this.registrationUrl = "http://localhost:6430/Auth/Registration";
        this.getAccountUrl = "http://localhost:6430/Auth/GetAccountData";
        this.addUserToFriendsUrl = "http://localhost:6430/Auth/AddUserToFriends";
        this.removeUserFromFriendsUrl = "http://localhost:6430/Auth/RemoveUserFromFriends";
        this.getAllUsersUrl = "http://localhost:6430/Auth/GetAllUsersInfo";
        this.getUsersFriendsUrl = "http://localhost:6430/Auth/GetUsersFriendsInfo";
        this.getUsersFollowersUrl = "http://localhost:6430/Auth/GetUsersFollowersInfo";
        this.getUsersFollowingsUrl = "http://localhost:6430/Auth/GetUsersFollowingsInfo";
        this.signOutUrl = "http://localhost:6430/Auth/SignOut";
    }
    login(loginPostData) {
        loginPostData.PasswordHash =
            ts_md5_dist_md5__WEBPACK_IMPORTED_MODULE_4__["Md5"].hashStr(loginPostData.PasswordHash)
                .toString();
        console.log('login');
        let oprions = {
            headers: new _angular_common_http__WEBPACK_IMPORTED_MODULE_2__["HttpHeaders"]({
                'Content-Type': 'application/json'
            })
        };
        console.log("login [post]: " +
            loginPostData.Nickname
            + " " +
            loginPostData.PasswordHash);
        let dataToPost = JSON.stringify(loginPostData);
        console.log("data to post: " + dataToPost);
        this.httpClient.post(this.loginUrl, dataToPost, oprions)
            .subscribe((data) => {
            if (data.toString() === "true") {
                console.log("login success [response: " + data.toString() + "]");
                if (data.toString() === "true") {
                    console.log("routing to a game");
                    this.router.navigate(['/game-menu']);
                }
                else {
                    console.log("obj isnt correct [routing to a login]");
                    this.router.navigate(['/login']);
                }
            }
        }, error => console.log("error: " + error));
    }
    registration(registrationPostData) {
        registrationPostData.PasswordHash =
            ts_md5_dist_md5__WEBPACK_IMPORTED_MODULE_4__["Md5"].hashStr(registrationPostData.PasswordHash)
                .toString();
        console.log('registration');
        let oprions = {
            headers: new _angular_common_http__WEBPACK_IMPORTED_MODULE_2__["HttpHeaders"]({
                'Content-Type': 'application/json'
            })
        };
        let dataToPost = JSON.stringify(registrationPostData);
        console.log("data to post: " + dataToPost);
        this.httpClient.post(this.registrationUrl, dataToPost, oprions)
            .subscribe(data => {
            if (data.toString().length > 0) {
                console.log("registration success [response: " + data.toString() + "]");
                if (data.toString() === "true") {
                    this.router.navigate(['/game-menu']);
                }
                else {
                    this.router.navigate(['/game-menu']);
                    //this.router.navigate(['/registration']);
                }
            }
        }, error => console.log(error));
    }
    GetAccountData() {
        console.log('GetAccountData:');
        return this.httpClient.get(this.getAccountUrl);
    }
    AddUserToFriends(users) {
        this.httpClient.post(this.addUserToFriendsUrl, users)
            .subscribe(data => console.log("success"), error => console.log("error" + error));
        ;
    }
    RemoveUserFromFriends(users) {
        this.httpClient.post(this.removeUserFromFriendsUrl, users);
    }
    GetAllUsers() {
        console.log("GetAllUserInfo");
        let result = this.httpClient.get(this.getAllUsersUrl);
        result.forEach(element => {
            for (var i = 0; i < element.length; i++) {
                console.log("user nick: " + element[i].Nickname);
            }
        });
        return result;
    }
    GetUsersFriendsInfo(userId) {
        return this.httpClient
            .get(this.getUsersFriendsUrl);
    }
    GetUsersFollowersInfo(userId) {
        return this.httpClient
            .get(this.getUsersFollowersUrl);
    }
    GetUsersFollowingsInfo(userId) {
        return this.httpClient
            .get(this.getUsersFollowingsUrl);
    }
    SignOut() {
        //http post
        this.httpClient.post(this.signOutUrl, "");
    }
};
HttpAuthService.ctorParameters = () => [
    { type: _angular_common_http__WEBPACK_IMPORTED_MODULE_2__["HttpClient"] },
    { type: _angular_router__WEBPACK_IMPORTED_MODULE_3__["Router"] }
];
HttpAuthService = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([
    Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Injectable"])()
], HttpAuthService);



/***/ }),

/***/ "./src/environments/environment.ts":
/*!*****************************************!*\
  !*** ./src/environments/environment.ts ***!
  \*****************************************/
/*! exports provided: environment */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "environment", function() { return environment; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "./node_modules/tslib/tslib.es6.js");
// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

const environment = {
    production: false
};
/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.


/***/ }),

/***/ "./src/main.ts":
/*!*********************!*\
  !*** ./src/main.ts ***!
  \*********************/
/*! no exports provided */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "./node_modules/tslib/tslib.es6.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm2015/core.js");
/* harmony import */ var _angular_platform_browser_dynamic__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/platform-browser-dynamic */ "./node_modules/@angular/platform-browser-dynamic/fesm2015/platform-browser-dynamic.js");
/* harmony import */ var _app_app_module__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./app/app.module */ "./src/app/app.module.ts");
/* harmony import */ var _environments_environment__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./environments/environment */ "./src/environments/environment.ts");





if (_environments_environment__WEBPACK_IMPORTED_MODULE_4__["environment"].production) {
    Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["enableProdMode"])();
}
Object(_angular_platform_browser_dynamic__WEBPACK_IMPORTED_MODULE_2__["platformBrowserDynamic"])().bootstrapModule(_app_app_module__WEBPACK_IMPORTED_MODULE_3__["AppModule"])
    .catch(err => console.error(err));


/***/ }),

/***/ 0:
/*!***************************!*\
  !*** multi ./src/main.ts ***!
  \***************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(/*! E:\General\Programming\C_Sharp\EpamPractice\EpicGame\EpicGame\EpicGameWeb\Client\src\main.ts */"./src/main.ts");


/***/ })

},[[0,"runtime","vendor"]]]);
//# sourceMappingURL=main-es2015.js.map