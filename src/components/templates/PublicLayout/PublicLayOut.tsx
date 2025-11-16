import type { PropsWithChildren } from 'react';

import Footer from '../../organisms/Footer/Footer';
import Nav from '../../organisms/Nav/Nav';

const PublicLayOut: React.FC<PropsWithChildren> = ({ children }) => {
  return (
    <div className="min-h-screen flex flex-col bg-background text-secondary-foreground">
      <Nav />
      <div>{children}</div>
      <Footer />
    </div>
  );
};

export default PublicLayOut;
