import { MOVEMENT_REQUEST_PATH } from '../constants';
import { getClient } from '../request';

export const fetchMovementApi = () => getClient().get(MOVEMENT_REQUEST_PATH);
