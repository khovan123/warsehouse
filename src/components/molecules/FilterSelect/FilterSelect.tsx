import type { ComponentProps } from 'react';

import { cn } from '../../../lib/utils';
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '../../ui/select';

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
  return (
    <Select defaultValue={defaultValue} value={value} onValueChange={onValueChange}>
      <SelectTrigger
        className={cn(
          'w-40 rounded-full h-8 border border-border text-xs bg-background',
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
