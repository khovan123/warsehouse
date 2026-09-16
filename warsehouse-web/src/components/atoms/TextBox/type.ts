import type { InputFieldDetail } from '@/@types/input';

export type TextBoxPresenterProps = InputFieldDetail & {
  value?: string | number | readonly string[];
  onChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
};
