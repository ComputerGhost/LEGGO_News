import { faPlus } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { IconButton } from "@material-ui/core";
import { User } from "oidc-client-ts";
import React from "react";
import SearchToolbar from "../../components/Toolbars/SearchToolbar";
import userContext from "../../contexts/userContext";
import AuthorizationService from "../../services/AuthorizationService";

interface IProps {
    onAddClick: () => void,
    onSearchChange: (newValue: string) => void,
    placeholder: string,
    rolesForAdd: string | string[],
}

export default function ({
    onAddClick,
    onSearchChange,
    placeholder,
    rolesForAdd,
}: IProps) {
    const canAddItem = (user: User | null) => {
        const service = new AuthorizationService(user);
        return service.hasRole(rolesForAdd);
    };

    return (
        <userContext.Consumer>
            {(user) =>
                <>
                    <SearchToolbar
                        onChange={onSearchChange}
                        placeholder={placeholder}
                    />
                    {canAddItem(user) &&
                        <IconButton
                            color='inherit'
                            onClick={onAddClick}
                        >
                            <FontAwesomeIcon icon={faPlus} fixedWidth />
                        </IconButton>
                    }
                </>
            }
        </userContext.Consumer>
    );
}
