using System;

namespace Zork
{
    enum Commands
    {
        QUIT,
        LOOK,
        NORTH,
        SOUTH,
        EAST,
        WEST,
        UNKNOWN
    }
    class Program
    {
        private static readonly string[,] Rooms =
        {
            {"Rocky Trail", "South of House", "Canyon View"},
            {"Forest","West of House","Behind House"},
            {"Dense Woods","North of House","Clearing"}
        };

        private static (int Row, int Column) Location;

        static void Main(string[] args)
        {
            
            Console.WriteLine("Welcome to Zork!");

            Commands command = Commands.UNKNOWN;
            while (command != Commands.QUIT)
            {
                Console.Write("> ");
                command = ToCommand(Console.ReadLine().Trim());

                string outputString;
                switch (command)
                {
                    case Commands.QUIT:
                        outputString = "Thank you for playing!";
                        break;

                    case Commands.LOOK:
                        outputString = "This is an open field west of a white house, with a boarded front door. \nA rubber mat saying 'Welcome to Zork!' lies by the door";
                        break;

                    case Commands.NORTH:
                    case Commands.SOUTH:
                    case Commands.EAST:
                    case Commands.WEST:
                        bool canMove = Move(command);
                        if (canMove)
                        {
                            outputString = $"You moved {command}";
                        }
                        else
                        {
                            outputString = "The way is shut";
                        }
                        Console.WriteLine(Rooms[Location.Row, Location.Column]);
                        break;

                    default:
                        outputString = "Unknown command";
                        break;
                }

                Console.WriteLine(outputString);
            }
        }
        private static bool Move(Commands commands)
        {
            (int Horizontal, int Vertical) Movement;
            Movement = (0, 0);
            switch(commands)
            {
                case Commands.NORTH:
                    Movement.Horizontal = 0;
                    Movement.Vertical = -1;
                    break;

                case Commands.SOUTH:
                    Movement.Horizontal = 0;
                    Movement.Vertical = 1;
                    break;

                case Commands.EAST:
                   Movement.Horizontal = 1;
                   Movement.Vertical = 0;
                    break;

                case Commands.WEST:
                    Movement.Horizontal = -1;
                    Movement.Vertical = 0;
                    break;
            }

            if (((Location.Row + Movement.Vertical) >= 0 && (Location.Row + Movement.Vertical) < Rooms.GetLength(0))
            && ((Location.Column + Movement.Horizontal) >= 0 && (Location.Column + Movement.Horizontal) < Rooms.GetLength(1)))
            {
                Location.Row += Movement.Vertical;
                Location.Column += Movement.Horizontal;
                return true;
            }

            else
            {
                return false;
            }
        }
        private static Commands ToCommand(string commandString)
        {
            //Interpets Input and checks for an associated command
            if (Enum.TryParse<Commands>(commandString, true, out Commands result))
            {
                return result;
            }
            else
            {
                return Commands.UNKNOWN;
            }
        }
    }
}
