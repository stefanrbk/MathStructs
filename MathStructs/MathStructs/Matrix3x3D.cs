using System;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace MathStructs
{
    public struct Matrix3x3D : IEquatable<Matrix3x3D>
    {
#if DEBUG
        public static bool AllowAvx = true;
        public static bool AllowSse = true;
#endif
        public double M11;
        public double M12;
        public double M13;
        public double M21;
        public double M22;
        public double M23;
        public double M31;
        public double M32;
        public double M33;
#pragma warning disable IDE0044 // Add readonly modifier
#pragma warning disable IDE0052 // Remove unread private members
        private double padding;
#pragma warning restore IDE0052 // Remove unread private members
#pragma warning restore IDE0044 // Add readonly modifier
        private static readonly Matrix3x3D _identity = new Matrix3x3D(1, 0, 0, 0, 1, 0, 0, 0, 1);
        public static Matrix3x3D Identity => _identity;
        public readonly bool IsIdentity =>
            M11 is 1.0 &&
            M12 is 0.0 &&
            M13 is 0.0 &&
            M21 is 0.0 &&
            M22 is 1.0 &&
            M23 is 0.0 &&
            M31 is 0.0 &&
            M32 is 0.0 &&
            M33 is 1.0;
        public Matrix3x3D(double m11, double m12, double m13, double m21, double m22, double m23, double m31, double m32, double m33)
        {
            M11 = m11;
            M12 = m12;
            M13 = m13;
            M21 = m21;
            M22 = m22;
            M23 = m23;
            M31 = m31;
            M32 = m32;
            M33 = m33;
            padding = 0;
        }
        public static unsafe Matrix3x3D operator -(Matrix3x3D value)
        {
            var result = new Matrix3x3D();

#if DEBUG
            if (Avx.IsSupported && AllowAvx)
#else
            if (Avx.IsSupported)
#endif
            {
                var zero = Vector256<double>.Zero;

                Avx.Store(&result.M11, Avx.Subtract(zero, Avx.LoadVector256(&value.M11)));
                Avx.Store(&result.M22, Avx.Subtract(zero, Avx.LoadVector256(&value.M22)));
            }
#if DEBUG
            else if (Sse.IsSupported && AllowSse)
#else
            else if (Sse.IsSupported)
#endif
            {
                var zero = Vector128<double>.Zero;

                Sse2.Store(&result.M11, Sse2.Subtract(zero, Sse2.LoadVector128(&value.M11)));
                Sse2.Store(&result.M13, Sse2.Subtract(zero, Sse2.LoadVector128(&value.M13)));
                Sse2.Store(&result.M22, Sse2.Subtract(zero, Sse2.LoadVector128(&value.M22)));
                Sse2.Store(&result.M31, Sse2.Subtract(zero, Sse2.LoadVector128(&value.M31)));
            }
            else
            {
                result.M11 = -value.M11;
                result.M12 = -value.M12;
                result.M13 = -value.M13;
                result.M21 = -value.M21;
                result.M22 = -value.M22;
                result.M23 = -value.M23;
                result.M31 = -value.M31;
                result.M32 = -value.M32;
            }
            result.M33 = -value.M33;
            return result;
        }
        public static Matrix3x3D operator +(Matrix3x3D value) =>
            value;
        public static unsafe Matrix3x3D operator +(Matrix3x3D left, Matrix3x3D right)
        {
            var result = new Matrix3x3D();

#if DEBUG
            if (Avx.IsSupported && AllowAvx)
#else
            if (Avx.IsSupported)
#endif
            {
                Avx.Store(&result.M11, Avx.Add(Avx.LoadVector256(&left.M11), Avx.LoadVector256(&right.M11)));
                Avx.Store(&result.M22, Avx.Add(Avx.LoadVector256(&left.M22), Avx.LoadVector256(&right.M22)));
            }
#if DEBUG
            else if (Sse2.IsSupported && AllowSse)
#else
            else if (Sse2.IsSupported)
#endif
            {
                Sse2.Store(&result.M11, Sse2.Add(Sse2.LoadVector128(&left.M11), Sse2.LoadVector128(&right.M11)));
                Sse2.Store(&result.M13, Sse2.Add(Sse2.LoadVector128(&left.M13), Sse2.LoadVector128(&right.M13)));
                Sse2.Store(&result.M22, Sse2.Add(Sse2.LoadVector128(&left.M22), Sse2.LoadVector128(&right.M22)));
                Sse2.Store(&result.M31, Sse2.Add(Sse2.LoadVector128(&left.M31), Sse2.LoadVector128(&right.M31)));
            }
            else
            {
                result.M11 = left.M11 + right.M11;
                result.M12 = left.M12 + right.M12;
                result.M13 = left.M13 + right.M13;
                result.M21 = left.M21 + right.M21;
                result.M22 = left.M22 + right.M22;
                result.M23 = left.M23 + right.M23;
                result.M31 = left.M31 + right.M31;
                result.M32 = left.M32 + right.M32;
            }
            result.M33 = left.M33 + right.M33;
            return result;
        }
        public static unsafe Matrix3x3D operator -(Matrix3x3D left, Matrix3x3D right)
        {
            var result = new Matrix3x3D();

#if DEBUG
            if (Avx.IsSupported && AllowAvx)
#else
            if (Avx.IsSupported)
#endif
            {
                Avx.Store(&result.M11, Avx.Subtract(Avx.LoadVector256(&left.M11), Avx.LoadVector256(&right.M11)));
                Avx.Store(&result.M22, Avx.Subtract(Avx.LoadVector256(&left.M22), Avx.LoadVector256(&right.M22)));
            }
#if DEBUG
            else if (Sse2.IsSupported && AllowSse)
#else
            else if (Sse2.IsSupported)
#endif
            {
                Sse2.Store(&result.M11, Sse2.Subtract(Sse2.LoadVector128(&left.M11), Sse2.LoadVector128(&right.M11)));
                Sse2.Store(&result.M13, Sse2.Subtract(Sse2.LoadVector128(&left.M13), Sse2.LoadVector128(&right.M13)));
                Sse2.Store(&result.M22, Sse2.Subtract(Sse2.LoadVector128(&left.M22), Sse2.LoadVector128(&right.M22)));
                Sse2.Store(&result.M31, Sse2.Subtract(Sse2.LoadVector128(&left.M31), Sse2.LoadVector128(&right.M31)));
            }
            else
            {
                result.M11 = left.M11 - right.M11;
                result.M12 = left.M12 - right.M12;
                result.M13 = left.M13 - right.M13;
                result.M21 = left.M21 - right.M21;
                result.M22 = left.M22 - right.M22;
                result.M23 = left.M23 - right.M23;
                result.M31 = left.M31 - right.M31;
                result.M32 = left.M32 - right.M32;
            }
            result.M33 = left.M33 - right.M33;
            return result;
        }
        public static unsafe Matrix3x3D operator *(Matrix3x3D left, Matrix3x3D right)
        {
            var result = new Matrix3x3D();

#if DEBUG
            if (Avx.IsSupported && AllowAvx)
#else
            if (Avx2.IsSupported)
#endif
            {
                var vector1 = Vector256.Create(left.M11);
                var vector2 = Vector256.Create(left.M12);
                var vector3 = Vector256.Create(left.M13);
                Avx.Store(&result.M11, Avx.Add(Avx.Add(Avx.Multiply(vector1,
                                                                    Avx.LoadVector256(&right.M11)),
                                                       Avx.Multiply(vector2,
                                                                    Avx.LoadVector256(&right.M21))),
                                               Avx.Multiply(vector3,
                                                            Avx.LoadVector256(&right.M31))));
                vector1 = Vector256.Create(left.M21);
                vector2 = Vector256.Create(left.M22);
                vector3 = Vector256.Create(left.M23);
                Avx.Store(&result.M21, Avx.Add(Avx.Add(Avx.Multiply(vector1,
                                                                    Avx.LoadVector256(&right.M11)),
                                                       Avx.Multiply(vector2,
                                                                    Avx.LoadVector256(&right.M21))),
                                               Avx.Multiply(vector3,
                                                            Avx.LoadVector256(&right.M31))));
                vector1 = Vector256.Create(left.M31);
                vector2 = Vector256.Create(left.M32);
                vector3 = Vector256.Create(left.M33);
                Avx.Store(&result.M31, Avx.Add(Avx.Add(Avx.Multiply(vector1,
                                                                    Avx.LoadVector256(&right.M11)),
                                                       Avx.Multiply(vector2,
                                                                    Avx.LoadVector256(&right.M21))),
                                               Avx.Multiply(vector3,
                                                            Avx.LoadVector256(&right.M31))));
            }
#if DEBUG
            else if (Sse2.IsSupported && AllowSse)
#else
            else if (Sse2.IsSupported)
#endif
            {
                var vector1 = Sse2.LoadVector128(&left.M11);
                var vector2 = Sse2.LoadScalarVector128(&left.M13);
                Sse2.Store(&result.M11, Sse2.Add(Sse2.Add(Sse2.Multiply(Sse2.Shuffle(vector1, vector1, 0),
                                                                        Sse2.LoadVector128(&right.M11)),
                                                          Sse2.Multiply(Sse2.Shuffle(vector1, vector1, 3),
                                                                        Sse2.LoadVector128(&right.M21))),
                                                 Sse2.Multiply(Sse2.Shuffle(vector2, vector2, 0),
                                                               Sse2.LoadVector128(&right.M31))));
                Sse2.Store(&result.M13, Sse2.Add(Sse2.Add(Sse2.Multiply(Sse2.Shuffle(vector1, vector1, 0),
                                                                        Sse2.LoadScalarVector128(&right.M13)),
                                                          Sse2.Multiply(Sse2.Shuffle(vector1, vector1, 3),
                                                                        Sse2.LoadScalarVector128(&right.M23))),
                                                 Sse2.Multiply(Sse2.Shuffle(vector2, vector2, 0),
                                                               Sse2.LoadScalarVector128(&right.M33))));
                vector1 = Sse2.LoadVector128(&left.M21);
                vector2 = Sse2.LoadScalarVector128(&left.M23);
                Sse2.Store(&result.M21, Sse2.Add(Sse2.Add(Sse2.Multiply(Sse2.Shuffle(vector1, vector1, 0),
                                                                        Sse2.LoadVector128(&right.M11)),
                                                          Sse2.Multiply(Sse2.Shuffle(vector1, vector1, 3),
                                                                        Sse2.LoadVector128(&right.M21))),
                                                 Sse2.Multiply(Sse2.Shuffle(vector2, vector2, 0),
                                                               Sse2.LoadVector128(&right.M31))));
                Sse2.Store(&result.M23, Sse2.Add(Sse2.Add(Sse2.Multiply(Sse2.Shuffle(vector1, vector1, 0),
                                                                        Sse2.LoadScalarVector128(&right.M13)),
                                                          Sse2.Multiply(Sse2.Shuffle(vector1, vector1, 3),
                                                                        Sse2.LoadScalarVector128(&right.M23))),
                                                 Sse2.Multiply(Sse2.Shuffle(vector2, vector2, 0),
                                                               Sse2.LoadScalarVector128(&right.M33))));
                vector1 = Sse2.LoadVector128(&left.M31);
                vector2 = Sse2.LoadScalarVector128(&left.M33);
                Sse2.Store(&result.M31, Sse2.Add(Sse2.Add(Sse2.Multiply(Sse2.Shuffle(vector1, vector1, 0),
                                                                        Sse2.LoadVector128(&right.M11)),
                                                          Sse2.Multiply(Sse2.Shuffle(vector1, vector1, 3),
                                                                        Sse2.LoadVector128(&right.M21))),
                                                 Sse2.Multiply(Sse2.Shuffle(vector2, vector2, 0),
                                                               Sse2.LoadVector128(&right.M31))));
                Sse2.Store(&result.M33, Sse2.Add(Sse2.Add(Sse2.Multiply(Sse2.Shuffle(vector1, vector1, 0),
                                                                        Sse2.LoadScalarVector128(&right.M13)),
                                                          Sse2.Multiply(Sse2.Shuffle(vector1, vector1, 3),
                                                                        Sse2.LoadScalarVector128(&right.M23))),
                                                 Sse2.Multiply(Sse2.Shuffle(vector2, vector2, 0),
                                                               Sse2.LoadScalarVector128(&right.M33))));
            }
            else
            {
                result.M11 = left.M11 * right.M11 + left.M12 * right.M21 + left.M13 * right.M31;
                result.M12 = left.M11 * right.M12 + left.M12 * right.M22 + left.M13 * right.M32;
                result.M13 = left.M11 * right.M13 + left.M12 * right.M23 + left.M13 * right.M33;
                result.M21 = left.M21 * right.M11 + left.M22 * right.M21 + left.M23 * right.M31;
                result.M22 = left.M21 * right.M12 + left.M22 * right.M22 + left.M23 * right.M32;
                result.M23 = left.M21 * right.M13 + left.M22 * right.M23 + left.M23 * right.M33;
                result.M31 = left.M31 * right.M11 + left.M32 * right.M21 + left.M33 * right.M31;
                result.M32 = left.M31 * right.M12 + left.M32 * right.M22 + left.M33 * right.M32;
                result.M33 = left.M31 * right.M13 + left.M32 * right.M23 + left.M33 * right.M33;
            }

            return result;
        }
        public static unsafe Matrix3x3D operator *(Matrix3x3D left, double right)
        {
            var result = new Matrix3x3D();

#if DEBUG
            if (Avx.IsSupported && AllowAvx)
#else
            if (Avx.IsSupported)
#endif
            {
                var r = Vector256.Create(right);
                Avx.Store(&result.M11, Avx.Multiply(Avx.LoadVector256(&left.M11), r));
                Avx.Store(&result.M22, Avx.Multiply(Avx.LoadVector256(&left.M22), r));
            }
#if DEBUG
            else if (Sse2.IsSupported && AllowSse)
#else
            else if (Sse2.IsSupported)
#endif
            {
                var r = Vector128.Create(right);
                Sse2.Store(&result.M11, Sse2.Multiply(Sse2.LoadVector128(&left.M11), r));
                Sse2.Store(&result.M13, Sse2.Multiply(Sse2.LoadVector128(&left.M13), r));
                Sse2.Store(&result.M22, Sse2.Multiply(Sse2.LoadVector128(&left.M22), r));
                Sse2.Store(&result.M31, Sse2.Multiply(Sse2.LoadVector128(&left.M31), r));
            }
            else
            {
                result.M11 = left.M11 * right;
                result.M12 = left.M12 * right;
                result.M13 = left.M13 * right;
                result.M21 = left.M21 * right;
                result.M22 = left.M22 * right;
                result.M23 = left.M23 * right;
                result.M31 = left.M31 * right;
                result.M32 = left.M32 * right;
            }
            result.M33 = left.M33 * right;
            return result;
        }
        private static bool Equal(Vector256<double> left, Vector256<double> right) =>
            Avx.MoveMask(Avx.CompareNotEqual(left, right)) == 0;
        private static bool Equal(Vector128<double> left, Vector128<double> right) =>
            Sse2.MoveMask(Sse2.CompareNotEqual(left, right)) == 0;
        public static unsafe bool operator ==(Matrix3x3D left, Matrix3x3D right)
        {
#if DEBUG
            if (Avx.IsSupported && AllowAvx)
#else
            if (Avx.IsSupported)
#endif
                return Equal(Avx.LoadVector256(&left.M11), Avx.LoadVector256(&right.M11)) &&
                       Equal(Avx.LoadVector256(&left.M22), Avx.LoadVector256(&right.M22)) &&
                       left.M33 == right.M33;
#if DEBUG
            else if (Sse2.IsSupported && AllowSse)
#else
            else if (Sse2.IsSupported)
#endif
                return Equal(Sse2.LoadVector128(&left.M11), Sse2.LoadVector128(&right.M11)) &&
                       Equal(Sse2.LoadVector128(&left.M13), Sse2.LoadVector128(&right.M13)) &&
                       Equal(Sse2.LoadVector128(&left.M22), Sse2.LoadVector128(&right.M22)) &&
                       Equal(Sse2.LoadVector128(&left.M31), Sse2.LoadVector128(&right.M31)) &&
                       Equal(Sse2.LoadScalarVector128(&left.M33), Sse2.LoadScalarVector128(&right.M33));
            else
                return left.M11 == right.M11 && left.M12 == right.M12 && left.M13 == right.M13 &&
                       left.M21 == right.M21 && left.M22 == right.M22 && left.M23 == right.M23 &&
                       left.M31 == right.M31 && left.M32 == right.M32 && left.M33 == right.M33;
        }
        public static bool operator !=(Matrix3x3D left, Matrix3x3D right) =>
            !(left == right);
        
        public bool Equals([AllowNull] Matrix3x3D other) =>
            this == other;

        public override bool Equals(object? obj) =>
            obj is Matrix3x3D matrix && this == matrix;

        public override string ToString() =>
            $"{{{{ {{{{M11:{M11} M12:{M12} M13:{M13}}}}} {{{{M21:{M21} M22:{M22} M23:{M23}}}}} {{{{M31:{M31} M32:{M32} M33:{M33}}}}} }}}}";

        public override int GetHashCode() =>
            M11.GetHashCode() + M12.GetHashCode() + M13.GetHashCode() +
            M21.GetHashCode() + M22.GetHashCode() + M23.GetHashCode() +
            M31.GetHashCode() + M32.GetHashCode() + M33.GetHashCode();
    }
}
