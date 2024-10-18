window.blazorExtensions = {
    GetFileData: async function (id) {
        var fileResult = [];

        var target = document.getElementById(id);
        var filesArray = Array.from(target.files);

        if (filesArray.length > 0) {
            var results = await Promise.all(filesArray.map(window.blazorExtensions.fileToDataURL));

            for (var i = 0; i < results.length; i++) {
                var fileData = {
                    FileName: filesArray[i].name,
                    data: results[i]
                };
                fileResult.push(fileData);
            }
        }

        return fileResult;
    },
    fileToDataURL: async function (file) {
        var reader = new FileReader();

        return new Promise(function (resolve, reject) {
            reader.onerror = function () {
                reader.abort();
                reject(new DOMException('Error occurred reading file ' + file.name));
            };
            reader.onload = function (event) {
                resolve(reader.result);
            };
            reader.readAsDataURL(file);
        });
    },

    InvokeClick: function (id) {
        var elem = document.getElementById(id);
        if (typeof elem.onclick == "function") {
            elem.onclick.apply(elem);
        }
        elem.click();
    }
};
