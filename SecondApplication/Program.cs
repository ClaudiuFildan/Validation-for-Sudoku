using System;

namespace SecondApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialization of the Sudoku matrix. 
            int[,] matrix = {  {7,8,4,  1,5,9,  3,2,6 },
                               {5,3,9,  6,7,2,  8,4,1 },
                               {6,1,2,  4,3,8,  7,5,9 },
                               {9,2,8,  7,1,5,  4,6,3 },
                               {3,5,7,  8,4,6,  1,9,2 },
                               {4,6,1,  9,2,3,  5,8,7 },
                               {8,7,6,  3,9,4,  2,1,5 },
                               {2,4,3,  5,6,1,  9,7,8 },
                               {1,9,5,  2,8,7,  6,3,4 }   };

            // Displaying the Sudoku matrix.
            Console.WriteLine("Initial Matrix:");
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }

            // The function FirstFunction gives the final answer regarding the validation.
            // If true, the matrix in correctly filled and the game is finish.
            // If false,the matrix in incorrectly filled and the game is not over.
            if (FirstFunction(matrix))
            {
                Console.WriteLine("\nVALIDATION:TRUE");
            }
            else
            {
                Console.WriteLine("\nVALIDATION:FALSE");
            }
        }
        public static bool FirstFunction(int[,]  matrix)
        {
            // Take the dimension of the matrix and convert it for future usage.
            int n = Convert.ToInt32(matrix.GetLength(0));
            Console.WriteLine("\nValue of N:"+n);

            // Check if the Value N is a perfect square
            // If it is not, the method stops. Otherwise it continues.
            int sqrtn = Convert.ToInt32(Math.Sqrt(matrix.GetLength(0)));
            Console.WriteLine("\nValue of the square root N:" + sqrtn);
            if ((Math.Sqrt(matrix.GetLength(0)) - sqrtn) != 0){
                return false;
            }

            // Check if any value of the matrix is equal to zero.
            // If a value is zero, the matrix is incorrectly filled and the method stops.
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (matrix[i, j] == 0)
                    {
                        return false;
                    }
                }
            }

            // The logical value answerRow     is used to check if a row is correctly filled.
            // the logical value answerAllRows is used to check if all the rows are correctly filled. 
            bool answerRow = true;
            bool answerAllRows = true;

            // it goes through each row and check if it is correct.
            // If a row is incorrect then previous assumption, that all the columns are correct,is wrong and the rows are incorrect filled.
            for (int i=0;i<n;i++)
            {
                answerRow=CheckRow(matrix, i, n);
                if (answerRow == false)
                {
                    answerAllRows = false;
                }
            }

            // The variable answerAllRows will be true if no row is found wrong.
            Console.WriteLine("\nLogical value of the rows check:");
            Console.WriteLine(answerAllRows);

            // The logical value answerColumn  is used to check if a column is correctly filled.
            // the logical value answerAllRows is used to check if all the columns are correctly filled. 
            bool answerColumn = true;
            bool answerAllColumns = true;

            // It goes through each column and check if it is correct.
            // If a column is incorrect then previous assumption, that all the columns are correct, is wrong and the columns are incorrect filled.
            for (int i = 0; i < n; i++)
            {
                answerColumn = CheckColumn(matrix, i, n);
                if (answerColumn == false)
                {
                    answerAllColumns = false;
                }
            }

            // The variable answerAllColumns will be true if no column is found wrong.
            Console.WriteLine("\nLogical value of the columns check:");
            Console.WriteLine(answerAllColumns);

            // The logical value answerSquare  is used to check if an inner square is correctly filled.
            // the logical value answerAllRows is used to check if all the inner squares are correctly filled. 
            bool answerSquare = true;
            bool answerAllSquares = true;

            // The variable matrixsmall is used to select an inner square from the main square. 
            int[,] matrixsmall = { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };

            // The first two loops goes through the main square and the last two ones are used to manage the selected inner square.  
            for(int i = 0; i < sqrtn; i++)
            {
                for(int j = 0; j < sqrtn; j++)
                {
                    for(int ii = 0; ii < sqrtn; ii++)
                    {
                        for (int jj = 0; jj < sqrtn; jj++)
                        {
                            matrixsmall[ii,jj] = matrix[i*3+ii,j*3+jj];
                        }
                    }

                    // Display the selected inner square matrix.
                    Console.WriteLine("\n\nMatrix of the small square ("+i+","+j+")");
                    for (int ii = 0; ii < sqrtn; ii++)
                    {
                        for (int jj = 0; jj < sqrtn; jj++)
                        {
                           Console.Write(matrixsmall[ii,jj]);
                        }
                        Console.WriteLine();
                    }

                    // Check if the selected inner square if correctly filled.
                    // If an inner square is wrong, the previous assumption, that all the square are correct, is worng.
                    answerSquare = CheckSquare(matrixsmall,sqrtn);
                    if(answerSquare == false)
                    {
                        answerAllSquares = false;
                    }
                
                }
            }

            // The variable answerAllSquares will be true if no inner square is found wrong.
            Console.WriteLine("\n\nValue of square check:");
            Console.WriteLine(answerAllSquares);

            // The variable finalAnswer is used to combined all the previous checks of the rows, columns, and inner squares.
            // The is return to the main function.
            bool finalAnswer = false;
            if (answerAllRows == true && answerAllColumns == true && answerAllSquares)
            {
                finalAnswer = true;
            }
            else
            {
                finalAnswer = false;
            }

            return finalAnswer;
        }

        // The function CheckSquare is used to check if an inner square is correctly filled.
        public static bool CheckSquare(int[,] matrix,int sqrtn)
        {
            // The variable auxVector is used count how many times a number appears.
            // In the first position of the vector, the number of times the figure 1 is counted.
            // The same thing is applied for all the position of the vector for the remaining figures.
            // It is assumed that the square is correctly filled out using the variable check.
            int[] auxVector = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            bool check = true;

            // It passes through the selected matrix. The switch is used to increment the position of 
            // the vector auxVector coresponding to that value. If the figure is not found, an error message appears.
            for (int i = 0; i < sqrtn; i++)
            {
                for (int j = 0; j < sqrtn; j++)
                {
                    switch (matrix[i, j])
                    {
                        case 1:
                            auxVector[0] = auxVector[0] + 1; break;
                        case 2:
                            auxVector[1] = auxVector[1] + 1; break;
                        case 3:
                            auxVector[2] = auxVector[2] + 1; break;
                        case 4:
                            auxVector[3] = auxVector[3] + 1; break;
                        case 5:
                            auxVector[4] = auxVector[4] + 1; break;
                        case 6:
                            auxVector[5] = auxVector[5] + 1; break;
                        case 7:
                            auxVector[6] = auxVector[6] + 1; break;
                        case 8:
                            auxVector[7] = auxVector[7] + 1; break;
                        case 9:
                            auxVector[8] = auxVector[8] + 1; break;

                        default: Console.Write("Error"); break;

                    }

                }
            }

            // Display the vector which contains the number of appearances.
            Console.WriteLine("Frequency vector");
            for (int j = 0; j < 9; j++)
            {
                Console.Write(auxVector[j] + " ");

                // Check if there are any missing figures, which means that the square is incorrect.
                if (auxVector[j] == 0)
                {
                     check = false;
                }

            }
            
            // Return the logical value of the previous assumption.
            return check;
        }

        // The function CheckRow is used to check if a row is correctly filled.
        public static bool CheckRow(int[,] matrix, int i, int n)
        {
            // The variable auxVector is used count how many times a number appears.
            // In the first position of the vector, the number of times the figure 1 is counted.
            // The same thing is applied for all the position of the vector for the remaining figures.
            // It is assumed that the row is correctly filled out using the variable check.
            int[] auxVector = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            bool check = true;

            // It passes through the main matrix. The switch is used to increment the position of 
            // the vector auxVector coresponding to that value. If the figure is not found, an error message appears.
            for (int j = 0; j < n; j++)
            {
                switch (matrix[i, j])
                {
                    case 1:
                        auxVector[0] = auxVector[0] + 1; break;
                    case 2:
                        auxVector[1] = auxVector[1] + 1; break;
                    case 3:
                        auxVector[2] = auxVector[2] + 1; break;
                    case 4:
                        auxVector[3] = auxVector[3] + 1; break;
                    case 5:
                        auxVector[4] = auxVector[4] + 1; break;
                    case 6:
                        auxVector[5] = auxVector[5] + 1; break;
                    case 7:
                        auxVector[6] = auxVector[6] + 1; break;
                    case 8:
                        auxVector[7] = auxVector[7] + 1; break;
                    case 9:
                        auxVector[8] = auxVector[8] + 1; break;

                    default: Console.Write("Error"); break;

                }
            }

            // Display the vector which contains the number of appearances.
            Console.WriteLine("\nFrequency vector");
            for (int j = 0; j < 9; j++)
            {
                // Check if there are any missing figures, which means that the row is incorrect.
                Console.Write(auxVector[j] + " ");
                if (auxVector[j] == 0)
                {
                    check = false;
                }
            }

            // Return the logical value of the previous assumption.
            return check;
        }

        // The function CheckColumn is used to check if a column is correctly filled.
        public static bool CheckColumn(int[,] matrix, int i, int n)
        {

            // The variable auxVector is used count how many times a number appears.
            // In the first position of the vector, the number of times the figure 1 is counted.
            // The same thing is applied for all the position of the vector for the remaining figures.
            // It is assumed that the column is correctly filled out using the variable check.
            int[] auxVector = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            bool check = true;

            // It passes through the main matrix. The switch is used to increment the position of 
            // the vector auxVector coresponding to that value. If the figure is not found, an error message appears.
            for (int j = 0; j < n; j++)
            {
                
                switch (matrix[j,i])
                {
                    case 1:
                        auxVector[0] = auxVector[0] + 1; break;
                    case 2:                        
                        auxVector[1] = auxVector[1] + 1; break;
                    case 3:
                        auxVector[2] = auxVector[2] + 1; break;
                    case 4:
                        auxVector[3] = auxVector[3] + 1; break;
                    case 5:
                        auxVector[4] = auxVector[4] + 1; break;
                    case 6:                        
                        auxVector[5] = auxVector[5] + 1; break;
                    case 7:                        
                        auxVector[6] = auxVector[6] + 1; break;
                    case 8:
                        auxVector[7] = auxVector[7] + 1; break;
                    case 9:
                        auxVector[8] = auxVector[8] + 1; break;

                    default: Console.Write("Error"); break;
                        
                }

            }

            // Display the vector which contains the number of appearances.
            Console.WriteLine("\nFrequency vector");
            for (int j = 0; j < 9; j++)
            {
                // Check if there are any missing figures, which means that the column is incorrect.
                Console.Write(auxVector[j] +" ");
                if (auxVector[j] == 0)
                {
                    check = false;
                }
                              
            }

            // Return the logical value of the previous assumption.
            return check;
        }
    }


}
