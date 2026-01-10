import React, { createContext, useContext, useState, type ReactNode } from 'react';

export interface ModalState {
  isOpen: boolean;
  errorMessage?: string;
  warningMessage?: string;
  data?: unknown;
  isSubmitting?: boolean;
}

interface ModalContextType {
  modals: Map<string, ModalState>;
  openModal: (id: string, data?: unknown) => void;
  closeModal: (id: string) => void;
  isModalOpen: (id: string) => boolean;
  getModalData: (id: string) => unknown;
  setModalError: (id: string, error: string) => void;
  setModalWarning: (id: string, warning: string) => void;
  clearModalError: (id: string) => void;
  clearModalWarning: (id: string) => void;
  clearModal: (id: string) => void;
  setModalSubmitting: (id: string, isSubmitting: boolean) => void;
}

const ModalContext = createContext<ModalContextType | null>(null);

export const ModalProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
  const [modals, setModals] = useState<Map<string, ModalState>>(new Map());

  const openModal = (id: string, data?: unknown): void => {
    setModals((prev) => {
      const modal = prev.get(id) || { isOpen: false };
      return new Map(prev).set(id, { ...modal, isOpen: true, data });
    });
  };

  const closeModal = (id: string): void => {
    setModals((prev) => {
      const modal = prev.get(id);
      if (!modal) {
        return prev;
      }
      return new Map(prev).set(id, { ...modal, isOpen: false });
    });
  };

  const isModalOpen = (id: string): boolean => {
    return modals.get(id)?.isOpen || false;
  };

  const getModalData = (id: string): unknown => {
    return modals.get(id)?.data;
  };

  const setModalError = (id: string, error: string): void => {
    setModals((prev) => {
      const modal = prev.get(id) || { isOpen: false };
      return new Map(prev).set(id, { ...modal, errorMessage: error });
    });
  };

  const setModalWarning = (id: string, warning: string): void => {
    setModals((prev) => {
      const modal = prev.get(id) || { isOpen: false };
      return new Map(prev).set(id, { ...modal, warningMessage: warning });
    });
  };

  const clearModalError = (id: string): void => {
    setModals((prev) => {
      const modal = prev.get(id);
      if (!modal) {
        return prev;
      }
      const updated = { ...modal };
      delete updated.errorMessage;
      return new Map(prev).set(id, updated);
    });
  };

  const clearModalWarning = (id: string): void => {
    setModals((prev) => {
      const modal = prev.get(id);
      if (!modal) {
        return prev;
      }
      const updated = { ...modal };
      delete updated.warningMessage;
      return new Map(prev).set(id, updated);
    });
  };

  const clearModal = (id: string): void => {
    setModals((prev) => {
      const newModals = new Map(prev);
      newModals.delete(id);
      return newModals;
    });
  };

  const setModalSubmitting = (id: string, isSubmitting: boolean): void => {
    setModals((prev) => {
      const modal = prev.get(id);
      if (!modal) {
        return prev;
      }
      return new Map(prev).set(id, { ...modal, isSubmitting });
    });
  };

  return (
    <ModalContext.Provider
      value={{
        modals,
        openModal,
        closeModal,
        isModalOpen,
        getModalData,
        setModalError,
        setModalWarning,
        clearModalError,
        clearModalWarning,
        clearModal,
        setModalSubmitting,
      }}
    >
      {children}
    </ModalContext.Provider>
  );
};

export const useModalContext = (): ModalContextType | null => {
  return useContext(ModalContext);
};
