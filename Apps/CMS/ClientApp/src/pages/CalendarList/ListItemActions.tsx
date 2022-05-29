import React from 'react';
import { IconButton } from '@mui/material';
import { CalendarSummary, useDeleteCalendar } from '../../api/endpoints/calendars';
import { useNavigate } from 'react-router-dom';
import userContext from '../../contexts/userContext';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faEdit, faTrash } from '@fortawesome/free-solid-svg-icons';
import { User } from 'oidc-client-ts';
import AuthorizationService from '../../services/AuthorizationService';
import UserRoles from '../../constants/UserRoles';

interface IProps {
    calendar: CalendarSummary,
}

export default function ({
    calendar
}: IProps) {
    var navigate = useNavigate();
    var deleteCalendar = useDeleteCalendar(calendar.id);

    const handleDeleteClick = () => {
        deleteCalendar.mutate(undefined);
    }

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
    }

    return (
        <userContext.Consumer>
            {(user) =>
                <>
                    <IconButton
                        disabled={canDeleteCalendar(user)}
                        onClick={handleDeleteClick}
                        size="large">
                        <FontAwesomeIcon icon={faTrash} fixedWidth />
                    </IconButton>
                    <IconButton disabled={canEditCalendar(user)} onClick={handleEditClick} size="large">
                        <FontAwesomeIcon icon={faEdit} fixedWidth />
                    </IconButton >
                </>
            }
        </userContext.Consumer>
    );
}
