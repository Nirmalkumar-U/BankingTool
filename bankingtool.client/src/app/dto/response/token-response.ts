import { ClaimDto } from "../claim-dto";
import { GetActionsByUserIdDto } from "../get-actions-by-user-id-dto";

export interface TokenResponse {
  accessToken: string;
  claims: ClaimDto[];
  expireIn: string;
  actionPaths: GetActionsByUserIdDto[];
}
