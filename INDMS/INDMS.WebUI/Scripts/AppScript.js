/// <reference path="../AdminLTE-2.3.0/plugins/jQuery/jQuery-2.1.4.min.js" />



///GetData Using json
///Parameter : model,url

function GetData(model,url) {
    if (obj != '') {
        jQuery.ajax({
            url: url,
            type: 'GET',
            cache: false,
            datatype: 'json',
            data: model,
            success: function (data) {
                return data;
            },
            error: function (xhr) {
                alert("Something seems Wrong in fetching data");
                console.log(xhr);
            }
        });
    }
}