function ImageUploadInput($element) {
    const SMALL_INLINE_GIF =
        "data:image/gif;base64,R0lGODlhAQABAIAAAP///wAAACH5BAEAAAAALAAAAAABAAEAAAICRAEAOw==";

    this.$img = $("<img>", {
        alt: "Album art",
        class: "img-thumbnail",
        src: $element.data("existing-image-uri") || SMALL_INLINE_GIF,
    });

    var $validationError = $element.next();
    this.$img.insertAfter($validationError);

    $element.change((event) => {
        if (event.target.files.length > 0) {
            var reader = new FileReader();
            reader.onload = () => this.$img.attr("src", reader.result);
            reader.readAsDataURL(event.target.files[0]);
        }
    });
}

$(function () {
    var imageUploadInputs = $("input[type=file][accept^='image/']");
    imageUploadInputs.each((_, element) => new ImageUploadInput($(element)));
});
