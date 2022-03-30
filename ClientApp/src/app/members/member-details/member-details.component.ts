import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserDetail } from 'src/app/_models/User';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-member-details',
  templateUrl: './member-details.component.html',
  styleUrls: ['./member-details.component.css'],
})
export class MemberDetailsComponent implements OnInit {
  userDetail!: UserDetail;
  followText: string = 'Takip Et';
  constructor(
    private route: ActivatedRoute,
    private userService: UserService,
    private alertifyService: AlertifyService
  ) {}

  ngOnInit(): void {
    this.route.data.subscribe((routeData) => {
      this.userDetail = routeData['ApiResponse']['data'];
    });
  }

  followUser(userId: number) {
    this.userService.followUserById(userId).subscribe(
      (next) => {
        this.alertifyService.success(
          `${this.userDetail.name} kullanıcısı takip ediyorsunuz.`
        );
        this.followText = 'Takip Ediyorsunuz';
      },
      (error) => {
        this.alertifyService.error(error?.message);
      }
    );
  }

  followed(): boolean {
    return true;
  }
}
