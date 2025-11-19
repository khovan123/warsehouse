import { type ColumnDef } from '@tanstack/react-table';
import { ChevronLeft, ChevronRight, ChevronsLeft, ChevronsRight, Download, X } from 'lucide-react';
import React from 'react';

import { FilterSelect } from '@/components/molecules/FilterSelect';
import PageLayout from '@/components/organisms/PageLayout/PageLayout';
import { Badge } from '@/components/ui/badge';
import { Button } from '@/components/ui/button';
import { Checkbox } from '@/components/ui/checkbox';
import { DataTable } from '@/components/ui/data-table';
import type { DataTableProps } from '@/components/ui/type';
import { useIsMobile } from '@/hooks/use-mobile';
import { cn } from '@/lib/utils';

type Product = {
  account: string;
  label: string;
  sku: string;
  baseUom: string;
  description: string;
  overbook: boolean;
  availability: 'Available' | 'Blocked' | 'Out of stock';
};

const DEMO_PRODUCTS: Product[] = [
  {
    account: '4046664115021',
    label: '4046664115021',
    sku: '5905417902',
    baseUom: '1',
    description: 'Holzspalter stehend HL1650 Zomtec - 400V 50Hz 350l',
    overbook: true,
    availability: 'Available',
  },
  {
    account: '4014915082057',
    label: '4014915082057',
    sku: '4901305902',
    baseUom: '1',
    description: 'Tischkreissäge TS310 Zomtec - 400V 50Hz 2800W - 3',
    overbook: true,
    availability: 'Available',
  },
  {
    account: '4014915042693',
    label: '4014915042693',
    sku: '10011210',
    baseUom: '1',
    description: 'Fahrvorrichtung',
    overbook: true,
    availability: 'Available',
  },
];

const columns: ColumnDef<Product>[] = [
  {
    id: 'select',
    header: ({ table }) => (
      <Checkbox
        checked={table.getIsAllPageRowsSelected()}
        onCheckedChange={(value) => table.toggleAllPageRowsSelected(!!value)}
        aria-label="Select all"
        className="h-3 w-3"
        size={12}
      />
    ),
    cell: ({ row }) => (
      <Checkbox
        checked={row.getIsSelected()}
        onCheckedChange={(value) => row.toggleSelected(!!value)}
        aria-label="Select row"
        className="h-3 w-3"
        size={12}
      />
    ),
    enableSorting: false,
    enableHiding: false,
  },
  {
    accessorKey: 'account',
    header: 'Account',
    cell: ({ row }) => <div className="whitespace-nowrap">{row.getValue('account')}</div>,
  },
  {
    accessorKey: 'label',
    header: 'Label',
    cell: ({ row }) => <div className="whitespace-nowrap">{row.getValue('label')}</div>,
  },
  {
    accessorKey: 'sku',
    header: 'SKU',
    cell: ({ row }) => <div className="whitespace-nowrap">{row.getValue('sku')}</div>,
  },
  {
    accessorKey: 'baseUom',
    header: () => <div className="text-center">Base UOM</div>,
    cell: ({ row }) => <div className="text-center">{row.getValue('baseUom')}</div>,
  },
  {
    accessorKey: 'description',
    header: 'Description',
  },
  {
    accessorKey: 'overbook',
    header: 'Overbook',
    cell: ({ row }) => <div>{String(row.getValue('overbook'))}</div>,
  },
  {
    accessorKey: 'availability',
    header: 'Av. state',
    cell: ({ row }) => (
      <div className="whitespace-nowrap">
        <Badge>{row.getValue('availability')}</Badge>
      </div>
    ),
  },
];

const ProductManagementPage: React.FC = () => {
  const isMobile = useIsMobile();

  const tableProps: DataTableProps<Product, unknown> = {
    columns,
    data: DEMO_PRODUCTS,
  };
  const toolbar = (
    <div
      className={cn(isMobile ? 'flex flex-col gap-2 w-full' : 'flex flex-wrap items-center gap-2')}
    >
      <Button
        type="button"
        variant={'outline'}
        size={isMobile ? 'default' : 'sm'}
        className="rounded-full cursor-not-allowed"
      >
        Details
      </Button>
      <Button type="button" size={isMobile ? 'default' : 'sm'} className="rounded-full">
        Export
        <Download size={isMobile ? 16 : 12} className="text-primary-foreground" />
      </Button>
    </div>
  );

  const headerMeta = (
    <div
      className={cn(
        'flex flex-wrap items-center gap-3 text-xs',
        isMobile ? 'flex-col' : 'justify-between'
      )}
    >
      <div className="flex items-center gap-2">
        <div className="inline-flex items-center rounded-md bg-card border border-border px-1 py-1 gap-0.5">
          <span>Result for:</span>
          <Button
            type="button"
            size={'sm'}
            variant={'ghost'}
            className="rounded-full hover:text-destructive"
          >
            Empty <X size={0} />
          </Button>
        </div>
      </div>
      <Button type="button" variant={'secondary'} size={'sm'}>
        Clear All Tags
      </Button>
    </div>
  );

  const footer = (
    <div
      className={cn(
        'border-t border-border bg-card flex items-center text-[11px]',
        isMobile ? 'px-3 py-2 flex-col gap-3' : 'px-6 py-2 justify-between'
      )}
    >
      <span>1 - {DEMO_PRODUCTS.length} of 726 items</span>
      <div className="flex items-center gap-1">
        <Button className={isMobile ? 'size-8' : 'size-6'}>
          <ChevronsLeft />
        </Button>
        <Button className={isMobile ? 'size-8' : 'size-6'}>
          <ChevronLeft />
        </Button>
        {[1, 2, 3, 4].map((page) => (
          <Button key={page} className={isMobile ? 'size-8' : 'size-6'}>
            {page}
          </Button>
        ))}
        <Button className={isMobile ? 'size-8' : 'size-6'}>
          <ChevronRight />
        </Button>
        <Button className={isMobile ? 'size-8' : 'size-6'}>
          <ChevronsRight />
        </Button>
      </div>
      <div className="flex items-center gap-1">
        <span>Items per page:</span>
        <FilterSelect
          defaultValue="200"
          triggerClassName={cn('rounded h-8', isMobile ? 'w-full' : 'w-20')}
          options={[
            { value: '200', label: '200' },
            { value: '100', label: '100' },
            { value: '50', label: '50' },
          ]}
        />
      </div>
    </div>
  );

  return (
    <PageLayout
      title="PRODUCT MANAGEMENT"
      description="Maintain product master data, tagging and availability for all warehouses."
      meta={headerMeta}
      toolbar={toolbar}
      footer={footer}
    >
      <div className={cn('w-full h-full', isMobile ? 'px-3 py-2' : 'px-6 py-3')}>
        <DataTable {...tableProps} />
      </div>
    </PageLayout>
  );
};

export default ProductManagementPage;
