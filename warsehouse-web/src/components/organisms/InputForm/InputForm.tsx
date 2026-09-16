import type { FieldValues } from 'react-hook-form';

import type { InputFormContainerProps } from '@/@types/input';

import InputFormContainer from './InputFormContainer';

const InputForm = <TypeValues extends FieldValues>(props: InputFormContainerProps<TypeValues>) => {
  return <InputFormContainer {...props} />;
};

export default InputForm;
