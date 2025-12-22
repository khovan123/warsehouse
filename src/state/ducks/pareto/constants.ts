export const ParetoClassTag = {
  A: 'A',
  B: 'B',
  C: 'C',
} as const;

export type ParetoClassTag = (typeof ParetoClassTag)[keyof typeof ParetoClassTag];

export const PARETO_CLASS_LABELS: Record<ParetoClassTag, string> = {
  [ParetoClassTag.A]: 'Class A',
  [ParetoClassTag.B]: 'Class B',
  [ParetoClassTag.C]: 'Class C',
};
