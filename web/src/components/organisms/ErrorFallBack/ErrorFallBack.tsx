import React, { useEffect } from 'react';
import type { FallbackProps } from 'react-error-boundary';
import { useNavigate } from 'react-router';

export const ErrorFallback: React.FC<FallbackProps> = ({ error, resetErrorBoundary }) => {
  const navigate = useNavigate();

  useEffect(() => {
    navigate('/error', {
      replace: true,
      state: { errorMessage: error?.message ?? 'Unexpected error' },
    });

    resetErrorBoundary();
  }, [error, navigate, resetErrorBoundary]);

  return null;
};
