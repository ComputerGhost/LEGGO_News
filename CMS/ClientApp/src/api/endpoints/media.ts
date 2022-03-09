import RestApi from "../RestApi";


export interface MediaSummary {
    id: number,
    caption: string,
    credit: string,
    creditUrl: string,
    originalUrl: string,
    thumbnailUrl: string,
    smallSizeUrl: string,
    mediumSizeUrl: string,
    largeSizeUrl: string,
}


const media = new RestApi<MediaSummary, unknown, unknown>('media');

export function useMedia(search: string) {
    return media.useItems(search);
}

export function useDeleteMedia(mediaId: number) {
    return media.useDeleteItem(mediaId);
}

export function useUploadMedia() {
    return media.useUploadItem();
}
