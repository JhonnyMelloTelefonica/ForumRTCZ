window.ChangeInputFileByLabel = function () {
    $('#btn_myFileInput').data('default', $('label[for=btn_myFileInput]').text()).click(function () {
        $('#myFileInput').click()
    });
    $('#myFileInput').on('change', function () {
        var files = this.files;
        if (!files.length) {
            $('label[for=btn_myFileInput]').text($('#btn_myFileInput').data('default'));
            return;
        }
        $('label[for=btn_myFileInput]').empty();
        for (var i = 0, l = files.length; i < l; i++) {
            $('label[for=btn_myFileInput]').append(files[i].name + '\n');
        }
    });
};

window.fileInputInterop = {
    clickFileInput: function () {
        var fileInput = document.getElementById('myFileInput');
        if (fileInput) {
            fileInput.click();
        }
    }
};