import { debounce } from "lodash";
import { useEffect, useMemo, useState } from "react";
import { Form, useSubmit } from "react-router-dom";

interface IProps {
    className?: string | undefined,
    defaultValue: string,
    label: string,
}

export default function SearchForm({
    className,
    defaultValue,
    label,
}: IProps) {
    const [previousValue, setPreviousValue] = useState<string>();
    const submit = useSubmit();

    const debouncedSubmit = useMemo(
        () => debounce(form => submit(form), 250),
        [submit]
    );

    const handleKeyUp = (event: React.KeyboardEvent<HTMLInputElement>) => {
        var value = event.currentTarget.value;
        if (value !== previousValue) {
            var form = (event.currentTarget as HTMLInputElement).form;
            debouncedSubmit(form);
        }
        setPreviousValue(value);
    };

    // fixes search not matching url when back button used
    useEffect(() => {
        var searchElement = document.getElementById('search') as HTMLInputElement;
        searchElement.value = defaultValue ?? "";
    }, [defaultValue]);

    return (
        <div className={className}>
            <Form role='search'>
                <div className='form-floating'>
                    <input
                        className='form-control'
                        defaultValue={defaultValue ?? ""}
                        id='search'
                        maxLength={50}
                        name='search'
                        onKeyUp={handleKeyUp}
                        placeholder={label}
                        type='search'
                    />
                    <label htmlFor='search'>{label}</label>
                </div>
            </Form>
        </div>
    );
}