function addInputField(targetId, type, name) {
    var element = document.createElement("input");
    element.setAttribute("type", type);
    element.setAttribute("name", name);

    var targetEl = document.getElementById(targetId);
    targetEl.appendChild(element);
    element = document.createElement("br");
    targetEl.appendChild(element);
}

function IncreaseValue(targetId, value) {

    var targetEl = document.getElementById(targetId)[0];
    targetEl.value = parseInt(targetEl.value) + parseInt(value);
}

function clickOnElement(targetId) {
    var targetEl = document.getElementById(targetId)[0];
    targetEl.clickOnElement();
}