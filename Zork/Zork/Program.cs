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
        private static string[] Rooms = new string[] { "Forest", "West of House", "Behind House", "Clearing", "Canyon View" };
        private static int playerLocation = 0;

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
                        Console.WriteLine(Rooms[playerLocation]);
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
            int movedir = 0;
            switch(commands)
            {
                case Commands.NORTH:
                    break;

                case Commands.SOUTH:
                    break;

                case Commands.EAST:
                    movedir = 1;
                    break;

                case Commands.WEST:
                    movedir = -1;
                    break;
            }

            if ((playerLocation + movedir) >= 0 && (playerLocation + movedir) < Rooms.Length)
            {
                playerLocation += movedir;
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
