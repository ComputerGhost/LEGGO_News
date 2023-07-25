class Index {

    // user provided
    #searchElement;
    #columns;
    #fetchAsyncFunction;

    // calculated
    #containerElement;
    #debouncedFetch;
    #isLoading = false;

    constructor(config) {
        this.#searchElement = config.searchElement;
        this.#columns = config.columns;
        this.#fetchAsyncFunction = config.fetchAsyncFunction;

        config.containerElement = $("#results");

        this.#debouncedFetch = Common.debounce((search) => this.#fetchAndPopulateAsync(search));

        config.searchElement.keyup(() => this.refresh());

        this.refresh();
    }

    refresh() {
        this.#setIsLoading();
        this.#debouncedFetch(this.#searchElement.val());
    }

    #setIsLoading() {
        if (this.#isLoading === false) {
            this.#isLoading = true;
            this.#containerElement.html(
                `<tr><td colspan='${this.#getColumnCount()}' class='text-secondary'>
                    <span class='spinner-border spinner-border-sm me-2' role='status'></span>
                    <span id='loadingStatus'></span>
                </td></tr>`);
        }

        // build this part separately to escape html in the input
        $('#loadingStatus').text((this.#searchElement.val().length > 0)
            ? `Searching for ${this.#searchElement.val()}...`
            : 'Loading data...');
    }

    #setIsNotLoading() {
        this.#isLoading = false;
    }

    #setIsError(exception) {
        this.#containerElement.empty();
        Common.errorModal(exception);
    }

    #getColumnCount() {
        return this.#columns.length + 1;
    }

    #appendDataRow(rowData) {
        var renameButton = $("<a href='#' class='rename-button px-2' data-bs-toggle='tooltip'><i class='fas fa-pen-to-square'></i></a>");

        var deleteButton = $("<a href='#' class='delete-button text-danger px-2' data-bs-toggle='tooltip'><i class='fas fa-trash'></i></a>");

        var tr = $("<tr>");
        this.#columns.forEach(c => {
            tr.append($("<td>").text(rowData[c.member]));
        });
        tr.append($("<td class='text-end'>").append(renameButton).append(deleteButton));

        this.#containerElement.append(tr);
    }

    async #fetchAndPopulateAsync(search) {
        try {
            var results = await this.#fetchAsyncFunction();
        }
        catch (exception) {
            this.#setIsError(exception);
            return;
        }

        // if search has changed, discard our results
        if (this.#searchElement.val() != search) {
            return;
        }

        this.#setIsNotLoading();
        this.#containerElement.empty();
        results.data.forEach((item) => {
            this.#appendDataRow(item);
        });

        if (results.nextCursor != null) {
            this.#containerElement.append($(
                `<tr><td colspan='${this.#getColumnCount()} class='text-center'>
                    <em>Additional results are not listed.</em>
                </td></tr>`));
        }
    }
}