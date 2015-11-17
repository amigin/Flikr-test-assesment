/// <reference path="typings/jquery/jquery.d.ts" />
interface IImageModel {
    title: string;
    url: string;
}


class Ui {

    private id = 0;

    private popupDiv:JQuery;
    private getPopupDiv(): JQuery {
        if (!this.popupDiv)
            this.popupDiv = $('#popupMessage');
        return this.popupDiv;
    }


    private centerPopup() {
        var popupDiv = this.getPopupDiv();
        var w = window.innerWidth;
        popupDiv.css({ left: ((w*0.5) - (popupDiv.width() * 0.5))+"px"})  ;
    }
    

    private timeOutHandle : number;
    showMessage(message: string) {

        var popupDiv = this.getPopupDiv();
        popupDiv.css("opacity", 0);
        popupDiv.show();
        popupDiv.html(message);
        this.centerPopup();
        popupDiv.hide();
        popupDiv.css("opacity", 1);


        popupDiv.fadeIn(500, () => {

            if (this.timeOutHandle)
                clearTimeout(this.timeOutHandle);

            this.timeOutHandle = setTimeout(() => {
                this.timeOutHandle = undefined;
                popupDiv.fadeOut(500);
            }, 3000);

        });

    }

    renderImages(images: IImageModel[]) {

    
        var c = $(".container");

            c.html("");
            $.each(images, (idx, item: IImageModel) => {

                var thumb = $("<div>").addClass('thumbnail');

                var thumbPad = $('<div>').addClass('pad');

                var thumbPadImg = $('<div id="img' + this.id + '">').addClass('thumb-img').css("display", "none");

                ImageLoader.loadImage({ div: '#img' + this.id, url: item.url });

                this.id++;
                var title = $('<div>').addClass("title")
                    .html(item.title);

                thumb.append(thumbPad);
                thumbPad.append(thumbPadImg);
                thumbPadImg.append(title);
                c.append(thumb);
  
            });


    }

    private headerJ :JQuery;
    private getHeaderJ():JQuery {
        if (this.headerJ == undefined)
            this.headerJ = $('.header');
        return this.headerJ;
    }

    private containerJ: JQuery;
    private getContainerJ(): JQuery {
        if (this.containerJ == undefined)
            this.containerJ = $('.container');
        return this.containerJ;
    }


    ressize() {
     //   var w = window.innerWidth;
        var h = window.innerHeight;
        var hj = this.getHeaderJ();
        var cj = this.getContainerJ();

        cj.css({ height: (h - hj.height()) + "px" });
    }

}


var ui = new Ui();
$(window).resize(() => { ui.ressize(); });
$(window).ready(() => ui.ressize());