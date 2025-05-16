export interface GetActionsByUserIdDto {
  actionId: number;
  actionName: string;
  actionPath: string | null;
  actionType: string;
  menuLevel: number | null;
  parrentMenuId: number | null;
  sequence: number | null;
}
