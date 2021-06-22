$(document).ready(function() {
    $(window).bind('scroll', function() {
        var navHeight = $(window).height() - 70;
        if ($(window).scrollTop() > navHeight) {
            document.title = "Projects...🔎";
            $('nav').addClass('navfadein fixed background');
            $('body').removeClass('returnblack');
            $('nav').removeClass('returnblack');
            $('#screen2').addClass('background2 backgroundchzange');
            $('#screen3').addClass('background2 backgroundchzange');
            $('#screen4').addClass('background2 backgroundchzange');
            $('body').addClass('background2 backgroundchzange');
            $('#textcolor').addClass('color');
            $('#textcolor1').addClass('color');
            $('#textcolor2').addClass('color');
            $('#textcolor3').addClass('color');
            $('#textcolor4').addClass('color');
        } else {
            document.title = "APM";
            $('nav').removeClass('fixed navfadein background');
            $('nav').addClass('returnblack');
            $('body').removeClass('background2 backgroundchzange');
            $('body').addClass('returnblack');
            $('#screen2').removeClass('background2 backgroundchzange');
            $('#screen3').removeClass('background2 backgroundchzange');
            $('#screen4').removeClass('background2 backgroundchzange');
            $('#textcolor').removeClass('color');
            $('#textcolor1').removeClass('color');
            $('#textcolor2').removeClass('color');
            $('#textcolor3').removeClass('color');
            $('#textcolor4').removeClass('color');
        }
    });
    $(window).scroll(function() {
        if($(window).scrollTop() + $(window).height() > $(document).height() - 100) {
            document.title = "is this... the End?";
        }
     });
});

var scene = new Scene({
    ".searchbox": {
        "0%": "width: 50px",
        "70%": "width: 300px",
    },
    ".line": {
        "30%": "width: 0%",
        "100%": "width: 100%",
    }
}, {
    duration: 1,
    easing: Scene.EASE_IN_OUT,
    selector: true,
}).exportCSS();

scene.setTime(0);
var toggle = false;
