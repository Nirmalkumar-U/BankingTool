import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { isNullOrEmpty } from '../../core/commonFunction/common-function';
import { UserService } from '../../core/service/user.service';
import { DropDownDto } from '../../dto/drop-down-dto';
import { GetCityListRequestObject } from '../../dto/request/user/get-city-list-request';
import { SaveUserRequestObject } from '../../dto/request/user/save-user-request';
import { ResponseDto } from '../../dto/response-dto';
import { UserInitialLoadResponse } from '../../dto/response/user-initial-load-response';
import { SaveUserDto } from '../../dto/save-user-dto';

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
    const initialData: ResponseDto<UserInitialLoadResponse> = this.activatedRoute.snapshot.data['DataResolver'];
    this.stateDropDownList = initialData.dropDownList.find(x => x.name == "State")!.dropDown;
    this.roleDropDownList = initialData.dropDownList.find(x => x.name == "Role")!.dropDown;

    this.userForm.setValue({
      userId: initialData.response.userDetail.userId == 0 ? '' : initialData.response.userDetail.userId,
      email: initialData.response.userDetail.emailId,
      password: initialData.response.userDetail.password,
      firstName: initialData.response.userDetail.firstName,
      lastName: initialData.response.userDetail.lastName,
      state: initialData.response.userDetail.state == 0 ? '' : initialData.response.userDetail.state,
      city: initialData.response.userDetail.city == 0 ? '' : initialData.response.userDetail.city,
      role: initialData.response.userDetail.roleId == 0 ? '' : initialData.response.userDetail.roleId
    });

    this.userForm.get('state')?.valueChanges.subscribe(stateValue => {
      if (stateValue && stateValue != null) {
        let model: GetCityListRequestObject = {
          request: {
            state: {
              id: stateValue
            }
          }
        }
        this.userService.getCityDropDownListByStateId(model).subscribe((cityList: ResponseDto<boolean>) => {
          this.cityDropDownList = cityList.dropDownList.find(x => x.name == "City")!.dropDown;;
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
        let model: SaveUserRequestObject = {
          request: {
            user: {
              email: user.email,
              password: user.password,
              firstName: user.firstName,
              lastName: user.lastName
            },
            state: {
              id: user.state
            },
            city: {
              id: user.city
            },
            role: {
              id: user.role
            }
          }
        };

        this.userService.saveUser(model).subscribe((response: ResponseDto<number | null>) => {
          this.messageList = [...response.errors.map(error => error.errorMessage), ...response.validationErrors.map(error => error.errorMessage)];
          if (response.status == true) {
            this.messageList.push("User created successfully.")
          }
        });
      }
    }
  }
}
