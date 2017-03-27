/*////////////////////////////////////////////////////////////////
// Programmers: Steven Bensinger, Dudly Harris, Dennis Losey    //
// Course: GSD311                                               //
// Program Name: Seminar 6 Deliverable 2                        //
// Created: 03/26/2017                                          //
// Last Modified: 03/27/2017                                    //
////////////////////////////////////////////////////////////////*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week6
{
    internal class BluePlayer : IPlayer
    {
        private static int _nextGuess = 0; 

        private int _index;
        private int _gridSize;

        //stack for ship placement AI
        private Stack<Ship> shipsStack { get; set; }

        //grid for ship placement AI
        private string[,] spaces { get; set; }

        //direction for ship placement AI
        private Direction direction { get; set; }

        public BluePlayer(string name)
        {
            Name = name;
        }

        public void StartNewGame(int playerIndex, int gridSize, Ships ships)
        {
            _gridSize = gridSize;
            _index = playerIndex;

            spaces = new string[_gridSize, _gridSize];

            for (int x = 0; x < _gridSize; x++)
            {
                for (int y = 0; y < _gridSize; y++)
                {
                    spaces[x, y] = "good";
                }
            }


            //variable for boat length
            Random rand = new Random();

            //variable for boat length
            int boatLength = 0;

            //variable to exit loop when stack is out of boats
            bool emptyStack = false;

            //loop to push the desired ships on a stack
            foreach (var ship in ships._ships)
            {
          
                //shipsStack.Push(ship); //this throws an exception for some reason

            }

            //loop to place ships until gone
            while (!emptyStack)
            {
                //counter to verify that there are no collisions
                int goodSpace = 0;

                //assigns boatLength variable based on ship in the Stack
                boatLength = shipsStack.Peek().Length;

                //generates random cooridinates
                int x = rand.Next(1, 10);
                int y = rand.Next(1, 10);

                //generates a random number for use in assigning direction
                int directionNumber = rand.Next(1, 10);

                //assigns a direction
                if(directionNumber > 0 && directionNumber < 6)
                {
                    direction = Direction.Horizontal;
                }
                else
                {
                    direction = Direction.Vertical;
                }

                

                //checks for direction and valid starting coordinate
                if (spaces[y,x] == "good" && direction == Direction.Horizontal)
                {
                    //check to make sure variables stay within scope
                    if (y + boatLength < 11)
                    {
                        //checks for more valid spaces
                        for (int i = 0; i < boatLength; i++)
                        {
                            if (spaces[y + i, x] == "good")
                            {
                                goodSpace++;
                            }
                        }
                    }

                    //if no collisions places boat in grid, adds to the ship's position array, and pops it out of the stack
                    if (goodSpace == boatLength)
                    {
                        shipsStack.Peek().Place(new Position(y, x), Direction.Horizontal);

                        for (int i = 0; i < boatLength; i++)
                        {
                            spaces[y + i, x] = "used";
                        }

                        shipsStack.Pop();
                    }
                }

                //same as the last block of code, but for different direction
                else if (spaces[y, x] == "good" && direction == Direction.Vertical)
                {
                    //check to make sure variables stay within scope
                    if (x + boatLength < 11)
                    {
                        //checks for more valid spaces
                        for (int i = 0; i < boatLength; i++)
                        {
                            if (spaces[y + i, x] == "good")
                            {
                                goodSpace++;
                            }
                        }
                    }

                    //if no collisions places boat in grid, adds to the ship's position array, and pops it out of the stack
                    if (goodSpace == boatLength)
                    {
                        shipsStack.Peek().Place(new Position(y, x), Direction.Horizontal);

                        for (int i = 0; i < boatLength; i++)
                        {
                            spaces[y, x + i] = "used";
                        }

                        shipsStack.Pop();
                    }
                }


                //check for empty stack to exit the loop
                if (shipsStack.Count() == 0)
                {
                    emptyStack = true;
                }
            }

           
        }

        public Position GetAttackPosition()
        {
            Random rand = new Random();
            //A *very* naive guessing algorithm that simply starts at 0, 0 and guess each square in order
            //All 'DumbPlayers' share the counter so they won't guess the same one
            //But we don't check to make sure the square has not been guessed before
            var pos = new Position(_nextGuess % _gridSize, (_nextGuess / _gridSize));
            _nextGuess++;
            return pos;
        }

        public void SetAttackResults(List<AttackResult> results)
        {
            //DumbPlayer does nothing with these results - its going to keep making dumb guesses
        }

        public string Name { get; }

        public int Index => _index;
    }
}
