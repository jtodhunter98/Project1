using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFL_Draft
{
    class Program
    {
        static void Main(string[] args)
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

            int draftPosition, draftPlayer, bank, numberOfPlayers;//selected position, player, and user's amount of money
            
            bank = 95000000;//starting money
            numberOfPlayers = 1;

            bool sentinel;
            sentinel = true;
            while (sentinel)
            {
                Console.WriteLine("Welcome to the NFL Draft!");

                for (var x = 0; x < players.GetLength(0); x++)//loop to display players
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.Write($"{position[x]}" + " (" + (x + 1) + ")");
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine("\n");
                    for (var y = 0; y < players.GetLength(1); y++)
                    {
                        Console.Write($"{x + 1}{y + 1}) {players[x, y]}  ");
                    }
                    Console.WriteLine("\n");
                    for (var z = 0; z < salary.GetLength(1); z++)
                    {
                        Console.Write($"      ${salary[x, z]}");
                    }

                    Console.WriteLine(" \n");
                }//end of loop

                Console.WriteLine("You may draft up to five players. (" + (6 - numberOfPlayers) + " picks left)");                       
                Console.WriteLine("Enter the number of the position you want to draft for.");
                Console.WriteLine("Bank: $" + (bank));
                draftPosition = Int32.Parse(Console.ReadLine());

                Console.Clear();

                Console.BackgroundColor = ConsoleColor.Blue;
                Console.WriteLine("You are drafting for a " + position[draftPosition - 1]);
                Console.WriteLine("Enter the number of the player you want to draft");
                Console.BackgroundColor = ConsoleColor.Black;

                for (var x = 0; x < 5; x++)//display players of the selected position
                {
                    Console.WriteLine($" {x + 1}) {players[draftPosition - 1, x]}  ${salary[draftPosition - 1, x]}");
                }//end of display position loop

                draftPlayer = Int32.Parse(Console.ReadLine());
                bank = bank - salary[draftPosition - 1, draftPlayer - 1];
                numberOfPlayers = numberOfPlayers + 1;
                

                Console.Clear();

                //check amount of money
                if (bank < 0)
                {
                    Console.WriteLine("You do not have enough money to draft this player");
                    break;
                }
                else
                {
                    Console.WriteLine("You have drafted " + (players[draftPosition - 1, draftPlayer - 1]));
                    if (numberOfPlayers > 5)
                    {
                        Console.WriteLine("You have drafted the maximum amount of players");
                        break;
                    }
                    Console.WriteLine("You now have $" + (bank) + " left to spend");
                    Console.WriteLine("Would you like to draft another player? (Y/N)");
                    string nextDraft = Console.ReadLine().ToUpper();
                    if (nextDraft == "Y")
                    {
                        sentinel = true;
                    }
                    else
                    {
                        sentinel = false;
                    }
                }

                Console.Clear();

                
            }
            Console.WriteLine("Thank you for using this program.");
            Console.WriteLine("(Press enter to close program)");
            Console.ReadLine();
        }//end of main
    }
}
