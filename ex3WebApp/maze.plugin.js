
// adding jQuery function drawMaze
// TODO check if reached end of maze
jQuery(function ($) {

	function isPosValid(maze, x, y) {
		//out of borders
		if (x < 0 || x >= maze.Rows || y < 0 || y >= maze.Cols)
			return false;
		// its a wall
		if (maze.StringMaze[x * maze.Cols + y] == '1') {
			return false;
		}
		return true;
	};



	function drawImage(maze, params, x, y) {

		var currPosImg = new Image();
		currPosImg.src = "images/monkey.png";
		currPosImg.onload = function () {
			
			params.context.drawImage(currPosImg, y * params.cellWidth, x * params.cellHeight,
				params.cellWidth, params.cellHeight);
			params.context.fillStyle = "#FFFFFF";
			params.context.fillRect(params.cellWidth * params.oldY, params.cellHeight * params.oldX,
				params.cellWidth, params.cellHeight);

		}

	};


	$.fn.move = function (maze, newx, newy, isEnemy) {
		
		var ctx = this[0].getContext("2d");
		var width = this[0].width / maze.Cols;
		var height = this[0].height / maze.Rows;
		var prevX = parseInt(maze.CurrPosRow);
		var prevY = parseInt(maze.CurrPosCol);
		

		if (isPosValid(maze, newx, newy) == true) {


			maze.CurrPosRow = newx;
			maze.CurrPosCol = newy;
			var goalx = maze.GoalPosRow;
			var goaly = maze.GoalPosCol;
			if ((newx != goalx) || (newy != goaly)) {

				var params = {
					context: ctx,
					cellWidth: width,
					cellHeight: height,
					oldY: prevY,
					oldX: prevX
				};

				drawImage(maze, params, newx, newy);

			} else {
				if (isEnemy == false) {
					alert("Congratuations, you win!\nBack to homepage.");
				} else {
					alert("I'm sorry, you lost!\nBack to homepage.");
				}
				loadPage('HomePage.html')
			}
		}

	

	};

	//declare drawMaze function
	$.fn.drawMaze = function (maze) {

		var context = this[0].getContext("2d");
		var rows = maze.Rows;
		var cols = maze.Cols;

		var cellWidth = this[0].width / cols;
		var cellHeight = this[0].height / rows;
		for (var i = 0; i < rows; i++) {
			for (var j = 0; j < cols; j++) {
				//if it is a wall
				if (maze.StringMaze[i * cols + j] == '1') {
					var ctx = this[0].getContext("2d");
					ctx.fillStyle = "#000000";
					ctx.fillRect(cellWidth * j, cellHeight * i,
						cellWidth, cellHeight);
				}
				// it is a free cell
				else {
					var ctx = this[0].getContext("2d");
					ctx.fillStyle = "#FFFFFF";
					ctx.fillRect(cellWidth * j, cellHeight * i,
						cellWidth, cellHeight);
				}
			}
		}
		var ctx = this[0].getContext("2d");
		var startx = maze.InitialPosRow;
		var starty = maze.InitialPosCol;
		var goalx = maze.GoalPosRow;
		var goaly = maze.GoalPosCol;
		alert("start: " + startx + "," + starty + "  goal: " + goalx + "," + goaly);
		var startImg = new Image();
		startImg.onload = function () {
			ctx.drawImage(startImg, starty * cellWidth, startx * cellHeight, cellWidth, cellHeight);
			var endImg = new Image();
			endImg.onload = function () {
				ctx.drawImage(endImg, goaly * cellWidth, goalx * cellHeight, cellWidth, cellHeight);
			}
			endImg.src = "images/goal.png";
		}
		startImg.src = "images/monkey.png";


		return this;
	};

})





