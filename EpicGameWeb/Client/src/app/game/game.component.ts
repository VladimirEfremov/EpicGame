import { Component, OnInit, Input } from '@angular/core';
import { AccountData } from '../shared/AccountData';
import { GameService } from '../shared/Game.service';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css'],
  providers: [GameService]
})
export class GameComponent implements OnInit 
{
  IsCoreActivated : boolean = true;
  IsCasernActivated : boolean;

  accountData : AccountData = {
    Nickname: "",
    FriendList: []
  }; 

  hp: number = 1500;
  workers: number = 3;
  type: string = "Core";

  warriorsCount: number = 0;
  attackAircraftsCount: number = 0;

  constructor(private gameService: GameService) { }

  ngOnInit() 
  {
      console.log("NG on init()")
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

  OnBtnCoreClick() : void
  {
      this.IsCoreActivated = true;
      this.IsCasernActivated = false;
  }

  OnBtnCasernClick() : void
  {
      this.IsCasernActivated = true;
      this.IsCoreActivated = false;
  }

}
