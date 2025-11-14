// src/components/pages/Transactions/PhysicalInventoryPage.tsx
import { type ColumnDef } from '@tanstack/react-table';
import React from 'react';

import PageLayout from '@/components/organisms/PageLayout/PageLayout';
import { Badge } from '@/components/ui/badge';
import { Button } from '@/components/ui/button';
import { Checkbox } from '@/components/ui/checkbox';
import { DataTable } from '@/components/ui/data-table';
import type { DataTableProps } from '@/components/ui/type';
import { useIsMobile } from '@/hooks/use-mobile';
import { cn } from '@/lib/utils';

type Inventory = {
  docNo: string;
  warehouse: string;
  description: string;
  countDate: string;
  status: 'In Progress' | 'Completed';
  createdBy: string;
};

const inventories: Inventory[] = [
  {
    docNo: 'PI-240010',
    warehouse: 'Main DC',
    description: 'Cycle Count Aisle A',
    countDate: '2025-11-16',
    status: 'In Progress',
    createdBy: 'operator01',
  },
  {
    docNo: 'PI-240009',
    warehouse: 'Main DC',
    description: 'Full count – Main DC',
    countDate: '2025-11-10',
    status: 'Completed',
    createdBy: 'superuser',
  },
];

const columns: ColumnDef<Inventory>[] = [
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
    accessorKey: 'warehouse',
    header: 'Warehouse',
  },
  {
    accessorKey: 'description',
    header: 'Description',
  },
  {
    accessorKey: 'countDate',
    header: 'Count Date',
  },
  {
    accessorKey: 'status',
    header: 'Status',
    cell: ({ row }) => {
      const status = row.getValue<'In Progress' | 'Completed'>('status');
      return <Badge variant={'process'}>{status === 'Completed' ? 'success' : 'processing'}</Badge>;
    },
  },
  {
    accessorKey: 'createdBy',
    header: 'Created By',
  },
];

const PhysicalInventoryPage: React.FC = () => {
  const isMobile = useIsMobile();

  const tableProps: DataTableProps<Inventory, unknown> = {
    columns,
    data: inventories,
  };
  const toolbar = (
    <div
      className={cn(isMobile ? 'flex flex-col gap-2 w-full' : 'flex flex-wrap items-center gap-2')}
    >
      <Button size={isMobile ? 'default' : 'sm'} className="rounded-full bg-primary px-4 py-1.5">
        + New Inventory
      </Button>
      <Button
        variant={'outline'}
        size={isMobile ? 'default' : 'sm'}
        type="button"
        disabled
        className="rounded-full border border-border cursor-not-allowed"
      >
        Count List
      </Button>
      <Button
        variant={'outline'}
        size={isMobile ? 'default' : 'sm'}
        type="button"
        disabled
        className="rounded-full border border-border cursor-not-allowed"
      >
        Process Inventory
      </Button>
    </div>
  );

  const footer = (
    <div
      className={cn(
        'border-t border-border bg-card text-[11px] flex',
        isMobile ? 'px-3 py-2 flex-col gap-2' : 'px-6 py-2 justify-between'
      )}
    >
      <span>
        1 - {inventories.length} of {inventories.length} documents
      </span>
      <span>Items per page: 50</span>
    </div>
  );

  return (
    <PageLayout
      title="PHYSICAL INVENTORY"
      description="Create and manage physical inventory documents to count goods and update stock quantities."
      toolbar={toolbar}
      footer={footer}
    >
      <div className={cn(isMobile ? 'px-3 py-2' : 'px-6 py-3')}>
        <DataTable {...tableProps} />
      </div>
    </PageLayout>
  );
};

export default PhysicalInventoryPage;
