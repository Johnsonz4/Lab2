
using sspLab2;
/**
*--------------------------------------------------------------------
* File name: ssLab1
* Project name: Lab 2 
*--------------------------------------------------------------------
* Author’s names and emails:  Zoe Johnson, johnsonz3@etsu.edu 
* Course-Section: CSCI-2910-001
* Creation Date: 9/18/23
* -------------------------------------------------------------------
*/
using System;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace sslab2;
class Program
{
    static void Main(string[] args)
    {
        
            List<VideoGame> videoGames = ReadVideoGameFromCSV();

        // step 2 
        Dictionary<string, List<VideoGame>> platformDictionary = videoGames
            .GroupBy(game => game.Platform)
            .ToDictionary(group => group.Key, group => group.ToList());

        // step 3 
        foreach (var platform in platformDictionary)
        {
            string platformName = platform.Key;
            List<VideoGame> platformGames = platform.Value;

           
            var top5Games = platformGames
                .OrderByDescending(game => game.GlobalSales)
                .Take(5);

            
            Console.WriteLine($"Top 5 Games for Platform: {platformName}");
            foreach (var game in top5Games)
            {
                Console.WriteLine(game.ToString());
            }

            Console.WriteLine();
            Console.ReadLine();
        }
    }

   
    static List<VideoGame> ReadVideoGameFromCSV()
    {
        List<VideoGame> videoGames = new List<VideoGame>();

        
        string filePath = @"/Users/zoe/Projects/videogames.csv";
        try
        {
            string[] lines = File.ReadAllLines(filePath);

            for (int i = 1; i < lines.Length; i++)
            {
                string[] values = lines[i].Split(',');

                if (values.Length >= 10)
                {
                    VideoGame game = new VideoGame
                    {
                        Name = values[0],
                        Platform = values[1],
                        Year = int.Parse(values[2]),
                        Genre = values[3],
                        Publisher = values[4],
                        NASales = double.Parse(values[5]),
                        EUSales = double.Parse(values[6]),
                        JPSales = double.Parse(values[7]),
                        OtherSales = double.Parse(values[8]),
                        GlobalSales = double.Parse(values[9])
                    };

                    videoGames.Add(game);
                }
                else
                {
                    Console.WriteLine($"Skipping line {i + 1}: Insufficient data.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

        return videoGames;
        Console.ReadLine();
    }
}












