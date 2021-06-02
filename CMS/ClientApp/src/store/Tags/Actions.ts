import { ThunkAction } from 'redux-thunk';
import { ApplicationState, AppThunkAction } from '../';
import { TagSummary, TagDetails, TagSaveData } from './Interfaces';
import { KnownAction } from './Reducer';


export const getTags = (search: string): AppThunkAction<KnownAction> => async (dispatch, getState) => {
    const state = getState().tags!;

    // New key for each search so that responses to previous requests aren't used.
    let key = state.key + 1;

    dispatch({ type: 'CLEAR_INDEX_ACTION' });

    dispatch({ type: 'FETCH_INDEX_BEGIN_ACTION', key, search });

    const parameters = {
        query: search,
        key: key.toString(),
    };
    const endpoint = `${process.env.REACT_APP_API_URL}/tags?`;
    const response = await fetch(endpoint + new URLSearchParams(parameters));
    const json = await response.json();

    dispatch({
        type: 'FETCH_INDEX_SUCCESS_ACTION',
        key: json.key,
        totalCount: json.totalCount,
        data: json.data,
    });
}

export const getMoreTags = (): AppThunkAction<KnownAction> => async (dispatch, getState) => {
    const state = getState().tags!;

    // Same key when getting more, because we still want the responses even if out of order.
    let key = state.key;

    if (state.isLoading || state.data.length === state.totalCount) {
        return;
    }

    dispatch({ type: 'FETCH_INDEX_BEGIN_ACTION', key, search: state.search });

    const parameters = {
        query: state.search,
        offset: state.data.length.toString(),
        key: key.toString(),
    };
    const endpoint = `${process.env.REACT_APP_API_URL}/tags?`;
    const response = await fetch(endpoint + new URLSearchParams(parameters));
    const json = await response.json();

    dispatch({
        type: 'FETCH_INDEX_SUCCESS_ACTION',
        key: json.key,
        totalCount: json.totalCount,
        data: json.data,
    });
}

export function getTag(tagId: number): AppThunkAction<KnownAction, TagDetails> {
    return async function (dispatch, getState) {
        const tags = getState().tags!;

        const tagItem = tags.data.find(item => item.id === tagId);
        if (tagItem !== undefined) {
            return tagItem as TagDetails;
        }
        else {
            dispatch({ type: 'GET_TAG_BEGIN_ACTION', tagId });

            const endpoint = `${process.env.REACT_APP_API_URL}/tags/${tagId}`;
            const response = await fetch(endpoint);
            const json = await response.json();

            dispatch({ type: 'GET_TAG_SUCCESS_ACTION', data: json, });
            return json as TagDetails;
        }
    }
}

export function createTag(saveData: TagSaveData) {
    return async function (dispatch: (action: KnownAction) => void) {
        dispatch({ type: 'CREATE_TAG_BEGIN_ACTION', data: saveData });

        const formData = new FormData();
        formData.append('name', saveData.name);
        formData.append('description', saveData.description);

        const endpoint = `${process.env.REACT_APP_API_URL}/tags`;
        const response = await fetch(endpoint, {
            method: 'POST',
            body: formData,
        });
        const newTag = await response.json() as TagDetails;

        dispatch({ type: 'CREATE_TAG_SUCCESS_ACTION', data: newTag });
        return newTag;
    }
}

export function updateTag(tagId: number, data: TagSaveData) {
    return async function (dispatch: (action: KnownAction) => void) {
        dispatch({ type: 'UPDATE_TAG_BEGIN_ACTION', tagId, data });

        const formData = new FormData();
        formData.append('name', data.name);
        formData.append('description', data.description);

        const endpoint = `${process.env.REACT_APP_API_URL}/tags/${tagId}`;
        const response = await fetch(endpoint, {
            method: 'PUT',
            body: formData,
        });

        dispatch({ type: 'UPDATE_TAG_SUCCESS_ACTION', tagId });
    }
}

export const deleteTag = (tagId: number): AppThunkAction<KnownAction> => async (dispatch, getState) => {
    dispatch({ type: 'DELETE_TAG_BEGIN_ACTION', tagId });

    const endpoint = `${process.env.REACT_APP_API_URL}/tags/${tagId}`;
    const response = await fetch(endpoint);

    dispatch({ type: 'DELETE_TAG_SUCCESS_ACTION', tagId });
}
