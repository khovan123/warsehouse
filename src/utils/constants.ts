export type ParetoClassTag = 'A' | 'B' | 'C';

export const PARETO_CLASS_LABELS: Record<ParetoClassTag, string> = {
  A: 'Class A',
  B: 'Class B',
  C: 'Class C',
};

export type MovementType = 'Receipt' | 'Shipment' | 'Movement' | 'Inventory';

export type Period = 'Today' | 'ThisWeek' | 'ThisMonth';

export const PERIOD_ENUM = {
  TODAY: 'Today',
  THIS_WEEK: 'ThisWeek',
  THIS_MONTH: 'ThisMonth',
} as const;

export const PERIOD_OPTIONS = [
  { value: PERIOD_ENUM.TODAY, label: 'Period: Today' },
  { value: PERIOD_ENUM.THIS_WEEK, label: 'This week' },
  { value: PERIOD_ENUM.THIS_MONTH, label: 'This month' },
];
