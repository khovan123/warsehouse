import { ChevronRight } from 'lucide-react';
import { Link } from 'react-router';

import { SearchForm } from '@/components/organisms/SearchForm/SearchForm';
import UserDropdown from '@/components/organisms/UserDropdown/UserDropdown';
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
import { usePageContext } from '@/context/page-context';
import { cn } from '@/lib/utils';
import { LANDING_PATH } from '@/routers/route.constants';
import { isActivePath } from '@/utils/navigation';

import type { SideBarProps } from './type';

const AppSideBar = ({ activePath, sections, iconSize = 4, ...props }: SideBarProps) => {
  const { setBreadCrumb } = usePageContext();
  return (
    <Sidebar {...props}>
      <SidebarHeader className="p-0">
        <div className="bg-primary px-2 h-16 flex items-center justify-center">
          <Link to={LANDING_PATH}>
            <span className="text-lg font-semibold text-primary-foreground">OpenWMS</span>
            <span className="text-[11px] hidden sm:inline">Warehouse Management</span>
          </Link>
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
                            onClick={() => {
                              setBreadCrumb({ group: name, items: [{ path, label }] });
                            }}
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
        <SidebarMenu>
          <SidebarMenuItem>
            <UserDropdown
              user={{ name: 'shadcn', email: 'shadcn.io', avatar: 'https://github.com/shadcn.png' }}
              inSidebar={true}
            />
          </SidebarMenuItem>
        </SidebarMenu>
      </SidebarFooter>
      <SidebarRail />
    </Sidebar>
  );
};
export default AppSideBar;
