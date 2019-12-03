import { RoleItemModel } from "../shared/role-item.model";
import { UserBasicDetailsResponseModel } from "./user-basic-details-response.model";

export interface UserAdvancedDetailsResponseModel
  extends UserBasicDetailsResponseModel {
  roleId: number;
  roles: RoleItemModel[];
  subscriptionTill;
}
