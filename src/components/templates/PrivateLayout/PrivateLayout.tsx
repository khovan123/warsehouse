type LayoutTemplateProps = {
  children: React.ReactNode;
};

const PrivateLayout: React.FC<LayoutTemplateProps> = ({ children }) => {
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
