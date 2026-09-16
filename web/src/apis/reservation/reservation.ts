import { RESERVATION_REQUEST_PATH } from '../constants';
import { getClient } from '../request';

export const fetchReservationApi = () => getClient().get(RESERVATION_REQUEST_PATH);
