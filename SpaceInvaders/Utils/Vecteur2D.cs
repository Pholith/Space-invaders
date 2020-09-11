using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace SpaceInvaders
{
    public class Vecteur2D
    {

        public static Vecteur2D zero = new Vecteur2D(0, 0);

        public double X { get; private set; }
        public double Y { get; private set; }

        public Vecteur2D(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }


        public Vecteur2D() : this(0, 0)
        {
        }

        public double Norme { get; private set; }

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

        public override bool Equals(object obj)
        {

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Vecteur2D v1 = (Vecteur2D)obj;
            return X == v1.X && Y == v1.Y;
        }

        public override int GetHashCode()
        {
            return (int)(X + Y);
        }


    }
}
