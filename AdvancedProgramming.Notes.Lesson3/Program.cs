#region non-ref method
//int b = 5;
//X(b);

////Çıktı olarak 5 değerini verecektir.
//Console.WriteLine(b);
//void X(int a)
//{
//	a = 124;
//}

//Console.Read();
#endregion

#region ref keyword

////Eğer ki biz ref anahtar kelimesini kullanırsak, metot içerisindeki değişikliklerin dışarıya yansımasını sağlarız. Yani değer tipli bir değişkenin referansını metota verip metot içerisinde değişiklik yaparsak, bu değişiklikler dışarıya da yansır.
//int b = 5;
//X(ref b);

////Çıktı olarak 5 değerini verecektir.
//Console.WriteLine(b);

////Bu örnek için a ve b değişkenleri bellekte aynı yeri işaret etmektedirler.
//void X(ref int a)
//{
//    a = 124;
//}

//Console.Read();

#endregion

#region ref return

int b = 5;

// b, c ve a değişkenleri bellekte aynı yeri işaret ederler.
// Yani a değişkeni b değişkeninin referansını tutar.
// c değişkeni ise a değişkeninin referansını tutar.
// Dolayısıyla a değişkeni üzerinde yapılan değişiklikler c ve b değişkenlerine de yansır.


//c değişkenin de bir değişiklik olursa b değişkeni de değişir.
ref int c = ref X(ref b);

//Critical: Bu şekilde kullanım ise X metodundan dönen değeri direkt olarak tutar. y bir referansı artık tutmaz. y değiştiğin de b ve c değerleri değişmez
int y = X(ref b);

y = 21;

Console.WriteLine($"B değişkeni: {b} - C değişkeni {c} - Y değişkeni {y}");


ref int X(ref int a)
{
    //a değişkeni burada değiştirildiğn de b, c değişkenlerin değeri de değişir.
    a = 124;
    return ref a;
}


//Citical: Bu metot hata verir. Local değişkenlerin referansını döndüremezsiniz. Değişken, dışarıdan gelmelidir. Sınıfın field'ı veya parametresi referansı döndürülebilir.

//ref int Z()
//{

//    int a = 5;
//    return ref a;
//}

Console.Read();
#endregion