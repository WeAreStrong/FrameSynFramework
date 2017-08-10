namespace FifaBattleEngine
{
    using System;
    using UnityEngine;

    public struct FixPoint
    {
        private int mInnerData;
        public int innerData
        {
            get { return mInnerData; }
            set { mInnerData = value; }
        }

        private const int precision = 100;

        public FixPoint(float value)
        {
            mInnerData = Convert.ToInt32(Math.Floor(value * precision));
        }

        public FixPoint(int value)
        {
            mInnerData = value * precision;
        }

        public float ToFloat()
        {
            return Convert.ToSingle(mInnerData) / precision;
        }

        public int ToInt32()
        {
            return mInnerData / precision;
        }

        public override string ToString()
        {
            return ToFloat().ToString();
        }

        public static implicit operator FixPoint(float value)
        {
            return new FixPoint(value);
        }

        public static implicit operator FixPoint(int value)
        {
            return new FixPoint(value);
        }

        public static implicit operator float(FixPoint value)
        {
            return value.ToFloat();
        }

        public static explicit operator int(FixPoint value)
        {
            return value.ToInt32();
        }

        public static FixPoint operator +(FixPoint a, FixPoint b)
        {
            int value = a.mInnerData + b.mInnerData;
            FixPoint add = new FixPoint();
            add.innerData = value;
            return add;
        }

        public static FixPoint operator -(FixPoint a, FixPoint b)
        {
            int value = a.mInnerData - b.mInnerData;
            FixPoint minus = new FixPoint();
            minus.innerData = value;
            return minus;
        }
        
        public static FixPoint operator *(FixPoint a, FixPoint b)
        {
            return new FixPoint(a.ToFloat() * b.ToFloat());
        }

        public static FixPoint operator /(FixPoint a, FixPoint b)
        {
            return new FixPoint(a.ToFloat() / b.ToFloat());
        }
    }

    public struct FixPointVector3
    {
        public FixPoint x;
        public FixPoint y;
        public FixPoint z;

        public FixPointVector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public float magnitude
        {
            get
            {
                float _x = x;
                float _y = y;
                float _z = z;
                return Mathf.Sqrt(_x * _x + _y * _y + _z * _z);
            }
        }

        public override string ToString()
        {
            return string.Format("x = {0} y = {1} z = {2}", x.ToFloat(), y.ToFloat(), z.ToFloat());
        }

        public static implicit operator Vector3(FixPointVector3 value)
        {
            return new Vector3(value.x, value.y, value.z);
        }

        public static implicit operator FixPointVector3(Vector3 value)
        {
            return new FixPointVector3(value.x, value.y, value.z);
        }
    }
}