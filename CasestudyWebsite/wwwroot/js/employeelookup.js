$(function () {

    $("#getbutton").click(async (e) => { // click event handler makes aysynchronous fetch to server
        try {
            let email = $("#TextBoxEmail").val();
            $("#status").text("please wait...");
            let response = await fetch(`/api/employees/GetByEmail/${email}`);
            if (!response.ok) //or check for response.status
                throw new Error(`Status - ${response.status}, Problem server side, see server console`);
            let data = await response.json(); // this returns a promise, so we await it
            if (data.Email !== "not found") {
                $("#lastname").text(data.lastname);
                $("#title").text(data.title);
                $("#firstname").text(data.firstname);
                $("#phone").text(data.phoneno);
                $("#status").text("employee found");
            } else {
                $("#firstname").text("not found");
                $("#lastname").text("");
                $("#title").text("");
                $("#phone").text("");
                $("#status").text("no such employee");
            }
        } catch (error) {
            $("#status").text(error.message);
        } //try/catch

    }); // click event

}); //jQuery ready method