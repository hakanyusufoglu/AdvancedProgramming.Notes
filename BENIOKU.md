# Gelişmiş Programlama Notları

Referans semantiği ve operatör aşırı yükleme gibi gelişmiş C# programlama kavramlarını gösteren .NET 8.0 eğitim çözümü.

## 🚀 Hızlı Başlangıç

### Ön Koşullar
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) veya daha yenisi
- Visual Studio 2022, Visual Studio Code veya JetBrains Rider

### Projeleri Çalıştırma

```bash
# Projeyi klonlayın ve dizine gidin
cd AdvancedProgramming.Notes

# Bağımlılıkları geri yükleyin
dotnet restore

# Ders 3 - Referans Semantiği
dotnet run --project AdvancedProgramming.Notes.Lesson3

# Ders 4 - Operatör Aşırı Yükleme  
dotnet run --project AdvancedProgramming.Notes.Lesson4

# Tüm çözümü derleyin
dotnet build
```

## 📚 Ne Öğreneceksiniz

### Ders 3: Referans Semantiği
- **`ref` anahtar kelimesi**: Değer türlerini referans ile geçirme
- **`ref` return**: Performans için kopyalar yerine referans döndürme
- **`ref` locals**: Diğer değişkenlere referans eden yerel değişkenler oluşturma
- **Performans optimizasyonu**: Büyük veri yapılarının gereksiz kopyalanmasından kaçınma

### Ders 4: Operatör Aşırı Yükleme
- **Özel operatörler**: Operatörler için domain-spesifik davranış uygulama
- **Tür tasarımı**: Tanıdık sözdizimi kullanarak sezgisel API'lar oluşturma
- **En iyi uygulamalar**: Parametre sırası ve operatör semantiği

## 🔧 Proje Yapısı

```
AdvancedProgramming.Notes/
├── 📁 AdvancedProgramming.Notes.Lesson3/    # Referans semantiği örnekleri
│   ├── Program.cs                           # ref, ref return, ref locals
│   └── AdvancedProgramming.Notes.Lesson3.csproj
├── 📁 AdvancedProgramming.Notes.Lesson4/    # Operatör aşırı yükleme örnekleri  
│   ├── Program.cs                           # Student + Book operatörü
│   └── AdvancedProgramming.Notes.Lesson4.csproj
├── AdvancedProgramming.Notes.sln            # Çözüm dosyası
├── 📖 API_DOKUMANTASYONU.md                 # Kapsamlı dokümantasyon
├── 📋 API_REFERANSI.md                      # Hızlı API referansı
└── 📄 BENIOKU.md                            # Bu dosya
```

## 💡 Kod Örnekleri

### Referans Semantiği (Ders 3)

```csharp
// Temel ref parametre
int deger = 5;
DegeriDegistir(ref deger);
Console.WriteLine(deger); // Çıktı: 124

void DegeriDegistir(ref int a) => a = 124;

// Performans için ref return
int[] buyukDizi = new int[1000];
ref int eleman = ref DiziElemaniAl(buyukDizi, 500);
eleman = 42; // Doğrudan buyukDizi[500]'ü değiştirir

ref int DiziElemaniAl(int[] dizi, int indeks) => ref dizi[indeks];

// ref locals
char orijinal = 'a';
ref char takmaAd = ref orijinal;
takmaAd = 'b'; // orijinal artık 'b'
```

### Operatör Aşırı Yükleme (Ders 4)

```csharp
// Özel operatörlü sınıfları tanımlama
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

// Kullanım - domain işlemleri için sezgisel sözdizimi
Student student = new() { Name = "Ayşe" };
Book book = new() { Name = "Temiz Kod", Author = "Robert Martin" };

Student zenginlestirilmisOgrenci = book + student; // Kitap öğrencinin koleksiyonuna eklendi
```

## 📖 Dokümantasyon

| Belge | Açıklama |
|----------|-------------|
| [📖 API Dokümantasyonu](API_DOKUMANTASYONU.md) | Örnekler, en iyi uygulamalar ve gelişmiş senaryolarla kapsamlı kılavuz |
| [📋 API Referansı](API_REFERANSI.md) | Tüm genel API'lar, sınıflar ve metotlar için hızlı referans |

## 🎯 Önemli Özellikler

### Performans Faydaları
- ✅ **Sıfır-kopya işlemleri** `ref` semantiği ile
- ✅ **Büyük veri yapıları için doğrudan bellek erişimi**  
- ✅ **Savunma amaçlı kopyalamadan kaçınarak azaltılmış tahsisler**
- ✅ **Referans parametreleri kullanarak optimize algoritmalar**

### Geliştirici Deneyimi
- ✅ **Operatör aşırı yükleme ile sezgisel API'lar**
- ✅ **Derleme zamanı kontrolü ile tip-güvenli işlemler**
- ✅ **Domain-spesifik işlemler için tanıdık sözdizimi**
- ✅ **Örneklerle kapsamlı dokümantasyon**

## ⚠️ Önemli Notlar

### Referans Semantiği Tuzakları
- Yerel değişkenlerin referansları döndürülemez
- Referans parametreleri kullanılmadan önce başlatılmalıdır
- Nesne yaşam süresi ve kapsam konusunda dikkatli olun
- Doğal olarak thread-safe değildir

### Operatör Aşırı Yükleme Kılavuzları
- Operatörler sezgisel davranmalıdır
- Parametre sırası önemlidir ve belgelenmelidir
- İlgili operatörleri birlikte uygulamayı düşünün
- Mümkün olduğunda değişmez işlemleri tercih edin

## 🧪 Test Etme

API'lar için örnek birim testler:

```csharp
[Test]
public void RefReturn_OrijinalDegiskeniDegistirir()
{
    int orijinal = 5;
    ref int referans = ref ReferansAl(ref orijinal);
    referans = 10;
    
    Assert.AreEqual(10, orijinal);
}

[Test] 
public void KitapEkleme_KitabiOgrenciyeEkler()
{
    var student = new Student { Name = "Test Öğrencisi" };
    var book = new Book { Name = "Test Kitabı", Author = "Test Yazarı" };
    
    var result = book + student;
    
    Assert.AreEqual(1, result.Books.Count);
    Assert.AreEqual("Test Kitabı", result.Books[0].Name);
}
```

## 🔍 Gelişmiş Konular

Üretim kullanımı için keşfetmeyi düşünün:
- Gelişmiş bellek yönetimi için `Span<T>` ve `Memory<T>`
- Stack-only tahsis için `ref struct` türleri
- Operatör aşırı yükleme ile özel koleksiyon türleri
- Pointer manipülasyonu için Unsafe kod
- BenchmarkDotNet ile performans benchmarking

## 📝 Lisans

Bu eğitim projesi öğrenme amaçları için olduğu gibi sunulmaktadır.

## 🤝 Katkıda Bulunma

Bu bir eğitim deposudur. Gösterilen kavramları fork'layıp deneyebilirsiniz.

---

**İyi Öğrenmeler! 🎓**

*Detaylı API dokümantasyonu ve gelişmiş kullanım senaryoları için [API_DOKUMANTASYONU.md](API_DOKUMANTASYONU.md) dosyasına bakın*

## 🎓 Eğitim Hedefleri

Bu proje aşağıdaki C# gelişmiş programlama kavramlarını öğretmeyi hedefler:

### Temel Kavramlar
- **Bellek Yönetimi**: Referans ve değer türleri arasındaki farklar
- **Performans Optimizasyonu**: Gereksiz kopyalamadan kaçınma teknikleri
- **API Tasarımı**: Kullanıcı dostu ve sezgisel arayüzler oluşturma
- **Tip Güvenliği**: Derleme zamanında hata yakalama

### Pratik Uygulamalar
- **Sistem Programlama**: Düşük seviyeli bellek manipülasyonu
- **Kütüphane Geliştirme**: Yeniden kullanılabilir bileşenler
- **Performans Kritik Uygulamalar**: Oyun geliştirme, veri işleme
- **Domain Modeling**: İş kurallarını kodda ifade etme

## 🛠️ Geliştirme Ortamı Kurulumu

### Visual Studio 2022
1. "ASP.NET ve web geliştirme" iş yükünü yükleyin
2. ".NET masaüstü geliştirme" iş yükünü yükleyin
3. C# 12.0 dil özelliklerini etkinleştirin

### Visual Studio Code
1. C# eklentisini yükleyin
2. .NET Core Test Explorer eklentisini yükleyin
3. IntelliCode eklentisini yükleyin (isteğe bağlı)

### JetBrains Rider
1. .NET geliştirme eklentilerini etkinleştirin
2. Code inspection ayarlarını yapılandırın
3. Debugging yapılandırmasını kurun

## 📊 Performans Benchmarkları

Referans semantiği kullanımının performans etkisini görmek için:

```bash
# Benchmark projesi oluşturun (isteğe bağlı)
dotnet new console -n PerformanceBenchmarks
cd PerformanceBenchmarks
dotnet add package BenchmarkDotNet
```

## 🔧 Sorun Giderme

### Yaygın Derleme Hataları
- **CS8156**: `ref return` ile yerel değişken döndürme
- **CS1620**: `ref` parametresi eksik
- **CS0165**: Başlatılmamış değişken kullanımı

### Çalışma Zamanı Sorunları
- **NullReferenceException**: Null referans kontrolleri
- **IndexOutOfRangeException**: Dizi sınır kontrolleri
- **InvalidOperationException**: Geçersiz durum işlemleri

Daha fazla yardım için [API_DOKUMANTASYONU.md](API_DOKUMANTASYONU.md) dosyasındaki "Yaygın Hatalar ve Çözümler" bölümüne bakın.