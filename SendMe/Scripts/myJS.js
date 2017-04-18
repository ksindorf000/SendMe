$().ready(function () {

    //---------------------------------------------
    //      Search in NavBar
    //      http://bootsnipp.com/snippets/featured/inline-navbar-search
    //---------------------------------------------
    // Remove Search if user Resets Form or hits Escape!
    $('body, .navbar-collapse form[role="search"] button[type="reset"]').on('click keyup', function (event) {
        if (event.which == 27 && $('.navbar-collapse form[role="search"]').hasClass('active') ||
            $(event.currentTarget).attr('type') == 'reset') {
            closeSearch();
        }
    });

    function closeSearch() {
        var $form = $('.navbar-collapse form[role="search"].active')
        $form.find('input').val('');
        $form.removeClass('active');
    }

    // Show Search if form is not active // event.preventDefault() is important, this prevents the form from submitting
    $(document).on('click', '.navbar-collapse form[role="search"]:not(.active) button[type="submit"]', function (event) {
        event.preventDefault();
        var $form = $(this).closest('form'),
            $input = $form.find('input');
        $form.addClass('active');
        $input.focus();
    });
    
    $('.search').submit(function (event) {
        event.preventDefault();

        var $form = $(this).closest('form'),
            $input = $form.find('input');

        //////////////////////////////////////////////////////////////
        // ----------- WILL NEED TO CHANGE THIS URL! ------------- //
        url = 'http://uscmed.azurewebsites.net/school/1/?searchstring=' + $input.val();
        //////////////////////////////////////////////////////////////            

        window.location = url;
        
        closeSearch();
    });

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
    //   Donate Button on School Profile
    //------------------------------------------------------
    $("#schDonateToggle").hide();

    $(".donateSch").click(function () {
        $("#sch-StuProfile-cards").hide();
        $("#schDonateToggle").show();
        tripId = $(this).data("id");
        userName = $(this).data("username");        
    });

    $("#paymentBackBtn").click(function () {
        location.reload();
    });
    
    
    //------------------------------------------------------
    //    Trip Menu Buttons
    //      http://codepen.io/apocheau/pen/EKeZgg
    //------------------------------------------------------
    //Expand Menu
    $("#tripMenuMain").click(function () {
        $("#mini-fab").toggleClass('hidden');
    });

    //Donation logged in
    $("#donateToggle").hide();
    $("#map").show();

    $("#donateBtn, #donateMain").click(function () {
        $(".tripDetails").hide();
        $("#donateToggle").show();
        $("#mini-fab").toggleClass('hidden');
        $("#map").hide();
    });

    //Donation logged out
    $("#donateBtnLO").click(function () {
        $(".tripDetails").hide();
        $("#donateToggle").show();
        $("#mini-fab").toggleClass('hidden');
    });

    //ViewDonations
    $("#viewDonationsToggle").hide();

    $("#viewDonationsBtn").click(function () {
        $(".tripDetails").hide();
        $("#viewDonationsToggle").show();
        $("#mini-fab").toggleClass('hidden');
    });

    $("#viewDonBackBtn").click(function () {
        $("#viewDonationsToggle").hide();
        $(".tripDetails").show();
        $("#map").show();
    });

    //ViewDonations
    $("#editTripToggle").hide();

    $("#editTripBtn").click(function () {
        $(".tripDetails").hide();
        $("#editTripToggle").show();
        $("#mini-fab").toggleClass('hidden');
    });

    //Cancel Trip 
    $("#confirmCancel").hide();

    $("#cancel").click(function () {
        $("#confirmCancel").show();
        $("#mini-fab").toggleClass('hidden');
        $("#map").hide();
    });

    $("#dontCancel").click(function () {
        $("#confirmCancel").hide();
        $("#map").show();
    });

    //Edit Trip
    $("#editTripBtn").click(function () {
        $("#editTripToggle").show();
    });

    //ToolTips
    $('[data-toggle="tooltip"]').tooltip();

    $.material.init();

});


//------------------------------------------------------
//      Contact Form
//------------------------------------------------------
$(document).ready(function () {
    $('#contact_form').bootstrapValidator({
        // To use feedback icons, ensure that you use Bootstrap v3.1.0 or later
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            name: {
                validators: {
                    stringLength: {
                        min: 2,
                    },
                    notEmpty: {
                        message: 'Please supply your first name'
                    }
                }
            },
            email: {
                validators: {
                    notEmpty: {
                        message: 'Please supply your email address'
                    },
                    emailAddress: {
                        message: 'Please supply a valid email address'
                    }
                }
            },
            contactMsg: {
                validators: {
                    stringLength: {
                        min: 10,
                        max: 200,
                        message: 'Please enter at least 10 characters and no more than 200'
                    },
                    notEmpty: {
                        message: 'Please supply a description of your project'
                    }
                }
            }
        }
    })
        .on('success.form.bv', function (e) {
            $('#success_message').slideDown({ opacity: "show" }, "slow") // Do something ...
            $('#contact_form').data('bootstrapValidator').resetForm();

            // Prevent form submission
            e.preventDefault();

            // Get the form instance
            var $form = $(e.target);

            // Get the BootstrapValidator instance
            var bv = $form.data('bootstrapValidator');

            // Use Ajax to submit form data
            $.post($form.attr('action'), $form.serialize(), function (result) {
                console.log(result);
            }, 'json');
        });

});

//Donation partial list accordian show each/ hide each/back button




