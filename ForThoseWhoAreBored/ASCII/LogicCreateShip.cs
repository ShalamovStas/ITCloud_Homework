
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCII
{
    class LogicCreateShip
    {
        private string ErrorMessage { get; set; }

        public bool IsItPossibleToCreateShip(Square[] squares, int countOfParts)
        {
            if (squares.Length != countOfParts)
            {
                ErrorMessage = "Wrong parts count";
                return false;
            }
            if (
                (CheckElementsDistributionWithoutGapsAlong_Y(squares) || CheckElementsDistributionWithoutGapsAlong_X(squares))
                && 
                (CheckDispositionAlong_Y(squares) || CheckDispositionAlong_X(squares))
                )
            {

                return true;
            }
            ErrorMessage = "Wrong parts position";
            return false;
        }



        public Ship GetShip(Square[] squares)
        {
          
                return new Ship(squares);
      
        }

      



        /// <summary>     How to find if ship has gaps              
        /// 
        /// 
        /// 
        /// 
        /// X
        ///                                         │   0    1    2    3    4    5    6
        ///                                        ─┼──────────────────────────────────────►x
        ///          ┌──────┐                       │ ┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐
        ///          |  p1  |                     0 │ | p1|| ■ ||   ||   ||   ||   ||   |
        ///          | Yp1  |                       │ └───┘└───┘└───┘└───┘└───┘└───┘└───┘
        ///          └──────┘                       │ ┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐
        ///          ┌──────┐                     1 │ | p2|| ■ ||   ||   ||   ||   ||   |
        ///          | Xp1  |                       │ └───┘└───┘└───┘└───┘└───┘└───┘└───┘
        ///          | Yp2  |                       │ ┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐
        ///          └──────┘                     2 │ | p3|| ■ ||   ||   ||   ||   ||   |
        ///                                         │ └───┘└───┘└───┘└───┘└───┘└───┘└───┘
        ///                                         │ ┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐
        ///                                       3 │ | p4|| ■ ||   ||   ||   ||   ||   |
        ///                                   Y     │ └───┘└───┘└───┘└───┘└───┘└───┘└───┘
        ///                                         │ ┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐
        ///                                       4 │ |   ||   ||   || x || x || x ||   |
        ///                                         │ └───┘└───┘└───┘└───┘└───┘└───┘└───┘
        ///                                         │ ┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐
        ///                                       5 │ |   ||   || 0 || 0 ||   || 0 ||   |
        ///                                         │ └───┘└───┘└───┘└───┘└───┘└───┘└───┘
        ///                                         │ ┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐┌───┐
        ///                                       6 │ |   ||   ||   ||   ||   ||   ||   |
        ///                                         │ └───┘└───┘└───┘└───┘└───┘└───┘└───┘
        ///                                         
        ///        
        ///         ship 1 : (1,0)(1,1)(1,2)(1,3)    ■ ■ ■ ■
        ///         ship 2 : (3,4)(4,4)(5,4)         x x x
        ///         ship 3 : (2,2)(2,3)(2,4)         0 0 0
        ///         
        ///         ship 1 : 
        ///         Xp1 + Xp2 + Xp3 + Xp4 = 1 + 1 + 1 + 1 = 4
        ///         
        ///         Yp1 + Yp2 + Yp3 + Yp4 = 0 + 1 + 2 + 3 = 6
        ///         sum(for 4) = [0] + [1] + [1+1] + [1+1+1] = 6
        ///         Ypmin*count + sum = 0 * 4 + 6 = 6                    
        ///         
        /// 
        ///         ship 2 : 
        ///         Yp1 + Yp2 + Yp3 = 4 + 4 + 4 = 12
        ///         
        ///         Xp1 + Xp2 + Xp3 = 3 + 4 + 5 = 12
        ///         sum(for 3) = [0] + [1] + [1+1] = 3
        ///         Xpmin*count + sum = 3 * 3 + 3 = 12 
        ///         
        ///         ship 3 : 
        ///         Yp1 + Yp2 + Yp3 = 5 + 5 + 5 = 15
        ///         
        ///         Xp1 + Xp2 + Xp3 = 2 + 3 + 5 = 10
        ///         sum(for 3) = [0] + [1] + [1+1] = 3
        ///         Xpmin*count + sum = 2 * 3 + 3 = 9
        ///         Xp1 + Xp2 + Xp3 > Xpmin*count + sum 
        /// 
        ///         steps
        ///         1. At first, it is necessary to find the parallel axe
        ///         2. Then, you have to compare Xp1 + Xp2 + Xp3 and Xpmin*count + sum (if ship is parallel to x axe)
        ///         
        /// 
        /// 
        /// 
        /// 
        /// 
        /// 
        /// <returns></returns>
        private bool CheckElementsDistributionWithoutGapsAlong_Y(Square[] squares)
        {
            int sum = 0, rightResult, minValue = squares[0].X, calculationResult = 0;
            for (int i = 0; i < squares.Length; i++)
            {
                sum += i;
            }

            for (int i = 0; i < squares.Length; i++)
            {
                calculationResult += squares[i].Y;
                if (squares[i].Y < minValue)
                {
                    minValue = squares[i].Y;
                }
            }

            rightResult = minValue * squares.Length + sum;

            if (calculationResult == rightResult)
            {
                return true;
            }
            else
            {
                ErrorMessage = "wrong elements position along X axe";
                return false;
            }

        }


        private bool CheckElementsDistributionWithoutGapsAlong_X(Square[] squares)
        {
            int sum = 0, rightResult, minValue = squares[0].Y, calculationResult = 0;
            for (int i = 0; i < squares.Length; i++)
            {
                sum += i;
            }

            for (int i = 0; i < squares.Length; i++)
            {
                calculationResult += squares[i].X;
                if (squares[i].X < minValue)
                {
                    minValue = squares[i].X;
                }
            }

            rightResult = minValue * squares.Length + sum;

            if (calculationResult == rightResult)
            {
                return true;
            }
            else
            {
                ErrorMessage = "wrong elements position along Y axe";
                return false;
            }

        }



        public string GetErrorMessage()
        {
            return ErrorMessage;
        }


        

        private bool CheckDispositionAlong_Y(Square[] squares)
        {
            int firstElementCoordinate_X = squares[0].X;
           

            for (int i = 0; i < squares.Length; i++)
            {
                if (squares[i].X != firstElementCoordinate_X)
                {
                    return false;
                }
                
            }
            return true;
        }

        private bool CheckDispositionAlong_X(Square[] squares)
        {
            
            int firstElementCoordinate_Y = squares[0].Y;

            for (int i = 0; i < squares.Length; i++)
            {
                if (squares[i].Y != firstElementCoordinate_Y)
                {
                    return false;
                }
                
            }
            return true;
        }


    }

}
