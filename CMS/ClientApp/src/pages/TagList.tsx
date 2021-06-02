import React, { useEffect } from 'react';
import { connect } from 'react-redux';
import { faPlus } from '@fortawesome/free-solid-svg-icons';
import { ThunkDispatch } from 'redux-thunk';
import { Page, SearchToolbar } from '../components';
import { Container, IconButton, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from '@material-ui/core';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { getMoreTags, getTags, TagItem } from '../store/Tags';
import { ApplicationState } from '../store';
import InfiniteScroll from '../components/InfiniteScroll';
import { useHistory } from 'react-router-dom';

interface IProps {
    tags?: TagItem[],
    hasMore?: boolean,
    getTags?: (search: string) => void,
    getMoreTags?: () => void,
}

function TagList({
    tags,
    hasMore,
    getTags,
    getMoreTags,
}: IProps) {
    var history = useHistory();

    function handleAddClicked() {
        history.push('/tags/new');
    }

    function handleSearchChanged(newSearch: string) {
        getTags!(newSearch);
    }

    useEffect(() => {
        handleSearchChanged('');
    }, [])

    const toolbar =
        <>
            <SearchToolbar placeholder='Search Tags...' onChange={handleSearchChanged} />
            <IconButton color='inherit' onClick={handleAddClicked}>
                <FontAwesomeIcon icon={faPlus} fixedWidth />
            </IconButton>
        </>;

    return (
        <Page title='Tags' toolbar={toolbar}>
            <Container>
                <InfiniteScroll data={tags!} hasMore={hasMore!} next={getMoreTags!}>
                    <TableContainer component={Paper}>
                        <Table>
                            <TableHead>
                                <TableRow>
                                    <TableCell>Name</TableCell>
                                </TableRow>
                            </TableHead>
                            <TableBody>
                                {tags!.map((tag) =>
                                    <TableRow>
                                        <TableCell>
                                            {tag.name}
                                        </TableCell>
                                    </TableRow>
                                )}
                            </TableBody>
                        </Table>
                    </TableContainer>
                </InfiniteScroll>
            </Container>
        </Page>
    );
}


// Redux

interface StateProps {
    tags: TagItem[],
    hasMore: boolean,
}

interface DispatchProps {
    getTags: (search: string) => void,
    getMoreTags: () => void,
}

const mapStateToProps = (state: ApplicationState, ownProps: IProps): StateProps => {
    const tags = state.tags!;
    return {
        tags: tags.data,
        hasMore: tags.data.length !== tags.totalCount,
    }
}

const mapDispatchToProps = (dispatch: ThunkDispatch<{}, {}, any>, ownProps: IProps): DispatchProps => {
    return {
        getTags: (search: string) => dispatch(getTags(search)),
        getMoreTags: () => dispatch(getMoreTags()),
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(TagList);
