$().ready(function () {

    //----------------------------------
    //       Upload Restrictions
    //----------------------------------
    $('#Upload').attr('disabled', true);

    $('#myFile').bind('change', function () {
        var errorMsg = $("#fileError");
        errorMsg.html("");

        var size = this.files[0].size;
        var fname = this.files[0].name;
        var ext = fname.substr(fname.lastIndexOf('.') + 1);
        var accExt = ["jpg", "jpeg", "png"];

        if ((size > 0 && size <= 500000) && (accExt.indexOf(ext) > -1)) {
            $('#Upload').attr('disabled', false);
            alert(size);
        }
        else if (size > 500000) {
            errorMsg.append("<p class=\"alert alert-danger\" role=\"alert\">"
                + "Your file is too big. Please upload a file no larger than 500KB.</p>");
        }
        else if (accExt.indexOf(ext) <= -1) {
            errorMsg.append("<p class=\"alert alert-danger\" role=\"alert\">"
                + "Sorry, we can't use that file type. Please see the list of acceptable file types below.</p>");
        }

    });

});

