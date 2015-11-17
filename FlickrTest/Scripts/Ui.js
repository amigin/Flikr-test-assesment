/// <reference path="typings/jquery/jquery.d.ts" />
var Ui = (function () {
    function Ui() {
        this.id = 0;
    }
    Ui.prototype.getPopupDiv = function () {
        if (!this.popupDiv)
            this.popupDiv = $('#popupMessage');
        return this.popupDiv;
    };
    Ui.prototype.centerPopup = function () {
        var popupDiv = this.getPopupDiv();
        var w = window.innerWidth;
        popupDiv.css({ left: ((w * 0.5) - (popupDiv.width() * 0.5)) + "px" });
    };
    Ui.prototype.showMessage = function (message) {
        var _this = this;
        var popupDiv = this.getPopupDiv();
        popupDiv.css("opacity", 0);
        popupDiv.show();
        popupDiv.html(message);
        this.centerPopup();
        popupDiv.hide();
        popupDiv.css("opacity", 1);
        popupDiv.fadeIn(500, function () {
            if (_this.timeOutHandle)
                clearTimeout(_this.timeOutHandle);
            _this.timeOutHandle = setTimeout(function () {
                _this.timeOutHandle = undefined;
                popupDiv.fadeOut(500);
            }, 3000);
        });
    };
    Ui.prototype.renderImages = function (images) {
        var _this = this;
        var c = $(".container");
        c.html("");
        $.each(images, function (idx, item) {
            var thumb = $("<div>").addClass('thumbnail');
            var thumbPad = $('<div>').addClass('pad');
            var thumbPadImg = $('<div id="img' + _this.id + '">').addClass('thumb-img').css("display", "none");
            ImageLoader.loadImage({ div: '#img' + _this.id, url: item.url });
            _this.id++;
            var title = $('<div>').addClass("title")
                .html(item.title);
            thumb.append(thumbPad);
            thumbPad.append(thumbPadImg);
            thumbPadImg.append(title);
            c.append(thumb);
        });
    };
    Ui.prototype.getHeaderJ = function () {
        if (this.headerJ == undefined)
            this.headerJ = $('.header');
        return this.headerJ;
    };
    Ui.prototype.getContainerJ = function () {
        if (this.containerJ == undefined)
            this.containerJ = $('.container');
        return this.containerJ;
    };
    Ui.prototype.ressize = function () {
        //   var w = window.innerWidth;
        var h = window.innerHeight;
        var hj = this.getHeaderJ();
        var cj = this.getContainerJ();
        cj.css({ height: (h - hj.height()) + "px" });
    };
    return Ui;
})();
var ui = new Ui();
$(window).resize(function () { ui.ressize(); });
$(window).ready(function () { return ui.ressize(); });
