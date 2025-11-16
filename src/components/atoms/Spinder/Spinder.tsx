import type { JSX } from 'react';

const Spinner = (): JSX.Element => {
  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center">
      <svg className="h-10 w-10 animate-spin text-primary" viewBox="0 0 24 24" aria-hidden="true">
        <circle
          className="opacity-25"
          cx="12"
          cy="12"
          r="10"
          stroke="currentColor"
          strokeWidth="4"
          fill="none"
        />
        <path className="opacity-75" fill="currentColor" d="M4 12a8 8 0 018-8v4a4 4 0 00-4 4H4z" />
      </svg>
    </div>
  );
};

export default Spinner;
