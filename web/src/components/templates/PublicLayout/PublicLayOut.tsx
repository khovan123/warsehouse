import type { PropsWithChildren } from 'react';

import PageContent from '@/components/molecules/PageContent/PageContent';
import Footer from '@/components/organisms/Footer/Footer';
import NavHeader from '@/components/organisms/NavHeader/NavHeader';

const PublicLayOut: React.FC<PropsWithChildren> = ({ children }) => {
  return (
    <div className="min-h-screen flex flex-col bg-background text-foreground">
      <NavHeader />
      <PageContent>{children}</PageContent>
      <Footer />
    </div>
  );
};

export default PublicLayOut;
