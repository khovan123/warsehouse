import { useNavigate } from 'react-router';

const LandingPage: React.FC = () => {
  const navigate = useNavigate();

  return (
    <main>
      <button onClick={() => navigate('/auth/login')}>Login</button>
    </main>
  );
};

export default LandingPage;
