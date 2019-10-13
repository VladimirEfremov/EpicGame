import { Component, OnInit, Input } from '@angular/core';
import { AccountData } from '../shared/AccountData';
import { GameService } from '../shared/Game.service';

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
      console.log("NG on init()")
      this.SetAccountData();
  }

  SetAccountData() : void 
  {
      let response : AccountData = this.gameService.GetAccountData();
      if (response != null)
      {
          console.log("Get account data " +
          "[Nickname: " + this.accountData.Nickname + 
          " Friend list: " + this.accountData.FriendList + "]");
          this.accountData = response;
      }
      else 
      {
          console.log("Account data == null");
          this.accountData.Nickname = "null";
          this.accountData.FriendList = [ "null" ];
      }
  }

}
