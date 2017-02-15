using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
namespace ShapeCalculator
{
    public abstract class Shape
    {
        public abstract double Area { get; }
        public abstract double Perimeter { get; }
    }
    public class Circle : Shape
    {
        private double radius;
        public Circle(double r)
        {
            radius = r;
        }
        public override double Area
        {
            get { return Math.PI * Math.Pow(radius, 2); }
        }

        public override double Perimeter
        {
            get { return 2 * Math.PI * radius; }
        }
    }

    public class Square : Shape
    {
        private double side;
        public Square(double s)
        {
            side = s;
        }
        public override double Area
        {
            get { return Math.Pow(side, 2); }
        }

        public override double Perimeter
        {
            get { return (4 * side); }
        }

    }
    public class Rectangle : Shape
    {
        private double width;
        private double length;
        public Rectangle(double w, double l)
        {
            width = w;
            length = l;
        }

        public override double Area
        {
            get { return width * length; }
        }

        public override double Perimeter
        {
            get { return 2 * width + 2 * length; }
        }

    }
    public class Triangle : Shape
    {
        private double side1;
        private double side2;
        private double side3;
        public Triangle(double s1, double s2, double s3)
        {
            side1 = s1;
            side2 = s2;
            side3 = s3;
        }
        public override double Area
        {
            get
            {
                double p = (side1 + side2 + side3) / 2;
                return Math.Sqrt(p * (p - side1) * (p - side2) * (p - side3));
            }
        }
        public override double Perimeter
        {
            get { return (side1 + side2 + side3); }
        }

    }

    public struct ShapeCal
    {
        public string Shape;
        public double Area;
        public double Perimeter;
    }
    public class ShapeCalculatorUtility
    {
        public List<ShapeCal> AreaPerimeterCalculation(string fileName)
        {
            List<ShapeCal> listA = new List<ShapeCal>();
            using (var fs = File.OpenRead(fileName))
            using (var reader = new StreamReader(fs))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    Shape shapeObj = null;

                    double shapeArea = 0;
                    double shapePerimeter = 0;
                    switch (values[0])
                    {
                        case "Circle":
                            shapeObj = new Circle(Convert.ToDouble(values[1]));
                            break;
                        case "Rectangle":
                            shapeObj = new Rectangle(Convert.ToDouble(values[1]), Convert.ToDouble(values[2]));
                            break;
                        case "Square":
                            shapeObj = new Square(Convert.ToDouble(values[1]));
                            break;
                        case "Triangle":
                            shapeObj = new Triangle(Convert.ToDouble(values[1]), Convert.ToDouble(values[2]), Convert.ToDouble(values[3]));
                            break;
                        default:
                            break;
                    }
                    shapeArea = shapeObj.Area;
                    shapePerimeter = shapeObj.Perimeter;
                    listA.Add(new ShapeCal() { Shape = values[0], Area = shapeArea, Perimeter = shapePerimeter });
                }
                return listA;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
                List<ShapeCal> myList = new List<ShapeCal>();
                try
                {
                    string filename = ConfigurationManager.AppSettings["ShapeFile"];
                    ShapeCalculatorUtility sc = new ShapeCalculatorUtility();
                    myList = sc.AreaPerimeterCalculation(filename);
                }
                catch (Exception e)
                {
                    Console.WriteLine("{0} Exception caught.", e);
                }
                finally
                { 
                    Console.WriteLine("Shape, Area ,  Perimeter");
                    for (int i = 0; i < myList.Count; i++)
                    {
                        Console.WriteLine(String.Format("{0} , {1}, {2}", myList[i].Shape, Math.Round(myList[i].Area, 2), Math.Round(myList[i].Perimeter, 2)));
                    }
                    Console.ReadLine();
                }
         }
    }
}
