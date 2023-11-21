Utils = new function () {
    function rootUrl() {
        const rooturl = window.location.origin + "/";
        return rooturl;
    };
    this.UrlRoot = rootUrl();
}