import * as tslib_1 from "tslib";
import { Component } from '@angular/core';
let RegistrationComponent = class RegistrationComponent {
    constructor(httpClient) {
        this.httpClient = httpClient;
        this.PostData = {
            firstName: '',
            secondName: '',
            passwordHash: '',
            nickname: '',
            email: ''
        };
    }
    ngOnInit() {
    }
    onClick() {
        alert('on click function works!');
        this.httpClient.post("http://localhost:6430/Account/Registration", this.PostData);
    }
};
RegistrationComponent = tslib_1.__decorate([
    Component({
        selector: 'app-registration',
        templateUrl: './registration.component.html',
        styleUrls: ['./registration.component.css']
    })
], RegistrationComponent);
export { RegistrationComponent };
//# sourceMappingURL=registration.component.js.map