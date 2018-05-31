using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Legends.ORM.Addon
{
    /// <summary>
    /// Semms hardcoded, but 1000ms to deserialize 1 000 000 vectors is ok. :3
    /// 
    /// ...
    /// 
    /// 
    /// ...
    /// 
    /// 
    /// LOL -> its hardcoded :'(
    /// </summary>
    public class SQLVector2
    {
        public const string FORMATTER = "({0} {1})";

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
        public Vector2 ToVector2()
        {
            return new Vector2(X, Y);
        }
        public SQLVector2()
        {

        }
        public SQLVector2(Vector2 vector2)
        {
            this.X = vector2.X;
            this.Y = vector2.Y;
        }
        public override string ToString()
        {
            return string.Format(FORMATTER, Convert(X), Convert(Y));
        }
        public static string Convert(float f)
        {
            string integer = Math.Truncate(f).ToString();

            var split = f.ToString().Split(',');

            if (split.Length > 1)
            {
                string deci = split.Last();
                return integer + "." + deci;
            }
            else
            {
                return integer;
            }
        }
        public static SQLVector2 Deserialize(string data)
        {
            string[] split = data.Split(' ');
            string x = new string(split[0].Skip(1).ToArray()).Replace('.', ',');
            string y = split[1].Remove(split[1].Length - 1).Replace('.', ',');

            return new SQLVector2()
            {
                X = float.Parse(x),
                Y = float.Parse(y),
            };
        }
    }
}
