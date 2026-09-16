import {
  ArrowLeftRight,
  BarChart3,
  ClipboardList,
  Coins,
  Container,
  FileText,
  Link2,
  Medal,
  Settings2,
  Truck,
  Warehouse,
} from 'lucide-react';
import React from 'react';
import { Outlet, useLocation } from 'react-router';

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
} from '../../../routers/route.constants';
import Nav from '../../organisms/Nav/Nav';
import SideBar from '../../organisms/SideBar/SideBar';
import type { SideBarItem } from '../../organisms/SideBar/type';

const TRANSACTION_ITEMS: SideBarItem[] = [
  { icon: ClipboardList, label: 'Physical Inventory', path: INVENTORY_PATH },
  { icon: ArrowLeftRight, label: 'Goods Movements', path: GOODS_MOVEMENT_PATH },
  { icon: Link2, label: 'Goods Transaction', path: GOODS_TRANSACTION_PATH },
  { icon: Container, label: 'Stock Reservation', path: STOCK_RESERVATION_PATH },
  { icon: Settings2, label: 'Average Costs', path: GENERATE_AVG_COSTS_PATH },
];

const ANALYSIS_ITEMS: SideBarItem[] = [
  { icon: FileText, label: 'Material Tx Report', path: MATERIAL_TRANSACTION_REPORT_PATH },
  { icon: BarChart3, label: 'Stock Report', path: STOCK_PATH },
  { icon: Coins, label: 'Valued Stock', path: VALUED_STOCK_REPORT_PATH },
  { icon: Truck, label: 'Movements', path: PRODUCT_MOVEMENTS_REPORT_PATH },
  { icon: Medal, label: 'Pareto Report', path: PARETO_PRODUCT_REPORT_PATH },
];

const SETUP_ITEMS: SideBarItem[] = [
  { icon: Warehouse, label: 'Warehouses & Bins', path: SETUP_PATH },
];

const PrivateLayout: React.FC = () => {
  const location = useLocation();
  return (
    <div className="min-h-screen flex bg-background text-foreground">
      <SideBar
        homePath={PRODUCT_MANAGEMENT_PATH}
        activePath={location.pathname}
        sections={[
          { id: 'transactions', items: TRANSACTION_ITEMS },
          { id: 'analysis', items: ANALYSIS_ITEMS },
          { id: 'setup', items: SETUP_ITEMS },
        ]}
      />

      <div className="flex-1 flex flex-col">
        <Nav />
        {/* <header className="h-12 md:h-14 flex items-center justify-between px-4 border-b border-border bg-card">
          <div className="flex items-center gap-2">
            <span className="text-lg font-semibold text-primary">OpenWMS</span>
            <span className="text-[11px] text-muted hidden sm:inline">Warehouse Management</span>
          </div>

          <div className="flex items-center gap-4 text-[11px]">
            <div className="hidden sm:flex items-center gap-2 text-muted">
              <span>Last update at 14:43:50</span>
              <button
                type="button"
                className="inline-flex items-center gap-1 text-primary hover:text-primary/80"
              >
                Auto Refresh
                <span className="inline-flex h-4 w-7 items-center rounded-full bg-border">
                  <span className="h-3 w-3 rounded-full bg-card ml-0.5" />
                </span>
              </button>
            </div>

            <div className="flex items-center gap-2">
              <div className="h-8 w-8 rounded-full bg-primary/20 flex items-center justify-center">
                <span className="text-xs font-bold text-primary-foreground">OP</span>
              </div>
              <button type="button" className="text-xs font-medium hover:text-primary">
                Operator ▾
              </button>
            </div>
          </div>
        </header> */}

        <main className="flex-1 overflow-auto bg-background">
          <Outlet />
        </main>
      </div>
    </div>
  );
};

export default PrivateLayout;
