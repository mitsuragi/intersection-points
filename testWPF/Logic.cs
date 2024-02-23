using System;
using System.Collections.Generic;

namespace Lab1
{
    public class Logic
    {
        // Функция для получения коэффициентов A B и C уравнения прямой вида Ax + By + C = 0
        public static void GetBeamFunctionParametrs(Point2D point1, Point2D point2, out double A, out int B, out double C)
        {
            if (point1.X == point2.X)
            {
                A = 0;
                B = 0;
                C = 0;
                return;
            }

            // В нашем случае коэффициент B будет всегда равен -1, так как нам нужно вынести y в правую часть уравнения,
            // из-за чего его коэффициент, всегда равный 1, поменяет знак и станет -1

            A = (point2.Y - point1.Y) / (point2.X - point1.X);

            B = -1;

            C = point2.Y - A * point2.X;
        }

        public static List<Point2D> GetIntersectionPoints(Point2D beamPoint1, Point2D beamPoint2, Point2D circlePoint, double rad)
        {
            //Погрешность
            const double EPS = 1.0E-5;
            double coefA, coefC;
            int coefB;
            Point2D temp = new Point2D();
            List<Point2D> result = new List<Point2D>();

            GetBeamFunctionParametrs(beamPoint1, beamPoint2, out coefA, out coefB, out coefC);
            
            if (coefB == 0)
            {
                return new List<Point2D>();
            }

            coefC -= circlePoint.Y - coefA * circlePoint.X;

            double x0 = -coefA * coefC / (Math.Pow(coefA, 2) + Math.Pow(coefB, 2));
            double y0 = -coefB * coefC / (Math.Pow(coefA, 2) + Math.Pow(coefB, 2));

            if (Math.Pow(coefC, 2) > Math.Pow(rad, 2) * (Math.Pow(coefA, 2) + Math.Pow(coefB, 2)) + EPS)
            {
                return new List<Point2D>();
            }
            else if (Math.Abs(Math.Pow(coefC, 2) - Math.Pow(rad, 2) * (Math.Pow(coefA, 2) + Math.Pow(coefB, 2))) < EPS)
            {
                x0 += circlePoint.X;
                y0 += circlePoint.Y;
                if (beamPoint2.X - beamPoint1.X > 0 && beamPoint1.X <= x0)
                {
                    temp.Set(x0, y0);
                    result.Add(temp);
                }
                else if (beamPoint2.X - beamPoint1.X < 0 && beamPoint1.X >= x0)
                {
                    temp.Set(x0, y0);
                    result.Add(temp);
                }
                else
                {
                    return new List<Point2D>();
                }
            }
            else
            {
                double d = Math.Pow(rad, 2) - Math.Pow(coefC, 2) / (Math.Pow(coefA, 2) + Math.Pow(coefB, 2));
                double mult = Math.Sqrt(d / (Math.Pow(coefA, 2) + Math.Pow(coefB, 2)));
                double ax, ay, bx, by;
                ax = x0 + coefB * mult + circlePoint.X;
                bx = x0 - coefB * mult + circlePoint.X;
                ay = y0 - coefA * mult + circlePoint.Y;
                by = y0 + coefA * mult + circlePoint.Y;
                if (beamPoint2.X - beamPoint1.X > 0)
                {
                    if (beamPoint1.X <= ax && beamPoint1.X <= bx)
                    {
                        temp.Set(ax, ay);
                        result.Add(temp);
                        temp.Set(bx, by);
                        result.Add(temp);
                    }
                    else if (beamPoint1.X <= ax)
                    {
                        temp.Set(ax, ay);
                        result.Add(temp);
                    }
                    else if (beamPoint1.X <= bx)
                    {
                        temp.Set(bx, by);
                        result.Add(temp);
                    }
                    else
                    {
                        return new List<Point2D>();
                    }
                }
                else
                {
                    if (beamPoint1.X >= ax && beamPoint1.X >= bx)
                    {
                        temp.Set(ax, ay);
                        result.Add(temp);
                        temp.Set(bx, by);
                        result.Add(temp);
                    }
                    else if (beamPoint1.X >= ax)
                    {
                        temp.Set(ax, ay);
                        result.Add(temp);
                    }
                    else if (beamPoint1.X >= bx)
                    {
                        temp.Set(bx, by);
                        result.Add(temp);
                    }
                    else
                    {
                        return new List<Point2D>();
                    }
                }
            }

            return result;
        }
    }

    public struct Point2D
    {
        public double X, Y;

        public void Set(string x, string y)
        {
            //try
            //{
                X = Convert.ToDouble(x);
                Y = Convert.ToDouble(y);
            //}
            //catch (FormatException)
            //{
            //    throw;
            //}
        }
        public void Set(double x, double y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"{X:f3} {Y:f3}";
        }
    }
}