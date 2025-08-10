import { ClaimDto } from "../internal-dto/claim-dto";
import { GetActionsByUserIdDto } from "../internal-dto/get-actions-by-user-id-dto";

export interface TokenResponse {
  accessToken: string;
  claims: ClaimDto[];
  expireIn: string;
  actionPaths: GetActionsByUserIdDto[];
}
