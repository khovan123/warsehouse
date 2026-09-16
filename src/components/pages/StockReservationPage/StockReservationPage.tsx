import { type ColumnDef } from '@tanstack/react-table';
import { Plus } from 'lucide-react';
import React from 'react';

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
  const tableProps: DataTableProps<Reservation, unknown> = {
    columns,
    data: reservations,
  };

  const toolbar = (
    <>
      <Button size={'sm'}>
        <Plus /> New Reservation
      </Button>

      <Select>
        <SelectTrigger className="w-32 text-xs rounded-full h-8 border border-border">
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
        className="rounded-full flex-1 min-w-40 focus:outline-none h-8"
      />
    </>
  );

  const footer = (
    <div className="border-t border-border bg-card px-6 py-2 text-[11px] flex justify-between">
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
      <div className="px-6 py-3">
        <DataTable {...tableProps} />
      </div>
    </PageLayout>
  );
};

export default StockReservationPage;
