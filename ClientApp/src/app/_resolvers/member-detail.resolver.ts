import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  Resolve,
  Router,
  RouterStateSnapshot,
} from '@angular/router';
import { catchError, Observable, of } from 'rxjs';
import { ApiResponse } from '../_models/ApiResponse';
import { UserDetail } from '../_models/User';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';
import { UserService } from '../_services/user.service';

@Injectable()
export class MemberDetailsResolver implements Resolve<ApiResponse<UserDetail>> {
  constructor(
    private userService: UserService,
    private alertifyService: AlertifyService,
    private routeService: Router
  ) {}
  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | ApiResponse<UserDetail>
    | Observable<ApiResponse<UserDetail>>
    | Promise<ApiResponse<UserDetail>> {
    return this.userService.getUserById(route.params['id']).pipe(
      catchError((error) => {
        this.alertifyService.error('server error!');
        this.routeService.navigate(['/home']);
        return of();
      })
    );
  }
}
