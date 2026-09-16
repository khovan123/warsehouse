import React from 'react';

import { useIsMobile } from '@/hooks/use-mobile';
import { cn } from '@/lib/utils';

const Footer: React.FC = () => {
  const isMobile = useIsMobile();

  return (
    <footer className="border-t border-border bg-card">
      <div
        className={cn(
          'max-w-7xl mx-auto py-4 flex items-center justify-between gap-4 text-muted-foreground',
          isMobile ? 'flex-col text-xs' : 'flex-row text-sm'
        )}
      >
        <p className={cn(isMobile ? 'text-center' : 'text-left')}>
          © {new Date().getFullYear()} Openbravo. All rights reserved.
        </p>

        <div
          id="support"
          className={cn(
            'flex flex-wrap items-center gap-4',
            isMobile ? 'justify-center' : 'justify-end'
          )}
        >
          <a href="#privacy" className="hover:text-foreground transition">
            Privacy policy
          </a>
          <a href="#terms" className="hover:text-foreground transition">
            Terms of use
          </a>
          <a href="#contact" className="hover:text-foreground transition">
            Contact support
          </a>
        </div>
      </div>
    </footer>
  );
};

export default Footer;
