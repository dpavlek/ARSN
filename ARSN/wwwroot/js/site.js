// Write your JavaScript code.

        var button = document.getElementById('show_button')
        button.addEventListener('click', hideshow, false);

        function hideshow() {
            document.getElementById('remove_hidden').classList.remove("hidden");
            
        } 

        var button1 = document.getElementById('list_it')
        button1.addEventListener('click', myFunction, false);
        //Gets elements from lists and writes them in ul
        function myFunction(list) {
            var text = [];
            var inputs = document.getElementById("list_home");
            var inputs1 = document.getElementById("list_away");
            for (var i = 0; i < inputs.options.length; i++) {
                if (inputs.options[i].selected == true) {
                    text += inputs[i].value+"-";
                }
            }
            for (var i = 0; i < inputs1.options.length; i++) {
                if (inputs1.options[i].selected == true) {
                    text += inputs1[i].value+"\n";
                }
            }
            document.getElementById("messageArea").value += text;
        }