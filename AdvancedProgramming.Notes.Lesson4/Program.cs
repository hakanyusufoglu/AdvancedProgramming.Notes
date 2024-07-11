#region Operator Overloading -1
Student student = new()
{
    Name = "Hakan"
};

Book book = new()
{
    Name = "Suç ve Ceza",
    Author = "Dostoyevski"
};

//Parametrelerin sırası önemlidir solda book olmalı, sağda student olmalıdır
var result = book + student;

class Student
{
    public string Name { get; set; }
    public List<Book> Books { get; set; } = new();
}
class Book
{
    public string Name { get; set; }
    public string Author { get; set; }

    // + operatorunu overloading etmiş oluyoruz. normalde + operatörü birleştirme veya toplama işlemindeyken bu sınıfa özgü olarak kitap eklemek için kullanılmıştır.
    //Bu tamamlama studentta da bookta da olabilir. Farketmez
    //Parametrelerin sırası önemlidir solda book olmalı, sağda student olmalıdır
    //Geriye dönecek olan değerler için operator overloading yapılır. Öbürtürlü anlamı olmaz.
    //Public ve static olmak zorundadır.
    public static Student operator +(Book book, Student student)
    {
        student.Books.Add(book);
        return student;
    }
}

#endregion