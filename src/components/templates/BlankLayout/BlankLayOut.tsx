import type { PropsWithChildren } from 'react';

const BlankLayOut: React.FC<PropsWithChildren> = ({ children }) => {
  return <div>{children}</div>;
};

export default BlankLayOut;
