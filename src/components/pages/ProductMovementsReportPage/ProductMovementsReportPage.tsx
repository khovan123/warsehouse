import { type ColumnDef } from '@tanstack/react-table';
import React from 'react';

import FilterSelect from '@/components/molecules/FilterSelect/FilterSelect';
import MetricCard from '@/components/molecules/MetricCard/MetricCard';
import PageContent from '@/components/molecules/PageContent/PageContent';
import PageHeader from '@/components/molecules/PageHeader/PageHeader';
import PageOverview from '@/components/organisms/PageOverview/PageOverview';
import { DataTable } from '@/components/ui/data-table';
import { Toolbar, ToolbarInput } from '@/components/ui/toolbar';
import type { DataTableProps } from '@/components/ui/type';
import { useIsMobile } from '@/hooks/use-mobile';
import { cn } from '@/lib/utils';

type Movement = {
  reference: string;
  movementDate: string;
  product: string;
  warehouse: string;
  type: string;
  qty: number;
  bin: string;
};

const movementSummary = [
  { label: 'Inbound Today', value: '+140', detail: 'Receipts + returns' },
  { label: 'Outbound Today', value: '-95', detail: 'Shipments + transfers' },
  { label: 'Net Variation', value: '+45', detail: 'Inventory increased' },
];

const movements: Movement[] = [
  {
    reference: 'PO-240012',
    movementDate: '2025-11-16',
    product: 'SKU-1001 Cotton T-Shirt Blue M',
    warehouse: 'Main DC',
    type: 'Receipt',
    qty: 50,
    bin: 'A-01-01',
  },
  {
    reference: 'SO-240110',
    movementDate: '2025-11-16',
    product: 'SKU-1001 Cotton T-Shirt Blue M',
    warehouse: 'Main DC',
    type: 'Shipment',
    qty: -30,
    bin: 'A-01-01',
  },
  {
    reference: 'MV-240045',
    movementDate: '2025-11-16',
    product: 'SKU-2001 Running Shoes 42',
    warehouse: 'Main DC → Store 01',
    type: 'Movement',
    qty: -20,
    bin: 'B-01-03',
  },
  {
    reference: 'PI-240010',
    movementDate: '2025-11-15',
    product: 'Cycle count Aisle A',
    warehouse: 'Main DC',
    type: 'Inventory',
    qty: 5,
    bin: 'A-03-01',
  },
];

const columns: ColumnDef<Movement>[] = [
  {
    accessorKey: 'reference',
    header: 'Reference',
  },
  {
    accessorKey: 'movementDate',
    header: 'Movement Date',
  },
  {
    accessorKey: 'product',
    header: 'Product / Description',
  },
  {
    accessorKey: 'warehouse',
    header: 'Warehouse / Route',
  },
  {
    accessorKey: 'type',
    header: 'Type',
  },
  {
    accessorKey: 'qty',
    header: () => <div className="text-right">Qty</div>,
    cell: ({ row }) => <div className="text-right">{row.getValue('qty')}</div>,
  },
  {
    accessorKey: 'bin',
    header: 'Bin',
  },
];

const ProductMovementsReportPage: React.FC = () => {
  const isMobile = useIsMobile();

  const tableProps: DataTableProps<Movement, unknown> = {
    columns,
    data: movements,
  };

  const Footer = () => (
    <div
      className={cn(
        'border-t border-border bg-card text-[11px] flex',
        isMobile ? 'px-3 py-2 flex-col gap-2' : 'px-6 py-2 justify-between'
      )}
    >
      <span>
        1 - {movements.length} of {movements.length} movements
      </span>
      <span>Auto refresh every 2 min</span>
    </div>
  );

  return (
    <PageOverview>
      <PageHeader
        title="PRODUCT MOVEMENTS REPORT"
        description="Goods Tracking view that lists movements and their bins to verify the history of each item."
      />
      <Toolbar>
        <FilterSelect
          defaultValue="today"
          placeholder="Period: Today"
          triggerClassName={cn(isMobile ? 'w-full' : 'w-40')}
          options={[
            { value: 'today', label: 'Period: Today' },
            { value: 'week', label: 'This week' },
            { value: 'month', label: 'This month' },
          ]}
        />
        <FilterSelect
          defaultValue="all"
          placeholder="Movement type: All"
          triggerClassName={cn(isMobile ? 'w-full' : 'w-48')}
          options={[
            { value: 'all', label: 'Movement type: All' },
            { value: 'receipt', label: 'Receipt' },
            { value: 'shipment', label: 'Shipment' },
            { value: 'movement', label: 'Movement' },
            { value: 'inventory', label: 'Inventory' },
          ]}
        />
        <ToolbarInput type="text" placeholder="Search by criteria" />
      </Toolbar>
      <PageContent>
        <div className={cn('space-y-4', isMobile ? 'px-3 py-2' : 'px-6 py-3')}>
          <div className={cn('grid gap-3', isMobile ? 'grid-cols-1' : 'sm:grid-cols-3')}>
            {movementSummary.map((card) => (
              <MetricCard
                key={card.label}
                label={card.label}
                value={card.value}
                description={card.detail}
                valueClassName={card.value.includes('-') ? 'text-destructive' : 'text-primary'}
              />
            ))}
          </div>

          <DataTable {...tableProps} />
        </div>
      </PageContent>
      <Footer />
    </PageOverview>
  );
};

export default ProductMovementsReportPage;
