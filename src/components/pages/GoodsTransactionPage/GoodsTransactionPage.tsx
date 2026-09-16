import { type ColumnDef } from '@tanstack/react-table';
import { Minus } from 'lucide-react';
import React from 'react';

import PageLayout from '@/components/organisms/PageLayout/PageLayout';
import { DataTable } from '@/components/ui/data-table';
import { Input } from '@/components/ui/input';
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from '@/components/ui/select';
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

  const toolbar = (
    <div
      className={cn(isMobile ? 'flex flex-col gap-2 w-full' : 'flex flex-wrap items-center gap-2')}
    >
      <Select>
        <SelectTrigger
          className={cn(
            'text-xs rounded-full h-8 border border-border',
            isMobile ? 'w-full' : 'w-40'
          )}
        >
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
        <SelectTrigger
          className={cn(
            'text-xs rounded-full h-8 border border-border',
            isMobile ? 'w-full' : 'w-36'
          )}
        >
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
        placeholder="Filter by product, doc, business partner"
        className={cn(
          'h-8 rounded-full focus:outline-none placeholder:text-xs',
          isMobile ? 'w-full' : 'flex-1 min-w-40'
        )}
      />
    </div>
  );

  const footer = (
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
    <PageLayout
      title="GOODS TRANSACTION"
      description="Read-only view of all inventory transactions with filtering capabilities."
      toolbar={toolbar}
      footer={footer}
    >
      <div className={cn(isMobile ? 'px-3 py-2' : 'px-6 py-3')}>
        <DataTable {...tableProps} />
      </div>
    </PageLayout>
  );
};

export default GoodsTransactionPage;
