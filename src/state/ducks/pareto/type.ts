import type { ApiError } from '@/apis/type';
import type { ParetoClassTag } from '@/utils/constants';

export type Pareto = {
  id: string;
  productId: string;
  categoryId: string;
  product: string;
  category: string;
  tag: ParetoClassTag;
  annualConsumption: number;
  value: number;
};

export type ParetoClassification = {
  tag: ParetoClassTag;
  tagPercentage: number;
  valuePercentage: number;
};

export type ParetoState = {
  data: {
    paretos: Pareto[] | [];
    classifications: ParetoClassification[] | [];
  };
  loading: boolean;
  error: ApiError | null;
};

export const INIT_PARETO_STATE: ParetoState = {
  data: {
    paretos: [],
    classifications: [],
  },
  loading: false,
  error: null,
};
