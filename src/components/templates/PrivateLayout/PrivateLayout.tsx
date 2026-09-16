import type { PropsWithChildren } from 'react';

const PrivateLayout: React.FC<PropsWithChildren> = ({ children }) => {
  return (
    <div>
      <header>
        <p>nav</p>
      </header>
      <div>{children}</div>
      <footer>
        <p>My App Footer</p>
      </footer>
    </div>
  );
};

export default PrivateLayout;
