using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using Microsoft.AspNet.SignalR;
using ex3.Models;
using System.Web.Http;
using Newtonsoft.Json.Linq;

namespace ex3WebApp
{
    public class MultiPlayerController : Hub
    {
        private IModel mazeModel;

        public MultiPlayerController()
        {
            this.mazeModel = new MazeModel();
        }

        public void GetList()
        {
            string clientId = Context.ConnectionId;
            List<string> list = this.mazeModel.GetGames();
            Clients.Client(clientId).updateList(list);
        }

        public void CreateGame(string name, int rows, int cols)
        {
            string clientId = Context.ConnectionId;
            try
            {
                
                mazeModel.AddGame(name, rows,
                    cols, clientId);


                Clients.Client(clientId).rcvMessage("Game added");

            }
            catch (ArgumentException e)
            {
                // Content bla bla BadRequest blabla
                Clients.Client(clientId).rcvMessage(e.Message);
            }

        }
        public void Send(string name, string message)
        {

            // Call the broadcastMessage method to update clients
            Clients.All.broadcastMessage(name, message);
        }

        public void Play(int x, int y)
        {
            string clientId = Context.ConnectionId;
            MazeGame game = this.mazeModel.GetGameByClient(clientId);
            string host = game.Host;
            string guest = game.Guest;
            string other = null;
            // If i'm the guest
            if ((!(host.Equals(clientId))) && host != null)
            {
                other = host;
            } else
            {
                // I'm the host
                if (guest != null)
                {
                    other = guest;
                }
            }
            if (other != null)
            {
                Clients.Client(other).play(x, y);
            }

        }

        public void Join(string name)
        {
            int x = 0;
            int y = 0;

            string clientId = Context.ConnectionId;
            MazeGame game = this.mazeModel.JoinGame(name, clientId);
            string hostId = game.Host;
            MazeLib.Maze maze = game.Maze;
            string jsonedMaze = game.GameToString();


            MazeParam partialMaze = new MazeParam();
            partialMaze.Name = maze.Name;
            partialMaze.Rows = maze.Rows;
            partialMaze.Cols = maze.Cols;
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

            Clients.Client(clientId).joinGame(partialMaze);
            Clients.Client(hostId).joinGame(partialMaze);
        }
    }
}