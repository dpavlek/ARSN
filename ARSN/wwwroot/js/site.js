// Write your JavaScript code.


        var buttonManualAdd = document.getElementById('show_button_manual')
        buttonManualAdd.addEventListener('click', HideshowManual, false);

        function HideshowManual() {
            document.getElementById('remove_hidden_manual').classList.remove("hidden");
            
        } 

        var buttonAutomaticAdd = document.getElementById('show_button_automatic')
        buttonManualAdd.addEventListener('click', HideshowAutomatic, false);

        function HideshowAutomatic() {
            document.getElementById('remove_hidden_automatic').classList.remove("hidden");

        } 

        var ButtonAddToCompetition = document.getElementById('list_it')
        ButtonAddToCompetition .addEventListener('click', myFunction, false);

        //Gets elements from lists and writes them in text box
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

        var ButtonAddToCompetition = document.getElementById('list_it')
        ButtonAddToCompetition.addEventListener('click', myFunction, false); 



if (ViewData.ModelState["Error"].Errors.Count > 0) {

    $(document).ready(function () {
        alert('@ViewData.ModelState["Error"].Errors.First().ErrorMessage');
    });

}