import { CategoryModel } from "../shared/categories/category.model";
import { TypeModel } from "../shared/types/type.model";

export class CategoriesWithTypesModel {
  topCategories: CategoryModel[];
  subCategories: TypeModel[];
}