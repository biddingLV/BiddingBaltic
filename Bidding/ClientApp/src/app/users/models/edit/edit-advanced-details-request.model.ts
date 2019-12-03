import { EditBasicDetailsRequestModel } from "./edit-basic-details-request.model";

export interface EditAdvancedDetailsRequestModel
  extends EditBasicDetailsRequestModel {
  userId: number;
  roleId: number;
  subscriptionTill: string;
}
