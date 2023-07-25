var Common = {

    debounce: function (callback) {
        const delay_ms = 250;
        var timer;
        return function () {
            timer && clearTimeout(timer);
            timer = setTimeout(
                () => callback.apply(this, arguments),
                delay_ms
            );
        }
    },

    errorModal: function (exception) {
        console.log(exception);
        new bootstrap.Modal('#error-modal').show();
    },

    fetchAsync: function (options) {
        var modal = new bootstrap.Modal('#login-modal');

        return new Promise((resolve) => {

            async function attemptFetchAsync() {
                try {
                    $('#login-modal-retry-button')[0].disabled = true;
                    $('#login-modal-spinner').removeClass('d-none');

                    var result = await $.ajax(options);

                    modal.hide();

                    resolve(result);
                }
                catch (ex) {
                    if (ex.status == 401) {
                        showLoginModal();
                    } else {
                        throw ex;
                    }
                }
            }

            function showLoginModal() {
                modal.show();
                $('#login-modal-retry-button')[0].disabled = false;
                $('#login-modal-spinner').addClass('d-none');
                $('#login-modal-retry-button').off().click(attemptFetchAsync);
            }

            attemptFetchAsync();
        });
    },

};