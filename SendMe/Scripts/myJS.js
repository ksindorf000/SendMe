$().ready(function () {


    //---------------------------------------------
    //       Upload Restrictions on Profile Pics
    //---------------------------------------------
    $('#ppUpload').attr('disabled', true);

    $('#ppFile').bind('change', function () {
        var errorMsg = $('#ppfileError');
        errorMsg.html("");

        var size = this.files[0].size;
        var fname = this.files[0].name;
        var ext = fname.substr(fname.lastIndexOf('.') + 1);
        var accExt = ['jpg', 'jpeg', 'png'];

        if ((size > 0 && size <= 250000) && (accExt.indexOf(ext) > -1)) {
            $('#ppUpload').attr('disabled', false);
            alert(size);
        }
        else if (size > 250000) {
            errorMsg.append("<p class=\"alert alert-danger\" role=\"alert\">"
                + "Your file is too big. Please upload a file no larger than 250KB.</p>");
        }
        else if (accExt.indexOf(ext) <= -1) {
            errorMsg.append("<p class=\"alert alert-danger\" role=\"alert\">"
                + "Sorry, we can't use that file type. Please see the list of acceptable file types below.</p>");
        }
    });

    //--------------------------------------------------------
    //       Upload Restrictions on School CoverImg
    //---------------------------------------------------------
    $('#scUpload').attr('disabled', true);

    $(document).on("change", '#scFile', function () {
        var errorMsg = $('#scfileError');
        errorMsg.html("");

        var size = this.files[0].size;
        var fname = this.files[0].name;
        var ext = fname.substr(fname.lastIndexOf('.') + 1);
        var accExt = ['jpg', 'jpeg', 'png'];

        if ((size > 0 && size <= 500000) && (accExt.indexOf(ext) > -1)) {
            $('#scUpload').attr('disabled', false);
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

    //------------------------------------------------------
    //       Upload Restrictions on School Logo
    //------------------------------------------------------
    $('#slUpload').attr('disabled', true);

    $('#slFile').bind('change', function () {
        var errorMsg = $('#slfileError');
        errorMsg.html("");

        var size = this.files[0].size;
        var fname = this.files[0].name;
        var ext = fname.substr(fname.lastIndexOf('.') + 1);
        var accExt = ['jpg', 'jpeg', 'png'];

        if ((size > 0 && size <= 250000) && (accExt.indexOf(ext) > -1)) {
            $('#slUpload').attr('disabled', false);
            alert(size);
        }
        else if (size > 250000) {
            errorMsg.append("<p class=\"alert alert-danger\" role=\"alert\">"
                + "Your file is too big. Please upload a file no larger than 250KB.</p>");
        }
        else if (accExt.indexOf(ext) <= -1) {
            errorMsg.append("<p class=\"alert alert-danger\" role=\"alert\">"
                + "Sorry, we can't use that file type. Please see the list of acceptable file types below.</p>");
        }
    });

    //------------------------------------------------------
    //    Validate School Url Edits (not working 03/16)
    //------------------------------------------------------
    $('#schUrl').on("keyup", function () {
        var inputVal = $(this).val();
        var regex = /^http(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?$/;

        if (!regex.test(inputVal)) {
            $("#urlError").append("Please use the full url (includes \"http://\" or \"https://\".)");
        }
        else {
            document.getElementById("urlError").innerHTML = "";
        }
    });

});
