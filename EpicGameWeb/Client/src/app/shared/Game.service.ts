import { Injectable, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders, HttpHeaderResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { pipe } from 'rxjs'; 
import { Observable } from 'rxjs';

import { AccountData } from './AccountData';
import { SessionCoresTable } from './SessionCoresTable';
import { CoreInfo } from '../game/game-structures/CoreInfo';
import { BattleResponse } from '../game/game-structures/BattleResponse';
import { Log } from '../game/game-structures/Log';

@Injectable()
export class GameService 
{
    coreBuildCasernUrl : string =
        "http://localhost:6430/api/game/CoreBuildCasern";
    coreBuildGoldMiningUrl : string =
        "http://localhost:6430/api/game/CoreBuildGoldMining";
    coreBuildDefenceTowerUrl : string =
        "http://localhost:6430/api/game/CoreBuildDefenceTower";

    baseProduceWorkerUrl : string =
        "http://localhost:6430/api/game/BaseProduceWorker";

    casernProduceWarriorUrl : string =
        "http://localhost:6430/api/game/CasernProduceWarrior";
    casernProduceAttackAircraftUrl : string =
        "http://localhost:6430/api/game/CasernProduceAttackAircraft";
    goldMiningAddWorkerUrl : string =
        "http://localhost:6430/api/game/GoldMiningAddWorker";
    


    getCoreInfoByIdUrl : string =
        "http://localhost:6430/api/game/GetCoreInfoById";

    duelBattleUrl: string =
        "http://localhost:6430/api/game/DuelBattle"; 
    
    coreBattleUrl: string =
        "http://localhost:6430/api/game/CoreBattle"; 


    getAllUserLogDataUrl: string =
        "http://localhost:6430/Auth/GetAllUserLogData"; 

    getAllUserBattleEventsUrl: string =
        "http://localhost:6430/Auth/GetAllUserBattleEvents"; 

    getAllUserProduceEventsUrl: string =
        "http://localhost:6430/Auth/GetAllUserProduceEvents"; 

    getAllUserCommunicationEventsUrl: string =
        "http://localhost:6430/Auth/GetAllUserCommunicationEvents"; 

    updateLogDataUrl: string =
        "http://localhost:6430/Auth/UpdateLogData"; 

    isLogUpdatedUrl: string = 
        "http://localhost:6430/Auth/IsLogUpdated"

    constructor(
        private httpClient: HttpClient,
        private router: Router) 
    {
    }

    SwitchToGame() : void 
    {
        this.router.navigate(["/game"]);
    }

    public CasernGetNumberOfWarriors(coreId:number):number
    {
        return 0;
    }

    public CasernGetNumberOfAttackAircraft(coreId:number):number
    {
        return 0;
    }

    public CoreBuildCasern(coreId:number):void
    {
        this.httpClient
        .post(this.coreBuildCasernUrl,
            coreId)
        .subscribe(
            data  => console.log("[CoreBuildCasern] success"),
            error => console.log("error" + error)
        );
    }
    
    public CoreBuildDefenceTower(coreId:number):void
    {
        this.httpClient
        .post(this.coreBuildDefenceTowerUrl,
            coreId)
        .subscribe(
            data  => console.log("[CoreBuildDefenceTower] success"),
            error => console.log("error" + error)
        );
    }

    public CoreBuildGoldMining(coreId:number):void
    {
        this.httpClient
        .post(this.coreBuildGoldMiningUrl,
            coreId)
        .subscribe(
            data  => console.log("[CoreBuildGoldMining] success"),
            error => console.log("error" + error)
        );
    }

    public BaseProduceWorker(coreId:number):void
    {
       this.httpClient
        .post(this.baseProduceWorkerUrl,
            coreId)
        .subscribe(
            data  => console.log("[BaseProduceWorker] success"),
            error => console.log("error" + error)
        );
    }

    public CasernProduceWarrior(coreId:number):void
    {
        this.httpClient
        .post(this.casernProduceWarriorUrl,
            coreId)
        .subscribe(
            data  => console.log("[CasernProduceWarrior] success"),
            error => console.log("error" + error)
        );
    }

    public CasernProduceAttackAircraft(coreId:number):void
    {
        this.httpClient
        .post(this.casernProduceAttackAircraftUrl,
            coreId)
        .subscribe(
            data  => console.log("[CasernProduceAttackAircraft] success"),
            error => console.log("error" + error)
        );
    }

    public GoldMiningAddWorker(coreId : number):void
    {
        this.httpClient
        .post(this.goldMiningAddWorkerUrl, coreId)
        .subscribe(
            data  => console.log("[GoldMiningAddWorker] success"),
            error => console.log("error" + error)
        );
    }

    public GetCoreById():Observable<CoreInfo>
    {
        return this.httpClient
            .get<CoreInfo>(
                this.getCoreInfoByIdUrl);;
    }

    public DuelBattle(coreId : number):BattleResponse
    {
        console.log("GetAllUserInfo");
        let result; 
        let dataToPost: BattleResponse ={
            WhoWonTheBattle:coreId,
            Message:""
        };
        this.httpClient.post<string>(
            this.duelBattleUrl, "2")
            .subscribe(
                (data:string) => {
                    console.log("[success] DuelBattle")
                    result = data;
                    console.log(result);
                    //console.log("Battle result: " + result.WhoWonTheBattle + " Message: " + result.Message);
                },
                error => {
                    console.log("[error] DuelBattle: " + error);
                }
            );
        return result;
    }

    public CoreBattle(coreId : number):BattleResponse
    {
        console.log("GetAllUserInfo");
        let result; 
        this.httpClient.post<string>(
            this.coreBattleUrl,
            coreId.toString())
            .subscribe(
                data => {
                    console.log("[success] CoreBattle")
                    let cdata : BattleResponse = JSON.parse(data);
                    result = cdata;
                },
                error => {
                    console.log("[error] CoreBattle: " + error);
                }
            );
        return result;
    }

    public GetAllUserLogData(userId:number) : Observable<Log[]>
    {
        return this.httpClient.get<Log[]>(this.getAllUserLogDataUrl);
    }

    public GetAllUserBattleEvents(userId:number) : Observable<Log[]>
    {
        return this.httpClient.get<Log[]>(this.getAllUserBattleEventsUrl);;
    }

    public GetAllUserProduceEvents(userId:number) : Observable<Log[]>
    {
        return this.httpClient.get<Log[]>(this.getAllUserProduceEventsUrl);
    }

    public GetAllUserCommunicationEvents(userId:number) : Observable<Log[]>
    {
        return this.httpClient.get<Log[]>(this.getAllUserCommunicationEventsUrl);;
    }

    public UpdateLogData(userId:number) : Observable<Log[]>
    {
        return this.httpClient.get<Log[]>(this.updateLogDataUrl);
    }

    public IsLogUpdated(userId:number) : Observable<number>
    {
        return this.httpClient.get<number>(this.isLogUpdatedUrl);
    }

}
