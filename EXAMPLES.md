# Code Examples - Advanced Programming Notes

This document provides comprehensive code examples demonstrating practical usage of the concepts covered in the Advanced Programming Notes solution.

## Table of Contents
- [Reference Semantics Examples](#reference-semantics-examples)
- [Operator Overloading Examples](#operator-overloading-examples)
- [Performance Comparisons](#performance-comparisons)
- [Real-World Scenarios](#real-world-scenarios)
- [Anti-Patterns and Gotchas](#anti-patterns-and-gotchas)

## Reference Semantics Examples

### Basic ref Parameter Usage

```csharp
// Example 1: Simple value modification
public void BasicRefExample()
{
    int originalValue = 10;
    Console.WriteLine($"Before: {originalValue}"); // Output: Before: 10
    
    ModifyByRef(ref originalValue);
    Console.WriteLine($"After: {originalValue}");  // Output: After: 25
}

void ModifyByRef(ref int value)
{
    value = value * 2 + 5;
}

// Example 2: Swapping values
public void SwapExample()
{
    int a = 5, b = 10;
    Console.WriteLine($"Before swap: a={a}, b={b}"); // Before swap: a=5, b=10
    
    Swap(ref a, ref b);
    Console.WriteLine($"After swap: a={a}, b={b}");  // After swap: a=10, b=5
}

void Swap(ref int x, ref int y)
{
    int temp = x;
    x = y;
    y = temp;
}
```

### ref return Examples

```csharp
public class ArrayManager
{
    private int[] _data = new int[100];
    
    // Example 1: Direct array element access
    public ref int GetElement(int index)
    {
        if (index < 0 || index >= _data.Length)
            throw new IndexOutOfRangeException();
            
        return ref _data[index];
    }
    
    // Example 2: Finding and returning reference to max element
    public ref int GetMaxElement()
    {
        if (_data.Length == 0)
            throw new InvalidOperationException("Array is empty");
            
        int maxIndex = 0;
        for (int i = 1; i < _data.Length; i++)
        {
            if (_data[i] > _data[maxIndex])
                maxIndex = i;
        }
        return ref _data[maxIndex];
    }
}

// Usage examples
public void RefReturnUsage()
{
    var manager = new ArrayManager();
    
    // Direct element modification
    ref int element50 = ref manager.GetElement(50);
    element50 = 999; // Directly modifies the array element
    
    // Modify max element directly
    ref int maxElement = ref manager.GetMaxElement();
    maxElement = 1000; // Now this element is definitely the max
    
    // Getting value vs getting reference
    int valueOnly = manager.GetElement(25);     // Gets copy of the value
    ref int reference = ref manager.GetElement(25); // Gets reference to the element
    
    valueOnly = 500;  // Only changes local variable
    reference = 600;  // Changes the actual array element
}
```

### ref locals Examples

```csharp
public void RefLocalsExample()
{
    // Example 1: Basic ref local
    int originalVariable = 42;
    ref int aliasVariable = ref originalVariable;
    
    Console.WriteLine($"Original: {originalVariable}"); // Output: Original: 42
    aliasVariable = 100;
    Console.WriteLine($"Original after alias change: {originalVariable}"); // Output: Original after alias change: 100
    
    // Example 2: Working with arrays
    int[] numbers = { 1, 2, 3, 4, 5 };
    ref int middleNumber = ref numbers[2];
    
    middleNumber = 999;
    Console.WriteLine($"Array after ref local change: [{string.Join(", ", numbers)}]");
    // Output: Array after ref local change: [1, 2, 999, 4, 5]
    
    // Example 3: Conditional ref assignment
    bool useFirst = true;
    int first = 10, second = 20;
    ref int chosen = ref (useFirst ? ref first : ref second);
    
    chosen = 50;
    Console.WriteLine($"First: {first}, Second: {second}"); 
    // Output: First: 50, Second: 20
}
```

## Operator Overloading Examples

### Basic Operator Implementation

```csharp
// Enhanced Book and Student classes with more operators
public class Book
{
    public string Name { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public int PageCount { get; set; }
    public decimal Price { get; set; }
    
    // Addition: Add book to student
    public static Student operator +(Book book, Student student)
    {
        student.Books.Add(book);
        return student;
    }
    
    // Equality operators
    public static bool operator ==(Book left, Book right)
    {
        if (ReferenceEquals(left, right)) return true;
        if (left is null || right is null) return false;
        return left.Name == right.Name && left.Author == right.Author;
    }
    
    public static bool operator !=(Book left, Book right) => !(left == right);
    
    // Comparison operators (by page count)
    public static bool operator <(Book left, Book right)
        => left?.PageCount < right?.PageCount;
        
    public static bool operator >(Book left, Book right)
        => left?.PageCount > right?.PageCount;
        
    public static bool operator <=(Book left, Book right)
        => left < right || left == right;
        
    public static bool operator >=(Book left, Book right)
        => left > right || left == right;
    
    public override bool Equals(object? obj) => obj is Book book && this == book;
    public override int GetHashCode() => HashCode.Combine(Name, Author);
    public override string ToString() => $"{Name} by {Author} ({PageCount} pages)";
}

public class Student
{
    public string Name { get; set; } = string.Empty;
    public List<Book> Books { get; set; } = new();
    public decimal TotalSpent { get; set; }
    
    // Subtraction: Remove book from student
    public static Student operator -(Student student, Book book)
    {
        student.Books.Remove(book);
        return student;
    }
    
    // Indexer-like access
    public Book this[int index] => Books[index];
    
    public override string ToString() 
        => $"{Name} has {Books.Count} book(s), spent ${TotalSpent:F2}";
}
```

### Advanced Operator Usage

```csharp
public void AdvancedOperatorExamples()
{
    // Create books
    var book1 = new Book 
    { 
        Name = "Clean Code", 
        Author = "Robert C. Martin", 
        PageCount = 464, 
        Price = 45.99m 
    };
    
    var book2 = new Book 
    { 
        Name = "Design Patterns", 
        Author = "Gang of Four", 
        PageCount = 395, 
        Price = 54.99m 
    };
    
    var book3 = new Book 
    { 
        Name = "Clean Code", 
        Author = "Robert C. Martin", 
        PageCount = 464, 
        Price = 45.99m 
    };
    
    // Create student
    var student = new Student { Name = "John Doe" };
    
    // Using + operator to add books
    student = book1 + student;
    student = book2 + student;
    
    Console.WriteLine(student); // John Doe has 2 book(s), spent $0.00
    
    // Using - operator to remove book
    student = student - book1;
    Console.WriteLine(student); // John Doe has 1 book(s), spent $0.00
    
    // Using comparison operators
    Console.WriteLine($"book1 == book3: {book1 == book3}"); // True
    Console.WriteLine($"book1 > book2: {book1 > book2}");   // True (464 > 395 pages)
    Console.WriteLine($"book1 < book2: {book1 < book2}");   // False
    
    // Chaining operations
    var anotherStudent = new Student { Name = "Jane Smith" };
    anotherStudent = book1 + book2 + anotherStudent;
    Console.WriteLine(anotherStudent); // Jane Smith has 2 book(s), spent $0.00
}
```

### Custom Mathematical Operators

```csharp
public struct Vector2D
{
    public double X { get; set; }
    public double Y { get; set; }
    
    public Vector2D(double x, double y) => (X, Y) = (x, y);
    
    // Arithmetic operators
    public static Vector2D operator +(Vector2D left, Vector2D right)
        => new(left.X + right.X, left.Y + right.Y);
        
    public static Vector2D operator -(Vector2D left, Vector2D right)
        => new(left.X - right.X, left.Y - right.Y);
        
    public static Vector2D operator *(Vector2D vector, double scalar)
        => new(vector.X * scalar, vector.Y * scalar);
        
    public static Vector2D operator *(double scalar, Vector2D vector)
        => vector * scalar;
        
    public static Vector2D operator /(Vector2D vector, double scalar)
        => new(vector.X / scalar, vector.Y / scalar);
    
    // Unary operators
    public static Vector2D operator -(Vector2D vector)
        => new(-vector.X, -vector.Y);
        
    public static Vector2D operator +(Vector2D vector)
        => vector;
    
    // Conversion operators
    public static implicit operator (double, double)(Vector2D vector)
        => (vector.X, vector.Y);
        
    public static implicit operator Vector2D((double X, double Y) tuple)
        => new(tuple.X, tuple.Y);
    
    public double Magnitude => Math.Sqrt(X * X + Y * Y);
    public override string ToString() => $"({X:F2}, {Y:F2})";
}

// Usage example
public void VectorOperatorExamples()
{
    var v1 = new Vector2D(3, 4);
    var v2 = new Vector2D(1, 2);
    
    var sum = v1 + v2;              // (4.00, 6.00)
    var difference = v1 - v2;       // (2.00, 2.00)
    var scaled = v1 * 2;            // (6.00, 8.00)
    var negated = -v1;              // (-3.00, -4.00)
    
    // Using implicit conversion
    (double x, double y) = v1;      // Converts to tuple
    Vector2D fromTuple = (5, 6);    // Converts from tuple
    
    Console.WriteLine($"v1: {v1}, magnitude: {v1.Magnitude:F2}");
    Console.WriteLine($"v2: {v2}, magnitude: {v2.Magnitude:F2}");
    Console.WriteLine($"Sum: {sum}");
    Console.WriteLine($"Difference: {difference}");
    Console.WriteLine($"Scaled: {scaled}");
}
```

## Performance Comparisons

### ref vs Value Parameter Performance

```csharp
public struct LargeStruct
{
    private readonly double[] _data;
    
    public LargeStruct(int size)
    {
        _data = new double[size];
        for (int i = 0; i < size; i++)
            _data[i] = Random.Shared.NextDouble();
    }
    
    public double Sum => _data.Sum();
    public int Length => _data.Length;
}

// Performance comparison methods
public class PerformanceExample
{
    // Method that copies the struct (slower)
    public double ProcessByValue(LargeStruct data)
    {
        return data.Sum * 2; // Struct is copied when passed
    }
    
    // Method that uses reference (faster)
    public double ProcessByRef(ref LargeStruct data)
    {
        return data.Sum * 2; // No copying occurs
    }
    
    // Method that returns reference for direct modification
    public ref LargeStruct GetStructRef(ref LargeStruct data)
    {
        return ref data;
    }
    
    public void DemonstratePerformanceDifference()
    {
        var largeStruct = new LargeStruct(10000);
        
        // Timing value-based approach
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        for (int i = 0; i < 1000; i++)
        {
            ProcessByValue(largeStruct); // Each call copies 80KB+ of data
        }
        stopwatch.Stop();
        Console.WriteLine($"Value-based: {stopwatch.ElapsedMilliseconds}ms");
        
        // Timing reference-based approach
        stopwatch.Restart();
        for (int i = 0; i < 1000; i++)
        {
            ProcessByRef(ref largeStruct); // No copying
        }
        stopwatch.Stop();
        Console.WriteLine($"Reference-based: {stopwatch.ElapsedMilliseconds}ms");
    }
}
```

## Real-World Scenarios

### Scenario 1: Configuration Management with ref

```csharp
public class ConfigurationManager
{
    private readonly Dictionary<string, object> _config = new();
    
    public ref T GetConfigValue<T>(string key) where T : struct
    {
        if (!_config.ContainsKey(key))
            _config[key] = default(T);
            
        return ref Unsafe.As<object, T>(ref _config[key]);
    }
    
    public void SetConfigValue<T>(string key, T value) where T : struct
    {
        _config[key] = value;
    }
}

// Usage
public void ConfigurationExample()
{
    var config = new ConfigurationManager();
    
    // Direct modification of config values
    ref int maxConnections = ref config.GetConfigValue<int>("MaxConnections");
    maxConnections = 100; // Directly modifies the stored value
    
    ref double timeout = ref config.GetConfigValue<double>("TimeoutSeconds");
    timeout = 30.0;
    
    Console.WriteLine($"Max Connections: {config.GetConfigValue<int>("MaxConnections")}");
    Console.WriteLine($"Timeout: {config.GetConfigValue<double>("TimeoutSeconds")}");
}
```

### Scenario 2: Library Management System

```csharp
public class Library
{
    private readonly List<Book> _books = new();
    private readonly List<Student> _students = new();
    
    public void AddBook(Book book) => _books.Add(book);
    public void RegisterStudent(Student student) => _students.Add(student);
    
    // Operator overloading for intuitive book lending
    public static Library operator +(Library library, (Book book, Student student) loan)
    {
        var (book, student) = loan;
        if (library._books.Contains(book))
        {
            student = book + student; // Use our overloaded operator
            library._books.Remove(book);
        }
        return library;
    }
    
    // Operator for returning books
    public static Library operator -(Library library, (Book book, Student student) return_)
    {
        var (book, student) = return_;
        student = student - book; // Use our overloaded operator
        library._books.Add(book);
        return library;
    }
}

// Usage
public void LibrarySystemExample()
{
    var library = new Library();
    var book = new Book { Name = "C# in Depth", Author = "Jon Skeet" };
    var student = new Student { Name = "Alice" };
    
    library.AddBook(book);
    library.RegisterStudent(student);
    
    // Lend book to student
    library = library + (book, student);
    Console.WriteLine($"Student now has: {student.Books.Count} books");
    
    // Return book to library
    library = library - (book, student);
    Console.WriteLine($"Student now has: {student.Books.Count} books");
}
```

### Scenario 3: Game Development with Vectors

```csharp
public class GameObject
{
    public Vector2D Position { get; set; }
    public Vector2D Velocity { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public void Update(double deltaTime)
    {
        // Using overloaded operators for natural physics calculations
        Position = Position + (Velocity * deltaTime);
    }
    
    public static GameObject operator +(GameObject obj, Vector2D force)
    {
        obj.Velocity = obj.Velocity + force;
        return obj;
    }
}

public void GamePhysicsExample()
{
    var player = new GameObject 
    { 
        Name = "Player", 
        Position = (0, 0), 
        Velocity = (5, 0) 
    };
    
    // Apply gravity
    var gravity = new Vector2D(0, -9.81);
    player = player + gravity;
    
    // Simulate movement for 1 second
    player.Update(1.0);
    
    Console.WriteLine($"Player position after 1 second: {player.Position}");
    Console.WriteLine($"Player velocity: {player.Velocity}");
}
```

## Anti-Patterns and Gotchas

### ❌ Common Mistakes with ref

```csharp
// WRONG: Trying to return ref to local variable
public ref int BadRefReturn()
{
    int localVar = 42;
    return ref localVar; // Compilation error: CS8168
}

// WRONG: Forgetting ref keyword in call
public void BadRefUsage()
{
    int value = 10;
    ModifyValue(value);     // Won't modify original value
    ModifyValue(ref value); // Correct way
}

void ModifyValue(ref int x) => x = 100;

// WRONG: Ref to readonly field
public class BadRefClass
{
    private readonly int _readonlyField = 42;
    
    public ref int GetReadonlyRef()
    {
        return ref _readonlyField; // Compilation error
    }
}
```

### ❌ Operator Overloading Pitfalls

```csharp
// WRONG: Non-intuitive operator behavior
public static Book operator +(Book book1, Book book2)
{
    // This is confusing - what does adding books mean?
    return new Book { Name = book1.Name + book2.Name };
}

// WRONG: Inconsistent parameter types
public static Student operator +(Book book, Student student) { /* ... */ }
public static Student operator +(Student student, Book book) { /* ... */ }
// Having both can be confusing

// WRONG: Modifying operands unexpectedly
public static Vector2D operator +(Vector2D left, Vector2D right)
{
    left.X += right.X; // Modifies the left operand!
    left.Y += right.Y;
    return left;
}

// CORRECT: Return new instance
public static Vector2D operator +(Vector2D left, Vector2D right)
{
    return new Vector2D(left.X + right.X, left.Y + right.Y);
}
```

### ✅ Best Practices

```csharp
// GOOD: Clear, intuitive operator behavior
public static Money operator +(Money left, Money right)
{
    if (left.Currency != right.Currency)
        throw new InvalidOperationException("Cannot add different currencies");
    
    return new Money(left.Amount + right.Amount, left.Currency);
}

// GOOD: Proper ref usage with validation
public ref T GetArrayElement<T>(T[] array, int index)
{
    if (array == null) throw new ArgumentNullException(nameof(array));
    if (index < 0 || index >= array.Length) 
        throw new IndexOutOfRangeException();
    
    return ref array[index];
}

// GOOD: Consistent operator pairs
public static bool operator ==(ComplexNumber left, ComplexNumber right)
    => left.Real == right.Real && left.Imaginary == right.Imaginary;

public static bool operator !=(ComplexNumber left, ComplexNumber right)
    => !(left == right);

// Override Equals and GetHashCode when implementing == and !=
public override bool Equals(object? obj) 
    => obj is ComplexNumber other && this == other;

public override int GetHashCode() 
    => HashCode.Combine(Real, Imaginary);
```

## Summary

These examples demonstrate the power and flexibility of advanced C# features:

- **Reference semantics** provide performance benefits and enable direct data manipulation
- **Operator overloading** creates intuitive, domain-specific APIs
- **Proper usage** requires understanding the implications and following best practices
- **Real-world applications** show how these features solve practical problems

Remember to always prioritize code clarity and maintainability over clever usage of language features.