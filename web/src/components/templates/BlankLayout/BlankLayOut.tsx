import type { PropsWithChildren } from 'react';

const BlankLayOut: React.FC<PropsWithChildren> = ({ children }) => {
  return <div className="min-h-screen flex flex-col bg-background">{children}</div>;
};

export default BlankLayOut;
