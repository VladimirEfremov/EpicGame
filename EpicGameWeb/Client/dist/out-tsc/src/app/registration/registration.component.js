import { __decorate } from "tslib";
import { Component } from '@angular/core';
import { HttpAuthService } from '../shared/HttpAuth.service';
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
RegistrationComponent = __decorate([
    Component({
        selector: 'app-registration',
        templateUrl: './registration.component.html',
        styleUrls: ['./registration.component.css'],
        providers: [HttpAuthService]
    })
], RegistrationComponent);
export { RegistrationComponent };
//# sourceMappingURL=registration.component.js.map