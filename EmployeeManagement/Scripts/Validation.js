function IsFirstNameEmpty() {
    if (document.getElementById('Name').value == "") {
        return 'First Name should not be empty';
    }
}
function IsValid() {

    var FirstNameEmptyMessage = IsFirstNameEmpty();
  

    var FinalErrorMessage = "Errors:";
    if (FirstNameEmptyMessage != "")
        FinalErrorMessage += "\n" + FirstNameEmptyMessage;
   

    if (FinalErrorMessage != "Errors:") {
        alert(FinalErrorMessage);
        return false;
    }
    else {
        return true;
    }
}