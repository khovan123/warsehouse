import { type ColumnDef } from '@tanstack/react-table';
import React from 'react';

import PageLayout from '../../organisms/PageLayout/PageLayout';
import { DataTable } from '../../ui/data-table';
import { Input } from '../../ui/input';
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '../../ui/select';
import type { DataTableProps } from '../../ui/type';

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
  const tableProps: DataTableProps<Transaction, unknown> = {
    columns,
    data: transactions,
  };

  const toolbar = (
    <>
      <Select>
        <SelectTrigger className="w-40 text-xs rounded-full h-8 border border-border">
          <SelectValue placeholder="All transaction types" />
        </SelectTrigger>
        <SelectContent className="w-fit border border-border">
          <SelectItem className="text-xs" value="All transaction types">
            All transaction types
          </SelectItem>
          <SelectItem className="text-xs" value="Receipt">
            Receipt
          </SelectItem>
          <SelectItem className="text-xs" value="Shipment">
            Shipment
          </SelectItem>
          <SelectItem className="text-xs" value="Movement">
            Movement
          </SelectItem>
          <SelectItem className="text-xs" value="Inventory">
            Inventory
          </SelectItem>
        </SelectContent>
      </Select>

      <Select>
        <SelectTrigger className="w-36 text-xs rounded-full h-8 border border-border">
          <SelectValue placeholder="All warehouses" />
        </SelectTrigger>
        <SelectContent className="w-fit border border-border">
          <SelectItem className="text-xs" value="All warehouses">
            All warehouses
          </SelectItem>
          <SelectItem className="text-xs" value="Main DC">
            Main DC
          </SelectItem>
          <SelectItem className="text-xs" value="Store 01">
            Store 01
          </SelectItem>
        </SelectContent>
      </Select>
      <Input
        type="text"
        placeholder="Filter by product, doc, business partner..."
        className="h-8 rounded-full flex-1 min-w-40 focus:outline-none"
      />
    </>
  );

  const footer = (
    <div className="border-t border-border bg-card px-6 py-2 text-[11px] flex justify-between">
      <span>
        1 - {transactions.length} of {transactions.length} transactions
      </span>
      <span>Items per page: 100</span>
    </div>
  );

  return (
    <PageLayout
      title="GOODS TRANSACTION"
      description="Read-only view of all inventory transactions with filtering capabilities."
      toolbar={toolbar}
      footer={footer}
    >
      <div className="px-6 py-3">
        <DataTable {...tableProps} />
      </div>
    </PageLayout>
  );
};

export default GoodsTransactionPage;
