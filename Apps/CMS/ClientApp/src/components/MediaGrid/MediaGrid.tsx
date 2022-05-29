import React from 'react';
import DropFile from './DropFile';
import userContext from '../../contexts/userContext';
import UserRoles from '../../constants/UserRoles';
import AuthorizationService from '../../services/AuthorizationService';
import { User } from 'oidc-client-ts';
import ImageList from './ImageList';

interface IProps {
    onFilesDrop: (files: File[]) => void,
    search: string,
}

export default function ({
    onFilesDrop,
    search,
}: IProps)
{
    const canAddMedia = (user: User | null) => {
        const service = new AuthorizationService(user);
        return service.hasAnyRole([UserRoles.Editor, UserRoles.Journalist]);
    }

    return (
        <userContext.Consumer>
            {(user) => canAddMedia(user)
                ?
                <DropFile onDrop={onFilesDrop}>
                    <ImageList search={search} />
                </DropFile>
                :
                <ImageList search={search} />
            }
        </userContext.Consumer>
    );
}
