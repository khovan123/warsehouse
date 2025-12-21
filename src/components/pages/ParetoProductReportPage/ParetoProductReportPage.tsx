import { type ColumnDef } from '@tanstack/react-table';
import React, { useEffect } from 'react';
import { useDispatch } from 'react-redux';

import FilterSelect from '@/components/molecules/FilterSelect/FilterSelect';
import PageContent from '@/components/molecules/PageContent/PageContent';
import PageHeader from '@/components/molecules/PageHeader/PageHeader';
import AppPagination from '@/components/organisms/AppPagination/AppPagination';
import PageOverview from '@/components/organisms/PageOverview/PageOverview';
import { Badge } from '@/components/ui/badge';
import { DataTable } from '@/components/ui/data-table';
import { Toolbar, ToolbarButton } from '@/components/ui/toolbar';
import type { DataTableProps } from '@/components/ui/type';
import { useIsMobile } from '@/hooks/use-mobile';
import { cn } from '@/lib/utils';
import { useParetoSelector } from '@/state/ducks/pareto/selectors';
import { fetchParetoRequest } from '@/state/ducks/pareto/slice';
import type { Pareto, ParetoClassification } from '@/state/ducks/pareto/type';
import { PARETO_CLASS_LABELS, type ParetoClassTag } from '@/utils/constants';
import { formatDecimalNumber } from '@/utils/helpers/format';

const columns: ColumnDef<Pareto>[] = [
  {
    accessorKey: 'product',
    header: 'Product',
  },
  {
    accessorKey: 'category',
    header: 'Category',
  },
  {
    accessorKey: 'annualConsumption',
    header: () => <div className="text-right">Annual Consumption</div>,
    cell: ({ row }) => <div className="text-right">{row.getValue('annualConsumption')}</div>,
  },
  {
    accessorKey: 'value',
    header: () => <div className="text-right">Annual Value (€)</div>,
    cell: ({ row }) => (
      <div className="text-right">{row.getValue<number>('value').toLocaleString()}</div>
    ),
  },
  {
    accessorKey: 'tag',
    header: 'Class',
    cell: ({ row }) => {
      const tag = row.getValue<ParetoClassTag>('tag');
      const variantMap: Record<ParetoClassTag, 'default' | 'process' | 'destructive'> = {
        A: 'default',
        B: 'process',
        C: 'destructive',
      };
      return <Badge variant={variantMap[tag]}>{tag}</Badge>;
    },
  },
];

const ParetoProductReportPage: React.FC = () => {
  const dispatch = useDispatch();
  const isMobile = useIsMobile();
  const paretoSelector = useParetoSelector();

  useEffect(() => {
    dispatch(fetchParetoRequest());
  }, [dispatch]);

  const rows: Pareto[] = paretoSelector.paretos || [];
  const classifications: ParetoClassification[] = paretoSelector.classifications || [];

  const tableProps: DataTableProps<Pareto, unknown> = {
    columns,
    data: rows,
  };

  return (
    <PageOverview>
      <PageHeader
        title="PARETO PRODUCT REPORT"
        description="Classify products using ABC analysis to prioritize counting and replenishment cycles."
      />
      <Toolbar>
        <ToolbarButton>Recalculate ABC</ToolbarButton>
        <FilterSelect
          defaultValue="all-warehouses"
          placeholder="Warehouse: All"
          triggerClassName={cn(isMobile ? 'w-full' : 'w-40')}
          options={[
            { value: 'all-warehouses', label: 'Warehouse: All' },
            { value: 'main-dc', label: 'Main DC' },
            { value: 'store-01', label: 'Store 01' },
          ]}
        />
        <FilterSelect
          defaultValue="last-12-months"
          placeholder="Period: Last 12 months"
          triggerClassName={cn(isMobile ? 'w-full' : 'w-48')}
          options={[
            { value: 'last-12-months', label: 'Period: Last 12 months' },
            { value: 'ytd', label: 'Year to date' },
            { value: 'quarter', label: 'Quarter' },
          ]}
        />
      </Toolbar>
      <PageContent>
        <div className={cn('space-y-4', isMobile ? 'px-3 py-2' : 'px-6 py-3')}>
          <div className={cn('grid gap-3', isMobile ? 'grid-cols-1' : 'sm:grid-cols-3')}>
            {classifications.map((item) => (
              <div
                key={item.tag}
                className={`rounded-xl border border-border bg-card px-4 py-3 text-xs bg-primary/20 text-primary`}
              >
                <p className="text-xs font-semibold">{PARETO_CLASS_LABELS[item.tag]}</p>
                <p className="mt-2 text-2xl font-bold">
                  {formatDecimalNumber(item.tagPercentage)}%
                </p>
                <p className="text-xs">
                  Value share:{' '}
                  <span className="font-semibold">
                    {formatDecimalNumber(item.valuePercentage)}%
                  </span>
                </p>
              </div>
            ))}
          </div>

          <DataTable {...tableProps} />
        </div>
      </PageContent>
      <AppPagination />
    </PageOverview>
  );
};

export default ParetoProductReportPage;
