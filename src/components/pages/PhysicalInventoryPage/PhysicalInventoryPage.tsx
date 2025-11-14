// src/components/pages/Transactions/PhysicalInventoryPage.tsx
import { type ColumnDef } from '@tanstack/react-table';
import React from 'react';

import PageLayout from '../../organisms/PageLayout/PageLayout';
import { Badge } from '../../ui/badge';
import { Button } from '../../ui/button';
import { Checkbox } from '../../ui/checkbox';
import { DataTable } from '../../ui/data-table';
import type { DataTableProps } from '../../ui/type';

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
  const tableProps: DataTableProps<Inventory, unknown> = {
    columns,
    data: inventories,
  };
  const toolbar = (
    <>
      <Button size={'sm'} className="rounded-full bg-primary px-4 py-1.5">
        + New Inventory
      </Button>
      <Button
        variant={'outline'}
        size={'sm'}
        type="button"
        disabled
        className="rounded-full border border-border cursor-not-allowed"
      >
        Count List
      </Button>
      <Button
        variant={'outline'}
        size={'sm'}
        type="button"
        disabled
        className="rounded-full border border-border cursor-not-allowed"
      >
        Process Inventory
      </Button>
    </>
  );

  const footer = (
    <div className="border-t border-border bg-card px-6 py-2 text-[11px] flex justify-between">
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
      <div className="px-6 py-3">
        <DataTable {...tableProps} />
      </div>
    </PageLayout>
  );
};

export default PhysicalInventoryPage;
