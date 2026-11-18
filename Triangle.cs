namespace Lab4Part2
{
    /// <summary>
    /// Треугольник, заданный длинами сторон.
    /// </summary>
    public class Triangle
    {
        private double _a;
        private double _b;
        private double _c;

        public Triangle()
            : this(1.0, 1.0, 1.0)
        {
        }

        public Triangle(double a, double b, double c)
        {
            A = a;
            B = b;
            C = c;
        }

        public Triangle(Triangle other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            _a = other._a;
            _b = other._b;
            _c = other._c;
        }

        public double A
        {
            get { return _a; }
            set
            {
                ValidateSide(value, nameof(A));
                _a = value;
            }
        }

        public double B
        {
            get { return _b; }
            set
            {
                ValidateSide(value, nameof(B));
                _b = value;
            }
        }

        public double C
        {
            get { return _c; }
            set
            {
                ValidateSide(value, nameof(C));
                _c = value;
            }
        }

        /// <summary>
        /// Определяет, существует ли треугольник с такими длинами сторон.
        /// </summary>
        public bool Exists()
        {
            return _a + _b > _c && _a + _c > _b && _b + _c > _a;
        }

        /// <summary>
        /// Площадь по формуле Герона; 0, если треугольник не существует.
        /// </summary>
        public double GetArea()
        {
            if (!Exists())
            {
                return 0.0;
            }

            double halfPerimeter = (_a + _b + _c) / 2.0;
            return Math.Sqrt(halfPerimeter * (halfPerimeter - _a) * (halfPerimeter - _b) * (halfPerimeter - _c));
        }

        public override string ToString()
        {
            return $"Triangle: a = {_a}, b = {_b}, c = {_c}";
        }

        // Унарная операция: площадь треугольника.
        public static double operator -(Triangle triangle)
        {
            if (triangle == null)
            {
                throw new ArgumentNullException(nameof(triangle));
            }

            return triangle.GetArea();
        }

        // Неявное приведение: периметр.
        public static implicit operator double(Triangle triangle)
        {
            if (triangle == null)
            {
                throw new ArgumentNullException(nameof(triangle));
            }

            return triangle._a + triangle._b + triangle._c;
        }

        // Явное приведение: существует ли треугольник.
        public static explicit operator bool(Triangle triangle)
        {
            if (triangle == null)
            {
                throw new ArgumentNullException(nameof(triangle));
            }

            return triangle.Exists();
        }

        public static bool operator <(Triangle left, Triangle right)
        {
            if (left == null || right == null)
            {
                throw new ArgumentNullException(left == null ? nameof(left) : nameof(right));
            }

            return left.GetArea() < right.GetArea();
        }

        public static bool operator >(Triangle left, Triangle right)
        {
            if (left == null || right == null)
            {
                throw new ArgumentNullException(left == null ? nameof(left) : nameof(right));
            }

            return left.GetArea() > right.GetArea();
        }

        private static void ValidateSide(double value, string name)
        {
            if (double.IsNaN(value) || double.IsInfinity(value) || value <= 0.0)
            {
                throw new ArgumentOutOfRangeException(name, "Длина стороны должна быть положительным числом.");
            }
        }
    }
}
