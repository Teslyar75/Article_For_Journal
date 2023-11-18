/*Задание 3:
Добавьте к предыдущему заданию список статей из журнала.
Нужно хранить такую информацию о каждой статье:
1. Название статьи
2. Количество символов
3. Анонс статьи
Измените функциональность из предыдущего задания таким
образом, чтобы она учитывала список статей.
Выбор конкретного формат сериализации необходимо сделать
вам. Обращаем ваше внимание, что выбор должен быть
обоснованным.
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

[Serializable]
public class Article_For_Journal
{
    public string Title { get; set; }
    public int Character_Count { get; set; }
    public string Announcement { get; set; }

    public Article_For_Journal() { }

    public Article_For_Journal(string title, int character_Count, string announcement)
    {
        Title = title;
        Character_Count = character_Count;
        Announcement = announcement;
    }

    public void PrintInfo()
    {
        Console.WriteLine($"Название статьи: {Title}");
        Console.WriteLine($"Количество символов: {Character_Count}");
        Console.WriteLine($"Анонс статьи: {Announcement}");
    }
}

[Serializable]
public class Journal
{
    public string Title { get; set; }
    public string Publisher { get; set; }
    public DateTime Release_Date { get; set; }
    public int Page_Count { get; set; }
    public List<Article_For_Journal> Articles { get; set; }

    public Journal() { }

    public Journal(string title, string publisher, DateTime release_Date, int page_Count, List<Article_For_Journal> articles)
    {
        Title = title;
        Publisher = publisher;
        Release_Date = release_Date;
        Page_Count = page_Count;
        Articles = articles;
    }

    public void PrintInfo()
    {
        Console.WriteLine($"Название журнала: {Title}");
        Console.WriteLine($"Издательство: {Publisher}");
        Console.WriteLine($"Дата выпуска: {Release_Date.ToShortDateString()}");
        Console.WriteLine($"Количество страниц: {Page_Count}");

        if (Articles != null && Articles.Count > 0)
        {
            Console.WriteLine("\nСтатьи в журнале:");
            foreach (var article in Articles)
            {
                article.PrintInfo();
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine("В журнале нет статей.");
        }
    }
}

class Program
{
    static void Main()
    {
        Journal journal = null;

        while (true)
        {
            Console.WriteLine("\nМеню:");
            Console.WriteLine("1. Ввод информации о журнале");
            Console.WriteLine("2. Вывод информации о журнале");
            Console.WriteLine("3. Добавление статьи");
            Console.WriteLine("4. Вывод информации о статьях");
            Console.WriteLine("5. Сериализация журнала и сохранение в файл");
            Console.WriteLine("6. Загрузка сериализованного журнала из файла и десериализация");
            Console.WriteLine("7. Выход");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    journal = Input_Journal_Info();
                    break;
                case "2":
                    Print_Journal_Info(journal);
                    break;
                case "3":
                    Add_Article(journal);
                    break;
                case "4":
                    Print_Articles(journal);
                    break;
                case "5":
                    Serialize_And_Save(journal);
                    break;
                case "6":
                    Deserialize_From_File();
                    break;
                case "7":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Некорректный выбор. Попробуйте еще раз.");
                    break;
            }
        }
    }

    static Journal Input_Journal_Info()
    {
        Console.Write("Введите название журнала: ");
        string title = Console.ReadLine();

        Console.Write("Введите название издательства: ");
        string publisher = Console.ReadLine();

        Console.Write("Введите дату выпуска (гггг-мм-дд): ");
        DateTime releaseDate;
        while (!DateTime.TryParse(Console.ReadLine(), out releaseDate))
        {
            Console.WriteLine("Некорректный ввод. Повторите попытку.");
        }

        Console.Write("Введите количество страниц: ");
        int pageCount;
        while (!int.TryParse(Console.ReadLine(), out pageCount))
        {
            Console.WriteLine("Некорректный ввод. Повторите попытку.");
        }

        List<Article_For_Journal> articles = InputArticles();

        return new Journal(title, publisher, releaseDate, pageCount, articles);
    }

    static List<Article_For_Journal> InputArticles()
    {
        List<Article_For_Journal> articles = new List<Article_For_Journal>();
        Console.WriteLine("Введите информацию о статьях (для завершения введите 'exit'):");

        while (true)
        {
            Console.Write("Введите название статьи: ");
            string title = Console.ReadLine();

            if (title.ToLower() == "exit")
                break;

            Console.Write("Введите количество символов: ");
            int characterCount;
            while (!int.TryParse(Console.ReadLine(), out characterCount))
            {
                Console.WriteLine("Некорректный ввод. Повторите попытку.");
            }

            Console.Write("Введите анонс статьи: ");
            string announcement = Console.ReadLine();

            articles.Add(new Article_For_Journal(title, characterCount, announcement));
        }

        return articles;
    }

    static void Print_Journal_Info(Journal journal)
    {
        if (journal != null)
        {
            Console.WriteLine("\nИнформация о журнале:");
            journal.PrintInfo();
        }
        else
        {
            Console.WriteLine("Информация о журнале отсутствует. Введите данные сначала.");
        }
    }

    static void Add_Article(Journal journal)
    {
        if (journal != null)
        {
            if (journal.Articles == null)
            {
                journal.Articles = new List<Article_For_Journal>();
            }

            Console.WriteLine("Добавление новой статьи:");

            Console.Write("Введите название статьи: ");
            string title = Console.ReadLine();

            Console.Write("Введите количество символов: ");
            int characterCount;
            while (!int.TryParse(Console.ReadLine(), out characterCount))
            {
                Console.WriteLine("Некорректный ввод. Повторите попытку.");
            }

            Console.Write("Введите анонс статьи: ");
            string announcement = Console.ReadLine();

            journal.Articles.Add(new Article_For_Journal(title, characterCount, announcement));

            Console.WriteLine("Статья успешно добавлена.");
        }
        else
        {
            Console.WriteLine("Информация о журнале отсутствует. Введите данные сначала.");
        }
    }

    static void Print_Articles(Journal journal)
    {
        if (journal != null)
        {
            if (journal.Articles != null && journal.Articles.Count > 0)
            {
                Console.WriteLine("\nИнформация о статьях в журнале:");
                foreach (var article in journal.Articles)
                {
                    article.PrintInfo();
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("В журнале нет статей.");
            }
        }
        else
        {
            Console.WriteLine("Информация о журнале отсутствует. Введите данные сначала.");
        }
    }

    static void Serialize_And_Save(Journal journal)
    {
        if (journal != null)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Journal));
            using (TextWriter writer = new StreamWriter("journal.xml"))
            {
                serializer.Serialize(writer, journal);
                Console.WriteLine("\nЖурнал успешно сериализован в XML и сохранен в файл journal.xml");
            }
        }
        else
        {
            Console.WriteLine("Информация о журнале отсутствует. Введите данные сначала.");
        }
    }

    static void Deserialize_From_File()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Journal));

        try
        {
            using (TextReader reader = new StreamReader("journal.xml"))
            {
                Journal loadedJournal = (Journal)serializer.Deserialize(reader);
                Console.WriteLine("\nЖурнал успешно загружен из файла и десериализован.");
                Console.WriteLine("\nИнформация о загруженном журнале:");
                loadedJournal.PrintInfo();
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Файл не найден.");
        }
    }
}

