import { createFilter } from 'redux-persist-transform-filter';

export const authPersistFilter = createFilter('auth', ['logined', 'data']);
