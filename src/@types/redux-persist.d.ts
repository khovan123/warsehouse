declare module 'redux-persist/lib/storage' {
  import { Storage } from 'redux-persist';

  const storage: Storage;
  export default storage;
}

declare module 'redux-persist/lib/storage/session' {
  import { Storage } from 'redux-persist';

  const storageSession: Storage;
  export default storageSession;
}
