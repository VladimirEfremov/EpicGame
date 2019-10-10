import { Injectable, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders, HttpHeaderResponse } from '@angular/common/http';
import { Router } from '@angular/router';

import { AccountData } from './AccountData';

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

    GetAccountData() : AccountData
    {
        let response : string = 
            this.httpClient.get<string>(
            "http://localhost:6430/api/auth/get_account_data")
            .subscribe(
                data => { console.log("succes [get account data]: " + data); return data; },
                error => { console.log("error [get account data]: " + error) }
            ).toString();
        //let result : AccountData = JSON.parse(response);
        return null;
    }

}