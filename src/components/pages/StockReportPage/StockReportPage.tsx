// src/components/pages/Analysis/StockReportPage.tsx
import { type ColumnDef } from '@tanstack/react-table';
import React from 'react';

import { useIsMobile } from '../../../hooks/use-mobile';
import { cn } from '../../../lib/utils';
import { FilterSelect } from '../../molecules/FilterSelect';
import PageLayout from '../../organisms/PageLayout/PageLayout';
import { DataTable } from '../../ui/data-table';
import { Input } from '../../ui/input';
import type { DataTableProps } from '../../ui/type';

type StockRow = {
  product: string;
  warehouse: string;
  bin: string;
  category: string;
  onHand: number;
  reserved: number;
  available: number;
};

const rows: StockRow[] = [
  {
    product: 'SKU-1001 Cotton T-Shirt Blue M',
    warehouse: 'Main DC',
    bin: 'A-01-01',
    category: 'Apparel',
    onHand: 120,
    reserved: 20,
    available: 100,
  },
  {
    product: 'SKU-2001 Running Shoes 42',
    warehouse: 'Main DC',
    bin: 'B-01-03',
    category: 'Footwear',
    onHand: 40,
    reserved: 5,
    available: 35,
  },
];

const columns: ColumnDef<StockRow>[] = [
  {
    accessorKey: 'product',
    header: 'Product',
  },
  {
    accessorKey: 'warehouse',
    header: 'Warehouse',
  },
  {
    accessorKey: 'bin',
    header: 'Bin',
  },
  {
    accessorKey: 'category',
    header: 'Category',
  },
  {
    accessorKey: 'onHand',
    header: () => <div className="text-right">On Hand</div>,
    cell: ({ row }) => <div className="text-right">{row.getValue('onHand')}</div>,
  },
  {
    accessorKey: 'reserved',
    header: () => <div className="text-right">Reserved</div>,
    cell: ({ row }) => <div className="text-right">{row.getValue('reserved')}</div>,
  },
  {
    accessorKey: 'available',
    header: () => <div className="text-right">Available</div>,
    cell: ({ row }) => <div className="text-right">{row.getValue('available')}</div>,
  },
];

const StockReportPage: React.FC = () => {
  const isMobile = useIsMobile();

  const tableProps: DataTableProps<StockRow, unknown> = {
    columns,
    data: rows,
  };
  const toolbar = (
    <div
      className={cn(isMobile ? 'flex flex-col gap-2 w-full' : 'flex flex-wrap items-center gap-2')}
    >
      <FilterSelect
        defaultValue="all-warehouses"
        placeholder="All warehouses"
        triggerClassName={cn(isMobile ? 'w-full' : 'w-36')}
        options={[
          { value: 'all-warehouses', label: 'All warehouses' },
          { value: 'main-dc', label: 'Main DC' },
          { value: 'store-01', label: 'Store 01' },
        ]}
      />
      <FilterSelect
        defaultValue="all-categories"
        placeholder="All categories"
        triggerClassName={cn(isMobile ? 'w-full' : 'w-36')}
        options={[
          { value: 'all-categories', label: 'All categories' },
          { value: 'apparel', label: 'Apparel' },
          { value: 'footwear', label: 'Footwear' },
        ]}
      />
      <Input
        type="text"
        placeholder="Search product..."
        className={cn('rounded-full h-8', isMobile ? 'w-full' : 'flex-1 min-w-40')}
      />
    </div>
  );

  const footer = (
    <div
      className={cn(
        'border-t border-border bg-card text-[11px] flex',
        isMobile ? 'px-3 py-2 flex-col gap-2' : 'px-6 py-2 justify-between'
      )}
    >
      <span>
        1 - {rows.length} of {rows.length} rows
      </span>
      <span>Items per page: 200</span>
    </div>
  );

  return (
    <PageLayout
      title="STOCK REPORT"
      description="Stock level of all products and their location (warehouse and storage bin) grouped by product category."
      toolbar={toolbar}
      footer={footer}
    >
      <div className={cn(isMobile ? 'px-3 py-2' : 'px-6 py-3')}>
        <DataTable {...tableProps} />
      </div>
    </PageLayout>
  );
};

export default StockReportPage;
