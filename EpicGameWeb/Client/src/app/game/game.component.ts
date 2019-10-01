import { Component, OnInit } from '@angular/core';
import { AccountData } from '../shared/AccountData';
import { GameService } from '../shared/Game.service';
import { nextTick } from 'q';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css'],
  providers: [GameService]
})
export class GameComponent implements OnInit {

  accountData : AccountData = {
    IsAuthorized: false,
    Nickname: "",
    FriendList: []
  }; 

  constructor(private gameService: GameService) { }

  ngOnInit() 
  {
      this.accountData = 
        this.gameService.isAuthorized();
  }

}
