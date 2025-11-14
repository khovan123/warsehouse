import type { PropsWithChildren } from 'react';

import Footer from '../../organisms/Footer/Footer';
import Nav from '../../organisms/Nav/Nav';
import PageLayout from '../../organisms/PageLayout/PageLayout';

const PublicLayOut: React.FC<PropsWithChildren> = ({ children }) => {
  return (
    <div className="min-h-screen flex flex-col bg-background text-foreground">
      <Nav />
      <PageLayout>{children}</PageLayout>
      <Footer />
    </div>
  );
};

export default PublicLayOut;
