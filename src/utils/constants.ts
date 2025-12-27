import type { FilterOption } from '@/components/molecules/FilterSelect/FilterSelect';

export type ParetoClassTag = 'A' | 'B' | 'C';

export const PARETO_CLASS_LABELS: Record<ParetoClassTag, string> = {
  A: 'Class A',
  B: 'Class B',
  C: 'Class C',
};

export type MovementType = 'Receipt' | 'Shipment' | 'Movement' | 'Inventory';

export type Period = 'Today' | 'This Week' | 'This Month';

export const PERIOD_OPTIONS: FilterOption[] = [
  { value: 0, label: 'Today' },
  { value: 1, label: 'This week' },
  { value: 2, label: 'This month' },
  { value: 3, label: 'This year' },
];
