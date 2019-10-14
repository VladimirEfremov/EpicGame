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
  
  //Info account
  accountData : AccountData = {
    Nickname: "",
    FriendList: []
  }; 
  
  //Base info
  money: number = 0;
  defencePower: number = 0;

  //Flags info core
  IsCoreActivated : boolean = true;
  IsCasernActivated : boolean;
  //Info core
  coreHp: number = 1500;
  coreWorkersCount: number = 3;
  coreType: string = "Core";

  //Info casern
  casernHp: number = 750;
  casernType: string = "AttackProduction";
  casernWarriorsCount: number = 0;
  casernAttackAircraftsCount: number = 0;

  //Communication 
  //list [All, Friends, Followers, Followings]
  selectedNicknames : string[] = [];
  all : string[] = ["none1", "none2"];
  friends : string[] = ["Friend1", "Friend2"];
  followers : string[] = ["Follower1", "Follower2"];
  followings : string[] = ["Followings1", "Followings2"];


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


  OnAllBtnClick() : void
  {
      //GetAllUsers
      this.selectedNicknames = this.all;
    }
    
    OnFriendsBtnClick() : void
    {
      //GetAllFriends
      this.selectedNicknames = this.friends;
    }
    
    OnFollowersBtnClick() : void
    {
      //GetAllFollowers
      this.selectedNicknames = this.followers;
    }
    
    OnFollowingsBtnClick() : void
    {
      //GetAllFollowings
      this.selectedNicknames = this.followings;
  }

  OnUserBtnClick(selectedNickname : string) : void
  {

  }

}
