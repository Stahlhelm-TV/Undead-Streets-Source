namespace UndeadStreets
{
    using GTA.Math;
    using System;
    using System.Collections.Generic;

    internal class RandoMath
    {
        private static System.Random random;

        public static int Abs(int value) => 
            ((value < 0) ? (value * -1) : value);

        public static T GetRandomElementFromArray<T>(T[] theArray)
        {
            T local2;
            if (theArray != null)
            {
                local2 = theArray[CachedRandom.Next(theArray.Length)];
            }
            else
            {
                local2 = default(T);
            }
            return local2;
        }

        public static T GetRandomElementFromList<T>(List<T> theList)
        {
            T local2;
            if (!ReferenceEquals(theList, null))
            {
                local2 = theList[CachedRandom.Next(theList.Count)];
            }
            else
            {
                local2 = default(T);
            }
            return local2;
        }

        public static int Max(int x, int y) => 
            ((x < y) ? y : x);

        public static float Max(float x, float y) => 
            ((x < y) ? y : x);

        public static int Min(int x, int y) => 
            ((x > y) ? y : x);

        public static float Min(float x, float y) => 
            ((x > y) ? y : x);

        public static bool RandomBool() => 
            (CachedRandom.Next(0, 2) == 0);

        public static Vector3 RandomDirection(bool zeroZ)
        {
            Vector3 zero = Vector3.Zero;
            zero = !zeroZ ? Vector3.RandomXYZ() : Vector3.RandomXY();
            zero.Normalize();
            return zero;
        }

        public static float RandomHeading() => 
            (((float) CachedRandom.NextDouble()) * 360f);

        public static int TrimValue(int value, int min, int max)
        {
            value = Max(min, value);
            value = Min(max, value);
            return value;
        }

        public static float TrimValue(float value, float min, float max)
        {
            value = Max(min, value);
            value = Min(max, value);
            return value;
        }

        public static System.Random CachedRandom
        {
            get
            {
                if (ReferenceEquals(random, null))
                {
                    random = new System.Random();
                }
                return random;
            }
        }
    }
}

