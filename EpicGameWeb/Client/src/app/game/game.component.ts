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
    Nickname: "",
    FriendList: []
  }; 

  constructor(private gameService: GameService) { }

  ngOnInit() 
  {
      let accountData : AccountData = this.gameService.GetAccountData();
      if (accountData != null)
      {
          console.log("Nickname: " + accountData.Nickname);
          this.accountData = accountData;
      }
      else 
      {
        console.log("Account data == null")
      }
  }

}
