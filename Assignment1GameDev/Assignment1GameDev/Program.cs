using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1GameDev
{
    class Program
    {
        static float getDeltaX(float point1, float point2)
        {
            float difference = point2 - point1;
            return difference;
        }

        static float getDeltaY(float point1, float point2)
        {
            float difference = point2 - point1;
            return difference;
        }

        static float pythagoreanTheorem(float side1, float side2)
        {
            // a^2 + b^2 = c^2
            float sides = (float)Math.Pow(side1, 2) + (float)Math.Pow(side2, 2);
            float result = (float)Math.Sqrt(sides);
            return result;
        }

        static float getAngle(float deltaX, float deltaY)
        {
            float angleInRadians = (float)Math.Atan2(deltaY, deltaX);
            return angleInRadians * (180 / (float)Math.PI);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to my Assignment 1. This application will calculate the distance\nbetween two points and the angle between those points.\n ");
            Console.WriteLine("Please enter an x value for the first point: \n ");
            float point1X = float.Parse(Console.ReadLine());
            Console.WriteLine("Please enter the y value for the first point: \n ");
            float point1Y = float.Parse(Console.ReadLine());
            Console.WriteLine("Please enter the x value for the second point: \n ");
            float point2X = float.Parse(Console.ReadLine());
            Console.WriteLine("Please enter the y value for the second point: \n ");
            float point2Y = float.Parse(Console.ReadLine());
            Console.WriteLine("The distance between the two points is ({0}, {1}) \n ", getDeltaX(point1X, point2X).ToString("F3"), getDeltaY(point1Y, point2Y).ToString("F3"));
            Console.WriteLine("The angle between the two points in degrees is: {0}", getAngle(getDeltaX(point1X, point2X), getDeltaY(point1Y, point2Y)));

            Console.ReadLine();
        }
    }
}
