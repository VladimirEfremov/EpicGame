import { Component, OnInit, Input } from '@angular/core';
import { Observable } from 'rxjs';

import { HttpAuthService } from '../shared/HttpAuth.service';
import { GameService } from '../shared/Game.service';

import { CoreInfo } from './game-structures/CoreInfo';
import { CasernInfo } from './game-structures/CasernInfo';
import { DefenceTowerInfo } from './game-structures/DefenceTowerInfo';
import { GoldMiningInfo } from './game-structures/GoldMiningInfo';

import { AccountData } from '../shared/AccountData';
import { UserTable } from '../shared/UserTable';
import { Logger } from './game-structures/Logger';
import { Map } from './game-structures/Map';
import { UserInfo } from '../game/game-structures/UserInfo';
import { TwoUsers } from '../game/game-structures/TwoUsers';

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
    UserId: -1,
    CoreId: -1,
    Nickname: "",
    FriendsList: [],
    FollowersList: [],
    FollowingsList: []
  }; 

  //Info core
  coreInfo: CoreInfo =
  {
    Money: -1,
    CoreId: -1,
    CoreMapId: -1,
    BaseLevel: -1,
    BaseCapacity: -1,
    BaseHp: -1,
    BaseAttack: -1,
    BaseDefence: -1,
    BaseWorkersCount: -1,
    BaseType: -1,
    BaseIncome: -1,
    BaseOutcome: -1,
    NumberOfWorkersInBase: -1,
    Casern:false,
    GoldMining:false,
    DefenceTower:false,
    CasernLevel: -1,
    CasernCapacity: -1,
    CasernHp: -1,
    CasernAttack: -1,
    CasernDefence: -1,
    CasernWarriorsCount: -1,
    CasernAttackAircraftsCount: -1,
    CasernType: -1,
    CasernIncome: -1,
    CasernOutcome: -1,
    NumberOfWarriors: -1,
    NumberOfAttackAircraft: -1,
    DefenceTowerLevel: -1,
    DefenceTowerCapacity: -1,
    DefenceTowerHp: -1,
    DefenceTowerAttack: -1,
    DefenceTowerDefence: -1,
    DefenceTowerType: -1,
    NumberOfDefenceTower: -1,
    GoldMiningLevel: -1,
    GoldMiningCapacity: -1,
    GoldMiningHp: -1,
    GoldMiningAttack: -1,
    GoldMiningDefence: -1,
    GoldMiningType: -1,
    GoldMiningIncome: -1,
    GoldMiningOutcome: -1,
    NumberOfWorkersInGoldMining: -1
  };

  //Flags info core
  IsCoreInfoActivated : boolean = true;
  IsCasernInfoActivated : boolean;
  IsDefenceTowerInfoActivated: boolean;
  IsGoldMiningInfoActivated: boolean;
  
  //Base info
  defencePower: number = 0;

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

  //Game


  //Communication 
  //0-All, 1-Friends, 2-Followers, 3-Followings
  numberOfSelectedList:number=-1;
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
  
  //Users stuff
  allUsers:UserInfo[] = [];
  friendsUsers:UserInfo[] = [];
  followersUsers:UserInfo[] = [];
  followingsUsers:UserInfo[] = [];

  selectedAll:UserInfo[] = [];
  selectedFriends:UserInfo[] = [];
  selectedFollowers:UserInfo[] = [];
  selectedFollowings:UserInfo[] = [];

  isUserActionsWindowVisible = false;
  selectedUser : UserInfo;

  //Log
  loggedData:Logger = new Logger();

  //Map
  map:Map = new Map();

  constructor(private httpGameService: GameService,
      private httpAuthService: HttpAuthService) { }

  ngOnInit() 
  {
      console.log("NG on init()");
      this.map.Init();
      this.map.DrawWorld();
      window.requestAnimationFrame(this.map.DrawWorld);
      
      this.GetAccountData();
      this.GetCoreInfo();

      this.coreInfo.Casern=true;
      this.coreInfo.GoldMining=true;
      this.coreInfo.DefenceTower=true;
  }

  GetAccountData():void
  {
    this.httpAuthService.GetAccountData()
        .subscribe(
          res => {
            this.accountData = res;
            console.log("Get account data " +
            "[Nickname: " + this.accountData.Nickname + 
            " Friends list: " + this.accountData.FriendsList + "]");
          },
          err => {
            console.log("GetAccountData error: " + err);
            this.accountData.Nickname = "null";
            this.accountData.FriendsList = [ "null" ];
          });
  }

  GetAllUsers()
  {
    this.httpAuthService.GetAllUsers()
    .subscribe(
      res=>{
        console.log("get all users");
        this.allUsers = res;
      },
      err=>{
        console.log("[error] get all users");
      }
    );
  }

  GetUsersFriends()
  {
    this.httpAuthService.GetUsersFriendsInfo()
    .subscribe(
      res=>{
        console.log("get user friends");
        this.friendsUsers = res;
      },
      err=>{
        console.log("[error] get user friends");
      }
    );
  }

  GetUsersFollowers()
  {
    this.httpAuthService.GetUsersFollowersInfo()
    .subscribe(
      res=>{
        console.log("get user followers");
        this.followersUsers = res;
      },
      err=>{
        console.log("[error] get user followers");
      }
    );
  }

  GetUsersFollowings()
  {
    this.httpAuthService.GetUsersFollowingsInfo()
    .subscribe(
      res=>{
        console.log("get user followings");
        this.followingsUsers = res;
      },
      err=>{
        console.log("[error] get user followings");
      }
    );
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
    this.loggedData.PushBuildingMsg("Строится казарма");
  }

  OnBuildGoldMiningBtnClick() : void
  {
    this.loggedData.PushBuildingMsg("Строится шахта для добывания золото");
  }

  OnBuildDefenceTowerBtnClick() : void
  {
    this.loggedData.PushBuildingMsg("Строится защитное сооружение");
  }

  OnProduceWorkerBtnClick() : void
  {
    this.loggedData.PushBuildingMsg("Создается unit рабочий");
    this.httpGameService.BaseProduceWorker(this.accountData.CoreId);
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
    this.loggedData.PushBuildingMsg("Создается unit воин");
  }

  OnProduceAttackAircraftBtnClick() : void 
  {
    this.loggedData.PushBuildingMsg("Создается unit AttackAircraft");
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
    this.loggedData.PushBuildingMsg("Добавлен рабочий к руднику");
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
            this.selectedAll = 
            this.allUsers
            .slice(this.pageStep*this.numberOfPage,
                this.pageStep*this.numberOfPage +this.pageStep);
        }
        else if (this.numberOfSelectedList == 1)
        {
            --this.numberOfPage;
            this.selectedFriends = 
            this.friendsUsers
            .slice(this.pageStep*this.numberOfPage,
                this.pageStep*this.numberOfPage + this.pageStep);
        }
        else if (this.numberOfSelectedList == 2)
        {
            --this.numberOfPage;
            this.selectedFollowers = 
            this.followersUsers
            .slice(this.pageStep*this.numberOfPage,
                this.pageStep*this.numberOfPage + this.pageStep);
        }
        else if (this.numberOfSelectedList == 3)
        {
            --this.numberOfPage;
            this.selectedFollowings = 
            this.followingsUsers
            .slice(this.pageStep*this.numberOfPage,
                this.pageStep*this.numberOfPage + this.pageStep);
        }
      }
  }

  OnGreaterBtnClick():void
  {
    if (this.numberOfSelectedList == 0)
    {
        if (this.pageStep*(this.numberOfPage+1) < this.allUsers.length)
        {
            ++this.numberOfPage;
            this.selectedAll = 
            this.allUsers
            .slice(this.pageStep*this.numberOfPage,
                this.pageStep*this.numberOfPage + this.pageStep-1);
        }
    }
    else if (this.numberOfSelectedList == 1)
    {
        if (this.pageStep*(this.numberOfPage+1) < this.friendsUsers.length)
        {
            ++this.numberOfPage;
            this.selectedFriends = 
            this.friendsUsers
            .slice(this.pageStep*this.numberOfPage,
                this.pageStep*this.numberOfPage + this.pageStep-1);
        }
    }
    else if (this.numberOfSelectedList == 2)
    {
        if (this.pageStep*(this.numberOfPage+1) < this.followersUsers.length)
        {
            ++this.numberOfPage;
            this.selectedFollowers = 
            this.followersUsers
            .slice(this.pageStep*this.numberOfPage,
                this.pageStep*this.numberOfPage + this.pageStep);
        }
    }
    else if (this.numberOfSelectedList == 3)
    {
        if (this.pageStep*(this.numberOfPage+1) < this.followingsUsers.length)
        {
            ++this.numberOfPage;
            this.selectedFollowings = 
            this.followingsUsers
            .slice(this.pageStep*this.numberOfPage,
                this.pageStep*this.numberOfPage + this.pageStep-1);
        }
    }
  }

  OnAllBtnClick() : void
  {
      //GetAllUsers
      this.GetAllUsers();
      this.selectedAll = 
        this.allUsers.slice(0, this.pageStep);
      
      this.numberOfPage = 0;
      this.numberOfSelectedList = 0;
  }
    
  OnFriendsBtnClick() : void
  {
      //GetAllFriends
      this.GetUsersFriends();

      this.selectedNicknames = 
        this.friends.slice(0, this.pageStep);

      this.numberOfPage = 0;
      this.numberOfSelectedList = 1;
  }
  
  OnFollowersBtnClick() : void
  {
      //GetAllFollowers
      this.GetUsersFollowers();

      this.selectedNicknames = 
        this.followers.slice(0, this.pageStep);

      this.numberOfPage = 0;
      this.numberOfSelectedList = 2;
  }
  
  OnFollowingsBtnClick() : void
  {
    //GetAllFollowings
    this.GetUsersFollowings();

    this.selectedNicknames = 
      this.followings.slice(0, this.pageStep);

    this.numberOfPage = 0;
    this.numberOfSelectedList = 3;
  }

  OnUserBtnClick(userInfo : UserInfo) : void
  {
      this.isUserActionsWindowVisible = true;
      console.log("OnUserBtnClick: " + userInfo.Nickname);
      this.selectedUser = userInfo;
  }

  IsUserFriend(nick:string) : boolean
  {
      for (var i = 0; i < this.friendsUsers.length; i++)
      {
          if (this.friendsUsers[i].Nickname == nick)
          {
            return true;
          }
      }
      return false;
  }

  AddUserToFriends() : void
  {
    let users : TwoUsers = {
      FirstId: this.accountData.UserId,
      SecondId: this.selectedUser.UserId
    };
    this.httpAuthService.AddUserToFriends(users);
    this.isUserActionsWindowVisible = false;
  }
  
  RemoveUserFromFriends() : void
  {
    let users : TwoUsers = {
      FirstId: this.accountData.UserId,
      SecondId: this.selectedUser.UserId
    };
    this.httpAuthService.RemoveUserFromFriends(users);
    this.isUserActionsWindowVisible = false;
    if (this.numberOfSelectedList == 1)
    {
      console.log("this.numberOfSelectedList == 1");
      this.OnFriendsBtnClick();
    }
    else if (this.numberOfSelectedList == 2)
    {
      console.log("this.numberOfSelectedList == 2");
      this.OnFollowersBtnClick();
    }
    else if (this.numberOfSelectedList == 3)
    {
      console.log("this.numberOfSelectedList == 3");
      this.OnFollowingsBtnClick();
    }
  }

  OnExitBtnClick():void
  {
    this.isUserActionsWindowVisible=false;
  }

  GetCoreInfo():void
  {
    let result =
      this
      .httpGameService
      .GetCoreById(this.accountData.CoreId);
    if (result != null)
    {
      this.coreInfo = result;
    }
    else
    {
      console.warn("can't get coreInfo from server: "+result);
    }  
  }

}
