import React from 'react';
import { IconButton } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faEdit, faTrash } from '@fortawesome/free-solid-svg-icons';
import { User } from 'oidc-client-ts';
import { CalendarSummary, useDeleteCalendar } from '../../api/endpoints/calendars';
import userContext from '../../contexts/userContext';
import AuthorizationService from '../../services/AuthorizationService';
import UserRoles from '../../constants/UserRoles';

interface IProps {
    calendar: CalendarSummary,
}

export default function ({ calendar }: IProps) {
    const navigate = useNavigate();
    const deleteCalendar = useDeleteCalendar(calendar.id);

    const handleDeleteClick = () => {
        deleteCalendar.mutate(undefined);
    };

    const handleEditClick = () => {
        navigate(`./${calendar.id}`);
    };

    const canDeleteCalendar = (user: User | null) => {
        const service = new AuthorizationService(user);
        return service.hasRole(UserRoles.Editor);
    };

    const canEditCalendar = (user: User | null) => {
        const service = new AuthorizationService(user);
        return service.hasRole(UserRoles.Journalist);
    };

    return (
        <userContext.Consumer>
            {(user) => (
                <>
                    <IconButton
                        disabled={canDeleteCalendar(user)}
                        onClick={handleDeleteClick}
                    >
                        <FontAwesomeIcon icon={faTrash} fixedWidth />
                    </IconButton>
                    <IconButton
                        disabled={canEditCalendar(user)}
                        onClick={handleEditClick}
                    >
                        <FontAwesomeIcon icon={faEdit} fixedWidth />
                    </IconButton>
                </>
            )}
        </userContext.Consumer>
    );
}
