import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../_models/apiresponse';
import { User } from '../_models/User';

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

  getUserById(id: number): Observable<User> {
    return this.httpClient.get<User>(`${this.serviceUri}getuser/${id}`);
  }
}
