using System;
using System.Linq;

class Vector : IComparable<Vector>
{
    private double[] components;

    // Конструктор за замовчуванням (створює нульовий вектор)
    public Vector()
    {
        components = new double[0];
    }

    // Конструктор з масиву
    public Vector(double[] components)
    {
        this.components = components.ToArray(); // Копіюємо значення
    }

    // Конструктор копіювання
    public Vector(Vector other)
    {
        components = other.components.ToArray();
    }

    // Деструктор
    ~Vector()
    {
        components = null;
    }

    // Додавання векторів
    public static Vector operator +(Vector v1, Vector v2)
    {
        if (v1.components.Length != v2.components.Length)
            throw new InvalidOperationException("Вектори повинні мати однакову розмірність!");

        double[] result = new double[v1.components.Length];
        for (int i = 0; i < v1.components.Length; i++)
            result[i] = v1.components[i] + v2.components[i];

        return new Vector(result);
    }

    // Віднімання векторів
    public static Vector operator -(Vector v1, Vector v2)
    {
        if (v1.components.Length != v2.components.Length)
            throw new InvalidOperationException("Вектори повинні мати однакову розмірність!");

        double[] result = new double[v1.components.Length];
        for (int i = 0; i < v1.components.Length; i++)
            result[i] = v1.components[i] - v2.components[i];

        return new Vector(result);
    }

    // Скалярне множення
    public static double operator *(Vector v1, Vector v2)
    {
        if (v1.components.Length != v2.components.Length)
            throw new InvalidOperationException("Вектори повинні мати однакову розмірність!");

        double sum = 0;
        for (int i = 0; i < v1.components.Length; i++)
            sum += v1.components[i] * v2.components[i];

        return sum;
    }

    // Множення вектора на число
    public static Vector operator *(Vector v, double scalar)
    {
        double[] result = new double[v.components.Length];
        for (int i = 0; i < v.components.Length; i++)
            result[i] = v.components[i] * scalar;

        return new Vector(result);
    }

    // Довжина вектора
    public double Length()
    {
        return Math.Sqrt(components.Sum(x => x * x));
    }

    // Метод для зміни компоненти вектора
    public void SetComponent(int index, double value)
    {
        if (index < 0 || index >= components.Length)
            throw new IndexOutOfRangeException("Невірний індекс!");

        components[index] = value;
    }

    // Порівняння векторів за довжиною (для сортування)
    public int CompareTo(Vector other)
    {
        return other.Length().CompareTo(this.Length()); // Сортування за спаданням
    }

    // Виведення вектора у вигляді рядка
    public override string ToString()
    {
        return $"[{string.Join(", ", components)}] (Довжина: {Length():F2})";
    }
}

class Program
{
    static void Main()
    {
        // Масив векторів
        Vector[] A =
        {
            new Vector(new double[] {1, 2, 3}),
            new Vector(new double[] {4, 5}),
            new Vector(new double[] {0, 0, 0, 1}),
            new Vector(new double[] {7, 1, 2}),
        };

        Console.WriteLine("До сортування:");
        foreach (var v in A)
            Console.WriteLine(v);

        // Сортування в порядку спадання довжин
        Array.Sort(A);

        Console.WriteLine("\nПісля сортування:");
        foreach (var v in A)
            Console.WriteLine(v);

        // Демонстрація операцій
        Console.WriteLine("\nОперації з векторами:");
        Vector v1 = new Vector(new double[] {1, 2, 3});
        Vector v2 = new Vector(new double[] {4, 5, 6});

        Console.WriteLine($"v1: {v1}");
        Console.WriteLine($"v2: {v2}");

        Console.WriteLine($"v1 + v2 = {v1 + v2}");
        Console.WriteLine($"v1 - v2 = {v1 - v2}");
        Console.WriteLine($"v1 * v2 (скалярне множення) = {v1 * v2}");
        Console.WriteLine($"v1 * 2 (множення на число) = {v1 * 2}");
    }
}
