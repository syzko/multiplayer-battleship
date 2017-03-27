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

        //grid for ship placement AI
        private string[,] spaces { get; set; }

        //direction for ship placement AI
        private Direction direction { get; set; }

        //list for attack guesses
        private static readonly List<Position> Guesses = new List<Position>();

        //random generators for AI
        private static readonly Random rand = new Random();
        private static readonly Random rand2 = new Random();

        // added fields and list for our algorithms
        private static int lastX, lastY;
        private static Position lastPosition = new Position(lastX, lastY);
        private static readonly List<Position> allGridPlays = new List<Position>();
        private static bool lastShotHit = false;
        private static bool lastShotSank = false;

        // just required for test guess code
        private static bool firstPassComplete = false;
        private enum playDirection { Vertical, Horizontal }
        private playDirection playdirection;


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

            //variable for placement loop
            bool okayMove = false;

            //loop to push the desired ships on a stack
            foreach (var ship in ships._ships)
            {
                okayMove = false;

                //loop to place ships until gone
                while (!okayMove)
                {
                    //counter to verify that there are no collisions
                    int goodSpace = 0;

                    //generates random cooridinates
                    int x = rand.Next(1, 10);
                    int y = rand.Next(1, 10);

                    //generates a random number for use in assigning direction
                    int directionNumber = rand.Next(1, 10);

                    //assigns a direction
                    if (directionNumber > 0 && directionNumber < 6)
                    {
                        direction = Direction.Horizontal;
                    }
                    else
                    {
                        direction = Direction.Vertical;
                    }



                    //checks for direction and valid starting coordinate
                    if (spaces[y, x] == "good" && direction == Direction.Horizontal)
                    {
                        //check to make sure variables stay within scope
                        if (y + ship.Length < _gridSize)
                        {
                            //checks for more valid spaces
                            for (int i = 0; i < ship.Length; i++)
                            {
                                if (spaces[y + i, x] == "good")
                                {
                                    goodSpace++;
                                }
                            }
                        }

                        //if no collisions places boat in grid, adds to the ship's position array, and pops it out of the stack
                        if (goodSpace == ship.Length)
                        {
                            ship.Place(new Position(y, x), Direction.Horizontal);

                            for (int i = 0; i < ship.Length; i++)
                            {
                                spaces[y + i, x] = "used";
                            }

                            okayMove = true;
                        }
                    }

                    //same as the last block of code, but for different direction
                    else if (spaces[y, x] == "good" && direction == Direction.Vertical)
                    {
                        //check to make sure variables stay within scope
                        if (x + ship.Length < _gridSize)
                        {
                            //checks for more valid spaces
                            for (int i = 0; i < ship.Length; i++)
                            {
                                if (spaces[y, x + i] == "good")
                                {
                                    goodSpace++;
                                }
                            }
                        }

                        //if no collisions places boat in grid, adds to the ship's position array, and pops it out of the stack
                        if (goodSpace == ship.Length)
                        {
                            ship.Place(new Position(y, x), Direction.Vertical);

                            for (int i = 0; i < ship.Length; i++)
                            {
                                spaces[y, x + i] = "used";
                            }

                            okayMove = true;
                        }
                    }
                    
                }
            }

        }

        private void GenerateGuesses()
        {
            //We want all instances of RandomPlayer to share the same pool of guesses
            //So they don't repeat each other.

            //We need to populate the guesses list, but not for every instance - so we only do it if the set is missing some guesses
            if (Guesses.Count < _gridSize * _gridSize)
            {
                Guesses.Clear();
                for (int x = 0; x < _gridSize; x++)
                {
                    for (int y = 0; y < _gridSize; y++)
                    {
                        Guesses.Add(new Position(x, y));
                    }
                }
            }
        }

        // Test code to move in a pattern vertical or horizontal across the grid
        // To use this, it must have comparison code added to prevent repeat guesses
        public Position GetAttackPosition()
        {
            int prevX, prevY;
            prevX = lastPosition.X;
            prevY = lastPosition.Y;
            Random rand3 = new Random();
            Random rand4 = new Random();
            Position myGuess = new Position(0, 0);
            if (!firstPassComplete)
            {
                playdirection = (Index % 2 == 0) ? playDirection.Horizontal : playDirection.Vertical;
                myGuess.X = rand3.Next(0, _gridSize);
                myGuess.Y = rand4.Next(0, _gridSize);
                firstPassComplete = true;
            }
            else
            {
                if (firstPassComplete && playdirection == playDirection.Horizontal)
                {
                    myGuess.X = (lastPosition.X <= _gridSize - 3) ? lastPosition.X + 2
                        : (lastPosition.X == _gridSize - 2) ? 0 : 1;
                    myGuess.Y = (lastPosition.X < _gridSize - 2) ? lastPosition.Y : lastPosition.Y + 1;
                    myGuess.Y = (myGuess.Y <= _gridSize - 1) ? myGuess.Y : 0;
                }
                else
                {
                    myGuess.X = (lastPosition.Y < _gridSize - 1) ? lastPosition.X : lastPosition.X + 1;
                    myGuess.Y = (lastPosition.Y <= _gridSize - 3) ? lastPosition.Y + 2
                        : (lastPosition.Y < _gridSize - 2) ? lastPosition.Y + 0 : 1;
                    myGuess.X = (myGuess.X <= _gridSize - 1) ? myGuess.X : 0;
                }
            }

            while (CheckGridAvailable(myGuess))
            {
                if (playdirection == playDirection.Horizontal)
                {
                    myGuess.X++;
                    if (myGuess.X > _gridSize - 1)
                    {
                        myGuess.X = 0;
                        myGuess.Y++;
                        if (myGuess.Y > _gridSize - 1)
                            myGuess.Y = 0;
                    }
                }
                else
                {
                    myGuess.Y++;
                    if (myGuess.Y > _gridSize - 1)
                    {
                        myGuess.Y = 0;
                        myGuess.X++;
                        if (myGuess.X > _gridSize - 1)
                            myGuess.X = 0;
                    }
                }
            }
            lastPosition = myGuess;
            return myGuess;
        }
        bool gridUsed = false;
        private bool CheckGridAvailable(Position p)
        {
            foreach (Position pos in allGridPlays)
            {

                gridUsed = (p.X == pos.X && p.Y == pos.Y) ? true : false;
                if (gridUsed) break;
            }
            return gridUsed;
        }

        // IPlayer SetAttackResults method implemented to read values from all plays
        // by all players to get data required for our algorithm
        public void SetAttackResults(List<AttackResult> results)
        {
            int _x = 0;
            int _y = 0;
            int _playerIndex;
            ShipTypes _shipType;
            Position _position;
            lastShotHit = false;
            lastShotSank = false;
            foreach (var result in results)
            {
                _playerIndex = result.PlayerIndex;
                _position = result.Position;
                if (_position != null)
                {
                    _x = _position.X;
                    _y = _position.Y;
                }
                lastShotHit = (result.ResultType == AttackResultType.Hit) ? true : false;
                lastShotSank = (result.ResultType == AttackResultType.Sank) ? true : false;
                _shipType = result.SunkShip;
            }
            allGridPlays.Add(new Position(_x, _y));
        }

        public string Name { get; }

        public int Index => _index;
    }
}
