import { QueryFunctionContext, useInfiniteQuery, useMutation, useQuery } from "react-query";
import { SearchResults, getNextPageParam } from "./search";


export interface CharacterDetails {
    id: number,
    birthDate: Date | null,
    englishName: string,
    koreanName: string,
    emoji: string,
    description: string,
}

export interface CharacterSaveData {
    birthDate: Date | null,
    englishName: string,
    koreanName: string,
    emoji: string,
    description: string,
}

export interface CharacterSummary {
    id: number,
    emoji: string,
    englishName: string,
}


export function useCharacters(search: string) {
    const getCharacters = async ({ pageParam }: QueryFunctionContext) => {
        const parameters = new URLSearchParams({
            query: search,
            offset: pageParam?.toString() ?? 0,
        });
        const endpoint = `${process.env.REACT_APP_API_URL}/characters`;
        const response = await fetch(endpoint + '?' + parameters);
        return await response.json() as SearchResults<CharacterSummary>;
    }
    return useInfiniteQuery(['characters', search], getCharacters, { getNextPageParam });
}

export function useCharacter(characterId: number | undefined) {
    const getCharacter = async () => {
        if (!characterId)
            return undefined;
        const endpoint = `${process.env.REACT_APP_API_URL}/characters/${characterId}`;
        const response = await fetch(endpoint);
        return await response.json() as CharacterDetails;
    }
    return useQuery(['characters', characterId], getCharacter);
}

export function useCreateCharacter() {
    return useMutation(async (data: CharacterSaveData) => {
        const endpoint = `${process.env.REACT_APP_API_URL}/characters`;
        const response = await fetch(endpoint, {
            method: 'POST',
            body: JSON.stringify(data)
        });
        return await response.json() as CharacterDetails;
    });
}

export function useUpdateCharacter(characterId: number | undefined) {
    return useMutation(async (data: CharacterSaveData) => {
        if (!characterId)
            throw new Error('characterId must be defined to update.');
        const endpoint = `${process.env.REACT_APP_API_URL}/characters/${characterId}`;
        const response = await fetch(endpoint, {
            method: 'PUT',
            body: JSON.stringify(data)
        });
        return await response.json() as CharacterDetails;
    });
}

export function useDeleteCharacter(characterId: number) {
    return useMutation(async () => {
        const endpoint = `${process.env.REACT_APP_API_URL}/characters/${characterId}`;
        const response = await fetch(endpoint, {
            method: 'DELETE',
        });
    });
}
