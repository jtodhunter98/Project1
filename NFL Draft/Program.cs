using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFL_Draft
{
    class Program
    {
        static void Main()
        {
            string[,] players =
            {
                {"Mason Rudolph","Lamar Jackson","Josh Rosen","Sam Darnold","Baker Mayfield" },
                {"Saquon Barkley","Derrius Guice","Bryce Love","Ronald Jones II","Damien Harris" },
                {"Courtland Sutton","James Washington","Marcell Ateman","Anthony Miller","Calvin Ridley" },
                {"Maurice Hurst","Vita Vea","Taven Bryan","Da'Ron Payne","Harrison Phillips" },
                {"Joshua Jackson","Derwin James","Denzel Ward","Minkah Fitzpatrick","Isaiah Oliver" },
                {"Mark Andrews","Dallas Goedert","Jaylen Samuels","Mike Gesicki","Troy Fumagalli" },
                {"Roquan Smith","Tremaine Edmunds","Kendall Joseph","Dorian O'Daniel","Malik Jefferson" },
                {"Orlando Brown","Kolton Miller","Chukwuma Okorafor","Connor Williams","Mike McGlinchey" }
            };

            string[] position = { "Quarterback", "Running Back", "Wide Receiver", "Defensive Lineman", "Defensive Back", "Tight End", "Line Backer", "Offensive Tackle" };

            int[,] salary =
            {
                {26400100, 20300100, 17420300, 13100145, 10300000 },
                {24500100, 19890200, 18700800, 15000000, 11600400 },
                {23400000, 21900300, 19300230, 13400230, 10000000 },
                {26200300, 22000000, 16000000, 18000000, 13000000 },
                {24000000, 22500249, 20000100, 16000200, 11899999 },
                {27800900, 21000800, 17499233, 27900200, 14900333 },
                {22900300, 19000590, 18000222, 12999999, 10000100 },
                {23000000, 20000000, 19400000, 16200700, 15900000 }
            };

            int draftPosition, draftPlayer, bank, numberOfPlayers, rosterVar;//user input position, player, and user's amount of money
            bool sentinel;//value to determine when the program ends

            bank = 95000000;//starting money
            numberOfPlayers = 1;//counter to limit how many players the user can draft
            rosterVar = 0;//counter to add drafted players to the roster (list)
            string[] roster = new string[5];

            sentinel = true;

            while (sentinel)
            {
                restoreOne://restore point if user does not successfully select position (1-8)

                renderPlayers(ref players, ref position, ref salary);
                Console.WriteLine();
                draftInfo(ref numberOfPlayers, ref bank);
                draftPosition = userInputPosition();//user selects position to draft for
                Console.Clear();

                if (draftPosition > 8)
                {
                    Console.WriteLine("That is not an option, please enter a number 1-8.");
                    Console.WriteLine("Press enter to try again.");
                    Console.ReadLine();
                    goto restoreOne;
                }

                restoreTwo://restore point if user does not successfully select player (1-5)

                renderSelectedPosition(ref players, ref position, ref salary, ref draftPosition);//player selection screen
                draftPlayer = userInputPlayer(ref bank);
                Console.Clear();
                updateBank(ref bank, ref salary, ref draftPosition, ref draftPlayer);
                updatePlayerLimit(ref numberOfPlayers);
                addToRoster(ref roster, ref rosterVar, ref players, ref draftPosition, ref draftPlayer);

                if (bank < 0)//check if user has sufficient funds
                {
                    Console.WriteLine("You do not have enough money to draft this player.");
                    Console.WriteLine("Press enter to draft a different player.");
                    Console.ReadLine();
                    goto restoreTwo;                    
                }
                if (numberOfPlayers > 5)// check for maximum amount of players
                {
                    Console.WriteLine("You have drafted the maximum amount of players.");
                    Console.WriteLine("Press enter to view your roster.");
                    Console.ReadLine();
                    break;
                }

                confirmDraft(ref players, ref draftPosition, ref draftPlayer, ref bank);

                

                string decision = nextDraft();
                if (decision == "Y")
                {
                    sentinel = true;
                }
                else
                {
                    sentinel = false;
                }
            }//end of while loop

            Console.Clear();
            rosterInfo(ref roster, ref bank);
        }//end of Main
        
        //Defining renderPlayers
        public static void renderPlayers(ref string[,] players, ref string[] position, ref int[,] salary)
        {
            Console.WriteLine("Welcome to the NFL Draft!");
            Console.WriteLine();


            string spacing = " ";
            int spacingVar = 20;
            

            for (var x = 0; x < players.GetLength(0); x++)//loop to display players
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.Write($"{position[x]}" + " (" + (x + 1) + ")");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine();

                for (var y = 0; y < players.GetLength(1); y++)
                {
                    string specificPlayer = players[x, y];
                    int adjustedVar = spacingVar - specificPlayer.Length;
                    string separate = String.Concat(Enumerable.Repeat(spacing, adjustedVar));
                    Console.Write($" {players[x, y]}{separate} ");
                }
                Console.WriteLine();
                for (var z = 0; z < salary.GetLength(1); z++)
                {
                    Console.Write($" ${salary[x, z]}            ");                    
                }
                Console.WriteLine();
                Console.WriteLine();
            }//end of display loop
        }//end of renderPlayers

        //defining draftInfo
        public static void draftInfo(ref int numberOfPlayers, ref int bank)
        {
            Console.WriteLine("You may draft up to five players. (" + (6 - numberOfPlayers) + " picks left)");
            Console.WriteLine("Bank: $" + (bank));
        }//end of draftInfo

        //defining userInputPosition
        public static int userInputPosition()
        {
            Console.WriteLine("Enter the number of the position you want to draft for.");
            return Int32.Parse(Console.ReadLine());

        }//end of userInputPosition

        //defining renderSelectedPosition
        public static void renderSelectedPosition(ref string[,] players, ref string[] position, ref int[,] salary, ref int draftPosition)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine("You are drafting for a " + position[draftPosition - 1]);
            Console.BackgroundColor = ConsoleColor.Black;

            for (var x = 0; x < 5; x++)//display players of the selected position
            {
                Console.WriteLine($" {x + 1}) {players[draftPosition - 1, x]}  ${salary[draftPosition - 1, x]}");
            }//end of loop
        }//end of renderSelectedPosition

        //defining userInputPlayer
        public static int userInputPlayer(ref int bank)
        {
            Console.WriteLine();
            Console.WriteLine("Bank: $" + bank);
            Console.WriteLine("Enter the number of the player you want to draft");
            return Int32.Parse(Console.ReadLine());
        }//end of userInputPlayer

        //defining updateBank
        public static void updateBank(ref int bank, ref int[,] salary, ref int draftPosition, ref int draftPlayer)
        {
            bank = bank - salary[draftPosition - 1, draftPlayer - 1];
        }//end of updateBank

        //defining updatePlayerLimit
        public static void updatePlayerLimit(ref int numberOfPlayers)
        {
            numberOfPlayers = numberOfPlayers + 1;
        }//end of updatePlayerLimit

        //defining addToRoster
        public static void addToRoster(ref string[] roster, ref int rosterVar, ref string[,] players, ref int draftPosition, ref int draftPlayer)
        {
            roster[rosterVar] = players[draftPosition - 1, draftPlayer - 1];
            rosterVar = rosterVar + 1;
        }//end of addToRoster

        //defining confirmDraft
        public static void confirmDraft(ref string[,] players, ref int draftPosition, ref int draftPlayer, ref int bank)
        {
            Console.WriteLine("You have drafted " + (players[draftPosition - 1, draftPlayer - 1]));
            Console.WriteLine("You now have $" + (bank) + " left to spend");
        }//end of confirmDraft

        //defining nextDraft
        public static string nextDraft()
        {
            Console.WriteLine("Would you like to draft another player? (Y/N)");
            return Console.ReadLine().ToUpper();
        }//end of nextDraft
        
        //defining endInfo
        public static void rosterInfo(ref string [] roster, ref int bank)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine("Roster");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine();
            for (var x = 0; x < roster.GetLength(0); x++)
            {
                Console.WriteLine($"{roster[x]}");
            }
            Console.WriteLine();
            Console.WriteLine("Money spent: $" + (95000000 - bank));
            Console.WriteLine();
            Console.WriteLine("(Press enter to close program)");
            Console.ReadLine();
        }
        

        

        
    }
}
