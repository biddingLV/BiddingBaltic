import { CategoryModel } from '../shared/categories/category.model';

export interface TopCategoryFilterModel extends CategoryModel {
  categoryTotalCount: number;
}
