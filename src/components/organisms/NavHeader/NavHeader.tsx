import { Menu } from 'lucide-react';
import React, { useEffect, useState } from 'react';
import { Link } from 'react-router';

import { Button } from '@/components/ui/button';
import {
  Sheet,
  SheetClose,
  SheetContent,
  SheetFooter,
  SheetHeader,
  SheetTitle,
} from '@/components/ui/sheet';
import { useIsMobile } from '@/hooks/use-mobile';
import { cn } from '@/lib/utils';
import { LOGIN_PATH } from '@/routers/route.constants';

type MenuItem = { path: string; name: string; customStyle?: string };

const PUBLIC_MENU: MenuItem[] = [
  {
    path: '#features',
    name: 'Features',
  },
  {
    path: '#pricing',
    name: 'Prices',
  },
  {
    path: '#support',
    name: 'Support',
  },
];

const NavHeader: React.FC = () => {
  const isMobile = useIsMobile();
  const [mobileMenuOpen, setMobileMenuOpen] = useState(false);

  // Tự động đóng sheet khi màn hình chuyển từ mobile sang desktop
  useEffect(() => {
    if (!isMobile && mobileMenuOpen) {
      setMobileMenuOpen(false);
    }
  }, [isMobile, mobileMenuOpen]);

  const handleMenuLinkClick = () => {
    setMobileMenuOpen(false);
  };

  const handleOpenMobileMenu = () => {
    if (isMobile) {
      setMobileMenuOpen(true);
    }
  };

  // Chỉ cho phép mở/đóng sheet trên mobile
  const handleSheetOpenChange = (open: boolean) => {
    if (isMobile) {
      setMobileMenuOpen(open);
    } else {
      setMobileMenuOpen(false);
    }
  };

  return (
    <nav
      className={cn(
        'sticky top-0 z-30 bg-card/95 backdrop-blur border-b border-border w-full px-3 sm:px-4 md:px-6'
      )}
    >
      <div className={cn('max-w-7xl mx-auto h-14 sm:h-16 flex items-center justify-between gap-4')}>
        <Link to="/" className="flex items-center gap-2 min-w-0 shrink-0">
          <div className="h-7 w-7 sm:h-8 sm:w-8 rounded-md bg-primary flex items-center justify-center shrink-0">
            <span className="text-xs font-bold text-primary-foreground">OB</span>
          </div>
          <div className="flex flex-col leading-tight min-w-0">
            <span className="font-semibold text-foreground text-xs sm:text-sm truncate">
              Openbravo
            </span>
            <span className="text-[9px] sm:text-[10px] uppercase tracking-[0.15em] text-muted-foreground hidden sm:block">
              Commerce &amp; ERP Platform
            </span>
          </div>
        </Link>

        {/* Desktop Menu */}
        <div className="hidden md:flex items-center gap-6 text-xs sm:text-sm font-medium shrink-0">
          {PUBLIC_MENU.map((item) => (
            <Link
              key={item.path}
              to={item.path}
              tabIndex={1}
              className="text-foreground hover:text-primary transition-colors whitespace-nowrap"
            >
              {item.name}
            </Link>
          ))}
        </div>

        <div className="hidden md:flex items-center gap-3 shrink-0">
          <Link
            to={LOGIN_PATH}
            className="text-xs sm:text-sm font-medium hover:text-primary transition-colors whitespace-nowrap"
          >
            Log in
          </Link>
          <Link
            to={LOGIN_PATH}
            className="inline-flex items-center justify-center rounded-full px-4 py-1.5 text-xs sm:text-sm font-medium bg-primary text-primary-foreground hover:bg-primary/80 transition-colors shadow-sm whitespace-nowrap"
          >
            Get started now
          </Link>
        </div>

        <div className="flex md:hidden items-center gap-2 shrink-0 ml-auto">
          <Link
            to={LOGIN_PATH}
            className="text-xs font-medium hover:text-primary transition-colors whitespace-nowrap"
          >
            Log in
          </Link>
          <Button
            variant="ghost"
            size="icon"
            className="h-9 w-9 shrink-0"
            onClick={handleOpenMobileMenu}
            aria-label="Open menu"
          >
            <Menu className="h-5 w-5" />
          </Button>
        </div>
      </div>

      {/* Sheet - chỉ hiển thị và cho phép mở trên mobile */}
      <Sheet open={mobileMenuOpen && isMobile} onOpenChange={handleSheetOpenChange}>
        <SheetContent side="right" className="w-80 sm:w-96">
          <SheetHeader>
            <SheetTitle className="text-left">Menu</SheetTitle>
          </SheetHeader>
          <div className="flex flex-col gap-4">
            {PUBLIC_MENU.map((item) => (
              <Link
                key={item.path}
                to={item.path}
                onClick={handleMenuLinkClick}
                className="text-base font-medium text-foreground hover:text-primary transition-colors py-2 hover:bg-accent px-6"
              >
                {item.name}
              </Link>
            ))}
          </div>
          <SheetFooter>
            <SheetClose asChild>
              <Button variant={'outline'}>Close</Button>
            </SheetClose>
          </SheetFooter>
        </SheetContent>
      </Sheet>
    </nav>
  );
};

export default NavHeader;
