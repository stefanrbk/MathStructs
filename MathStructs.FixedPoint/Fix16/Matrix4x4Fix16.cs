using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;

namespace MathStructs
{
    /// <summary>
    /// A structure encapsulating a 4x4 matrix of <see cref="int"/> values.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Pack = 4)]
    public readonly struct Matrix4x4Fix16
    {
        #region Public Fields

        /// <summary>
        /// Value at row 1, column 1 of the matrix.
        /// </summary>
        [FieldOffset(0)]
        public readonly int M11;

        /// <summary>
        /// Value at row 1, column 2 of the matrix.
        /// </summary>
        [FieldOffset(4)]
        public readonly int M12;

        /// <summary>
        /// Value at row 1, column 3 of the matrix.
        /// </summary>
        [FieldOffset(8)]
        public readonly int M13;

        /// <summary>
        /// Value at row 1, column 4 of the matrix.
        /// </summary>
        [FieldOffset(12)]
        public readonly int M14;

        /// <summary>
        /// Value at row 2, column 1 of the matrix.
        /// </summary>
        [FieldOffset(16)]
        public readonly int M21;

        /// <summary>
        /// Value at row 2, column 2 of the matrix.
        /// </summary>
        [FieldOffset(20)]
        public readonly int M22;
        /// <summary>
        /// Value at row 2, column 3 of the matrix.
        /// </summary>
        [FieldOffset(24)]
        public readonly int M23;

        /// <summary>
        /// Value at row 2, column 4 of the matrix.
        /// </summary>
        [FieldOffset(28)]
        public readonly int M24;

        /// <summary>
        /// Value at row 3, column 1 of the matrix.
        /// </summary>
        [FieldOffset(32)]
        public readonly int M31;

        /// <summary>
        /// Value at row 3, column 2 of the matrix.
        /// </summary>
        [FieldOffset(36)]
        public readonly int M32;

        /// <summary>
        /// Value at row 3, column 3 of the matrix.
        /// </summary>
        [FieldOffset(40)]
        public readonly int M33;

        /// <summary>
        /// Value at row 3, column 4 of the matrix.
        /// </summary>
        [FieldOffset(44)]
        public readonly int M34;

        /// <summary>
        /// Value at row 4, column 1 of the matrix.
        /// </summary>
        [FieldOffset(48)]
        public readonly int M41;

        /// <summary>
        /// Value at row 4, column 2 of the matrix.
        /// </summary>
        [FieldOffset(52)]
        public readonly int M42;

        /// <summary>
        /// Value at row 4, column 3 of the matrix.
        /// </summary>
        [FieldOffset(56)]
        public readonly int M43;

        /// <summary>
        /// Value at row 4, column 4 of the matrix.
        /// </summary>
        [FieldOffset(60)]
        public readonly int M44;

        #endregion Public Fields

        #region Private Fields

        private const MethodImplOptions Inline = MethodImplOptions.AggressiveInlining;
        private const MethodImplOptions Optimize = Inline | MethodImplOptions.AggressiveOptimization;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Constructs a <see cref="Matrix4x4Fix16"/> from the given components.
        /// </summary>
        [MethodImpl(Optimize)]
        public Matrix4x4Fix16(int m11, int m12, int m13, int m14,
                                   int m21, int m22, int m23, int m24,
                                   int m31, int m32, int m33, int m34,
                                   int m41, int m42, int m43, int m44)
        {
            M11 = m11;
            M12 = m12;
            M13 = m13;
            M14 = m14;
            M21 = m21;
            M22 = m22;
            M23 = m23;
            M24 = m24;
            M31 = m31;
            M32 = m32;
            M33 = m33;
            M34 = m34;
            M41 = m41;
            M42 = m42;
            M43 = m43;
            M44 = m44;
        }

        /// <summary>
        /// Constructs a <see cref="Matrix4x4Fix16"/> from the given components.
        /// </summary>
        [MethodImpl(Optimize)]
        public Matrix4x4Fix16(double m11, double m12, double m13, double m14,
                                   double m21, double m22, double m23, double m24,
                                   double m31, double m32, double m33, double m34,
                                   double m41, double m42, double m43, double m44)
            : this((int)m11, (int)m12, (int)m13, (int)m14,
                   (int)m21, (int)m22, (int)m23, (int)m24,
                   (int)m31, (int)m32, (int)m33, (int)m34,
                   (int)m41, (int)m42, (int)m43, (int)m44) { }

        /// <summary>
        /// Constructs a Matrix4x4D from the given <see cref="Matrix3x3F"/>.
        /// </summary>
        /// <param name="value">
        /// The source <see cref="Matrix3x3F"/>.
        /// </param>
        [MethodImpl(Optimize)]
        public Matrix4x4Fix16(Matrix3x3Fix16 value)
        {
            M11 = value.M11;
            M12 = value.M12;
            M13 = value.M13;
            M14 = 0;
            M21 = value.M21;
            M22 = value.M22;
            M23 = value.M23;
            M24 = 0;
            M31 = value.M31;
            M32 = value.M32;
            M33 = value.M33;
            M34 = 0;
            M41 = 0;
            M42 = 0;
            M43 = 0;
            M44 = 1;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Returns the multiplicative identity matrix.
        /// </summary>
        public static Matrix4x4Fix16 Identity => new Matrix4x4Fix16(1f, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f);

        /// <summary>
        /// Returns a matrix with all values set to NaN.
        /// </summary>
        public static Matrix4x4Fix16 Zero => new Matrix4x4Fix16(0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0);

        /// <summary>
        /// Returns whether the matrix is the identity matrix.
        /// </summary>
        public bool IsIdentity =>
            this == Identity;

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Adds two matrices together.
        /// </summary>
        /// <param name="left">
        /// The first source matrix.
        /// </param>
        /// <param name="right">
        /// The second source matrix.
        /// </param>
        [MethodImpl(Optimize)]
        public static Matrix4x4Fix16 Add(Matrix4x4Fix16 left, Matrix4x4Fix16 right) =>
            left + right;

        /// <summary>
        ///     Copies the contents of the matrix into the given span.
        /// </summary>
        [MethodImpl(Optimize)]
        public void CopyTo(Span<int> span) =>
            CopyTo(span, 0);

        /// <summary>
        ///     Copies the contents of the matrix into the given span, starting from index.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     If index is greater than end of the span or index is less than zero.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     If number of elements in source matrix is greater than those available in destination span.
        /// </exception>
        [MethodImpl(Optimize)]
        public void CopyTo(Span<int> span, int index)
        {
            if (index < 0 || index >= span.Length)
                throw new ArgumentOutOfRangeException(nameof(index));
            if (span.Length - index < 16)
                throw new ArgumentException("Elements in source is greater than destination");
            span[index +  0] = M11;
            span[index +  1] = M12;
            span[index +  2] = M13;
            span[index +  3] = M14;
            span[index +  4] = M21;
            span[index +  5] = M22;
            span[index +  6] = M23;
            span[index +  7] = M24;
            span[index +  8] = M31;
            span[index +  9] = M32;
            span[index + 10] = M33;
            span[index + 11] = M34;
            span[index + 12] = M41;
            span[index + 13] = M42;
            span[index + 14] = M43;
            span[index + 15] = M44;
        }

        private unsafe delegate Vector128<T> LoadFunc<T>(T* address) where T:unmanaged;
        private unsafe delegate void StoreFunc<T>(T* address, Vector128<T> vector) where T : unmanaged;

        /// <summary>
        /// Attempts to calculate the inverse of the given matrix. If successful, the result will contain the inverted matrix.
        /// </summary>
        /// <param name="matrix">
        /// The source matrix to invert.
        /// </param>
        /// <returns>
        /// If successful, the inverted matrix; NaN matrix otherwise.
        /// </returns>
        [MethodImpl(Optimize)]
        public static Matrix4x4Fix16 Invert(Matrix4x4Fix16 matrix)
        {
            // This implementation is based on the DirectX Math Library XMMatrixInverse method
            // https://github.com/microsoft/DirectXMath/blob/master/Inc/DirectXMathMatrix.inl

            //if (Sse41.IsSupported)
            //    return SseImpl(matrix);

            return SoftwareFallback(matrix);

            //static unsafe Matrix4x4Fix16 SseImpl(Matrix4x4Fix16 matrix)
            //{
            //    if (!Sse41.IsSupported)
            //        // Redundant test so we won't preJIT remainder of this method on platforms without SSE.
            //        throw new PlatformNotSupportedException();

            //    LoadFunc<int> Load = Sse2.LoadVector128;
            //    Func<Vector128<int>,Vector128<int>, Vector128<int>> Mul = Sse41.MultiplyLow;
            //    Func<Vector128<int>,Vector128<int>, Vector128<int>> Sub = Sse2.Subtract;
            //    Func<Vector128<int>,Vector128<int>, Vector128<int>> Add = Sse2.Add;
            //    Func<Vector128<float>,Vector128<float>,byte, Vector128<float>> Shuf = Sse.Shuffle;

            //    // Load the matrix values into rows
            //    var row1 = Load((int*)&matrix.M11).AsSingle();
            //    var row2 = Load((int*)&matrix.M21).AsSingle();
            //    var row3 = Load((int*)&matrix.M31).AsSingle();
            //    var row4 = Load((int*)&matrix.M41).AsSingle();

            //    // Transpose the matrix
            //    var vTemp1 = Shuf(row1, row2, 0x44); //_MM_SHUFFLE(1, 0, 1, 0)
            //    var vTemp3 = Shuf(row1, row2, 0xEE); //_MM_SHUFFLE(3, 2, 3, 2)
            //    var vTemp2 = Shuf(row3, row4, 0x44); //_MM_SHUFFLE(1, 0, 1, 0)
            //    var vTemp4 = Shuf(row3, row4, 0xEE); //_MM_SHUFFLE(3, 2, 3, 2)

            //    row1 = Shuf(vTemp1, vTemp2, 0x88); //_MM_SHUFFLE(2, 0, 2, 0)
            //    row2 = Shuf(vTemp1, vTemp2, 0xDD); //_MM_SHUFFLE(3, 1, 3, 1)
            //    row3 = Shuf(vTemp3, vTemp4, 0x88); //_MM_SHUFFLE(2, 0, 2, 0)
            //    row4 = Shuf(vTemp3, vTemp4, 0xDD); //_MM_SHUFFLE(3, 1, 3, 1)

            //    var V00 = Permute(row3, 0x50);           //_MM_SHUFFLE(1, 1, 0, 0)
            //    var V10 = Permute(row4, 0xEE);           //_MM_SHUFFLE(3, 2, 3, 2)
            //    var V01 = Permute(row1, 0x50);           //_MM_SHUFFLE(1, 1, 0, 0)
            //    var V11 = Permute(row2, 0xEE);           //_MM_SHUFFLE(3, 2, 3, 2)
            //    var V02 = Shuf(row3, row1, 0x88); //_MM_SHUFFLE(2, 0, 2, 0)
            //    var V12 = Shuf(row4, row2, 0xDD); //_MM_SHUFFLE(3, 1, 3, 1)

            //    var D0 = Mul(V00.AsInt32(), V10.AsInt32());
            //    var D1 = Mul(V01.AsInt32(), V11.AsInt32());
            //    var D2 = Mul(V02.AsInt32(), V12.AsInt32());

            //    V00 = Permute(row3, 0xEE);           //_MM_SHUFFLE(3, 2, 3, 2)
            //    V10 = Permute(row4, 0x50);           //_MM_SHUFFLE(1, 1, 0, 0)
            //    V01 = Permute(row1, 0xEE);           //_MM_SHUFFLE(3, 2, 3, 2)
            //    V11 = Permute(row2, 0x50);           //_MM_SHUFFLE(1, 1, 0, 0)
            //    V02 = Shuf(row3, row1, 0xDD); //_MM_SHUFFLE(3, 1, 3, 1)
            //    V12 = Shuf(row4, row2, 0x88); //_MM_SHUFFLE(2, 0, 2, 0)

            //    // Note: We use this expansion pattern instead of Fused Multiply Add
            //    // in order to support older hardware
            //    D0 = Sub(D0, Mul(V00.AsInt32(), V10.AsInt32()));
            //    D1 = Sub(D1, Mul(V01.AsInt32(), V11.AsInt32()));
            //    D2 = Sub(D2, Mul(V02.AsInt32(), V12.AsInt32()));

            //    // V11 = D0Y,D0W,D2Y,D2Y
            //    V11 = Shuf(D0.AsSingle(), D2.AsSingle(), 0x5D);  //_MM_SHUFFLE(1, 1, 3, 1)
            //    V00 = Permute(row2, 0x49);        //_MM_SHUFFLE(1, 0, 2, 1)
            //    V10 = Shuf(V11, D0.AsSingle(), 0x32); //_MM_SHUFFLE(0, 3, 0, 2)
            //    V01 = Permute(row1, 0x12);        //_MM_SHUFFLE(0, 1, 0, 2)
            //    V11 = Shuf(V11, D0.AsSingle(), 0x99); //_MM_SHUFFLE(2, 1, 2, 1)

            //    // V13 = D1Y,D1W,D2W,D2W
            //    var V13 = Shuf(D1.AsSingle(), D2.AsSingle(), 0xFD); //_MM_SHUFFLE(3, 3, 3, 1)
            //    V02 = Permute(row4, 0x49);           //_MM_SHUFFLE(1, 0, 2, 1)
            //    V12 = Shuf(V13, D1.AsSingle(), 0x32);    //_MM_SHUFFLE(0, 3, 0, 2)
            //    var V03 = Permute(row3, 0x12);       //_MM_SHUFFLE(0, 1, 0, 2)
            //    V13 = Shuf(V13, D1.AsSingle(), 153);     //_MM_SHUFFLE(2, 1, 2, 1)

            //    var C0 = Mul(V00.AsInt32(), V10.AsInt32());
            //    var C2 = Mul(V01.AsInt32(), V11.AsInt32());
            //    var C4 = Mul(V02.AsInt32(), V12.AsInt32());
            //    var C6 = Mul(V03.AsInt32(), V13.AsInt32());

            //    // V11 = D0X,D0Y,D2X,D2X
            //    V11 = Shuf(D0.AsSingle(), D2.AsSingle(), 0x04);  //_MM_SHUFFLE(0, 0, 1, 0)
            //    V00 = Permute(row2, 0x9E);        //_MM_SHUFFLE(2, 1, 3, 2)
            //    V10 = Shuf(D0.AsSingle(), V11, 0x93); //_MM_SHUFFLE(2, 1, 0, 3)
            //    V01 = Permute(row1, 0x7B);        //_MM_SHUFFLE(1, 3, 2, 3)
            //    V11 = Shuf(D0.AsSingle(), V11, 0x26); //_MM_SHUFFLE(0, 2, 1, 2)

            //    // V13 = D1X,D1Y,D2Z,D2Z
            //    V13 = Shuf(D1.AsSingle(), D2.AsSingle(), 0xA4);  //_MM_SHUFFLE(2, 2, 1, 0)
            //    V02 = Permute(row4, 0x9E);        //_MM_SHUFFLE(2, 1, 3, 2)
            //    V12 = Shuf(D1.AsSingle(), V13, 0x93); //_MM_SHUFFLE(2, 1, 0, 3)
            //    V03 = Permute(row3, 0x7B);        //_MM_SHUFFLE(1, 3, 2, 3)
            //    V13 = Shuf(D1.AsSingle(), V13, 0x26); //_MM_SHUFFLE(0, 2, 1, 2)

            //    C0 = Sub(C0, Mul(V00.AsInt32(), V10.AsInt32()));
            //    C2 = Sub(C2, Mul(V01.AsInt32(), V11.AsInt32()));
            //    C4 = Sub(C4, Mul(V02.AsInt32(), V12.AsInt32()));
            //    C6 = Sub(C6, Mul(V03.AsInt32(), V13.AsInt32()));

            //    V00 = Permute(row2, 0x33); //_MM_SHUFFLE(0, 3, 0, 3)

            //    // V10 = D0Z,D0Z,D2X,D2Y
            //    V10 = Shuf(D0.AsSingle(), D2.AsSingle(), 0x4A); //_MM_SHUFFLE(1, 0, 2, 2)
            //    V10 = Permute(V10, 0x2C);        //_MM_SHUFFLE(0, 2, 3, 0)
            //    V01 = Permute(row1, 0x8D);       //_MM_SHUFFLE(2, 0, 3, 1)

            //    // V11 = D0X,D0W,D2X,D2Y
            //    V11 = Shuf(D0.AsSingle(), D2.AsSingle(), 0x4C); //_MM_SHUFFLE(1, 0, 3, 0)
            //    V11 = Permute(V11, 0x93);        //_MM_SHUFFLE(2, 1, 0, 3)
            //    V02 = Permute(row4, 0x33);       //_MM_SHUFFLE(0, 3, 0, 3)

            //    // V12 = D1Z,D1Z,D2Z,D2W
            //    V12 = Shuf(D1.AsSingle(), D2.AsSingle(), 0xEA); //_MM_SHUFFLE(3, 2, 2, 2)
            //    V12 = Permute(V12, 0x2C);        //_MM_SHUFFLE(0, 2, 3, 0)
            //    V03 = Permute(row3, 0x8D);       //_MM_SHUFFLE(2, 0, 3, 1)

            //    // V13 = D1X,D1W,D2Z,D2W
            //    V13 = Shuf(D1.AsSingle(), D2.AsSingle(), 0xEC); //_MM_SHUFFLE(3, 2, 3, 0)
            //    V13 = Permute(V13, 0x93);        //_MM_SHUFFLE(2, 1, 0, 3)

            //    V00 = Mul(V00.AsInt32(), V10.AsInt32()).AsSingle();
            //    V01 = Mul(V01.AsInt32(), V11.AsInt32()).AsSingle();
            //    V02 = Mul(V02.AsInt32(), V12.AsInt32()).AsSingle();
            //    V03 = Mul(V03.AsInt32(), V13.AsInt32()).AsSingle();

            //    var C1 = Sub(C0, V00.AsInt32());
            //    C0 = Add(C0, V00.AsInt32());
            //    var C3 = Add(C2, V01.AsInt32());
            //    C2 = Sub(C2, V01.AsInt32());
            //    var C5 = Sub(C4, V02.AsInt32());
            //    C4 = Add(C4, V02.AsInt32());
            //    var C7 = Add(C6, V03.AsInt32());
            //    C6 = Sub(C6, V03.AsInt32());

            //    C0 = Shuf(C0.AsSingle(), C1.AsSingle(), 0xD8).AsInt32(); //_MM_SHUFFLE(3, 1, 2, 0)
            //    C2 = Shuf(C2.AsSingle(), C3.AsSingle(), 0xD8).AsInt32(); //_MM_SHUFFLE(3, 1, 2, 0)
            //    C4 = Shuf(C4.AsSingle(), C5.AsSingle(), 0xD8).AsInt32(); //_MM_SHUFFLE(3, 1, 2, 0)
            //    C6 = Shuf(C6.AsSingle(), C7.AsSingle(), 0xD8).AsInt32(); //_MM_SHUFFLE(3, 1, 2, 0)

            //    C0 = Permute(C0.AsSingle(), 0xD8).AsInt32(); //_MM_SHUFFLE(3, 1, 2, 0)
            //    C2 = Permute(C2.AsSingle(), 0xD8).AsInt32(); //_MM_SHUFFLE(3, 1, 2, 0)
            //    C4 = Permute(C4.AsSingle(), 0xD8).AsInt32(); //_MM_SHUFFLE(3, 1, 2, 0)
            //    C6 = Permute(C6.AsSingle(), 0xD8).AsInt32(); //_MM_SHUFFLE(3, 1, 2, 0)

            //    // Get the determinant
            //    vTemp2 = row1;
            //    var det = Vector4Fix16.Dot(C0.AsVector4Fix16(), vTemp2.AsVector4Fix16());

            //    // Check determinate is not zero
            //    if (det == Fix16.Zero)
            //        return Zero;

            //    // Create Vector128<Fix16> copy of the determinant and invert them.
            //    var ones = Vector128.Create(1);
            //    var vTemp = Vector128.Create(Unsafe.As<Fix16, int>(ref det));
            //    vTemp = Divide(ones, vTemp);

            //    row1 = Mul(C0, vTemp).AsSingle();
            //    row2 = Mul(C2, vTemp).AsSingle();
            //    row3 = Mul(C4, vTemp).AsSingle();
            //    row4 = Mul(C6, vTemp).AsSingle();

            //    Unsafe.SkipInit<Matrix4x4Fix16>(out var result);
            //    ref var vResult = ref Unsafe.As<Matrix4x4Fix16, Vector128<float>>(ref result);

            //    vResult = row1;
            //    Unsafe.Add(ref vResult, 1) = row2;
            //    Unsafe.Add(ref vResult, 2) = row3;
            //    Unsafe.Add(ref vResult, 3) = row4;

            //    return result;
            //}

            static Matrix4x4Fix16 SoftwareFallback(Matrix4x4Fix16 matrix)
            {
                /*
                 * If you have matrix M, inverse Matrix M⁻¹ can compute
                 *
                 *              1
                 *    M⁻¹ = ⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯ A
                 *            det(M)
                 *
                 * A is adjugate (adjoint) of M, where,
                 *
                 *
                 * A = Cᵀ
                 *
                 * C is Cofactor matrix of M, where,
                 *
                 * Cᵢⱼ = ( -1 ) ^ ( i + j ) ∙ det(Mᵢⱼ)
                 *
                 *
                 *     ⎡ a b c d ⎤
                 * M = ⎢ e f g h ⎥
                 *     ⎢ i j k l ⎥
                 *     ⎣ m n o p ⎦
                 *
                 * First Row
                 *             ⎡ f g h ⎤
                 * C₁₁ = (-1)² ⎢ j k l ⎥ = + ( f ( kp - lo ) - g ( jp - ln ) + h ( jo - kn ) )
                 *             ⎣ n o p ⎦
                 *
                 *             ⎡ e g h ⎤
                 * C₁₂ = (-1)³ ⎢ i k l ⎥ = - ( e ( kp - lo ) - g ( ip - lm ) + h ( io - km ) )
                 *             ⎣ m o p ⎦
                 *
                 *             ⎡ e f h ⎤
                 * C₁₃ = (-1)⁴ ⎢ i j l ⎥ = + ( e ( jp - ln ) - f ( ip - lm ) + h ( in - jm ) )
                 *             ⎣ m n p ⎦
                 *
                 *             ⎡ e f g ⎤
                 * C₁₄ = (-1)⁵ ⎢ i j k ⎥ = - ( e ( jo - kn ) - f ( io - km ) + g ( in - jm ) )
                 *             ⎣ m n o ⎦
                 *
                 * Second Row
                 *             ⎡ b c d ⎤
                 * C₂₁ = (-1)³ ⎢ j k l ⎥ = - ( b ( kp - lo ) - c ( jp - ln ) + d ( jo - kn ) )
                 *             ⎣ n o p ⎦
                 *
                 *             ⎡ a c d ⎤
                 * C₂₂ = (-1)⁴ ⎢ i k l ⎥ = + ( a ( kp - lo ) - c ( ip - lm ) + d ( io - km ) )
                 *             ⎣ m o p ⎦
                 *
                 *             ⎡ a b d ⎤
                 * C₂₃ = (-1)⁵ ⎢ i j l ⎥ = - ( a ( jp - ln ) - b ( ip - lm ) + d ( in - jm ) )
                 *             ⎣ m n p ⎦
                 *
                 *             ⎡ a b c ⎤
                 * C₂₄ = (-1)⁶ ⎢ i j k ⎥ = + ( a ( jo - kn ) - b ( io - km ) + c ( in - jm ) )
                 *             ⎣ m n o ⎦
                 *
                 * Third Row
                 *             ⎡ b c d ⎤
                 * C₃₁ = (-1)⁴ ⎢ f g h ⎥ = + ( b ( gp - ho ) - c ( fp - hn ) + d ( fo - gn ) )
                 *             ⎣ n o p ⎦
                 *
                 *             ⎡ a c d ⎤
                 * C₃₂ = (-1)⁵ ⎢ e g h ⎥ = - ( a ( gp - ho ) - c ( ep - hm ) + d ( eo - gm ) )
                 *             ⎣ m o p ⎦
                 *
                 *             ⎡ a b d ⎤
                 * C₃₃ = (-1)⁶ ⎢ e f h ⎥ = + ( a ( fp - hn ) - b ( ep - hm ) + d ( en - fm ) )
                 *             ⎣ m n p ⎦
                 *
                 *             ⎡ a b c ⎤
                 * C₃₄ = (-1)⁷ ⎢ e f g ⎥ = - ( a ( fo - gn ) - b ( eo - gm ) + c ( en - fm ) )
                 *             ⎣ m n o ⎦
                 *
                 * Fourth Row
                 *             ⎡ b c d ⎤
                 * C₄₁ = (-1)⁵ ⎢ f g h ⎥ = - ( b ( gl - hk ) - c ( fl - hj ) + d ( fk - gj ) )
                 *             ⎣ j k l ⎦
                 *
                 *             ⎡ a c d ⎤
                 * C₄₂ = (-1)⁶ ⎢ e g h ⎥ = + ( a ( gl - hk ) - c ( el - hi ) + d ( ek - gi ) )
                 *             ⎣ i k l ⎦
                 *
                 *             ⎡ a b d ⎤
                 * C₄₃ = (-1)⁷ ⎢ e f h ⎥ = - ( a ( fl - hj ) - b ( el - hi ) + d ( ej - fi ) )
                 *             ⎣ i j l ⎦
                 *
                 *             ⎡ a b c ⎤
                 * C₄₄ = (-1)⁸ ⎢ e f g ⎥ = + ( a ( fk - gj ) - b ( ek - gi ) + c ( ej - fi ) )
                 *             ⎣ i j k ⎦
                 *
                 * Cost of operation
                 * 53 adds, 104 muls, and 1 div.
                 */
                (var a, var b, var c, var d) = (matrix.M11, matrix.M12, matrix.M13, matrix.M14);
                (var e, var f, var g, var h) = (matrix.M21, matrix.M22, matrix.M23, matrix.M24);
                (var i, var j, var k, var l) = (matrix.M31, matrix.M32, matrix.M33, matrix.M34);
                (var m, var n, var o, var p) = (matrix.M41, matrix.M42, matrix.M43, matrix.M44);

                var kp_lo = ( k * p ) - ( l * o );
                var jp_ln = ( j * p ) - ( l * n );
                var jo_kn = ( j * o ) - ( k * n );
                var ip_lm = ( i * p ) - ( l * m );
                var io_km = ( i * o ) - ( k * m );
                var in_jm = ( i * n ) - ( j * m );

                var a11 =    ( f * kp_lo ) - ( g * jp_ln ) + ( h * jo_kn );
                var a12 = -( ( e * kp_lo ) - ( g * ip_lm ) + ( h * io_km ) );
                var a13 =    ( e * jp_ln ) - ( f * ip_lm ) + ( h * in_jm );
                var a14 = -( ( e * jo_kn ) - ( f * io_km ) + ( g * in_jm ) );

                var det = ( a * a11 ) + ( b * a12 ) + ( c * a13 ) + ( d * a14 );

                if (det == 0)
                    return Zero;

                var invDet = 1 / det;

                var gp_ho = ( g * p ) - ( h * o );
                var fp_hn = ( f * p ) - ( h * n );
                var fo_gn = ( f * o ) - ( g * n );
                var ep_hm = ( e * p ) - ( h * m );
                var eo_gm = ( e * o ) - ( g * m );
                var en_fm = ( e * n ) - ( f * m );

                var gl_hk = ( g * l ) - ( h * k );
                var fl_hj = ( f * l ) - ( h * j );
                var fk_gj = ( f * k ) - ( g * j );
                var el_hi = ( e * l ) - ( h * i );
                var ek_gi = ( e * k ) - ( g * i );
                var ej_fi = ( e * j ) - ( f * i );
                return new Matrix4x4Fix16(m11: a11 * invDet,
                                          m21: a12 * invDet,
                                          m31: a13 * invDet,
                                          m41: a14 * invDet,
                                          m12: -( ( b * kp_lo ) - ( c * jp_ln ) + ( d * jo_kn ) ) * invDet,
                                          m22:  ( ( a * kp_lo ) - ( c * ip_lm ) + ( d * io_km ) ) * invDet,
                                          m32: -( ( a * jp_ln ) - ( b * ip_lm ) + ( d * in_jm ) ) * invDet,
                                          m42:  ( ( a * jo_kn ) - ( b * io_km ) + ( c * in_jm ) ) * invDet,
                                          m13:  ( ( b * gp_ho ) - ( c * fp_hn ) + ( d * fo_gn ) ) * invDet,
                                          m23: -( ( a * gp_ho ) - ( c * ep_hm ) + ( d * eo_gm ) ) * invDet,
                                          m33:  ( ( a * fp_hn ) - ( b * ep_hm ) + ( d * en_fm ) ) * invDet,
                                          m43: -( ( a * fo_gn ) - ( b * eo_gm ) + ( c * en_fm ) ) * invDet,
                                          m14: -( ( b * gl_hk ) - ( c * fl_hj ) + ( d * fk_gj ) ) * invDet,
                                          m24:  ( ( a * gl_hk ) - ( c * el_hi ) + ( d * ek_gi ) ) * invDet,
                                          m34: -( ( a * fl_hj ) - ( b * el_hi ) + ( d * ej_fi ) ) * invDet,
                                          m44:  ( ( a * fk_gj ) - ( b * ek_gi ) + ( c * ej_fi ) ) * invDet);
            }
        }

        //[MethodImpl(Optimize)]
        //private static Vector128<int> Divide(Vector128<int> a, Vector128<int> b)
        //{
        //    if (Avx.IsSupported)
        //    {
        //        var v0 = Avx.ConvertToVector256Double(a);
        //        var v1 = Avx.ConvertToVector256Double(b);

        //        var v2 = Avx.Divide(v0, v1);
        //        return Avx.ConvertToVector128Int32(v2);
        //    }
        //    else if (Sse2.IsSupported)
        //    {
        //        var v0 = Sse2.ConvertToVector128Double(a);
        //        var v1 = a.WithUpper(a.GetLower());
        //        var v2 = Sse2.ConvertToVector128Double(v1);

        //        var v3 = Sse2.ConvertToVector128Double(b);
        //        var v4 = b.WithUpper(b.GetLower());
        //        var v5 = Sse2.ConvertToVector128Double(v4);

        //        var v6 = Sse2.Divide(v0, v3);
        //        var v7 = Sse2.Divide(v2, v5);
        //        return Sse2.ConvertToVector128Int32(v6).WithUpper(Sse2.ConvertToVector128Int32(v7).GetLower());
        //    }
        //    else
        //        throw new PlatformNotSupportedException();
        //}

        /// <summary>
        /// Linearly interpolates between the corresponding values of two matrices.
        /// </summary>
        /// <param name="matrix1">
        /// The first source matrix.
        /// </param>
        /// <param name="matrix2">
        /// The second source matrix.
        /// </param>
        /// <param name="amount">
        /// The relative weight of the second source matrix.
        /// </param>
        [MethodImpl(Optimize)]
        public unsafe static Matrix4x4Fix16 Lerp(Matrix4x4Fix16 matrix1, Matrix4x4Fix16 matrix2, int amount)
        {
            (var m1, var m2) = (matrix1, matrix2);
            //Unsafe.SkipInit<Matrix4x4Fix16>(out var result);

            //if (Sse2.IsSupported)
            //{
            //    var amountVec = Vector128.Create(Unsafe.As<Fix16, int>(ref amount));

            //    Sse2.Store((int*)&result.M11, VectorMath.Lerp(Sse2.LoadVector128((int*)&m1.M11), Sse2.LoadVector128((int*)&m2.M11), amountVec));
            //    Sse2.Store((int*)&result.M21, VectorMath.Lerp(Sse2.LoadVector128((int*)&m1.M21), Sse2.LoadVector128((int*)&m2.M21), amountVec));
            //    Sse2.Store((int*)&result.M31, VectorMath.Lerp(Sse2.LoadVector128((int*)&m1.M31), Sse2.LoadVector128((int*)&m2.M31), amountVec));
            //    Sse2.Store((int*)&result.M41, VectorMath.Lerp(Sse2.LoadVector128((int*)&m1.M41), Sse2.LoadVector128((int*)&m2.M41), amountVec));

            //    return result;
            //}
            //else if (AdvSimd.IsSupported)
            //{
            //    var amountVec = Vector128.Create(Unsafe.As<Fix16, int>(ref amount));

            //    AdvSimd.Store((int*)&result.M11, VectorMath.Lerp(AdvSimd.LoadVector128((int*)&matrix1.M11), AdvSimd.LoadVector128((int*)&matrix2.M11), amountVec));
            //    AdvSimd.Store((int*)&result.M21, VectorMath.Lerp(AdvSimd.LoadVector128((int*)&matrix1.M21), AdvSimd.LoadVector128((int*)&matrix2.M21), amountVec));
            //    AdvSimd.Store((int*)&result.M31, VectorMath.Lerp(AdvSimd.LoadVector128((int*)&matrix1.M31), AdvSimd.LoadVector128((int*)&matrix2.M31), amountVec));
            //    AdvSimd.Store((int*)&result.M41, VectorMath.Lerp(AdvSimd.LoadVector128((int*)&matrix1.M41), AdvSimd.LoadVector128((int*)&matrix2.M41), amountVec));

            //    return result;
            //}
            return new Matrix4x4Fix16(m1.M11 + ( (m2.M11 - m1.M11) * amount ),
                                      m1.M12 + ( (m2.M12 - m1.M12) * amount ),
                                      m1.M13 + ( (m2.M13 - m1.M13) * amount ),
                                      m1.M14 + ( (m2.M14 - m1.M14) * amount ),
                                      m1.M21 + ( (m2.M21 - m1.M21) * amount ),
                                      m1.M22 + ( (m2.M22 - m1.M22) * amount ),
                                      m1.M23 + ( (m2.M23 - m1.M23) * amount ),
                                      m1.M24 + ( (m2.M24 - m1.M24) * amount ),
                                      m1.M31 + ( (m2.M31 - m1.M31) * amount ),
                                      m1.M32 + ( (m2.M32 - m1.M32) * amount ),
                                      m1.M33 + ( (m2.M33 - m1.M33) * amount ),
                                      m1.M34 + ( (m2.M34 - m1.M34) * amount ),
                                      m1.M41 + ( (m2.M41 - m1.M41) * amount ),
                                      m1.M42 + ( (m2.M42 - m1.M42) * amount ),
                                      m1.M43 + ( (m2.M43 - m1.M43) * amount ),
                                      m1.M44 + ( (m2.M44 - m1.M44) * amount ));
        }

        /// <summary>
        /// Multiplies a matrix by another matrix.
        /// </summary>
        /// <param name="left">
        /// The first source matrix.
        /// </param>
        /// <param name="right">
        /// The second source matrix.
        /// </param>
        [MethodImpl(Optimize)]
        public static Matrix4x4Fix16 Multiply(Matrix4x4Fix16 left, Matrix4x4Fix16 right) =>
            left * right;

        /// <summary>
        /// Multiplies a matrix by a scalar value.
        /// </summary>
        /// <param name="left">
        /// The source matrix.
        /// </param>
        /// <param name="right">
        /// The scaling factor.
        /// </param>
        [MethodImpl(Optimize)]
        public static Matrix4x4Fix16 Multiply(Matrix4x4Fix16 left, int right) =>
            left * right;

        /// <summary>
        /// Returns a new matrix with the negated elements of the given matrix.
        /// </summary>
        /// <param name="value">
        /// The source matrix.
        /// </param>
        [MethodImpl(Optimize)]
        public static Matrix4x4Fix16 Negate(Matrix4x4Fix16 value) =>
            -value;

        /// <summary>
        /// Returns a new matrix with the negated elements of the given matrix.
        /// </summary>
        /// <param name="value">
        /// The source matrix.
        /// </param>
        [MethodImpl(Optimize)]
        public unsafe static Matrix4x4Fix16 operator -(Matrix4x4Fix16 value)
        {
            Unsafe.SkipInit<Matrix4x4Fix16>(out var result);

            //if (Avx2.IsSupported)
            //{
            //    var zero = Vector256<int>.Zero;

            //    Avx.Store((int*)&result.M11, Avx2.Subtract(zero, Avx.LoadVector256((int*)&value.M11)));
            //    Avx.Store((int*)&result.M31, Avx2.Subtract(zero, Avx.LoadVector256((int*)&value.M31)));

            //    return result;
            //}
            //else if (Sse2.IsSupported)
            //{
            //    var zero = Vector128<int>.Zero;

            //    Sse2.Store((int*)&value.M11, Sse2.Subtract(zero, Sse2.LoadVector128((int*)&value.M11)));
            //    Sse2.Store((int*)&value.M21, Sse2.Subtract(zero, Sse2.LoadVector128((int*)&value.M21)));
            //    Sse2.Store((int*)&value.M31, Sse2.Subtract(zero, Sse2.LoadVector128((int*)&value.M31)));
            //    Sse2.Store((int*)&value.M41, Sse2.Subtract(zero, Sse2.LoadVector128((int*)&value.M41)));

            //    return value;
            //}
            //else if (AdvSimd.IsSupported)
            //{
            //    AdvSimd.Store((int*)&result.M11, AdvSimd.Negate(AdvSimd.LoadVector128((int*)&value.M11)));
            //    AdvSimd.Store((int*)&result.M21, AdvSimd.Negate(AdvSimd.LoadVector128((int*)&value.M21)));
            //    AdvSimd.Store((int*)&result.M31, AdvSimd.Negate(AdvSimd.LoadVector128((int*)&value.M31)));
            //    AdvSimd.Store((int*)&result.M41, AdvSimd.Negate(AdvSimd.LoadVector128((int*)&value.M41)));

            //    return result;
            //}
            return new Matrix4x4Fix16(-value.M11, -value.M12, -value.M13, -value.M14,
                                      -value.M21, -value.M22, -value.M23, -value.M24,
                                      -value.M31, -value.M32, -value.M33, -value.M34,
                                      -value.M41, -value.M42, -value.M43, -value.M44);
        }

        /// <summary>
        /// Subtracts the second matrix from the first.
        /// </summary>
        /// <param name="left">
        /// The first source matrix.
        /// </param>
        /// <param name="right">
        /// The second source matrix.
        /// </param>
        [MethodImpl(Optimize)]
        public unsafe static Matrix4x4Fix16 operator -(Matrix4x4Fix16 left, Matrix4x4Fix16 right)
        {
            //Unsafe.SkipInit<Matrix4x4Fix16>(out var result);

            //if (Avx2.IsSupported)
            //{
            //    Avx.Store((int*)&result.M11, Avx2.Subtract(Avx.LoadVector256((int*)&left.M11), Avx.LoadVector256((int*)&right.M11)));
            //    Avx.Store((int*)&result.M31, Avx2.Subtract(Avx.LoadVector256((int*)&left.M31), Avx.LoadVector256((int*)&right.M31)));

            //    return result;
            //}
            //else if (Sse2.IsSupported)
            //{
            //    Sse2.Store((int*)&result.M11, Sse2.Subtract(Sse2.LoadVector128((int*)&left.M11), Sse2.LoadVector128((int*)&right.M11)));
            //    Sse2.Store((int*)&result.M21, Sse2.Subtract(Sse2.LoadVector128((int*)&left.M21), Sse2.LoadVector128((int*)&right.M21)));
            //    Sse2.Store((int*)&result.M31, Sse2.Subtract(Sse2.LoadVector128((int*)&left.M31), Sse2.LoadVector128((int*)&right.M31)));
            //    Sse2.Store((int*)&result.M41, Sse2.Subtract(Sse2.LoadVector128((int*)&left.M41), Sse2.LoadVector128((int*)&right.M41)));

            //    return result;
            //}
            //else if (AdvSimd.IsSupported)
            //{
            //    AdvSimd.Store((int*)&result.M11, AdvSimd.Subtract(AdvSimd.LoadVector128((int*)&left.M11), AdvSimd.LoadVector128((int*)&right.M11)));
            //    AdvSimd.Store((int*)&result.M21, AdvSimd.Subtract(AdvSimd.LoadVector128((int*)&left.M21), AdvSimd.LoadVector128((int*)&right.M21)));
            //    AdvSimd.Store((int*)&result.M31, AdvSimd.Subtract(AdvSimd.LoadVector128((int*)&left.M31), AdvSimd.LoadVector128((int*)&right.M31)));
            //    AdvSimd.Store((int*)&result.M41, AdvSimd.Subtract(AdvSimd.LoadVector128((int*)&left.M41), AdvSimd.LoadVector128((int*)&right.M41)));

            //    return result;
            //}
            return new Matrix4x4Fix16(left.M11 - right.M11, left.M12 - right.M12, left.M13 - right.M13, left.M14 - right.M14,
                                      left.M21 - right.M21, left.M22 - right.M22, left.M23 - right.M23, left.M24 - right.M24,
                                      left.M31 - right.M31, left.M32 - right.M32, left.M33 - right.M33, left.M34 - right.M34,
                                      left.M41 - right.M41, left.M42 - right.M42, left.M43 - right.M43, left.M44 - right.M44);
        }

        /// <summary>
        /// Returns a boolean indicating whether the given two matrices are not equal.
        /// </summary>
        /// <param name="value1">
        /// The first matrix to compare.
        /// </param>
        /// <param name="value2">
        /// The second matrix to compare.
        /// </param>
        /// <returns>
        /// True if the given matrices are not equal; False if they are equal.
        /// </returns>
        [MethodImpl(Optimize)]
        public unsafe static bool operator !=(Matrix4x4Fix16 value1, Matrix4x4Fix16 value2)
        {
            //if (AdvSimd.IsSupported)
            //    return VectorMath.NotEqual(AdvSimd.LoadVector128((int*)&value1.M11), AdvSimd.LoadVector128((int*)&value2.M11)) ||
            //           VectorMath.NotEqual(AdvSimd.LoadVector128((int*)&value1.M21), AdvSimd.LoadVector128((int*)&value2.M21)) ||
            //           VectorMath.NotEqual(AdvSimd.LoadVector128((int*)&value1.M31), AdvSimd.LoadVector128((int*)&value2.M31)) ||
            //           VectorMath.NotEqual(AdvSimd.LoadVector128((int*)&value1.M41), AdvSimd.LoadVector128((int*)&value2.M41));

            //else if (Avx2.IsSupported)
            //    return VectorMath.NotEqual(Avx.LoadVector256((int*)&value1.M11), Avx.LoadVector256((int*)&value2.M11)) ||
            //           VectorMath.NotEqual(Avx.LoadVector256((int*)&value1.M31), Avx.LoadVector256((int*)&value2.M31));

            //else if (Sse2.IsSupported)
            //    return VectorMath.NotEqual(Sse2.LoadVector128((int*)&value1.M11), Sse2.LoadVector128((int*)&value2.M11)) ||
            //           VectorMath.NotEqual(Sse2.LoadVector128((int*)&value1.M21), Sse2.LoadVector128((int*)&value2.M21)) ||
            //           VectorMath.NotEqual(Sse2.LoadVector128((int*)&value1.M31), Sse2.LoadVector128((int*)&value2.M31)) ||
            //           VectorMath.NotEqual(Sse2.LoadVector128((int*)&value1.M41), Sse2.LoadVector128((int*)&value2.M41));

            //else
                return value1.M11 != value2.M11 || value1.M22 != value2.M22 ||
                       value1.M33 != value2.M33 || value1.M44 != value2.M44 ||
                       value1.M12 != value2.M12 || value1.M13 != value2.M13 ||
                       value1.M14 != value2.M14 || value1.M21 != value2.M21 ||
                       value1.M23 != value2.M23 || value1.M24 != value2.M24 ||
                       value1.M31 != value2.M31 || value1.M32 != value2.M32 ||
                       value1.M34 != value2.M34 || value1.M41 != value2.M41 ||
                       value1.M42 != value2.M42 || value1.M43 != value2.M43;
        }

        /// <summary>
        /// Multiplies a matrix by another matrix.
        /// </summary>
        /// <param name="value1">
        /// The first source matrix.
        /// </param>
        /// <param name="value2">
        /// The second source matrix.
        /// </param>
        [MethodImpl(Optimize)]
        public unsafe static Matrix4x4Fix16 operator *(Matrix4x4Fix16 value1, Matrix4x4Fix16 value2)
        {
            //Unsafe.SkipInit<Matrix4x4Fix16>(out var result);

            //if (Sse2.IsSupported)
            //{
            //    Sse2.Store((int*)&result.M11, MultiplyRow(value2, Sse2.LoadVector128((int*)&value1.M11)));
            //    Sse2.Store((int*)&result.M21, MultiplyRow(value2, Sse2.LoadVector128((int*)&value1.M21)));
            //    Sse2.Store((int*)&result.M31, MultiplyRow(value2, Sse2.LoadVector128((int*)&value1.M31)));
            //    Sse2.Store((int*)&result.M41, MultiplyRow(value2, Sse2.LoadVector128((int*)&value1.M41)));
            //    return result;
            //}
            //else if (AdvSimd.Arm64.IsSupported)
            //{
            //    var M11 = AdvSimd.LoadVector128((int*)&value1.M11);

            //    var vX = AdvSimd.MultiplyBySelectedScalar(AdvSimd.LoadVector128((int*)&value2.M11), M11, 0);
            //    var vY = AdvSimd.MultiplyBySelectedScalar(AdvSimd.LoadVector128((int*)&value2.M21), M11, 1);
            //    var vZ = AdvSimd.MultiplyAddBySelectedScalar(vX, AdvSimd.LoadVector128((int*)&value2.M31), M11, 2);
            //    var vW = AdvSimd.MultiplyAddBySelectedScalar(vY, AdvSimd.LoadVector128((int*)&value2.M41), M11, 3);

            //    AdvSimd.Store((int*)&result.M11, AdvSimd.Add(vZ, vW));
            //}

            return new Matrix4x4Fix16(( value1.M11 * value2.M11 ) + ( value1.M12 * value2.M21 ) + ( value1.M13 * value2.M31 ) + ( value1.M14 * value2.M41 ),
                                      ( value1.M11 * value2.M12 ) + ( value1.M12 * value2.M22 ) + ( value1.M13 * value2.M32 ) + ( value1.M14 * value2.M42 ),
                                      ( value1.M11 * value2.M13 ) + ( value1.M12 * value2.M23 ) + ( value1.M13 * value2.M33 ) + ( value1.M14 * value2.M43 ),
                                      ( value1.M11 * value2.M14 ) + ( value1.M12 * value2.M24 ) + ( value1.M13 * value2.M34 ) + ( value1.M14 * value2.M44 ),
                                      ( value1.M21 * value2.M11 ) + ( value1.M22 * value2.M21 ) + ( value1.M23 * value2.M31 ) + ( value1.M24 * value2.M41 ),
                                      ( value1.M21 * value2.M12 ) + ( value1.M22 * value2.M22 ) + ( value1.M23 * value2.M32 ) + ( value1.M24 * value2.M42 ),
                                      ( value1.M21 * value2.M13 ) + ( value1.M22 * value2.M23 ) + ( value1.M23 * value2.M33 ) + ( value1.M24 * value2.M43 ),
                                      ( value1.M21 * value2.M14 ) + ( value1.M22 * value2.M24 ) + ( value1.M23 * value2.M34 ) + ( value1.M24 * value2.M44 ),
                                      ( value1.M31 * value2.M11 ) + ( value1.M32 * value2.M21 ) + ( value1.M33 * value2.M31 ) + ( value1.M34 * value2.M41 ),
                                      ( value1.M31 * value2.M12 ) + ( value1.M32 * value2.M22 ) + ( value1.M33 * value2.M32 ) + ( value1.M34 * value2.M42 ),
                                      ( value1.M31 * value2.M13 ) + ( value1.M32 * value2.M23 ) + ( value1.M33 * value2.M33 ) + ( value1.M34 * value2.M43 ),
                                      ( value1.M31 * value2.M14 ) + ( value1.M32 * value2.M24 ) + ( value1.M33 * value2.M34 ) + ( value1.M34 * value2.M44 ),
                                      ( value1.M41 * value2.M11 ) + ( value1.M42 * value2.M21 ) + ( value1.M43 * value2.M31 ) + ( value1.M44 * value2.M41 ),
                                      ( value1.M41 * value2.M12 ) + ( value1.M42 * value2.M22 ) + ( value1.M43 * value2.M32 ) + ( value1.M44 * value2.M42 ),
                                      ( value1.M41 * value2.M13 ) + ( value1.M42 * value2.M23 ) + ( value1.M43 * value2.M33 ) + ( value1.M44 * value2.M43 ),
                                      ( value1.M41 * value2.M14 ) + ( value1.M42 * value2.M24 ) + ( value1.M43 * value2.M34 ) + ( value1.M44 * value2.M44 ));
        }

        /// <summary>
        /// Multiplies a matrix by a scalar value.
        /// </summary>
        /// <param name="value1">
        /// The source matrix.
        /// </param>
        /// <param name="value2">
        /// The scaling factor.
        /// </param>
        [MethodImpl(Optimize)]
        public unsafe static Matrix4x4Fix16 operator *(Matrix4x4Fix16 value1, int value2)
        {
            //Unsafe.SkipInit<Matrix4x4Fix16>(out var result);

            //if (AdvSimd.IsSupported)
            //{
            //    var right = Vector128.Create(value2);
            //    AdvSimd.Store(&result.M11, AdvSimd.Multiply(AdvSimd.LoadVector128(&value1.M11), right));
            //    AdvSimd.Store(&result.M21, AdvSimd.Multiply(AdvSimd.LoadVector128(&value1.M21), right));
            //    AdvSimd.Store(&result.M31, AdvSimd.Multiply(AdvSimd.LoadVector128(&value1.M31), right));
            //    AdvSimd.Store(&result.M41, AdvSimd.Multiply(AdvSimd.LoadVector128(&value1.M41), right));
            //}
            //else if (Avx.IsSupported)
            //{
            //    var right = Vector256.Create(value2);

            //    Avx.Store(&result.M11, Avx.Multiply(Avx.LoadVector256(&value1.M11), right));
            //    Avx.Store(&result.M31, Avx.Multiply(Avx.LoadVector256(&value1.M31), right));
            //}
            //else if (Sse.IsSupported)
            //{
            //    Vector128<Fix16> right = Vector128.Create(value2);
            //    Sse.Store(&result.M11, Sse.Multiply(Sse.LoadVector128(&value1.M11), right));
            //    Sse.Store(&result.M21, Sse.Multiply(Sse.LoadVector128(&value1.M21), right));
            //    Sse.Store(&result.M31, Sse.Multiply(Sse.LoadVector128(&value1.M31), right));
            //    Sse.Store(&result.M41, Sse.Multiply(Sse.LoadVector128(&value1.M41), right));
            //}
            //else
            return new Matrix4x4Fix16(value1.M11 * value2,
                                      value1.M12 * value2,
                                      value1.M13 * value2,
                                      value1.M14 * value2,
                                      value1.M21 * value2,
                                      value1.M22 * value2,
                                      value1.M23 * value2,
                                      value1.M24 * value2,
                                      value1.M31 * value2,
                                      value1.M32 * value2,
                                      value1.M33 * value2,
                                      value1.M34 * value2,
                                      value1.M41 * value2,
                                      value1.M42 * value2,
                                      value1.M43 * value2,
                                      value1.M44 * value2);
        }

        /// <summary>
        /// Returns itself.
        /// </summary>
        [MethodImpl(Optimize)]
        public static Matrix4x4Fix16 operator +(Matrix4x4Fix16 value) =>
            value;

        /// <summary>
        /// Adds two matrices together.
        /// </summary>
        /// <param name="left">
        /// The first source matrix.
        /// </param>
        /// <param name="right">
        /// The second source matrix.
        /// </param>
        [MethodImpl(Optimize)]
        public unsafe static Matrix4x4Fix16 operator +(Matrix4x4Fix16 left, Matrix4x4Fix16 right)
        {
            //Unsafe.SkipInit<Matrix4x4Fix16>(out var result);

            //if (AdvSimd.IsSupported)
            //{
            //    AdvSimd.Store(&result.M11, AdvSimd.Add(AdvSimd.LoadVector128(&left.M11), AdvSimd.LoadVector128(&right.M11)));
            //    AdvSimd.Store(&result.M21, AdvSimd.Add(AdvSimd.LoadVector128(&left.M21), AdvSimd.LoadVector128(&right.M21)));
            //    AdvSimd.Store(&result.M31, AdvSimd.Add(AdvSimd.LoadVector128(&left.M31), AdvSimd.LoadVector128(&right.M31)));
            //    AdvSimd.Store(&result.M41, AdvSimd.Add(AdvSimd.LoadVector128(&left.M41), AdvSimd.LoadVector128(&right.M41)));

            //    return result;
            //}
            //else if (Avx.IsSupported)
            //{
            //    Avx.Store(&result.M11, Avx.Add(Avx.LoadVector256(&left.M11), Avx.LoadVector256(&right.M11)));
            //    Avx.Store(&result.M31, Avx.Add(Avx.LoadVector256(&left.M31), Avx.LoadVector256(&right.M31)));

            //    return result;
            //}
            //else if(Sse.IsSupported)
            //{
            //    Sse.Store(&result.M11, Sse.Add(Sse.LoadVector128(&left.M11), Sse.LoadVector128(&right.M11)));
            //    Sse.Store(&result.M21, Sse.Add(Sse.LoadVector128(&left.M21), Sse.LoadVector128(&right.M21)));
            //    Sse.Store(&result.M31, Sse.Add(Sse.LoadVector128(&left.M31), Sse.LoadVector128(&right.M31)));
            //    Sse.Store(&result.M41, Sse.Add(Sse.LoadVector128(&left.M41), Sse.LoadVector128(&right.M41)));

            //    return result;
            //}
            //else
                return new Matrix4x4Fix16(left.M11 + right.M11, left.M12 + right.M12, left.M13 + right.M13, left.M14 + right.M14,
                                          left.M21 + right.M21, left.M22 + right.M22, left.M23 + right.M23, left.M24 + right.M24,
                                          left.M31 + right.M31, left.M32 + right.M32, left.M33 + right.M33, left.M34 + right.M34,
                                          left.M41 + right.M41, left.M42 + right.M42, left.M43 + right.M43, left.M44 + right.M44);
        }

        /// <summary>
        /// Returns a boolean indicating whether the given two matrices are equal.
        /// </summary>
        /// <param name="value1">
        /// The first matrix to compare.
        /// </param>
        /// <param name="value2">
        /// The second matrix to compare.
        /// </param>
        /// <returns>
        /// True if the given matrices are equal; False otherwise.
        /// </returns>
        [MethodImpl(Optimize)]
        public unsafe static bool operator ==(Matrix4x4Fix16 value1, Matrix4x4Fix16 value2)
        {
            //if (AdvSimd.IsSupported)
            //    return VectorMath.Equal(AdvSimd.LoadVector128(&value1.M11), AdvSimd.LoadVector128(&value2.M11)) &&
            //           VectorMath.Equal(AdvSimd.LoadVector128(&value1.M21), AdvSimd.LoadVector128(&value2.M21)) &&
            //           VectorMath.Equal(AdvSimd.LoadVector128(&value1.M31), AdvSimd.LoadVector128(&value2.M31)) &&
            //           VectorMath.Equal(AdvSimd.LoadVector128(&value1.M41), AdvSimd.LoadVector128(&value2.M41));

            //else if (Avx.IsSupported)
            //    return VectorMath.Equal(Avx.LoadVector256(&value1.M11), Avx.LoadVector256(&value2.M11)) &&
            //           VectorMath.Equal(Avx.LoadVector256(&value1.M31), Avx.LoadVector256(&value2.M31));

            //else if (Sse.IsSupported)
            //    return VectorMath.Equal(Sse.LoadVector128(&value1.M11), Sse.LoadVector128(&value2.M11)) &&
            //           VectorMath.Equal(Sse.LoadVector128(&value1.M21), Sse.LoadVector128(&value2.M21)) &&
            //           VectorMath.Equal(Sse.LoadVector128(&value1.M31), Sse.LoadVector128(&value2.M31)) &&
            //           VectorMath.Equal(Sse.LoadVector128(&value1.M41), Sse.LoadVector128(&value2.M41));

            //else
                return value1.M11 == value2.M11 && value1.M22 == value2.M22 &&
                       value1.M33 == value2.M33 && value1.M44 == value2.M44 &&
                       value1.M12 == value2.M12 && value1.M13 == value2.M13 &&
                       value1.M14 == value2.M14 && value1.M21 == value2.M21 &&
                       value1.M23 == value2.M23 && value1.M24 == value2.M24 &&
                       value1.M31 == value2.M31 && value1.M32 == value2.M32 &&
                       value1.M34 == value2.M34 && value1.M41 == value2.M41 &&
                       value1.M42 == value2.M42 && value1.M43 == value2.M43;
        }

        /// <summary>
        /// Subtracts the second matrix from the first.
        /// </summary>
        /// <param name="left">
        /// The first source matrix.
        /// </param>
        /// <param name="right">
        /// The second source matrix.
        /// </param>
        [MethodImpl(Optimize)]
        public static Matrix4x4Fix16 Subtract(Matrix4x4Fix16 left, Matrix4x4Fix16 right) =>
            left - right;

        /// <summary>
        /// Transposes the rows and columns of a matrix.
        /// </summary>
        /// <param name="matrix">
        /// The source matrix.
        /// </param>
        [MethodImpl(Optimize)]
        public unsafe static Matrix4x4Fix16 Transpose(Matrix4x4Fix16 matrix)
        {
            //Unsafe.SkipInit<Matrix4x4Fix16>(out var result);

            //if (AdvSimd.Arm64.IsSupported)
            //{
            //    // This implementation is based on the DirectX Math Library XMMatrixTranspose method
            //    // https://github.com/microsoft/DirectXMath/blob/master/Inc/DirectXMathMatrix.inl

            //    var M11 = AdvSimd.LoadVector128(&matrix.M11);
            //    var M31 = AdvSimd.LoadVector128(&matrix.M31);

            //    var P00 = AdvSimd.Arm64.ZipLow(M11, M31);
            //    var P01 = AdvSimd.Arm64.ZipHigh(M11, M31);

            //    var M21 = AdvSimd.LoadVector128(&matrix.M21);
            //    var M41 = AdvSimd.LoadVector128(&matrix.M41);

            //    var P10 = AdvSimd.Arm64.ZipLow(M21, M41);
            //    var P11 = AdvSimd.Arm64.ZipHigh(M21, M41);

            //    AdvSimd.Store(&result.M11, AdvSimd.Arm64.ZipLow(P00, P10));
            //    AdvSimd.Store(&result.M21, AdvSimd.Arm64.ZipHigh(P00, P10));
            //    AdvSimd.Store(&result.M31, AdvSimd.Arm64.ZipLow(P01, P11));
            //    AdvSimd.Store(&result.M41, AdvSimd.Arm64.ZipHigh(P01, P11));
            //}
            //else if (Sse.IsSupported)
            //{
            //    var row1 = Sse.LoadVector128(&matrix.M11);
            //    var row2 = Sse.LoadVector128(&matrix.M21);
            //    var row3 = Sse.LoadVector128(&matrix.M31);
            //    var row4 = Sse.LoadVector128(&matrix.M41);

            //    var l12 = Sse.UnpackLow(row1, row2);
            //    var l34 = Sse.UnpackLow(row3, row4);
            //    var h12 = Sse.UnpackHigh(row1, row2);
            //    var h34 = Sse.UnpackHigh(row3, row4);

            //    Sse.Store(&result.M11, Sse.MoveLowToHigh(l12, l34));
            //    Sse.Store(&result.M21, Sse.MoveHighToLow(l34, l12));
            //    Sse.Store(&result.M31, Sse.MoveLowToHigh(h12, h34));
            //    Sse.Store(&result.M41, Sse.MoveHighToLow(h34, h12));
            //}
            //else
                return new Matrix4x4Fix16(matrix.M11, matrix.M21, matrix.M31, matrix.M41,
                                          matrix.M12, matrix.M22, matrix.M32, matrix.M42,
                                          matrix.M13, matrix.M23, matrix.M33, matrix.M43,
                                          matrix.M14, matrix.M24, matrix.M34, matrix.M44);
        }

        /// <summary>
        /// Returns a boolean indicating whether this matrix instance is equal to the other given matrix.
        /// </summary>
        /// <param name="other">
        /// The matrix to compare this instance to.
        /// </param>
        /// <returns>
        /// True if the matrices are equal; False otherwise.
        /// </returns>
        [MethodImpl(Optimize)]
        public readonly bool Equals(Matrix4x4Fix16 other) =>
            this == other;

        /// <summary>
        /// Returns a boolean indicating whether the given Object is equal to this matrix instance.
        /// </summary>
        /// <param name="obj">
        /// The Object to compare against.
        /// </param>
        /// <returns>
        /// True if the Object is equal to this matrix; False otherwise.
        /// </returns>
        [MethodImpl(Optimize)]
        public override readonly bool Equals(object? obj)
        {
            if (obj is Matrix4x4Fix16 value)
            {
                return this == value;
            }
            return false;
        }

        /// <summary>
        /// Calculates the determinant of the matrix.
        /// </summary>
        public int GetDeterminant()
        {
            /*
             *  ⎡  a  b  c  d  ⎤       ⎡  f  g  h  ⎤       ⎡  e  g  h  ⎤       ⎡  e  f  h  ⎤       ⎡  e  f  g  ⎤
             *  ⎢  e  f  g  h  ⎥ = a ∙ ⎢  j  k  l  ⎥ - b ∙ ⎢  i  k  l  ⎥ + c ∙ ⎢  i  j  l  ⎥ - d ∙ ⎢  i  j  k  ⎥
             *  ⎢  i  j  k  l  ⎥       ⎣  n  o  p  ⎦       ⎣  m  o  p  ⎦       ⎣  m  n  p  ⎦       ⎣  m  n  o  ⎦
             *  ⎣  m  n  o  p  ⎦
             *
             *     ⎡  f  g  h  ⎤       ⎛                                                     ⎞
             * a ∙ ⎢  j  k  l  ⎥ = a ∙ ⎜ f ∙ ( kp - lo ) - g ∙ ( jp - ln ) + h ∙ ( jo - kn ) ⎟
             *     ⎣  n  o  p  ⎦       ⎝                                                     ⎠
             *
             *     ⎡  e  g  h  ⎤       ⎛                                                     ⎞
             * b ∙ ⎢  i  k  l  ⎥ = b ∙ ⎜ e ∙ ( kp - lo ) - g ∙ ( ip - lm ) + h ∙ ( io - km ) ⎟
             *     ⎣  m  o  p  ⎦       ⎝                                                     ⎠
             *
             *     ⎡  e  f  h  ⎤       ⎛                                                     ⎞
             * c ∙ ⎢  i  j  l  ⎥ = c ∙ ⎜ e ∙ ( jp - ln ) - f ∙ ( ip - lm ) + h ∙ ( in - jm ) ⎟
             *     ⎣  m  n  p  ⎦       ⎝                                                     ⎠
             *
             *     ⎡  e  f  g  ⎤       ⎛                                                     ⎞
             * d ∙ ⎢  i  j  k  ⎥ = d ∙ ⎜ e ∙ ( jo - kn ) - f ∙ ( io - km ) + g ∙ ( in - jm ) ⎟
             *     ⎣  m  n  o  ⎦       ⎝                                                     ⎠
             *
             * Cost of operation
             * 17 adds and 28 muls.
             *
             * add: 6 + 8 + 3 = 17
             * mul: 12 + 16 = 28
             */
            var a = M11;
            var b = M12;
            var c = M13;
            var d = M14;
            var e = M21;
            var f = M22;
            var g = M23;
            var h = M24;
            var i = M31;
            var j = M32;
            var k = M33;
            var l = M34;
            var m = M41;
            var n = M42;
            var o = M43;
            var p = M44;

            var kp_lo = (k * p) - (l * o);
            var jp_ln = (j * p) - (l * n);
            var jo_kn = (j * o) - (k * n);
            var ip_lm = (i * p) - (l * m);
            var io_km = (i * o) - (k * m);
            var in_jm = (i * n) - (j * m);

            return a * (( f * kp_lo ) - ( g * jp_ln ) + ( h * jo_kn ) ) -
                   b * (( e * kp_lo ) - ( g * ip_lm ) + ( h * io_km ) ) +
                   c * (( e * jp_ln ) - ( f * ip_lm ) + ( h * in_jm ) ) -
                   d * (( e * jo_kn ) - ( f * io_km ) + ( g * in_jm ) );
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(M11);
            hash.Add(M12);
            hash.Add(M13);
            hash.Add(M14);
            hash.Add(M21);
            hash.Add(M22);
            hash.Add(M23);
            hash.Add(M24);
            hash.Add(M31);
            hash.Add(M32);
            hash.Add(M33);
            hash.Add(M34);
            hash.Add(M41);
            hash.Add(M42);
            hash.Add(M43);
            hash.Add(M44);
            return hash.ToHashCode();
        }

        /// <summary>
        /// Attempts to calculate the inverse of this matrix. If successful, the inverted matrix will be returned.
        /// </summary>
        /// <returns>
        /// The inverted matrix or a NaN matrix if the inverse could not be calculated.
        /// </returns>
        [MethodImpl(Optimize)]
        public Matrix4x4Fix16 Invert() =>
            Invert(this);

        /// <summary>
        /// Returns a String representing this matrix instance.
        /// </summary>
        public override readonly string ToString() =>
            $"{{ {{M11:{M11} M12:{M12} M13:{M13} M14:{M14}}} {{M21:{M21} M22:{M22} M23:{M23} M24:{M24}}} {{M31:{M31} M32:{M32} M33:{M33} M34:{M34}}} {{M41:{M41} M42:{M42} M43:{M43} M44:{M44}}} }}";

        /// <summary>
        /// Transposes the rows and columns of this matrix.
        /// </summary>
        [MethodImpl(Optimize)]
        public Matrix4x4Fix16 Transpose() =>
            Transpose(this);

        /// <summary>
        /// Provides a record-style <see langword="with"/>-like constructor.
        /// </summary>
        [MethodImpl(Optimize)]
        public Matrix4x4Fix16 With(int? m11 = null, int? m12 = null, int? m13 = null, int? m14 = null, int? m21 = null, int? m22 = null, int? m23 = null, int? m24 = null, int? m31 = null, int? m32 = null, int? m33 = null, int? m34 = null, int? m41 = null, int? m42 = null, int? m43 = null, int? m44 = null) =>
            new Matrix4x4Fix16(
            m11 ?? M11,
            m12 ?? M12,
            m13 ?? M13,
            m14 ?? M14,
            m21 ?? M21,
            m22 ?? M22,
            m23 ?? M23,
            m24 ?? M24,
            m31 ?? M31,
            m32 ?? M32,
            m33 ?? M33,
            m34 ?? M34,
            m41 ?? M41,
            m42 ?? M42,
            m43 ?? M43,
            m44 ?? M44);

        [MethodImpl(Optimize)]
        public Matrix4x4Fix16 WithTranslation(Vector3Fix16 value) =>
            With(m41: value.X, m42: value.Y, m43: value.Z);

        #endregion Public Methods

        #region Internal Methods

        internal Matrix3x3Fix16 As3x3() =>
            new Matrix3x3Fix16(M11, M12, M13,
                                    M21, M22, M23,
                                    M31, M32, M33);

        #endregion Internal Methods

#region Private Methods

//[MethodImpl(Optimize)]
//private static unsafe Vector128<int> MultiplyRow(Matrix4x4Fix16 value2, Vector128<int> vector)
//{
//    return Sse2.Add(Sse2.Add(Sse41.Multiply(Sse2.Shuffle(vector, 0x00),
//                                            Sse2.LoadVector128((int*)&value2.M11)),
//                             Sse41.Multiply(Sse2.Shuffle(vector, 0x55),
//                                            Sse2.LoadVector128((int*)&value2.M21))),
//                    Sse2.Add(Sse41.Multiply(Sse2.Shuffle(vector, 0xAA),
//                                            Sse2.LoadVector128((int*)&value2.M31)),
//                             Sse41.Multiply(Sse2.Shuffle(vector, 0xFF),
//                                            Sse2.LoadVector128((int*)&value2.M41))));
//}
#if !NOSIMD
        [MethodImpl(Optimize)]
        private static Vector128<float> Permute(Vector128<float> value, byte control) =>
#if !NOAVX
            Avx.IsSupported ? 
                Avx.Permute(value, control) : 
#endif
                Sse.Shuffle(value, value, control);
#endif

#endregion Private Methods

#region Private Structs

        private struct CanonicalBasis
        {
#region Public Fields

            public Vector3Fix16 Row0;
            public Vector3Fix16 Row1;
            public Vector3Fix16 Row2;

#endregion Public Fields
        }

        private struct VectorBasis
        {
#region Public Fields

            public unsafe Vector3Fix16* Element0;
            public unsafe Vector3Fix16* Element1;
            public unsafe Vector3Fix16* Element2;

#endregion Public Fields
        }

        #endregion Private Structs

#region Private Classes

#if !NOSIMD

        private static class VectorMath
        {
#region Public Methods

#if !NOAVX
            [MethodImpl(Optimize)]
            public static bool Equal(Vector256<int> a, Vector256<int> b)
            {
                if (Avx2.IsSupported)
                    return Avx.MoveMask(Avx2.Xor(Avx2.CompareEqual(a, b), Vector256.Create(-1)).AsSingle()) == 0;
                else
                    throw new PlatformNotSupportedException();
            }
#endif

            [MethodImpl(Optimize)]
            public static bool Equal(Vector128<int> a, Vector128<int> b)
            {
                // This implementation is based on the DirectX Math Library XMVector4Equal method
                // https://github.com/microsoft/DirectXMath/blob/master/Inc/DirectXMathVector.inl

                if (AdvSimd.Arm64.IsSupported)
                {
                    var vResult = AdvSimd.CompareEqual(a, b).AsUInt32();

                    var vResult0 = vResult.GetLower().AsByte();
                    var vResult1 = vResult.GetUpper().AsByte();

                    var vTemp10 = AdvSimd.Arm64.ZipLow(vResult0, vResult1);
                    var vTemp11 = AdvSimd.Arm64.ZipHigh(vResult0, vResult1);

                    var vTemp21 = AdvSimd.Arm64.ZipHigh(vTemp10.AsUInt16(), vTemp11.AsUInt16());
                    return vTemp21.AsUInt32().GetElement(1) == 0xFFFFFFFF;
                }

                else if (Sse2.IsSupported)
                    return Sse.MoveMask(Sse2.Xor(Sse2.CompareEqual(a, b), Vector128.Create(-1)).AsSingle()) == 0;

                else
                    throw new PlatformNotSupportedException();
            }

            [MethodImpl(Optimize)]
            public static Vector128<int> Lerp(Vector128<int> a, Vector128<int> b, Vector128<int> t)
            {
                // This implementation is based on the DirectX Math Library XMVectorLerp method
                // https://github.com/microsoft/DirectXMath/blob/master/Inc/DirectXMathVector.inl

                if (AdvSimd.IsSupported)
                    return AdvSimd.Add(a, AdvSimd.Multiply(AdvSimd.Subtract(b, a), t));

                else if (Sse41.IsSupported)
                    return Sse2.Add(a, Sse41.MultiplyLow(Sse2.Subtract(b, a), t));

                else
                    // Redundant test so we won't preJIT remainder of this method on platforms without SIMD.
                    throw new PlatformNotSupportedException();
            }

            [MethodImpl(Optimize)]
            public static bool NotEqual(Vector128<int> a, Vector128<int> b)
            {
                // This implementation is based on the DirectX Math Library XMVector4Equal method
                // https://github.com/microsoft/DirectXMath/blob/master/Inc/DirectXMathVector.inl

                if (AdvSimd.Arm64.IsSupported)
                {
                    var vResult = AdvSimd.CompareEqual(a, b).AsUInt32();

                    var vResult0 = vResult.GetLower().AsByte();
                    var vResult1 = vResult.GetUpper().AsByte();

                    var vTemp10 = AdvSimd.Arm64.ZipLow(vResult0, vResult1);
                    var vTemp11 = AdvSimd.Arm64.ZipHigh(vResult0, vResult1);

                    var vTemp21 = AdvSimd.Arm64.ZipHigh(vTemp10.AsUInt16(), vTemp11.AsUInt16());
                    return vTemp21.AsUInt32().GetElement(1) != 0xFFFFFFFF;
                }

                else if (Sse2.IsSupported)
                    return Sse.MoveMask(Sse2.Xor(Sse2.CompareEqual(a, b),Vector128.Create(-1)).AsSingle()) != 0;

                else
                    throw new PlatformNotSupportedException();
            }
#if !NOAVX
            [MethodImpl(Optimize)]
            public static bool NotEqual(Vector256<int> a, Vector256<int> b)
            {
                if (Avx2.IsSupported)
                    return Avx.MoveMask(Avx2.Xor(Avx2.CompareEqual(a, b), Vector256.Create(-1)).AsSingle()) != 0;
                else
                    throw new PlatformNotSupportedException();
            }
#endif

#endregion Public Methods
        }
#endif

#endregion Private Classes
    }
}