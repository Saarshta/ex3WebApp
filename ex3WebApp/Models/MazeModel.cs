using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;
using MazeLib;
using System.Net.Sockets;
using Adapters;
namespace ex3.Models
{

    /// <summary>
    /// A mazemodel class.
    /// </summary>
    /// <seealso cref="Server.Model.IModel" />
    class MazeModel : IModel
    {

        /// <summary>
        /// The data base
        /// </summary>
        private MazeModelDataBase dataBase;
        /// <summary>
        /// The controler
        /// </summary>

        /// <summary>
        /// Initializes a new instance of the <see cref="MazeModel"/> class.
        /// </summary>
        /// <param name="c">The c.</param>
        public MazeModel()
        {
            dataBase = new MazeModelDataBase();
        }

        /// <summary>
        /// Joins the game.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="guest">The client.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">no such game, re-enter a command</exception>
        public MazeGame JoinGame(string name, string guest)
        {
            if (!dataBase.IsGameExists(name))
            {
                throw new ArgumentException("no such game");
            }
           
            MazeGame game =  dataBase.GetGame(name);
            game.Join(guest);
            return game;
        }

        /// <summary>
        /// Generates a maze with the given name, in the given size.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="row">The row.</param>
        /// <param name="col">The col.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">name already taken, re-enter a command</exception>
        public Maze Generate(string name, int row, int col)
        {
            if (dataBase.IsMazeExists(name))
            {
                throw new ArgumentException("name already taken");
            }
            MazeGeneratorLib.DFSMazeGenerator generator =
                               new MazeGeneratorLib.DFSMazeGenerator();
            Maze maze = generator.Generate(row, col);
            maze.Name = name;
            dataBase.AddMaze(maze);
            return maze;
            
        }

        /// <summary>
        /// Solves the specified maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="algo">The search algorithm.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">no such maze , re-enter a command</exception>
        public Solution<State<Position>> Solve(string name, Searcher<Position> algo)
        {
            if (!dataBase.IsMazeExists(name))
            {
                throw new ArgumentException("no such maze , re-enter a command");
            }
            Maze maze = dataBase.GetMaze(name);
            MazeAdapter mazeA = new MazeAdapter(maze);
            if (algo is Dfs<Position>)
            {
                if (!dataBase.IsDfsSolutionExists(name))
                {
                    dataBase.AddDfsSolution(name, algo.Search(mazeA));
                    State<Position>.StatePool.ResetPool();
                }
                return dataBase.getDfsSolution(name);
            }
            else
            {
                if (!dataBase.IsBfsSolutionExists(name))
                {
                    dataBase.AddBfsSolution(name, algo.Search(mazeA));
                    State<Position>.StatePool.ResetPool();
                }
                return dataBase.getBfsSolution(name);
            }
        }

        /// <summary>
        /// Adds the game.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="r">The r.</param>
        /// <param name="c">The c.</param>
        /// <param name="client">The client.</param>
        /// <exception cref="System.ArgumentException">name already taken, re-enter a command</exception>
        public void AddGame(string name, int r, int c, string clientId)
        {
            if (dataBase.IsGameExists(name))
            {
                throw new ArgumentException("name already taken");
            }
            MazeGeneratorLib.DFSMazeGenerator generator =
                               new MazeGeneratorLib.DFSMazeGenerator();
            Maze maze = generator.Generate(r, c);
            maze.Name = name;
            MazeGame game = new MazeGame(maze, clientId);
            dataBase.AddGame(name, game);
        }

        /// <summary>
        /// Removes the game.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <exception cref="System.ArgumentException">no such game, re-enter a command</exception>
        public void RemoveGame(string name)
        {
            if (!dataBase.IsGameExists(name))
            {
                throw new ArgumentException("no such game, re-enter a command");
            }
            this.dataBase.RemoveGame(name);
        }

        /// <summary>
        /// Gets the game.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">no such game, re-enter a command</exception>
        public MazeGame GetGame(string name)
        {
            if (!dataBase.IsGameExists(name))
            {
                throw new ArgumentException("no such game");
            }
            return this.dataBase.GetGame(name);
        }

        /// <summary>
        /// Gets the game by client.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException">you are not taking part in a game</exception>
        public MazeGame GetGameByClient(string client)
        {
            MazeGame game =  this.dataBase.GetGameByClient(client);
            if(game == null)
            {
                throw new InvalidOperationException("you are not taking part in a game");
            }
            return game;
        }

        /// <summary>
        /// Determines whether the client is part of a multiplayer game.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <returns>
        ///   <c>true</c> if [is in multi game] [the specified client]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsInMultiGame(string client)
        {
            MazeGame game = this.dataBase.GetGameByClient(client);
            if (game == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Gets the available games.
        /// </summary>
        /// <returns></returns>
        public List<string> GetGames()
        {
            return dataBase.GetGamesList();
        }
        
    }


}
