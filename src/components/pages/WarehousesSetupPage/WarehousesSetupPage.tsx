import { type ColumnDef } from '@tanstack/react-table';
import { Plus } from 'lucide-react';
import React, { useEffect } from 'react';
import { useDispatch } from 'react-redux';

import PageContent from '@/components/molecules/PageContent/PageContent';
import PageHeader from '@/components/molecules/PageHeader/PageHeader';
import PageOverview from '@/components/organisms/PageOverview/PageOverview';
import { Badge } from '@/components/ui/badge';
import { DataTable } from '@/components/ui/data-table';
import { Toolbar, ToolbarButton } from '@/components/ui/toolbar';
import type { DataTableProps } from '@/components/ui/type';
import { useIsMobile } from '@/hooks/use-mobile';
import { cn } from '@/lib/utils';
import { useSetupsSelectopr } from '@/state/ducks/setups-warehouse/selectors';
import { fetchSetupsWarehouseRequest } from '@/state/ducks/setups-warehouse/slice';
import type { Bin, Warehouse } from '@/state/ducks/setups-warehouse/type';

const warehouseColumns: ColumnDef<Warehouse>[] = [
  {
    accessorKey: 'id',
    header: 'Warehouse',
  },
  {
    accessorKey: 'code',
    header: 'Code',
  },
  {
    accessorKey: 'name',
    header: 'Name',
  },
  {
    accessorKey: 'organization',
    header: 'Organization',
  },
  {
    accessorKey: 'isActive',
    header: 'Active',
    cell: ({ row }) => {
      const active = row.getValue<boolean>('isActive');
      return <Badge variant={active ? 'default' : 'destructive'}>{active ? 'Yes' : 'No'}</Badge>;
    },
  },
];

const binColumns: ColumnDef<Bin>[] = [
  {
    accessorKey: 'id',
    header: 'BinId',
  },
  {
    accessorKey: 'warehouseId',
    header: 'Warehouse',
  },
  {
    accessorKey: 'code',
    header: 'Bin',
  },
  {
    accessorKey: 'description',
    header: 'Description',
  },
];

const WarehousesSetupPage: React.FC = () => {
  const dispatch = useDispatch();
  const isMobile = useIsMobile();
  const setupsSelector = useSetupsSelectopr();

  const warehouseTableProps: DataTableProps<Warehouse, unknown> = {
    columns: warehouseColumns,
    data: setupsSelector.data.warehouses ?? [],
  };

  const binTableProps: DataTableProps<Bin, unknown> = {
    columns: binColumns,
    data: setupsSelector.data.bins ?? [],
  };

  useEffect(() => {
    dispatch(fetchSetupsWarehouseRequest());
  }, [dispatch]);

  return (
    <PageOverview>
      <PageHeader
        title="WAREHOUSES & STORAGE BINS"
        description="Define warehouses and storage bins before executing Warehouse Management transactions."
      />
      <Toolbar>
        <ToolbarButton>
          <Plus size={isMobile ? 16 : 12} /> New warehouse
        </ToolbarButton>
        <ToolbarButton>
          <Plus size={isMobile ? 16 : 12} /> New bin
        </ToolbarButton>
      </Toolbar>
      <PageContent>
        <div className="flex-1 overflow-auto">
          <div
            className={cn(
              'grid gap-4',
              isMobile ? 'px-3 py-2 grid-cols-1' : 'px-6 py-3 md:grid-cols-2'
            )}
          >
            <div className="border border-border rounded bg-card overflow-hidden">
              <header
                className={cn(
                  'border-b border-border text-xs font-semibold',
                  isMobile ? 'px-2 py-1.5' : 'px-3 py-2'
                )}
              >
                Warehouses
              </header>
              <DataTable {...warehouseTableProps} className="border-0 rounded-none" />
            </div>

            <div className="border border-border rounded bg-card overflow-hidden">
              <header
                className={cn(
                  'border-b border-border  text-xs font-semibold',
                  isMobile ? 'px-2 py-1.5' : 'px-3 py-2'
                )}
              >
                Storage Bins
              </header>
              <DataTable {...binTableProps} className="border-0 rounded-none" />
            </div>
          </div>
        </div>
      </PageContent>
    </PageOverview>
  );
};

export default WarehousesSetupPage;
