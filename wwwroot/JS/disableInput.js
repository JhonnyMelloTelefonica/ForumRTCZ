window.disableMatriculaInput = function () {
    // Desabilitar o input com a classe .matricula-input
    var matriculaInput = document.querySelector('.matricula-input');
    if (matriculaInput) {
        matriculaInput.disabled = true;
    }
};

window.disableAllInput = function () {
    // Desabilitar o input com a classe .matricula-input
    var matriculaInput = document.querySelector('input');
    if (matriculaInput) {
        matriculaInput.disabled = true;
    }
};