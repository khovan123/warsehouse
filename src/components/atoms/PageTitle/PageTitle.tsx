import { cn } from '../../../lib/utils';
import type { UIComponentProps } from '../../ui/type';

const PageTitle = ({ children, className }: UIComponentProps) => {
  return (
    <h1 className={cn('text-lg font-semibold tracking-wide text-foreground', className)}>
      {children}
    </h1>
  );
};

export default PageTitle;
