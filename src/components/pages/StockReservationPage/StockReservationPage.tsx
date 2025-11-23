import { type ColumnDef } from '@tanstack/react-table';
import { Plus } from 'lucide-react';
import React from 'react';

import FilterSelect from '@/components/molecules/FilterSelect/FilterSelect';
import PageContent from '@/components/molecules/PageContent/PageContent';
import PageHeader from '@/components/molecules/PageHeader/PageHeader';
import PageOverview from '@/components/organisms/PageOverview/PageOverview';
import { Badge } from '@/components/ui/badge';
import { DataTable } from '@/components/ui/data-table';
import { Toolbar, ToolbarButton, ToolbarInput } from '@/components/ui/toolbar';
import type { DataTableProps } from '@/components/ui/type';
import { useIsMobile } from '@/hooks/use-mobile';
import { cn } from '@/lib/utils';

type Reservation = {
  reservationNo: string;
  product: string;
  warehouse: string;
  reservedQty: number;
  uom: string;
  promisedDate: string;
  status: 'Reserved' | 'Draft' | 'Closed';
  orderRef: string;
};

const reservations: Reservation[] = [
  {
    reservationNo: 'SR-240020',
    product: 'SKU-1001 Cotton T-Shirt Blue M',
    warehouse: 'Main DC',
    reservedQty: 40,
    uom: 'Unit',
    promisedDate: '2025-11-20',
    status: 'Reserved',
    orderRef: 'SO-240110',
  },
  {
    reservationNo: 'SR-240021',
    product: 'SKU-2001 Running Shoes 42',
    warehouse: 'Main DC',
    reservedQty: 10,
    uom: 'Pair',
    promisedDate: '2025-11-22',
    status: 'Draft',
    orderRef: 'SO-240111',
  },
];

const columns: ColumnDef<Reservation>[] = [
  {
    accessorKey: 'reservationNo',
    header: 'Reservation No.',
  },
  {
    accessorKey: 'product',
    header: 'Product',
  },
  {
    accessorKey: 'warehouse',
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
    header: 'Promised Date',
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

  const tableProps: DataTableProps<Reservation, unknown> = {
    columns,
    data: reservations,
  };

  const Footer = () => (
    <div
      className={cn(
        'border-t border-border bg-card text-[11px] flex',
        isMobile ? 'px-3 py-2 flex-col gap-2' : 'px-6 py-2 justify-between'
      )}
    >
      <span>
        1 - {reservations.length} of {reservations.length} reservations
      </span>
      <span>Items per page: 100</span>
    </div>
  );

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
          <DataTable {...tableProps} />
        </div>
      </PageContent>
      <Footer />
    </PageOverview>
  );
};

export default StockReservationPage;
