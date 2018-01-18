// Write your JavaScript code.

        var button = document.getElementById('show_button')
        button.addEventListener('click', hideshow, false);

        function hideshow() {
            document.getElementById('remove_hidden').classList.remove("hidden");
            
        } 

        var button1 = document.getElementById('list_it')
        button1.addEventListener('click', myFunction, false);

function myFunction(list) {
    var text = "";
    var inputs = document.getElementById("list_away");
    for (var i = 0; i < inputs.length; i++) {
        text += inputs[i].value;
    }
    var li = document.createElement("li");
    var node = document.createTextNode(text);
    li.appendChild(node);
    document.getElementById("list").appendChild(li);
}