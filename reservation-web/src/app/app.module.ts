import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginModule } from './login/login.module';
import { RestaurantModule } from './restaurant/restaurant.module';
import { AuthGuard } from './shared/guards/auth.guard';
import { AuthenticationService } from './shared/services/authentication.service';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { LoggedInGuard } from './shared/guards/logged-in.guard';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    RestaurantModule,
    LoginModule,
    HttpClientModule,
    FontAwesomeModule
  ],
  providers: [AuthGuard,
    LoggedInGuard,
    AuthenticationService],
  bootstrap: [AppComponent]
})
export class AppModule { }
