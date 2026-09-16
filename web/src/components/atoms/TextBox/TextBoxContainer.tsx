import TextBoxPresenter from './TextBoxPresenter';
import type { TextBoxPresenterProps } from './type';

const TextBoxContainer = (props: TextBoxPresenterProps) => {
  return <TextBoxPresenter {...props} />;
};

export default TextBoxContainer;
