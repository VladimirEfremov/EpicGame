import { __decorate } from "tslib";
import { Component } from '@angular/core';
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
NotFoundPageComponent = __decorate([
    Component({
        selector: 'app-not-found-page',
        templateUrl: './not-found-page.component.html',
        styleUrls: ['./not-found-page.component.css']
    })
], NotFoundPageComponent);
export { NotFoundPageComponent };
//# sourceMappingURL=not-found-page.component.js.map