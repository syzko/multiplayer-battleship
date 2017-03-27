/*////////////////////////////////////////////////////////////////
// Programmers: Steven Bensinger, Dudly Harris, Dennis Losey    //
// Course: GSD311                                               //
// Program Name: Seminar 6 Deliverable 2                        //
// Created: 03/26/2017                                          //
// Last Modified: 03/27/2017                                    //
////////////////////////////////////////////////////////////////*/

namespace Week6
{
    public class Position
    {
        public int X;
        public int Y;
        public bool Hit;
        public Position(int x, int y)
        {
            X = x;
            Y = y;
            Hit = false;
        }
    }
}
