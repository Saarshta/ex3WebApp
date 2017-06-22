using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
namespace ex3.Models
{

    /// <summary>
    /// A mazegame class.
    /// </summary>
    public class MazeGame
    {
        /// <summary>
        /// The maze
        /// </summary>
        private MazeLib.Maze maze;
        /// <summary>
        /// The amount of active players
        /// </summary>
        private int activePlayers;
        /// <summary>
        /// The host
        /// </summary>
        private string host;
        /// <summary>
        /// The guest
        /// </summary>
        private string guest;

        /// <summary>
        /// Initializes a new instance of the <see cref="MazeGame"/> class.
        /// </summary>
        /// <param name="maze">The maze.</param>
        /// <param name="host">The host.</param>
        public MazeGame(MazeLib.Maze maze, string hostId )
        {
            this.maze = maze;
            this.host = hostId;
            this.activePlayers = 1;
        }

        /// <summary>
        /// Gets the host.
        /// </summary>
        /// <value>
        /// The host.
        /// </value>
        public string Host
        {
            get
            {
                return this.host;
            }
            
        }
        /// <summary>
        /// Gets the guest.
        /// </summary>
        /// <value>
        /// The guest.
        /// </value>
        public string Guest
        {
            get
            {
                return this.guest;
            }

        }
        /// <summary>
        /// Gets the maze.
        /// </summary>
        /// <value>
        /// The maze.
        /// </value>
        public MazeLib.Maze Maze
        {
            get
            {
                return maze;
            }
        }

        /// <summary>
        /// Gets the active players.
        /// </summary>
        /// <value>
        /// The active players.
        /// </value>
        public int ActivePlayers
        {
            get
            {
                return activePlayers;
            }
        }

        /// <summary>
        /// Gets the name of the game.
        /// </summary>
        /// <returns></returns>
        public string GetGameName()
        {
            return this.maze.Name;
        }

        /// <summary>
        /// Determines whether the client is part of a game.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <returns>
        ///   <c>true</c> if [is part of game] [the specified client]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsPartOfGame(string client)
        {
            if(client == Host || client == Guest)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Joins the specified guest to the game.
        /// </summary>
        /// <param name="guest">The guest.</param>
        /// <exception cref="System.InvalidOperationException">
        /// cant join, game has already began!
        /// or
        /// you are the host, re-enter a command
        /// </exception>
        public void Join(string guest)
        {
            if(activePlayers == 2)
            {
                throw new InvalidOperationException("cant join, game has already began!");
            }
            if(guest == Host)
            {
                throw new InvalidOperationException("you are the host, re-enter a command");
            }
            this.activePlayers++;
            this.guest = guest;

        }

        /// <summary>
        /// Games to string.
        /// </summary>
        /// <returns></returns>
        public string GameToString()
        {
            return this.maze.ToJSON();
        }
    }
}
