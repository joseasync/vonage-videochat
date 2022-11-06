$(document).ready(function () {
    Setup()
    Events();
});

function  Setup(){
}

function Events() {
    
    $('#joinSession').on('click', function () {
        let sessionValue = $('#sessionId').val()
        window.location.href='/Session/SessionSetup/'+sessionValue;
    });
    

    $('#createSession').on('click', function () {

        $.ajax({
            url: '/Session/CreateSession',
            type: 'POST',
            dataType: 'text',
            success: function (data) {
                console.log();
                $('#sessionId').attr("value",data)
                $("#myModal").modal('toggle')
            },
            error: function (xhr, textStatus, errorThrown) {
                console.log(textStatus);
            }

        });
    });

}