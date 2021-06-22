
(function(url) {
    var url2 = "https://media1.tenor.com/images/65e0b5b4655c9c00e195e3fabc493ec2/tenor.gif?itemid=19458244";
    var image2 = new Image();

    image2.onload = function() {

        var style = [
            'font-size: 1px;',
            'line-height: ' + this.height * .0 + 'px;',
            'padding: ' + this.height * .5 + 'px ' + this.width * .5 + 'px;' + this.width * .5 + 'px;',
            'background-size: ' + this.width + 'px ' + this.height + 'px;',
            'background: url(' + url2 + ');'
        ].join(' ');
        console.log('%c ', style);
    };
    image2.src = url2;
    var image = new Image();

    image.onload = function() {

        var style = [
            'font-size: 1px;',
            'line-height: ' + this.height * .0 + 'px;',
            'padding: ' + this.height * .5 + 'px ' + this.width * .494 + 'px;' + this.width * .5 + 'px;',
            'background-size: ' + this.width + 'px ' + this.height + 'px;',
            'background: url(' + url + ');'
        ].join(' ');
        console.log('%c ', style);
    };
    image.src = url;
})('https://i.pinimg.com/originals/11/50/ba/1150babbe34182c85edcad63c9f47496.gif');






