import { type ColumnDef } from '@tanstack/react-table';
import { Plus } from 'lucide-react';
import React from 'react';

import { useIsMobile } from '../../../hooks/use-mobile';
import { cn } from '../../../lib/utils';
import PageLayout from '../../organisms/PageLayout/PageLayout';
import { Badge } from '../../ui/badge';
import { Button } from '../../ui/button';
import { DataTable } from '../../ui/data-table';
import { Input } from '../../ui/input';
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '../../ui/select';
import type { DataTableProps } from '../../ui/type';

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

  const toolbar = (
    <div
      className={cn(isMobile ? 'flex flex-col gap-2 w-full' : 'flex flex-wrap items-center gap-2')}
    >
      <Button size={isMobile ? 'default' : 'sm'}>
        <Plus className={isMobile ? 'mr-2 h-4 w-4' : 'mr-1 h-3 w-3'} /> New Reservation
      </Button>

      <Select>
        <SelectTrigger
          className={cn(
            'text-xs rounded-full h-8 border border-border',
            isMobile ? 'w-full' : 'w-32'
          )}
        >
          <SelectValue placeholder="All" />
        </SelectTrigger>
        <SelectContent className="w-fit border border-border">
          <SelectItem className="text-xs" value="All">
            All
          </SelectItem>
          <SelectItem className="text-xs" value="Draft">
            Draft
          </SelectItem>
          <SelectItem className="text-xs" value="Reserved">
            Reserved
          </SelectItem>
          <SelectItem className="text-xs" value="Closed">
            Closed
          </SelectItem>
        </SelectContent>
      </Select>

      <Input
        type="text"
        placeholder="Search by reservation, product, order..."
        className={cn(
          'rounded-full focus:outline-none h-8',
          isMobile ? 'w-full' : 'flex-1 min-w-40'
        )}
      />
    </div>
  );

  const footer = (
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
    <PageLayout
      title="STOCK RESERVATION"
      description="Reserve stock for sales or production orders following the Openbravo Stock Reservation workflow."
      toolbar={toolbar}
      footer={footer}
    >
      <div className={cn(isMobile ? 'px-3 py-2' : 'px-6 py-3')}>
        <DataTable {...tableProps} />
      </div>
    </PageLayout>
  );
};

export default StockReservationPage;
