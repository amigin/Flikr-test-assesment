/// <reference path="typings/jquery/jquery.d.ts" />
var ImageLoader = (function () {
    function ImageLoader() {
    }
    ImageLoader.loadImage = function (data) {
        var img = new Image();
        img.onload = function () {
            $(data.div).css("background-image", 'url(' + img.src + ')');
            $(data.div).fadeIn(500);
        };
        img.src = data.url;
    };
    return ImageLoader;
})();
