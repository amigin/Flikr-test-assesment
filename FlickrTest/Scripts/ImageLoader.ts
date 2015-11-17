/// <reference path="typings/jquery/jquery.d.ts" />
interface IImageLoadingProcess {
    div: string;
    url:string;
}


class ImageLoader {

    static loadImage(data: IImageLoadingProcess) {
        var img = new Image();
        img.onload = () => {
            $(data.div).css("background-image", 'url(' + img.src + ')');
            $(data.div).fadeIn(500);
        };
        img.src = data.url;
    }
}

