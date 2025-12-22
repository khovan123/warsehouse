import type { Pareto, ParetoClassification } from '@/state/ducks/pareto/type';

export type ParetoResponse = {
  paretos: Pareto[];
  classifications: ParetoClassification[];
};
