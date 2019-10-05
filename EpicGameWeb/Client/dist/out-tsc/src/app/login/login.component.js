import { __decorate } from "tslib";
import { Component } from '@angular/core';
import { HttpAuthService } from '../shared/HttpAuth.service';
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
        this.httpAuth.login(this.LoginPostData);
    }
};
LoginComponent = __decorate([
    Component({
        selector: 'app-login',
        templateUrl: './login.component.html',
        styleUrls: ['./login.component.css'],
        providers: [HttpAuthService]
    })
], LoginComponent);
export { LoginComponent };
//# sourceMappingURL=login.component.js.map