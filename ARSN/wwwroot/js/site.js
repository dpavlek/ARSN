// Write your JavaScript code.

        var NumberOfTeams = 0;
        //Manual adding for comeptitions remove hidden
        var buttonManualAdd = document.getElementById('show_button_manual')
        buttonManualAdd.addEventListener('click', HideshowManual, false);

        function HideshowManual() {
            document.getElementById('remove_hidden_manual').classList.remove("hidden");
            document.getElementById('show_button_manual').classList.add("hidden"); 
            document.getElementById('remove_hidden_automatic').classList.add("hidden");
            document.getElementById('show_button_automatic').classList.remove("hidden");
        } 
        //Automatic adding for competitions remove hidden
        var buttonAutomaticAdd = document.getElementById('show_button_automatic')
        buttonAutomaticAdd.addEventListener('click', HideshowAutomatic, false);

        function HideshowAutomatic() {
            document.getElementById('remove_hidden_automatic').classList.remove("hidden");
            document.getElementById('show_button_automatic').classList.add("hidden");  
            document.getElementById('remove_hidden_manual').classList.add("hidden");
            document.getElementById('show_button_manual').classList.remove("hidden");
        } 

        var ButtonAddToCompetitionManual = document.getElementById('list_it_manual')
        ButtonAddToCompetitionManual.addEventListener('click', AddToTextAreaForManual, false);

        //Gets elements from lists and writes them in text box for manual additions
        function AddToTextAreaForManual(list) {
            if (document.getElementById("list_home").value == "" || document.getElementById("list_away").value == ""
                || document.getElementById("list_home").value == document.getElementById("list_away").value) {
                return false;
            }
            else {
                var text = [];
                var inputs = document.getElementById("list_home");
                var inputs1 = document.getElementById("list_away");
                for (var i = 0; i < inputs.options.length; i++) {
                    if (inputs.options[i].selected == true) {
                        text += inputs[i].value + "-";
                        inputs.remove(i);
                        inputs1.remove(i);
                    }
                }
                for (var i = 0; i < inputs1.options.length; i++) {
                    if (inputs1.options[i].selected == true) {
                        text += inputs1[i].value + "\n";
                        inputs.remove(i);
                        inputs1.remove(i);
                    }
                }

                document.getElementById("messageAreaManual").value += text;
            }
        }


        var ButtonAddToCompetitionAutomatic = document.getElementById('list_it_automatic')
        ButtonAddToCompetitionAutomatic.addEventListener('click', AddToTextAreaForAutomatic, false); 


        //Gets elements from lists and writes them in text box for automatic additions
        function AddToTextAreaForAutomatic(list) {
           
            if (document.getElementById("AutomaticList").value == "") {
                return false;
            }
            else {
                var text = [];
                var inputs = document.getElementById("AutomaticList");
                for (var i = 0; i < inputs.options.length; i++) {
                    if (inputs.options[i].selected == true && inputs.options[i].selected != "") {
                        text += inputs[i].value + "\n";
                        NumberOfTeams++;
                    }
                }
                document.getElementById("messageAreaAutomatic").value += text;
                inputs.remove(inputs.selectedIndex);
            } 
        }

        //Checks wheather user selected any team for the championship
        function CheckCreateValidationAutomatic() {
            var sportType = document.getElementById("SportType").value;
            var textVal = document.getElementById("messageAreaAutomatic").value;
            var error = document.getElementById("errorMessage");
            if (textVal == "" || sportType == "") {
                error.innerHTML = "Nisu dodani timovi ili nije odabran sport";
                return false;
            }
            else if (NumberOfTeams < 2) {
                var error = document.getElementById("errorMessage");
                error.innerHTML = "Dodajte barem dva tima.";
                return false;
            }
            else {
                error.innerHTML = "";
                return true
            }
        }

        function CheckCreateValidationManual() {
            var sportType = document.getElementById("SportType").value;
            var textVal = document.getElementById("messageAreaManual").value;
            var error = document.getElementById("errorMessage");
            if (textVal == "" || sportType == "") {
                error.innerHTML = "Nisu dodani timovi ili nije odabran sport";
                return false;
            }
            else {
                error.innerHTML = "";
                return true
            }
        }

if (ViewData.ModelState["Error"].Errors.Count > 0) {

    $(document).ready(function () {
        alert('@ViewData.ModelState["Error"].Errors.First().ErrorMessage');
    });

}