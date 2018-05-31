using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Legends.ORM.Addon
{
    public class SQLVector3
    {
        public const string FORMATTER = "({0} {1} {2})";

        public float X
        {
            get;
            set;
        }
        public float Y
        {
            get;
            set;
        }
        public float Z
        {
            get;
            set;
        }
        public Vector3 ToVector3()
        {
            return new Vector3(X, Y, Z);
        }
        public SQLVector3()
        {

        }
        public SQLVector3(Vector3 vector3)
        {
            this.X = vector3.X;
            this.Y = vector3.Y;
            this.Z = vector3.Z;
        }
        public override string ToString()
        {
            string x = SQLVector2.Convert(X);
            string y = SQLVector2.Convert(Y);
            string z = SQLVector2.Convert(Z);
            return string.Format(FORMATTER, x, y, z);
        }
      
        public static SQLVector3 Deserialize(string data)
        {
            string[] split = data.Split(' ');
            string x = new string(split[0].Skip(1).ToArray()).Replace('.', ',');
            string y = split[1].Replace('.', ',');
            string z = split[2].Substring(0, split[2].Length - 1).Replace('.', ',');

            return new SQLVector3()
            {
                X = float.Parse(x),
                Y = float.Parse(y),
                Z = float.Parse(z),
            };
        }
    }
}

