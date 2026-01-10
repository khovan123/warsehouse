import { useModalContext } from '@/context/modal-context';

export function useModal(modalId: string) {
  const context = useModalContext();

  if (!context) {
    throw new Error('useModal must be used within a ModalProvider');
  }

  const {
    openModal: contextOpenModal,
    closeModal: contextCloseModal,
    isModalOpen: contextIsModalOpen,
    getModalData: contextGetModalData,
    setModalError: contextSetModalError,
    setModalWarning: contextSetModalWarning,
    clearModalError: contextClearModalError,
    clearModalWarning: contextClearModalWarning,
    clearModal: contextClearModal,
    setModalSubmitting: contextSetModalSubmitting,
  } = context;

  return {
    isOpen: contextIsModalOpen(modalId),
    data: contextGetModalData(modalId),

    open: (data?: unknown) => contextOpenModal(modalId, data),
    close: () => contextCloseModal(modalId),
    clear: () => contextClearModal(modalId),

    setError: (error: string) => contextSetModalError(modalId, error),
    clearError: () => contextClearModalError(modalId),

    setWarning: (warning: string) => contextSetModalWarning(modalId, warning),
    clearWarning: () => contextClearModalWarning(modalId),

    setSubmitting: (isSubmitting: boolean) => contextSetModalSubmitting(modalId, isSubmitting),
  };
}
