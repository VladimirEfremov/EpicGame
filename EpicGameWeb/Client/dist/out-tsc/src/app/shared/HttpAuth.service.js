import { __decorate } from "tslib";
import { Injectable } from '@angular/core';
import { HttpHeaders } from '@angular/common/http';
import { Md5 } from 'ts-md5/dist/md5';
let HttpAuthService = class HttpAuthService {
    constructor(httpClient, router) {
        this.httpClient = httpClient;
        this.router = router;
        this.loginUrl = 
        //"http://httpbin.org/post"; 
        "http://localhost:6430/api/auth/login";
        this.registrationUrl = "http://localhost:6430/api/auth/registration";
    }
    login(loginPostData) {
        loginPostData.PasswordHash =
            Md5.hashStr(loginPostData.PasswordHash)
                .toString();
        console.log('login');
        let oprions = {
            headers: new HttpHeaders({
                'Content-Type': 'application/json'
            })
        };
        this.httpClient.post(this.loginUrl, JSON.stringify(loginPostData), oprions)
            .subscribe(data => {
            if (data.toString().length > 0) {
                console.log("success");
                let obj = JSON.parse(data.toString());
                if (obj.IsCorrect) {
                    this.router.navigate(['/game']);
                }
                else {
                    this.router.navigate(['/login']);
                }
            }
        }, error => console.log(error));
    }
    registration(registrationPostData) {
        registrationPostData.PasswordHash =
            Md5.hashStr(registrationPostData.PasswordHash)
                .toString();
        console.log('registration');
        let oprions = {
            headers: new HttpHeaders({
                'Content-Type': 'application/json'
            })
        };
        this.httpClient.post(this.registrationUrl, JSON.stringify(registrationPostData), oprions)
            .subscribe(data => {
            if (data.toString().length > 0) {
                console.log("success");
                let obj = JSON.parse(data.toString());
                if (obj.IsCorrect) {
                    this.router.navigate(['/game']);
                }
                else {
                    this.router.navigate(['/login']);
                }
            }
        }, error => console.log(error));
    }
    isAuthorized() {
        console.log('registration: onClick()');
        let oprions = {
            headers: new HttpHeaders({
                'Content-Type': 'application/json'
            })
        };
        this.httpClient.get("http://localhost:6430/api/auth/isauth")
            .subscribe(data => {
            console.log('success');
            return true;
        }, error => {
            console.log("error: " + error);
            return false;
        });
        return false;
    }
};
HttpAuthService = __decorate([
    Injectable()
], HttpAuthService);
export { HttpAuthService };
//# sourceMappingURL=HttpAuth.service.js.map