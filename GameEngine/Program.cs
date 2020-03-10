using System;
using System.Collections.Generic;

namespace GameEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            var player1 = new GamePlayer() { Id = Guid.NewGuid(), Name = "Player 1" };
            var player2 = new GamePlayer() { Id = Guid.NewGuid(), Name = "Player 2" };

            List<GamePlayer> players = new List<GamePlayer> { player1, player2 };

            List<GameAction> actions = new List<GameAction>
            {
                new GameAction(player1, Command.Down),
                new GameAction(player2, Command.Up),
                new GameAction(player1, Command.Right),
                new GameAction(player2, Command.Left),
                new GameAction(player1, Command.Up),
                new GameAction(player2, Command.Down),
                new GameAction(player1, Command.Left),
                new GameAction(player2, Command.Right),
            };

            var gameGrid = new[,]
            {
                { CellType.Bot1, CellType.Free, CellType.Free, CellType.Free,CellType.Free, CellType.Free, CellType.Free, CellType.Free, CellType.Free,CellType.Free },
                { CellType.Free, CellType.Free, CellType.Free, CellType.Free,CellType.Free, CellType.Free, CellType.Free, CellType.Free, CellType.Free,CellType.Free },
                { CellType.Free, CellType.Free, CellType.Free, CellType.Free,CellType.Free, CellType.Free, CellType.Free, CellType.Free, CellType.Free,CellType.Free },
                { CellType.Free, CellType.Free, CellType.Free, CellType.Free,CellType.Free, CellType.Free, CellType.Free, CellType.Free, CellType.Free,CellType.Free },
                { CellType.Free, CellType.Free, CellType.Free, CellType.Free,CellType.Free, CellType.Free, CellType.Free, CellType.Free, CellType.Free,CellType.Free },
                { CellType.Free, CellType.Free, CellType.Free, CellType.Free,CellType.Free, CellType.Free, CellType.Free, CellType.Free, CellType.Free,CellType.Free },
                { CellType.Free, CellType.Free, CellType.Free, CellType.Free,CellType.Free, CellType.Free, CellType.Free, CellType.Free, CellType.Free,CellType.Free },
                { CellType.Free, CellType.Free, CellType.Free, CellType.Free,CellType.Free, CellType.Free, CellType.Free, CellType.Free, CellType.Free,CellType.Free },
                { CellType.Free, CellType.Free, CellType.Free, CellType.Free,CellType.Free, CellType.Free, CellType.Free, CellType.Free, CellType.Free,CellType.Free },
                { CellType.Free, CellType.Free, CellType.Free, CellType.Free,CellType.Free, CellType.Free, CellType.Free, CellType.Free, CellType.Free,CellType.Bot2 },
            };

            GameEngine engine = new GameEngine(player1, player2, gameGrid);

            Console.WriteLine("--Initial Board State--");
            Console.WriteLine(engine.State.Grid.To2dChars().ToText());

            foreach (GameAction action in actions)
            {
                bool result = engine.Update(action);
                Console.WriteLine($"{action.Player.Name} does {action.Command}");
                Console.WriteLine(engine.State.Grid.To2dChars().ToText());
            }
        }
    }
}
