/// <reference path="typings/jquery/jquery.d.ts" />
class DataCache<T> {

    private cache = new Array();

    get(key: string):T {
        return this.cache[key];
    }

    set(key: string, value:T) {
        this.cache[key] = value;
    }

}

var imagesCache = new DataCache<IImageModel[]>();