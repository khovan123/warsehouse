import { type ColumnDef } from '@tanstack/react-table';
import React from 'react';

import { FilterSelect } from '@/components/molecules/FilterSelect';
import PageLayout from '@/components/organisms/PageLayout/PageLayout';
import { Button } from '@/components/ui/button';
import { DataTable } from '@/components/ui/data-table';
import type { DataTableProps } from '@/components/ui/type';
import { useIsMobile } from '@/hooks/use-mobile';
import { cn } from '@/lib/utils';

type CostRun = {
  jobNo: string;
  period: string;
  warehouse: string;
  startedAt: string;
  duration: string;
  status: 'Completed' | 'Failed' | 'Running';
  createdBy: string;
};

const costRuns: CostRun[] = [
  {
    jobNo: 'AVG-2025-11-16',
    period: 'Nov 2025',
    warehouse: 'Main DC',
    startedAt: '2025-11-16 14:30',
    duration: '00:08:12',
    status: 'Completed',
    createdBy: 'cost.manager',
  },
  {
    jobNo: 'AVG-2025-11-09',
    period: 'Nov 2025',
    warehouse: 'Store 01',
    startedAt: '2025-11-09 08:05',
    duration: '00:05:40',
    status: 'Completed',
    createdBy: 'system',
  },
  {
    jobNo: 'AVG-2025-11-02',
    period: 'Oct 2025',
    warehouse: 'Main DC',
    startedAt: '2025-11-02 07:55',
    duration: '00:11:23',
    status: 'Failed',
    createdBy: 'cost.manager',
  },
];

const metrics = [
  { label: 'Last Run', value: '16 Nov 2025 14:30' },
  { label: 'Pending Cost Rules', value: '0' },
  { label: 'Current Algorithm', value: 'Average (per legal entity)' },
];

const columns: ColumnDef<CostRun>[] = [
  {
    accessorKey: 'jobNo',
    header: 'Job No.',
  },
  {
    accessorKey: 'period',
    header: 'Period',
  },
  {
    accessorKey: 'warehouse',
    header: 'Warehouse',
  },
  {
    accessorKey: 'startedAt',
    header: 'Started At',
  },
  {
    accessorKey: 'duration',
    header: 'Duration',
  },
  {
    accessorKey: 'status',
    header: 'Status',
    cell: ({ row }) => {
      const status = row.getValue<'Completed' | 'Failed' | 'Running'>('status');
      return (
        <span
          className={
            'inline-flex rounded-full px-2 py-0.5 text-xs ' +
            (status === 'Completed'
              ? 'bg-primary/15 text-primary'
              : status === 'Failed'
                ? 'bg-destructive/15 text-destructive'
                : 'bg-secondary/20 text-secondary')
          }
        >
          {status}
        </span>
      );
    },
  },
  {
    accessorKey: 'createdBy',
    header: 'Created By',
  },
];

const GenerateAverageCostsPage: React.FC = () => {
  const isMobile = useIsMobile();

  const tableProps: DataTableProps<CostRun, unknown> = {
    columns,
    data: costRuns,
  };

  const toolbar = (
    <div
      className={cn(isMobile ? 'flex flex-col gap-2 w-full' : 'flex flex-wrap items-center gap-2')}
    >
      <Button className="rounded-full" size={'sm'}>
        Run cost calculation
      </Button>
      <FilterSelect
        defaultValue="org-all"
        placeholder="Organization: All"
        triggerClassName={cn(isMobile ? 'w-full' : 'w-48')}
        options={[
          { value: 'org-all', label: 'Organization: All' },
          { value: 'org-1', label: 'Org 1' },
          { value: 'org-2', label: 'Org 2' },
        ]}
      />
      <FilterSelect
        defaultValue="rule-default"
        placeholder="Costing Rule: Default"
        triggerClassName={cn(isMobile ? 'w-full' : 'w-48')}
        options={[
          { value: 'rule-default', label: 'Costing Rule: Default' },
          { value: 'rule-warehouse', label: 'Warehouse specific' },
        ]}
      />
    </div>
  );

  return (
    <PageLayout
      title="GENERATE AVERAGE COSTS"
      description="Schedule and monitor the costing background process defined in Inventory Accuracy."
      toolbar={toolbar}
    >
      <div className={cn('space-y-4', isMobile ? 'px-3 py-2' : 'px-6 py-3')}>
        <div className={cn('grid gap-3', isMobile ? 'grid-cols-1' : 'sm:grid-cols-3')}>
          {metrics.map((metric) => (
            <div
              key={metric.label}
              className="rounded-xl border border-border bg-card px-4 py-3 text-xs"
            >
              <p className="text-muted-foreground uppercase tracking-wide text-xs">
                {metric.label}
              </p>
              <p className="mt-2 text-lg font-semibold">{metric.value}</p>
            </div>
          ))}
        </div>

        <div className="border border-border rounded bg-card overflow-hidden">
          <header
            className={cn(
              'border-b border-border text-xs font-semibold',
              isMobile ? 'px-2 py-1.5' : 'px-4 py-2'
            )}
          >
            Costing Jobs
          </header>
          <DataTable {...tableProps} className="border-0 rounded-none" />
        </div>
      </div>
    </PageLayout>
  );
};

export default GenerateAverageCostsPage;
