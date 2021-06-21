import { FormControl, IconButton, InputAdornment, InputLabel, OutlinedInput } from "@material-ui/core";
import React, { ChangeEvent, useState } from "react";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faEye, faEyeSlash } from '@fortawesome/free-solid-svg-icons';

interface IProps {
    label: string,
    value: string,
    onChange: (event: ChangeEvent<HTMLInputElement>) => void,
    // Forwarded props
    fullWidth?: boolean,
    margin?: 'dense' | 'normal' | 'none',
    required?: boolean,
}

export default function PasswordField({
    label,
    value,
    onChange,
    // Forwarded props
    fullWidth,
    margin,
    required,
}: IProps) {
    const [showPassword, setShowPassword] = useState(false);

    const uniqueId = 'password';

    function handleClickShowPassword() {
        setShowPassword(!showPassword);
    }

    function handleMouseDownPassword(event: React.MouseEvent<HTMLButtonElement>) {
        event.preventDefault();
    }

    return (
        <FormControl fullWidth={fullWidth} margin={margin} required={required} variant='outlined'>
            <InputLabel htmlFor={uniqueId}>{label}</InputLabel>
            <OutlinedInput
                id={uniqueId}
                type={showPassword ? 'text' : 'password'}
                value={value}
                onChange={onChange}
                endAdornment={
                    <InputAdornment position="end">
                        <IconButton
                            aria-label="toggle password visibility"
                            onClick={handleClickShowPassword}
                            onMouseDown={handleMouseDownPassword}
                            edge="end"
                        >
                            <FontAwesomeIcon icon={showPassword ? faEye : faEyeSlash} fixedWidth />
                        </IconButton>
                    </InputAdornment>
                }
                label={label}
            />
        </FormControl>
    );
}