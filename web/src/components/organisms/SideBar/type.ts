import type { LucideIcon } from 'lucide-react';

export type SideBarItem = {
  icon: LucideIcon;
  label: string;
  path: string;
};

export type SideBarSection = {
  id: string;
  items: SideBarItem[];
};

export type SideBarProps = {
  homePath: string;
  sections: SideBarSection[];
  activePath: string;
};
