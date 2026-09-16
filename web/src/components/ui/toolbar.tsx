import React from 'react';

import type { UIComponentProps } from '@/components/ui/type';
import { useIsMobile } from '@/hooks/use-mobile';
import { cn } from '@/lib/utils';

import { Button, type ButtonProps } from './button';
import { Input } from './input';

const ToolbarInput = ({ className, ...props }: React.ComponentProps<'input'>) => {
  const isMobile = useIsMobile();

  return (
    <Input
      {...props}
      className={cn(
        'rounded-full text-xs focus:outline-none h-8 placeholder:text-xs',
        isMobile ? 'w-full' : 'flex-1 min-w-40',
        className
      )}
    />
  );
};

const ToolbarButton = React.forwardRef<HTMLButtonElement, ButtonProps>(
  ({ className, variant = 'outline', size, children, disabled, ...props }, ref) => {
    const isMobile = useIsMobile();

    return (
      <Button
        {...props}
        disabled={disabled}
        variant={variant}
        size={size ?? (isMobile ? 'default' : 'sm')}
        className={cn('rounded-md', disabled ? 'cursor-not-allowed' : 'cursor-pointer', className)}
        ref={ref}
      >
        {children}
      </Button>
    );
  }
);

const Toolbar = ({ children, className }: UIComponentProps) => {
  const isMobile = useIsMobile();

  return (
    <div
      className={cn(
        'border-b border-border bg-card flex flex-wrap items-center gap-2 text-xs text-foreground',
        isMobile ? 'px-3 py-2 flex flex-col w-full' : 'px-6 py-2 flex flex-wrap items-center',
        className
      )}
    >
      {children}
    </div>
  );
};

export { Toolbar, ToolbarButton, ToolbarInput };
