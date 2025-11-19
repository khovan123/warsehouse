import React from 'react';
import { useDispatch } from 'react-redux';
import { Link } from 'react-router';

import { Avatar, AvatarFallback, AvatarImage } from '../../../components/ui/avatar';
import { cn } from '../../../lib/utils';
import { LOGIN_PATH } from '../../../routers/route.constants';
import { useAuthSelector } from '../../../state/ducks/auth/selectors';
import { logout } from '../../../state/ducks/auth/slice';

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

const Nav: React.FC = () => {
  const dispatch = useDispatch();
  const authSelector = useAuthSelector();
  return (
    <nav className="sticky top-0 z-30 bg-card/95 backdrop-blur border-b border-border">
      <div
        className={cn(
          authSelector.logined
            ? 'h-12 md:h-14 flex items-center justify-between px-4 border-b border-border bg-card'
            : 'max-w-7xl mx-auto h-14 sm:h-16 flex items-center justify-between'
        )}
      >
        <Link to="/" className="flex items-center gap-2">
          {authSelector.logined ? (
            <>
              <span className="text-lg font-semibold text-primary">OpenWMS</span>
              <span className="text-[11px] hidden sm:inline">Warehouse Management</span>
            </>
          ) : (
            <>
              <div className="h-8 w-8 rounded-md bg-primary flex items-center justify-center">
                <span className="text-xs font-bold text-primary-foreground">OB</span>
              </div>
              <div className="flex flex-col leading-tight">
                <span className="font-semibold text-foreground text-sm">Openbravo</span>
                <span className="text-[10px] uppercase tracking-[0.15em] text-muted-foreground">
                  Commerce &amp; ERP Platform
                </span>
              </div>
            </>
          )}
        </Link>
        {!authSelector.logined && (
          <div className="hidden md:flex items-center gap-6 text-xs sm:text-sm font-medium text-muted">
            {PUBLIC_MENU.map((item) => (
              <Link
                key={item.path}
                to={item.path}
                tabIndex={1}
                className="text-foreground hover:text-primary transition"
              >
                {item.name}
              </Link>
            ))}
          </div>
        )}

        <div className="flex items-center gap-3">
          {authSelector.logined ? (
            <>
              <Avatar className="h-8 w-8">
                <AvatarImage src="https://github.com/shadcn.png" />
                <AvatarFallback>CN</AvatarFallback>
              </Avatar>
              <Link
                to={LOGIN_PATH}
                className="text-xs sm:text-sm font-medium hover:text-primary transition"
                onClick={() => {
                  dispatch(logout());
                }}
              >
                Log out
              </Link>
            </>
          ) : (
            <>
              <Link
                to={LOGIN_PATH}
                className="text-xs sm:text-sm font-medium hover:text-primary transition"
              >
                Log in
              </Link>
              <Link
                to={LOGIN_PATH}
                className="hidden sm:inline-flex items-center justify-center rounded-full px-4 py-1.5 text-xs sm:text-sm font-medium bg-primary text-primary-foreground hover:bg-primary/80 transition shadow-sm"
              >
                Get started now
              </Link>
            </>
          )}
        </div>
      </div>
    </nav>
  );
};

export default Nav;
