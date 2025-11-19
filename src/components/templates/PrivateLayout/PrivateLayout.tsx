import {
  ArrowLeftRight,
  BarChart3,
  ClipboardList,
  Coins,
  Container,
  FileText,
  Home,
  Link2,
  Medal,
  Settings2,
  Truck,
  Warehouse,
} from 'lucide-react';
import React from 'react';
import { Outlet, useLocation } from 'react-router';

import AppSideBar from '@/components/organisms/AppSideBar/AppSideBar';
import type { SideBarProps, SideBarSection } from '@/components/organisms/AppSideBar/type';
import {
  Breadcrumb,
  BreadcrumbItem,
  BreadcrumbLink,
  BreadcrumbList,
  BreadcrumbPage,
  BreadcrumbSeparator,
} from '@/components/ui/breadcrumb';
import { Separator } from '@/components/ui/separator';
import { SidebarInset, SidebarProvider, SidebarTrigger } from '@/components/ui/sidebar';
import {
  GENERATE_AVG_COSTS_PATH,
  GOODS_MOVEMENT_PATH,
  GOODS_TRANSACTION_PATH,
  INVENTORY_PATH,
  MATERIAL_TRANSACTION_REPORT_PATH,
  PARETO_PRODUCT_REPORT_PATH,
  PRODUCT_MANAGEMENT_PATH,
  PRODUCT_MOVEMENTS_REPORT_PATH,
  SETUP_PATH,
  STOCK_PATH,
  STOCK_RESERVATION_PATH,
  VALUED_STOCK_REPORT_PATH,
} from '@/routers/route.constants';

export const SIDEBAR_ITEMS: SideBarSection[] = [
  {
    id: 'management',
    name: 'Management',
    items: [{ icon: Home, path: PRODUCT_MANAGEMENT_PATH, label: 'Product' }],
  },
  {
    id: 'transaction',
    name: 'Transaction',
    items: [
      { icon: ClipboardList, label: 'Physical Inventory', path: INVENTORY_PATH },
      { icon: ArrowLeftRight, label: 'Goods Movements', path: GOODS_MOVEMENT_PATH },
      { icon: Link2, label: 'Goods Transaction', path: GOODS_TRANSACTION_PATH },
      { icon: Container, label: 'Stock Reservation', path: STOCK_RESERVATION_PATH },
      { icon: Settings2, label: 'Average Costs', path: GENERATE_AVG_COSTS_PATH },
    ],
  },
  {
    id: 'analysis',
    name: 'Analysis',
    items: [
      { icon: FileText, label: 'Material Tx Report', path: MATERIAL_TRANSACTION_REPORT_PATH },
      { icon: BarChart3, label: 'Stock Report', path: STOCK_PATH },
      { icon: Coins, label: 'Valued Stock', path: VALUED_STOCK_REPORT_PATH },
      { icon: Truck, label: 'Movements', path: PRODUCT_MOVEMENTS_REPORT_PATH },
      { icon: Medal, label: 'Pareto Report', path: PARETO_PRODUCT_REPORT_PATH },
    ],
  },
  {
    id: 'setup',
    name: 'Set up',
    items: [{ icon: Warehouse, label: 'Warehouses & Bins', path: SETUP_PATH }],
  },
];

const PrivateLayout: React.FC = () => {
  const location = useLocation();
  const sidebarProps: SideBarProps = {
    activePath: location.pathname,
    sections: SIDEBAR_ITEMS,
  };

  return (
    <SidebarProvider>
      <div className="min-h-screen flex-1 text-foreground">
        <div className="flex w-full h-full">
          <AppSideBar {...sidebarProps} />
          <SidebarInset className="h-full">
            <header className="bg-background sticky top-0 flex h-16 shrink-0 items-center gap-2 border-b px-4">
              <SidebarTrigger className="-ml-1" />
              <Separator
                orientation="vertical"
                className="mr-2 h-full data-[orientation=vertical]:h-4"
              />
              <Breadcrumb>
                <BreadcrumbList>
                  <BreadcrumbItem className="hidden md:block">
                    <BreadcrumbLink href="#">Management</BreadcrumbLink>
                  </BreadcrumbItem>
                  <BreadcrumbSeparator className="hidden md:block" />
                  <BreadcrumbItem>
                    <BreadcrumbPage>Product</BreadcrumbPage>
                  </BreadcrumbItem>
                </BreadcrumbList>
              </Breadcrumb>
            </header>
            <Outlet />
          </SidebarInset>
        </div>
      </div>
    </SidebarProvider>
  );
};

export default PrivateLayout;
