import type { LucideIcon } from 'lucide-react';

import type { Sidebar } from '@/components/ui/sidebar';

export type SideBarItem = {
  icon: LucideIcon;
  label: string;
  path: string;
};

export type SideBarSection = {
  id: string;
  name: string;
  items: SideBarItem[];
};

export type SideBarProps = React.ComponentProps<typeof Sidebar> & {
  sections: SideBarSection[];
  activePath: string;
  iconSize?: number;
};
