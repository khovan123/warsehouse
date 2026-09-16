import { type ColumnDef } from '@tanstack/react-table';
import { Download } from 'lucide-react';
import React, { useEffect } from 'react';
import { useDispatch } from 'react-redux';

import FilterSelect from '@/components/molecules/FilterSelect/FilterSelect';
import MetricCard from '@/components/molecules/MetricCard/MetricCard';
import PageContent from '@/components/molecules/PageContent/PageContent';
import PageHeader from '@/components/molecules/PageHeader/PageHeader';
import AppPagination from '@/components/organisms/AppPagination/AppPagination';
import PageOverview from '@/components/organisms/PageOverview/PageOverview';
import { DataTable } from '@/components/ui/data-table';
import { Toolbar, ToolbarButton, ToolbarInput } from '@/components/ui/toolbar';
import type { DataTableProps } from '@/components/ui/type';
import { useIsMobile } from '@/hooks/use-mobile';
import { cn } from '@/lib/utils';
import { useStockSelector } from '@/state/ducks/stock/selectors';
import { stockRequest } from '@/state/ducks/stock/slice';
import type { Stock } from '@/state/ducks/stock/type';

const valuationSummary = [
  { label: 'Inventory Value', value: '3,178.5 €' },
  { label: 'Average Cost Variance', value: '+1.5 %' },
  { label: 'Products Valued', value: '2' },
];

const columns: ColumnDef<Stock>[] = [
  {
    accessorKey: 'product',
    header: 'Product',
  },
  {
    accessorKey: 'category',
    header: 'Category',
  },
  {
    accessorKey: 'warehouse',
    header: 'Warehouse',
  },
  {
    accessorKey: 'onHand',
    header: () => <div className="text-right">On Hand</div>,
    cell: ({ row }) => <div className="text-right">{row.getValue('onHand')}</div>,
  },
  {
    accessorKey: 'averageCost',
    header: () => <div className="text-right">Avg Cost</div>,
    cell: ({ row }) => (
      <div className="text-right">{row.getValue<number>('averageCost').toFixed(2)} €</div>
    ),
  },
  {
    accessorKey: 'inventoryValue',
    header: () => <div className="text-right">Inventory Value</div>,
    cell: ({ row }) => (
      <div className="text-right">{row.getValue<number>('inventoryValue').toFixed(2)} €</div>
    ),
  },
];

const ValuedStockReportPage: React.FC = () => {
  const dispatch = useDispatch();
  const isMobile = useIsMobile();
  const stockSelector = useStockSelector();

  useEffect(() => {
    dispatch(stockRequest());
  }, [dispatch]);

  const tableProps: DataTableProps<Stock, unknown> = {
    columns,
    data: stockSelector.data.stocks || [],
  };

  return (
    <PageOverview>
      <PageHeader
        title="VALUED STOCK REPORT"
        description="Inventory valuation per product based on Average Costing algorithm."
      />
      <Toolbar>
        <ToolbarButton>
          Export CSV <Download size={isMobile ? 16 : 12} />
        </ToolbarButton>
        <FilterSelect
          defaultValue="all-warehouses"
          placeholder="Warehouse: All"
          triggerClassName={cn(isMobile ? 'w-full' : 'w-44')}
          options={[
            { value: 'all-warehouses', label: 'Warehouse: All' },
            { value: 'main-dc', label: 'Main DC' },
            { value: 'store-01', label: 'Store 01' },
          ]}
        />
        <FilterSelect
          defaultValue="all-categories"
          placeholder="Product category: All"
          triggerClassName={cn(isMobile ? 'w-full' : 'w-48')}
          options={[
            { value: 'all-categories', label: 'Product category: All' },
            { value: 'apparel', label: 'Apparel' },
            { value: 'footwear', label: 'Footwear' },
          ]}
        />
        <ToolbarInput type="date" />
      </Toolbar>
      <PageContent>
        <div className={cn('space-y-4', isMobile ? 'px-3 py-2' : 'px-6 py-3')}>
          <div className={cn('grid gap-3', isMobile ? 'grid-cols-1' : 'sm:grid-cols-3')}>
            {valuationSummary.map((item) => (
              <MetricCard key={item.label} label={item.label} value={item.value} />
            ))}
          </div>

          <DataTable {...tableProps} isLoadingData={stockSelector.loading} />
        </div>
      </PageContent>
      <AppPagination />
    </PageOverview>
  );
};

export default ValuedStockReportPage;
