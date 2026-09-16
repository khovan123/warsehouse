import { Home } from 'lucide-react';
import React from 'react';
import { Link } from 'react-router';

import { cn } from '../../../lib/utils';
import { Button } from '../../ui/button';
import { Separator } from '../../ui/separator';

import type { SideBarProps } from './type';

const SideBar: React.FC<SideBarProps> = ({ homePath, sections, activePath }) => {
  const isActive = (path: string) => activePath === path;

  return (
    <aside className="w-12 md:w-14 bg-secondary text-secondary-foreground flex flex-col items-center py-2 gap-2">
      <div className="mt-2 flex-1 flex flex-col items-center gap-1 text-[11px]">
        <Button
          asChild
          size="icon"
          variant={isActive(homePath) ? 'default' : 'ghost'}
          className={cn(
            'rounded border border-transparent',
            isActive(homePath)
              ? 'bg-primary text-primary-foreground hover:bg-primary/90'
              : 'text-secondary-foreground hover:bg-secondary-foreground/10 hover:text-primary'
          )}
        >
          <Link to={homePath} aria-label="Go to dashboard" title="Home">
            <Home className="h-4 w-4" />
          </Link>
        </Button>

        {sections.map((section) => (
          <>
            <Separator className="h-px" />
            {section.items.map((item) => {
              const IconComponent = item.icon;
              return (
                <Button
                  key={item.path}
                  asChild
                  size="icon"
                  variant={isActive(item.path) ? 'default' : 'ghost'}
                  className={cn(
                    'rounded border border-transparent',
                    isActive(item.path)
                      ? 'bg-primary text-primary-foreground hover:bg-primary/90'
                      : 'text-secondary-foreground hover:bg-secondary-foreground/10 hover:text-primary'
                  )}
                >
                  <Link to={item.path} title={item.label} aria-label={item.label}>
                    <IconComponent className="h-4 w-4" />
                  </Link>
                </Button>
              );
            })}
          </>
        ))}
      </div>
    </aside>
  );
};

export default SideBar;
