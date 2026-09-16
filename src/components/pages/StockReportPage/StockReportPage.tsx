import { type ColumnDef } from '@tanstack/react-table';
import React, { useEffect } from 'react';
import { useDispatch } from 'react-redux';

import FilterSelect from '@/components/molecules/FilterSelect/FilterSelect';
import PageContent from '@/components/molecules/PageContent/PageContent';
import PageHeader from '@/components/molecules/PageHeader/PageHeader';
import AppPagination from '@/components/organisms/AppPagination/AppPagination';
import PageOverview from '@/components/organisms/PageOverview/PageOverview';
import { DataTable } from '@/components/ui/data-table';
import { Toolbar, ToolbarInput } from '@/components/ui/toolbar';
import type { DataTableProps } from '@/components/ui/type';
import { useIsMobile } from '@/hooks/use-mobile';
import { cn } from '@/lib/utils';
import { useStockSelector } from '@/state/ducks/stock/selectors';
import { stockRequest } from '@/state/ducks/stock/slice';
import type { Stock } from '@/state/ducks/stock/type';

const columns: ColumnDef<Stock>[] = [
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
  const dispatch = useDispatch();
  const isMobile = useIsMobile();
  const stockSelector = useStockSelector();

  useEffect(() => {
    dispatch(stockRequest());
  }, [dispatch]);

  const rows: Stock[] = stockSelector.data.stocks || [];

  const tableProps: DataTableProps<Stock, unknown> = {
    columns,
    data: rows,
  };

  return (
    <PageOverview>
      <PageHeader
        title="STOCK REPORT"
        description="Stock level of all products and their location (warehouse and storage bin) grouped by product category."
      />
      <Toolbar>
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
        <ToolbarInput type="text" placeholder="Search by criteria" />
      </Toolbar>
      <PageContent>
        <div className={cn(isMobile ? 'px-3 py-2' : 'px-6 py-3')}>
          <DataTable {...tableProps} />
        </div>
      </PageContent>
      <AppPagination />
    </PageOverview>
  );
};

export default StockReportPage;
