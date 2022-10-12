import React, { useState } from 'react';
import { Container } from '@mui/material';
import EditorJS from '@editorjs/editorjs';
import { useNavigate } from 'react-router-dom';
import { ArticleSaveData, useCreateArticle } from '../../api/endpoints/articles';
import Page from '../../components/Page';
import UserRoles from '../../constants/UserRoles';
import { SaveToolbar } from '../../components/Toolbars';
import Form from './Form';

export default function () {
    const createArticle = useCreateArticle();
    const navigate = useNavigate();
    const [editorApi, setEditorApi] = useState<EditorJS>();
    const [articleData, setArticleData] = useState<ArticleSaveData>({
        title: '',
        format: 'editorjs',
        content: '',
    });

    const getSerializedContent = async () => {
        const content = await editorApi!.save();
        return JSON.stringify(content.blocks);
    };

    const handleSaveClick = async () => {
        if (editorApi != null) {
            const response = await createArticle.mutateAsync({
                ...articleData,
                content: await getSerializedContent(),
            });
            navigate(`../${response.id}`);
        }
    };

    return (
        <Page
            requiresRole={UserRoles.Journalist}
            title='Create Article'
            toolbar={<SaveToolbar onSaveClick={handleSaveClick} />}
        >
            <Container>
                <Form
                    articleData={articleData}
                    setArticleData={setArticleData}
                    setEditorApi={setEditorApi}
                />
            </Container>
        </Page>
    );
}
