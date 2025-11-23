import { type ColumnDef } from '@tanstack/react-table';
import { Minus } from 'lucide-react';
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
  docNo: string;
  txType: string;
  product: string;
  warehouse: string;
  qty: number;
  uom: string;
  movementDate: string;
  bpartner: string;
};

const transactions: Transaction[] = [
  {
    docNo: 'GR-240001',
    txType: 'Receipt',
    product: 'SKU-1001 Cotton T-Shirt Blue M',
    warehouse: 'Main DC',
    qty: 50,
    uom: 'Unit',
    movementDate: '2025-11-16',
    bpartner: 'Supplier A',
  },
  {
    docNo: 'SO-240098',
    txType: 'Shipment',
    product: 'SKU-2001 Running Shoes 42',
    warehouse: 'Main DC',
    qty: -10,
    uom: 'Pair',
    movementDate: '2025-11-16',
    bpartner: 'Store 01',
  },
];

const columns: ColumnDef<Transaction>[] = [
  {
    accessorKey: 'docNo',
    header: 'Document No.',
  },
  {
    accessorKey: 'txType',
    header: 'Type',
  },
  {
    accessorKey: 'product',
    header: 'Product',
  },
  {
    accessorKey: 'warehouse',
    header: 'Warehouse',
  },
  {
    accessorKey: 'qty',
    header: () => <div className="text-right">Qty</div>,
    cell: ({ row }) => <div className="text-right">{row.getValue('qty')}</div>,
  },
  {
    accessorKey: 'uom',
    header: 'UOM',
  },
  {
    accessorKey: 'movementDate',
    header: 'Movement Date',
  },
  {
    accessorKey: 'bpartner',
    header: 'Business Partner',
  },
];

const GoodsTransactionPage: React.FC = () => {
  const isMobile = useIsMobile();

  const tableProps: DataTableProps<Transaction, unknown> = {
    columns,
    data: transactions,
  };

  const Footer = () => (
    <div
      className={cn(
        'border-t border-border bg-card text-[11px] flex items-center',
        isMobile ? 'px-3 py-2 flex-col gap-2' : 'px-6 py-2 justify-between'
      )}
    >
      <span className="flex items-center">
        1 <Minus /> {transactions.length} of {transactions.length} transactions
      </span>
      <span>Items per page: 100</span>
    </div>
  );

  return (
    <PageOverview>
      <PageHeader
        title="GOODS TRANSACTION"
        description="Read-only view of all inventory transactions with filtering capabilities."
      />
      <Toolbar>
        <FilterSelect
          defaultValue="All transaction types"
          placeholder="All transaction types"
          options={[
            { value: 'All transaction types', label: 'All transaction types' },
            { value: 'Receipt', label: 'Receipt' },
            { value: 'Shipment', label: 'Shipment' },
            { value: 'Movement', label: 'Movement' },
            { value: 'Inventory', label: 'Inventory' },
          ]}
        />

        <FilterSelect
          defaultValue="All warehouses"
          placeholder="All warehouses"
          options={[
            { value: 'All warehouses', label: 'All warehouses' },
            { value: 'Main DC', label: 'Main DC' },
            { value: 'Store 01', label: 'Store 01' },
          ]}
        />
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

export default GoodsTransactionPage;
