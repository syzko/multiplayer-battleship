/*////////////////////////////////////////////////////////////////
// Programmers: Steven Bensinger, Dudly Harris, Dennis Loosey   //
// Course: GSD311				                                //
// Program Name: Seminar 6 Deliverable 2                        //
// Created: 03/26/2017                  				        //
// Last Modified: 03/27/2017			                        //
////////////////////////////////////////////////////////////////*/

using System;

namespace Week6
{
    class Battleship : Ship
    {
        public Battleship() : base(4, ConsoleColor.DarkGreen, ShipTypes.Battleship)
        {

        }

        public override bool IsBattleShip => true;
    }
}
