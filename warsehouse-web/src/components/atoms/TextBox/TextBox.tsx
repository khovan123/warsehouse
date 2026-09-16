import { withFormController, type FormController } from '@/hocs/withFormController';

import TextBoxContainer from './TextBoxContainer';
import type { TextBoxPresenterProps } from './type';

const TextBox = withFormController<FormController & TextBoxPresenterProps>(TextBoxContainer);

export default TextBox;
