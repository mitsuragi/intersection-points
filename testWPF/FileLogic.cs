using System.Collections.Generic;
using System.IO;
using Lab1;

namespace Lab1File
{
    public class FileLogic
    {
        public static void FileSaveInitial(string filename, List<Point2D> points, double rad)
        {
            string text = "";

            foreach (var pt in points)
            {
                text += pt.X.ToString() + '\n';
                text += pt.Y.ToString() + '\n';
            }

            text += rad;

            StreamWriter sw = new StreamWriter(filename);

            sw.WriteLine(text);

            sw.Close();
        }

        public static void FileSaveResult(string filename, List<Point2D> points)
        {
            string text = "";

            foreach (Point2D pt in points)
            {
                text += pt.ToString() + '\n';
            }

            StreamWriter sw = new StreamWriter(filename);

            sw.WriteLine(text);

            sw.Close();
        }

        public static List<string> FileLoad(string filename)
        {
            List<string> values = new List<string>();

            StreamReader sr = new StreamReader(filename);

            while (!sr.EndOfStream)
            {
                string? text = sr.ReadLine();

                if (!string.IsNullOrWhiteSpace(text))
                {
                    values.Add(text);
                }
            }

            sr.Close();

            return values;
        }
    }
}
