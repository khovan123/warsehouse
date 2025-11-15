import React from 'react';

import type { TextBoxPresenterProps } from './type';

const TextBoxPresenter: React.FC<TextBoxPresenterProps> = ({
  label,
  name,
  type = 'text',
  value,
  placeholder,
  onChange,
}) => {
  return (
    <div className="mb-4 w-full">
      {label && (
        <label htmlFor={name} className="block mb-2 text-sm font-medium text-gray-700">
          {label}
        </label>
      )}
      <input
        id={name}
        name={name}
        type={type}
        value={value}
        placeholder={placeholder}
        onChange={onChange}
        className="w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm 
                   focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500"
      />
    </div>
  );
};
export default TextBoxPresenter;
