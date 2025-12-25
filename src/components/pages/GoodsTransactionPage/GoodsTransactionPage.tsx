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
import { useGoodTransactionSelector } from '@/state/ducks/good-transaction/selectors';
import { fetchGoodTransactionRequest } from '@/state/ducks/good-transaction/slice';
import { flatGoodTransactionData, type FlattedGoodTransaction } from '@/utils/helpers/data-helper';

const columns: ColumnDef<FlattedGoodTransaction>[] = [
  {
    accessorKey: 'docNo',
    header: 'Document No.',
  },
  {
    accessorKey: 'warehouseId',
    header: 'Warehouse',
  },
  {
    accessorKey: 'productId',
    header: 'Product',
  },
  {
    accessorKey: 'binId',
    header: 'Bin',
  },
  {
    accessorKey: 'countedQty',
    header: () => <div className="text-right">Acctual Qty</div>,
    cell: ({ row }) => <div className="text-right">{row.getValue('countedQty')}</div>,
  },
  {
    accessorKey: 'expectedQty',
    header: () => <div className="text-right">Expected Qty</div>,
    cell: ({ row }) => <div className="text-right">{row.getValue('expectedQty')}</div>,
  },
  {
    accessorKey: 'uom',
    header: 'UOM',
  },
  {
    accessorKey: 'countDate',
    header: 'Date',
  },
  {
    accessorKey: 'createdBy',
    header: 'Business Partner',
  },
];

const GoodsTransactionPage: React.FC = () => {
  const isMobile = useIsMobile();
  const dispatch = useDispatch();
  const goodTransactionSelector = useGoodTransactionSelector();

  useEffect(() => {
    dispatch(fetchGoodTransactionRequest());
  }, [dispatch]);

  const tableProps: DataTableProps<FlattedGoodTransaction, unknown> = {
    columns,
    data: flatGoodTransactionData(goodTransactionSelector.data.goodTransactions),
  };

  return (
    <PageOverview>
      <PageHeader
        title="GOODS TRANSACTION"
        description="Read-only view of all inventory transactions with filtering capabilities."
      />
      <Toolbar>
        <FilterSelect
          defaultValue="All transaction types"
          placeholder="All transaction types"
          options={[
            { value: 'All transaction types', label: 'All transaction types' },
            { value: 'Receipt', label: 'Receipt' },
            { value: 'Shipment', label: 'Shipment' },
            { value: 'Movement', label: 'Movement' },
            { value: 'Inventory', label: 'Inventory' },
          ]}
        />

        <FilterSelect
          defaultValue="All warehouses"
          placeholder="All warehouses"
          options={[
            { value: 'All warehouses', label: 'All warehouses' },
            { value: 'Main DC', label: 'Main DC' },
            { value: 'Store 01', label: 'Store 01' },
          ]}
        />
        <ToolbarInput type="text" placeholder="Search by criteria" />
      </Toolbar>
      <PageContent>
        <div className={cn(isMobile ? 'px-3 py-2' : 'px-6 py-3')}>
          <DataTable {...tableProps} isLoadingData={goodTransactionSelector.loading} />
        </div>
      </PageContent>
      <AppPagination />
    </PageOverview>
  );
};

export default GoodsTransactionPage;
