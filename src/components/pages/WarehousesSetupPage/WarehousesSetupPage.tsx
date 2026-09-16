// src/components/pages/Setup/WarehousesSetupPage.tsx
import { type ColumnDef } from '@tanstack/react-table';
import { Plus } from 'lucide-react';
import React from 'react';

import PageContent from '@/components/molecules/PageContent/PageContent';
import PageHeader from '@/components/molecules/PageHeader/PageHeader';
import PageOverview from '@/components/organisms/PageOverview/PageOverview';
import { Badge } from '@/components/ui/badge';
import { DataTable } from '@/components/ui/data-table';
import { Toolbar, ToolbarButton } from '@/components/ui/toolbar';
import type { DataTableProps } from '@/components/ui/type';
import { useIsMobile } from '@/hooks/use-mobile';
import { cn } from '@/lib/utils';

type Warehouse = {
  code: string;
  name: string;
  active: boolean;
  organization: string;
};

type Bin = {
  warehouse: string;
  bin: string;
  description: string;
};

const warehouses: Warehouse[] = [
  { code: 'MAIN-DC', name: 'Main Distribution Center', active: true, organization: 'Org 1' },
  { code: 'STORE-01', name: 'Store 01 Backroom', active: true, organization: 'Org 1' },
];

const bins: Bin[] = [
  { warehouse: 'MAIN-DC', bin: 'A-01-01', description: 'Aisle A / Rack 01 / Level 01' },
  { warehouse: 'MAIN-DC', bin: 'A-01-02', description: 'Aisle A / Rack 01 / Level 02' },
  { warehouse: 'STORE-01', bin: 'B-02-01', description: 'Backroom shelf B-02' },
];

const warehouseColumns: ColumnDef<Warehouse>[] = [
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
    accessorKey: 'active',
    header: 'Active',
    cell: ({ row }) => {
      const active = row.getValue<boolean>('active');
      return <Badge variant={active ? 'default' : 'destructive'}>{active ? 'Yes' : 'No'}</Badge>;
    },
  },
];

const binColumns: ColumnDef<Bin>[] = [
  {
    accessorKey: 'warehouse',
    header: 'Warehouse',
  },
  {
    accessorKey: 'bin',
    header: 'Bin',
  },
  {
    accessorKey: 'description',
    header: 'Description',
  },
];

const WarehousesSetupPage: React.FC = () => {
  const isMobile = useIsMobile();

  const warehouseTableProps: DataTableProps<Warehouse, unknown> = {
    columns: warehouseColumns,
    data: warehouses,
  };

  const binTableProps: DataTableProps<Bin, unknown> = {
    columns: binColumns,
    data: bins,
  };

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
