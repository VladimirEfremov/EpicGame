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

    isAuthorized() : AccountData
    {
        console.log('registration: onClick()');
        let httpResponse : string = 
        this.httpClient.get<string>(
            "http://localhost:6430/api/auth/isauth")
            .subscribe(
                data => {
                    console.log("success");
                    return data;
                },
                error => {
                    console.log("error: " + error)
                }
        ).toString();
        console.log("httpResponse: " + httpResponse);
        var convertedResponse : AccountData = 
            JSON.parse(httpResponse);
        return convertedResponse;
    }

}