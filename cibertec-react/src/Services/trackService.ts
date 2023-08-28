import axios from "axios";

export interface ITrack {
    trackId: number;
    name: string;
    albumId: number;
    mediaTypeId: number;
    genreId?: number;
    unitPrice: number;
    albumName: string;
    genreName?: string;
    mediaTypeName: string;
    artistName: string;
}

export interface IGetListResponse<T> {
    items: T[];
    nextPage: number;
}

export async function getTrackList(page?: number): Promise<IGetListResponse<ITrack>> {
    const axiosResult = await axios.get(`${process.env.REACT_APP_API_URL}/track${page ? `?page=${page}` : ""}`,
        {
            headers: {
                "Authorization": `Bearer ${window.localStorage.getItem("auth.key")}`
            }
        });
    return axiosResult.data as IGetListResponse<ITrack>;
}