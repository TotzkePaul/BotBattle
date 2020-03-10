using System;
using System.Collections.Generic;
using GameEngine;

namespace GameEngine
{
    public class Game
    {
    }
    public class GamePlayer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
    public class GameAction
    {
        public GamePlayer Player { get; }
        public Command Command { get; }
        public GameAction(GamePlayer player, Command command)
        {
            Player = player;
            Command = command;
        }
    }

    public enum Command
    {
        Up,
        Down,
        Left,
        Right,
        Attack
    }

    public enum CellType
    {
        Coin = '$',
        Free = 'O',
        Bot1 = '1',
        Bot2 = '2',
        Barrier = 'X'
    }

    public class GameCell
    {
        public GameCell(CellType value)
        {
            Value = value;
        }
        public CellType Value { get; set; }
    }

    public class GameState
    {
        public GamePlayer ActivePlayer { get; set; }
        public GamePlayer InactivePlayer { get; set; }
        public GameCell[,] Grid { get; set; }
        public GameState(GamePlayer active, GamePlayer inactive, CellType[,] grid)
        {
            Grid = grid.ToGameCells();
            ActivePlayer = active;
            InactivePlayer = inactive;
        }
    }
    public class GameEngine
    {
        public GamePlayer Player1 { get; set; }
        public GamePlayer Player2 { get; set; }
        public GameState State { get; set; }

        public GameEngine(GamePlayer player1, GamePlayer player2, CellType[,] grid)
        {
            Player1 = player1;
            Player2 = player2;
            State = new GameState(player1, player2, grid);
        }

        public List<Command> AvailableCommands()
        {
            CellType activeBot = (State.ActivePlayer == Player1) ? CellType.Bot1 : CellType.Bot2;
            var current = State.Grid.CoordinatesOf(activeBot);
            Tuple<int, int> up = Tuple.Create(-1, 0);
            Tuple<int, int> down = Tuple.Create(+1, 0);
            Tuple<int, int> left = Tuple.Create(0, -1);
            Tuple<int, int> right = Tuple.Create(0, +1);

            var validCommandList = new List<Command>();

            if (IsCommandValid(current, up))
            {
                validCommandList.Add(Command.Up);
            }

            return validCommandList;
        }

        public bool IsCommandValid(Tuple<int, int> current, Tuple<int, int> diff)
        {
            var target = Tuple.Create(current.Item1 + diff.Item1, current.Item2 + diff.Item2);

            bool isValidRow = 0 <= target.Item1 && target.Item1 <= State.Grid.GetUpperBound(0);
            bool isValidCol = 0 <= target.Item2 && target.Item2 <= State.Grid.GetUpperBound(1);

            return isValidRow && isValidCol;
        }

        public bool Update(GameAction action)
        {
            CellType activeBot = (action.Player == Player1) ? CellType.Bot1 : CellType.Bot2;
            var myBotLocation = State.Grid.CoordinatesOf(activeBot);

            if (myBotLocation != null)
            {
                switch (action.Command)
                {
                    case Command.Up:
                        State.Grid[myBotLocation.Item1, myBotLocation.Item2].Value = CellType.Free;
                        State.Grid[myBotLocation.Item1 - 1, myBotLocation.Item2].Value = activeBot;
                        break;
                    case Command.Down:
                        State.Grid[myBotLocation.Item1, myBotLocation.Item2].Value = CellType.Free;
                        State.Grid[myBotLocation.Item1 + 1, myBotLocation.Item2].Value = activeBot;
                        break;
                    case Command.Left:
                        State.Grid[myBotLocation.Item1, myBotLocation.Item2].Value = CellType.Free;
                        State.Grid[myBotLocation.Item1, myBotLocation.Item2 - 1].Value = activeBot;
                        break;
                    case Command.Right:
                        State.Grid[myBotLocation.Item1, myBotLocation.Item2].Value = CellType.Free;
                        State.Grid[myBotLocation.Item1, myBotLocation.Item2 + 1].Value = activeBot;
                        break;
                }
            }


            return true;
        }
    }
}
