# Kod Örnekleri - Gelişmiş Programlama Notları

Bu belge, Gelişmiş Programlama Notları çözümünde kapsanan kavramların pratik kullanımını gösteren kapsamlı kod örnekleri sağlar.

## İçindekiler
- [Referans Semantiği Örnekleri](#referans-semantiği-örnekleri)
- [Operatör Aşırı Yükleme Örnekleri](#operatör-aşırı-yükleme-örnekleri)
- [Performans Karşılaştırmaları](#performans-karşılaştırmaları)
- [Gerçek Dünya Senaryoları](#gerçek-dünya-senaryoları)
- [Anti-Desenler ve Tuzaklar](#anti-desenler-ve-tuzaklar)

## Referans Semantiği Örnekleri

### Temel ref Parametre Kullanımı

```csharp
// Örnek 1: Basit değer değiştirme
public void TemelRefOrnegi()
{
    int orijinalDeger = 10;
    Console.WriteLine($"Önce: {orijinalDeger}"); // Çıktı: Önce: 10
    
    ReferansIleDegistir(ref orijinalDeger);
    Console.WriteLine($"Sonra: {orijinalDeger}");  // Çıktı: Sonra: 25
}

void ReferansIleDegistir(ref int deger)
{
    deger = deger * 2 + 5;
}

// Örnek 2: Değerleri takas etme
public void TakasOrnegi()
{
    int a = 5, b = 10;
    Console.WriteLine($"Takas öncesi: a={a}, b={b}"); // Takas öncesi: a=5, b=10
    
    Takas(ref a, ref b);
    Console.WriteLine($"Takas sonrası: a={a}, b={b}");  // Takas sonrası: a=10, b=5
}

void Takas(ref int x, ref int y)
{
    int temp = x;
    x = y;
    y = temp;
}
```

### ref return Örnekleri

```csharp
public class DiziYoneticisi
{
    private int[] _veri = new int[100];
    
    // Örnek 1: Doğrudan dizi elemanı erişimi
    public ref int ElemanAl(int indeks)
    {
        if (indeks < 0 || indeks >= _veri.Length)
            throw new IndexOutOfRangeException();
            
        return ref _veri[indeks];
    }
    
    // Örnek 2: En büyük elemanın referansını bulma ve döndürme
    public ref int EnBuyukElemaniAl()
    {
        if (_veri.Length == 0)
            throw new InvalidOperationException("Dizi boş");
            
        int enBuyukIndeks = 0;
        for (int i = 1; i < _veri.Length; i++)
        {
            if (_veri[i] > _veri[enBuyukIndeks])
                enBuyukIndeks = i;
        }
        return ref _veri[enBuyukIndeks];
    }
}

// Kullanım örnekleri
public void RefReturnKullanimi()
{
    var yonetici = new DiziYoneticisi();
    
    // Doğrudan eleman değiştirme
    ref int eleman50 = ref yonetici.ElemanAl(50);
    eleman50 = 999; // Doğrudan dizi elemanını değiştirir
    
    // En büyük elemanı doğrudan değiştirme
    ref int enBuyukEleman = ref yonetici.EnBuyukElemaniAl();
    enBuyukEleman = 1000; // Artık bu eleman kesinlikle en büyük
    
    // Değer alma vs referans alma
    int sadeceDeger = yonetici.ElemanAl(25);     // Değerin kopyasını alır
    ref int referans = ref yonetici.ElemanAl(25); // Elemanın referansını alır
    
    sadeceDeger = 500;  // Sadece yerel değişkeni değiştirir
    referans = 600;     // Gerçek dizi elemanını değiştirir
}
```

### ref locals Örnekleri

```csharp
public void RefYerellerOrnegi()
{
    // Örnek 1: Temel ref local
    int orijinalDegisken = 42;
    ref int takmaAdDegisken = ref orijinalDegisken;
    
    Console.WriteLine($"Orijinal: {orijinalDegisken}"); // Çıktı: Orijinal: 42
    takmaAdDegisken = 100;
    Console.WriteLine($"Takma ad değişikliği sonrası orijinal: {orijinalDegisken}"); // Çıktı: Orijinal: 100
    
    // Örnek 2: Dizilerle çalışma
    int[] sayilar = { 1, 2, 3, 4, 5 };
    ref int ortaSayi = ref sayilar[2];
    
    ortaSayi = 999;
    Console.WriteLine($"Ref local değişikliği sonrası dizi: [{string.Join(", ", sayilar)}]");
    // Çıktı: Ref local değişikliği sonrası dizi: [1, 2, 999, 4, 5]
    
    // Örnek 3: Koşullu ref ataması
    bool ilkiniKullan = true;
    int ilk = 10, ikinci = 20;
    ref int secilen = ref (ilkiniKullan ? ref ilk : ref ikinci);
    
    secilen = 50;
    Console.WriteLine($"İlk: {ilk}, İkinci: {ikinci}"); 
    // Çıktı: İlk: 50, İkinci: 20
}
```

## Operatör Aşırı Yükleme Örnekleri

### Temel Operatör Uygulaması

```csharp
// Geliştirilmiş Book ve Student sınıfları daha fazla operatörle
public class Book
{
    public string Name { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public int PageCount { get; set; }
    public decimal Price { get; set; }
    
    // Toplama: Kitabı öğrenciye ekle
    public static Student operator +(Book book, Student student)
    {
        student.Books.Add(book);
        return student;
    }
    
    // Eşitlik operatörleri
    public static bool operator ==(Book sol, Book sag)
    {
        if (ReferenceEquals(sol, sag)) return true;
        if (sol is null || sag is null) return false;
        return sol.Name == sag.Name && sol.Author == sag.Author;
    }
    
    public static bool operator !=(Book sol, Book sag) => !(sol == sag);
    
    // Karşılaştırma operatörleri (sayfa sayısına göre)
    public static bool operator <(Book sol, Book sag)
        => sol?.PageCount < sag?.PageCount;
        
    public static bool operator >(Book sol, Book sag)
        => sol?.PageCount > sag?.PageCount;
        
    public static bool operator <=(Book sol, Book sag)
        => sol < sag || sol == sag;
        
    public static bool operator >=(Book sol, Book sag)
        => sol > sag || sol == sag;
    
    public override bool Equals(object? obj) => obj is Book book && this == book;
    public override int GetHashCode() => HashCode.Combine(Name, Author);
    public override string ToString() => $"{Name} - {Author} ({PageCount} sayfa)";
}

public class Student
{
    public string Name { get; set; } = string.Empty;
    public List<Book> Books { get; set; } = new();
    public decimal ToplamHarcama { get; set; }
    
    // Çıkarma: Kitabı öğrenciden çıkar
    public static Student operator -(Student student, Book book)
    {
        student.Books.Remove(book);
        return student;
    }
    
    // İndeksleyici benzeri erişim
    public Book this[int indeks] => Books[indeks];
    
    public override string ToString() 
        => $"{Name} {Books.Count} kitaba sahip, {ToplamHarcama:C2} harcamış";
}
```

### Gelişmiş Operatör Kullanımı

```csharp
public void GelismisOperatorOrnekleri()
{
    // Kitapları oluştur
    var kitap1 = new Book 
    { 
        Name = "Temiz Kod", 
        Author = "Robert C. Martin", 
        PageCount = 464, 
        Price = 45.99m 
    };
    
    var kitap2 = new Book 
    { 
        Name = "Tasarım Desenleri", 
        Author = "Gang of Four", 
        PageCount = 395, 
        Price = 54.99m 
    };
    
    var kitap3 = new Book 
    { 
        Name = "Temiz Kod", 
        Author = "Robert C. Martin", 
        PageCount = 464, 
        Price = 45.99m 
    };
    
    // Öğrenci oluştur
    var student = new Student { Name = "Ahmet Yılmaz" };
    
    // + operatörünü kullanarak kitap ekleme
    student = kitap1 + student;
    student = kitap2 + student;
    
    Console.WriteLine(student); // Ahmet Yılmaz 2 kitaba sahip, ₺0,00 harcamış
    
    // - operatörünü kullanarak kitap çıkarma
    student = student - kitap1;
    Console.WriteLine(student); // Ahmet Yılmaz 1 kitaba sahip, ₺0,00 harcamış
    
    // Karşılaştırma operatörlerini kullanma
    Console.WriteLine($"kitap1 == kitap3: {kitap1 == kitap3}"); // True
    Console.WriteLine($"kitap1 > kitap2: {kitap1 > kitap2}");   // True (464 > 395 sayfa)
    Console.WriteLine($"kitap1 < kitap2: {kitap1 < kitap2}");   // False
    
    // İşlemleri zincirleme
    var baskaStudent = new Student { Name = "Ayşe Kaya" };
    baskaStudent = kitap1 + kitap2 + baskaStudent;
    Console.WriteLine(baskaStudent); // Ayşe Kaya 2 kitaba sahip, ₺0,00 harcamış
}
```

### Özel Matematiksel Operatörler

```csharp
public struct Vektor2D
{
    public double X { get; set; }
    public double Y { get; set; }
    
    public Vektor2D(double x, double y) => (X, Y) = (x, y);
    
    // Aritmetik operatörler
    public static Vektor2D operator +(Vektor2D sol, Vektor2D sag)
        => new(sol.X + sag.X, sol.Y + sag.Y);
        
    public static Vektor2D operator -(Vektor2D sol, Vektor2D sag)
        => new(sol.X - sag.X, sol.Y - sag.Y);
        
    public static Vektor2D operator *(Vektor2D vektor, double skaler)
        => new(vektor.X * skaler, vektor.Y * skaler);
        
    public static Vektor2D operator *(double skaler, Vektor2D vektor)
        => vektor * skaler;
        
    public static Vektor2D operator /(Vektor2D vektor, double skaler)
        => new(vektor.X / skaler, vektor.Y / skaler);
    
    // Tekli operatörler
    public static Vektor2D operator -(Vektor2D vektor)
        => new(-vektor.X, -vektor.Y);
        
    public static Vektor2D operator +(Vektor2D vektor)
        => vektor;
    
    // Dönüşüm operatörleri
    public static implicit operator (double, double)(Vektor2D vektor)
        => (vektor.X, vektor.Y);
        
    public static implicit operator Vektor2D((double X, double Y) tuple)
        => new(tuple.X, tuple.Y);
    
    public double Buyukluk => Math.Sqrt(X * X + Y * Y);
    public override string ToString() => $"({X:F2}, {Y:F2})";
}

// Kullanım örneği
public void VektorOperatorOrnekleri()
{
    var v1 = new Vektor2D(3, 4);
    var v2 = new Vektor2D(1, 2);
    
    var toplam = v1 + v2;              // (4.00, 6.00)
    var fark = v1 - v2;                // (2.00, 2.00)
    var olcekli = v1 * 2;              // (6.00, 8.00)
    var negatif = -v1;                 // (-3.00, -4.00)
    
    // Implicit dönüşüm kullanımı
    (double x, double y) = v1;         // Tuple'a dönüştür
    Vektor2D tupleDan = (5, 6);        // Tuple'dan dönüştür
    
    Console.WriteLine($"v1: {v1}, büyüklük: {v1.Buyukluk:F2}");
    Console.WriteLine($"v2: {v2}, büyüklük: {v2.Buyukluk:F2}");
    Console.WriteLine($"Toplam: {toplam}");
    Console.WriteLine($"Fark: {fark}");
    Console.WriteLine($"Ölçekli: {olcekli}");
}
```

## Performans Karşılaştırmaları

### ref vs Değer Parametre Performansı

```csharp
public struct BuyukStruct
{
    private readonly double[] _veri;
    
    public BuyukStruct(int boyut)
    {
        _veri = new double[boyut];
        for (int i = 0; i < boyut; i++)
            _veri[i] = Random.Shared.NextDouble();
    }
    
    public double Toplam => _veri.Sum();
    public int Uzunluk => _veri.Length;
}

// Performans karşılaştırma metotları
public class PerformansOrnegi
{
    // Struct'ı kopyalayan metot (yavaş)
    public double DegerIleIsle(BuyukStruct veri)
    {
        return veri.Toplam * 2; // Struct geçirilirken kopyalanır
    }
    
    // Referans kullanan metot (hızlı)
    public double ReferansIleIsle(ref BuyukStruct veri)
    {
        return veri.Toplam * 2; // Kopyalama olmaz
    }
    
    // Doğrudan değişiklik için referans döndüren metot
    public ref BuyukStruct StructRefAl(ref BuyukStruct veri)
    {
        return ref veri;
    }
    
    public void PerformansiFarkiniGoster()
    {
        var buyukStruct = new BuyukStruct(10000);
        
        // Değer tabanlı yaklaşımı zamanlama
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        for (int i = 0; i < 1000; i++)
        {
            DegerIleIsle(buyukStruct); // Her çağrıda 80KB+ veri kopyalar
        }
        stopwatch.Stop();
        Console.WriteLine($"Değer tabanlı: {stopwatch.ElapsedMilliseconds}ms");
        
        // Referans tabanlı yaklaşımı zamanlama
        stopwatch.Restart();
        for (int i = 0; i < 1000; i++)
        {
            ReferansIleIsle(ref buyukStruct); // Kopyalama yok
        }
        stopwatch.Stop();
        Console.WriteLine($"Referans tabanlı: {stopwatch.ElapsedMilliseconds}ms");
    }
}
```

## Gerçek Dünya Senaryoları

### Senaryo 1: ref ile Konfigürasyon Yönetimi

```csharp
public class KonfigurasyonYoneticisi
{
    private readonly Dictionary<string, object> _konfig = new();
    
    public ref T KonfigDegeriAl<T>(string anahtar) where T : struct
    {
        if (!_konfig.ContainsKey(anahtar))
            _konfig[anahtar] = default(T);
            
        return ref Unsafe.As<object, T>(ref _konfig[anahtar]);
    }
    
    public void KonfigDegeriAyarla<T>(string anahtar, T deger) where T : struct
    {
        _konfig[anahtar] = deger;
    }
}

// Kullanım
public void KonfigurasyonOrnegi()
{
    var konfig = new KonfigurasyonYoneticisi();
    
    // Konfig değerlerini doğrudan değiştirme
    ref int maksBaglanti = ref konfig.KonfigDegeriAl<int>("MaksBaglanti");
    maksBaglanti = 100; // Saklanan değeri doğrudan değiştirir
    
    ref double timeout = ref konfig.KonfigDegeriAl<double>("TimeoutSaniye");
    timeout = 30.0;
    
    Console.WriteLine($"Maks Bağlantı: {konfig.KonfigDegeriAl<int>("MaksBaglanti")}");
    Console.WriteLine($"Timeout: {konfig.KonfigDegeriAl<double>("TimeoutSaniye")}");
}
```

### Senaryo 2: Kütüphane Yönetim Sistemi

```csharp
public class Kutuphane
{
    private readonly List<Book> _kitaplar = new();
    private readonly List<Student> _ogrenciler = new();
    
    public void KitapEkle(Book kitap) => _kitaplar.Add(kitap);
    public void OgrenciKaydet(Student ogrenci) => _ogrenciler.Add(ogrenci);
    
    // Sezgisel kitap ödünç verme için operatör aşırı yükleme
    public static Kutuphane operator +(Kutuphane kutuphane, (Book kitap, Student ogrenci) odunc)
    {
        var (kitap, ogrenci) = odunc;
        if (kutuphane._kitaplar.Contains(kitap))
        {
            ogrenci = kitap + ogrenci; // Aşırı yüklenmiş operatörümüzü kullan
            kutuphane._kitaplar.Remove(kitap);
        }
        return kutuphane;
    }
    
    // Kitap iade etme operatörü
    public static Kutuphane operator -(Kutuphane kutuphane, (Book kitap, Student ogrenci) iade)
    {
        var (kitap, ogrenci) = iade;
        ogrenci = ogrenci - kitap; // Aşırı yüklenmiş operatörümüzü kullan
        kutuphane._kitaplar.Add(kitap);
        return kutuphane;
    }
}

// Kullanım
public void KutuphaneSystemiOrnegi()
{
    var kutuphane = new Kutuphane();
    var kitap = new Book { Name = "C# Derinlemesine", Author = "Jon Skeet" };
    var ogrenci = new Student { Name = "Zeynep" };
    
    kutuphane.KitapEkle(kitap);
    kutuphane.OgrenciKaydet(ogrenci);
    
    // Kitabı öğrenciye ödünç ver
    kutuphane = kutuphane + (kitap, ogrenci);
    Console.WriteLine($"Öğrenci şimdi {ogrenci.Books.Count} kitaba sahip");
    
    // Kitabı kütüphaneye iade et
    kutuphane = kutuphane - (kitap, ogrenci);
    Console.WriteLine($"Öğrenci şimdi {ogrenci.Books.Count} kitaba sahip");
}
```

### Senaryo 3: Vektörlerle Oyun Geliştirme

```csharp
public class OyunNesnesi
{
    public Vektor2D Pozisyon { get; set; }
    public Vektor2D Hiz { get; set; }
    public string Ad { get; set; } = string.Empty;
    
    public void Guncelle(double deltaZaman)
    {
        // Doğal fizik hesaplamaları için aşırı yüklenmiş operatörler
        Pozisyon = Pozisyon + (Hiz * deltaZaman);
    }
    
    public static OyunNesnesi operator +(OyunNesnesi nesne, Vektor2D kuvvet)
    {
        nesne.Hiz = nesne.Hiz + kuvvet;
        return nesne;
    }
}

public void OyunFizigiOrnegi()
{
    var oyuncu = new OyunNesnesi 
    { 
        Ad = "Oyuncu", 
        Pozisyon = (0, 0), 
        Hiz = (5, 0) 
    };
    
    // Yerçekimi uygula
    var yercekimi = new Vektor2D(0, -9.81);
    oyuncu = oyuncu + yercekimi;
    
    // 1 saniye boyunca hareketi simüle et
    oyuncu.Guncelle(1.0);
    
    Console.WriteLine($"Oyuncu pozisyonu 1 saniye sonra: {oyuncu.Pozisyon}");
    Console.WriteLine($"Oyuncu hızı: {oyuncu.Hiz}");
}
```

## Anti-Desenler ve Tuzaklar

### ❌ ref ile Yaygın Hatalar

```csharp
// YANLIŞ: Yerel değişkenin referansını döndürmeye çalışma
public ref int KotuRefReturn()
{
    int yerelDegisken = 42;
    return ref yerelDegisken; // Derleme hatası: CS8168
}

// YANLIŞ: Çağrıda ref anahtar kelimesini unutma
public void KotuRefKullanimi()
{
    int deger = 10;
    DegeriDegistir(deger);     // Orijinal değeri değiştirmez
    DegeriDegistir(ref deger); // Doğru yol
}

void DegeriDegistir(ref int x) => x = 100;

// YANLIŞ: Readonly alanın referansı
public class KotuRefSinifi
{
    private readonly int _readonlyAlan = 42;
    
    public ref int ReadonlyRefAl()
    {
        return ref _readonlyAlan; // Derleme hatası
    }
}
```

### ❌ Operatör Aşırı Yükleme Tuzakları

```csharp
// YANLIŞ: Sezgisel olmayan operatör davranışı
public static Book operator +(Book kitap1, Book kitap2)
{
    // Bu kafa karıştırıcı - kitapları toplamak ne demek?
    return new Book { Name = kitap1.Name + kitap2.Name };
}

// YANLIŞ: Tutarsız parametre türleri
public static Student operator +(Book kitap, Student ogrenci) { /* ... */ }
public static Student operator +(Student ogrenci, Book kitap) { /* ... */ }
// Her ikisine de sahip olmak kafa karıştırıcı olabilir

// YANLIŞ: Operandları beklenmedik şekilde değiştirme
public static Vektor2D operator +(Vektor2D sol, Vektor2D sag)
{
    sol.X += sag.X; // Sol operandı değiştirir!
    sol.Y += sag.Y;
    return sol;
}

// DOĞRU: Yeni instance döndür
public static Vektor2D operator +(Vektor2D sol, Vektor2D sag)
{
    return new Vektor2D(sol.X + sag.X, sol.Y + sag.Y);
}
```

### ✅ En İyi Uygulamalar

```csharp
// İYİ: Net, sezgisel operatör davranışı
public static Para operator +(Para sol, Para sag)
{
    if (sol.ParaBirimi != sag.ParaBirimi)
        throw new InvalidOperationException("Farklı para birimleri toplanamaz");
    
    return new Para(sol.Miktar + sag.Miktar, sol.ParaBirimi);
}

// İYİ: Doğrulama ile uygun ref kullanımı
public ref T DiziElemaniAl<T>(T[] dizi, int indeks)
{
    if (dizi == null) throw new ArgumentNullException(nameof(dizi));
    if (indeks < 0 || indeks >= dizi.Length) 
        throw new IndexOutOfRangeException();
    
    return ref dizi[indeks];
}

// İYİ: Tutarlı operatör çiftleri
public static bool operator ==(KarmasikSayi sol, KarmasikSayi sag)
    => sol.Gercek == sag.Gercek && sol.Sanal == sag.Sanal;

public static bool operator !=(KarmasikSayi sol, KarmasikSayi sag)
    => !(sol == sag);

// == ve != uygularken Equals ve GetHashCode'u override et
public override bool Equals(object? obj) 
    => obj is KarmasikSayi diger && this == diger;

public override int GetHashCode() 
    => HashCode.Combine(Gercek, Sanal);
```

## Özet

Bu örnekler gelişmiş C# özelliklerinin gücünü ve esnekliğini göstermektedir:

- **Referans semantiği** performans faydaları sağlar ve doğrudan veri manipülasyonunu mümkün kılar
- **Operatör aşırı yükleme** sezgisel, domain-spesifik API'lar oluşturur
- **Doğru kullanım** etkileri anlamayı ve en iyi uygulamaları takip etmeyi gerektirir
- **Gerçek dünya uygulamaları** bu özelliklerin pratik problemleri nasıl çözdüğünü gösterir

Dil özelliklerinin akıllıca kullanımından çok kod netliği ve sürdürülebilirliği öncelemek her zaman önemlidir.