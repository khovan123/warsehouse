export type ParetoClassTag = 'A' | 'B' | 'C';

export const PARETO_CLASS_LABELS: Record<ParetoClassTag, string> = {
  A: 'Class A',
  B: 'Class B',
  C: 'Class C',
};

export type MovementType = 'Receipt' | 'Shipment' | 'Movement' | 'Inventory';

export type Period = 'Daily' | 'Weekly' | 'Monthly';

export const PERIOD_VALUES = {
  DAILY: 'Daily',
  WEEKLY: 'Weekly',
  MONTHLY: 'Monthly',
} as const;

export const PERIOD_OPTIONS = [
  { value: PERIOD_VALUES.DAILY, label: 'Period: Today' },
  { value: PERIOD_VALUES.WEEKLY, label: 'This week' },
  { value: PERIOD_VALUES.MONTHLY, label: 'This month' },
];
