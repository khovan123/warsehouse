import { type ColumnDef } from '@tanstack/react-table';
import { Minus, Plus } from 'lucide-react';
import React from 'react';

import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from '../../../components/ui/select';
import PageLayout from '../../organisms/PageLayout/PageLayout';
import { Badge } from '../../ui/badge';
import { Button } from '../../ui/button';
import { Checkbox } from '../../ui/checkbox';
import { DataTable } from '../../ui/data-table';
import { Input } from '../../ui/input';
import type { DataTableProps } from '../../ui/type';

type Movement = {
  docNo: string;
  movementDate: string;
  fromWarehouse: string;
  toWarehouse: string;
  lines: number;
  totalQty: number;
  status: 'Completed' | 'Draft';
  reason: string;
};

const movements: Movement[] = [
  {
    docNo: 'MV-240045',
    movementDate: '2025-11-16',
    fromWarehouse: 'Main DC',
    toWarehouse: 'Store 01',
    lines: 5,
    totalQty: 200,
    status: 'Completed',
    reason: 'Replenishment',
  },
  {
    docNo: 'MV-240046',
    movementDate: '2025-11-16',
    fromWarehouse: 'Main DC',
    toWarehouse: 'Store 02',
    lines: 3,
    totalQty: 120,
    status: 'Draft',
    reason: 'Initial stock',
  },
];

const columns: ColumnDef<Movement>[] = [
  {
    id: 'select',
    header: ({ table }) => (
      <Checkbox
        checked={table.getIsAllPageRowsSelected()}
        onCheckedChange={(value) => table.toggleAllPageRowsSelected(!!value)}
        aria-label="Select all"
        className="h-3 w-3"
      />
    ),
    cell: ({ row }) => (
      <Checkbox
        checked={row.getIsSelected()}
        onCheckedChange={(value) => row.toggleSelected(!!value)}
        aria-label="Select row"
        className="h-3 w-3"
      />
    ),
    enableSorting: false,
    enableHiding: false,
  },
  {
    accessorKey: 'docNo',
    header: 'Document No.',
  },
  {
    accessorKey: 'movementDate',
    header: 'Movement Date',
  },
  {
    accessorKey: 'fromWarehouse',
    header: 'From Warehouse',
  },
  {
    accessorKey: 'toWarehouse',
    header: 'To Warehouse',
  },
  {
    accessorKey: 'lines',
    header: () => <div className="text-right">Lines</div>,
    cell: ({ row }) => <div className="text-right">{row.getValue('lines')}</div>,
  },
  {
    accessorKey: 'totalQty',
    header: () => <div className="text-right">Total Qty</div>,
    cell: ({ row }) => <div className="text-right">{row.getValue('totalQty')}</div>,
  },
  {
    accessorKey: 'reason',
    header: 'Reason',
  },
  {
    accessorKey: 'status',
    header: 'Status',
    cell: ({ row }) => {
      const status = row.getValue<'Completed' | 'Draft'>('status');
      return <Badge variant={status === 'Completed' ? 'default' : 'destructive'}>{status}</Badge>;
    },
  },
];

const GoodsMovementPage: React.FC = () => {
  const tableProps: DataTableProps<Movement, unknown> = {
    columns,
    data: movements,
  };
  const toolbar = (
    <>
      <Button size={'sm'}>
        <Plus /> New Movement
      </Button>

      <Input
        type="text"
        placeholder="Search by document, warehouse..."
        className="h-8 rounded-full border border-border bg-background px-3 text-xs flex-1 min-w-40 focus:outline-none focus:ring-2 focus:ring-primary/40 placeholder:text-xs"
      />

      <Select>
        <SelectTrigger className="w-36 text-xs rounded-full h-8 border border-border">
          <SelectValue placeholder="All warehouses" />
        </SelectTrigger>
        <SelectContent className="w-fit border border-border">
          <SelectItem className="text-xs" value="All warehouses">
            All warehouses
          </SelectItem>
          <SelectItem className="text-xs" value="Main DC">
            Main DC
          </SelectItem>
          <SelectItem className="text-xs" value="Store 01">
            Store 01
          </SelectItem>
        </SelectContent>
      </Select>
    </>
  );

  const footer = (
    <div className="border-t border-border bg-card px-6 py-2 text-[11px] flex justify-between">
      <span>
        1 <Minus /> {movements.length} of {movements.length} movements
      </span>
      <span>Items per page: 50</span>
    </div>
  );

  return (
    <PageLayout
      title="GOODS MOVEMENTS"
      description="Move inventory between storage bins or warehouses following Openbravo Goods Movement flow."
      toolbar={toolbar}
      footer={footer}
    >
      <div className="px-6 py-3">
        <DataTable {...tableProps} />
      </div>
    </PageLayout>
  );
};

export default GoodsMovementPage;
