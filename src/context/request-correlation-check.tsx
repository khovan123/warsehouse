import React, { createContext, useContext, useState } from 'react';

interface RequestCorrelationContextType {
  errorMessages: Record<string, string | string[]>;
  setErrorMessages: React.Dispatch<React.SetStateAction<Record<string, string | string[]>>>;
  clearErrorMessages: (id?: string) => void;
  warningMessages: Record<string, string | string[]>;
  setWarningMessages: React.Dispatch<React.SetStateAction<Record<string, string | string[]>>>;
  clearWarningMessages: (id?: string) => void;
}

const RequestCorrelationCheckContext = createContext<RequestCorrelationContextType | null>(null);

export const RequestCorrelationCheckProvider: React.FC<{
  children: React.ReactNode;
}> = ({ children }) => {
  const [errorMessages, setErrorMessages] = useState<Record<string, string | string[]>>({});
  const [warningMessages, setWarningMessages] = useState<Record<string, string | string[]>>({});

  const clearErrorMessages = (id?: string) => {
    if (id) {
      setErrorMessages((prev) => {
        const newErr = { ...prev };
        delete newErr[id];
        return newErr;
      });
    } else {
      setErrorMessages({});
    }
  };

  const clearWarningMessages = (id?: string) => {
    if (id) {
      setWarningMessages((prev) => {
        const newWarning = { ...prev };
        delete newWarning[id];
        return newWarning;
      });
    } else {
      setWarningMessages({});
    }
  };

  return (
    <RequestCorrelationCheckContext.Provider
      value={{
        errorMessages,
        setErrorMessages,
        clearErrorMessages,
        warningMessages,
        setWarningMessages,
        clearWarningMessages,
      }}
    >
      {children}
    </RequestCorrelationCheckContext.Provider>
  );
};

export const useRequestCorrelationCheck = () => {
  const context = useContext(RequestCorrelationCheckContext);
  if (!context) {
    throw new Error(
      'useRequestCorrelationCheck must be used inside RequestCorrelationCheckProvider'
    );
  }
  return context;
};
