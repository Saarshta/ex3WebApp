
$(document).ready(function () {
	$("#RegisterForm").submit(function (event) {
		event.preventDefault();

		// get the action attribute from the <form action=""> element
		var $form = $(this);
		// document.getElementById("mazeRows").value;
		var name = new String(document.getElementById("Name").value);
		var pw = document.getElementById("Password").value;
		var rePw = document.getElementById("Reenter").value;
		// TODO NEED to check if password = reenter
		if (rePw != pw) {
			alert("Error: Passwords do not match.");
		} else {

			var mail = document.getElementById("Mail").value;
			var params = { Name: name, Password: pw, Email: mail };

			//ajax post command
			$.ajax({
				type: 'Post',
				url: 'api/Users/PostUsers',
				contentType: "application/json; charset=utf-8",
				data: JSON.stringify(params),
				dataType: 'text',
				success: function (data) {
					alert(data);
				},
				// TODO need to change?
				error: function (xhr, textStatus, errorThrown) {
					alert("failedddd" + err.text);
					$("#result").text("Error: " + err);
				}
			});
		}
	});

	$("#LoginForm").submit(function (event) {
		event.preventDefault();

		// get the action attribute from the <form action=""> element
		var $form = $(this);
		// document.getElementById("mazeRows").value;
		var name = new String(document.getElementById("Username").value);
		var pw = document.getElementById("Password").value;

		var params = { Name: name, Password: pw };

		//ajax post command
		$.ajax({
			type: 'Get',
			url: 'api/Users/GetUser',
			//contentType: "application/json; charset=utf-8",
			data: params,
			//dataType: 'json',
			success: function (data) {
				if (data == "User not found") {
					alert(data);
				} else if (data == "Wrong password") {
					alert(data);
				} else {
					sessionStorage.setItem("username", data);
					alert("Logged in");
				}
				
			},
			// TODO need to change?
			error: function (xhr, textStatus, errorThrown) {
				alert("failedddd" + err.text);
				$("#result").text("Error: " + err);
			}
		});

	});

});
