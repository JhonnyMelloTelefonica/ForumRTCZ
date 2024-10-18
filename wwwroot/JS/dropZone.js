export function initializeFileDropZone(dropZoneElement, inputFile) {
    // Add a class when the user drags a file over the drop zone
    function onDragHover(e) {
        e.preventDefault();
        dropZoneElement.classList.add("hover");
    }

    function onDragLeave(e) {
        e.preventDefault();
        dropZoneElement.classList.remove("hover");
    }

    // Handle the paste and drop events
    function onDrop(e) {
        e.preventDefault();
        dropZoneElement.classList.remove("hover");

        // Set the files property of the input element and raise the change event
        inputFile.files = e.dataTransfer.files;
        const event = new Event('change', { bubbles: true });
        inputFile.dispatchEvent(event);
    }

    function onPaste(e) {
        // Set the files property of the input element and raise the change event
        inputFile.files = e.clipboardData.files;
        const event = new Event('change', { bubbles: true });
        inputFile.dispatchEvent(event);
    }

    // Register all events
    dropZoneElement.addEventListener("dragenter", onDragHover);
    dropZoneElement.addEventListener("dragover", onDragHover);
    dropZoneElement.addEventListener("dragleave", onDragLeave);
    dropZoneElement.addEventListener("drop", onDrop);
    dropZoneElement.addEventListener('paste', onPaste);

    // The returned object allows to unregister the events when the Blazor component is destroyed
    return {
        dispose: () => {
            dropZoneElement.removeEventListener('dragenter', onDragHover);
            dropZoneElement.removeEventListener('dragover', onDragHover);
            dropZoneElement.removeEventListener('dragleave', onDragLeave);
            dropZoneElement.removeEventListener("drop", onDrop);
            dropZoneElement.removeEventListener('paste', onPaste);
        }
    }
}

window.blazorExtensions = {
    GetFileData: async function (target) {
        var fileResult = [];

        var target = document.getElementById(target.id);
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