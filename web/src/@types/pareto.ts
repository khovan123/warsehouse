import type { Pareto, ParetoClassification } from '@/state/ducks/pareto/type';

export type FetchParetoResponse = {
  paretos: Pareto[] | [];
  classifications: ParetoClassification[] | [];
};
