import { __decorate } from "tslib";
import { Injectable } from '@angular/core';
let GameService = class GameService {
    constructor(httpClient, router) {
        this.httpClient = httpClient;
        this.router = router;
    }
    isAuthorized() {
        console.log('registration: onClick()');
        let httpResponse = this.httpClient.get("http://localhost:6430/api/auth/isauth")
            .subscribe(data => {
            console.log("success");
            return data;
        }, error => {
            console.log("error: " + error);
        }).toString();
        console.log("httpResponse: " + httpResponse);
        var convertedResponse = JSON.parse(httpResponse);
        return convertedResponse;
    }
};
GameService = __decorate([
    Injectable()
], GameService);
export { GameService };
//# sourceMappingURL=Game.service.js.map