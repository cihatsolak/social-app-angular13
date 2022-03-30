import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../_models/ApiResponse';
import { User, UserDetail } from '../_models/User';

// const httpOptions = {
//   headers: new HttpHeaders({
//     Authorization: `Bearer ${localStorage.getItem('token')}`,
//   }),
// };

@Injectable({
  providedIn: 'root',
})
export class UserService {
  serviceUri: string = 'https://localhost:7095/api/users/';
  constructor(private httpClient: HttpClient) {}

  getUsers(
    followParams?: any,
    userParams?: any
  ): Observable<ApiResponse<User[]>> {
    let httpParams = new HttpParams();

    if (followParams == 'followers') {
      httpParams = httpParams.set('followers', 'true');
    }

    if (followParams == 'followings') {
      httpParams = httpParams.set('followings', 'true');
    }

    if (userParams != null) {
      if (userParams.orderby != null) {
        httpParams = httpParams.set('orderby', userParams.orderby);
      }

      if (userParams.gender != null) {
        httpParams = httpParams.set('gender', userParams.gender);
      }

      if (userParams.minAge != null) {
        httpParams = httpParams.set('minAge', userParams.minAge);
      }

      if (userParams.maxAge != null) {
        httpParams = httpParams.set('maxAge', userParams.maxAge);
      }

      if (userParams.country != null) {
        httpParams = httpParams.set('country', userParams.country);
      }

      if (userParams.city != null) {
        httpParams = httpParams.set('city', userParams.city);
      }
    }
    return this.httpClient.get<ApiResponse<User[]>>(
      `${this.serviceUri}getusers`,
      { params: httpParams }
    );
  }

  getUserById(id: number): Observable<ApiResponse<UserDetail>> {
    return this.httpClient.get<ApiResponse<UserDetail>>(
      `${this.serviceUri}getuser/${id}`
    );
  }

  updateUser(userDetail: UserDetail): Observable<ApiResponse<null>> {
    return this.httpClient.put<ApiResponse<null>>(
      `${this.serviceUri}updateuser/${userDetail.id}`,
      userDetail
    );
  }

  followUserById(userId: number): Observable<ApiResponse<null>> {
    return this.httpClient.post<ApiResponse<null>>(
      `${this.serviceUri}followuser/${userId}`,
      {}
    );
  }
}
