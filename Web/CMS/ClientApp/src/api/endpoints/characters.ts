import RestApi from '../internal/HookedApi';

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

const characters = new RestApi<CharacterSummary, CharacterDetails, CharacterSaveData>('characters');

export function useCharacters(search: string) {
    return characters.useItems(search);
}

export function useCharacter(characterId: number | undefined) {
    return characters.useItem(characterId);
}

export function useCreateCharacter() {
    return characters.useCreateItem();
}

export function useUpdateCharacter(characterId: number | undefined) {
    if (!characterId) {
        throw new Error('characterId must be defined to update.');
    }
    return characters.useUpdateItem(characterId);
}

export function useDeleteCharacter(characterId: number) {
    return characters.useDeleteItem(characterId);
}
