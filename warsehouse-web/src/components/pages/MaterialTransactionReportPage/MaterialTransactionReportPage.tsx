import { type ColumnDef } from '@tanstack/react-table';
import React from 'react';

import FilterSelect from '@/components/molecules/FilterSelect/FilterSelect';
import PageContent from '@/components/molecules/PageContent/PageContent';
import PageHeader from '@/components/molecules/PageHeader/PageHeader';
import PageOverview from '@/components/organisms/PageOverview/PageOverview';
import { DataTable } from '@/components/ui/data-table';
import { Toolbar, ToolbarInput } from '@/components/ui/toolbar';
import type { DataTableProps } from '@/components/ui/type';
import { useIsMobile } from '@/hooks/use-mobile';
import { cn } from '@/lib/utils';

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

  const totalCost = transactions.reduce((sum, tx) => sum + tx.cost * tx.movementQty, 0);

  const Footer = () => (
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
    <PageOverview>
      <PageHeader
        title="MATERIAL TRANSACTION REPORT"
        description="Lists all documents (shipments or receipts) grouped by Business Partner, aligning with Openbravo's analysis menu."
      />
      <Toolbar>
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
        <ToolbarInput type="date" defaultValue="2025-11-15" />
        <ToolbarInput type="date" defaultValue="2025-11-16" />
        <ToolbarInput type="text" placeholder="Search by criteria" />
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

export default MaterialTransactionReportPage;
