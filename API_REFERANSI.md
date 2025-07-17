# API Referansı - Gelişmiş Programlama Notları

## Hızlı Referans

Bu belge, Gelişmiş Programlama Notları çözümündeki tüm genel API'lar, sınıflar ve metotlar için özlü bir referans sağlar.

## Ders 3 API'ları - Referans Semantiği

### Metotlar

#### X(ref int a)
**Namespace**: Global  
**Erişim Belirleyici**: Internal  
**Tür**: Metot  

```csharp
void X(ref int a)
```

**Açıklama**: Referans parametre değişikliğini gösterir.

**Parametreler**:
- `a` (ref int): Referans ile geçirilen tamsayı

**Kullanım**:
```csharp
int deger = 5;
X(ref deger); // deger 124 olur
```

---

#### X(ref int a) - ref return versiyonu
**Namespace**: Global  
**Erişim Belirleyici**: Internal  
**Tür**: Metot  

```csharp
ref int X(ref int a)
```

**Açıklama**: Referans dönüş fonksiyonalitesini gösterir.

**Parametreler**:
- `a` (ref int): Referans ile geçirilen tamsayı

**Döndürür**:
- `ref int`: Değiştirilmiş parametrenin referansı

**Kullanım**:
```csharp
int b = 5;
ref int c = ref X(ref b); // c, b ile aynı belleği referans eder
int y = X(ref b);         // y değeri alır, referans almaz
```

## Ders 4 API'ları - Operatör Aşırı Yükleme

### Sınıflar

#### Student (Öğrenci) Sınıfı
**Namespace**: Global  
**Erişim Belirleyici**: Public  
**Tür**: Sınıf  

```csharp
public class Student
```

**Özellikler**:
| Ad | Tür | Erişim | Açıklama |
|------|------|--------|-------------|
| Name | string | get; set; | Öğrencinin adı |
| Books | List\<Book\> | get; set; | Öğrencinin kitap koleksiyonu |

**Kurucu**:
```csharp
public Student() // Implicit parametresiz kurucu
```

**Kullanım**:
```csharp
Student student = new()
{
    Name = "Ahmet Yılmaz"
};
```

---

#### Book (Kitap) Sınıfı
**Namespace**: Global  
**Erişim Belirleyici**: Public  
**Tür**: Sınıf  

```csharp
public class Book
```

**Özellikler**:
| Ad | Tür | Erişim | Açıklama |
|------|------|--------|-------------|
| Name | string | get; set; | Kitap başlığı |
| Author | string | get; set; | Kitap yazarı |

**Operatörler**:
| Operatör | İmza | Açıklama |
|----------|-----------|-------------|
| + | `Student operator +(Book book, Student student)` | Kitabı öğrencinin koleksiyonuna ekler |

**Kurucu**:
```csharp
public Book() // Implicit parametresiz kurucu
```

**Kullanım**:
```csharp
Book book = new()
{
    Name = "Temiz Kod",
    Author = "Robert Martin"
};
```

### Operatörler

#### Toplama Operatörü (+)
**Sınıf**: Book  
**Erişim Belirleyici**: Public Static  
**Tür**: Operatör Aşırı Yüklemesi  

```csharp
public static Student operator +(Book book, Student student)
```

**Açıklama**: Bir kitabı öğrencinin kitap koleksiyonuna ekler.

**Parametreler**:
- `book` (Book): Eklenecek kitap (sol operand)
- `student` (Student): Kitabı alan öğrenci (sağ operand)

**Döndürür**:
- `Student`: Books koleksiyonuna kitap eklenmiş öğrenci nesnesi

**Yan Etkiler**:
- `student.Books` koleksiyonunu `book` ekleyerek değiştirir

**Kullanım**:
```csharp
Student student = new() { Name = "Ayşe" };
Book book = new() { Name = "1984", Author = "George Orwell" };

Student result = book + student; // kitap öğrencinin Books listesine eklenir
```

**Thread Güvenliği**: Thread-safe değil

**İstisnalar**: Açıkça fırlatılan istisna yok

## Referans Semantiği API'ları

### Anahtar Kelimeler ve Belirleyiciler

#### ref anahtar kelimesi
**Tür**: Dil Anahtar Kelimesi  
**Kullanım Bağlamları**:
- Metot parametreleri
- Dönüş türleri  
- Yerel değişkenler
- Alan erişimi

**Sözdizimi Örnekleri**:
```csharp
// Parametre
void Metot(ref int parametre) { }

// Dönüş türü
ref int Metot() { return ref alan; }

// Yerel değişken
ref int yerel = ref digerDegisken;

// Metot çağrısı
Metot(ref degisken);
ref int sonuc = ref Metot();
```

## Kullanım Desenleri

### Desen 1: Referans Parametre Değişikliği
```csharp
void Degistir(ref int a, ref int b)
{
    int temp = a;
    a = b;
    b = temp;
}

// Kullanım
int x = 1, y = 2;
Degistir(ref x, ref y); // x=2, y=1
```

### Desen 2: Performans için Referans Dönüş
```csharp
private int[] _dizi = new int[1000];

public ref int ElemanAl(int indeks)
{
    return ref _dizi[indeks];
}

// Kullanım
ref int eleman = ref ElemanAl(500);
eleman = 42; // Doğrudan dizi[500]'ü değiştirir
```

### Desen 3: Domain Lojiği için Operatör Aşırı Yükleme
```csharp
public static Student operator +(Book book, Student student)
{
    student.Books.Add(book);
    return student;
}

// Kullanım
Student zenginlestirilmisOgrenci = book1 + book2 + student;
```

## Hata Kodları ve Yaygın Sorunlar

### Derleme Hataları

| Hata Kodu | Açıklama | Çözüm |
|------------|-------------|----------|
| CS8156 | Yerel değişken referans olarak döndürülemez | Bunun yerine alan veya parametre referansı döndürün |
| CS8168 | Yerel değişken referans olarak döndürülemez | Döndürülen referansın uygun kapsamda olduğundan emin olun |
| CS1620 | Argüman ref anahtar kelimesi ile geçirilmelidir | Metot çağrısına ref anahtar kelimesi ekleyin |

### Çalışma Zamanı Değerlendirmeleri

- **Performans**: Referans semantiği kopyalamayı önler ancak dikkatli yaşam süresi yönetimi gerektirir
- **Bellek**: Referanslar nesne yaşam süresini uzatmaz - kapsam farkında olun
- **Thread Güvenliği**: Referans işlemleri doğal olarak thread-safe değildir

## Versiyon Bilgileri

- **.NET Versiyonu**: 8.0
- **C# Dil Versiyonu**: 12.0 (implicit)
- **Nullable Reference Types**: Etkin
- **Implicit Usings**: Etkin

## Ayrıca Bakınız

- [API_DOKUMANTASYONU.md](API_DOKUMANTASYONU.md) - Örneklerle kapsamlı dokümantasyon
- [Microsoft Docs - ref anahtar kelimesi](https://docs.microsoft.com/tr-tr/dotnet/csharp/language-reference/keywords/ref)
- [Microsoft Docs - Operatör Aşırı Yükleme](https://docs.microsoft.com/tr-tr/dotnet/csharp/language-reference/operators/operator-overloading)

## Hızlı Başvuru Tablosu

### ref Anahtar Kelimesi Kullanımları

| Kullanım | Sözdizimi | Açıklama |
|----------|-----------|----------|
| Parametre | `void Metot(ref int x)` | Değişkeni referans ile geçir |
| Dönüş | `ref int Metot()` | Referans döndür |
| Yerel | `ref int y = ref x` | Yerel referans değişkeni |
| Çağrı | `Metot(ref deger)` | Referans ile çağır |

### Operatör Aşırı Yükleme Kuralları

| Kural | Açıklama |
|-------|----------|
| public static | Operatörler public ve static olmalıdır |
| Parametre sırası | Operand sırası tutarlı olmalıdır |
| Sezgisel davranış | Beklenen şekilde çalışmalıdır |
| Çift operatörler | == ile != birlikte implement edilmelidir |

## Performans İpuçları

### ref Kullanımı
- ✅ Büyük struct'lar için ref kullanın
- ✅ Kopyalama maliyeti yüksek olan türler için ref return
- ❌ Küçük değer türleri için ref kullanmayın
- ❌ ref'i gereksiz yere kullanmayın

### Operatör Aşırı Yükleme
- ✅ Domain-specific işlemler için kullanın
- ✅ Tutarlı ve tahmin edilebilir davranış
- ❌ Kafa karıştırıcı operatör anlamları
- ❌ Karmaşık iş lojiğini operatörlere koymayın