import type { ReactNode } from 'react';
import { useSelector } from 'react-redux';

import PageHeader from '@/components/molecules/PageHeader/PageHeader';
import type { PageHeaderProps } from '@/components/molecules/PageHeader/type';
import PageToolbar from '@/components/molecules/PageToolbar/PageToolbar';
import { useIsMobile } from '@/hooks/use-mobile';
import { cn } from '@/lib/utils';
import type { GlobalState } from '@/state/store';

type PageLayoutProps = PageHeaderProps & {
  children: ReactNode;
  toolbar?: ReactNode;
  toolbarClassName?: string;
  footer?: ReactNode;
  contentClassName?: string;
};

const PageLayout = ({
  children,
  toolbar,
  toolbarClassName,
  footer,
  contentClassName,
  ...headerProps
}: PageLayoutProps) => {
  const authSelector = useSelector((state: GlobalState) => state.auth);
  const isMobile = useIsMobile();

  return authSelector.logined ? (
    <div className="min-h-full flex flex-col bg-background text-foreground">
      <PageHeader {...headerProps} />
      {toolbar ? <PageToolbar className={toolbarClassName}>{toolbar}</PageToolbar> : null}
      <div className={cn('flex-1 overflow-auto bg-background', contentClassName)}>{children}</div>
      {footer}
    </div>
  ) : (
    <div className={cn('bg-background mx-auto', isMobile ? 'w-full px-4' : 'max-w-6xl')}>
      {children}
    </div>
  );
};

export default PageLayout;
