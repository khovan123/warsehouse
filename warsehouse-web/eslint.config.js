import path from 'path';

import js from '@eslint/js';
import tsParser from '@typescript-eslint/parser';
import cssModules from 'eslint-plugin-css-modules';
import importPlugin from 'eslint-plugin-import';
import jsxA11y from 'eslint-plugin-jsx-a11y';
import prettier from 'eslint-plugin-prettier';
import reactPlugin from 'eslint-plugin-react';
import reactHooks from 'eslint-plugin-react-hooks';
import reactRefresh from 'eslint-plugin-react-refresh';
import { defineConfig, globalIgnores } from 'eslint/config';
import globals from 'globals';
import tseslint from 'typescript-eslint';

export default defineConfig([
  globalIgnores(['dist', 'node_modules', 'build']),
  {
    files: ['**/*.{js,jsx,ts,tsx}'],
    ignores: ['dist', 'node_modules', 'build'],
    extends: [
      js.configs.recommended,
      ...tseslint.configs.recommended,
      reactHooks.configs['recommended-latest'],
      reactRefresh.configs.vite,
    ],
    languageOptions: {
      ecmaVersion: 2020,
      sourceType: 'module',
      parser: tsParser,
      globals: {
        ...globals.browser,
        ...globals.es2020,
      },
    },
    plugins: {
      '@typescript-eslint': tseslint.plugin,
      react: reactPlugin,
      'jsx-a11y': jsxA11y,
      prettier,
      'css-modules': cssModules,
      import: importPlugin,
    },
    settings: {
      react: { version: 'detect' },
      'import/parsers': {
        '@typescript-eslint/parser': ['.ts', '.tsx'],
      },
      'import/resolver': {
        typescript: {
          project: path.resolve('./tsconfig.json'),
        },
        node: {
          extensions: ['.js', '.jsx', '.ts', '.tsx'],
        },
      },
    },
    rules: {
      // ⚙️ TypeScript & Prettier
      '@typescript-eslint/ban-types': 'off',
      '@typescript-eslint/no-unused-vars': [
        'error',
        {
          argsIgnorePattern: '^(_|err|error|action|state)',
          varsIgnorePattern: '^(_|err|error|action|state)',
          caughtErrorsIgnorePattern: '^(_|err|error|action|state)',
        },
      ],
      'prettier/prettier': [
        'error',
        {
          singleQuote: true,
          endOfLine: 'auto',
          semi: true,
          trailingComma: 'es5',
          printWidth: 100,
          tabWidth: 2,
        },
      ],

      // 🎨 CSS Modules
      'css-modules/no-unused-class': 1,
      'css-modules/no-undef-class': 1,

      // 🧠 Common rules
      curly: 2,
      'linebreak-style': 0,
      'max-len': [
        2,
        {
          code: 120,
          ignoreTemplateLiterals: true,
          ignoreStrings: true,
          ignoreComments: true,
        },
      ],
      'no-case-declarations': 1,
      'no-console': ['warn', { allow: ['warn', 'error', 'info'] }],
      // 'no-shadow': 'error',
      'no-use-before-define': 0,
      'no-useless-escape': 1,

      // ⚛️ React Hooks
      'react-hooks/rules-of-hooks': 'error',
      'react-hooks/exhaustive-deps': 'warn',

      // ⚛️ React rules
      'react/no-deprecated': 1,
      'react/no-string-refs': 1,
      'react/no-array-index-key': 1,
      'react/jsx-indent-props': [2, 2],
      'react/jsx-indent': [2, 2],
      'react/jsx-filename-extension': 0,
      'react/jsx-props-no-spreading': 0,
      'react/prop-types': 0,
      'react/require-default-props': 0,
      'react-refresh/only-export-components': 0,

      // ♿ Accessibility
      'jsx-a11y/click-events-have-key-events': 0,
      'jsx-a11y/no-static-element-interactions': 0,
      'jsx-a11y/label-has-associated-control': 0,
      'jsx-a11y/alt-text': 0,
      'jsx-a11y/anchor-is-valid': 0,

      // 📦 Import rules
      'import/prefer-default-export': 0,
      'import/extensions': [
        'error',
        'ignorePackages',
        {
          js: 'never',
          jsx: 'never',
          ts: 'never',
          tsx: 'never',
        },
      ],
      'import/order': [
        2,
        {
          groups: ['builtin', 'external', 'internal', 'parent', 'sibling', 'index'],
          'newlines-between': 'always',
          alphabetize: { order: 'asc', caseInsensitive: true },
        },
      ],
      'import/newline-after-import': 2,
      'import/no-unresolved': 'error',
    },
  },
  {
    files: ['*.config.js'],
    rules: {
      'import/order': 'off',
    },
  },
]);
