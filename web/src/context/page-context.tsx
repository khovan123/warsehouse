import { createContext, useContext, useState } from 'react';

import { PRODUCT_MANAGEMENT_PATH } from '@/routers/route.constants';

const functionDefault = () => undefined;

type BreadcrumbItem = { label: string; path: string };

type BreadcrumbType = {
  group?: string;
  items: BreadcrumbItem[] | [];
};

type PageContextType = {
  breadcrumb?: BreadcrumbType;
  setBreadCrumb: (page: BreadcrumbType) => void;
  clearBreadCrumb: () => void;
};

const defaultPageContextValue: PageContextType = {
  breadcrumb: {
    group: 'Management',
    items: [{ path: PRODUCT_MANAGEMENT_PATH, label: 'Product' }],
  },
  setBreadCrumb: functionDefault,
  clearBreadCrumb: functionDefault,
};

const PageContext = createContext<PageContextType>(defaultPageContextValue);

export const PageContextProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const [breadcrumbContext, setBreadCrumbContext] = useState<PageContextType['breadcrumb']>(
    defaultPageContextValue.breadcrumb
  );

  const setBreadCrumb = (page: BreadcrumbType) => {
    setBreadCrumbContext((prev) => {
      if (prev?.group !== page.group) {
        return page;
      } else {
        return {
          group: page.group,
          items: page.items,
        };
      }
    });
  };

  const clearBreadCrumb = () => {
    setBreadCrumbContext(undefined);
  };

  return (
    <PageContext.Provider value={{ breadcrumb: breadcrumbContext, setBreadCrumb, clearBreadCrumb }}>
      {children}
    </PageContext.Provider>
  );
};

export const usePageContext = () => {
  return useContext(PageContext);
};
