import { type ColumnDef } from '@tanstack/react-table';
import { Download } from 'lucide-react';
import React, { useEffect } from 'react';
import { useDispatch } from 'react-redux';

import PageContent from '@/components/molecules/PageContent/PageContent';
import PageHeader from '@/components/molecules/PageHeader/PageHeader';
import AppPagination from '@/components/organisms/AppPagination/AppPagination';
import PageOverview from '@/components/organisms/PageOverview/PageOverview';
import { Badge } from '@/components/ui/badge';
import { Checkbox } from '@/components/ui/checkbox';
import { DataTable } from '@/components/ui/data-table';
import { Toolbar, ToolbarButton } from '@/components/ui/toolbar';
import type { DataTableProps } from '@/components/ui/type';
import { useIsMobile } from '@/hooks/use-mobile';
import { cn } from '@/lib/utils';
import { useProductSelector } from '@/state/ducks/product/selectors';
import { productRequest } from '@/state/ducks/product/slice';
import type { Product } from '@/state/ducks/product/type';
// type Product = {
//   account: string;
//   label: string;
//   sku: string;
//   baseUom: string;
//   description: string;
//   overbook: boolean;
//   availability: 'Available' | 'Blocked' | 'Out of stock';
// };

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
    accessorKey: 'accountId',
    header: 'Account',
    cell: ({ row }) => <div className="whitespace-nowrap">{row.getValue('accountId')}</div>,
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
    accessorKey: 'isOverBook',
    header: 'Overbook',
    cell: ({ row }) => <div>{String(row.getValue('isOverBook'))}</div>,
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
  const dispatch = useDispatch();
  const isMobile = useIsMobile();
  const productSelector = useProductSelector();

  const tableProps: DataTableProps<Product, unknown> = {
    columns,
    data: productSelector.data.products || [],
  };

  useEffect(() => {
    dispatch(productRequest());
  }, [dispatch]);

  return (
    <PageOverview>
      <PageHeader
        title="PRODUCT MANAGEMENT"
        description="Maintain product master data, tagging and availability for all warehouses."
      />
      <Toolbar>
        <ToolbarButton disabled>Details</ToolbarButton>
        <ToolbarButton>
          Export
          <Download size={isMobile ? 16 : 12} />
        </ToolbarButton>
      </Toolbar>
      <PageContent>
        <div className={cn('w-full h-full', isMobile ? 'px-3 py-2' : 'px-6 py-3')}>
          <DataTable {...tableProps} />
        </div>
      </PageContent>
      <AppPagination />
    </PageOverview>
  );
};

export default ProductManagementPage;
