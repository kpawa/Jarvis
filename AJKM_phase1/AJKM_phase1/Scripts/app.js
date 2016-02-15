$(document).ready(function () {
    $("#btnJarvisGet").click(function () {
        var actionUrl = "http://jarvis.kpawa.com//Home/GetJarvisAPI";
        $.getJSON(actionUrl, displayData);
    });

    function displayData(response) {
        if (response != null) {
            for (var i = 0; i < response.length; i++) {
                $("#userList").append("<li>" + response[i].UserName + " "
                + response[i].Email + "</li>")
            }
        }
    }
});
