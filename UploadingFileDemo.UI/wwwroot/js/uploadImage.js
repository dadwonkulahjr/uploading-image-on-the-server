const Max_Upload_Size = 100000;
function validateInput() {
    var doc = $('#imageUpload');
    if (doc.val() == '') {
        swal('Error', 'Please select an image', 'error');
        return false;

    }
    else {
        if (doc[0].files[0].type == "image/jpeg" || doc[0].files[0].type == "image/jpg" || doc[0].files[0].type == "image/png") {
            //Process the file
            if (doc[0].files[0].size <= Max_Upload_Size) {
                //Process the file
                return true;
            }
            else {
                swal({
                    title: 'Error',
                    text: 'The file is to large. The size of the image should be not less than 1MB.',
                    icon: 'error'
                });
                return false;
            }
        }
        else {
            swal({
                title: 'Error',
                text: 'Only file with the following extentions are allowed (.jpg, .jpeg, or png).',
                icon: 'error'
            });
            return false;
        }
        //console.log(doc[0].files);
        //console.log(doc[0].files[0].name);
        //console.log(doc[0].files[0].size);
        //console.log(doc[0].files[0].type);
        //console.log(doc.length);

        return false;
    }
}
function sendData() {
    document.getElementById("btnSubmit").addEventListener('click', function (env) {
        env.preventDefault();
    });
}

$(document).ready(function () {
    $('.custom-file-input').on('change', function () {
        var fileName = $(this).val().split('\\').pop();
        $(this).next('.custom-file-label').html(fileName);
    });

});
