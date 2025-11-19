import { type ColumnDef } from '@tanstack/react-table';
import React from 'react';

import { FilterSelect } from '../../molecules/FilterSelect';
import PageLayout from '../../organisms/PageLayout/PageLayout';
import { Badge } from '../../ui/badge';
import { Button } from '../../ui/button';
import { DataTable } from '../../ui/data-table';
import type { DataTableProps } from '../../ui/type';

type ParetoRow = {
  product: string;
  category: string;
  annualConsumption: number;
  value: number;
  classTag: 'A' | 'B' | 'C';
};

const classification = [
  { label: 'Class A', percentage: 20, valueShare: 70, color: 'bg-primary/20 text-primary' },
  { label: 'Class B', percentage: 30, valueShare: 20, color: 'bg-primary/20 text-primary' },
  { label: 'Class C', percentage: 50, valueShare: 10, color: 'bg-primary/20 text-primary' },
];

const paretoRows: ParetoRow[] = [
  {
    product: 'SKU-1001 Cotton T-Shirt Blue M',
    category: 'Apparel',
    annualConsumption: 2000,
    value: 18800,
    classTag: 'A',
  },
  {
    product: 'SKU-2001 Running Shoes 42',
    category: 'Footwear',
    annualConsumption: 800,
    value: 27360,
    classTag: 'A',
  },
  {
    product: 'SKU-3010 Leather Belt',
    category: 'Accessories',
    annualConsumption: 450,
    value: 5400,
    classTag: 'B',
  },
  {
    product: 'SKU-5002 Keychain',
    category: 'Accessories',
    annualConsumption: 1200,
    value: 1800,
    classTag: 'C',
  },
];

const columns: ColumnDef<ParetoRow>[] = [
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
    accessorKey: 'classTag',
    header: 'Class',
    cell: ({ row }) => {
      const classTag = row.getValue<'A' | 'B' | 'C'>('classTag');
      return (
        <Badge
          variant={classTag === 'A' ? 'default' : classTag === 'B' ? 'process' : 'destructive'}
        >
          {classTag}
        </Badge>
      );
    },
  },
];

const ParetoProductReportPage: React.FC = () => {
  const tableProps: DataTableProps<ParetoRow, unknown> = {
    columns,
    data: paretoRows,
  };

  const toolbar = (
    <>
      <FilterSelect
        defaultValue="all-warehouses"
        placeholder="Warehouse: All"
        triggerClassName="w-40"
        options={[
          { value: 'all-warehouses', label: 'Warehouse: All' },
          { value: 'main-dc', label: 'Main DC' },
          { value: 'store-01', label: 'Store 01' },
        ]}
      />
      <FilterSelect
        defaultValue="last-12-months"
        placeholder="Period: Last 12 months"
        triggerClassName="w-48"
        options={[
          { value: 'last-12-months', label: 'Period: Last 12 months' },
          { value: 'ytd', label: 'Year to date' },
          { value: 'quarter', label: 'Quarter' },
        ]}
      />
      <Button className="rounded-full" size={'sm'}>
        Recalculate ABC
      </Button>
    </>
  );

  const footer = (
    <div className="border-t border-border bg-card px-6 py-2 text-[11px] flex justify-between">
      <span>
        1 - {paretoRows.length} of {paretoRows.length} SKUs
      </span>
      <span>ABC classification aligned with Pareto Product Report</span>
    </div>
  );

  return (
    <PageLayout
      title="PARETO PRODUCT REPORT"
      description="Classify products using ABC analysis to prioritize counting and replenishment cycles."
      toolbar={toolbar}
      footer={footer}
    >
      <div className="px-6 py-3 space-y-4">
        <div className="grid gap-3 sm:grid-cols-3">
          {classification.map((item) => (
            <div
              key={item.label}
              className={`rounded-xl border border-border bg-card px-4 py-3 text-xs ${item.color}`}
            >
              <p className="text-[12px] font-semibold">{item.label}</p>
              <p className="mt-2 text-2xl font-bold">{item.percentage}%</p>
              <p className="text-[11px]">
                Value share: <span className="font-semibold">{item.valueShare}%</span>
              </p>
            </div>
          ))}
        </div>

        <DataTable {...tableProps} />
      </div>
    </PageLayout>
  );
};

export default ParetoProductReportPage;
