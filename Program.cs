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
    class Program
    {
        static void Main(string[] args)
        {


            List<IPlayer> players = new List<IPlayer>();
            //players.Add(new DumbPlayer("Dumb 1"));
            //players.Add(new DumbPlayer("Dumb 2"));
            //players.Add(new DumbPlayer("Dumb 3"));
            //players.Add(new RandomPlayer("Random 1"));
            //players.Add(new RandomPlayer("Random 2"));
            //players.Add(new RandomPlayer("Random 3"));
            //players.Add(new RandomPlayer("Random 4"));
            //players.Add(new RandomPlayer("Random 5"));

            //Your code here
            players.Add(new BluePlayer("Blue Team"));

            MultiPlayerBattleShip game = new MultiPlayerBattleShip(players);
            game.Play(PlayMode.Pause);
        }
    }
}
