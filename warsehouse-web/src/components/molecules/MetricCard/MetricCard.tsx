import type { ReactNode } from 'react';

import { cn } from '@/lib/utils';

type MetricCardProps = {
  label?: ReactNode;
  value?: ReactNode;
  description?: ReactNode;
  className?: string;
  labelClassName?: string;
  valueClassName?: string;
  descriptionClassName?: string;
  children?: ReactNode;
};

const MetricCard = ({
  label,
  value,
  description,
  className,
  labelClassName,
  valueClassName,
  descriptionClassName,
  children,
}: MetricCardProps) => {
  return (
    <div className={cn('rounded-xl border border-border bg-card px-4 py-3 text-xs', className)}>
      {label ? (
        <p
          className={cn(
            'text-muted-foreground uppercase tracking-wide text-[10px]',
            labelClassName
          )}
        >
          {label}
        </p>
      ) : null}
      {value ? <p className={cn('mt-2 text-lg font-semibold', valueClassName)}>{value}</p> : null}
      {description ? (
        <p className={cn('text-[11px] text-muted-foreground', descriptionClassName)}>
          {description}
        </p>
      ) : null}
      {children}
    </div>
  );
};

export default MetricCard;
