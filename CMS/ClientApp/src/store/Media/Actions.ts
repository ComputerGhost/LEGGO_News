import { AppThunkAction } from '../';
import { KnownAction } from './Reducer';


async function httpGet(parameters: Record<string, string>) {
    const endpoint = `${process.env.REACT_APP_API_URL}/media?`;
    const response = await fetch(endpoint + new URLSearchParams(parameters));
    return await response.json();
}

async function httpPost(body: BodyInit) {
    const endpoint = `${process.env.REACT_APP_API_URL}/media`;
    const response = await fetch(endpoint, {
        method: 'POST',
        body: body,
    });
    return await response.json();
}


export const getMedia = (search: string): AppThunkAction<KnownAction> => async (dispatch, getState) => {
    const state = getState().media!;

    // New key for each search so that responses to previous requests aren't used.
    let key = state.key + 1;

    dispatch({ type: 'CLEAR_INDEX_ACTION' });

    dispatch({ type: 'FETCH_INDEX_BEGIN_ACTION', key, search });

    const json = await httpGet({
        query: search,
        key: key.toString(),
    });

    dispatch({
        type: 'FETCH_INDEX_SUCCESS_ACTION',
        key: json.key,
        totalCount: json.totalCount,
        data: json.data,
    });
}

export const getMoreMedia = (): AppThunkAction<KnownAction> => async (dispatch, getState) => {
    const state = getState().media!;

    // Same key when getting more, because we still want the responses even if out of order.
    let key = state.key;

    if (state.isLoading || state.data.length === state.totalCount) {
        return;
    }

    dispatch({ type: 'FETCH_INDEX_BEGIN_ACTION', key, search: state.search });

    const json = await httpGet({
        query: state.search,
        offset: state.data.length.toString(),
        key: key.toString(),
    });

    dispatch({
        type: 'FETCH_INDEX_SUCCESS_ACTION',
        key: json.key,
        totalCount: json.totalCount,
        data: json.data,
    });
}

export const postMedia = (file: File): AppThunkAction<KnownAction> => async (dispatch, getState) => {
    dispatch({ type: 'POST_MEDIA_BEGIN_ACTION' });

    const data = new FormData();
    data.append('file', file);

    const json = await httpPost(data);

    dispatch({
        type: 'POST_MEDIA_SUCCESS_ACTION',
        data: json,
    });
}

