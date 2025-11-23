import type { ReactNode } from 'react';

import type { PageHeaderProps } from '@/components/molecules/PageHeader/type';
import { cn } from '@/lib/utils';

type PageContentProps = PageHeaderProps & {
  children: ReactNode;
  className?: string;
};

const PageContent = ({ className, children }: PageContentProps) => {
  return <div className={cn('flex-1 overflow-auto bg-background', className)}>{children}</div>;
};

export default PageContent;
