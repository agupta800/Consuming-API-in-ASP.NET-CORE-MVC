$(function () {
    if ($('div.alert.notification').length){
        setTimeout(() => {
            $('div.alert.notification').fadeOut();
        
        }, 3000)

    }
});



function displayBusyIndication() {
    $('.loading').show();
}

function hideBusyIndication() {
    $('.loading').hide();
}

$(document).ready(function () {
    $(window).on('beforeunload', function () {
        displayBusyIndication();
    });

    $(document).on('submit', 'form', function () {
        displayBusyIndication();
    });

    window.setTimeout(function () {
        hideBusyIndication();
    }, 3000);
});

$(window).on('load', function () {
    hideBusyIndication();
});
