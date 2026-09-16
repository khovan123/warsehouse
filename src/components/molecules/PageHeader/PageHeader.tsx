import PageDescription from '@/components/atoms/PageDescription/PageDescription';
import PageTitle from '@/components/atoms/PageTitle/PageTitle';
import { cn } from '@/lib/utils';

import type { PageHeaderProps } from './type';

const PageHeader = ({ title, description, rightSlot, meta, className }: PageHeaderProps) => {
  return (
    <div
      className={cn(
        'px-6 pt-4 pb-2 border-b border-border bg-background text-foreground',
        className
      )}
    >
      <div className="flex flex-col gap-3 sm:flex-row sm:items-center sm:justify-between">
        <div>
          <PageTitle>{title}</PageTitle>
          {description ? <PageDescription>{description}</PageDescription> : null}
        </div>
        {rightSlot ? <div className="shrink-0">{rightSlot}</div> : null}
      </div>
      {meta ? <div className="mt-3">{meta}</div> : null}
    </div>
  );
};

export default PageHeader;
