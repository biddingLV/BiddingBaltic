import { TopCategoryFilterModel } from './top-category-filter.model';
import { SubCategoryFilterModel } from './sub-category-filter.model';

export class AuctionFilterModel {
  topCategories: TopCategoryFilterModel[];
  subCategories: SubCategoryFilterModel[];
}
