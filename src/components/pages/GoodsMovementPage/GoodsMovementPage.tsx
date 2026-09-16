import { type ColumnDef } from '@tanstack/react-table';
import { Plus } from 'lucide-react';
import React, { useEffect } from 'react';
import { useDispatch } from 'react-redux';

import FilterSelect from '@/components/molecules/FilterSelect/FilterSelect';
import PageContent from '@/components/molecules/PageContent/PageContent';
import PageHeader from '@/components/molecules/PageHeader/PageHeader';
import AppPagination from '@/components/organisms/AppPagination/AppPagination';
import PageOverview from '@/components/organisms/PageOverview/PageOverview';
import { Badge } from '@/components/ui/badge';
import { Checkbox } from '@/components/ui/checkbox';
import { DataTable } from '@/components/ui/data-table';
import { Toolbar, ToolbarButton, ToolbarInput } from '@/components/ui/toolbar';
import type { DataTableProps } from '@/components/ui/type';
import { useIsMobile } from '@/hooks/use-mobile';
import { cn } from '@/lib/utils';
import { useMovementSelectors } from '@/state/ducks/movement/selectors';
import { fetchMovementRequest } from '@/state/ducks/movement/slice';
import { flatMovementData, type FlatedMovement } from '@/utils/helpers/data-helper';

const columns: ColumnDef<FlatedMovement>[] = [
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
    header: 'Product',
    accessorKey: 'productId',
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
    accessorKey: 'fromBin',
    header: 'From Bin',
  },
  {
    accessorKey: 'toBin',
    header: 'To Bin',
  },
  // {
  //   accessorKey: 'lines',
  //   header: () => <div className="text-right">Lines</div>,
  //   cell: ({ row }) => <div className="text-right">{row.getValue('lines')}</div>,
  // },
  {
    accessorKey: 'qty',
    header: () => <div className="text-right">Qty</div>,
    cell: ({ row }) => <div className="text-right">{row.getValue('qty')}</div>,
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
  const dispatch = useDispatch();
  const movementSelectors = useMovementSelectors();

  const tableProps: DataTableProps<FlatedMovement, unknown> = {
    columns,
    data: flatMovementData(movementSelectors.movements),
  };

  useEffect(() => {
    dispatch(fetchMovementRequest());
  }, [dispatch]);

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
      <AppPagination />
    </PageOverview>
  );
};

export default GoodsMovementPage;
