import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginModule } from './login/login.module';
import { RestaurantModule } from './restaurant/restaurant.module';
import { AuthGuard } from './shared/guards/auth.guard';
import { AuthenticationService } from './shared/services/authentication.service';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { LoggedInGuard } from './shared/guards/logged-in.guard';
import { PrimeNgComponentsModule } from './shared/modules/prime-ng-components/prime-ng-components.module';
import { JwtTokenInterceptor } from './shared/interceptors/jwt-token.interceptor';


@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    RestaurantModule,
    LoginModule,
    HttpClientModule,
    FontAwesomeModule,
    PrimeNgComponentsModule,
    BrowserAnimationsModule
  ],
  providers: [AuthGuard,
    { provide: HTTP_INTERCEPTORS, useClass: JwtTokenInterceptor, multi: true },
    LoggedInGuard,
    AuthenticationService],
  bootstrap: [AppComponent]
})
export class AppModule { }
