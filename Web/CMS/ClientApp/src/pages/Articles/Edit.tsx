import React, { useEffect, useState } from 'react';
import { Container } from '@mui/material';
import EditorJS from '@editorjs/editorjs';
import { useParams } from 'react-router-dom';
import { ArticleSaveData, useArticle, useUpdateArticle } from '../../api/endpoints/articles';
import Page from '../../components/Page';
import { SaveToolbar } from '../../components/Toolbars';
import UserRoles from '../../constants/UserRoles';
import Form from './Form';

export default function () {
    const articleId = parseInt(useParams().id!, 10);
    const updateArticle = useUpdateArticle(articleId);
    const [editorApi, setEditorApi] = useState<EditorJS>();
    const [articleData, setArticleData] = useState<ArticleSaveData | null>(null);

    const { data } = useArticle(articleId);
    useEffect(() => {
        setArticleData({
            ...data!,
        });
    }, [data]);

    const getSerializedContent = async () => {
        const content = await editorApi!.save();
        return JSON.stringify(content.blocks);
    };

    const handleSaveClick = async () => {
        if (articleData != null && editorApi != null) {
            await updateArticle.mutateAsync({
                ...articleData,
                content: await getSerializedContent(),
            });
        }
    };

    return (
        <Page
            requiresRole={UserRoles.Journalist}
            title='Edit Article'
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
