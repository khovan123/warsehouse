import { cn } from '../../../lib/utils';
import type { UIComponentProps } from '../../ui/type';

const PageToolbar = ({ children, className }: UIComponentProps) => {
  return (
    <div
      className={cn(
        'px-6 py-2 border-b border-border bg-card flex flex-wrap items-center gap-2 text-xs text-foreground',
        className
      )}
    >
      {children}
    </div>
  );
};

export default PageToolbar;
