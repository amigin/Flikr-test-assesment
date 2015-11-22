/// <reference path="typings/jquery/jquery.d.ts" />
interface IImageLoaderSettings {
    requestUrl : string;
}

var ireq = 5;
class ImageRequester {

    private settings:IImageLoaderSettings;

    private isRequesting = false;
    
    private requestedVal = "";
    init(settings: IImageLoaderSettings) {
        this.settings = settings;
    }


    private stopRequesting() {
        this.isRequesting = false;
        $('#loadingIndicator').hide();

    }

    tagKeyUp() {

        if (this.isRequesting)
            return;

        this.isRequesting = true;

        var linkJ = $('#link');

        setTimeout(() => {

            this.requestedVal = $("#tags").val();
            if (this.requestedVal == undefined || this.requestedVal.length < 3) {
                this.stopRequesting();
                linkJ.css('display', 'none');
                return;
            }

            linkJ.attr("href", $('#link').attr("data-href") +"/"+this.requestedVal);
            linkJ.css('display', 'inline');

            var imgData = imagesCache.get(this.requestedVal);

            if (imgData) {
                this.stopRequesting();
                ui.renderImages(imgData);
                ui.showMessage("Loaded from cache...");
                return;
            }

            var url = this.settings.requestUrl + "?" + $("#searchForm").serialize();
            $('#loadingIndicator').show();
            $.post(url)
                .then(data => {
                    this.stopRequesting();
                    var val = $("#tags").val();
                    if (this.requestedVal === val) {
                        imagesCache.set(val, data);
                        ui.renderImages(data);
                        ui.showMessage("Loaded from flickr...");
                    }
                    else
                        this.tagKeyUp();
                })
                .fail(() => {
                    this.stopRequesting();
                });
        }, 250);

    }
}

var imageRequester = new ImageRequester();


$(window).ready(() => {
    $("#tags").keyup(() => imageRequester.tagKeyUp());
});
