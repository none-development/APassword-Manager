
(function(url) {

    var image = new Image();

    image.onload = function() {

        var style = [
            'font-size: 1px;',
            'line-height: ' + this.height * .0 + 'px;',
            'padding: ' + this.height * .5 + 'px ' + this.width * .5 + 'px;' + this.width * .5 + 'px;',
            'background-size: ' + this.width + 'px ' + this.height + 'px;',
            'background: url(' + url + ');'
        ].join(' ');
        console.log('%c ', style);
    };
    image.src = url;
    console.log("DANCE");
})('https://i.pinimg.com/originals/49/26/52/492652d5649bb403be09e0ea3d04fbe7.gif');


(function(url) {

    var image = new Image();

    image.onload = function() {

        var style = [
            'position: relative;',
            'font-size: 24px;'
        ].join(' ');
        console.log('%cHello', style);
    };
    image.src = url;
    console.log("DANCE");
})('https://i.pinimg.com/originals/49/26/52/492652d5649bb403be09e0ea3d04fbe7.gif');