/*
    custom made function for hitting api for performing crud operation
*/

//hitting the api for enabling row value editing
function editData(elem) {
    $(elem).parents("tr").attr("bgcolor", "yellow");
    $(elem).parents("tr").children(".beggining").focus();
    $(elem).parents("tr").children("td").attr("contenteditable", "true");
    //toggle();
    $(elem).parent("td").children("button").hide();
    $(elem).parent("td").children(".toggleInvert").show();
    let val = document.getElementsByClassName("NonEditable");
    console.log(val)
    for (let i = 0; i < val.length; i++) {
        val[i].setAttribute("contenteditable", "false");
    }
    button = $("<button onclick='cancel(this,1)'>");
    button.append("Cancel");
    $(elem).parent("td").append(button);
    console.log("yup1");
}
function cancel(elem,i)
{
    if (i == 1)
    {
        $(elem).parent("td").children(".toggle").show();
        $(elem).parent("td").children(".toggleInvert").hide();
        $(elem).parents("tr").removeAttr("bgcolor");
        $(elem).remove();
    }
    else
    {
        $(elem).parents("tr").remove();
    }
}

//hiting the api for saving changes done through editing
function saveChanges(elem) {
    $(elem).attr("disabled", true);
    var user = new Object();
    var _row = $(elem).parents("tr");
    var cols = _row.children("td");
    user.uid = $(elem).data('assigned-id');
    console.log($(elem).data('assigned-id'));
    user.firstName = $(cols[0]).text().trim();
    user.middleName = $(cols[1]).text().trim();
    user.lastName = $(cols[2]).text().trim();
    user.address = $(cols[3]).text().trim();
    console.log('firstName : ' + user.firstName + '\n middleName : ' + user.middleName + '\nlastName : ' + user.lastName + '\naddress : ' + user.address);
    $.ajax({
        url: 'http://localhost:52028/api/values/update',
        type: 'PUT',
        dataType: 'json',
        data: user,
        success: function (data, textStatus, xhr) {
            console.log(data);
            if (data == 'True')
                //alert('record saved successfully');
            {
                $("#status").show();
                document.getElementById('status').innerHTML = "record save successfully";
                $("#status").fadeOut("slow");
            }
            else
            {
                $("#status").show();
                document.getElementById('status').innerHTML = "No rows affected";
                $("#status").fadeOut("slow");
            }
            $(elem).attr("disabled", false);
            refreshTableData(elem);
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log('Error in Operation');
            $(elem).attr("disabled", false);
        }
    });
    $(elem).parents("tr").children("td").attr("contenteditable", "false");

}


//hitting the api for getting latest table data
function refreshTableData(elem) {
    $.ajax({
        url: 'http://localhost:52028/api/values/retreive',
        type: 'GET',
        dataType: 'json',
        success: function (data, textStatus, xhr) {
            console.log(data);
            $("#dataTable tbody").remove();
            let row = data[0];
            console.log(data[0].firstName);
            let bodyStart = $('<tbody>');
            let bodyEnd = $('</tbody>');
            for (var i = 0; i < data.length; i++) {
                console.log(i)
                tr = $('<tr>')
                tr.append("<td>" + data[i].FirstName + "</td>");
                tr.append("<td>" + data[i].MiddleName + "</td>");
                tr.append("<td>" + data[i].LastName + "</td>");
                tr.append("<td>" + data[i].Address + "</td>");
                tr.append("<td><button onclick=\"editData(this)\" class=\"NonEditable toggle\" >Edit</button> <button onclick=\"deleteRecord(this)\" class=\"NonEditable toggle\" id=\"delete\" data-assigned-id=" + data[i].Uid + ">Delete</button> <button onclick=\"saveChanges(this)\" class=\"NonEditable toggleInvert\" style=\"display:none\" data-assigned-id=" + data[i].Uid + ">Save</button></td>");
                //tr.append("");
                tr.append("</tr></tbody>");
                $("#dataTable").append(tr);
            }
            console.log
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log('Error in Operation');
        }
    });
}

//for hitting the api for deleting the selected record
function deleteRecord(elem) {
    console.log("entered 1")
    let userVal = $(elem).data('assigned-id');
    //console.log('firstName : ' + userVal.firstName + '\n middleName : ' + userVal.middleName + '\nlastName : ' + userVal.lastName + '\naddress : ' + userVal.address);
    $.ajax({
        url: 'http://localhost:52028/api/Values/' + userVal,
        type: 'DELETE',
        success: function (data, textStatus, xhr) {
            console.log(data);
            if (data == 'True') {
                refreshTableData(elem);
                $("#status").show();
                document.getElementById('status').innerHTML = "record deleted successfully";
                $("#status").fadeOut("slow");
            }
            else
            {
                $("#status").show();
                document.getElementById('status').innerHTML = "No rows affected";
                $("#status").fadeOut("slow");
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log('Error in Operation');
        }
    });

}

//hitting the api for creating a blank row for user to enter the values
function initInsertRecord(elem) {
    console.log("Yeah entered");
    tr = $('<tr>');
    for (var i = 0; i < 4; i++) {
        tr.append("<td contentEditable=\"true\"></td>")
    }
    tr.append("<td><button onclick=\"insertRecord(this)\">Save</button> <button onclick=\"cancel(this,2)\">Cancel</button></td></tr>");
    $("#dataTable tr:last").after(tr);

}
//hitting the api to insert the record
function insertRecord(elem) {
    console.log("hello");
    let userVal = new Object();
    var _row = $(elem).parents("tr");
    var cols = _row.children("td");
    userVal.firstName = $(cols[0]).text().trim();
    userVal.middleName = $(cols[1]).text().trim();
    userVal.lastName = $(cols[2]).text().trim();
    userVal.address = $(cols[3]).text().trim();

    $.ajax({
        url: 'http://localhost:52028/api/values/insert',
        type: 'POST',
        dataType: 'json',
        data: userVal,
        success: function (data, textStatus, xhr) {
            console.log(data);
            if (data == 'True')
            {
                $("#status").show();
                document.getElementById('status').innerHTML = "record inserted successfully";
                $("#status").fadeOut("slow");
            }
            else
            {
                $("#status").show();
                document.getElementById('status').innerHTML = "No rows affected";
                $("#status").fadeOut("slow");
            }
            refreshTableData(elem);
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log('Error in Operation');
            refreshTableData(elem);
        }
    });
    $(elem).parents("tr").children("td").attr("contenteditable", "false");
}
