import * as tslib_1 from "tslib";
import { Component } from '@angular/core';
let LoginComponent = class LoginComponent {
    constructor(httpClient) {
        this.httpClient = httpClient;
        this.PostData = {
            login: '', password: ''
        };
        this.url = "http://httpbin.org/post"; //"http://localhost:2003/Login"
    }
    ngOnInit() { }
    onClick() {
        console.log('login: onClick()');
        this.httpClient.post("http://localhost:6430/api/values/post", //"http://localhost:6430/Account/Login", 
        this.PostData).toPromise().then(function (apiResponse) {
            console.log("success!");
        }, function (apiError) {
            console.log("api operation failed!");
        });
    }
};
LoginComponent = tslib_1.__decorate([
    Component({
        selector: 'app-login',
        templateUrl: './login.component.html',
        styleUrls: ['./login.component.css']
    })
], LoginComponent);
export { LoginComponent };
//# sourceMappingURL=login.component.js.map