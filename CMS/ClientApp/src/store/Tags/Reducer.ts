import { Action, Reducer } from 'redux';
import { TagSummary, TagDetails, TagSaveData, TagsState } from './Interfaces';


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
    data: TagSummary[],
}

export interface GetTagBeginAction {
    type: 'GET_TAG_BEGIN_ACTION',
    tagId: number,
}

export interface GetTagSuccessAction {
    type: 'GET_TAG_SUCCESS_ACTION',
    data: TagDetails,
}

export interface CreateTagBeginAction {
    type: 'CREATE_TAG_BEGIN_ACTION',
    data: TagSaveData,
}

export interface CreateTagSuccessAction {
    type: 'CREATE_TAG_SUCCESS_ACTION',
    data: TagDetails,
}

export interface UpdateTagBeginAction {
    type: 'UPDATE_TAG_BEGIN_ACTION',
    tagId: number,
    data: TagSaveData,
}

export interface UpdateTagSuccessAction {
    type: 'UPDATE_TAG_SUCCESS_ACTION',
    tagId: number,
}

export interface DeleteTagBeginAction {
    type: 'DELETE_TAG_BEGIN_ACTION',
    tagId: number,
}

export interface DeleteTagSuccessAction {
    type: 'DELETE_TAG_SUCCESS_ACTION',
    tagId: number,
}

export type KnownAction = ClearIndexAction
    | FetchIndexBeginAction | FetchIndexSuccessAction
    | GetTagBeginAction | GetTagSuccessAction
    | CreateTagBeginAction | CreateTagSuccessAction
    | UpdateTagBeginAction | UpdateTagSuccessAction
    | DeleteTagBeginAction | DeleteTagSuccessAction
;


const initialState: TagsState = {
    key: 0,
    search: '',
    data: [],
    totalCount: 0,
    isLoading: false,
}


export const reducer: Reducer<TagsState> = (state: TagsState | undefined, incomingAction: Action): TagsState => {
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

        case 'POST_TAG_BEGIN_ACTION':
            return {
                ...state,
                isLoading: true
            }

        case 'POST_TAG_SUCCESS_ACTION':
            return {
                ...state,
                isLoading: false,
                data: [ action.data, ...state.data ],
            }

        case 'GET_TAG_BEGIN_ACTION':
            return {
                ...state,
                isLoading: true,
            };

        case 'GET_TAG_SUCCESS_ACTION':
            return {
                ...state,
                isLoading: false,
                data: [ action.data, ...state.data ],
            }
    }

    return state;
};
