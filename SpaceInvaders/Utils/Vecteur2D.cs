using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace SpaceInvaders
{
    public sealed class Vecteur2D
    {

        public static Vecteur2D zero = new Vecteur2D(0, 0);

        public double X { get; private set; }
        public double Y { get; private set; }

        public Vecteur2D(float x, float y)
        {
            X = x;
            Y = y;
        }

        public Vecteur2D(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Vecteur2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Vecteur2D() : this(0, 0)
        {
        }

        public double Norme()
        {
            return Math.Sqrt((X * X) + (Y * Y));
        }

        public Vecteur2D Normalize()
        {
            double a = Norme();
            double x = a * X / Math.Abs(a);
            double y = a * Y / Math.Abs(a);
            return new Vecteur2D(x, y);
        }

        public Vecteur2D SetNewMagnitude(double newNorme)
        {
            double x = X / Norme() * newNorme;
            double y = Y / Norme() * newNorme;

            return new Vecteur2D(x, y);
        }

        public static Vecteur2D operator +(Vecteur2D a, Vecteur2D b)
        {
            return new Vecteur2D(a.X + b.X, a.Y + b.Y);
        }
        public static Vecteur2D operator -(Vecteur2D a, Vecteur2D b)
        {
            return new Vecteur2D(a.X - b.X, a.Y - b.Y);
        }

        public static Vecteur2D operator -(Vecteur2D a)
        {
            return new Vecteur2D(-a.X, -a.Y);
        }
        public static Vecteur2D operator *(Vecteur2D a, double b)
        {
            return new Vecteur2D(a.X * b, a.Y * b);
        }
        public static Vecteur2D operator *(double a, Vecteur2D b)
        {
            return new Vecteur2D(b.X * a, b.Y * a);
        }
        public static Vecteur2D operator /(Vecteur2D a, double b)
        {
            return new Vecteur2D(a.X / b, a.Y / b);
        }
        public static bool operator ==(Vecteur2D a, Vecteur2D b)
        {
            if (a is null || b is null) return false;
            return a.X == b.X && a.Y == b.Y;
        }
        public static bool operator !=(Vecteur2D a, Vecteur2D b)
        {
            return !(a == b);
        }


        public static double Distance(Vecteur2D a, Vecteur2D b)
        {
            return Math.Sqrt(Math.Pow(b.X - a.X, 2) + Math.Pow(b.Y - a.Y, 2));
        }
        public override bool Equals(object obj)
        {

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Vecteur2D v1 = (Vecteur2D)obj;
            return X == v1.X && Y == v1.Y;
        }

        
        public static Vecteur2D FromAngle(int degreeAngle, int magnitude = 1)
        {
            return FromAngle(Utils.Utils.DegToRad(degreeAngle), magnitude);
        }
        public static Vecteur2D FromAngle(double radianAngle, int magnitude = 1)
        {
            return new Vecteur2D(Math.Cos(radianAngle), Math.Sin(radianAngle)) * magnitude;
        }
        
        public Vecteur2D Rotate(int degrees)
        {
            return Rotate(Utils.Utils.DegToRad(degrees));
        }

        public Vecteur2D Rotate(double radians)
        {
            double ca = Math.Cos(radians);
            double sa = Math.Sin(radians);
            return new Vecteur2D(ca * X - sa * Y, sa * X + ca * Y);
        }

        public Vecteur2D Round()
        {
            return new Vecteur2D(Math.Round(X), Math.Round(Y));
        }

        
        public static Vecteur2D FromTargetObject(Vecteur2D srcPosition, Vecteur2D targetPosition, double speed)
        {
            return (targetPosition - srcPosition).SetNewMagnitude(speed);
        }

        public override int GetHashCode()
        {
            return (int)(X + Y);
        }

    }
}
