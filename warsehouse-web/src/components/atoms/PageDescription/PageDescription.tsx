import type { UIComponentProps } from '@/components/ui/type';
import { cn } from '@/lib/utils';

const PageDescription = ({ children, className }: UIComponentProps) => {
  return <p className={cn('text-[11px] mt-1 leading-relaxed', className)}>{children}</p>;
};

export default PageDescription;
