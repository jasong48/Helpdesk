$(function () { // employeelist.js

    const getAll = async (msg) => {
        try {
            $("#employeeList").text("Finding Employee Information...");
            let response = await fetch(`api/employee`);
            if (!response.ok) // or check for response.status
                throw new Error(`Status - ${response.status}, Text - ${response.statusText}`);
            let data = await response.json(); // this returns a promise, so we await it
            buildEmployeeList(data, true);
            msg === "" ? // we are appending to an existing message
                $("#status").text("Employees Loaded") : $("#status").text(`${msg} - Employees Loaded`);
        } catch (error) {
            $("#status").text(error.message);
        }

        response = await fetch(`api/department`);
        if (!response.ok)
            throw new Error(`Status - ${response.status}, Problem server side, see server console`);
        let divs = await response.json();
        sessionStorage.setItem('alldepartments', JSON.stringify(divs));
    };

    const setupForUpdate = (id, data) => {
        $("#actionbutton").val("update");
        $("#modaltitle").html("<h4>update Employee</h4>");
        clearModalFields();
        $("#deletebutton").show();
        data.map(employee => {
            if (employee.id === parseInt(id)) {
                $("#TextBoxTitle").val(employee.title);
                $("#TextBoxFirstname").val(employee.firstname);
                $("#TextBoxLastname").val(employee.lastname);
                $("#TextBoxPhoneno").val(employee.phoneno);
                $("#TextBoxEmail").val(employee.email);
                $('#ImageHolder').html(`<img height="120" width="110" src="data:image/png;base64,${employee.staffPicture64}"/>`);
                sessionStorage.setItem("Id", employee.id);
                sessionStorage.setItem("DepartmentId", employee.departmentId);
                sessionStorage.setItem("Timer", employee.timer);
                $("#modalstatus").text("update data");
                let validator = $("#EmployeeModalForm").validate();
                validator.resetForm();
                $("#modalstatus").attr("class", "");
                loadDepartmentDDL(employee.departmentId.toString());
                $("#theModal").modal("toggle");
            } // if 
        }); //data.map
    };//setup for update

    const setupForAdd = () => {
        loadDepartmentDDL(-1);

        $("#actionbutton").val("add");
        $("#modaltitle").html("<h4>add employee</h4>");
        $("#theModal").modal("show");
        $("#theModal").css("z-index", "1500");
        $("#modalstatus").text("add new employee");
        $("#deletebutton").hide();
        clearModalFields();
    }; 

    const loadDepartmentDDL = (studiv) => {
        html = '';
        $('#ddlDivs').empty();
        let alldepartments = JSON.parse(sessionStorage.getItem('alldepartments'));
        alldepartments.map(div => html += `<option value="${div.id}">${div.name}</option>`);
        $('#ddlDivs').append(html);
        $('#ddlDivs').val(studiv);
    };

    const clearModalFields = () => {
        $("#TextBoxTitle").val("");
        $("#TextBoxFirstname").val("");
        $("#TextBoxLastname").val("");
        $("#TextBoxPhoneno").val("");
        $("#TextBoxEmail").val("");
        sessionStorage.removeItem("Id");
        sessionStorage.removeItem("DepartmentId");
        sessionStorage.removeItem("Timer");
        //$("EmployeeModalForm").validate().resetForm();
    }; // clearModalFields

    const buildEmployeeList = (data, usealldata) => {
        $("#employeeList").empty();
        div = $(`<div class="list-group-item text-white bg-secondary row d-flex" id="status">Employee Info</div>
                    <div class= "list-group-item row d-flex text-center" id="heading">
                    <div class="col-4 h4">Title</div>
                    <div class="col-4 h4">First</div>
                    <div class="col-4 h4">Last</div>
                 </div>`);
        div.appendTo($("#employeeList"));
        usealldata ? sessionStorage.setItem("allemployees", JSON.stringify(data)) : null;
        btn = $(`<button class="list-group-item row d-flex" id="0"><div class="col-12 text-left">...click to add employee</div></button>`);
        btn.appendTo($("#employeeList"));
        data.map(emp => {
            btn = $(`<button class="list-group-item row d-flex" id="${emp.id}">`);
            btn.html(`<div class="col-4" id="employeetitle${emp.id}">${emp.title}</div>
                      <div class="col-4" id="employeefname${emp.id}">${emp.firstname}</div>
                      <div class="col-4" id="employeelastnam${emp.id}">${emp.lastname}</div>`
            );
            btn.appendTo($("#employeeList"));
        }); // map
    }; // buildEmployeeList


    const update = async () => {
        try {
            emp = new Object();
            emp.Title = $("#TextBoxTitle").val();
            emp.Firstname = $("#TextBoxFirstname").val();
            emp.Lastname = $("#TextBoxLastname").val();
            emp.Phoneno = $("#TextBoxPhoneno").val();
            emp.Email = $("#TextBoxEmail").val();
            // we stored these 3 earlier
            emp.Id = sessionStorage.getItem("Id");
            emp.departmentId = $("#ddlDivs").val();
            emp.timer = sessionStorage.getItem("Timer");
            localStorage.getItem('StaffPicture')
                ? emp.StaffPicture64 = localStorage.getItem('StaffPicture')
                : null;
            //send the updated back to the server asynchronously using PUT
            let response = await fetch("api/employee", {
                method: "PUT",
                headers: {
                    "Content-Type": "application/json; charset=utf-8"
                },
                body: JSON.stringify(emp)
            });
            if (response.ok) // or check for response.status
            {
                let data = await response.json();
                getAll(data.msg);
            } else {
                $("#status").text(`${response.status}, Text - ${response.statusText}`);
            } //else
            $("#theModal").modal("toggle");
        } catch (error) {
            $("#status").text(error.message);
        }
    }; // update


    const add = async () => {
        try {
            emp = new Object();
            emp.Title = $("#TextBoxTitle").val();
            emp.Firstname = $("#TextBoxFirstname").val();
            emp.Lastname = $("#TextBoxLastname").val();
            emp.Phoneno = $("#TextBoxPhoneno").val();
            emp.Email = $("#TextBoxEmail").val();
            emp.departmentId = $("#ddlDivs").val(); 
            emp.Id = -1;
            emp.Timer = null;
            emp.StaffPicture64 = null;
            // send the employee info to the server asynchrononously using POST
            let response = await fetch("api/employee", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json; charset=utf-8"
                },
                body: JSON.stringify(emp)
            });
            if (response.ok) {
                let data = await response.json();
                getAll(data.msg);
            } else {
                $("#status").text(`${response.status}, Text - ${response.statusText}`);
            }//else
            $("#theModal").modal("toggle");
        } catch (error) {
            $("#status").text(error.message);
        }
    };

    const _delete = async () => {
        try {
            let response = await fetch(`api/employee/${sessionStorage.getItem('Id')}`, {
                method: 'DELETE',
                headers: { 'Content-Type': 'application/json; charset=utf-8' }
            });
            if (response.ok) // or check response.status
            {
                let data = await response.json();
                getAll(data.msg);
            } else {
                $('#status').text(`Status - ${response.status}, Problem on delete server side, see server console`);
            } // else
            $('#theModal').modal('toggle');
        } catch (error) {
            $('#status').text(error.message);
        }

    };

    $("#actionbutton").click(() => {
        $("#actionbutton").val() === "update" ? update() : add();
    }); //actionbutton click


    $('[data-toggle=confirmation]').confirmation({ rootSelector: '[data-toggle=confirmation]' });

    $('#deletebutton').click(() => _delete()); //if yes was chosen

    $("#employeeList").click((e) => {
        if (!e) e = window.event;
        let Id = e.target.parentNode.id;
        if (Id === "employeeList" || Id === "") {
            Id = e.target.id;
        } // clicked on row somewhere else

        if (Id !== "status" && Id !== "heading") {
            let data = JSON.parse(sessionStorage.getItem("allemployees"));
            Id === "0" ? setupForAdd() : setupForUpdate(Id, data);
        } else {
            return false; // ignore if they clicked on heading or status
        }
    });

   

    
 

    $("#updatebutton").click(async (e) => {
        try {
            //set up a new client side instance of employee
            emp = new Object();
            //populate the properties
            emp.Title = $("#TextBoxTitle").val();
            emp.Firstname = $("#TextBoxFirstname").val();
            emp.Lastname = $("#TextBoxLastname").val();
            emp.Phoneno = $("#TextBoxPhoneno").val();
            emp.Email = $("#TextBoxEmail").val();
            // we stored these 3 earlier
            emp.Id = sessionStorage.getItem("Id");
            emp.departmentId = sessionStorage.getItem("DepartmentId");
            emp.Timer = sessionStorage.getItem("Timer");
           
            // send the updated back to the server asynchronosly using PUT
            let response = await fetch("/api/employees", {
                method: "PUT",
                headers: { "Content-Type": "application/json; charset=utf-8" },
                body: JSON.stringify(emp)
            });

            if (response.ok) //or check for response.status
            {
                let data = await response.json();
                getAll(data.msg);
            } else {
                throw new Error(`Status - ${response.status}, Problem server side, see server console`);
            }//else
            $("#theModal").modal("toggle");
        } catch (error) {
            $("#status").text(error.message);
        }
    }); // update button click

   


    //$('#teModal').on('shown.bs.modal', function () {
    //    //To relate the z-index make sure backdrop and modal are siblings
    //    $(this).before($('.modal-backdrop'));
    //    //Now set z-index of modal greater than backdrop
    //    $(this).css("z-index", parseInt($('.modal-backdrop').css('z-index')) + 1);
    //}); 

    getAll(""); // first grab the data from the server


    document.addEventListener("keyup", e => {
        $("#modalstatus").removeClass(); //remove any existing css on div
        if ($("#EmployeeModalForm").valid()) {
            $("#modalstatus").attr("class", "badge badge-success");
            $("#modalstatus").text("data entered is valid");
            $("#actionbutton").prop('disabled', false);
        }
        else {
            $("#modalstatus").attr("class", "badge badge-danger");
            $("#actionbutton").prop('disabled', true);
            $("#modalstatus").text("fix errors");
        }
    });



    $.validator.addMethod("validTitle", (value) => { //custom rule
        return (value === "Mr." || value === "Ms." || value === "Mrs." || value === "Dr.");
    }, ""); //.validator.addMethod

    $("#EmployeeModalForm").validate({
        rules: {
            TextBoxTitle: { maxlength: 4, required: true, validTitle: true },
            TextBoxFirstname: { maxlength: 25, required: true },
            TextBoxLastname: { maxlength: 25, required: true },
            TextBoxEmail: { maxlength: 40, required: true, email: true },
            TextBoxPhoneno: { maxlength: 15, required: true }

        },
        errorElement: "div",
        messages: {
            TextBoxTitle: {
                required: "required 1-4 chars.", maxlength: "required 1-4 chars.", validTitle: "Mr. Ms. Mrs. or Dr."
            },
            TextBoxFirstname: {
                required: "required 1-25 chars.", maxlength: "required 1-25 chars."
            },
            TextBoxLastname: {
                required: "required 1-25 chars.", maxlength: "required 1-25 chars."
            },
            TextBoxPhoneno: {
                required: "required 1-15 chars.", maxlength: "required 1-15 chars."
            },
            TextBoxEmail: {
                required: "required 1-40 chars.", maxlength: "required 1-40 chars.", email: "need valid email format"
            }
        }
    }); //EmployeeModalForm.validate

    $("#srch").keyup(() => {
        let alldata = JSON.parse(sessionStorage.getItem("allemployees"));
        let filtereddata = alldata.filter((emp) => emp.lastname.match(new RegExp($("#srch").val(), 'i')));
        buildEmployeeList(filtereddata, false);
    }); //srch keyup


 

    // do we have a picture?
    $("input:file").change(() => {
        const reader = new FileReader();
        const file = $("#uploader")[0].files[0];

        file ? reader.readAsBinaryString(file) : null;

        reader.onload = (readerEvt) => {
            // get binary data then convert to encoded string
            const binaryString = reader.result;
            const encodedString = btoa(binaryString);
            sessionStorage.setItem('staffPicture', encodedString);
        };
    }); // input file change


}); // jQuery ready method