import { useModalContext } from '@/context/modal-context';

export function useModal() {
  const context = useModalContext();

  if (!context) {
    throw new Error('useModal must be used within a ModalProvider');
  }

  const { isOpen, openModal, closeModal, setModalSubmitting } = context;

  return {
    isOpen,
    open: () => openModal(),
    close: () => closeModal(),

    setSubmitting: (isSubmitting: boolean) => setModalSubmitting(isSubmitting),
  };
}
