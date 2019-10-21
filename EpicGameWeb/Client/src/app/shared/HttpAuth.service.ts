import { Injectable, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders, HttpHeaderResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { Md5 } from 'ts-md5/dist/md5';
import { AccountData } from './AccountData';
import { Observable } from 'rxjs';

import { HttpLoginPostData } from './HttpLoginPostData';
import { HttpRegistrationPostData } from './HttpRegistrationData';
import { UserTable } from './UserTable';
import { UserFriendsTable } from './UserFriendsTable';
import { UserFollowersTable } from './UserFollowersTable';
import { UserFollowingTable } from './UserFollowingTable';

@Injectable()
export class HttpAuthService
{
    loginUrl: string = 
        //"http://httpbin.org/post"; 
        "http://localhost:6430/Auth/Login";

    registrationUrl: string = 
        "http://localhost:6430/Auth/Registration";
    
    getAccountUrl: string = 
        "http://localhost:6430/Auth/GetAccountData";    

    getAllUsersUrl:string =
        "http://localhost:6430/Auth/GetAllUsers"; 

    getUsersFriendsUrl:string =
        "http://localhost:6430/Auth/GetUsersFriendsTable"; 

    getUsersFollowersUrl:string =
        "http://localhost:6430/Auth/GetUsersFollowersTable"; 

    getUsersFollowingsUrl:string =
        "http://localhost:6430/Auth/GetUsersFollowingsTable"; 

    signOutUrl: string = 
        "http://localhost:6430/Auth/SignOut"; 

    constructor(
        private httpClient: HttpClient,
        private router: Router) 
    { }

    login(loginPostData: HttpLoginPostData) : void 
    {
        loginPostData.PasswordHash = 
            Md5.hashStr(loginPostData.PasswordHash)
            .toString();

        console.log('login');
        let oprions = {
        headers: new HttpHeaders({ 
            'Content-Type': 'application/json'})
        }

        console.log("login [post]: " + 
            loginPostData.Nickname 
            + " " + 
            loginPostData.PasswordHash);

        let dataToPost = JSON.stringify(loginPostData);
        console.log("data to post: " + dataToPost);

        this.httpClient.post<HttpLoginPostData>(
            this.loginUrl, 
            dataToPost, 
            oprions)
            .subscribe(
                (data) => {
                    if (data.toString() === "true")
                    {
                        console.log("login success [response: "+data.toString()+"]");
                        if (data.toString() === "true")
                        {
                            console.log("routing to a game");
                            this.router.navigate(['/game-menu']);
                        }
                        else 
                        {
                            console.log("obj isnt correct [routing to a login]");
                            this.router.navigate(['/login']);
                        }
                    }
                },
                error => console.log("error: "+error)
            );
    }

    registration(registrationPostData: HttpRegistrationPostData)
    {
        registrationPostData.PasswordHash = 
            Md5.hashStr(registrationPostData.PasswordHash)
            .toString();

        console.log('registration');
        let oprions = {
            headers: new HttpHeaders({ 
            'Content-Type': 
                'application/json' })
        }
        
        let dataToPost = JSON.stringify(registrationPostData);
        console.log("data to post: " + dataToPost);

        this.httpClient.post<HttpRegistrationPostData>(
            this.registrationUrl, 
            dataToPost, oprions)
            .subscribe(
                data => {
                if (data.toString().length > 0)
                {
                    console.log("registration success [response: "+data.toString()+"]");
                    if (data.toString() === "true")
                    {
                        this.router.navigate(['/game-menu']);
                    }
                    else 
                    {
                        this.router.navigate(['/registration']);
                    }
                }
                },
                error => console.log(error)
            );
    }

    GetAccountData() : Observable<AccountData>
    {
        console.log('GetAccountData:');
        return this.httpClient.get<AccountData>(this.getAccountUrl);
    }

    public AddUserToFriends(thisId:number, idToAdd:number):void
    {
        //http post
        //this.httpClient.post()
    }

    public RemoveUserFromFriends(thisId:number, idToRemove:number):void
    {
        //http post
    }

    public GetAllUsers():Observable<UserTable[]>
    {
        return this.httpClient
            .get<UserTable[]>(
                this.getAllUsersUrl);
    }

    public GetUsersFriendsTable(userId:number):Observable<UserFriendsTable[]>
    {
        return this.httpClient
            .get<UserFriendsTable[]>(
            this.getUsersFriendsUrl);
    }

    public GetUsersFollowersTable(userId:number):Observable<UserFollowersTable[]>
    {
        return this.httpClient
        .get<UserFollowersTable[]>(
            this.getUsersFollowersUrl);
    }

    public GetUsersFollowingsTable(userId:number):Observable<UserFollowingTable[]>
    {
       return this.httpClient
        .get<UserFollowingTable[]>(
        this.getUsersFollowingsUrl);
    }

    public SignOut() : void
    {
        //http post
        this.httpClient.post(this.signOutUrl, "");
    }


}
