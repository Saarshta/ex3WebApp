using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;
using Newtonsoft.Json;
using Converters;
namespace Adapters
{

    /// <summary>
    /// Represents a maze solution adapter.
    /// </summary>
    public class MazeSolutionAdapter
    {
        private Solution<State<MazeLib.Position>> sol;

        /// <summary>
        /// Initializes a new instance of the <see cref="MazeSolutionAdapter"/> class.
        /// </summary>
        /// <param name="sol">The solution.</param>
        public MazeSolutionAdapter(Solution<State<MazeLib.Position>> sol)
        {
            this.sol = sol;
            
        }

        /// <summary>
        /// Returns the string representation of the solution.
        /// </summary>
        /// <returns></returns>
        public String SolutionToString()
        {
            StringBuilder stringSolution = new StringBuilder();
            // Building the string.
            for (int i = 1; i < sol.Size(); i++)

            {
                State<MazeLib.Position> prev = sol.GetItemAt(i - 1);

                State<MazeLib.Position> curr = sol.GetItemAt(i);

                if (curr.InnerState.Col > prev.InnerState.Col)
                    {
                        stringSolution.Append((int)MazeLib.Direction.Right);
                    }
                    if (curr.InnerState.Col < prev.InnerState.Col)
                    {
                        stringSolution.Append((int)MazeLib.Direction.Left);
                    }
                    if (curr.InnerState.Row > prev.InnerState.Row)
                    {
                        stringSolution.Append((int)MazeLib.Direction.Down);
                    }
                    if (curr.InnerState.Row < prev.InnerState.Row)
                    {
                        stringSolution.Append((int)MazeLib.Direction.Up);
                    }

            }
            return stringSolution.ToString();
        }


        /// <summary>
        /// Returns a string representation of the solution's json serialization.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public string ToJson(string name)
        {
            // Creates an object which holds only the relevant information for the json format.
            SolutionToJson jsonSol = new SolutionToJson(name, this.SolutionToString(), sol.GetMovesDone());
            return JsonConvert.SerializeObject(jsonSol);
        }

           }
}
