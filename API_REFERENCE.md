# API Reference - Advanced Programming Notes

## Quick Reference

This document provides a concise reference for all public APIs, classes, and methods in the Advanced Programming Notes solution.

## Lesson 3 APIs - Reference Semantics

### Methods

#### X(ref int a)
**Namespace**: Global  
**Access Modifier**: Internal  
**Type**: Method  

```csharp
void X(ref int a)
```

**Description**: Demonstrates reference parameter modification.

**Parameters**:
- `a` (ref int): Integer passed by reference

**Usage**:
```csharp
int value = 5;
X(ref value); // value becomes 124
```

---

#### X(ref int a) - ref return version
**Namespace**: Global  
**Access Modifier**: Internal  
**Type**: Method  

```csharp
ref int X(ref int a)
```

**Description**: Demonstrates reference return functionality.

**Parameters**:
- `a` (ref int): Integer passed by reference

**Returns**:
- `ref int`: Reference to the modified parameter

**Usage**:
```csharp
int b = 5;
ref int c = ref X(ref b); // c references same memory as b
int y = X(ref b);         // y gets the value, not reference
```

## Lesson 4 APIs - Operator Overloading

### Classes

#### Student Class
**Namespace**: Global  
**Access Modifier**: Public  
**Type**: Class  

```csharp
public class Student
```

**Properties**:
| Name | Type | Access | Description |
|------|------|--------|-------------|
| Name | string | get; set; | Student's name |
| Books | List\<Book\> | get; set; | Collection of student's books |

**Constructor**:
```csharp
public Student() // Implicit parameterless constructor
```

**Usage**:
```csharp
Student student = new()
{
    Name = "John Doe"
};
```

---

#### Book Class
**Namespace**: Global  
**Access Modifier**: Public  
**Type**: Class  

```csharp
public class Book
```

**Properties**:
| Name | Type | Access | Description |
|------|------|--------|-------------|
| Name | string | get; set; | Book title |
| Author | string | get; set; | Book author |

**Operators**:
| Operator | Signature | Description |
|----------|-----------|-------------|
| + | `Student operator +(Book book, Student student)` | Adds book to student's collection |

**Constructor**:
```csharp
public Book() // Implicit parameterless constructor
```

**Usage**:
```csharp
Book book = new()
{
    Name = "Clean Code",
    Author = "Robert Martin"
};
```

### Operators

#### Addition Operator (+)
**Class**: Book  
**Access Modifier**: Public Static  
**Type**: Operator Overload  

```csharp
public static Student operator +(Book book, Student student)
```

**Description**: Adds a book to a student's book collection.

**Parameters**:
- `book` (Book): The book to add (left operand)
- `student` (Student): The student receiving the book (right operand)

**Returns**:
- `Student`: The student object with the book added to their Books collection

**Side Effects**:
- Modifies the `student.Books` collection by adding the `book`

**Usage**:
```csharp
Student student = new() { Name = "Alice" };
Book book = new() { Name = "1984", Author = "George Orwell" };

Student result = book + student; // book is added to student's Books list
```

**Thread Safety**: Not thread-safe

**Exceptions**: None explicitly thrown

## Reference Semantics APIs

### Keywords and Modifiers

#### ref keyword
**Type**: Language Keyword  
**Usage Contexts**:
- Method parameters
- Return types  
- Local variables
- Field access

**Syntax Examples**:
```csharp
// Parameter
void Method(ref int parameter) { }

// Return type
ref int Method() { return ref field; }

// Local variable
ref int local = ref otherVariable;

// Method call
Method(ref variable);
ref int result = ref Method();
```

## Usage Patterns

### Pattern 1: Reference Parameter Modification
```csharp
void Swap(ref int a, ref int b)
{
    int temp = a;
    a = b;
    b = temp;
}

// Usage
int x = 1, y = 2;
Swap(ref x, ref y); // x=2, y=1
```

### Pattern 2: Reference Return for Performance
```csharp
private int[] _array = new int[1000];

public ref int GetElement(int index)
{
    return ref _array[index];
}

// Usage
ref int element = ref GetElement(500);
element = 42; // Directly modifies array[500]
```

### Pattern 3: Operator Overloading for Domain Logic
```csharp
public static Student operator +(Book book, Student student)
{
    student.Books.Add(book);
    return student;
}

// Usage
Student enrichedStudent = book1 + book2 + student;
```

## Error Codes and Common Issues

### Compilation Errors

| Error Code | Description | Solution |
|------------|-------------|----------|
| CS8156 | Cannot return local by reference | Return field or parameter reference instead |
| CS8168 | Cannot return local by reference | Ensure returned reference has proper scope |
| CS1620 | Argument must be passed with ref keyword | Add ref keyword to method call |

### Runtime Considerations

- **Performance**: Reference semantics avoid copying but require careful lifetime management
- **Memory**: References don't extend object lifetime - be aware of scope
- **Thread Safety**: Reference operations are not inherently thread-safe

## Version Information

- **.NET Version**: 8.0
- **C# Language Version**: 12.0 (implicit)
- **Nullable Reference Types**: Enabled
- **Implicit Usings**: Enabled

## See Also

- [API_DOCUMENTATION.md](API_DOCUMENTATION.md) - Comprehensive documentation with examples
- [Microsoft Docs - ref keyword](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/ref)
- [Microsoft Docs - Operator Overloading](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/operator-overloading)