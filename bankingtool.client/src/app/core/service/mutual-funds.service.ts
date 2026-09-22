import { Injectable } from '@angular/core';
import { HttpService } from '../http/http.service';
import { ResponseDto } from '../../dto/internal-dto/response-dto';
import { MutualFundsDto, UserInvestmentDto, InsertUserInvestmentRequest } from '../../dto/mutual-funds-dto';
import { UserInvestmentProfileDto } from '../../dto/user-investment-profile-dto';

@Injectable({
  providedIn: 'root'
})
export class MutualFundsService {
  private readonly endpoint = 'MutualFunds';

  constructor(private httpService: HttpService) { }

  getAllMutualFunds() {
    return this.httpService.get(
      `${this.endpoint}/GetAllMutualFunds`
    );
  }

  getAllMutualFundsInDB() {
    return this.httpService.get(
      `${this.endpoint}/GetAllMutualFundsInDB`
    );
  }

  getAllUserInvestment() {
    return this.httpService.get(
      `${this.endpoint}/GetAllUserInvestment`
    );
  }

  saveSchemes(codes: string[]) {
    return this.httpService.post(
      `${this.endpoint}/SaveSchemes`,
      codes
    );
  }

  insertUserInvestment(request: InsertUserInvestmentRequest) {
    return this.httpService.post(
      `${this.endpoint}/InsertUserInvestment`,
      request
    );
  }

  getUserInvestmentProfile() {
    return this.httpService.get(
      `${this.endpoint}/GetUserInvestmentProfile`
    );
  }
}
