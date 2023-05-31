class Index {

    // user provided
    #searchElement;
    #tableElement;
    #createRowFunction;
    #dataUrl;

    // calculated
    #containerElement;
    #debouncedFetch;

    // config contains:
    // - tableElement: $(table) containing the index
    // - createRowFunction: function(rowData) returning a $(tr)
    // - searchElement: $(input) that on change should query the data
    // - dataUrl: endpoint for fetching data
    constructor(config) {
        this.#tableElement = config.tableElement;
        this.#createRowFunction = config.createRowFunction;
        this.#searchElement = config.searchElement;
        this.#dataUrl = config.dataUrl;

        this.#containerElement = config.tableElement.find("tbody");
        if (this.#containerElement.length == 0) {
            this.#containerElement = $("<tbody>");
            config.tableElement.append(this.#containerElement);
        }

        this.#debouncedFetch = Common.debounce((search) => this.#fetchAndPopulateAsync(search));

        config.searchElement.keyup(() => this.refresh());

        this.refresh();
    }

    refresh() {
        this.#setIsLoading();
        this.#debouncedFetch(this.#searchElement.val());
    }

    #setIsLoading() {
        if (!this.#tableElement.hasClass('loading')) {
            this.#tableElement.addClass('loading');
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
        this.#tableElement.removeClass('loading');
    }

    #setIsError() {
        this.#containerElement.html(
            `<tr><td colspan='${this.#getColumnCount()}'>
                <div class='alert alert-danger my-0' role='alert'>
                    <i class='fas fa-bug fa-fw me-2'></i>
                    The data failed to load.  Check the console for details.
                </div>
            </td></tr>`);
    }

    #getColumnCount() {
        var columnCount = 0;
        this.#tableElement.find("tr:first-child").children().each((_, e) => {
            columnCount += e.colSpan;
        });
        return Math.max(columnCount, 1);
    }

    async #fetchAndPopulateAsync(search) {
        try {
            var results = await Common.fetchAsync({
                url: this.#dataUrl,
                data: {
                    search: search.replace(/^#/, ''),
                },
            });
        }
        catch (ex) {
            this.#setIsError();
            console.error(ex);
            return;
        }

        // if search has changed, discard our results
        if (this.#searchElement.val() != search) {
            return;
        }

        this.#setIsNotLoading();
        this.#containerElement.empty();
        results.data.forEach((item) => {
            this.#containerElement.append(this.#createRowFunction(item));
        });

        if (results.nextCursor != null) {
            this.#containerElement.append($(
                `<tr><td colspan='${this.#getColumnCount()} class='text-center'>
                    <em>Additional results are not listed.</em>
                </td></tr>`));
        }
    }
}