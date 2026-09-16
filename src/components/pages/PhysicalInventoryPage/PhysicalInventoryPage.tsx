import { type ColumnDef } from '@tanstack/react-table';
import { Plus } from 'lucide-react';
import React, { useEffect } from 'react';
import { useDispatch } from 'react-redux';

import PageContent from '@/components/molecules/PageContent/PageContent';
import PageHeader from '@/components/molecules/PageHeader/PageHeader';
import AppPagination from '@/components/organisms/AppPagination/AppPagination';
import PageOverview from '@/components/organisms/PageOverview/PageOverview';
import { Checkbox } from '@/components/ui/checkbox';
import { DataTable } from '@/components/ui/data-table';
import { Toolbar, ToolbarButton } from '@/components/ui/toolbar';
import type { DataTableProps } from '@/components/ui/type';
import { useIsMobile } from '@/hooks/use-mobile';
import { cn } from '@/lib/utils';
import { useInventorySelector } from '@/state/ducks/inventory/selectors';
import { fetchInventoryRequest } from '@/state/ducks/inventory/slice';
import type { Inventory } from '@/state/ducks/inventory/type';

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
    accessorKey: 'document',
    header: 'Document No.',
  },
  {
    accessorKey: 'line',
    header: 'Line',
  },
  {
    accessorKey: 'type',
    header: 'Type',
  },
  {
    accessorKey: 'movementDate',
    header: 'Movement Date',
  },
  {
    accessorKey: 'warehouseId',
    header: 'Warehouse',
  },
  {
    accessorKey: 'productId',
    header: 'Product',
  },
  {
    accessorKey: 'binId',
    header: 'Bin',
  },
  {
    accessorKey: 'qty',
    header: 'Quantity',
  },
  {
    accessorKey: 'uom',
    header: 'Uom',
  },
  {
    accessorKey: 'cost',
    header: 'Cost',
  },
  // {
  //   accessorKey: 'status',
  //   header: 'Status',
  //   cell: ({ row }) => {
  //     const status = row.getValue<'In Progress' | 'Completed'>('status');
  //     return <Badge variant={'process'}>{status === 'Completed' ? 'success' : 'processing'}</Badge>;
  //   },
  // },
  {
    accessorKey: 'bpartnerId',
    header: 'Business partner',
  },
];

const PhysicalInventoryPage: React.FC = () => {
  const dispatch = useDispatch();
  const inventorySelector = useInventorySelector();
  const isMobile = useIsMobile();

  useEffect(() => {
    dispatch(fetchInventoryRequest());
  }, [dispatch]);

  const tableProps: DataTableProps<Inventory, unknown> = {
    columns,
    data: inventorySelector.data.inventories ?? [],
  };

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
      <AppPagination />
    </PageOverview>
  );
};

export default PhysicalInventoryPage;
