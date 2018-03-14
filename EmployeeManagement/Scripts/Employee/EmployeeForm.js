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

function PopulateAge(input) {
    dob = new Date($('#DateOfBirth').val());
    alert(dob);
    var today = new Date();
    var age = today.getTime() - dob.getTime();
    age = Math.floor(age / (1000 * 60 * 60 * 24 * 365.25));

    var temp = 12;
    $('#Age').val(age);
    alert(age);
    alert('Changed!');
}
