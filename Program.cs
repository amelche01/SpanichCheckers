using System;

public class Checkers
{
    public enum Piece { NN, BN, WN, WK, BK };//EE=>empty ,WN=>WhiteNormal,BN=>BlackNormal,WK=>WhiteKing,BlackKing
    public static bool turn = true;//true=>turn of white ,False=> turn of Black
    public Piece[,] board = new Piece[8, 8];

    public void InitializeBoard()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                board[i, j] = Piece.NN;

            }

        }
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if ((i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0))
                    board[i, j] = Piece.WN;

            }

        }
        for (int i = 5; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if ((i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0))
                    board[i, j] = Piece.BN;

            }

        }

    }
    public void PrintBoard()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                Console.Write(board[i, j] + " ");

            }
            Console.WriteLine();

        }
    }
    public bool IsValidePosition(int i, int j)
    {
        return i >= 0 && i < 8 && j >= 0 && j < 8;
    }
    public List<Tuple<int, int>> GenMovPiece(int i, int j)
    {
        List<Tuple<int, int>> Mov = new List<Tuple<int, int>>();
        if (IsValidePosition(i, j))
        {
            switch (board[i, j])
            {
                case Piece.NN:

                    break;

                case Piece.BN:
                    if (IsValidePosition(i - 1, j - 1) && board[i - 1, j - 1] == Piece.NN)
                    {
                        Mov.Add(new Tuple<int, int>(i - 1, j - 1));

                    }
                    if (IsValidePosition(i - 1, j + 1) && board[i - 1, j + 1] == Piece.NN)
                    {
                        Mov.Add(new Tuple<int, int>(i - 1, j + 1));

                    }
                    break;
                case Piece.WN:
                    if (IsValidePosition(i + 1, j - 1) && board[i + 1, j - 1] == Piece.NN)
                    {
                        Mov.Add(new Tuple<int, int>(i + 1, j - 1));

                    }
                    if (IsValidePosition(i + 1, j + 1) && board[i + 1, j + 1] == Piece.NN)
                    {
                        Mov.Add(new Tuple<int, int>(i + 1, j + 1));

                    }

                    break;
                case Piece.BK:
                    for (int k = 1; k < 8; k++)
                    {

                        if (IsValidePosition(i + k, j - k) && board[i + k, j - k] == Piece.NN)
                        {
                            Mov.Add(new Tuple<int, int>(i + k, j - k));
                        }
                        if (IsValidePosition(i + k, j + k) && board[i + k, j + k] == Piece.NN)
                        {
                            Mov.Add(new Tuple<int, int>(i + k, j + k));

                        }
                        if (IsValidePosition(i - k, j - k) && board[i - k, j - k] == Piece.NN)
                        {
                            Mov.Add(new Tuple<int, int>(i - k, j - k));

                        }
                        if (IsValidePosition(i - k, j + k) && board[i - k, j + k] == Piece.NN)
                        {
                            Mov.Add(new Tuple<int, int>(i - k, j + k));

                        }

                    }

                    break;
                case Piece.WK:
                    for (int k = 1; k < 8; k++)
                    {
                        if (IsValidePosition(i - k, j + k) && board[i - k, j + k] == Piece.NN)
                        {
                            Mov.Add(new Tuple<int, int>(i - k, j + k));

                        }
                        if (IsValidePosition(i + k, j + k) && board[i + k, j + k] == Piece.NN)
                        {
                            Mov.Add(new Tuple<int, int>(i + k, j + k));

                        }
                    }
                    break;

            }

        }
        return Mov;
    }
    public List<Tuple<int, int>> GenCapturePiece(int i, int j)
    {
        List<Tuple<int, int>> Cap = new List<Tuple<int, int>>();
        if (IsValidePosition(i, j))
        {
            switch (board[i, j])
            {
                case Piece.NN:

                    break;
                    ;
                case Piece.WN:
                    if (IsValidePosition(i + 1, j + 1) && IsValidePosition(i + 2, j + 2) && (board[i + 1, j + 1] == Piece.BN || board[i + 1, j + 1] == Piece.BK))
                    {
                        Cap.Add(new Tuple<int, int>(i + 2, j + 2));
                    }
                    if (IsValidePosition(i + 1, j - 1) && IsValidePosition(i + 2, j - 2) && (board[i + 1, j - 1] == Piece.BN || board[i + 1, j - 1] == Piece.BK))
                    {
                        Cap.Add(new Tuple<int, int>(i + 2, j - 2));
                    }
                    break;
                case Piece.BN:
                    if (IsValidePosition(i - 1, j + 1) && IsValidePosition(i - 2, j + 2) && (board[i - 1, j + 1] == Piece.WN || board[i - 1, j + 1] == Piece.WK))
                    {
                        Cap.Add(new Tuple<int, int>(i - 2, j + 2));
                    }
                    if (IsValidePosition(i - 1, j - 1) && IsValidePosition(i - 2, j - 2) && (board[i - 1, j - 1] == Piece.WN || board[i - 1, j - 1] == Piece.WK))
                    {
                        Cap.Add(new Tuple<int, int>(i - 2, j - 2));
                    }
                    break;
                case Piece.WK:
                    for (int k = 1; k < 8; k++)
                    {
                        if (IsValidePosition(i + k, j + k) && IsValidePosition(i + k + 1, j + k + 1) && board[i + k + 1, j + k + 1] == Piece.NN && (board[i + k, j + k] == Piece.BN || board[i + k, j + k] == Piece.BK))
                        {
                            Cap.Add(new Tuple<int, int>(i + k + 1, j + k + 1));
                        }
                        if (IsValidePosition(i + k, j - k) && IsValidePosition(i + k + 1, j - k - 1) && (board[i + k, j - k] == Piece.BN || board[i + k, j - k] == Piece.BK))
                        {
                            Cap.Add(new Tuple<int, int>(i + k + 1, j - k - 1));
                        }
                        if (IsValidePosition(i - k, j + k) && IsValidePosition(i - k - 1, j + k + 1) && (board[i - k, j + k] == Piece.BN || board[i - k, j + k] == Piece.BK))
                        {
                            Cap.Add(new Tuple<int, int>(i - k + 1, j + k + 1));
                        }
                        if (IsValidePosition(i - k, j - k) && IsValidePosition(i - k - 1, j - k - 1) && (board[i - k, j - k] == Piece.BN || board[i - k, j - k] == Piece.BK))
                        {
                            Cap.Add(new Tuple<int, int>(i - k - 1, j - k - 1));
                        }
                    }
                    break;
                case Piece.BK:
                    for (int k = 1; k < 8; k++)
                    {
                        if (IsValidePosition(i + k, j + k) && IsValidePosition(i + k + 1, j + k + 1) && (board[i + k, j + k] == Piece.WN || board[i + k, j + k] == Piece.WK))
                        {
                            Cap.Add(new Tuple<int, int>(i + k + 1, j + k + 1));
                        }
                        if (IsValidePosition(i + k, j - k) && IsValidePosition(i + k + 1, j - k - 1) && (board[i + k, j - k] == Piece.WN || board[i + k, j - k] == Piece.WK))
                        {
                            Cap.Add(new Tuple<int, int>(i + k + 1, j - k - 1));
                        }
                        if (IsValidePosition(i - k, j + k) && IsValidePosition(i - k - 1, j + k + 1) && (board[i - k, j + k] == Piece.WN || board[i - 1, j + 1] == Piece.WK))
                        {
                            Cap.Add(new Tuple<int, int>(i - k + 1, j + k + 1));
                        }
                        if (IsValidePosition(i - k, j - k) && IsValidePosition(i - k - 1, j - k - 1) && (board[i - k, j - k] == Piece.WN || board[i - 1, j - 1] == Piece.WK))
                        {
                            Cap.Add(new Tuple<int, int>(i - k - 1, j - k - 1));
                        }
                    }
                    break;
            }
        }
        return Cap;
    }

    public List<Tuple<int, int>> GenMovPlayers(bool turn)
    {
        List<Tuple<int, int>> MovP = new List<Tuple<int, int>>();
        if (turn)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j] == Piece.WN || board[i, j] == Piece.WK)
                    {
                        MovP.AddRange(GenMovPiece(i, j));
                    }
                }
            }

        }
        else
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j] == Piece.BN || board[i, j] == Piece.BK)
                    {
                        MovP.AddRange(GenMovPiece(i, j));
                    }
                }
            }
        }
        return MovP;
    }
    public List<Tuple<int, int>> GenCapturePlayers(bool turn)
    {
        List<Tuple<int, int>> CapP = new List<Tuple<int, int>>();
        if (turn)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j] == Piece.WN || board[i, j] == Piece.WK)
                    {
                        CapP.AddRange(GenCapturePiece(i, j));
                    }
                }
            }

        }
        else
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j] == Piece.BN || board[i, j] == Piece.BK)
                    {
                        CapP.AddRange(GenCapturePiece(i, j));
                    }
                }
            }
        }
        return CapP;
    }




    public int NumWhitePiece()
    {
        int numW = 0;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (board[i, j] == Piece.WN || board[i, j] == Piece.WK)
                    numW++;

            }

        }
        return numW;
    }
    public int NumBlackPiece()
    {
        int numB = 0;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (board[i, j] == Piece.BN || board[i, j] == Piece.BK)
                    numB++;

            }

        }
        return numB;
    }
    public void MovePiece(int fromI, int fromJ, int toI, int toJ)
    {
        Piece movingPiece = board[fromI, fromJ];
        board[toI, toJ] = movingPiece;
        board[fromI, fromJ] = Piece.NN;


        if (movingPiece == Piece.WN && toI == 7)
        {
            board[toI, toJ] = Piece.WK;
            Console.WriteLine("White piece promoted to King!");
        }


        if (movingPiece == Piece.BN && toI == 0)
        {
            board[toI, toJ] = Piece.BK;
            Console.WriteLine("Black piece promoted to King!");
        }
    }



    static void Main(string[] args)
    {
        Checkers checkers = new Checkers();
        checkers.InitializeBoard();

        while (checkers.NumBlackPiece() > 0 && checkers.NumWhitePiece() > 0)
        {
            checkers.PrintBoard();
            Console.WriteLine(turn ? "White's Turn (WN/WK)" : "Black's Turn (BN/BK)");

            var allCaptures = checkers.GenCapturePlayers(turn);
            var allMoves = checkers.GenMovPlayers(turn);

            if (allCaptures.Count == 0 && allMoves.Count == 0)
            {
                Console.WriteLine(turn ? "Black Wins!" : "White Wins!");
                break;
            }

            int fromI, fromJ;
            List<Tuple<int, int>> possibleMoves = new List<Tuple<int, int>>();


            while (true)
            {
                Console.WriteLine("Enter the piece coordinates i(0-7) j(0-7):");
                fromI = int.Parse(Console.ReadLine());
                fromJ = int.Parse(Console.ReadLine());

                if (!checkers.IsValidePosition(fromI, fromJ))
                {
                    Console.WriteLine("Invalid coordinates. Try again.");
                    continue;
                }

                var piece = checkers.board[fromI, fromJ];

                if ((turn && (piece != Checkers.Piece.WN && piece != Checkers.Piece.WK)) ||
                    (!turn && (piece != Checkers.Piece.BN && piece != Checkers.Piece.BK)))
                {
                    Console.WriteLine("You must select your own piece.");
                    continue;
                }

                var possibleCaptures = checkers.GenCapturePiece(fromI, fromJ);
                var regularMoves = checkers.GenMovPiece(fromI, fromJ);


                if (possibleCaptures.Count > 0)
                {
                    Console.WriteLine("Capture moves available:");
                    foreach (var move in possibleCaptures)
                        Console.WriteLine($"→ ({move.Item1},{move.Item2})");

                    possibleMoves = possibleCaptures;
                }
                else if (allCaptures.Count == 0 && regularMoves.Count > 0)
                {
                    Console.WriteLine("Available moves:");
                    foreach (var move in regularMoves)
                        Console.WriteLine($"→ ({move.Item1},{move.Item2})");

                    possibleMoves = regularMoves;
                }
                else
                {
                    Console.WriteLine("This piece has no valid moves. Choose another.");
                    continue;
                }

                break;
            }


            Console.WriteLine("Enter destination coordinates i(0-7) j(0-7):");
            int toI = int.Parse(Console.ReadLine());
            int toJ = int.Parse(Console.ReadLine());
            var moveTo = Tuple.Create(toI, toJ);

            if (possibleMoves.Contains(moveTo))
            {

                if (Math.Abs(fromI - toI) == 2 || Math.Abs(fromI - toI) > 2)
                {
                    int midI = (fromI + toI) / 2;
                    int midJ = (fromJ + toJ) / 2;
                    checkers.board[midI, midJ] = Checkers.Piece.NN;
                    Console.WriteLine("Captured!");
                }

                checkers.MovePiece(fromI, fromJ, toI, toJ);
                turn = !turn;
            }
            else
            {
                Console.WriteLine("Invalid move. Try again from the beginning.");
            }
        }
    }


}
