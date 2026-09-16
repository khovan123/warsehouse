import { type ColumnDef } from '@tanstack/react-table';
import React, { useEffect, useState } from 'react';
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
import { useMaterialTransactionSelector } from '@/state/ducks/material-transaction/selectors';
import { fetchMaterialTransactionRequest } from '@/state/ducks/material-transaction/slice';
import type { MaterialTransaction } from '@/state/ducks/material-transaction/type';
import { useSetupsSelector } from '@/state/ducks/setups-warehouse/selectors';
import { fetchSetupsWarehouseRequest } from '@/state/ducks/setups-warehouse/slice';
import type { Warehouse } from '@/state/ducks/setups-warehouse/type';
import type { InventoryType } from '@/utils/constants';
import { INVENTORY_TYPE_OPTIONS } from '@/utils/constants';

const columns: ColumnDef<MaterialTransaction>[] = [
  {
    accessorKey: 'line',
    header: 'Line',
  },
  {
    accessorKey: 'document',
    header: 'Document',
  },
  {
    accessorKey: 'type',
    header: 'Type',
  },
  {
    accessorKey: 'movementDate',
    header: 'Movement Date',
  },
  {
    accessorKey: 'product',
    header: 'Product',
  },
  {
    id: 'warehouseBin',
    header: 'Warehouse / Bin',
    cell: ({ row }) => (
      <div>
        {row.original.fromWarehouse} / {row.original.bin}
      </div>
    ),
  },
  {
    accessorKey: 'qty',
    header: () => <div className="text-right">Qty</div>,
    cell: ({ row }) => <div className="text-right">{row.getValue('qty')}</div>,
  },
  {
    accessorKey: 'uom',
    header: 'UOM',
  },
  {
    accessorKey: 'cost',
    header: () => <div className="text-right">Cost</div>,
    cell: ({ row }) => (
      <div className="text-right">{row.getValue<number>('cost').toFixed(2)} €</div>
    ),
  },
  {
    accessorKey: 'businessPartner',
    header: 'Business Partner',
  },
];

const MaterialTransactionReportPage: React.FC = () => {
  const isMobile = useIsMobile();
  const dispatch = useDispatch();
  const { data, loading } = useMaterialTransactionSelector();
  const { data: setupsData } = useSetupsSelector();
  const [filtering, setFiltering] = useState<{ inventoryType: InventoryType; warehouseId: string }>(
    {
      inventoryType: INVENTORY_TYPE_OPTIONS[0].value as InventoryType,
      warehouseId: '-1',
    }
  );

  useEffect(() => {
    dispatch(fetchSetupsWarehouseRequest());
  }, [dispatch]);

  useEffect(() => {
    dispatch(fetchMaterialTransactionRequest({ ...filtering }));
  }, [dispatch, filtering]);

  const tableProps: DataTableProps<MaterialTransaction, unknown> = {
    columns,
    data: data.materialTransactions,
  };

  return (
    <PageOverview>
      <PageHeader
        title="MATERIAL TRANSACTION REPORT"
        description="Lists all documents (shipments or receipts) grouped by Business Partner, aligning with Openbravo's analysis menu."
      />
      <Toolbar>
        <FilterSelect
          defaultValue={INVENTORY_TYPE_OPTIONS[0].value.toString()}
          onValueChange={(value) => {
            setFiltering((prev) => ({ ...prev, inventoryType: value as unknown as InventoryType }));
          }}
          placeholder={INVENTORY_TYPE_OPTIONS[0].label}
          triggerClassName={cn(isMobile ? 'w-full' : 'w-48')}
          options={INVENTORY_TYPE_OPTIONS}
        />
        <FilterSelect
          defaultValue="-1"
          onValueChange={(value) => {
            setFiltering((prev) => ({ ...prev, warehouseId: value }));
          }}
          placeholder="All warehouses"
          triggerClassName={cn(isMobile ? 'w-full' : 'w-40')}
          options={[
            { value: '-1', label: 'All warehouses' },
            ...(setupsData.warehouses?.map((warehouse: Warehouse) => ({
              value: warehouse.id,
              label: warehouse.name,
            })) || []),
          ]}
        />
        <ToolbarInput type="date" defaultValue="2025-11-15" />
        <ToolbarInput type="date" defaultValue="2025-11-16" />
        <ToolbarInput type="text" placeholder="Search by criteria" />
      </Toolbar>
      <PageContent>
        <div className={cn(isMobile ? 'px-3 py-2' : 'px-6 py-3')}>
          <DataTable {...tableProps} isLoadingData={loading} />
        </div>
      </PageContent>
      <AppPagination />
    </PageOverview>
  );
};

export default MaterialTransactionReportPage;
