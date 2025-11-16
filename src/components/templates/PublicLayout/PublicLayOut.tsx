import { Outlet } from 'react-router';

const PublicLayOut: React.FC = () => {
  return (
    <div className="min-h-screen bg-background">
      <Outlet />
    </div>
  );
};

export default PublicLayOut;
