import { Injectable, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders, HttpHeaderResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { pipe } from 'rxjs'; 
import { Observable } from 'rxjs';

import { AccountData } from './AccountData';
import { SessionCoresTable } from './SessionCoresTable';
import { CoreInfo } from '../game/game-structures/CoreInfo';

@Injectable()
export class GameService 
{
    baseProduceWorkerUrl : string =
        "http://localhost:6430/api/game/BaseProduceWorker";
    
    getCoreInfoByIdUrl : string =
        "http://localhost:6430/api/game/GetCoreInfoById";

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

}
