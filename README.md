# Advanced Programming Notes

A .NET 8.0 educational solution demonstrating advanced C# programming concepts including reference semantics and operator overloading.

## 🚀 Quick Start

### Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later
- Visual Studio 2022, Visual Studio Code, or JetBrains Rider

### Running the Projects

```bash
# Clone the repository and navigate to the project
cd AdvancedProgramming.Notes

# Restore dependencies
dotnet restore

# Run Lesson 3 - Reference Semantics
dotnet run --project AdvancedProgramming.Notes.Lesson3

# Run Lesson 4 - Operator Overloading  
dotnet run --project AdvancedProgramming.Notes.Lesson4

# Build entire solution
dotnet build
```

## 📚 What You'll Learn

### Lesson 3: Reference Semantics
- **`ref` keyword**: Pass value types by reference
- **`ref` return**: Return references instead of copies for performance
- **`ref` locals**: Create local variables that reference other variables
- **Performance optimization**: Avoid unnecessary copying of large data structures

### Lesson 4: Operator Overloading
- **Custom operators**: Implement domain-specific behavior for operators
- **Type design**: Create intuitive APIs using familiar syntax
- **Best practices**: Parameter ordering and operator semantics

## 🔧 Project Structure

```
AdvancedProgramming.Notes/
├── 📁 AdvancedProgramming.Notes.Lesson3/    # Reference semantics examples
│   ├── Program.cs                           # ref, ref return, ref locals
│   └── AdvancedProgramming.Notes.Lesson3.csproj
├── 📁 AdvancedProgramming.Notes.Lesson4/    # Operator overloading examples  
│   ├── Program.cs                           # Student + Book operator
│   └── AdvancedProgramming.Notes.Lesson4.csproj
├── AdvancedProgramming.Notes.sln            # Solution file
├── 📖 API_DOCUMENTATION.md                  # Comprehensive documentation
├── 📋 API_REFERENCE.md                      # Quick API reference
└── 📄 README.md                             # This file
```

## 💡 Code Examples

### Reference Semantics (Lesson 3)

```csharp
// Basic ref parameter
int value = 5;
ModifyValue(ref value);
Console.WriteLine(value); // Output: 124

void ModifyValue(ref int a) => a = 124;

// ref return for performance
int[] largeArray = new int[1000];
ref int element = ref GetArrayElement(largeArray, 500);
element = 42; // Directly modifies largeArray[500]

ref int GetArrayElement(int[] array, int index) => ref array[index];

// ref locals
char original = 'a';
ref char alias = ref original;
alias = 'b'; // original is now 'b'
```

### Operator Overloading (Lesson 4)

```csharp
// Define classes with custom operator
public class Book
{
    public string Name { get; set; }
    public string Author { get; set; }
    
    public static Student operator +(Book book, Student student)
    {
        student.Books.Add(book);
        return student;
    }
}

public class Student  
{
    public string Name { get; set; }
    public List<Book> Books { get; set; } = new();
}

// Usage - intuitive syntax for domain operations
Student student = new() { Name = "Alice" };
Book book = new() { Name = "Clean Code", Author = "Robert Martin" };

Student enrichedStudent = book + student; // Book added to student's collection
```

## 📖 Documentation

| Document | Description |
|----------|-------------|
| [📖 API Documentation](API_DOCUMENTATION.md) | Comprehensive guide with examples, best practices, and advanced scenarios |
| [📋 API Reference](API_REFERENCE.md) | Quick reference for all public APIs, classes, and methods |

## 🎯 Key Features

### Performance Benefits
- ✅ **Zero-copy operations** with `ref` semantics
- ✅ **Direct memory access** for large data structures  
- ✅ **Reduced allocations** by avoiding defensive copying
- ✅ **Optimized algorithms** using reference parameters

### Developer Experience
- ✅ **Intuitive APIs** through operator overloading
- ✅ **Type-safe operations** with compile-time checking
- ✅ **Familiar syntax** for domain-specific operations
- ✅ **Comprehensive documentation** with examples

## ⚠️ Important Notes

### Reference Semantics Gotchas
- Cannot return references to local variables
- Reference parameters must be initialized before use
- Be mindful of object lifetime and scope
- Not inherently thread-safe

### Operator Overloading Guidelines
- Operators should behave intuitively
- Parameter order matters and should be documented
- Consider implementing related operators together
- Prefer immutable operations when possible

## 🧪 Testing

Example unit tests for the APIs:

```csharp
[Test]
public void RefReturn_ModifiesOriginalVariable()
{
    int original = 5;
    ref int reference = ref GetReference(ref original);
    reference = 10;
    
    Assert.AreEqual(10, original);
}

[Test] 
public void BookAddition_AddsBookToStudent()
{
    var student = new Student { Name = "Test Student" };
    var book = new Book { Name = "Test Book", Author = "Test Author" };
    
    var result = book + student;
    
    Assert.AreEqual(1, result.Books.Count);
    Assert.AreEqual("Test Book", result.Books[0].Name);
}
```

## 🔍 Advanced Topics

For production use, consider exploring:
- `Span<T>` and `Memory<T>` for advanced memory management
- `ref struct` types for stack-only allocation
- Custom collection types with operator overloading
- Unsafe code for pointer manipulation
- Performance benchmarking with BenchmarkDotNet

## 📝 License

This educational project is provided as-is for learning purposes.

## 🤝 Contributing

This is an educational repository. Feel free to fork and experiment with the concepts demonstrated.

---

**Happy Learning! 🎓**

*For detailed API documentation and advanced usage scenarios, see [API_DOCUMENTATION.md](API_DOCUMENTATION.md)*