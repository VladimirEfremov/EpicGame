import { Injectable, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders, HttpHeaderResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { pipe } from 'rxjs'; 

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
        return this.httpClient.get<AccountData>(
            "http://localhost:6430/Auth/GetAccountData")[0];
            //.subscribe(
            //    data  => { 
            //        return data;
            //    },
            //    error => { 
            //        console.log("error [GetAccountData]: " + error); 
            //    }
            //);
        //console.log("result [GetAccountData]: " + 
        //        result.Nickname + " " + result.FriendList); 
        //return result;
    }

}