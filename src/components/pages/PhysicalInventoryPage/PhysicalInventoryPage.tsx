// src/components/pages/Transactions/PhysicalInventoryPage.tsx
import { type ColumnDef } from '@tanstack/react-table';
import { Plus } from 'lucide-react';
import React from 'react';

import PageContent from '@/components/molecules/PageContent/PageContent';
import PageHeader from '@/components/molecules/PageHeader/PageHeader';
import PageOverview from '@/components/organisms/PageOverview/PageOverview';
import { Badge } from '@/components/ui/badge';
import { Checkbox } from '@/components/ui/checkbox';
import { DataTable } from '@/components/ui/data-table';
import { Toolbar, ToolbarButton } from '@/components/ui/toolbar';
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

  const Footer = () => (
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
    <PageOverview>
      <PageHeader
        title="PHYSICAL INVENTORY"
        description="Create and manage physical inventory documents to count goods and update stock quantities."
      />
      <Toolbar>
        <ToolbarButton>
          <Plus size={isMobile ? 16 : 12} /> New Inventory
        </ToolbarButton>
        <ToolbarButton disabled>Count List</ToolbarButton>
        <ToolbarButton disabled>Process Inventory</ToolbarButton>
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

export default PhysicalInventoryPage;
