using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Web.Http;
using ex3.Models;
using MazeLib;
using SearchAlgorithmsLib;
using ex3WebApp.Models;
using Adapters;

namespace ex3.Controllers
{
    public class MazeController : ApiController
    {
        private IModel mazeModel;

        public MazeController()
        {
            this.mazeModel = new MazeModel();
        }



        [HttpPost]
        [Route("api/Maze/CreateMaze")]
        public IHttpActionResult CreateMaze(MazeParam partialMaze)
        {
            try
            {
                int x = 0;
                int y = 0;
                
               MazeLib.Maze maze = this.mazeModel.Generate
                    (partialMaze.Name, partialMaze.Rows, partialMaze.Cols);

                partialMaze.GoalPos = maze.GoalPos.ToString();
                Converters.PositionConverter.ConvertPos(ref x, ref y, partialMaze.GoalPos);
                partialMaze.GoalPosRow = x;
                partialMaze.GoalPosCol = y;
               
                partialMaze.InitialPos = maze.InitialPos.ToString();
                Converters.PositionConverter.ConvertPos(ref x, ref y, partialMaze.InitialPos);
                partialMaze.InitialPosRow = x;
                partialMaze.InitialPosCol = y;
                JObject jmaze = JObject.Parse(maze.ToJSON());
                partialMaze.AsString = jmaze.GetValue("Maze").ToString();
                

                return Ok(JObject.Parse(partialMaze.ToJson()));
               
            }
            catch(ArgumentException  )
            {
                // Content bla bla BadRequest blabla
                return NotFound() ;
            }

        }

        [HttpPost]
        [Route("api/Maze/SolveMaze")]
        public IHttpActionResult SolveMaze(MazeName NameParam)
        {
            try
            {
                Searcher<Position> algorithm = new BestFirstSearch<Position>(new StateComparerByCost<Position>());
                Solution<State<Position>> sol = this.mazeModel.Solve(NameParam.Name, algorithm);
                MazeSolutionAdapter solutionAdapter = new MazeSolutionAdapter(sol);
                return Ok(JObject.Parse(solutionAdapter.ToJson(NameParam.Name)));

            }
            catch (ArgumentException)
            {
                // Content bla bla BadRequest blabla
                return NotFound();
            }




        }
    }
}
