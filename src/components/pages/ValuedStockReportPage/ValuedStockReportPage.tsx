import { type ColumnDef } from '@tanstack/react-table';
import { Download } from 'lucide-react';
import React from 'react';

import { FilterSelect } from '../../molecules/FilterSelect';
import { MetricCard } from '../../molecules/MetricCard';
import PageLayout from '../../organisms/PageLayout/PageLayout';
import { Button } from '../../ui/button';
import { DataTable } from '../../ui/data-table';
import { Input } from '../../ui/input';
import type { DataTableProps } from '../../ui/type';

type ValuedStockRow = {
  product: string;
  category: string;
  warehouse: string;
  onHand: number;
  averageCost: number;
  inventoryValue: number;
};

const valuedRows: ValuedStockRow[] = [
  {
    product: 'SKU-1001 Cotton T-Shirt Blue M',
    category: 'Apparel',
    warehouse: 'Main DC',
    onHand: 120,
    averageCost: 9.4,
    inventoryValue: 1128,
  },
  {
    product: 'SKU-2001 Running Shoes 42',
    category: 'Footwear',
    warehouse: 'Main DC',
    onHand: 40,
    averageCost: 34.2,
    inventoryValue: 1368,
  },
  {
    product: 'SKU-4005 Winter Jacket L',
    category: 'Apparel',
    warehouse: 'Store 01',
    onHand: 15,
    averageCost: 45.5,
    inventoryValue: 682.5,
  },
];

const valuationSummary = [
  { label: 'Inventory Value', value: '3,178.5 €' },
  { label: 'Average Cost Variance', value: '+1.5 %' },
  { label: 'Products Valued', value: valuedRows.length.toString() },
];

const columns: ColumnDef<ValuedStockRow>[] = [
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
  const tableProps: DataTableProps<ValuedStockRow, unknown> = {
    columns,
    data: valuedRows,
  };
  const toolbar = (
    <>
      <Input type="date" className="rounded-full w-fit h-8" />
      <FilterSelect
        defaultValue="all-warehouses"
        placeholder="Warehouse: All"
        triggerClassName="w-44"
        options={[
          { value: 'all-warehouses', label: 'Warehouse: All' },
          { value: 'main-dc', label: 'Main DC' },
          { value: 'store-01', label: 'Store 01' },
        ]}
      />
      <FilterSelect
        defaultValue="all-categories"
        placeholder="Product category: All"
        triggerClassName="w-48"
        options={[
          { value: 'all-categories', label: 'Product category: All' },
          { value: 'apparel', label: 'Apparel' },
          { value: 'footwear', label: 'Footwear' },
        ]}
      />
      <Button className="rounded-full text-xs">
        Export CSV <Download />
      </Button>
    </>
  );

  const footer = (
    <div className="border-t border-border bg-card px-6 py-2 text-[11px] flex justify-between">
      <span>
        1 - {valuedRows.length} of {valuedRows.length} products
      </span>
      <span>Total value: {valuationSummary[0].value}</span>
    </div>
  );

  return (
    <PageLayout
      title="VALUED STOCK REPORT"
      description="Inventory valuation per product based on Average Costing algorithm."
      toolbar={toolbar}
      footer={footer}
    >
      <div className="px-6 py-3 space-y-4">
        <div className="grid gap-3 sm:grid-cols-3">
          {valuationSummary.map((item) => (
            <MetricCard key={item.label} label={item.label} value={item.value} />
          ))}
        </div>

        <DataTable {...tableProps} />
      </div>
    </PageLayout>
  );
};

export default ValuedStockReportPage;
