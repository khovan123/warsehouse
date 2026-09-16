import type { FilterOption } from '@/components/molecules/FilterSelect/FilterSelect';

export type ParetoClassTag = 'A' | 'B' | 'C';

export const PARETO_CLASS_LABELS: Record<ParetoClassTag, string> = {
  A: 'Class A',
  B: 'Class B',
  C: 'Class C',
};

export type InventoryType = -1 | 0 | 1 | 2 | 3;

export const INVENTORY_TYPE_OPTIONS: FilterOption[] = [
  { value: -1, label: 'All' },
  { value: 0, label: 'Receipt' },
  { value: 1, label: 'Shipment' },
  { value: 2, label: 'Movement' },
  { value: 3, label: 'Inventory' },
];

export type Period = 0 | 1 | 2 | 3;

export const PERIOD_OPTIONS: FilterOption[] = [
  { value: 0, label: 'Today' },
  { value: 1, label: 'This week' },
  { value: 2, label: 'This month' },
  { value: 3, label: 'This year' },
];
