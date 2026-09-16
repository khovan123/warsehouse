import { type ColumnDef } from '@tanstack/react-table';
import { Plus } from 'lucide-react';
import React, { useEffect } from 'react';
import { useDispatch } from 'react-redux';

import FilterSelect from '@/components/molecules/FilterSelect/FilterSelect';
import PageContent from '@/components/molecules/PageContent/PageContent';
import PageHeader from '@/components/molecules/PageHeader/PageHeader';
import AppPagination from '@/components/organisms/AppPagination/AppPagination';
import PageOverview from '@/components/organisms/PageOverview/PageOverview';
import { Badge } from '@/components/ui/badge';
import { DataTable } from '@/components/ui/data-table';
import { Toolbar, ToolbarButton, ToolbarInput } from '@/components/ui/toolbar';
import type { DataTableProps } from '@/components/ui/type';
import { useIsMobile } from '@/hooks/use-mobile';
import { cn } from '@/lib/utils';
import { useReservationSelector } from '@/state/ducks/reservation/selectors';
import { fetchReservationRequest } from '@/state/ducks/reservation/slice';
import type { Reservation } from '@/state/ducks/reservation/type';

const columns: ColumnDef<Reservation>[] = [
  {
    accessorKey: 'reservationNo',
    header: 'Reservation No.',
  },
  {
    accessorKey: 'productName',
    header: 'Product',
  },
  {
    accessorKey: 'warehouseName',
    header: 'Warehouse',
  },
  {
    accessorKey: 'reservedQty',
    header: () => <div className="text-right">Reserved Qty</div>,
    cell: ({ row }) => <div className="text-right">{row.getValue('reservedQty')}</div>,
  },
  {
    accessorKey: 'uom',
    header: 'UOM',
  },
  {
    accessorKey: 'promisedDate',
    header: 'Date',
  },
  {
    accessorKey: 'orderRef',
    header: 'Order Ref.',
  },
  {
    accessorKey: 'status',
    header: 'Status',
    cell: ({ row }) => {
      const status = row.getValue<'Reserved' | 'Draft' | 'Closed'>('status');
      return <Badge variant={status === 'Reserved' ? 'default' : 'outline'}>{status}</Badge>;
    },
  },
];

const StockReservationPage: React.FC = () => {
  const isMobile = useIsMobile();
  const dispatch = useDispatch();
  const reservationSelector = useReservationSelector();

  useEffect(() => {
    dispatch(fetchReservationRequest());
  }, [dispatch]);

  const tableProps: DataTableProps<Reservation, unknown> = {
    columns,
    data: reservationSelector.data.reservations,
  };

  return (
    <PageOverview>
      <PageHeader
        title="STOCK RESERVATION"
        description="Reserve stock for sales or production orders following the Openbravo Stock Reservation workflow."
      />
      <Toolbar>
        <ToolbarButton>
          <Plus size={isMobile ? 16 : 12} /> New Reservation
        </ToolbarButton>

        <FilterSelect
          defaultValue="All"
          placeholder="All"
          triggerClassName={cn(isMobile ? 'w-full' : 'w-36')}
          options={[
            { value: 'All', label: 'All' },
            { value: 'Draft', label: 'Draft' },
            { value: 'Reserved', label: 'Reserved' },
            { value: 'Closed', label: 'Closed' },
          ]}
        />

        <ToolbarInput type="text" placeholder="Search by criteria" />
      </Toolbar>
      <PageContent>
        <div className={cn(isMobile ? 'px-3 py-2' : 'px-6 py-3')}>
          <DataTable {...tableProps} isLoadingData={reservationSelector.loading} />
        </div>
      </PageContent>
      <AppPagination />
    </PageOverview>
  );
};

export default StockReservationPage;
