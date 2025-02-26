import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { isNullOrEmpty } from '../../core/commonFunction/common-function';
import { UserService } from '../../core/service/user.service';
import { DropDownDto } from '../../dto/drop-down-dto';
import { ResponseDto } from '../../dto/response-dto';
import { SaveUserDto } from '../../dto/save-user-dto';
import { UserInitialLoadDto } from '../../dto/user-initial-load-dto';

@Component({
  selector: 'app-add-edit-user',
  templateUrl: './add-edit-user.component.html',
  styleUrls: ['./add-edit-user.component.css']
})
export class AddEditUserComponent implements OnInit {
  userForm: FormGroup;
  stateDropDownList: DropDownDto[] = [];
  cityDropDownList: DropDownDto[] = [];
  roleDropDownList: DropDownDto[] = [];
  messageList: string[] = [];

  constructor(private readonly activatedRoute: ActivatedRoute, private fb: FormBuilder, private userService: UserService) {
    this.userForm = this.fb.group({
      userId: [],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      state: ['', [Validators.required]],
      city: ['', [Validators.required]],
      role: ['', [Validators.required]]
    });
  }
  ngOnInit() {
    const initialData: ResponseDto<UserInitialLoadDto> = this.activatedRoute.snapshot.data['DataResolver'];
    this.stateDropDownList = initialData.result.stateDropDown;
    this.roleDropDownList = initialData.result.roleDropDown;

    this.userForm.setValue({
      userId: initialData.result.userDetail.userId == 0 ? '' : initialData.result.userDetail.userId,
      email: initialData.result.userDetail.emailId,
      password: initialData.result.userDetail.password,
      firstName: initialData.result.userDetail.firstName,
      lastName: initialData.result.userDetail.lastName,
      state: initialData.result.userDetail.state == 0 ? '' : initialData.result.userDetail.state,
      city: initialData.result.userDetail.city == 0 ? '' : initialData.result.userDetail.city,
      role: initialData.result.userDetail.roleId == 0 ? '' : initialData.result.userDetail.roleId
    });

    this.userForm.get('state')?.valueChanges.subscribe(stateValue => {
      if (stateValue && stateValue != null) {
        this.userService.getCityDropDownListByStateId(stateValue).subscribe((cityList: DropDownDto[]) => {
          this.cityDropDownList = cityList;
        });
      } else {
        this.cityDropDownList = [];
      }
    });
  }

  saveUser() {
    if (!this.userForm.invalid) {
      let isValid = true;
      let user: SaveUserDto = this.userForm.value;
      if (isNullOrEmpty(user.email)) isValid = false;
      if (isNullOrEmpty(user.password)) isValid = false;
      if (isNullOrEmpty(user.firstName)) isValid = false;
      if (isNullOrEmpty(user.lastName)) isValid = false;
      if (isNullOrEmpty(user.state)) isValid = false;
      if (isNullOrEmpty(user.city)) isValid = false;
      if (isNullOrEmpty(user.role)) isValid = false;
      if (isValid) {
        this.messageList = [];
        this.userService.saveUser(user).subscribe((response: ResponseDto<number | null>) => {
          this.messageList = response.message
        });
      }
    }
  }
}
