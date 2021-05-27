import React, { ReactElement } from "react";

interface IProps {
    onDrop: (files: File[]) => void,
    children: ReactElement,
}

export default function DropFile({
    onDrop,
    children,
}: IProps) {

    function handleDragOver(event: React.DragEvent<HTMLDivElement>) {
        event.preventDefault();
    }

    function handleDragEnter(event: React.DragEvent<HTMLDivElement>) {
        event.preventDefault();
    }

    function handleDragLeave(event: React.DragEvent<HTMLDivElement>) {
        event.preventDefault();
    }

    function handleDrop(event: React.DragEvent<HTMLDivElement>) {
        event.preventDefault();
        const files = Array.from(event.dataTransfer.files);
        onDrop(files);
    }

    return (
        <div
            onDragOver={handleDragOver}
            onDragEnter={handleDragEnter}
            onDragLeave={handleDragLeave}
            onDrop={handleDrop}
        >
            {children}
        </div>
    );
}
