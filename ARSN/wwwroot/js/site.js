// Write your JavaScript code.

        //Manual adding for comeptitions remove hidden
        var buttonManualAdd = document.getElementById('show_button_manual')
        buttonManualAdd.addEventListener('click', HideshowManual, false);

        function HideshowManual() {
            document.getElementById('remove_hidden_manual').classList.remove("hidden");
            document.getElementById('show_button_automatic').classList.add("hidden");
            document.getElementById('show_button_manual').classList.add("hidden");//makne i kliknuti gumb da ne smeta i ne zbunjuje korisnika 
            
        } 
        //Automatic adding for competitions remove hidden
        var buttonAutomaticAdd = document.getElementById('show_button_automatic')
        buttonAutomaticAdd.addEventListener('click', HideshowAutomatic, false);

        function HideshowAutomatic() {
            document.getElementById('remove_hidden_automatic').classList.remove("hidden");
            document.getElementById('show_button_manual').classList.add("hidden");
            document.getElementById('show_button_automatic').classList.add("hidden");//makne i kliknuti gumb da ne smeta i ne zbunjuje korisnika   

        } 

        var ButtonAddToCompetitionManual = document.getElementById('list_it_manual')
        ButtonAddToCompetitionManual.addEventListener('click', AddToTextAreaForManual, false);

        //Gets elements from lists and writes them in text box for manual additions
        function AddToTextAreaForManual(list) {
            var text = [];
            var inputs = document.getElementById("list_home");
            var inputs1 = document.getElementById("list_away");
            for (var i = 0; i < inputs.options.length; i++) {
                if (inputs.options[i].selected == true) {
                    text += inputs[i].value+"-";
                }
            }
            for (var i = 0; i < inputs1.options.length; i++) {
                if (inputs1.options[i].selected == true)
                {
                    text += inputs1[i].value+"\n";
                }
            }
            document.getElementById("messageAreaManual").value += text;
        }


        var ButtonAddToCompetitionAutomatic = document.getElementById('list_it_automatic')
        ButtonAddToCompetitionAutomatic.addEventListener('click', AddToTextAreaForAutomatic, false); 


        //Gets elements from lists and writes them in text box for automatic additions
        function AddToTextAreaForAutomatic(list) {
            var text = [];
            var inputs = document.getElementById("AutomaticList");
            for (var i = 0; i < inputs.options.length; i++) {
                if (inputs.options[i].selected == true && inputs.options[i].selected != "")
                {
                    text += inputs[i].value + "\n";
                }
            }
            document.getElementById("messageAreaAutomatic").value += text;            
        }
