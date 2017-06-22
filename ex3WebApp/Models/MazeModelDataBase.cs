using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;
using MazeLib;
using System.Net.Sockets;

namespace ex3.Models
{
    /// <summary>
    ///  a database for the maze model.
    /// </summary>
    public class MazeModelDataBase
    {
        /// <summary>
        /// The maze DFS solutions
        /// </summary>
         static Dictionary<int, Solution<State<Position>>> MazeDfsSolutions;
        /// <summary>
        /// The maze BFS solutions
        /// </summary>
         static Dictionary<int, Solution<State<Position>>> MazeBfsSolutions;
        /// <summary>
        /// The single player mazes
        /// </summary>
         static Dictionary<int, Maze> SinglePlayerMazes;
        /// <summary>
        /// The games
        /// </summary>
         static Dictionary<int, MazeGame> games;

        /// <summary>
        /// Adds the game.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="game">The game.</param>
        public void AddGame(string name, MazeGame game)
        {
           MazeModelDataBase.games.Add(name.GetHashCode(), game);
        }

        /// <summary>
        /// Gets the game by client.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        public MazeGame GetGameByClient(string client)
        {
            foreach(MazeGame game in games.Values)
            {
                if (game.IsPartOfGame(client) == true)
                {
                    return game;
                }
            }
            return null;
        }

        /// <summary>
        /// Gets the game.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public MazeGame GetGame(string name)
        {
            return games[name.GetHashCode()];
        }

        /// <summary>
        /// Determines whether the game with the given name exists..
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>
        ///   <c>true</c> if [is game exists] [the specified name]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsGameExists(string name)
        {
            return games.ContainsKey(name.GetHashCode());
        }

        /// <summary>
        /// Removes the game.
        /// </summary>
        /// <param name="name">The name.</param>
        public void RemoveGame(string name)
        {

            MazeModelDataBase.games.Remove(name.GetHashCode());
        }

        /// <summary>
        /// Gets the games list.
        /// </summary>
        /// <returns></returns>
        public List<string> GetGamesList()
        {
            List<string> list = new List<string>();
            foreach(MazeGame game in games.Values)
            {
                //add only available games to the list
                if(game.ActivePlayers == 1)
                {
                    list.Add(game.Maze.Name);
                }
                
            }
            return list;
        }


        /// <summary>
        /// Adds the maze.
        /// </summary>
        /// <param name="maze">The maze.</param>
        public void AddMaze(Maze maze)
        {

            MazeModelDataBase.SinglePlayerMazes.Add(maze.Name.GetHashCode(), maze);
        }

        /// <summary>
        /// Determines whether maze with the given name exists..
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>
        ///   <c>true</c> if [is maze exists] [the specified name]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsMazeExists(string name)
        {
            if (SinglePlayerMazes.ContainsKey(name.GetHashCode()))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets the BFS solution.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public Solution<State<Position>> getBfsSolution(string name)
        {
            if (MazeBfsSolutions.ContainsKey(name.GetHashCode()))
            {
                return MazeBfsSolutions[name.GetHashCode()];
            }
            return null;
        }

        /// <summary>
        /// Gets the DFS solution.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public Solution<State<Position>> getDfsSolution(string name)
        {
            if (MazeDfsSolutions.ContainsKey(name.GetHashCode()))
            {
                return MazeDfsSolutions[name.GetHashCode()];
            }
            return null;
        }

        /// <summary>
        /// Adds the DFS solution.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="sol">The sol.</param>
        public void AddDfsSolution(string name, Solution<State<Position>> sol)
        {
            MazeDfsSolutions.Add(name.GetHashCode(), sol);
        }

        /// <summary>
        /// Adds the BFS solution.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="sol">The sol.</param>
        public void AddBfsSolution(string name, Solution<State<Position>> sol)
        {
            MazeBfsSolutions.Add(name.GetHashCode(), sol);
        }

        /// <summary>
        /// Determines whether [is DFS solution exists] [the specified name].
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>
        ///   <c>true</c> if [is DFS solution exists] [the specified name]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsDfsSolutionExists(string name)
        {
            return MazeDfsSolutions.ContainsKey(name.GetHashCode());
        }

        /// <summary>
        /// Determines whether [is BFS solution exists] [the specified name].
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>
        ///   <c>true</c> if [is BFS solution exists] [the specified name]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsBfsSolutionExists(string name)
        {
            return MazeBfsSolutions.ContainsKey(name.GetHashCode());
        }

        /// <summary>
        /// Gets the maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public Maze GetMaze(string name)
        {
            if (SinglePlayerMazes.ContainsKey(name.GetHashCode()))
            {
                return SinglePlayerMazes[name.GetHashCode()];
            }
            return null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MazeModelDataBase"/> class.
        /// </summary>
         static MazeModelDataBase()
        {
            MazeModelDataBase.SinglePlayerMazes = new Dictionary<int, Maze>();
            MazeModelDataBase.MazeDfsSolutions = new Dictionary<int, Solution<State<Position>>>();
            MazeModelDataBase.MazeBfsSolutions = new Dictionary<int, Solution<State<Position>>>();
            MazeModelDataBase.games = new Dictionary<int, MazeGame>();
            

        }
        


    }
}
