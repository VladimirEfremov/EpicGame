import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { Routes, RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { RegistrationComponent } from './registration/registration.component';
import { MainComponent } from './main/main.component';
import { NotFoundPageComponent } from './not-found-page/not-found-page.component';
import { GameComponent } from './game/game.component';
import { GameGuard } from './shared/GameGuard';
import { GameMenuComponent } from "./game/game-menu/game-menu.component";

const appRoutes: Routes = [
   { path: '', component: MainComponent },
   { path: 'login', component: LoginComponent },
   { path: 'registration', component: RegistrationComponent },
   { path: 'game', component: GameComponent, canActivate: [GameGuard]},
   { path: 'game-menu', component: GameMenuComponent, canActivate: [GameGuard]},
   { path: '**', component: NotFoundPageComponent }
];

@NgModule({
   declarations: [
      AppComponent,
      MainComponent,
      LoginComponent,RegistrationComponent,
      NotFoundPageComponent,
      GameComponent, GameMenuComponent
   ],
   imports: [
      BrowserModule,
      RouterModule.forRoot(appRoutes),
      HttpClientModule,
      FormsModule
   ],
   providers: [
      GameGuard
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
