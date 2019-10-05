import { __decorate } from "tslib";
import { Component } from '@angular/core';
import { GameService } from '../shared/Game.service';
let GameComponent = class GameComponent {
    constructor(gameService) {
        this.gameService = gameService;
        this.accountData = {
            IsAuthorized: false,
            Nickname: "",
            FriendList: []
        };
    }
    ngOnInit() {
        this.accountData =
            this.gameService.isAuthorized();
    }
};
GameComponent = __decorate([
    Component({
        selector: 'app-game',
        templateUrl: './game.component.html',
        styleUrls: ['./game.component.css'],
        providers: [GameService]
    })
], GameComponent);
export { GameComponent };
//# sourceMappingURL=game.component.js.map