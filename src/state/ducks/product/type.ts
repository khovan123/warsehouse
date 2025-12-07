import type { ApiError } from '@/apis/type';

export type Product = {
  id: string;
  accountId: string;
  label: string;
  sku: string;
  baseUom: string;
  description: string;
  isOverBook: boolean;
  availability: string;
  categoryId: string;
};

export type ProductState = {
  data: {
    products: Product[] | null;
  };
  loading: boolean;
  error: ApiError | null;
};

export const INIT_PRODUCT_STATE: ProductState = {
  data: {
    products: null,
  },
  loading: false,
  error: null,
};
