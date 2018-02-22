using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.DeviceSensors.Shared
{
    /// <summary>
    /// Device sensor vector.
    /// </summary>
    public class VectorReading 
    {

        public VectorReading(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Gets the x.
        /// </summary>
        /// <value>The x.</value>
        public double X { get; }
        /// <summary>
        /// Gets the y.
        /// </summary>
        /// <value>The y.</value>
        public double Y { get; }
        /// <summary>
        /// Gets the z.
        /// </summary>
        /// <value>The z.</value>
        public double Z { get;  }

        /// <summary>
        /// Vector to string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"X={X}, Y={Y}, Z={Z}";
        }

       /* /// <summary>
        /// Gets total value.
        /// </summary>
        /// <value>The value.</value>
        public override double? Value
        {
            get
            {
                return Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2) + Math.Pow(Z, 2));
            }
        }*/
    }
}
