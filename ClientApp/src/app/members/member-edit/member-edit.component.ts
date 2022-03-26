import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserDetail } from 'src/app/_models/User';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css'],
})
export class MemberEditComponent implements OnInit {
  userDetail!: UserDetail;
  constructor(
    private route: ActivatedRoute,
    private alertifyService: AlertifyService,
    private userService: UserService
  ) {}

  ngOnInit(): void {
    this.route.data.subscribe((routeData) => {
      this.userDetail = routeData['ApiResponse']['data'];
    });
  }

  updateUser() {
    this.userService.updateUser(this.userDetail).subscribe(
      (apiResponse) => {
        this.alertifyService.success(apiResponse.message);
      },
      (error) => {
        this.alertifyService.error(error);
      }
    );
  }
}
