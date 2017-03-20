$().ready(function () {


    //---------------------------------------------
    //       Upload Restrictions on Profile Pics
    //---------------------------------------------
    $('#ppUpload').attr('disabled', true);

    $('#ppFile').bind('change', function () {
        var errorMsg = $('#ppfileError');
        var btn = $('#ppUpload');
        errorMsg.html("");

        var size = this.files[0].size;
        var fname = this.files[0].name;
        var ext = fname.substr(fname.lastIndexOf('.') + 1);
        var accExt = ['jpg', 'jpeg', 'png'];

        if ((size > 0 && size <= 250000) && (accExt.indexOf(ext) > -1)) {
            btn.attr('disabled', false);
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
        var btn = $('#scUpload');
        errorMsg.html("");

        var size = this.files[0].size;
        var fname = this.files[0].name;
        var ext = fname.substr(fname.lastIndexOf('.') + 1);
        var accExt = ['jpg', 'jpeg', 'png'];

        if ((size > 0 && size <= 1000000) && (accExt.indexOf(ext) > -1)) {
            btn.attr('disabled', false);
        }
        else if (size > 1000000) {
            errorMsg.append("<p class=\"alert alert-danger\" role=\"alert\">"
                + "Your file is too big. Please upload a file no larger than 1MB.</p>");
        }
        else if (size <= 500000) {
            errorMsg.append("<p class=\"alert alert-danger\" role=\"alert\">"
                + "Your file is too small. Please upload a file bewteen 500KB and 1MB.</p>");
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
        var btn = $('#slUpload');
        errorMsg.html("");

        var size = this.files[0].size;
        var fname = this.files[0].name;
        var ext = fname.substr(fname.lastIndexOf('.') + 1);
        var accExt = ['jpg', 'jpeg', 'png'];

        if ((size > 0 && size <= 250000) && (accExt.indexOf(ext) > -1)) {
            btn.attr('disabled', false);
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
    //    Validate School Url Edits
    //------------------------------------------------------
    $('#schUrl').on("keyup", function () {
        var inputVal = $(this).val();
        var regex = /^http(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?$/;

        if (!regex.test(inputVal)) {
            $("#urlError").append("Please use the full url (includes \"http://\" or \"https://\".)");
        }
        else {
            $("#urlError").innerHTML = "";
        }
    });

    //------------------------------------------------------
    //    Trip Menu Buttons
    //      http://codepen.io/apocheau/pen/EKeZgg
    //------------------------------------------------------

    $("#tripMenuMain").click(function () {
        $("#mini-fab").toggleClass('hidden');
    });

    $("#editTrip").hide();

    $("#edit").click(function () {
        $("#tripDetails").hide();
        $("#editTrip").show();
    });

    $('[data-toggle="tooltip"]').tooltip();

    $.material.init();


});


