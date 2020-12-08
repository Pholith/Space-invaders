using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace SpaceInvaders
{
    public sealed class Vector2
    {

        public static Vector2 zero = new Vector2(0, 0);

        public double X { get; private set; }
        public double Y { get; private set; }

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public Vector2(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Vector2(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Vector2() : this(0, 0)
        {
        }

        public double Norme()
        {
            return Math.Sqrt((X * X) + (Y * Y));
        }

        public Vector2 Normalize()
        {
            double a = Norme();
            double x = a * X / Math.Abs(a);
            double y = a * Y / Math.Abs(a);
            return new Vector2(x, y);
        }

        public Vector2 SetNewMagnitude(double newNorme)
        {
            double x = X / Norme() * newNorme;
            double y = Y / Norme() * newNorme;

            return new Vector2(x, y);
        }

        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X + b.X, a.Y + b.Y);
        }
        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X - b.X, a.Y - b.Y);
        }

        public static Vector2 operator -(Vector2 a)
        {
            return new Vector2(-a.X, -a.Y);
        }
        public static Vector2 operator *(Vector2 a, double b)
        {
            return new Vector2(a.X * b, a.Y * b);
        }
        public static Vector2 operator *(double a, Vector2 b)
        {
            return new Vector2(b.X * a, b.Y * a);
        }
        public static Vector2 operator /(Vector2 a, double b)
        {
            return new Vector2(a.X / b, a.Y / b);
        }
        public static bool operator ==(Vector2 a, Vector2 b)
        {
            if (a is null || b is null) return false;
            return a.X == b.X && a.Y == b.Y;
        }
        public static bool operator !=(Vector2 a, Vector2 b)
        {
            return !(a == b);
        }


        public static double Distance(Vector2 a, Vector2 b)
        {
            return Math.Sqrt(Math.Pow(b.X - a.X, 2) + Math.Pow(b.Y - a.Y, 2));
        }
        public override bool Equals(object obj)
        {

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Vector2 v1 = (Vector2)obj;
            return X == v1.X && Y == v1.Y;
        }

        
        public static Vector2 FromAngle(int degreeAngle, int magnitude = 1)
        {
            return FromAngle(Utils.Utils.DegToRad(degreeAngle), magnitude);
        }
        public static Vector2 FromAngle(double radianAngle, int magnitude = 1)
        {
            return new Vector2(Math.Cos(radianAngle), Math.Sin(radianAngle)) * magnitude;
        }
        
        public Vector2 Rotate(int degrees)
        {
            return Rotate(Utils.Utils.DegToRad(degrees));
        }

        public Vector2 Rotate(double radians)
        {
            double ca = Math.Cos(radians);
            double sa = Math.Sin(radians);
            return new Vector2(ca * X - sa * Y, sa * X + ca * Y);
        }

        public Vector2 Round()
        {
            return new Vector2(Math.Round(X), Math.Round(Y));
        }

        
        public static Vector2 FromTargetObject(Vector2 srcPosition, Vector2 targetPosition, double speed)
        {
            return (targetPosition - srcPosition).SetNewMagnitude(speed);
        }

        public override int GetHashCode()
        {
            return (int)(X + Y);
        }

    }
}
