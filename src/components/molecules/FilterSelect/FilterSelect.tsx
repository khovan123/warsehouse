import type { ComponentProps } from 'react';

import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from '@/components/ui/select';
import { useIsMobile } from '@/hooks/use-mobile';
import { cn } from '@/lib/utils';

type FilterOption = {
  label: string;
  value: string;
  disabled?: boolean;
};

type FilterSelectProps = {
  options: FilterOption[];
  placeholder?: string;
  defaultValue?: string;
  value?: string;
  onValueChange?: ComponentProps<typeof Select>['onValueChange'];
  triggerClassName?: string;
  contentClassName?: string;
};

const FilterSelect = ({
  options,
  placeholder,
  defaultValue,
  value,
  onValueChange,
  triggerClassName,
  contentClassName,
}: FilterSelectProps) => {
  const isMobile = useIsMobile();

  return (
    <Select defaultValue={defaultValue} value={value} onValueChange={onValueChange}>
      <SelectTrigger
        className={cn(
          'w-40 rounded-full h-8 border border-border text-xs bg-background',
          isMobile ? 'w-full' : 'w-48',
          triggerClassName
        )}
      >
        <SelectValue placeholder={placeholder ?? options[0]?.label} />
      </SelectTrigger>
      <SelectContent align="start" className={cn('text-xs', contentClassName)}>
        {options.map((option) => (
          <SelectItem
            key={option.value}
            value={option.value}
            disabled={option.disabled}
            className="text-xs"
          >
            {option.label}
          </SelectItem>
        ))}
      </SelectContent>
    </Select>
  );
};

export default FilterSelect;
