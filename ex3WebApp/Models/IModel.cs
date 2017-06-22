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
    /// A model interface.
    /// </summary>
    public interface IModel
    {
        /// <summary>
        /// Generates a maze with the given name, in the given size..
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="row">The row.</param>
        /// <param name="col">The col.</param>
        /// <returns></returns>
        Maze Generate(string name, int row, int col);

        /// <summary>
        /// Solves the specified maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="algo">The search algorithm.</param>
        /// <returns></returns>
        Solution<State<Position>> Solve(string name, Searcher<Position> algo);

        /// <summary>
        /// Gets the available games.
        /// </summary>
        /// <returns></returns>
        List<string> GetGames();

        /// <summary>
        /// Adds the game with a given name and size.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="r">The rows.</param>
        /// <param name="c">The columns.</param>
        /// <param name="host">The host.</param>
        void AddGame(string name, int r, int c, string host);

        /// <summary>
        /// Joins the game.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="guest">The client.</param>
        /// <returns></returns>
        MazeGame JoinGame(string name, string guest);

        /// <summary>
        /// Removes the game.
        /// </summary>
        /// <param name="name">The name.</param>
        void RemoveGame(string name);

        /// <summary>
        /// Gets the game.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        MazeGame GetGame(string name);

        /// <summary>
        /// Gets the game by client.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        MazeGame GetGameByClient(string client);

        /// <summary>
        /// Determines whether the client is part of a multiplayer game.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <returns>
        ///   <c>true</c> if [is in multi game] [the specified client]; otherwise, <c>false</c>.
        /// </returns>
        bool IsInMultiGame(string client);

    }
}
