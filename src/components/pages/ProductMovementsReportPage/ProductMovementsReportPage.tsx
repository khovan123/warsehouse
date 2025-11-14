import { type ColumnDef } from '@tanstack/react-table';
import React from 'react';

import { FilterSelect } from '../../molecules/FilterSelect';
import { MetricCard } from '../../molecules/MetricCard';
import PageLayout from '../../organisms/PageLayout/PageLayout';
import { DataTable } from '../../ui/data-table';
import { Input } from '../../ui/input';
import type { DataTableProps } from '../../ui/type';

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
  const tableProps: DataTableProps<Movement, unknown> = {
    columns,
    data: movements,
  };

  const toolbar = (
    <>
      <FilterSelect
        defaultValue="today"
        placeholder="Period: Today"
        triggerClassName="w-40"
        options={[
          { value: 'today', label: 'Period: Today' },
          { value: 'week', label: 'This week' },
          { value: 'month', label: 'This month' },
        ]}
      />
      <FilterSelect
        defaultValue="all"
        placeholder="Movement type: All"
        triggerClassName="w-48"
        options={[
          { value: 'all', label: 'Movement type: All' },
          { value: 'receipt', label: 'Receipt' },
          { value: 'shipment', label: 'Shipment' },
          { value: 'movement', label: 'Movement' },
          { value: 'inventory', label: 'Inventory' },
        ]}
      />
      <Input
        type="text"
        placeholder="Product / Document / Bin"
        className="rounded-full flex-1 min-w-40 h-8"
      />
    </>
  );

  const footer = (
    <div className="border-t border-border bg-card px-6 py-2 text-[11px] flex justify-between">
      <span>
        1 - {movements.length} of {movements.length} movements
      </span>
      <span>Auto refresh every 2 min</span>
    </div>
  );

  return (
    <PageLayout
      title="PRODUCT MOVEMENTS REPORT"
      description="Goods Tracking view that lists movements and their bins to verify the history of each item."
      toolbar={toolbar}
      footer={footer}
    >
      <div className="px-6 py-3 space-y-4">
        <div className="grid gap-3 sm:grid-cols-3">
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
    </PageLayout>
  );
};

export default ProductMovementsReportPage;
