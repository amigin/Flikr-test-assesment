/// <reference path="typings/jquery/jquery.d.ts" />
var DataCache = (function () {
    function DataCache() {
        this.cache = new Array();
    }
    DataCache.prototype.get = function (key) {
        return this.cache[key];
    };
    DataCache.prototype.set = function (key, value) {
        this.cache[key] = value;
    };
    return DataCache;
})();
var imagesCache = new DataCache();
//# sourceMappingURL=DataCache.js.map