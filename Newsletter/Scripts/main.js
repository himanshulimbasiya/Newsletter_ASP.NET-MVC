$(document).ready(function() {
    $("#submit").on("click", function (e) {
        $("#email-error").css("display", "none");
        $("#source-error").css("display", "none");
        var data = $("#subscription").serialize();
        submitForm(data);
    });

    function submitForm(data) {

        $.ajax({
            method: "POST",
            url: "/newsletter/index",
            data: data

        }).done(function(response) {
            result(response);
        }).fail(function(jqXHR, textStatus, errorThrown) {
            response.write(errorThrown);
        });

    }

    function result(response)
    {
        if (response === "Email and source null")
        {
            document.getElementById("email-error").innerHTML = "This Field is Required";
            document.getElementById("source-error").innerHTML = "This Field is Required";
        }
        else if (response === "Source null")
        {
            document.getElementById("source-error").innerHTML = "This Field is Required";
        }
        else if (response === "Null email")
        {
            document.getElementById("email-error").innerHTML = "This Field is Required";
        }
        else if (response === "Invalid email")
        {
            document.getElementById("email-error").innerHTML = "Invalid Email";
        }
        else if (response === "Both valid") {
            $("#success-subscriber").css("display", "inline");
            document.getElementById("success-subscriber").innerHTML = "Thank You For Subscribing";
        }
        else if (response === "Already subscribed") {
            $("#email-error").css("display", "inline");
            document.getElementById("success-subscriber").innerHTML = "Email already subscribed";
        }
    }

})