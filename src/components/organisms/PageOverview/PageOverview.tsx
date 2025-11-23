import type { ReactNode } from 'react';

import type { PageHeaderProps } from '@/components/molecules/PageHeader/type';
import { cn } from '@/lib/utils';

type PageOverviewProps = PageHeaderProps & {
  children: ReactNode;
  className?: string;
};

const PageOverview = ({ className, children }: PageOverviewProps) => {
  return (
    <div className={cn('min-h-full flex flex-col text-foreground', className)}>{children}</div>
  );
};

export default PageOverview;
