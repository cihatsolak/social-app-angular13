import { HttpClient, HttpHeaders } from '@angular/common/http';
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

  getUsers(): Observable<ApiResponse<User[]>> {
    return this.httpClient.get<ApiResponse<User[]>>(
      `${this.serviceUri}getusers`
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
