using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace MathStructs
{
    public struct Matrix3x3F : IEquatable<Matrix3x3F>
    {
#if DEBUG
        public static bool AllowAvx = true;
        public static bool AllowSse = true;
#endif
        public float M11;
        public float M12;
        public float M13;
        public float M21;
        public float M22;
        public float M23;
        public float M31;
        public float M32;
        public float M33;
        private static readonly Matrix3x3F _identity = new Matrix3x3F(1, 0, 0, 0, 1, 0, 0, 0, 1);
        public static Matrix3x3F Identity => _identity;
        public readonly bool IsIdentity =>
            M11 is 1.0f &&
            M12 is 0.0f &&
            M13 is 0.0f &&
            M21 is 0.0f &&
            M22 is 1.0f &&
            M23 is 0.0f &&
            M31 is 0.0f &&
            M32 is 0.0f &&
            M33 is 1.0f;
        public Matrix3x3F(float m11, float m12, float m13, float m21, float m22, float m23, float m31, float m32, float m33)
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
        }
        public static unsafe Matrix3x3F operator -(Matrix3x3F value)
        {
            var result = new Matrix3x3F();
#if DEBUG
            if (Avx.IsSupported && AllowAvx)
#else
            if (Avx.IsSupported)
#endif
            {
                var zero = Vector256<float>.Zero;

                Avx.Store(&result.M11, Avx.Subtract(zero, Avx.LoadVector256(&value.M11)));
            }
#if DEBUG
            else if (Sse.IsSupported && AllowSse)
#else
            else if (Sse.IsSupported)
#endif
            {
                var zero = Vector128<float>.Zero;

                Sse.Store(&result.M11, Sse.Subtract(zero, Sse.LoadVector128(&value.M11)));
                Sse.Store(&result.M22, Sse.Subtract(zero, Sse.LoadVector128(&value.M22)));
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
        public static Matrix3x3F operator +(Matrix3x3F value) =>
            value;
        public static unsafe Matrix3x3F operator +(Matrix3x3F left, Matrix3x3F right)
        {
            var result = new Matrix3x3F();

#if DEBUG
            if (Avx.IsSupported && AllowAvx)
#else
            if (Avx.IsSupported)
#endif
                Avx.Store(&result.M11, Avx.Add(Avx.LoadVector256(&left.M11), Avx.LoadVector256(&right.M11)));

#if DEBUG
            else if (Sse.IsSupported && AllowSse)
#else
            else if (Sse.IsSupported)
#endif
            {
                Sse.Store(&result.M11, Sse.Add(Sse.LoadVector128(&left.M11), Sse.LoadVector128(&right.M11)));
                Sse.Store(&result.M22, Sse.Add(Sse.LoadVector128(&left.M22), Sse.LoadVector128(&right.M22)));
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
        public static unsafe Matrix3x3F operator -(Matrix3x3F left, Matrix3x3F right)
        {
            var result = new Matrix3x3F();

#if DEBUG
            if (Avx.IsSupported && AllowAvx)
#else
            if (Avx.IsSupported)
#endif
                Avx.Store(&result.M11, Avx.Subtract(Avx.LoadVector256(&left.M11), Avx.LoadVector256(&right.M11)));

#if DEBUG
            else if (Sse.IsSupported && AllowSse)
#else
            else if (Sse.IsSupported)
#endif
            {
                Sse.Store(&result.M11, Sse.Subtract(Sse.LoadVector128(&left.M11), Sse.LoadVector128(&right.M11)));
                Sse.Store(&result.M22, Sse.Subtract(Sse.LoadVector128(&left.M22), Sse.LoadVector128(&right.M22)));
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
        public static unsafe Matrix3x3F operator *(Matrix3x3F left, Matrix3x3F right)
        {
            var result = new Matrix3x3F();

#if DEBUG
            if (Sse.IsSupported && AllowSse)
#else
            if (Sse.IsSupported)
#endif
            {
                var vector = Sse.LoadVector128(&left.M11);
                Sse.Store(&result.M11, Sse.Add(Sse.Add(Sse.Multiply(Sse.Shuffle(vector, vector, 0),
                                                                    Sse.LoadVector128(&right.M11)),
                                                       Sse.Multiply(Sse.Shuffle(vector, vector, 85),
                                                                    Sse.LoadVector128(&right.M21))),
                                               Sse.Multiply(Sse.Shuffle(vector, vector, 170),
                                                            Sse.LoadVector128(&right.M31))));
                vector = Sse.LoadVector128(&left.M21);
                Sse.Store(&result.M21, Sse.Add(Sse.Add(Sse.Multiply(Sse.Shuffle(vector, vector, 0),
                                                                    Sse.LoadVector128(&right.M11)),
                                                       Sse.Multiply(Sse.Shuffle(vector, vector, 85),
                                                                    Sse.LoadVector128(&right.M21))),
                                               Sse.Multiply(Sse.Shuffle(vector, vector, 170),
                                                            Sse.LoadVector128(&right.M31))));
                vector = Sse.LoadVector128(&left.M31);
                vector = Sse.Add(Sse.Add(Sse.Multiply(Sse.Shuffle(vector, vector, 0),
                                                      Sse.LoadVector128(&right.M11)),
                                         Sse.Multiply(Sse.Shuffle(vector, vector, 85),
                                                      Sse.LoadVector128(&right.M21))),
                                 Sse.Multiply(Sse.Shuffle(vector, vector, 170),
                                              Sse.LoadVector128(&right.M31)));
                Sse.StoreLow(&result.M31, vector);
                Sse.StoreScalar(&result.M33, Sse.Shuffle(vector, vector, 170));
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
        public static unsafe Matrix3x3F operator *(Matrix3x3F left, float right)
        {
            var result = new Matrix3x3F();

#if DEBUG
            if (Avx.IsSupported && AllowAvx)
#else
            if (Avx.IsSupported)
#endif
                Avx.Store(&result.M11, Avx.Multiply(Avx.LoadVector256(&left.M11), Vector256.Create(right)));

#if DEBUG
            else if (Sse.IsSupported && AllowSse)
#else
            else if (Sse.IsSupported)
#endif
            {
                var r = Vector128.Create(right);
                Sse.Store(&result.M11, Sse.Multiply(Sse.LoadVector128(&left.M11), r));
                Sse.Store(&result.M22, Sse.Multiply(Sse.LoadVector128(&left.M22), r));
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
        private static bool Equal(Vector256<float> left, Vector256<float> right) =>
            Avx.MoveMask(Avx.CompareNotEqual(left, right)) == 0;
        private static bool Equal(Vector128<float> left, Vector128<float> right) =>
            Sse.MoveMask(Sse.CompareNotEqual(left, right)) == 0;
        public static unsafe bool operator ==(Matrix3x3F left, Matrix3x3F right)
        {
#if DEBUG
            if (Avx.IsSupported && AllowAvx)
#else
            if (Avx.IsSupported)
#endif
                return Equal(Avx.LoadVector256(&left.M11), Avx.LoadVector256(&right.M11)) &&
                       left.M33 == right.M33;
#if DEBUG
            else if (Sse.IsSupported && AllowSse)
#else
            else if (Sse.IsSupported)
#endif
                return Equal(Sse.LoadVector128(&left.M11), Sse.LoadVector128(&right.M11)) &&
                       Equal(Sse.LoadVector128(&left.M22), Sse.LoadVector128(&right.M22)) &&
                       left.M33 == right.M33;
            else
                return left.M11 == right.M11 && left.M12 == right.M12 && left.M13 == right.M13 &&
                       left.M21 == right.M21 && left.M22 == right.M22 && left.M23 == right.M23 &&
                       left.M31 == right.M31 && left.M32 == right.M32 && left.M33 == right.M33;
        }
        public static bool operator !=(Matrix3x3F left, Matrix3x3F right) =>
            !(left == right);

        public bool Equals([AllowNull] Matrix3x3F other) =>
            this == other;

        public override bool Equals(object? obj) =>
            obj is Matrix3x3F matrix && this == matrix;

        public override string ToString() =>
            $"{{{{ {{{{M11:{M11} M12:{M12} M13:{M13}}}}} {{{{M21:{M21} M22:{M22} M23:{M23}}}}} {{{{M31:{M31} M32:{M32} M33:{M33}}}}} }}}}";

        public override int GetHashCode() =>
            M11.GetHashCode() + M12.GetHashCode() + M13.GetHashCode() +
            M21.GetHashCode() + M22.GetHashCode() + M23.GetHashCode() +
            M31.GetHashCode() + M32.GetHashCode() + M33.GetHashCode();
    }
}
