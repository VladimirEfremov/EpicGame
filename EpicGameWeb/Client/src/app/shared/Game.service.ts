import { Injectable, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders, HttpHeaderResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { pipe } from 'rxjs'; 
import { Observable } from 'rxjs';

import { AccountData } from './AccountData';
import { SessionCoresTable } from './SessionCoresTable';
import { CoreInfo } from '../game/game-structures/CoreInfo';
import { BattleResponse } from '../game/game-structures/BattleResponse';

@Injectable()
export class GameService 
{
    baseProduceWorkerUrl : string =
        "http://localhost:6430/api/game/BaseProduceWorker";
    
    getCoreInfoByIdUrl : string =
        "http://localhost:6430/api/game/GetCoreInfoById";

    duelBattleUrl: string =
        "http://localhost:6430/api/game/DuelBattle"; 
    
    coreBattleUrl: string =
        "http://localhost:6430/api/game/CoreBattle"; 

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
        //post
    }

    public CoreBuildGoldMining(coreId:number):void
    {
        //post
    }

    public CoreBuildDefenceTower(coreId:number):void
    {
        //post
    }

    public BaseProduceWorker(coreId:number):void
    {
       //post
       this.httpClient
        .get(this.baseProduceWorkerUrl)
        .subscribe(
            data => console.log("success"),
            error => console.log("error"+error)
        );
    }

    public CasernProduceWarrior(coreId:number):void
    {
        //post
    }

    public CasernProduceAttackAircraft(coreId:number):void
    {
        //post
    }

    public GoldMiningAddWorker(coreId : number):void
    {
        //post
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

}
