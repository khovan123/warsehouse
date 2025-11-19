import { FormProvider, useForm, type FieldValues } from 'react-hook-form';

import type { InputFormContainerProps } from '../../../@types/input';

import InputFormPresenter from './InputFormPresenter';

const InputFormContainer = <TypeValues extends FieldValues>(
  props: InputFormContainerProps<TypeValues>
) => {
  const { defaultValues, onSubmit, ...rest } = props;

  const methods = useForm<TypeValues>({
    defaultValues,
  });

  const { handleSubmit } = methods;

  return (
    <FormProvider {...methods}>
      <InputFormPresenter onSubmit={handleSubmit(onSubmit)} {...rest} />
    </FormProvider>
  );
};

export default InputFormContainer;
