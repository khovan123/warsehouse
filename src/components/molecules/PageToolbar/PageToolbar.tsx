import { useIsMobile } from '../../../hooks/use-mobile';
import { cn } from '../../../lib/utils';
import type { UIComponentProps } from '../../ui/type';

const PageToolbar = ({ children, className }: UIComponentProps) => {
  const isMobile = useIsMobile();

  return (
    <div
      className={cn(
        'border-b border-border bg-card flex flex-wrap items-center gap-2 text-xs text-foreground',
        isMobile ? 'px-3 py-2' : 'px-6 py-2',
        className
      )}
    >
      {children}
    </div>
  );
};

export default PageToolbar;
