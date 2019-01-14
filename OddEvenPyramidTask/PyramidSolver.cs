using OddEvenPyramidTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OddEvenPyramidTask
{
    public class PyramidSolver
    {
        /*
         * Created this Nested class here as non other classes would care or share this
         * And no other knowledge of this class is needed in other places.
         * As well this is done in order to track previous node.
         * In a similar task to find the largest path you would add up the values and
         * this way find it.
         */
        private class Node
        {
            public int Value { get; set; }
            public Node PreviousValidNode { get; set; }
        }

        public PyramidResult GetPyramidOddEvenLongestPath(string stringArray)
        {
            var pyramid = StringToNodeArray(stringArray);
            // This is done in order to check if the convertion was successful.
            // The input string might be of any kind and would not always be successfull
            // We need to validate different case scenarios
            if (pyramid != null)
            {
                for (int i = pyramid.Length - 2; i >= 0; i--)
                {
                    for (int j = 0; j < pyramid[i].Length; j++)
                    {

                        var res = GetMostValid(pyramid[i + 1][j], pyramid[i + 1][j + 1], pyramid[i][j]);
                        if (res != null)
                            pyramid[i][j].PreviousValidNode = res;
                    }
                }

                var validPath = GetValidPath(pyramid[0][0]);

                //Meaning the longest path has a missing link assumingly which makes it not valid automatically
                if (validPath.Count != pyramid.Length) return null;

                return new PyramidResult() { MaxSum = validPath.Sum(), Path = string.Join(" ", validPath) };
            }
            return null;
        }

        private List<int> GetValidPath(Node firstNode)
        {
            List<int> path = new List<int>();
            Node currentNode = firstNode;

            while (currentNode != null)
            {
                path.Add(currentNode.Value);
                currentNode = currentNode.PreviousValidNode;
            }

            return path;
        }

        private Node[][] StringToNodeArray(string input)
        {
            //fail fast
            if (string.IsNullOrEmpty(input)) return null;

            string[] AllInputs = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            var result = new Node[AllInputs.Length][];

            for (int i = 0; i < AllInputs.Length; i++)
            {
                var stringArray = AllInputs[i].Trim().Split(" ");
                /*
                 You could do this with something like:
                 Array.ConvertAll(stringArray, s => int.Parse(s))
                 Which is fine, however if it fails to parse it will through an exception
                 we do not want it.
                 */
                Node[] myNodes = new Node[stringArray.Length];
                for (int t = 0; t < stringArray.Length; t++)
                {
                    int myInt;
                    if (int.TryParse(stringArray[t], out myInt))
                    {
                        myNodes[t] = new Node() { Value = myInt };
                    }
                    else
                    {
                        // if there are non numeric/digital values the input string is not valid, thus return null
                        return null;
                    }

                }

                result[i] = myNodes;
            }

            return result;
        }

        private bool IsOddEven(int input)
        {
            return input % 2 == 0;
        }

        private Node GetMostValid(Node leftNode, Node rightNode, Node originalNode)
        {
            if (leftNode == null || rightNode == null) return null;

            var originalIsOddEven = IsOddEven(originalNode.Value);
            var firstIsOddEven = IsOddEven(leftNode.Value);
            var secondIsOddEven = IsOddEven(rightNode.Value);

            if (firstIsOddEven ^ secondIsOddEven)
            {
                return firstIsOddEven ^ originalIsOddEven ? leftNode : rightNode;
            }
            else
            {
                /* You have to check if all of the children odd even type 
                 is the same as the parent
                 Thus preventing from an unvalid path.
                 You would need to check if both of them are true or false, 
                 which would end up in a longer if statement
                 this is why it is easier to achieve it without boolean logic.
                 */
                if (firstIsOddEven == originalIsOddEven && secondIsOddEven == originalIsOddEven)
                {
                    return null;
                }

                return leftNode.Value > rightNode.Value ? leftNode : rightNode;
            }

        }
    }
}
