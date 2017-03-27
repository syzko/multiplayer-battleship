/*////////////////////////////////////////////////////////////////
// Programmers: Steven Bensinger, Dudly Harris, Dennis Losey    //
// Course: GSD311                                               //
// Program Name: Seminar 6 Deliverable 2                        //
// Created: 03/26/2017                                          //
// Last Modified: 03/27/2017                                    //
////////////////////////////////////////////////////////////////*/

using System;

namespace Week6
{
    public struct AttackResult
    {
        public int PlayerIndex;
        public Position Position;
        public AttackResultType ResultType;
        public ShipTypes SunkShip; //Filled in if ResultType is Sunk

        public AttackResult(int playerIndex, Position position, AttackResultType attackResultType= AttackResultType.Miss, ShipTypes sunkShip = ShipTypes.None)
        {
            PlayerIndex = playerIndex;
            Position = position;
            ResultType = attackResultType;
            SunkShip = sunkShip;
        }
    }
}
