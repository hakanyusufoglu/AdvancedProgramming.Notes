# Gelişmiş Programlama Notları - API Dokümantasyonu

## Genel Bakış

Bu dokümantasyon, referans semantiği ve operatör aşırı yükleme gibi gelişmiş C# programlama kavramlarını gösteren Gelişmiş Programlama Notları çözümünü kapsamaktadır. Çözüm .NET 8.0 üzerine kurulmuştur ve iki ana ders projesinden oluşmaktadır.

## Çözüm Yapısı

```
AdvancedProgramming.Notes/
├── AdvancedProgramming.Notes.sln
├── AdvancedProgramming.Notes.Lesson3/
│   ├── AdvancedProgramming.Notes.Lesson3.csproj
│   └── Program.cs
└── AdvancedProgramming.Notes.Lesson4/
    ├── AdvancedProgramming.Notes.Lesson4.csproj
    └── Program.cs
```

## Ders 3: Referans Semantiği ve ref Anahtar Kelimeleri

### Proje Bilgileri
- **Framework**: .NET 8.0
- **Proje Türü**: Konsol Uygulaması
- **Özellikler**: ref anahtar kelimesi, ref return, ref locals

### Gösterilen Temel Kavramlar

#### 1. Referans Parametreleri (`ref` anahtar kelimesi)

`ref` anahtar kelimesi, değer tiplerini referans ile geçirmenize olanak tanır ve metotların orijinal değişkeni değiştirmesini sağlar.

**Kullanım Örneği:**
```csharp
int b = 5;
X(ref b);
Console.WriteLine(b); // Çıktı: 124

void X(ref int a)
{
    a = 124; // Orijinal değişkeni değiştirir
}
```

**Önemli Noktalar:**
- Hem metot parametresi hem de argüman `ref` anahtar kelimesini kullanmalıdır
- Değişken metoda geçirilmeden önce başlatılmalıdır
- Metot orijinal değişkenin değerini değiştirebilir

#### 2. Referans Dönüş Değerleri (`ref return`)

Referans dönüş, metotların bir değişkenin değeri yerine referansını döndürmesine olanak tanır, bu da orijinal verinin doğrudan manipülasyonunu mümkün kılar.

**Kullanım Örneği:**
```csharp
int b = 5;

// c, b ile aynı bellek konumuna referans tutar
ref int c = ref X(ref b);

// Doğrudan atama (referans değil)
int y = X(ref b);

ref int X(ref int a)
{
    a = 124;
    return ref a; // Parametrenin referansını döndürür
}
```

**Önemli Noktalar:**
- Hem metot imzasında hem de return ifadesinde `ref` kullanın
- Döndürülen referansı atarken çağıran taraf `ref` kullanmalıdır
- Yerel değişkenlerin referansları döndürülemez
- Derin kopyaları önleyerek performans optimizasyonu için yararlıdır

#### 3. Referans Yerelleri (`ref locals`)

Referans yereller, başka bir değişkenin bellek konumuna referans eden yerel bir değişken oluşturmanıza olanak tanır.

**Kullanım Örneği:**
```csharp
char d = 'a';
ref char e = ref d; // e, d'ye bir referanstır

// e'yi değiştirmek d'yi de değiştirir
e = 'b';
Console.WriteLine(d); // Çıktı: 'b'
```

**Önemli Noktalar:**
- Yerel değişkeni bildirirken `ref` anahtar kelimesini kullanın
- Referans yerel üzerindeki değişiklikler orijinal değişkeni etkiler
- Her iki değişken de aynı bellek konumunu işaret eder

### Performans Faydaları
- **Azaltılmış Bellek Tahsisi**: Büyük veri yapılarının kopyalarını oluşturmaktan kaçınır
- **Gelişmiş Performans**: Değer kopyalama yerine doğrudan bellek erişimi
- **Optimize İşlemler**: Özellikle büyük struct'lar ve diziler için faydalıdır

### Sınırlamalar ve Dikkat Edilmesi Gerekenler
- Metotlardan yerel değişkenlerin referansları döndürülemez
- Referans parametreleri geçirilmeden önce başlatılmalıdır
- İstenmeyen yan etkilerden kaçınmak için referans semantiği konusunda dikkatli olun

## Ders 4: Operatör Aşırı Yükleme

### Proje Bilgileri
- **Framework**: .NET 8.0
- **Proje Türü**: Konsol Uygulaması
- **Özellikler**: Özel operatör aşırı yükleme

### Genel Sınıflar ve API'lar

#### 1. Student (Öğrenci) Sınıfı

```csharp
public class Student
{
    public string Name { get; set; }
    public List<Book> Books { get; set; } = new();
}
```

**Özellikler:**
- `Name` (string): Öğrencinin adını alır veya ayarlar
- `Books` (List<Book>): Öğrencinin sahip olduğu kitap koleksiyonunu alır veya ayarlar

**Kullanım Örneği:**
```csharp
Student student = new()
{
    Name = "Hakan"
};
```

#### 2. Book (Kitap) Sınıfı

```csharp
public class Book
{
    public string Name { get; set; }
    public string Author { get; set; }
    
    public static Student operator +(Book book, Student student)
}
```

**Özellikler:**
- `Name` (string): Kitabın başlığını alır veya ayarlar
- `Author` (string): Kitabın yazarını alır veya ayarlar

**Operatörler:**
- `+` (Toplama): Bir kitabı öğrencinin koleksiyonuna ekler

**Kullanım Örneği:**
```csharp
Book book = new()
{
    Name = "Suç ve Ceza",
    Author = "Dostoyevski"
};
```

### Operatör Aşırı Yükleme API'sı

#### Toplama Operatörü (+)

**İmza:**
```csharp
public static Student operator +(Book book, Student student)
```

**Parametreler:**
- `book` (Book): Öğrencinin koleksiyonuna eklenecek kitap
- `student` (Student): Kitabı alacak öğrenci

**Döndürür:**
- `Student`: Kitabın koleksiyonuna eklendiği değiştirilmiş öğrenci nesnesi

**Kullanım Örneği:**
```csharp
Student student = new() { Name = "Hakan" };
Book book = new() { Name = "Suç ve Ceza", Author = "Dostoyevski" };

// Kitabı öğrencinin koleksiyonuna ekle
Student result = book + student;

// Öğrenci artık kitap listesinde kitaba sahip
Console.WriteLine($"Öğrenci {result.Name} {result.Books.Count} kitaba sahip");
```

**Önemli Noktalar:**
- Operatör `public static` olarak bildirilmelidir
- Parametre sırası önemlidir: `Book` solda, `Student` sağda olmalıdır
- İşlem öğrencinin Books koleksiyonunu değiştirir
- Metot zincirleme için değiştirilmiş öğrenci nesnesini döndürür

### Uygulama Kılavuzları

#### Operatör Aşırı Yükleme En İyi Uygulamaları
1. **Sezgisel Davranış**: Aşırı yüklenmiş operatör doğal ve beklenen şekilde davranmalıdır
2. **Tutarlılık**: Bir operatörü aşırı yüklüyorsanız, ilgili operatörleri de aşırı yüklemeyi düşünün
3. **Değişmezlik Değerlendirmesi**: İşlemin mevcut nesneleri değiştirmesi mi yoksa yenilerini oluşturması mı gerektiğini düşünün
4. **Parametre Sırası**: Parametre sırasında tutarlı olun ve açıkça belgelendirin

#### Desteklenen Aşırı Yüklenebilir Operatörler
- Aritmetik: `+`, `-`, `*`, `/`, `%`
- Karşılaştırma: `==`, `!=`, `<`, `>`, `<=`, `>=`
- Mantıksal: `!`, `&`, `|`, `^`
- Diğerleri: `++`, `--`, `true`, `false`

## Başlangıç

### Ön Koşullar
- .NET 8.0 SDK veya daha yenisi
- Visual Studio 2022 veya C# eklentili Visual Studio Code

### Çözümü Derleme

```bash
# Proje dizinine gidin
cd /path/to/AdvancedProgramming.Notes

# Bağımlılıkları geri yükle
dotnet restore

# Tüm çözümü derle
dotnet build

# Ders 3'ü çalıştır
dotnet run --project AdvancedProgramming.Notes.Lesson3

# Ders 4'ü çalıştır
dotnet run --project AdvancedProgramming.Notes.Lesson4
```

### Proje Konfigürasyonu

Her iki proje de şunlarla yapılandırılmıştır:
- **Hedef Framework**: .NET 8.0
- **Çıktı Türü**: Konsol Uygulaması
- **Implicit Usings**: Etkin
- **Nullable Reference Types**: Etkin

## Gelişmiş Kullanım Senaryoları

### Senaryo 1: Performans Kritik Referans İşlemleri

```csharp
// Büyük struct işlemleri için ref return kullanın
public ref LargeStruct GetLargeStructReference()
{
    return ref _largeStructField;
}

// Büyük veri yapılarının kopyalanmasından kaçının
ref LargeStruct structRef = ref GetLargeStructReference();
structRef.SomeProperty = newValue; // Doğrudan değişiklik
```

### Senaryo 2: Operatör Aşırı Yükleme ile Akıcı API

```csharp
// Birden fazla kitap eklemeyi zincirleme
Student student = new() { Name = "Okuyucu" };
Book book1 = new() { Name = "Kitap 1", Author = "Yazar 1" };
Book book2 = new() { Name = "Kitap 2", Author = "Yazar 2" };

// Akıcı stil zincirleme
Student result = (book1 + (book2 + student));
```

### Senaryo 3: Dizi Manipülasyonu için Referans Yereller

```csharp
int[] array = { 1, 2, 3, 4, 5 };
ref int middleElement = ref array[2];

// Dizi indeksleme olmadan doğrudan değişiklik
middleElement = 100;
Console.WriteLine(array[2]); // Çıktı: 100
```

## Yaygın Hatalar ve Çözümler

### 1. Referans Dönüş Hataları

**Problem**: Yerel değişkenin referansını döndürme
```csharp
// ❌ Bu derleme hatası verecektir
ref int BadMethod()
{
    int localVar = 5;
    return ref localVar; // Hata: Yerel değişken referans olarak döndürülemez
}
```

**Çözüm**: Alan veya parametre referansı döndürün
```csharp
// ✅ Doğru yaklaşım
private int _field = 5;
ref int GoodMethod()
{
    return ref _field; // Tamam: Alan referansı döndürülüyor
}
```

### 2. Operatör Aşırı Yükleme Parametre Sırası

**Problem**: Tutarsız parametre sırası
```csharp
// ❌ Kafa karıştırıcı parametre sırası
public static Student operator +(Student student, Book book)
// karşı
public static Student operator +(Book book, Student student)
```

**Çözüm**: Belgelendirin ve tutarlı sıralamayı koruyun
```csharp
// ✅ İyi belgelenmiş parametre sırası
/// <summary>
/// Bir kitabı öğrencinin koleksiyonuna ekler.
/// </summary>
/// <param name="book">Eklenecek kitap (sol operand)</param>
/// <param name="student">Kitabı alan öğrenci (sağ operand)</param>
/// <returns>Kitap eklenmiş öğrenci</returns>
public static Student operator +(Book book, Student student)
```

## Test Örnekleri

### Birim Test Örnekleri

```csharp
[Test]
public void RefReturn_OriginalVariableModifies()
{
    // Arrange
    int original = 5;
    
    // Act
    ref int reference = ref GetReference(ref original);
    reference = 10;
    
    // Assert
    Assert.AreEqual(10, original);
}

[Test]
public void BookAddition_AddsBookToStudent()
{
    // Arrange
    var student = new Student { Name = "Test Öğrencisi" };
    var book = new Book { Name = "Test Kitabı", Author = "Test Yazarı" };
    
    // Act
    var result = book + student;
    
    // Assert
    Assert.AreEqual(1, result.Books.Count);
    Assert.AreEqual("Test Kitabı", result.Books[0].Name);
}
```

## Performans Değerlendirmeleri

### Bellek Yönetimi
- **ref return**: Büyük değer tiplerinin gereksiz kopyalanmasını ortadan kaldırır
- **ref parametreler**: Dönüş değeri ek yükü olmadan değişiklik sağlar
- **ref locals**: Bellek konumlarına doğrudan erişim sağlar

### Benchmarking Örneği
```csharp
// ref olmadan (veriyi kopyalar)
LargeStruct ProcessWithoutRef(LargeStruct data) => ModifyStruct(data);

// ref ile (kopyalama yok)
void ProcessWithRef(ref LargeStruct data) => ModifyStruct(ref data);
```

## Sonuç

Bu Gelişmiş Programlama Notları çözümü, yüksek performanslı, sürdürülebilir kod yazmak için temel olan kritik C# kavramlarını göstermektedir. Referans semantiği özellikleri bellek kullanımını ve performansı optimize etmeye yardımcı olurken, operatör aşırı yükleme daha sezgisel ve ifadeli API'lar sağlar.

### Önemli Çıkarımlar
1. Performans kritik senaryolar için `ref` anahtar kelimesini akıllıca kullanın
2. Operatör aşırı yükleme sezgisel ve iyi belgelenmiş olmalıdır
3. Nesne değişebilirliği üzerinde referans semantiğinin etkilerini her zaman düşünün
4. Özel operatörler uygularken kenar durumları kapsamlı şekilde test edin

Daha gelişmiş senaryolar ve desenler için ilgili konuları keşfetmeyi düşünün:
- Daha da gelişmiş bellek yönetimi için Span<T> ve Memory<T>
- Operatör aşırı yükleme ile özel koleksiyon türleri
- Stack-only türler için Ref struct'lar
- Pointer manipülasyonu için Unsafe kod