
function ActivateCardView(user, dotNetObject, context=false) {
    dotNetObject.invokeMethodAsync('SetUserCard', user, context);
}

function CloseCardView(dotNetObject) {
    dotNetObject.invokeMethodAsync('CloseUserCard');
}

function fitTextToContainer(outputDiv) {
    const maxFontSize = 24; // Set your desired max font size
    const text = outputDiv.textContent.trim();
    const containerWidth = outputDiv.clientWidth;
    const fontSize = Math.min(maxFontSize, containerWidth / text.length);
    outputDiv.style.fontSize = `${fontSize}px`;
}

function countToNumber(elementSelector, targetNumber) {
    var duration = 1500; // Duração total da contagem em milissegundos
    var increments = 100; // Número fixo de incrementos
    var increment = targetNumber / increments; // Valor de cada incremento
    var intervalTime = duration / increments; // Intervalo de tempo fixo entre incrementos
    var currentNumber = 0;
    var interval = setInterval(function () {
        currentNumber += increment;
        if (currentNumber >= targetNumber) {
            currentNumber = targetNumber; // Garante que o número final seja o número alvo
            $(elementSelector).text(currentNumber);
            clearInterval(interval); // Para a contagem
        } else {
            $(elementSelector).text(Math.floor(currentNumber)); // Arredonda para baixo para evitar decimais
        }
    }, intervalTime);
}


//function countToNumber(elementSelector, targetNumber) {
//    var currentNumber = 0;
//    var increment = 1;
//    var intervalTime = 50; // Tempo entre cada incremento (em milissegundos)
//    var interval = setInterval(function () {
//        $(elementSelector).text(currentNumber);
//        if (currentNumber === targetNumber) {
//            clearInterval(interval); // Para a contagem quando alcançar o número desejado
//        } else {
//            currentNumber += increment;
//            if (currentNumber > targetNumber) {
//                currentNumber = targetNumber; // Garante que não ultrapasse o número desejado
//            }
//        }
//    }, intervalTime);
//}

function updateBorderRadius() {
    const element = document.getElementById('analista-selected');
    var top = element.getBoundingClientRect().top + window.scrollY;
    var bottom = element.getBoundingClientRect().bottom + window.scrollX;
    if (top <= 0) {
        element.style.borderRadius = '0 0 50% 50%';
    }
    if (bottom <= 0) {
        element.style.borderRadius = '50% 50% 0 0';
    }
}


function HandleButtonClick(dotNetObject) {
    document.querySelectorAll('.fluent-wizard-icon').forEach(item => {
        item.addEventListener('click', event => {
            var saida = item.querySelector('.fluent-wizard-icon-number').textContent;
            dotNetObject.invokeMethodAsync('StepClicked', saida.toString());
        });
    });
};