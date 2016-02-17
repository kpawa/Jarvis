// TODO: Replace with the URL of your WebService app
var serviceUrl = 'http://jarvis.kpawa.com/Home/api/Providers'
// http://www.asp.net/web-api/overview/security/enabling-cross-origin-requests-in-web-api
function sendRequest() {
    var method = $('#method').val();
    $.ajax({
        type: method,
        url: serviceUrl
    }).done(function (data) {
        for (var i = 0; i < data.providerInfo.length; i++) {
            callback(data.providerInfo[i]);
        }
    }).error(function (jqXHR, textStatus, errorThrown) {
        //$('#value1').text(jqXHR.responseText || textStatus);
    });
}

function callback(val) {
    //  $("#manufacturers").replaceWith("<span id='value1'>(Result)</span>");
    $("#value").replaceWith("<ul id='manufacturers' />");
    var str = "Username: " + val.username + " Provider: " + val.provider;
    $('<li/>', { text: str }).appendTo($('#manufacturers'));
}