import { ChevronRight } from 'lucide-react';
import { Link } from 'react-router';

import NavUser from '@/components/organisms/NavUser/NavUser';
import { SearchForm } from '@/components/organisms/SearchForm/SearchForm';
import { Collapsible, CollapsibleContent, CollapsibleTrigger } from '@/components/ui/collapsible';
import {
  Sidebar,
  SidebarContent,
  SidebarFooter,
  SidebarGroup,
  SidebarGroupContent,
  SidebarGroupLabel,
  SidebarHeader,
  SidebarMenu,
  SidebarMenuButton,
  SidebarMenuItem,
  SidebarRail,
} from '@/components/ui/sidebar';
import { cn } from '@/lib/utils';
import { isActivePath } from '@/utils/navigation';

import type { SideBarProps } from './type';

const AppSideBar = ({ activePath, sections, iconSize = 4, ...props }: SideBarProps) => {
  return (
    <Sidebar {...props}>
      <SidebarHeader className="p-0">
        <div className="bg-primary px-2 h-16 flex items-center justify-center">
          <p>
            <span className="text-lg font-semibold text-primary-foreground">OpenWMS</span>
            <span className="text-[11px] hidden sm:inline">Warehouse Management</span>
          </p>
        </div>
        <SearchForm />
      </SidebarHeader>
      <SidebarContent className="gap-0">
        {sections.map(({ id, items, name }) => (
          <Collapsible key={id} title={name} defaultOpen className="group/collapsible">
            <SidebarGroup>
              <SidebarGroupLabel
                asChild
                className="group/label text-sm text-sidebar-foreground hover:bg-sidebar-accent hover:text-sidebar-accent-foreground"
              >
                <CollapsibleTrigger>
                  {name}
                  <ChevronRight className="ml-auto transition-transform group-data-[state=open]/collapsible:rotate-90" />
                </CollapsibleTrigger>
              </SidebarGroupLabel>
              <CollapsibleContent>
                <SidebarGroupContent>
                  <SidebarMenu>
                    {items.map(({ icon, path, label }) => {
                      const Icon = icon;
                      return (
                        <SidebarMenuItem key={path}>
                          <SidebarMenuButton
                            asChild
                            isActive={isActivePath(activePath, path)}
                            className={cn(
                              isActivePath(activePath, path)
                                ? 'text-primary!'
                                : 'hover:text-primary'
                            )}
                          >
                            <Link to={path}>
                              <Icon size={iconSize} />
                              {label}
                            </Link>
                          </SidebarMenuButton>
                        </SidebarMenuItem>
                      );
                    })}
                  </SidebarMenu>
                </SidebarGroupContent>
              </CollapsibleContent>
            </SidebarGroup>
          </Collapsible>
        ))}
      </SidebarContent>
      <SidebarFooter>
        <NavUser
          user={{ name: 'shadcn', email: 'shadcn.io', avatar: 'https://github.com/shadcn.png' }}
        />
      </SidebarFooter>
      <SidebarRail />
    </Sidebar>
  );
};
export default AppSideBar;
