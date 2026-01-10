import { AlertCircle, AlertTriangle } from 'lucide-react';
import { type ReactNode } from 'react';

import { Button } from '@/components/ui/button';
import {
  Dialog,
  DialogClose,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from '@/components/ui/dialog';
import { Spinner } from '@/components/ui/spinner';
import { useModalContext } from '@/context/modal-context';

export interface ModalProps {
  trigger?: ReactNode;
  title: string;
  description: string;
  children: ReactNode;
  onConfirm?: () => void | Promise<void>;
  onCancel?: () => void;
  confirmText?: string;
  cancelText?: string;
  id?: string;
  formId?: string;
}

export function Modal({
  trigger,
  title,
  description,
  children,
  onCancel,
  confirmText = 'Confirm',
  cancelText = 'Cancel',
  id,
  formId = 'input-form',
}: ModalProps) {
  const modalContext = useModalContext();

  const modalContextState = id && modalContext ? modalContext.modals.get(id) : null;
  const open = id && modalContext ? modalContext.isModalOpen(id) : false;
  const displayError = modalContextState?.errorMessage;
  const displayWarning = modalContextState?.warningMessage;
  const isSubmitting = modalContextState?.isSubmitting || false;

  const handleCancel = () => {
    if (onCancel) {
      onCancel();
    }
    if (id && modalContext) {
      modalContext.clearModalError(id);
      modalContext.clearModalWarning(id);
    }
  };

  const handleOpenChange = (open: boolean) => {
    if (id && modalContext) {
      if (open) {
        modalContext.openModal(id);
      } else {
        modalContext.closeModal(id);
        handleCancel();
      }
    } else if (!open) {
      handleCancel();
    }
  };

  return (
    <Dialog open={open} onOpenChange={handleOpenChange}>
      {trigger && <DialogTrigger asChild>{trigger}</DialogTrigger>}
      <DialogContent className="sm:max-w-[500px]">
        <DialogHeader>
          <DialogTitle>{title}</DialogTitle>
          <DialogDescription>{description}</DialogDescription>
        </DialogHeader>

        <div className="grid gap-4 py-4">
          {displayWarning && (
            <div className="flex gap-3 rounded-md border border-yellow-200 bg-yellow-50 p-3 text-sm">
              <AlertTriangle className="h-5 w-5 shrink-0 text-yellow-600" />
              <div className="text-yellow-800">{displayWarning}</div>
            </div>
          )}

          {displayError && (
            <div className="flex gap-3 rounded-md border border-red-200 bg-red-50 p-3 text-sm">
              <AlertCircle className="h-5 w-5 shrink-0 text-red-600" />
              <div className="text-red-800">{displayError}</div>
            </div>
          )}

          {children}
        </div>

        <DialogFooter>
          <DialogClose asChild>
            <Button type="button" variant="outline" onClick={handleCancel}>
              {cancelText}
            </Button>
          </DialogClose>
          <Button type="submit" form={formId} disabled={isSubmitting}>
            {isSubmitting ? <Spinner /> : confirmText}
          </Button>
        </DialogFooter>
      </DialogContent>
    </Dialog>
  );
}
