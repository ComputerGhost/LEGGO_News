export { getTags, getMoreTags, createTag, updateTag, deleteTag } from './Actions';
export { reducer } from './Reducer';

// Can't re-export interfaces normally, so do it this way...
export * from './Interfaces';
