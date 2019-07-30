import { UserListItemModel } from './user-list-item.model';
import { BaseListResponseModel } from 'ClientApp/src/app/shared/models/base-list-response.model';

export interface UserListResponseModel extends BaseListResponseModel {
  users: UserListItemModel[];
}
