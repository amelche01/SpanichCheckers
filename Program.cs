using System;


public class Checkers
{
    public enum Piece
    {
        Empty = 0,
        WhiteNormal = 1,
        WhiteKing = 3,
        BlackNormal = 2,
        BlackKing = 4
    };
    public Piece[,] board = new Piece[8, 8];
    public struct Move
    {
        public int FromI;
        public int FromJ;
        public int ToI;
        public int ToJ;
        public Move(int i, int j, int toI, int toJ)
        {
            this.FromI = i;
            this.FromJ = j;
            this.ToI = toI;
            this.ToJ = toJ;
        }
    }
    public struct MoveHistory
    {
        public Move move;
        public Piece typePiece;
        public Piece capturePiece;
        public int capturedI, capturedJ;
        public bool wasKing;
    }
    public Stack<MoveHistory> moveHistory = new Stack<MoveHistory>();

    public void InitializeBoard()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                board[i, j] = Piece.Empty;

            }

        }
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if ((i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0))
                    board[i, j] = Piece.WhiteNormal;

            }

        }
        for (int i = 5; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if ((i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0))
                    board[i, j] = Piece.BlackNormal;

            }

        }

    }
    public void PrintBoard()
    {
        Console.WriteLine("   0 1 2 3 4 5 6 7 ");
        Console.WriteLine();
        for (int i = 0; i < 8; i++)
        {
            Console.Write(i + "  ");
            for (int j = 0; j < 8; j++)
            {
                Console.Write((int)board[i, j] + " ");

            }
            Console.WriteLine();
        }

    }
    public bool IsValidePosition(int i, int j)
    {
        return i >= 0 && i < 8 && j >= 0 && j < 8;
    }
    public List<Move> GenMovPiece(int i, int j)
    {
        List<Move> Mov = new List<Move>();
        if (IsValidePosition(i, j))
        {
            switch (board[i, j])
            {
                case Piece.Empty:

                    break;

                case Piece.BlackNormal:
                    if (IsValidePosition(i - 1, j - 1) && board[i - 1, j - 1] == Piece.Empty)
                    {

                        Mov.Add(new Move(i, j, i - 1, j - 1));

                    }
                    if (IsValidePosition(i - 1, j + 1) && board[i - 1, j + 1] == Piece.Empty)
                    {
                        Mov.Add(new Move(i, j, i - 1, j + 1));

                    }
                    break;
                case Piece.WhiteNormal:
                    if (IsValidePosition(i + 1, j - 1) && board[i + 1, j - 1] == Piece.Empty)
                    {
                        Mov.Add(new Move(i, j, i + 1, j - 1));

                    }
                    if (IsValidePosition(i + 1, j + 1) && board[i + 1, j + 1] == Piece.Empty)
                    {
                        Mov.Add(new Move(i, j, i + 1, j + 1));

                    }

                    break;
                case Piece.BlackKing:
                    for (int k = 1; k < 8; k++)
                    {

                        if (IsValidePosition(i + k, j - k) && board[i + k, j - k] == Piece.Empty)
                        {
                            Mov.Add(new Move(i, j, i + k, j - k));
                        }
                        if (IsValidePosition(i + k, j + k) && board[i + k, j + k] == Piece.Empty)
                        {
                            Mov.Add(new Move(i, j, i + k, j + k));

                        }
                        if (IsValidePosition(i - k, j - k) && board[i - k, j - k] == Piece.Empty)
                        {
                            Mov.Add(new Move(i, j, i - k, j - k));

                        }
                        if (IsValidePosition(i - k, j + k) && board[i - k, j + k] == Piece.Empty)
                        {
                            Mov.Add(new Move(i, j, i - k, j + k));

                        }

                    }

                    break;
                case Piece.WhiteKing:
                    for (int k = 1; k < 8; k++)
                    {
                        if (IsValidePosition(i - k, j + k) && board[i - k, j + k] == Piece.Empty)
                        {
                            Mov.Add(new Move(i, j, i - k, j + k));

                        }
                        if (IsValidePosition(i + k, j + k) && board[i + k, j + k] == Piece.Empty)
                        {
                            Mov.Add(new Move(i, j, i + k, j + k));

                        }
                        if (IsValidePosition(i - k, j - k) && board[i - k, j - k] == Piece.Empty)
                        {
                            Mov.Add(new Move(i, j, i - k, j - k));

                        }
                        if (IsValidePosition(i + k, j - k) && board[i + k, j - k] == Piece.Empty)
                        {
                            Mov.Add(new Move(i, j, i + k, j - k));

                        }
                    }
                    break;

            }

        }
        return Mov;
    }
    public List<Move> GenCapturePiece(int i, int j)
    {
        List<Move> Cap = new List<Move>();
        if (IsValidePosition(i, j))
        {
            switch (board[i, j])
            {
                case Piece.Empty:

                    break;

                case Piece.WhiteNormal:
                    if (IsValidePosition(i + 1, j + 1) && IsValidePosition(i + 2, j + 2) && board[i + 2, j + 2] == Piece.Empty && (board[i + 1, j + 1] == Piece.BlackNormal || board[i + 1, j + 1] == Piece.BlackKing))
                    {
                        Cap.Add(new Move(i, j, i + 2, j + 2));
                    }
                    if (IsValidePosition(i + 1, j - 1) && IsValidePosition(i + 2, j - 2) && board[i + 2, j - 2] == Piece.Empty && (board[i + 1, j - 1] == Piece.BlackNormal || board[i + 1, j - 1] == Piece.BlackKing))
                    {
                        Cap.Add(new Move(i, j, i + 2, j - 2));
                    }
                    break;
                case Piece.BlackNormal:
                    if (IsValidePosition(i - 1, j + 1) && IsValidePosition(i - 2, j + 2) && board[i - 2, j + 2] == Piece.Empty && (board[i - 1, j + 1] == Piece.WhiteNormal || board[i - 1, j + 1] == Piece.WhiteKing))
                    {
                        Cap.Add(new Move(i, j, i - 2, j + 2));
                    }
                    if (IsValidePosition(i - 1, j - 1) && IsValidePosition(i - 2, j - 2) && board[i - 2, j - 2] == Piece.Empty && (board[i - 1, j - 1] == Piece.WhiteNormal || board[i - 1, j - 1] == Piece.WhiteKing))
                    {
                        Cap.Add(new Move(i, j, i - 2, j - 2));
                    }
                    break;
                case Piece.WhiteKing:
                    for (int k = 1; k < 8; k++)
                    {
                        if (IsValidePosition(i + k, j + k) && IsValidePosition(i + k + 1, j + k + 1) && board[i + k + 1, j + k + 1] == Piece.Empty && board[i + k + 1, j + k + 1] == Piece.Empty && (board[i + k, j + k] == Piece.BlackNormal || board[i + k, j + k] == Piece.BlackKing))
                        {
                            Cap.Add(new Move(i, j, i + k + 1, j + k + 1));
                        }
                        if (IsValidePosition(i + k, j - k) && IsValidePosition(i + k + 1, j - k - 1) && board[i + k + 1, j - k - 1] == Piece.Empty && (board[i + k, j - k] == Piece.BlackNormal || board[i + k, j - k] == Piece.BlackKing))
                        {
                            Cap.Add(new Move(i, j, i + k + 1, j - k - 1));
                        }
                        if (IsValidePosition(i - k, j + k) && IsValidePosition(i - k - 1, j + k + 1) && board[i - k - 1, j + k + 1] == Piece.Empty && (board[i - k, j + k] == Piece.BlackNormal || board[i - k, j + k] == Piece.BlackKing))
                        {
                            Cap.Add(new Move(i, j, i - k - 1, j + k + 1));
                        }
                        if (IsValidePosition(i - k, j - k) && IsValidePosition(i - k - 1, j - k - 1) && board[i - k - 1, j - k - 1] == Piece.Empty && (board[i - k, j - k] == Piece.BlackNormal || board[i - k, j - k] == Piece.BlackKing))
                        {
                            Cap.Add(new Move(i, j, i - k - 1, j - k - 1));
                        }
                    }
                    break;
                case Piece.BlackKing:
                    for (int k = 1; k < 8; k++)
                    {
                        if (IsValidePosition(i + k, j + k) && IsValidePosition(i + k + 1, j + k + 1) && board[i + k + 1, j + k + 1] == Piece.Empty && (board[i + k, j + k] == Piece.WhiteNormal || board[i + k, j + k] == Piece.WhiteKing))
                        {
                            Cap.Add(new Move(i, j, i + k + 1, j + k + 1));
                        }
                        if (IsValidePosition(i + k, j - k) && IsValidePosition(i + k + 1, j - k - 1) && board[i + k + 1, j - k - 1] == Piece.Empty && (board[i + k, j - k] == Piece.WhiteNormal || board[i + k, j - k] == Piece.WhiteKing))
                        {
                            Cap.Add(new Move(i, j, i + k + 1, j - k - 1));
                        }
                        if (IsValidePosition(i - k, j + k) && IsValidePosition(i - k - 1, j + k + 1) && board[i - k - 1, j + k + 1] == Piece.Empty && (board[i - k, j + k] == Piece.WhiteNormal || board[i - 1, j + 1] == Piece.WhiteKing))
                        {
                            Cap.Add(new Move(i, j, i - k + 1, j + k + 1));
                        }
                        if (IsValidePosition(i - k, j - k) && IsValidePosition(i - k - 1, j - k - 1) && board[i - k - 1, j - k - 1] == Piece.Empty && (board[i - k, j - k] == Piece.WhiteNormal || board[i - 1, j - 1] == Piece.WhiteKing))
                        {
                            Cap.Add(new Move(i, j, i - k - 1, j - k - 1));
                        }
                    }
                    break;
            }
        }
        return Cap;
    }

    public List<Move> GenMovPlayers(bool turn)
    {
        List<Move> MovP = new List<Move>();
        if (turn)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j] == Piece.WhiteNormal || board[i, j] == Piece.WhiteKing)
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
                    if (board[i, j] == Piece.BlackNormal || board[i, j] == Piece.BlackKing)
                    {
                        MovP.AddRange(GenMovPiece(i, j));
                    }
                }
            }
        }
        return MovP;
    }
    public List<Move> GenCapturePlayers(bool turn)
    {
        List<Move> CapP = new List<Move>();
        if (turn)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j] == Piece.WhiteNormal || board[i, j] == Piece.WhiteKing)
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
                    if (board[i, j] == Piece.BlackNormal || board[i, j] == Piece.BlackKing)
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
                if (board[i, j] == Piece.WhiteNormal || board[i, j] == Piece.WhiteKing)
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
                if (board[i, j] == Piece.BlackNormal || board[i, j] == Piece.BlackKing)
                    numB++;

            }

        }
        return numB;
    }
    public bool MovePiece(int fromI, int fromJ, int toI, int toJ)
    {
        Piece typePiece = board[fromI, fromJ]; 
        Piece movingPiece = board[fromI, fromJ];
        board[toI, toJ] = movingPiece;
        board[fromI, fromJ] = Piece.Empty;
        Piece capturedPiece = board[fromI, fromJ]; 
        bool wasKing = false;
        Piece capPiece = Piece.Empty;
        int capturedI=-1, capturedJ=-1;
        bool captured = false;
        if (Math.Abs(fromI - toI) >= 2 && Math.Abs(fromJ - toJ) >= 2)
        {
            int midI = (fromI + toI) / 2;
            int midJ = (fromJ + toJ) / 2;
            capturedPiece = board[midI, midJ];
            capturedI = midI;
            capturedJ = midJ;

            board[midI, midJ] = Piece.Empty;
            Console.WriteLine("Captured!");
        }
        captured = true;


        if (movingPiece == Piece.WhiteNormal && toI == 7)
        {
            board[toI, toJ] = Piece.WhiteKing;
            wasKing = true;
            Console.WriteLine("White piece promoted to King!");
        }


        if (movingPiece == Piece.BlackNormal && toI == 0)
        {
            board[toI, toJ] = Piece.BlackKing;
            wasKing = true;
            Console.WriteLine("Black piece promoted to King!");
        }

        moveHistory.Push(new MoveHistory
        {
            move = new Move(fromI, fromJ, toI, toJ),
            typePiece = typePiece,
            capturePiece = capPiece,
            capturedI = capturedI,
            capturedJ = capturedJ,
            wasKing = wasKing,
        });
        return captured;
    }
    public void UndoMove(bool turn)
    {
        if (!moveHistory.Any())
        {
            Console.WriteLine("No move to undo.");
            return;
        }

        MoveHistory lastMove = moveHistory.Peek();

        if ((turn && (lastMove.typePiece == Piece.WhiteNormal || lastMove.typePiece == Piece.WhiteKing)) ||
            (!turn && (lastMove.typePiece == Piece.BlackNormal || lastMove.typePiece == Piece.BlackKing)))
        {
            lastMove = moveHistory.Pop();
            board[lastMove.move.FromI, lastMove.move.FromJ] = lastMove.typePiece;

            if (lastMove.wasKing)
            {
                if (lastMove.typePiece == Piece.WhiteNormal || lastMove.typePiece == Piece.WhiteKing)
                    board[lastMove.move.FromI, lastMove.move.FromJ] = Piece.WhiteNormal;
                else if (lastMove.typePiece == Piece.BlackNormal || lastMove.typePiece == Piece.BlackKing)
                    board[lastMove.move.FromI, lastMove.move.FromJ] = Piece.BlackNormal;
            }

            board[lastMove.move.ToI, lastMove.move.ToJ] = Piece.Empty;

            if (lastMove.capturePiece != Piece.Empty && lastMove.capturedI >= 0 && lastMove.capturedJ >= 0)
            {
                board[lastMove.capturedI, lastMove.capturedJ] = lastMove.capturePiece;
                Console.WriteLine("Captured piece restored.");
            }



            Console.WriteLine("Move undone.");
        }
        else
        {
            lastMove = moveHistory.Pop();
            board[lastMove.move.FromI, lastMove.move.FromJ] = lastMove.typePiece;

            if (lastMove.wasKing)
            {
                if (lastMove.typePiece == Piece.WhiteNormal || lastMove.typePiece == Piece.WhiteKing)
                    board[lastMove.move.FromI, lastMove.move.FromJ] = Piece.WhiteNormal;
                else if (lastMove.typePiece == Piece.BlackNormal || lastMove.typePiece == Piece.BlackKing)
                    board[lastMove.move.FromI, lastMove.move.FromJ] = Piece.BlackNormal;
            }

            board[lastMove.move.ToI, lastMove.move.ToJ] = Piece.Empty;

            if (lastMove.capturePiece != Piece.Empty && lastMove.capturedI >= 0 && lastMove.capturedJ >= 0)
            {
                board[lastMove.capturedI, lastMove.capturedJ] = lastMove.capturePiece;
                Console.WriteLine("Captured piece restored.");
                lastMove = moveHistory.Pop();
                board[lastMove.move.FromI, lastMove.move.FromJ] = lastMove.typePiece;

                if (lastMove.wasKing)
                {
                    if (lastMove.typePiece == Piece.WhiteNormal || lastMove.typePiece == Piece.WhiteKing)
                        board[lastMove.move.FromI, lastMove.move.FromJ] = Piece.WhiteNormal;
                    else if (lastMove.typePiece == Piece.BlackNormal || lastMove.typePiece == Piece.BlackKing)
                        board[lastMove.move.FromI, lastMove.move.FromJ] = Piece.BlackNormal;
                }

                board[lastMove.move.ToI, lastMove.move.ToJ] = Piece.Empty;

                if (lastMove.capturePiece != Piece.Empty && lastMove.capturedI >= 0 && lastMove.capturedJ >= 0)
                {
                    board[lastMove.capturedI, lastMove.capturedJ] = lastMove.capturePiece;
                    Console.WriteLine("Captured piece restored.");
                    //Console.WriteLine("You can only undo your last move.");
                }
                lastMove = moveHistory.Pop();
                board[lastMove.move.FromI, lastMove.move.FromJ] = lastMove.typePiece;

                if (lastMove.wasKing)
                {
                    if (lastMove.typePiece == Piece.WhiteNormal || lastMove.typePiece == Piece.WhiteKing)
                        board[lastMove.move.FromI, lastMove.move.FromJ] = Piece.WhiteNormal;
                    else if (lastMove.typePiece == Piece.BlackNormal || lastMove.typePiece == Piece.BlackKing)
                        board[lastMove.move.FromI, lastMove.move.FromJ] = Piece.BlackNormal;
                }

                board[lastMove.move.ToI, lastMove.move.ToJ] = Piece.Empty;

                if (lastMove.capturePiece != Piece.Empty && lastMove.capturedI >= 0 && lastMove.capturedJ >= 0)
                {
                    board[lastMove.capturedI, lastMove.capturedJ] = lastMove.capturePiece;
                    Console.WriteLine("Captured piece restored.");
                    lastMove = moveHistory.Pop();
                    board[lastMove.move.FromI, lastMove.move.FromJ] = lastMove.typePiece;

                    if (lastMove.wasKing)
                    {
                        if (lastMove.typePiece == Piece.WhiteNormal || lastMove.typePiece == Piece.WhiteKing)
                            board[lastMove.move.FromI, lastMove.move.FromJ] = Piece.WhiteNormal;
                        else if (lastMove.typePiece == Piece.BlackNormal || lastMove.typePiece == Piece.BlackKing)
                            board[lastMove.move.FromI, lastMove.move.FromJ] = Piece.BlackNormal;
                    }

                    board[lastMove.move.ToI, lastMove.move.ToJ] = Piece.Empty;

                    if (lastMove.capturePiece != Piece.Empty && lastMove.capturedI >= 0 && lastMove.capturedJ >= 0)
                    {
                        board[lastMove.capturedI, lastMove.capturedJ] = lastMove.capturePiece;
                        Console.WriteLine("Captured piece restored.");
                        
                    }
                    Console.WriteLine("Move undone.");

                }
            }
        }

    }
        static void Main(string[] args)
        {
            Checkers checkers = new Checkers();
            checkers.InitializeBoard();
            bool turn = true;

            while (checkers.NumBlackPiece() > 0 && checkers.NumWhitePiece() > 0)
            {
                checkers.PrintBoard();
                Console.WriteLine(turn ? "White's Turn (WN/WK)" : "Black's Turn (BN/BK)");

                List<Move> allMov = new List<Move>();
                List<Move> allCap = new List<Move>();
                allCap = checkers.GenCapturePlayers(turn);

                if (allCap.Count == 0)
                {
                    allMov = checkers.GenMovPlayers(turn);

                }


                if (allMov.Count == 0 && allCap.Count == 0)
                {
                    Console.WriteLine(turn ? "Black Wins!" : "White Wins!");
                    break;
                }


                int fromI, fromJ, ToI, ToJ;
                List<Move> possibleMoves = new List<Move>();



                while (true)
                {
                    if (allCap.Count > 0)
                    {
                        Console.WriteLine("Capture moves available:");
                        foreach (var cap in allCap)
                        {
                            Console.WriteLine($"From ({cap.FromI},{cap.FromJ}) To ({cap.ToI},{cap.ToJ})");
                        }
                        possibleMoves = allCap;

                    }
                    else if (allMov.Count > 0)
                    {
                        Console.WriteLine("Available moves:");
                        foreach (var move in allMov)
                        {
                            Console.WriteLine($"From ({move.FromI},{move.FromJ}) To ({move.ToI},{move.ToJ})");
                        }
                        possibleMoves = allMov;

                    }
                    Console.WriteLine("To go back a step, enter undo");
                    string undo = Console.ReadLine().ToUpper();
                    if (undo == "UNDO")
                    {
                        checkers.UndoMove(turn);

                    }
                    Console.WriteLine("enter moves");

                    fromI = int.Parse(Console.ReadLine());
                    fromJ = int.Parse(Console.ReadLine());
                    ToI = int.Parse(Console.ReadLine());
                    ToJ = int.Parse(Console.ReadLine());
                    if (possibleMoves.Any(m => m.FromI == fromI && m.FromJ == fromJ && m.ToI == ToI && m.ToJ == ToJ))
                    {
                        bool captured = checkers.MovePiece(fromI, fromJ, ToI, ToJ);
                        if (captured)
                        {
                            int newI = ToI;
                            int newJ = ToJ;
                            while (true)
                            {
                                List<Move> NewCap = new List<Move>();
                                NewCap = checkers.GenCapturePiece(newI, newJ);
                                if (NewCap.Count == 0)
                                {
                                    break;
                                }
                                checkers.PrintBoard();
                                Console.WriteLine("Additional capture available");
                                foreach (var cap in NewCap)
                                {
                                    Console.WriteLine($"From ({cap.FromI},{cap.FromJ}) To ({cap.ToI},{cap.ToJ})");

                                }
                                Console.WriteLine("To continue, type ok, otherwise type no.");
                                string cont = Console.ReadLine();
                                if (cont == "ok")
                                {
                                    fromI = int.Parse(Console.ReadLine());
                                    fromJ = int.Parse(Console.ReadLine());
                                    ToI = int.Parse(Console.ReadLine());
                                    ToJ = int.Parse(Console.ReadLine());
                                    if (NewCap.Any(m => m.FromI == fromI && m.FromJ == fromJ && m.ToI == ToI && m.ToJ == ToJ))
                                    {
                                        captured = checkers.MovePiece(fromI, fromJ, ToI, ToJ);

                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid move");
                                }

                            }
                        }
                        turn = !turn;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid move. Try again.");
                    }



                }
                Console.Clear();




            }
        }
    }
    

