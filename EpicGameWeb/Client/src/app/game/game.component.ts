import { Component, OnInit, Input } from '@angular/core';
import { AccountData } from '../shared/AccountData';

import { HttpAuthService } from '../shared/HttpAuth.service';
import { GameService } from '../shared/Game.service';

import { CoreInfo } from './game-structures/CoreInfo';
import { CasernInfo } from './game-structures/CasernInfo';
import { DefenceTowerInfo } from './game-structures/DefenceTowerInfo';
import { GoldMiningInfo } from './game-structures/GoldMiningInfo';

import {Logger} from './Logger';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css'],
  providers: [GameService, HttpAuthService]
})
export class GameComponent implements OnInit 
{
  
  //Info account
  accountData : AccountData = {
    Nickname: "",
    FriendsList: [],
    FollowersList: [],
    FollowingsList: []
  }; 
  
  //Flags info core
  IsCoreInfoActivated : boolean = true;
  IsCasernInfoActivated : boolean;
  IsDefenceTowerInfoActivated: boolean;
  IsGoldMiningInfoActivated: boolean;
  
  IsCasernActive: boolean = true;
  IsDefenceTowerActive: boolean = true;
  IsGoldMiningActive: boolean = true;
  
  //Base info
  money: number = 0;
  defencePower: number = 0;

  //Info core
  coreInfo: CoreInfo =
  {
    CoreHp: 10000,
    CoreAttack: 0,
    CoreDefence: 20,
    CoreWorkersCount: 3,
    CoreCapacity: 10,
    CoreIncome: 0,
    CoreOutcome: 10,
    CoreType: "Core"
  };

  casernInfo: CasernInfo =
  {
      CasernHp: 7500,
      CasernAttack: 0,
      CasernDefence: 15,
      CasernIncome: 0,
      CasernOutcome: 10,
      CasernType: "ProductionAttack",
      CasernWarriorsCount: 0,
      CasernAttackAircraftsCount: 0,
      CasernCapacity: 0
  };

  //Info DefenceTower
  defenceTowerInfo: DefenceTowerInfo =
  {
      DefenceTowerHp: 12500,
      DefenceTowerAttack: 200,
      DefenceTowerDefence: 15,
      DefenceTowerIncome: 0,
      DefenceTowerOutcome: 10,
      DefenceTowerType: "Defence",
      DefenceTowerWorkersCount: 0
  };

  //Info GoldMining
  goldMiningInfo: GoldMiningInfo =
  {
      GoldMiningHp: 2500,
      GoldMiningAttack: 0,
      GoldMiningDefence: 0,
      GoldMiningIncome: 0,
      GoldMiningOutcome: 0,
      GoldMiningType: "Production",
      GoldMiningWorkersCount: 0,
      GoldMiningCapacity: 0
  };

  //Communication 
  //0-All, 1-Friends, 2-Followers, 3-Followings
  numberOfSelectedList:number=0;
  numberOfPage:number=0;
  pageStep:number=4;
  //list [All, Friends, Followers, Followings]
  selectedNicknames : string[] = [];
  all : string[] = ["none1", "none2"];
  friends : string[] = ["Friend1", "Friend2"];
  followers : string[] = [
      "Fol0","Fol1", "Fol2","Fol3","Fol4",
      "Fol5","Fol6","Fol7","Fol8","Fol9",
      "Fol10","Fol11","Fol12"
    ];
  followings : string[] = ["Followings1", "Followings2"];

  //Log
  loggedData:Logger = new Logger();

  constructor(private gameService: GameService,
      private httpAuthService: HttpAuthService) { }

  ngOnInit() 
  {
      console.log("NG on init()")
      let response : AccountData = 
        this
        .httpAuthService
        .GetAccountData();
      if (response != null)
      {
          console.log("Get account data " +
          "[Nickname: " + this.accountData.Nickname + 
          " Friends list: " + this.accountData.FriendsList + "]");
          this.accountData = response;
      }
      else 
      {
          console.log("Account data == null");
          this.accountData.Nickname = "null";
          this.accountData.FriendsList = [ "null" ];
      }
  }

  OnCoreBtnClick() : void
  {
      this.IsCoreInfoActivated = true;
      this.IsCasernInfoActivated = false;
      this.IsDefenceTowerInfoActivated = false;
      this.IsGoldMiningInfoActivated = false;
  }

  OnBuildCasernBtnClick() : void
  {
    this.loggedData.PushBuildingMsg("Начали строить казарму");
  }

  OnBuildGoldMiningBtnClick() : void
  {
    
  }

  OnBuildDefenceTowerBtnClick() : void
  {
     
  }

  OnProduceWorkerBtnClick() : void
  {
     
  }

  OnCasernBtnClick() : void
  {
      this.IsCoreInfoActivated = false;
      this.IsCasernInfoActivated = true;
      this.IsDefenceTowerInfoActivated = false;
      this.IsGoldMiningInfoActivated = false;
  }

  OnProduceWarriorBtnClick() : void 
  {
    
  }

  OnProduceAttackAircraftBtnClick() : void 
  {
    
  }

  OnGoldMiningBtnClick() : void
  {
      this.IsCoreInfoActivated = false;
      this.IsCasernInfoActivated = false;
      this.IsDefenceTowerInfoActivated = false;
      this.IsGoldMiningInfoActivated = true;
  }

  OnAddWorkerToMineBtnClick() : void
  {

  }

  OnDefenceTowerBtnClick() : void
  {
      this.IsCasernInfoActivated = false;
      this.IsCoreInfoActivated = false;
      this.IsDefenceTowerInfoActivated = true;
      this.IsGoldMiningInfoActivated = false;
  }

  OnLessBtnClick():void
  {
      if (this.numberOfPage > 0)
      {
        if (this.numberOfSelectedList == 0)
        {
            --this.numberOfPage;
            this.selectedNicknames = 
            this.all
            .slice(this.pageStep*this.numberOfPage,
                this.pageStep*this.numberOfPage +this.pageStep);
        }
        else if (this.numberOfSelectedList == 1)
        {
            --this.numberOfPage;
            this.selectedNicknames = 
            this.friends
            .slice(this.pageStep*this.numberOfPage,
                this.pageStep*this.numberOfPage + this.pageStep);
        }
        else if (this.numberOfSelectedList == 2)
        {
            --this.numberOfPage;
            this.selectedNicknames = 
            this.followers
            .slice(this.pageStep*this.numberOfPage,
                this.pageStep*this.numberOfPage + this.pageStep);
        }
        else if (this.numberOfSelectedList == 3)
        {
            --this.numberOfPage;
            this.selectedNicknames = 
            this.followings
            .slice(this.pageStep*this.numberOfPage,
                this.pageStep*this.numberOfPage + this.pageStep);
        }
      }
  }

  OnGreaterBtnClick():void
  {
    if (this.numberOfSelectedList == 0)
    {
        if (this.pageStep*(this.numberOfPage+1) < this.all.length)
        {
            ++this.numberOfPage;
            this.selectedNicknames = 
            this.all
            .slice(this.pageStep*this.numberOfPage,
                this.pageStep*this.numberOfPage + this.pageStep-1);
        }
    }
    else if (this.numberOfSelectedList == 1)
    {
        if (this.pageStep*(this.numberOfPage+1) < this.friends.length)
        {
            ++this.numberOfPage;
            this.selectedNicknames = 
            this.friends
            .slice(this.pageStep*this.numberOfPage,
                this.pageStep*this.numberOfPage + this.pageStep-1);
        }
    }
    else if (this.numberOfSelectedList == 2)
    {
        if (this.pageStep*(this.numberOfPage+1) < this.followers.length)
        {
            ++this.numberOfPage;
            this.selectedNicknames = 
            this.followers
            .slice(this.pageStep*this.numberOfPage,
                this.pageStep*this.numberOfPage + this.pageStep);
        }
    }
    else if (this.numberOfSelectedList == 3)
    {
        if (this.pageStep*(this.numberOfPage+1) < this.followings.length)
        {
            ++this.numberOfPage;
            this.selectedNicknames = 
            this.followings
            .slice(this.pageStep*this.numberOfPage,
                this.pageStep*this.numberOfPage + this.pageStep-1);
        }
    }
  }

  OnAllBtnClick() : void
  {
      //GetAllUsers
      //this.all = this.gameService.GetAllUsers();
      this.selectedNicknames = this.all.slice(0, this.pageStep);
      this.numberOfPage = 0;
      this.numberOfSelectedList = 0;
  }
    
  OnFriendsBtnClick() : void
  {
      //GetAllFriends
      this.selectedNicknames = this.friends.slice(0, this.pageStep);
      this.numberOfPage = 0;
      this.numberOfSelectedList = 1;
  }
  
  OnFollowersBtnClick() : void
  {
      //GetAllFollowers
      this.selectedNicknames = this.followers.slice(0, this.pageStep);
      this.numberOfPage = 0;
      this.numberOfSelectedList = 2;
  }
  
  OnFollowingsBtnClick() : void
  {
    //GetAllFollowings
    this.selectedNicknames = this.followings.slice(0, this.pageStep);
    this.numberOfPage = 0;
    this.numberOfSelectedList = 3;
  }

  OnUserBtnClick(selectedNickname : string) : void
  {
      console.log("OnUserBtnClick: " + selectedNickname);
  }

}
