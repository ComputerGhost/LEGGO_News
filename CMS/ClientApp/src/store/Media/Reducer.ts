import { Action, Reducer } from 'redux';
import { MediaItem, MediaState } from './Interfaces';


export interface ClearIndexAction {
    type: 'CLEAR_INDEX_ACTION',
}

export interface FetchIndexBeginAction {
    type: 'FETCH_INDEX_BEGIN_ACTION',
    key: number,
    search: string,
}

export interface FetchIndexSuccessAction {
    type: 'FETCH_INDEX_SUCCESS_ACTION',
    key: number,
    totalCount: number,
    data: MediaItem[],
}

export interface PostMediaBeginAction {
    type: 'POST_MEDIA_BEGIN_ACTION',
}

export interface PostMediaSuccessAction {
    type: 'POST_MEDIA_SUCCESS_ACTION',
    data: MediaItem,
}

export interface GetItemBeginAction {
    type: 'GET_ITEM_BEGIN_ACTION',
}

export interface GetItemSuccessAction {
    type: 'GET_ITEM_SUCCESS_ACTION',
}

export type KnownAction = ClearIndexAction
    | FetchIndexBeginAction | FetchIndexSuccessAction
    | PostMediaBeginAction | PostMediaSuccessAction
    | GetItemBeginAction | GetItemSuccessAction;


const initialState: MediaState = {
    key: 0,
    search: '',
    data: [],
    totalCount: 0,
    isLoading: false,
}


export const reducer: Reducer<MediaState> = (state: MediaState | undefined, incomingAction: Action): MediaState => {
    if (state === undefined) {
        return initialState;
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {

        case 'CLEAR_INDEX_ACTION':
            return {
                ...initialState,
                key: state.key,
            }

        case 'FETCH_INDEX_BEGIN_ACTION':
            return {
                ...state,
                isLoading: true,
                key: action.key,
                search: action.search,
            };

        case 'FETCH_INDEX_SUCCESS_ACTION':
            if (state.key === action.key) {
                return {
                    ...state,
                    isLoading: false,
                    data: [ ...state.data, ...action.data ],
                    totalCount: action.totalCount,
                };
            }
            break;

        case 'POST_MEDIA_BEGIN_ACTION':
            return {
                ...state,
                isLoading: true
            }

        case 'POST_MEDIA_SUCCESS_ACTION':
            return {
                ...state,
                isLoading: false,
                data: [ action.data, ...state.data ],
            }

        case 'GET_ITEM_BEGIN_ACTION':
            break;

        case 'GET_ITEM_SUCCESS_ACTION':
            break;
    }

    return state;
};
