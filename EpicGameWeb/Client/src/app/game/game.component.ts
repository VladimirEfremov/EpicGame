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
    Nickname: ""
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
    Casern:0,
    GoldMining:0,
    DefenceTower:0,
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
    GoldMiningNumberOfWorkers: -1
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
  numberOfSelectedList:number=0;
  numberOfPage:number=1;
  pageStep:number=4;
  
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
  isSelectedUserFriend:boolean = false;

  //Log
  loggedData:Logger = new Logger();

  //Map
  map:Map = new Map();

  constructor(private httpGameService: GameService,
      private httpAuthService: HttpAuthService) { }

  ngOnInit() 
  {
      console.log("OnInit()");
      //this.httpGameService.GetCoreRenderable()
      //  .subscribe( data => {
      //      console.log("[success] GetCoreRenderable");
      //      this.map.thisRenderable = data;
      //  },
      //  error => {
      //      console.log("[error] GetCoreRenderable");
      //  });

      this.GetAccountData();

      this.map.Init();
      this.map.DrawWorld();
      window.requestAnimationFrame(this.map.DrawWorld);

      this.httpGameService
        .GetAllUserLogData(
          this.accountData.UserId)
          .subscribe(
            data => {
                console.log("[success] GetAllUserLogData");
                this.loggedData.Data2 = data;
                if (this.loggedData.Data2 != null)
                {
                  console.log("Get log data !");
                }
                else {
                  this.loggedData.Data2 = [];
                  console.log("Get empty log data !");
                }
                setInterval(
                  ()=>{
                    this.httpGameService.IsLogUpdated(this.accountData.UserId)
                      .subscribe(
                        data => {
                          if (data == 1)
                          {
                            this.httpGameService
                            .UpdateLogData(
                              this.accountData.UserId)
                              .subscribe(
                                data2 => {
                                    //console.log("[success] UpdateLogData");
                                    if (this.loggedData.Data2 == null)
                                    {
                                      this.loggedData.Data2 = [];
                                    }
          
                                    if (data2 != null)
                                    {
                                      let update_length:number = data2.length;
                                      for (var i = 0; i < (update_length); i++)
                                      {
                                        let temp = data2.pop();
                                        this.loggedData.Data2.push(temp);
                                      }
                                      console.log("Log data been updaed !");
                                    }
                                    else{
                                      console.log("data2 is null");
                                    }
                                }, error => { console.log("[error] UpdateLogData" + error); }
                                );
                          }
                        }, error => { console.log("[error] IsLogUpdated" + error); }
                    );
                }, 250);
            },
            error => {
                console.log(
                    "[error] GetAllUserLogData" 
                    + error);
            }
        );

        //setInterval(
        //  ()=>{
        //    this.httpGameService.GetOtherRenderable()
        //      .subscribe(
        //        data => {
        //            console.log("[success] GetOtherRenderable");
        //            if (data.length > 0)
        //            {
        //                this.map.otherRenderable = data;
        //            }
        //        }, error => { console.log("[error] GetOtherRenderable" + error); }
        //    );
        //}, 250);
  }

  OnSignOutBtnClick():void
  {
    this.httpAuthService.SignOut();
    this.httpAuthService.GoToLoginPage();
  }

  GetAccountData():void
  {
    this.httpAuthService.GetAccountData()
        .subscribe(
          res => {
            this.accountData = res;
            console.log("Get account data " +
            "[Nickname: " + this.accountData.Nickname);
            this.httpGameService
              .GetCoreInfoById()
              .subscribe(
                  (data:CoreInfo) => { 
                      console.log("[success] GetCoreById " +
                      "{ "+
                      "CoreId:"+ data.CoreId+"\n"+
                      " Money:"+ data.Money+"\n"+
                      " CoreMapId:"+ data.CoreMapId+"\n"+
                      " BaseLevel:"+ data.BaseLevel+"\n"+
                      " BaseCapacity:"+ data.BaseCapacity+"\n"+
                      " BaseHp:"+ data.BaseHp+"\n"+
                      " BaseAttack:"+ data.BaseAttack+"\n"+
                      " BaseDefence:"+ data.BaseDefence+"\n"+
                      " BaseWorkersCount:"+ data.BaseWorkersCount+"\n"+
                      " BaseType:"+ data.BaseType+"\n"+
                      " BaseIncome:"+ data.BaseIncome+"\n"+
                      " BaseOutcome:"+ data.BaseOutcome+"\n"+
                      " NumberOfWorkersInBase:"+ data.NumberOfWorkersInBase+"\n"+
                      " Casern:"+ data.Casern+"\n"+
                      " GoldMining:"+ data.GoldMining+"\n"+
                      " DefenceTower:"+ data.DefenceTower+"\n"+
                      " CasernLevel:"+ data.CasernLevel+"\n"+
                      " CasernCapacity:"+ data.CasernCapacity+"\n"+
                      " CasernHp:"+ data.CasernHp+"\n"+
                      " CasernAttack:"+ data.CasernAttack+"\n"+
                      " CasernDefence:"+ data.CasernDefence+"\n"+
                      " CasernWarriorsCount:"+ data.CasernWarriorsCount+"\n"+
                      " CasernAttackAircraftsCount:"+ data.CasernAttackAircraftsCount+"\n"+
                      " CasernType:"+ data.CasernWarriorsCount+"\n"+
                      " CasernIncome:"+ data.CasernIncome+"\n"+
                      " CasernOutcome:"+ data.CasernOutcome+"\n"+
                      " DefenceTowerLevel:"+ data.DefenceTowerLevel+"\n"+
                      " DefenceTowerCapacity:"+ data.DefenceTowerCapacity+"\n"+
                      " DefenceTowerHp:"+ data.DefenceTowerHp+"\n"+
                      " DefenceTowerAttack:"+ data.DefenceTowerAttack+"\n"+
                      " DefenceTowerDefence:"+ data.DefenceTowerDefence+"\n"+
                      " DefenceTowerType:"+ data.DefenceTowerType+"\n"+
                      " NumberOfDefenceTower:"+ data.NumberOfDefenceTower+"\n"+
                      " GoldMiningLevel:"+ data.GoldMiningLevel+"\n"+
                      " GoldMiningCapacity:"+ data.GoldMiningCapacity+"\n"+
                      " GoldMiningHp:"+ data.GoldMiningHp+"\n"+
                      " GoldMiningAttack:"+ data.GoldMiningAttack+"\n"+
                      " GoldMiningDefence:"+ data.GoldMiningDefence+"\n"+
                      " GoldMiningType:"+ data.GoldMiningType+"\n"+
                      " GoldMiningIncome:"+ data.GoldMiningIncome+"\n"+
                      " GoldMiningOutcome:"+ data.GoldMiningOutcome+"\n"+
                      " NumberOfWorkersInGoldMining:"+ data.GoldMiningNumberOfWorkers+"\n"+
                      "}"
                      ); 
                      this.coreInfo = data;
                  },
                  error => console.log("[error] GetCoreById: "+error)
              );
              this.OnAllBtnClick();
          },
          err => {
            console.log("[error] GetAccountData: " + err);
            this.accountData.Nickname = "null";
        });
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
    this.httpGameService.CoreBuildCasern(this.accountData.CoreId);
    this.httpGameService.GetCoreInfoById()
      .subscribe(
          (data:CoreInfo) => { 
              console.log("[success] GetCoreById "); 
              this.coreInfo = data;
          },
          error => console.log("[error] GetCoreById: "+error)
      );
  }

  OnBuildGoldMiningBtnClick() : void
  {
    this.loggedData.PushBuildingMsg("Строится шахта для добывания золото");
    this.httpGameService
      .CoreBuildGoldMining(this.accountData.CoreId)
      .subscribe(
          data  => {
              console.log("[CoreBuildGoldMining] success");
              this.httpGameService.GetCoreInfoById()
              .subscribe(
                  (data:CoreInfo) => { 
                      console.log("[success] GetCoreById "); 
                      this.coreInfo = data;
                  },
                  error => console.log("[error] GetCoreById: "+error)
              );
          },
          error => console.log("error" + error)
      );
  }

  OnBuildDefenceTowerBtnClick() : void
  {
    this.loggedData.PushBuildingMsg("Строится защитное сооружение");
    this.httpGameService
      .CoreBuildDefenceTower(this.accountData.CoreId)
      .subscribe(
          data  => {
              console.log("[CoreBuildDefenceTower] success");
              this.httpGameService.GetCoreInfoById()
                .subscribe(
                    (data:CoreInfo) => { 
                        console.log("[success] GetCoreById "); 
                        this.coreInfo = data;
                    },
                    error => console.log("[error] GetCoreById: "+error)
                );
          },
          error => console.log("error" + error)
      );
    
  }

  OnBaseCapacityUpgrade() : void
  {
    this.httpGameService.BaseCapacityUpgrade(this.accountData.CoreId)
        .subscribe(
          data => {
              console.log("[success] BaseAttackUpgrade")
              this.httpGameService
                .GetCoreInfoById()
                .subscribe(
                    (data:CoreInfo) => { 
                        console.log("[success] GetCoreById");
                        this.coreInfo = data;
                    },
                    error => console.log("[error] GetCoreById: "+error)
                );
          },
          error => {console.log("[error] BaseAttackUpgrade: " + error);}
        );
  }

  OnBaseAttackUpgrade() : void
  {
    this.httpGameService.BaseAttackUpgrade(this.accountData.CoreId)
      .subscribe(
          data => {
              console.log("[success] BaseAttackUpgrade")
              this.httpGameService
                .GetCoreInfoById()
                .subscribe(
                    (data:CoreInfo) => { 
                        console.log("[success] GetCoreById");
                        this.coreInfo = data;
                    },
                    error => console.log("[error] GetCoreById: "+error)
                );
          },
          error => {console.log("[error] BaseAttackUpgrade: " + error);}
      );
  }

  OnBaseDefenceUpgrade() : void
  {
    this.httpGameService.BaseDefenceUpgrade(this.accountData.CoreId)
      .subscribe(
        data => {
            console.log("[success] BaseDefenceUpgrade")
            this.httpGameService
                .GetCoreInfoById()
                .subscribe(
                    (data:CoreInfo) => { 
                        console.log("[success] GetCoreById");
                        this.coreInfo = data;
                    },
                    error => console.log("[error] GetCoreById: "+error)
                );
        },
        error => {console.log("[error] BaseDefenceUpgrade: " + error);}
      );
  }

  OnCasernCapacityUpgrade():void
  {
      this.httpGameService.CasernCapacityUpgrade(this.accountData.CoreId)
      .subscribe(
        data => {
            console.log("[success] CasernCapacityUpgrade")
            this.httpGameService
                .GetCoreInfoById()
                .subscribe(
                    (data:CoreInfo) => { 
                        console.log("[success] GetCoreById");
                        this.coreInfo = data;
                    },
                    error => console.log("[error] GetCoreById: "+error)
                );
        },
        error => {console.log("[error] BaseDefenceUpgrade: " + error);}
      );
  }

  OnGoldMiningCapacityUpgrade():void 
  {
      this.httpGameService.GoldMiningCapacityUpgrade(this.accountData.CoreId)
      .subscribe(
        data => {
            console.log("[success] GoldMiningCapacityUpgrade")
            this.httpGameService
                .GetCoreInfoById()
                .subscribe(
                    (data:CoreInfo) => { 
                        console.log("[success] GetCoreById");
                        this.coreInfo = data;
                    },
                    error => console.log("[error] GetCoreById: "+error)
                );
        },
        error => {console.log("[error] BaseDefenceUpgrade: " + error);}
      );
  }

  OnDefenceTowerAttackUpgrade():void 
  {
      console.log("DefenceTowerAttackUpgrade");
      this.httpGameService.DefenceTowerAttackUpgrade(this.accountData.CoreId)
      .subscribe(
        data => {
            console.log("[success] DefenceTowerAttackUpgrade")
            this.httpGameService
                .GetCoreInfoById()
                .subscribe(
                    (data:CoreInfo) => { 
                        console.log("[success] GetCoreById");
                        this.coreInfo = data;
                    },
                    error => console.log("[error] GetCoreById: "+error)
                );
        },
        error => {console.log("[error] BaseDefenceUpgrade: " + error);}
      );
  }

  OnDefenceTowerDefenceUpgrade():void  
  {
      console.log("DefenceTowerDefenceUpgrade");
      this.httpGameService.DefenceTowerDefenceUpgrade(this.accountData.CoreId)
      .subscribe(
        data => {
            console.log("[success] DefenceTowerDefenceUpgrade")
            this.httpGameService
                .GetCoreInfoById()
                .subscribe(
                    (data:CoreInfo) => { 
                        console.log("[success] GetCoreById");
                        this.coreInfo = data;
                    },
                    error => console.log("[error] GetCoreById: "+error)
                );
        },
        error => {console.log("[error] BaseDefenceUpgrade: " + error);}
      );
  }

  OnProduceWorkerBtnClick() : void
  {
    this.httpGameService
      .BaseProduceWorker(this.accountData.CoreId)
      .subscribe(
        data  => {
            console.log("[BaseProduceWorker] success");
            this.httpGameService.GetCoreInfoById()
              .subscribe(
                  (data:CoreInfo) => { 
                      console.log("[success] GetCoreById "); 
                      this.coreInfo = data;
                  },
                  error => console.log("[error] GetCoreById: "+error)
              );
        },
        error => console.log("error" + error)
      );
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
    this.httpGameService
        .CasernProduceWarrior(this.accountData.CoreId)
        .subscribe(
          data  => { 
              console.log("[CasernProduceWarrior] success");
              this.httpGameService.GetCoreInfoById()
              .subscribe(
                  (data:CoreInfo) => { 
                      console.log("[success] GetCoreById "); 
                      this.coreInfo = data;
                  },
                  error => console.log("[error] GetCoreById: "+error)
              );
          },
          error => console.log("error" + error)
        );
    
  }

  OnProduceAttackAircraftBtnClick() : void 
  {
    this.httpGameService
        .CasernProduceAttackAircraft(this.accountData.CoreId)
        .subscribe(
            data  => {
                console.log("[CasernProduceAttackAircraft] success");
                this.httpGameService.GetCoreInfoById()
              .subscribe(
                  (data:CoreInfo) => { 
                      console.log("[success] GetCoreById "); 
                      this.coreInfo = data;
                  },
                  error => console.log("[error] GetCoreById: "+error)
              );
            },
            error => console.log("error" + error)
        );
    
    
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
    this.httpGameService
      .GoldMiningAddWorker(this.accountData.CoreId)
      .subscribe(
          data  => { 
              console.log("[GoldMiningAddWorker] success");
              this.httpGameService.GetCoreInfoById()
              .subscribe(
                  (data:CoreInfo) => { 
                      console.log("[success] GetCoreById "); 
                      this.coreInfo = data;
                  },
                  error => console.log("[error] GetCoreById: "+error)
              );
          },
          error => console.log("error" + error)
      );
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
      if (this.numberOfPage > 1)
      {
        if (this.numberOfSelectedList == 0)
        {
            --this.numberOfPage;
            this.selectedAll = 
            this.allUsers
            .slice((this.pageStep*(this.numberOfPage - 1)),
                this.pageStep*this.numberOfPage);
        }
        else if (this.numberOfSelectedList == 1)
        {
            --this.numberOfPage;
            this.selectedFriends = 
            this.friendsUsers
            .slice(this.pageStep*(this.numberOfPage - 1),
                this.pageStep*this.numberOfPage);
        }
        else if (this.numberOfSelectedList == 2)
        {
            --this.numberOfPage;
            this.selectedFollowers = 
            this.followersUsers
            .slice(this.pageStep*(this.numberOfPage - 1),
                this.pageStep*this.numberOfPage);
        }
        else if (this.numberOfSelectedList == 3)
        {
            --this.numberOfPage;
            this.selectedFollowings = 
            this.followingsUsers
            .slice(this.pageStep*(this.numberOfPage - 1),
                this.pageStep*this.numberOfPage);
        }
      }
  }

  OnGreaterBtnClick():void
  {
    if (this.numberOfSelectedList == 0)
    {
        if (this.pageStep*this.numberOfPage < this.allUsers.length)
        {
            ++this.numberOfPage;
            this.selectedAll = 
            this.allUsers
            .slice(this.pageStep * (this.numberOfPage - 1),
                  this.pageStep * this.numberOfPage);
        }
    }
    else if (this.numberOfSelectedList == 1)
    {
        if (this.pageStep*this.numberOfPage < this.friendsUsers.length)
        {
            ++this.numberOfPage;
            this.selectedFriends = 
            this.friendsUsers
            .slice(this.pageStep * (this.numberOfPage - 1),
            this.pageStep * this.numberOfPage);
        }
    }
    else if (this.numberOfSelectedList == 2)
    {
        if (this.pageStep*this.numberOfPage < this.followersUsers.length)
        {
            ++this.numberOfPage;
            this.selectedFollowers = 
            this.followersUsers
            .slice(this.pageStep * (this.numberOfPage - 1),
            this.pageStep * this.numberOfPage);
        }
    }
    else if (this.numberOfSelectedList == 3)
    {
        if (this.pageStep*this.numberOfPage < this.followingsUsers.length)
        {
            ++this.numberOfPage;
            this.selectedFollowings = 
            this.followingsUsers
            .slice(this.pageStep * (this.numberOfPage - 1),
            this.pageStep * this.numberOfPage);
        }
    }
  }

  OnAllBtnClick() : void
  {
      //GetAllUsers
      this.httpAuthService.GetAllUsersInfo().subscribe(
        (data)=>{
          console.log("[success] GetAllUsers");
          this.allUsers = data;
          this.selectedAll = 
            this.allUsers.slice(0, this.pageStep);
            this.numberOfPage = 1;
            this.numberOfSelectedList = 0;
        },
        (error)=>{
          console.log("[error] GetAllUsers: " + error);
        }
      );
      
  }
    
  OnFriendsBtnClick() : void
  {
      //GetFriends
      this.httpAuthService.GetUsersFriendsInfo().subscribe(
        (data)=>{
          console.log("[success] GetUsersFriendsInfo");
          this.friendsUsers = data;
          this.selectedFriends = 
            this.friendsUsers.slice(0, this.pageStep);
          this.numberOfPage = 1;
          this.numberOfSelectedList = 1;
        },
        (error)=>{
          console.log("[error] GetUsersFriendsInfo: " + error);
        }
      );
  }
  
  OnFollowersBtnClick() : void
  {
      //GetAllFollowers
      this.httpAuthService.GetUsersFollowersInfo().subscribe(
        (data)=>{
          console.log("[success] GetUsersFollowersInfo");
          this.followersUsers = data;
          this.selectedFollowers = 
            this.followersUsers.slice(0, this.pageStep);
          this.numberOfPage = 1;
          this.numberOfSelectedList = 2;
        },
        (error)=>{
          console.log("[error] GetUsersFollowersInfo: " + error);
        }
      );
  }
  
  OnFollowingsBtnClick() : void
  {
    //GetAllFollowings
    this.httpAuthService.GetUsersFollowingsInfo().subscribe(
      (data)=>{
        console.log("[success] GetUsersFollowingsInfo");
        this.followingsUsers = data;
        this.selectedFollowings = 
          this.followingsUsers.slice(0, this.pageStep);
        this.numberOfPage = 1;
        this.numberOfSelectedList = 3;
      },
      (error)=>{
        console.log("[error] GetUsersFollowingsInfo: " + error);
      }
    );
  }

  OnUserBtnClick(userInfo : UserInfo) : void
  {
    console.log("On user btn click: [selected list: " 
    + this.numberOfSelectedList 
    + "]");
    this.isUserActionsWindowVisible = true;
    console.log("OnUserBtnClick: [nick: " + userInfo.Nickname + 
      ", userid: " + userInfo.UserId + 
      ", coreid: " + userInfo.CoreId);
    this.selectedUser = userInfo;
    this.isSelectedUserFriend = this.IsUserFriend();
  }

  IsUserFriend() : boolean
  {
      let result:boolean = false;
      this.httpAuthService.GetUsersFriendsInfo()
        .subscribe(
          (data) => {
            console.log("[success] GetUsersFriendsInfo: ");
            this.friendsUsers = data;
            for (var f = 0; f < this.friendsUsers.length; f++)
            {
              console.log(this.friendsUsers[f].Nickname + " === " + this.selectedUser.Nickname);
              if (this.friendsUsers[f].Nickname ===
                this.selectedUser.Nickname)
              {
                  console.log("[IsUserFriend] true");
                  result = true;
              }
            }

            if (!result)
            {
                this.httpAuthService.GetUsersFollowingsInfo().subscribe(
                  (data) => {
                    console.log("[success] GetUsersFriendsInfo: ");
                    this.followingsUsers = data;
                    for (var f = 0; f < this.followingsUsers.length; f++)
                    {
                        console.log(this.followingsUsers[f].Nickname + " === " + 
                          this.selectedUser.Nickname);
                        if (this.followingsUsers[f].Nickname ===
                          this.selectedUser.Nickname)
                        {
                          console.log("[IsUserFriend] true");
                          result = true;
                        }
                    }
                  },
                  (error) => {
                      console.log("[error] GetUsersFriendsInfo: "+error);
                  }
                )
            }
          },
          (error) => {
              console.log("[error] GetUsersFriendsInfo: " + error);
          }
        );
      
      if (!result) console.log("[IsUserFriend] false");
      return result;
  }

  AddUserToFriends() : void
  {
    let users : TwoUsers = {
      FirstId: this.accountData.UserId,
      SecondId: this.selectedUser.UserId
    };
    this.httpAuthService.AddUserToFriends(users).subscribe(
      (data)=>{
          this.httpAuthService.GetUsersFriendsInfo().subscribe(
            (data) => {
              console.log("[success] GetUsersFriendsInfo: ");
              this.friendsUsers = data;
              this.httpAuthService.GetUsersFollowingsInfo().subscribe(
                (data) => {
                  console.log("[success] GetUsersFriendsInfo: ");
                  this.followingsUsers = data;
                  this.httpAuthService.GetUsersFollowersInfo().subscribe(
                      (data) => {
                        this.followersUsers = data;
                        if (this.numberOfSelectedList == 1)
                        {
                            console.log("this.numberOfSelectedList == 1");
                            this.selectedFriends = 
                              this.friendsUsers.slice(0, this.pageStep);
                        }
                        else if (this.numberOfSelectedList == 2)
                        {
                            console.log("this.numberOfSelectedList == 2");
                            this.selectedFollowers = 
                              this.followersUsers.slice(0, this.pageStep);
                        }
                        else if (this.numberOfSelectedList == 3)
                        {
                            console.log("this.numberOfSelectedList == 3");
                            this.selectedFollowings = 
                              this.followingsUsers.slice(0, this.pageStep);
                        }
                      },
                      (error) => {
                        console.log("[error] GetUsersFriendsInfo: " + error);
                      });
                },
                (error) => {
                  console.log("[error] GetUsersFriendsInfo: " + error);
                });
              },
              (error) => {
                console.log("[error] GetUsersFriendsInfo: " + error);
          });
      },
      (error) => {
          console.log("[error] AddUserToFriends: " + error);
      });
   
      this.isUserActionsWindowVisible = false;
  }
  
  RemoveUserFromFriends() : void
  {
    let users : TwoUsers = {
      FirstId: this.accountData.UserId,
      SecondId: this.selectedUser.UserId
    };
    this.httpAuthService.RemoveUserFromFriends(users)
    .subscribe(
      data => { 
        console.log("[success] RemoveUserFromFriends");
        this.httpAuthService.GetUsersFriendsInfo()
        .subscribe(
          (data) => {
            console.log("[success] GetUsersFriendsInfo: ");
            this.friendsUsers = data;
            this.httpAuthService.GetUsersFollowingsInfo()
            .subscribe(
              (data) => {
                console.log("[success] GetUsersFriendsInfo: ");
                this.followingsUsers = data;
                this.httpAuthService.GetUsersFollowersInfo()
                .subscribe(
                  (data) => {
                    if (this.numberOfSelectedList == 1)
                    {
                        console.log("this.numberOfSelectedList == 1");
                        this.selectedFriends = 
                          this.friendsUsers.slice(0, this.pageStep);
                    }
                    else if (this.numberOfSelectedList == 2)
                    {
                        console.log("this.numberOfSelectedList == 2");
                        this.selectedFollowers = 
                          this.followersUsers.slice(0, this.pageStep);
                    }
                    else if (this.numberOfSelectedList == 3)
                    {
                        console.log("this.numberOfSelectedList == 3");
                        this.selectedFollowings = 
                          this.followingsUsers.slice(0, this.pageStep);
                    }
                },
                (error) => {
                  console.log("[error] GetUsersFriendsInfo: " + error);
                });
              },
              (error) => {
                console.log("[error] GetUsersFriendsInfo: " + error);
              });
            },
            (error) => {
              console.log("[error] GetUsersFriendsInfo: " + error);
        });
      },
      error => { 
        console.log("[error] RemoveUserFromFriends"+error);
      }
    );
    this.isUserActionsWindowVisible = false;
  }

  OnExitBtnClick():void
  {
    this.isUserActionsWindowVisible=false;
  }

  OnDuelBattleBtnClick() :void
  {
    let result = this
    .httpGameService
    .DuelBattle(this.selectedUser.CoreId);
  }

  OnCoreAttackBtnClick() :void
  {
    let result = this
    .httpGameService
    .CoreBattle(this.selectedUser.CoreId);
    console.log(result.Message);
  }

}
