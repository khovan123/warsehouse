import React from 'react';

import PrivateLayout from '@/components/templates/PrivateLayout/PrivateLayout';

import {
  GENERATE_AVG_COSTS_PATH,
  GOODS_MOVEMENT_PATH,
  GOODS_TRANSACTION_PATH,
  INVENTORY_PATH,
  LANDING_PATH,
  LOGIN_PATH,
  MATERIAL_TRANSACTION_REPORT_PATH,
  PARETO_PRODUCT_REPORT_PATH,
  PRODUCT_MANAGEMENT_PATH,
  PRODUCT_MOVEMENTS_REPORT_PATH,
  SETUP_PATH,
  STOCK_PATH,
  STOCK_RESERVATION_PATH,
  VALUED_STOCK_REPORT_PATH,
} from './route.constants';
import type { PrivateRouteConfig, RouteConfig } from './type';

export const LoginPage = React.lazy(() =>
  import('../components/pages/LoginPage').then(({ LoginPage }) => ({
    default: LoginPage,
  }))
);
export const LandingPage = React.lazy(() =>
  import('../components/pages/LandingPage').then(({ LandingPage }) => ({
    default: LandingPage,
  }))
);
export const TopPage = React.lazy(() =>
  import('../components/pages/TopPage').then(({ TopPage }) => ({
    default: TopPage,
  }))
);

export const StockReportPage = React.lazy(() =>
  import('../components/pages/StockReportPage').then(({ StockReportPage }) => ({
    default: StockReportPage,
  }))
);

export const MaterialTransactionReportPage = React.lazy(() =>
  import('../components/pages/MaterialTransactionReportPage').then(
    ({ MaterialTransactionReportPage }) => ({
      default: MaterialTransactionReportPage,
    })
  )
);

export const ProductMovementsReportPage = React.lazy(() =>
  import('../components/pages/ProductMovementsReportPage').then(
    ({ ProductMovementsReportPage }) => ({
      default: ProductMovementsReportPage,
    })
  )
);

export const ValuedStockReportPage = React.lazy(() =>
  import('../components/pages/ValuedStockReportPage').then(({ ValuedStockReportPage }) => ({
    default: ValuedStockReportPage,
  }))
);

export const ParetoProductReportPage = React.lazy(() =>
  import('../components/pages/ParetoProductReportPage').then(({ ParetoProductReportPage }) => ({
    default: ParetoProductReportPage,
  }))
);

export const PhysicalInventoryPage = React.lazy(() =>
  import('../components/pages/PhysicalInventoryPage').then(({ PhysicalInventoryPage }) => ({
    default: PhysicalInventoryPage,
  }))
);

export const GoodsMovementPage = React.lazy(() =>
  import('../components/pages/GoodsMovementPage').then(({ GoodsMovementPage }) => ({
    default: GoodsMovementPage,
  }))
);

export const GoodsTransactionPage = React.lazy(() =>
  import('../components/pages/GoodsTransactionPage').then(({ GoodsTransactionPage }) => ({
    default: GoodsTransactionPage,
  }))
);

export const StockReservationPage = React.lazy(() =>
  import('../components/pages/StockReservationPage').then(({ StockReservationPage }) => ({
    default: StockReservationPage,
  }))
);

export const GenerateAverageCostsPage = React.lazy(() =>
  import('../components/pages/GenerateAverageCostsPage').then(({ GenerateAverageCostsPage }) => ({
    default: GenerateAverageCostsPage,
  }))
);

export const WarehousesSetupPage = React.lazy(() =>
  import('../components/pages/WarehousesSetupPage').then(({ WarehousesSetupPage }) => ({
    default: WarehousesSetupPage,
  }))
);

export const ProductManagementPage = React.lazy(() =>
  import('../components/pages/ProductManagementPage').then(({ ProductManagementPage }) => ({
    default: ProductManagementPage,
  }))
);

export const PUBLIC_ROUTES: RouteConfig[] = [
  {
    path: LANDING_PATH,
    component: LandingPage,
  },
  {
    path: LOGIN_PATH,
    component: LoginPage,
  },
];

export const PRIVATE_ROUTES: PrivateRouteConfig[] = [
  {
    id: 'private-layout',
    template: PrivateLayout,
    routes: [
      {
        path: PRODUCT_MANAGEMENT_PATH,
        component: ProductManagementPage,
      },
      {
        path: STOCK_PATH,
        component: StockReportPage,
      },
      {
        path: MATERIAL_TRANSACTION_REPORT_PATH,
        component: MaterialTransactionReportPage,
      },
      {
        path: PRODUCT_MOVEMENTS_REPORT_PATH,
        component: ProductMovementsReportPage,
      },
      {
        path: VALUED_STOCK_REPORT_PATH,
        component: ValuedStockReportPage,
      },
      {
        path: PARETO_PRODUCT_REPORT_PATH,
        component: ParetoProductReportPage,
      },
      {
        path: SETUP_PATH,
        component: WarehousesSetupPage,
      },
      {
        path: INVENTORY_PATH,
        component: PhysicalInventoryPage,
      },
      {
        path: GOODS_MOVEMENT_PATH,
        component: GoodsMovementPage,
      },
      {
        path: GOODS_TRANSACTION_PATH,
        component: GoodsTransactionPage,
      },
      {
        path: STOCK_RESERVATION_PATH,
        component: StockReservationPage,
      },
      {
        path: GENERATE_AVG_COSTS_PATH,
        component: GenerateAverageCostsPage,
      },
    ],
  },
];
