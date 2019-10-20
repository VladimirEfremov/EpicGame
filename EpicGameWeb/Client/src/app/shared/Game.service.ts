import { Injectable, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders, HttpHeaderResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { pipe } from 'rxjs'; 

import { AccountData } from './AccountData';
import { SessionCoresTable } from './SessionCoresTable';

@Injectable()
export class GameService 
{

    constructor(
        private httpClient: HttpClient,
        private router: Router) 
    {
    }

    SwitchToGame() : void 
    {
        this.router.navigate(["/game"]);
    }

    public GetCoreById(coreId:number) : SessionCoresTable
    {
        return null;
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

}