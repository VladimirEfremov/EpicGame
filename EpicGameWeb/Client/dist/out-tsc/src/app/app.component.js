import { __decorate } from "tslib";
import { Component, ViewEncapsulation } from '@angular/core';
let AppComponent = class AppComponent {
    constructor() {
        this.title = 'Angular works correctly with nodejs';
        this.isRegistered = false;
    }
};
AppComponent = __decorate([
    Component({
        selector: 'app-root',
        templateUrl: './app.component.html',
        styleUrls: ['./app.component.css'],
        encapsulation: ViewEncapsulation.None
    })
], AppComponent);
export { AppComponent };
//# sourceMappingURL=app.component.js.map