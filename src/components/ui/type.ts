import type { ColumnDef } from '@tanstack/react-table';
import type { PropsWithChildren } from 'react';

export type UIComponentProps = PropsWithChildren<{
  className?: string;
}>;
export type DataTableProps<TData, TValue> = {
  columns: ColumnDef<TData, TValue>[];
  data: TData[];
  className?: string;
};
