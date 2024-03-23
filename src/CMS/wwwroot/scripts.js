$(document).ready(function () {
    const SMALL_INLINE_GIF =
        "data:image/gif;base64,R0lGODlhAQABAIAAAP///wAAACH5BAEAAAAALAAAAAABAAEAAAICRAEAOw==";

    function ImageUploadInput($element) {
        this.$img = $("<img>", {
            alt: "Album art",
            class: "img-thumbnail",
            src: $element.data("existing-image-uri") || SMALL_INLINE_GIF,
        });
        this.$img.insertAfter($element);

        $element.change((event) => {
            if (event.target.files.length > 0) {
                var reader = new FileReader();
                reader.onload = () => this.$img.attr("src", reader.result);
                reader.readAsDataURL(event.target.files[0]);
            }
        });
    }

    var imageUploadInputs = $("input[type=file][accept^='image/']");
    imageUploadInputs.each((_, element) => new ImageUploadInput($(element)));
});
