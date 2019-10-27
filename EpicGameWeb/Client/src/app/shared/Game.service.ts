import { Injectable, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders, HttpHeaderResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { pipe } from 'rxjs'; 

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
        .post(this.baseProduceWorkerUrl, coreId)
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

    public GetCoreById(coreId:number):CoreInfo
    {
        let toSend : CoreInfo = {
            Money: 0,
            CoreId: coreId,
            CoreMapId: 0,
            BaseLevel: 0,
            BaseCapacity: 0,
            BaseHp: 0,
            BaseAttack: 0,
            BaseDefence: 0,
            BaseWorkersCount: 0,
            BaseType: 0,
            BaseIncome: 0,
            BaseOutcome: 0,
            NumberOfWorkersInBase: 0,
            Casern:false,
            GoldMining:false,
            DefenceTower:false,
            CasernLevel: 0,
            CasernCapacity: 0,
            CasernHp: 0,
            CasernAttack: 0,
            CasernDefence: 0,
            CasernWarriorsCount: 0,
            CasernAttackAircraftsCount: 0,
            CasernType: 0,
            CasernIncome: 0,
            CasernOutcome: 0,
            NumberOfWarriors: 0,
            NumberOfAttackAircraft: 0,
            DefenceTowerLevel: 0,
            DefenceTowerCapacity: 0,
            DefenceTowerHp: 0,
            DefenceTowerAttack: 0,
            DefenceTowerDefence: 0,
            DefenceTowerType: 0,
            NumberOfDefenceTower: 0,
            GoldMiningLevel: 0,
            GoldMiningCapacity: 0,
            GoldMiningHp: 0,
            GoldMiningAttack: 0,
            GoldMiningDefence: 0,
            GoldMiningType: 0,
            GoldMiningIncome: 0,
            GoldMiningOutcome: 0,
            NumberOfWorkersInGoldMining: 0

        };

        let coreInfo;
        this.httpClient
            .post(
                this.getCoreInfoByIdUrl, 
                JSON.stringify(toSend))
            .subscribe(
                data => { 
                    console.log("success[GetCoreById]"); 
                    coreInfo = data 
                },
                error => console.log("error[GetCoreById]: "+error)
            );
        return coreInfo;
    }

}