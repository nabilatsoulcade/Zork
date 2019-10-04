using System;
using System.Collections.Generic;
using System.Diagnostics;

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
        
        private static Room CurrentRoom
        {
            get
            {
                return Rooms[Location.Row, Location.Column];
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!");
            InializeRoomDescriptions();

            Room previousRoom = null;
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
                        outputString = CurrentRoom.Description;
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
                        Console.WriteLine(CurrentRoom.Name);
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
            Assert.IsTrue(IsDirection(commands), "Invalid direction.");

            bool isValidMove = true;
            switch(commands)
            {
                case Commands.SOUTH when Location.Row < Rooms.GetLength(0) - 1:
                    Location.Row++;
                    break;

                case Commands.NORTH when Location.Row > 0:
                    Location.Row--;
                    break;

                case Commands.WEST when Location.Column < Rooms.GetLength(1) - 1:
                    Location.Column++;
                    break;

                case Commands.EAST when Location.Column > 0:
                    Location.Column--;
                    break;

                default:
                    isValidMove = false;
                    break;
            }

            return isValidMove;
        }
        private static Commands ToCommand(string commandString) =>
            Enum.TryParse(commandString, true, out Commands result) ? result : Commands.UNKNOWN;

        private static bool IsDirection(Commands command) => Directions.Contains(command);

        private static readonly Room[,] Rooms =
        {
            {new Room("Rocky Trail"), new Room("South of House"), new Room("Canyon View")},
            {new Room ("Forest"), new Room("West of House"), new Room("Behind House")},
            {new Room ("Dense Woods"), new Room ("North of House"), new Room ("Clearing")}
        };
        private static void InializeRoomDescriptions()
        {
            var roomMap = new Dictionary<string, Room>();
            foreach(Room room in Rooms)
            {
                roomMap[room.Name] = room;
            }
            roomMap["Rocky Trail"].Description = "You are on a rock-stewn trail.";                                                                             // Rocky Trail
            roomMap["South of House"].Description = "You are facing the south side of a white house. There is no door here, and all the windows are barred.";     // South of House
            roomMap["Canyon View"].Description = "You are at the top of the Great Canyon on its south wall.";                                                  // Canyon View

            roomMap["Forest"].Description = "This is a forest, with trees in all directions around you.";                                                 // Forest
            roomMap["West of House"].Description = "This is an open field west of a white house, with a boarded front door.";                                    // West of House
            roomMap["Behind House"].Description = "You are behind the white house. In one corner of the house there is a small window which is slightly ajar."; // Behind House

            roomMap["Dense Woods"].Description = "This is a dimly lit forest, with large trees all around. To the east, there appears to be sunlight.";        // Dense Woods
            roomMap["North of House"].Description = "You are facing north side of a white house. There is no door here, and all the windows are barred.";         // North of House
            roomMap["Clearing"].Description = "You are in a clearing, with a forest surronding you on the west and south.";                                 // Clearing
        }
        private static readonly List<Commands> Directions = new List<Commands>
        {
            Commands.NORTH,
            Commands.SOUTH,
            Commands.EAST,
            Commands.WEST
        };
        
        private static (int Row, int Column) Location;
    }

    public static class Assert
    {
        [Conditional("DEBUG")]
        public static void IsTrue(bool expression, string message = null)
        {
            if (expression == false)
            {
                throw new Exception(message);
            }
        }
    }
}
