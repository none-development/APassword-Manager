function Trolling(){
    console.log("%cThis is my No No Square")
}

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
})('https://data.whicdn.com/images/208532956/original.gif');