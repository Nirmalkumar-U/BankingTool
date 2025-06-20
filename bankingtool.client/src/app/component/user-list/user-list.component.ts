import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NotificationService } from '../../core/notificationService/notification-service';
import { ResponseDto } from '../../dto/response-dto';
import { UserListResponse } from '../../dto/response/user-list-response';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent {
  userList: UserListResponse[] = [];
  constructor(private readonly activatedRoute: ActivatedRoute, private notificationService: NotificationService) {
  }
  ngOnInit() {
    const initialData: ResponseDto<UserListResponse[]> = this.activatedRoute.snapshot.data['DataResolver'];
    if (initialData.status == false) {
      let messageList: string[] = [];
      if (Array.isArray(initialData?.errors) && initialData?.errors)
        messageList = [...initialData?.errors?.map(x => x.errorMessage), ...initialData?.validationErrors?.map(x => x.errorMessage)]
      messageList.push("User List page load failed.");
      messageList.forEach((msg) => {
        this.notificationService.show(msg, 'error')
      });
    }
    this.userList = initialData.response;
  }
}
