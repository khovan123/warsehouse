import { type ColumnDef } from '@tanstack/react-table';
import { Minus, Plus } from 'lucide-react';
import React from 'react';

import FilterSelect from '@/components/molecules/FilterSelect/FilterSelect';
import PageContent from '@/components/molecules/PageContent/PageContent';
import PageHeader from '@/components/molecules/PageHeader/PageHeader';
import PageOverview from '@/components/organisms/PageOverview/PageOverview';
import { Badge } from '@/components/ui/badge';
import { Checkbox } from '@/components/ui/checkbox';
import { DataTable } from '@/components/ui/data-table';
import { Toolbar, ToolbarButton, ToolbarInput } from '@/components/ui/toolbar';
import type { DataTableProps } from '@/components/ui/type';
import { useIsMobile } from '@/hooks/use-mobile';
import { cn } from '@/lib/utils';

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
  const isMobile = useIsMobile();

  const tableProps: DataTableProps<Movement, unknown> = {
    columns,
    data: movements,
  };

  const Footer = () => (
    <div
      className={cn(
        'border-t border-border bg-card text-[11px] flex items-center',
        isMobile ? 'px-3 py-2 flex-col gap-2' : 'px-6 py-2 justify-between'
      )}
    >
      <span className="flex items-center">
        1 <Minus /> {movements.length} of {movements.length} movements
      </span>
      <span>Items per page: 50</span>
    </div>
  );

  return (
    <PageOverview>
      <PageHeader
        title="GOODS MOVEMENTS"
        description="Move inventory between storage bins or warehouses following Openbravo Goods Movement flow."
      />
      <Toolbar>
        <ToolbarButton>
          <Plus size={isMobile ? 16 : 12} /> New Movement
        </ToolbarButton>
        <FilterSelect
          defaultValue="All warehouses"
          placeholder="All warehouses"
          options={[
            { value: 'All warehouses', label: 'All warehouses' },
            { value: 'Main DC', label: 'Main DC' },
            { value: 'Store 01', label: 'Store 01' },
          ]}
        />

        <ToolbarInput type="text" placeholder="Search by document, warehouse" />
      </Toolbar>
      <PageContent>
        <div className={cn(isMobile ? 'px-3 py-2' : 'px-6 py-3')}>
          <DataTable {...tableProps} />
        </div>
      </PageContent>
      <Footer />
    </PageOverview>
  );
};

export default GoodsMovementPage;
