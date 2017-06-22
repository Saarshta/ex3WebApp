
$(document).ready(function () {
	var maze;
	var enableKeys = false;
	var intervalStatus = null;
    //declare what to do on form submit
    $("#GenerateForm").submit(function (event) {
        /* stop form from submitting normally */
		event.preventDefault();
		enableKeys = false;
		if (intervalStatus != null) {
			clearInterval(intervalStatus);
			intervalStatus = null;
		}
        // get the action attribute from the <form action=""> element
        var $form = $(this);
        // document.getElementById("mazeRows").value;
		var name = new String(document.getElementById("Name").value);
        var rows = document.getElementById("Rows").value;
        var cols = document.getElementById("Cols").value;
        var params = { Name: name, Rows: rows, Cols: cols };

        //ajax post command
        $.ajax({
            type: 'Post',
            url: 'api/Maze/CreateMaze',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(params),
            dataType: 'json',
            success: function (data) {
                 maze = {
                    Name: data.Name,
                    Rows: data.Rows,
                    Cols: data.Cols,
					InitialPosRow: data.InitialPosRow,
					InitialPosCol: data.InitialPosCol,
					GoalPosRow: data.GoalPosRow,
					GoalPosCol: data.GoalPosCol,
					CurrPosRow: data.InitialPosRow,
					CurrPosCol: data.InitialPosCol,
					StringMaze: data.AsString
				};
                $("#mazeCanvas").drawMaze(maze);
				$("#mazeCanvas").css('border', 'solid 5px black');
				$("#mazeCanvas").off("keyup");
				$("#mazeCanvas").off("keydown");
				$("#mazeCanvas").on('keyup', function (e) {
					//alert("key up");
					if (enableKeys == false) {
						e.preventDefault();

						var currRow = parseInt(maze.CurrPosRow);
						var currCol = parseInt(maze.CurrPosCol);
						// every time a key is pressed
						switch (e.which) {

							// Move left
							case 37:
								$("#mazeCanvas").move(maze, currRow, currCol - 1, false);
								break;
							// Move up
							case 38:
								$("#mazeCanvas").move(maze, currRow - 1, currCol, false);
								break;
							// Move right
							case 39:
								$("#mazeCanvas").move(maze, currRow, currCol + 1, false);
								break;
							// Move down
							case 40:
								$("#mazeCanvas").move(maze, currRow + 1, currCol, false);
								break;
							default: break;
						};
					}
				});
				$("#mazeCanvas").on('keydown', function (e) {
					//alert("key down");
					e.preventDefault();
				});
            },
            error: function (xhr, textStatus, errorThrown) {
                alert("failedddd" + err.text);
                $("#result").text("Error: " + err);
            }
        });
	});




	function solve(solution) {
		var i = 0;
		var initDelay = 0;
		var initx = parseInt(maze.InitialPosRow);
		var inity = parseInt(maze.InitialPosCol);
		$("#mazeCanvas").move(maze, initx, inity, false);
		intervalStatus = setInterval((function fn() {
			if (initDelay == 1) {
				var currRow = parseInt(maze.CurrPosRow);
				var currCol = parseInt(maze.CurrPosCol);
				//alert("before currdir");
				var currDir = parseInt(solution[i]);
				//alert("after currdir");
				// If we havent reached goal position
				if (i < solution.length) {
					switch (currDir) {
						// Move left
						case 0:
							//alert("before move");
							$("#mazeCanvas").move(maze, currRow, currCol - 1, false);
							break;
						// Move up
						case 2:
							//alert("before move");
							$("#mazeCanvas").move(maze, currRow - 1, currCol, false);
							break;
						// Move right
						case 1:
							//alert("before move");
							$("#mazeCanvas").move(maze, currRow, currCol + 1, false);
							break;
						// Move down
						case 3:
							//alert("before move");
							$("#mazeCanvas").move(maze, currRow + 1, currCol, false);
							break;
						default: break;
					};
					// Reached goal position
				} else {
					clearInterval(intervalStatus);
					intervalStatus = null;
					
				}
				i++;
			}
			initDelay = 1;
			return fn;
		})(), 500);
	}

	//declare what to do on form submit
	$("#Solvebtn").click(function () {
		/* stop form from submitting normally */

		// get the action attribute from the <form action=""> element
		var $form = $(this);
		// document.getElementById("mazeRows").value;
		var name = new String(document.getElementById("Name").value);
		var params = { Name: name };

		//ajax post command
		$.ajax({
			type: 'Post',
			url: 'api/Maze/SolveMaze',
			contentType: "application/json; charset=utf-8",
			data: JSON.stringify(params),
			dataType: 'json',
			success: function (data) {
				sol = {
					Solution: data.Solution
				};
				enableKeys = true;
				solve(sol.Solution);

			},
			error: function (xhr, textStatus, errorThrown) {
				alert("failedddd" + err.text);
				$("#result").text("Error: " + err);
			}
		});
	});
});
