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
import { Renderable } from '../game/game-structures/Renderable';
import { StatInfo } from '../game/game-structures/StatInfo';

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
    
    casernGetNumberOfWarriorsUrl:string=
        "http://localhost:6430/api/game/CasernGetNumberOfWarriors";
    casernGetNumberOfAttackAircraftUrl:string=
        "http://localhost:6430/api/game/CasernGetNumberOfAttackAircraft";


    getCoreInfoByIdUrl : string =
        "http://localhost:6430/api/game/GetCoreInfoById";

    duelBattleUrl: string =
        "http://localhost:6430/api/game/DuelBattle"; 
    
    coreBattleUrl: string =
        "http://localhost:6430/api/game/CoreBattle"; 

    BaseAttackUpgradeUrl: string =
        "http://localhost:6430/api/game/BaseAttackUpgrade";
    BaseDefenceUpgradeUrl: string =
        "http://localhost:6430/api/game/BaseDefenceUpgrade";
    BaseCapacityUpgradeUrl: string =
        "http://localhost:6430/api/game/BaseCapacityUpgrade";
    CasernCapacityUpgradeUrl: string =
        "http://localhost:6430/api/game/CasernCapacityUpgrade";
    GoldMiningCapacityUpgradeUrl: string =
        "http://localhost:6430/api/game/GoldMiningCapacityUpgrade";
    DefenceTowerAttackUpgradeUrl: string =
        "http://localhost:6430/api/game/DefenceTowerAttackUpgrade";
    DefenceTowerDefenceUpgradeUrl: string =
        "http://localhost:6430/api/game/DefenceTowerDefenceUpgrade";
        
    getCoreRenderableUrl:string =
            "http://localhost:6430/api/game/GetCoreRenderable";
    
    getOtherRenderableUrl:string =
            "http://localhost:6430/api/game/GetOtherRenderable";
    
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

    getAllStats: string =
        "http://localhost:6430/api/game/GetAllStats";

    constructor(
        private httpClient: HttpClient,
        private router: Router) 
    {
    }

    SwitchToGame() : void 
    {
        this.router.navigate(["/game"]);
    }

    SwitchToMenu() : void
    {
        this.router.navigate(["/game-menu"]);
    }

    SwitchToStatisticTable() : void 
    {
        this.router.navigate(["/stats"]);
    }

    public CoreBuildCasern(coreId:number):Observable<Object>
    {
        return this.httpClient.post(
            this.coreBuildCasernUrl,
            coreId)
    }
    
    public CoreBuildDefenceTower(coreId:number):Observable<Object>
    {
        return this.httpClient.post(
            this.coreBuildDefenceTowerUrl,
            coreId);
    }

    public CoreBuildGoldMining(coreId:number):Observable<Object>
    {
        return this.httpClient.post(
            this.coreBuildGoldMiningUrl,
            coreId);
    }

    public BaseProduceWorker(coreId:number):Observable<Object>
    {
       return this.httpClient.post(this.baseProduceWorkerUrl, coreId);
    }

    public CasernProduceWarrior(coreId:number):Observable<Object>
    {
        return this.httpClient.post(
            this.casernProduceWarriorUrl,
            coreId);
    }

    public CasernProduceAttackAircraft(coreId:number):Observable<Object>
    {
        return this.httpClient.post(
            this.casernProduceAttackAircraftUrl,
            coreId);
    }

    public GoldMiningAddWorker(coreId : number):Observable<Object>
    {
        return this.httpClient.post(this.goldMiningAddWorkerUrl, coreId)
    }

    public GetCoreInfoById():Observable<CoreInfo>
    {
        return this.httpClient.get<CoreInfo>(this.getCoreInfoByIdUrl);
    }

    public DuelBattle(coreId : number):number
    {
        console.log("GetAllUserInfo");
        console.log("[Duel battle] CoreId: " + coreId);
        let result; 
        this.httpClient.post<number>(
            this.duelBattleUrl, coreId)
            .subscribe(
                (data:number) => {
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

    public BaseAttackUpgrade(coreId:number):Observable<number> 
    {
        console.log("BaseAttackUpgrade");
        return this.httpClient.post<number>(
            this.BaseAttackUpgradeUrl, coreId);
    }

    public BaseDefenceUpgrade(coreId:number):Observable<number>  
    {
        console.log("BaseDefenceUpgrade");
        return this.httpClient.post<number>(
            this.BaseDefenceUpgradeUrl, coreId);
    }

    public BaseCapacityUpgrade(coreId:number):Observable<number>  
    {
        console.log("BaseAttackUpgrade");
        return this.httpClient.post<number>(
            this.BaseCapacityUpgradeUrl, coreId);
    }
    
    public CasernCapacityUpgrade(coreId:number):Observable<number>  
    {
        console.log("BaseAttackUpgrade");
        return this.httpClient.post<number>(
            this.CasernCapacityUpgradeUrl, coreId);
    }

    public GoldMiningCapacityUpgrade(coreId:number):Observable<number>  
    {
        console.log("BaseAttackUpgrade");
        return this.httpClient.post<number>(
            this.GoldMiningCapacityUpgradeUrl, coreId);
    }

    public DefenceTowerAttackUpgrade(coreId:number):Observable<number>  
    {
        console.log("BaseAttackUpgrade");
        return this.httpClient.post<number>(
            this.DefenceTowerAttackUpgradeUrl, coreId);
    }

    public DefenceTowerDefenceUpgrade(coreId:number):Observable<number>  
    {
        console.log("BaseAttackUpgrade");
        return this.httpClient.post<number>(
            this.DefenceTowerDefenceUpgradeUrl, coreId);
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

    public GetCoreRenderable() : Observable<Renderable>
    {
        return this.httpClient.get<Renderable>(this.getCoreRenderableUrl);
    }

    public GetOtherRenderable() : Observable<Renderable[]>
    {
        return this.httpClient.get<Renderable[]>(this.getOtherRenderableUrl);
    }

    public GetAllStats():Observable<StatInfo[]>
    {
        return this.httpClient.get<StatInfo[]>(this.getAllStats);
    }

}
