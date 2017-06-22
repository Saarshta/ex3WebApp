function loadList() {
	var str = "<option>Click here to see list of games</option>";
	$(str).appendTo("#joindropdown");
}

loadList();

var chat = $.connection.multiPlayerController;

chat.client.rcvMessage = function (text) {
	alert(text);
}

chat.client.updateList = function (list) {

	$("#joindropdown").empty();
	list.forEach(function (game) {
		// Add a list item for the product
		var str = "<option>" + game + "</option>";
		$(str).appendTo("#joindropdown");
	});
}
var enemyMaze;
chat.client.play = function (x, y) {

	//alert("inside play");
	$("#mazeCanvasEnemy").move(enemyMaze, x, y, true);
}

function initCanvas(canvasId, maze, isEnemy) {
	$(canvasId).drawMaze(maze);
	$(canvasId).css('border', 'solid 5px black');
	if (isEnemy == false) {
		$(canvasId).off("keyup");
		$(canvasId).off("keydown");
		$(canvasId).on('keyup', function (e) {

				e.preventDefault();

				var currRow = parseInt(maze.CurrPosRow);
				var currCol = parseInt(maze.CurrPosCol);
				// every time a key is pressed
				switch (e.which) {

					// Move left
					case 37:
						$(canvasId).move(maze, currRow, currCol - 1, false);
						chat.server.play(currRow, currCol - 1);
						break;
					// Move up
					case 38:
						$(canvasId).move(maze, currRow - 1, currCol, false);
						chat.server.play(currRow - 1, currCol);
						break;
					// Move right
					case 39:
						$(canvasId).move(maze, currRow, currCol + 1, false);
						chat.server.play(currRow, currCol + 1);
						break;
					// Move down
					case 40:
						$(canvasId).move(maze, currRow + 1, currCol, false);
						chat.server.play(currRow + 1, currCol);
						break;
					default: break;
				};
		});
		$(canvasId).on('keydown', function (e) {
			e.preventDefault();
		});
	} else {
		//sessionStorage.setItem("EnemyMaze", maze);
		enemyMaze = maze;
	}
}

chat.client.joinGame = function (data) {
	maze = {
		Name: data.Name,
		Rows: data.Rows,
		Cols: data.Cols,
		//InitialPos: data.InitialPos,
		InitialPosRow: data.InitialPosRow,
		InitialPosCol: data.InitialPosCol,
		//GoalPos: data.GoalPos,
		GoalPosRow: data.GoalPosRow,
		GoalPosCol: data.GoalPosCol,
		//CurrPos: data.InitialPos,
		CurrPosRow: data.InitialPosRow,
		CurrPosCol: data.InitialPosCol,
		StringMaze: data.AsString
	};
	enemyMaze = {
		Name: data.Name,
		Rows: data.Rows,
		Cols: data.Cols,
		//InitialPos: data.InitialPos,
		InitialPosRow: data.InitialPosRow,
		InitialPosCol: data.InitialPosCol,
		//GoalPos: data.GoalPos,
		GoalPosRow: data.GoalPosRow,
		GoalPosCol: data.GoalPosCol,
		//CurrPos: data.InitialPos,
		CurrPosRow: data.InitialPosRow,
		CurrPosCol: data.InitialPosCol,
		StringMaze: data.AsString
	};
	initCanvas("#mazeCanvas", maze, false);
	initCanvas("#mazeCanvasEnemy", enemyMaze, true);

}

$(document).ready(function () {

	$.connection.hub.start().done(function () {
		$('#StartForm').submit(function (event) {
			/* stop form from submitting normally */
			event.preventDefault();

			// get the action attribute from the <form action=""> element
			var $form = $(this);
			// document.getElementById("mazeRows").value;
			var name = new String(document.getElementById("Name").value);
			var rows = document.getElementById("Rows").value;
			var cols = document.getElementById("Cols").value;
			chat.server.createGame(name, rows, cols);
		});

		$('#joindropdown').on('click change',function () {
			/* stop form from submitting normally */
			event.preventDefault();

			chat.server.getList();
			
		});

		$("#Joinbtn").click(function () {
			/* stop form from submitting normally */

			var e = document.getElementById("joindropdown");
			var gameName = e.options[e.selectedIndex].text;
			chat.server.join(gameName);

		});


	});

});

