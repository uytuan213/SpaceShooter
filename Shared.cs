 /*
 * Program ID: Game Final Project
 * 
 * Purpose: Store wid and height of the screeen
 * 
 * Revision History:
 *      Tony Trieu written Nov 19, 2018
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace UTFinalProject
{
    /// <summary>
    /// Static class to store width, height of the screen
    /// and read and write high score to file
    /// </summary>
    public class Shared
    {
        public static Vector2 stage;

        private static string fileName = "highScore.txt";
        /// <summary>
        /// get the highest score in file
        /// If there is no file return 0
        /// </summary>
        /// <returns>Highest score</returns>
        public static int getHighestScore()
        {
            if (File.Exists(fileName))
            {
                StreamReader reader = new StreamReader(fileName);
                int result = int.Parse(reader.ReadLine());
                reader.Close();
                return result;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Get all score in file
        /// </summary>
        /// <returns>List of scores</returns>
        public static List<int> getHighScoresList()
        {
            int max = 10;
            List<int> lstHighScores = new List<int>();

            if (File.Exists(fileName))
            {
                StreamReader reader = new StreamReader(fileName);
                int count = 1;
                while (!reader.EndOfStream)
                {
                    lstHighScores.Add(int.Parse(reader.ReadLine()));
                    if (count >= max)
                    {
                        break;
                    }
                    count++;
                }
                reader.Close();
                return lstHighScores; 
            }
            return null;
        }

        /// <summary>
        /// Add new score to file
        /// </summary>
        /// <param name="score">Score that need to be stored</param>
        /// <returns>true if succeed, false if errors</returns>
        public static bool recordNewScore(int score)
        {
            int maxCount = 10;
            List<int> lstHighScores =  getHighScoresList();

            if (lstHighScores != null)
            {
                lstHighScores.Add(score);
                lstHighScores.Sort();
                lstHighScores.Reverse(); 
            }
            else
            {
                lstHighScores = new List<int>() { score};
            }
            try
            {
                int count = 0;
                StreamWriter writer = new StreamWriter(fileName, false);
                foreach (int item in lstHighScores)
                {
                    if (count < maxCount)
                    {
                        writer.WriteLine(item);
                    }
                    else
                        break;
                    count++;
                }
                writer.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
