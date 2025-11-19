import { type ColumnDef } from '@tanstack/react-table';
import React from 'react';

import { useIsMobile } from '../../../hooks/use-mobile';
import { cn } from '../../../lib/utils';
import { FilterSelect } from '../../molecules/FilterSelect';
import PageLayout from '../../organisms/PageLayout/PageLayout';
import { DataTable } from '../../ui/data-table';
import { Input } from '../../ui/input';
import type { DataTableProps } from '../../ui/type';

type Transaction = {
  line: number;
  document: string;
  movementDate: string;
  product: string;
  warehouse: string;
  bin: string;
  movementQty: number;
  uom: string;
  cost: number;
  txType: string;
  bpartner: string;
};

const transactions: Transaction[] = [
  {
    line: 1,
    document: 'GR-240001',
    movementDate: '2025-11-16',
    product: 'SKU-1001 Cotton T-Shirt Blue M',
    warehouse: 'Main DC',
    bin: 'A-01-01',
    movementQty: 50,
    uom: 'Unit',
    cost: 9.4,
    txType: 'Receipt',
    bpartner: 'Supplier A',
  },
  {
    line: 2,
    document: 'SO-240098',
    movementDate: '2025-11-16',
    product: 'SKU-2001 Running Shoes 42',
    warehouse: 'Main DC',
    bin: 'B-01-03',
    movementQty: -10,
    uom: 'Pair',
    cost: 34.2,
    txType: 'Shipment',
    bpartner: 'Store 01',
  },
  {
    line: 3,
    document: 'MV-240045',
    movementDate: '2025-11-16',
    product: 'SKU-3001 Travel Backpack',
    warehouse: 'Main DC',
    bin: 'C-02-03',
    movementQty: -5,
    uom: 'Unit',
    cost: 21.5,
    txType: 'Movement',
    bpartner: 'Store 02',
  },
];

const columns: ColumnDef<Transaction>[] = [
  {
    accessorKey: 'line',
    header: 'Line',
  },
  {
    accessorKey: 'document',
    header: 'Document',
  },
  {
    accessorKey: 'txType',
    header: 'Type',
  },
  {
    accessorKey: 'movementDate',
    header: 'Movement Date',
  },
  {
    accessorKey: 'product',
    header: 'Product',
  },
  {
    id: 'warehouseBin',
    header: 'Warehouse / Bin',
    cell: ({ row }) => (
      <div>
        {row.original.warehouse} / {row.original.bin}
      </div>
    ),
  },
  {
    accessorKey: 'movementQty',
    header: () => <div className="text-right">Qty</div>,
    cell: ({ row }) => <div className="text-right">{row.getValue('movementQty')}</div>,
  },
  {
    accessorKey: 'uom',
    header: 'UOM',
  },
  {
    accessorKey: 'cost',
    header: () => <div className="text-right">Cost</div>,
    cell: ({ row }) => (
      <div className="text-right">{row.getValue<number>('cost').toFixed(2)} €</div>
    ),
  },
  {
    accessorKey: 'bpartner',
    header: 'Business Partner',
  },
];

const MaterialTransactionReportPage: React.FC = () => {
  const isMobile = useIsMobile();

  const tableProps: DataTableProps<Transaction, unknown> = {
    columns,
    data: transactions,
  };
  const toolbar = (
    <div
      className={cn(isMobile ? 'flex flex-col gap-2 w-full' : 'flex flex-wrap items-center gap-2')}
    >
      <Input
        type="date"
        className={cn('rounded-full text-xs h-8', isMobile ? 'w-full' : 'w-fit')}
        defaultValue="2025-11-15"
      />
      <Input
        type="date"
        className={cn('rounded-full text-xs h-8', isMobile ? 'w-full' : 'w-fit')}
        defaultValue="2025-11-16"
      />
      <FilterSelect
        defaultValue="all-types"
        placeholder="All transaction types"
        triggerClassName={cn(isMobile ? 'w-full' : 'w-48')}
        options={[
          { value: 'all-types', label: 'All transaction types' },
          { value: 'receipt', label: 'Receipt' },
          { value: 'shipment', label: 'Shipment' },
          { value: 'movement', label: 'Movement' },
          { value: 'inventory', label: 'Inventory' },
        ]}
      />
      <FilterSelect
        defaultValue="all-warehouses"
        placeholder="All warehouses"
        triggerClassName={cn(isMobile ? 'w-full' : 'w-40')}
        options={[
          { value: 'all-warehouses', label: 'All warehouses' },
          { value: 'main-dc', label: 'Main DC' },
          { value: 'store-01', label: 'Store 01' },
        ]}
      />
      <Input
        type="text"
        placeholder="Product / Document / Partner"
        className={cn(
          'rounded-full focus:outline-none h-8',
          isMobile ? 'w-full' : 'flex-1 min-w-40'
        )}
      />
    </div>
  );

  const totalCost = transactions.reduce((sum, tx) => sum + tx.cost * tx.movementQty, 0);

  const footer = (
    <div
      className={cn(
        'border-t border-border bg-card text-[11px] flex',
        isMobile ? 'px-3 py-2 flex-col gap-2' : 'px-6 py-2 justify-between'
      )}
    >
      <span>
        1 - {transactions.length} of {transactions.length} lines
      </span>
      <span>Total Valuation: {totalCost.toFixed(2)} €</span>
    </div>
  );

  return (
    <PageLayout
      title="MATERIAL TRANSACTION REPORT"
      description="Lists all documents (shipments or receipts) grouped by Business Partner, aligning with Openbravo's analysis menu."
      toolbar={toolbar}
      footer={footer}
    >
      <div className={cn(isMobile ? 'px-3 py-2' : 'px-6 py-3')}>
        <DataTable {...tableProps} />
      </div>
    </PageLayout>
  );
};

export default MaterialTransactionReportPage;
