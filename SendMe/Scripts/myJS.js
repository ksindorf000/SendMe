$().ready(function () {

    $('#Upload').attr('disabled', true);

    $('#myFile').bind('change', function () {
        var size = this.files[0].size;
        var fname = this.files[0].name;
        var ext = fname.substr(fname.lastIndexOf('.') + 1);

        if (this.files[0].size > 0) {
            $('#Upload').attr('disabled', false);
            alert(size + ", " + fname + ", " + ext);

        }
    });

});

