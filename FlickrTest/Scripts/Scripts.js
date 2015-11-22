/// <reference path="typings/jquery/jquery.d.ts" />
var ireq = 5;
var ImageRequester = (function () {
    function ImageRequester() {
        this.isRequesting = false;
        this.requestedVal = "";
    }
    ImageRequester.prototype.init = function (settings) {
        this.settings = settings;
    };
    ImageRequester.prototype.stopRequesting = function () {
        this.isRequesting = false;
        $('#loadingIndicator').hide();
    };
    ImageRequester.prototype.tagKeyUp = function () {
        var _this = this;
        if (this.isRequesting)
            return;
        this.isRequesting = true;
        var linkJ = $('#link');
        setTimeout(function () {
            _this.requestedVal = $("#tags").val();
            if (_this.requestedVal == undefined || _this.requestedVal.length < 3) {
                _this.stopRequesting();
                linkJ.css('display', 'none');
                return;
            }
            linkJ.attr("href", $('#link').attr("data-href") + "/" + _this.requestedVal);
            linkJ.css('display', 'inline');
            var imgData = imagesCache.get(_this.requestedVal);
            if (imgData) {
                _this.stopRequesting();
                ui.renderImages(imgData);
                ui.showMessage("Loaded from cache...");
                return;
            }
            var url = _this.settings.requestUrl + "?" + $("#searchForm").serialize();
            $('#loadingIndicator').show();
            $.post(url)
                .then(function (data) {
                _this.stopRequesting();
                var val = $("#tags").val();
                if (_this.requestedVal === val) {
                    imagesCache.set(val, data);
                    ui.renderImages(data);
                    ui.showMessage("Loaded from flickr...");
                }
                else
                    _this.tagKeyUp();
            })
                .fail(function () {
                _this.stopRequesting();
            });
        }, 250);
    };
    return ImageRequester;
})();
var imageRequester = new ImageRequester();
$(window).ready(function () {
    $("#tags").keyup(function () { return imageRequester.tagKeyUp(); });
});
//# sourceMappingURL=Scripts.js.map