import { type ColumnDef } from '@tanstack/react-table';
import React, { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';

import FilterSelect from '@/components/molecules/FilterSelect/FilterSelect';
import MetricCard from '@/components/molecules/MetricCard/MetricCard';
import PageContent from '@/components/molecules/PageContent/PageContent';
import PageHeader from '@/components/molecules/PageHeader/PageHeader';
import AppPagination from '@/components/organisms/AppPagination/AppPagination';
import PageOverview from '@/components/organisms/PageOverview/PageOverview';
import { DataTable } from '@/components/ui/data-table';
import { Toolbar, ToolbarInput } from '@/components/ui/toolbar';
import type { DataTableProps } from '@/components/ui/type';
import { useIsMobile } from '@/hooks/use-mobile';
import { cn } from '@/lib/utils';
import { useMovementSelector } from '@/state/ducks/movement/selectors';
import {
  fetchMovementReportRequest,
  fetchMovementSummaryRequest,
} from '@/state/ducks/movement/slice';
import type { MovementReport } from '@/state/ducks/movement/type';
import type { Period } from '@/utils/constants';
import { PERIOD_ENUM, PERIOD_OPTIONS } from '@/utils/constants';

const columns: ColumnDef<MovementReport>[] = [
  {
    accessorKey: 'docNo',
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
    id: 'warehouse',
    header: 'Warehouse / Route',
    cell: ({ row }) => {
      const fromWarehouse = row.original.fromWarehouse;
      const toWarehouse = row.original.toWarehouse;
      return fromWarehouse === toWarehouse ? fromWarehouse : `${fromWarehouse} → ${toWarehouse}`;
    },
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
  const isMobile = useIsMobile();
  const dispatch = useDispatch();
  const { data, reportLoading } = useMovementSelector();
  const [period, setPeriod] = useState<Period>(PERIOD_ENUM.DAILY);

  useEffect(() => {
    dispatch(fetchMovementReportRequest());
  }, [dispatch]);

  useEffect(() => {
    dispatch(fetchMovementSummaryRequest({ period }));
  }, [dispatch, period]);

  const summary = data.movementSummaries[0];
  const movementSummary = summary
    ? [
        {
          label: 'Inbound',
          value: `+${summary.positiveQty}`,
          detail: 'Receipts + returns',
        },
        {
          label: 'Outbound',
          value: `${summary.negativeQty}`,
          detail: 'Shipments + transfers',
        },
        {
          label: 'Net Variation',
          value: `${summary.totalQty}`,
          detail: summary.totalQty >= 0 ? 'Inventory increased' : 'Inventory decreased',
        },
      ]
    : [];

  const tableProps: DataTableProps<MovementReport, unknown> = {
    columns,
    data: data.movementReports,
  };

  return (
    <PageOverview>
      <PageHeader
        title="PRODUCT MOVEMENTS REPORT"
        description="Goods Tracking view that lists movements and their bins to verify the history of each item."
      />
      <Toolbar>
        <FilterSelect
          value={period}
          onValueChange={(value) => setPeriod(value as Period)}
          placeholder="Period: Today"
          triggerClassName={cn(isMobile ? 'w-full' : 'w-40')}
          options={PERIOD_OPTIONS}
        />
        <FilterSelect
          defaultValue="all"
          placeholder="Movement type: All"
          triggerClassName={cn(isMobile ? 'w-full' : 'w-48')}
          options={[
            { value: 'all', label: 'Movement type: All' },
            { value: 'receipt', label: 'Receipt' },
            { value: 'shipment', label: 'Shipment' },
            { value: 'movement', label: 'Movement' },
            { value: 'inventory', label: 'Inventory' },
          ]}
        />
        <ToolbarInput type="text" placeholder="Search by criteria" />
      </Toolbar>
      <PageContent>
        <div className={cn('space-y-4', isMobile ? 'px-3 py-2' : 'px-6 py-3')}>
          <div className={cn('grid gap-3', isMobile ? 'grid-cols-1' : 'sm:grid-cols-3')}>
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

          <DataTable {...tableProps} isLoadingData={reportLoading} />
        </div>
      </PageContent>
      <AppPagination />
    </PageOverview>
  );
};

export default ProductMovementsReportPage;
