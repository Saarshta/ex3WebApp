using SearchAlgorithmsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Adapters
{
    /// <summary>
    /// An adapter which represents a searchable maze.
    /// </summary>
    /// <seealso cref="SearchAlgorithmsLib.ISearchable{MazeLib.Position}" />
    public class MazeAdapter : ISearchable<MazeLib.Position>
    {
        private MazeLib.Maze maze;
        /// <summary>
        /// Initializes a new instance of the <see cref="MazeAdapter"/> class.
        /// </summary>
        /// <param name="maze">The maze.</param>
        public MazeAdapter(MazeLib.Maze maze)
        {
            this.maze = maze;
            
        }


        /// <summary>
        /// Gets all possible neighbour states.
        /// </summary>
        /// <param name="s">The node which we will find it's neighbours..</param>
        /// <returns></returns>
        public List<State<MazeLib.Position>> GetAllPossibleStates(State<MazeLib.Position> s)
        {
            List<State<MazeLib.Position>> neighborList = new List<State<MazeLib.Position>>();
            //left , up , right, down - order
            try
            {
                //left neighbor
                if (GetCellType(s.InnerState.Row, s.InnerState.Col - 1) == MazeLib.CellType.Free)
                {
                    State<MazeLib.Position> leftNeighbor = State<MazeLib.Position>.StatePool.GetState(new MazeLib.Position(s.InnerState.Row, s.InnerState.Col - 1));
                    neighborList.Add(leftNeighbor);
                    CheckAndUpdateCost(s, leftNeighbor);
                }
            }
            catch (IndexOutOfRangeException e) { };
            try
            {
                //upper neighbor
                if (GetCellType(s.InnerState.Row - 1, s.InnerState.Col) == MazeLib.CellType.Free)
                {
                    State<MazeLib.Position> lowerNeighbor = State<MazeLib.Position>.StatePool.GetState(new MazeLib.Position(s.InnerState.Row - 1, s.InnerState.Col));
                    neighborList.Add(lowerNeighbor);
                    CheckAndUpdateCost(s, lowerNeighbor);
                }
            }
            catch (IndexOutOfRangeException e) { };
            try
            {
                //right neighbor
                if (GetCellType(s.InnerState.Row, s.InnerState.Col + 1) == MazeLib.CellType.Free)
                {
                    State<MazeLib.Position> rightNeighbor = State<MazeLib.Position>.StatePool.GetState(new MazeLib.Position(s.InnerState.Row, s.InnerState.Col + 1));
                    neighborList.Add(rightNeighbor);
                    CheckAndUpdateCost(s, rightNeighbor);
                }
            }
            catch (IndexOutOfRangeException e) { };
            try

            {
                //lower neighbor
                if (GetCellType(s.InnerState.Row + 1, s.InnerState.Col) == MazeLib.CellType.Free)
                {
                    State<MazeLib.Position> upperNeighbor = State<MazeLib.Position>.StatePool.GetState(new MazeLib.Position(s.InnerState.Row + 1, s.InnerState.Col));
                    neighborList.Add(upperNeighbor);
                    CheckAndUpdateCost(s, upperNeighbor);
                }
            }
            catch (IndexOutOfRangeException e) { };

            return neighborList;
            
        }

        /// <summary>
        /// Checks if the cost of the neighbour needs to be updated.
        /// </summary>
        /// <param name="s">The node.</param>
        /// <param name="neighbor">The neighbor.</param>
        private void CheckAndUpdateCost(State<MazeLib.Position> s, State<MazeLib.Position> neighbor)
        {
            if(s.Cost+1 < neighbor.Cost)
            {
                neighbor.Cost = s.Cost + 1;
                neighbor.CostUpdated = true;
                neighbor.CameFrom = s;
              
            }
        }

        /// <summary>
        /// Gets the type of the cell.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="col">The col.</param>
        /// <returns>the type of the cell, free or wall.</returns>
        /// <exception cref="System.IndexOutOfRangeException"></exception>
        public MazeLib.CellType GetCellType(int row, int col)
        {
            if(row<0 || row >= maze.Rows || col < 0 || col >= maze.Cols)
            {
                throw new IndexOutOfRangeException();
            }
            return maze[row, col];
        }

        /// <summary>
        /// Gets the goal state.
        /// </summary>
        /// <returns></returns>
        public State<MazeLib.Position> GetGoalState()
        {
         
            return State<MazeLib.Position>.StatePool.GetState(maze.GoalPos);
        }

        /// <summary>
        /// Gets the initial state.
        /// </summary>
        /// <returns></returns>
        public State<MazeLib.Position> GetInitialState()
        {
            State<MazeLib.Position>  init = State<MazeLib.Position>.StatePool.GetState(maze.InitialPos);
            init.Cost = 0;
            return init;
            
        }

    }
}
