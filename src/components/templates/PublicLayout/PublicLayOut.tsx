import type { PropsWithChildren } from 'react';

import Footer from '@/components/organisms/Footer/Footer';
import NavHeader from '@/components/organisms/NavHeader/NavHeader';
import PageLayout from '@/components/organisms/PageLayout/PageLayout';

const PublicLayOut: React.FC<PropsWithChildren> = ({ children }) => {
  return (
    <div className="min-h-screen flex flex-col bg-background text-foreground">
      <NavHeader />
      <PageLayout>{children}</PageLayout>
      <Footer />
    </div>
  );
};

export default PublicLayOut;
