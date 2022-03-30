import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  serviceUri: string = 'https://localhost:7095/api';
  jwtHelper = new JwtHelperService();
  decodedToken!: any;
  constructor(private httpClient: HttpClient) {}

  login(userForLoginDto: any): Observable<void> {
    return this.httpClient
      .post(`${this.serviceUri}/auth/login`, userForLoginDto)
      .pipe(
        map((response: any) => {
          const apiResult = response;
          if (apiResult) {
            localStorage.setItem('token', apiResult.token);
            this.decodedToken = this.jwtHelper.decodeToken(apiResult.token);
          }
        })
      );
  }

  register(userForRegisterDto: any) {
    return this.httpClient.post(
      `${this.serviceUri}/auth/register`,
      userForRegisterDto
    );
  }

  logginIn() {
    const token = localStorage.getItem('token');
    if (token === null) {
      return false;
    }
    return !this.jwtHelper.isTokenExpired(token);
  }
}
