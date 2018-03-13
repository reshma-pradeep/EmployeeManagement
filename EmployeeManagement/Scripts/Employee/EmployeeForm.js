function show(input) {
    filecheck(input);
    if (input.files && input.files[0]) {
        var filerdr = new FileReader();
        filerdr.onload = function (e) {
            $('#EmployeeImage').attr('src', e.target.result);
        }
        filerdr.readAsDataURL(input.files[0]);

    }
}
function filecheck(input) {
    var fileExtension = ['jpeg', 'jpg', 'png', 'gif', 'bmp'];
    if ($.inArray($(input).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
        alert("Only '.jpeg','.jpg', '.png', '.gif', '.bmp' formats are allowed.");
    }
}
