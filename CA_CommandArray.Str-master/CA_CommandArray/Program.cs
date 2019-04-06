using FinchAPI;
using System;
using System.Collections.Generic;

namespace CommandArray
{
    // *************************************************************
    // CommandArray 
    // Payton Bramer
    // April 3, 2019
    // *************************************************************

    /// <summary>
    /// control commands for the finch robot
    /// </summary>
    public enum FinchCommand
    {
        DONE,
        MOVEFORWARD,
        MOVEBACKWARD,
        STOPMOTORS,
        DELAY,
        TURNRIGHT,
        TURNLEFT,
        LEDON,
        LEDOFF
    }

    class Program
    {
        static void Main(string[] args)
        {
            Finch myFinch = new Finch();

            DisplayOpeningScreen();
            DisplayInitializeFinch(myFinch);
            DisplayMainMenu(myFinch);
            DisplayClosingScreen(myFinch);
        }

        /// <summary>
        /// display the main menu
        /// </summary>
        /// <param name="myFinch">Finch object</param>
        static void DisplayMainMenu(Finch myFinch)
        {
            string menuChoice;
            bool exiting = false;

            int delayDuration=0;
            int numberOfCommands=0;
            int motorSpeed=0;
            int LEDBrightness=0;
            //FinchCommand[] commands = null;
            List<FinchCommand> commands = new List<FinchCommand>();

            while (!exiting)
            {
                //
                // display menu
                //
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("Main Menu");
                Console.WriteLine();

                Console.WriteLine("\t1) Get Command Parameters");
                Console.WriteLine("\t2) Get Finch Robot Commands");
                Console.WriteLine("\t3) Display Finch Robot Commands");
                Console.WriteLine("\t4) Execute Finch Robot Commands");
                Console.WriteLine("\tE) Exit");
                Console.WriteLine();
                Console.Write("Enter Choice:");
                menuChoice = Console.ReadLine();

                //
                // process menu
                //
                switch (menuChoice)
                {
                    case "1":
                        numberOfCommands = DisplayGetNumberOfCommands();
                        delayDuration = DisplayGetDelayDuration();
                        motorSpeed = DisplayGetMotorSpeed();
                        LEDBrightness = DisplayGetLEDBrightness();
                        break;
                    case "2":
                        DisplayGetFinchCommands(commands,numberOfCommands);
                        break;
                    case "3":
                        DisplayFinchCommands(commands);
                        break;
                    case "4":
                        DisplayExecuteFinchCommands(myFinch,commands,motorSpeed,LEDBrightness,delayDuration);
                        break;
                    case "e":
                    case "E":
                        exiting = true;
                        break;
                    default:
                        break;
                }
            }
        }

        static void DisplayExecuteFinchCommands(Finch myFinch, List<FinchCommand> commands, int motorSpeed, int lEDBrightness, int delayDuration)
        {
            commands = null;
           
            DisplayHeader("Execute Finch Commands");
            if (commands !=null)
            {            
                Console.WriteLine("Click any key when ready to execute commands");
                DisplayContinuePrompt();

                for (int index = 0; index < commands.Count; index++)
                {
                    Console.WriteLine($"Command:{commands[index]}");

                    switch (commands[index])
                    {
                        case FinchCommand.DONE:
                            break;
                        case FinchCommand.MOVEFORWARD:
                            myFinch.setMotors(motorSpeed, motorSpeed);
                            break;
                        case FinchCommand.MOVEBACKWARD:
                            myFinch.setMotors(-motorSpeed, -motorSpeed);
                            break;
                        case FinchCommand.STOPMOTORS:
                            myFinch.setMotors(0, 0);
                            break;
                        case FinchCommand.DELAY:
                            myFinch.wait(delayDuration);
                            break;
                        case FinchCommand.TURNRIGHT:
                            myFinch.setMotors(motorSpeed, -motorSpeed);
                            break;
                        case FinchCommand.TURNLEFT:
                            myFinch.setMotors(-motorSpeed, motorSpeed);
                            break;
                        case FinchCommand.LEDON:
                            myFinch.setLED(lEDBrightness, lEDBrightness, lEDBrightness);
                            break;
                        case FinchCommand.LEDOFF:
                            myFinch.setLED(0, 0, 0);
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Please enter Finch Robot commands first.");
            }

            DisplayContinuePrompt();
        }

        static void DisplayGetFinchCommands(List<FinchCommand>commands, int numberofCommands)
        {
            FinchCommand command;
            commands = null;

            DisplayHeader("Get Finch commands");
            if (commands != null)
            {
                for (int index = 0; index < numberofCommands; index++)
                {
                    Console.WriteLine($"Command #{index + 1}:");
                    Enum.TryParse(Console.ReadLine().ToUpper(), out command);
                    commands.Add(command);
                }

                Console.WriteLine();
                Console.WriteLine("The commands:");
                foreach (FinchCommand finchCommand in commands)
                {
                    Console.WriteLine("\t" + finchCommand);
                }
            }
            else
            {
                Console.WriteLine("Please enter Finch Robot commands first.");
            }

            DisplayContinuePrompt();

           
        }
        static void DisplayFinchCommands(List<FinchCommand> commands)
        {
            commands = null;

            DisplayHeader("Display Finch commands");

            if (commands !=null)
            {
                Console.WriteLine("The commands:");
                foreach (FinchCommand command in commands)
                {
                    Console.WriteLine(command);
                }
            }
            else
            {
                Console.WriteLine("Please enter Finch Robot commands first.");
            }
            DisplayContinuePrompt();
            
        }

        static int DisplayGetDelayDuration()
        {
            int delayDuration;
            string userResponse = null;

            DisplayHeader("Length of Delay");
            while (!int.TryParse(userResponse, out delayDuration))
            {
                Console.WriteLine();
                Console.WriteLine("Enter Length of delay (milliseconds):");
                userResponse = Console.ReadLine();
            }            
            //delayDuration = int.Parse(userResponse);
            //delayDuration = Convert.ToInt32(userResponse);            
            //int.TryParse(Console.ReadLine(),out delayDuration);

            DisplayContinuePrompt();
            return delayDuration;
        }

        /// <summary>
        /// get the number of commands from the user
        /// </summary>
        /// <returns>number of commands</returns>
        static int DisplayGetNumberOfCommands()
        {
            int numberOfCommands;
            string userResponse = null;

            DisplayHeader("Number of Commands");
            while (!int.TryParse(userResponse, out numberOfCommands))
            {                
                Console.Clear();                
                Console.Write("Enter the number of commands:");
                userResponse = Console.ReadLine();
            }      

            return numberOfCommands;
        }
        //
        //Get Motor Speed from user
        //
        static int DisplayGetMotorSpeed()
        {
            int motorSpeed;
            string userResponse = null;

            DisplayHeader("Motor Speed");            
            while (!int.TryParse(userResponse,out motorSpeed))
            {
                Console.Write("Enter the Motor speed [1-255]:");
                userResponse = Console.ReadLine();
            }          

            return motorSpeed;
        }
        //
        //Get LED Brightness
        //
        static int DisplayGetLEDBrightness()
        {
            int LEDBrightness;
            string userResponse = null;

            DisplayHeader("LED Brightness");
            while (!int.TryParse(userResponse,out LEDBrightness))
            {
                Console.Write("Enter the LED Brightness[1-255]:");
                userResponse = Console.ReadLine();
            }           

            return LEDBrightness;
        }

        /// <summary>
        /// initialize and confirm the finch connects
        /// </summary>
        /// <param name="myFinch"></param>
        static void DisplayInitializeFinch(Finch myFinch)
        {
            DisplayHeader("Initialize the Finch");

            Console.WriteLine("Please plug your Finch Robot into the computer.");
            Console.WriteLine();
            DisplayContinuePrompt();

            while (!myFinch.connect())
            {
                Console.WriteLine("Please confirm the Finch Robot is connect");
                DisplayContinuePrompt();
            }

            FinchConnectedAlert(myFinch);
            Console.WriteLine("Your Finch Robot is now connected");

            DisplayContinuePrompt();
        }

        /// <summary>
        /// audio notification that the finch is connected
        /// </summary>
        /// <param name="myFinch">Finch object</param>
        static void FinchConnectedAlert(Finch myFinch)
        {
            myFinch.setLED(0, 255, 0);

            for (int frequency = 17000; frequency > 100; frequency -= 100)
            {
                myFinch.noteOn(frequency);
                myFinch.wait(10);
            }

            myFinch.noteOff();
        }

        /// <summary>
        /// display opening screen
        /// </summary>
        static void DisplayOpeningScreen()
        {
            Console.WriteLine();
            Console.WriteLine("\tProgram Your Finch");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display closing screen and disconnect finch robot
        /// </summary>
        /// <param name="myFinch">Finch object</param>
        static void DisplayClosingScreen(Finch myFinch)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("\t\tThank You!");
            Console.WriteLine();

            myFinch.disConnect();

            DisplayContinuePrompt();
        }

        #region HELPER  METHODS

        /// <summary>
        /// display header
        /// </summary>
        /// <param name="header"></param>
        static void DisplayHeader(string header)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + header);
            Console.WriteLine();
        }

        /// <summary>
        /// display the continue prompt
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        #endregion
    }
}
