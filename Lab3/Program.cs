using System;
using System.Linq;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        CreateXmlFile();

        DisplayXmlFile();

        Console.WriteLine("Info about Stud1':");
        GetStudentInfoBySurname("Stud1");

        Console.WriteLine("Number of boys excelling in faculty x: ");
        CountExcellentMaleStudents(3, "x");
    }

    static void CreateXmlFile()
    {
        var students = new XElement("Students",
            new XElement("Student",
                new XElement("Surname", "Stud1"),
                new XElement("Faculty", "x"),
                new XElement("Course", 3),
                new XElement("Gender", "male"),
                new XElement("SchShip", 1500),
                new XElement("Grade1", 5),
                new XElement("Grade2", 5),
                new XElement("Grade3", 5)
            ),
            new XElement("Student",
                new XElement("Surname", "Stud2"),
                new XElement("Faculty", "y"),
                new XElement("Course", 2),
                new XElement("Gender", "female"),
                new XElement("SchShip", 1400),
                new XElement("Grade1", 4),
                new XElement("Grade2", 5),
                new XElement("Grade3", 5)
            ),
            new XElement("Student",
                new XElement("Surname", "Stud3"),
                new XElement("Faculty", "Факультет Y"),
                new XElement("Gender", "male"),
                new XElement("SchShip", 1600),
                new XElement("Grade1", 5),
                new XElement("Grade2", 5),
                new XElement("Grade3", 5)
            )
        );

        students.Save("Student.xml");
    }

    static void DisplayXmlFile()
    {
        var doc = XDocument.Load("Student.xml");
        Console.WriteLine(doc);
    }

    static void GetStudentInfoBySurname(string surname)
    {
        var doc = XDocument.Load("Student.xml");

        var student = doc.Descendants("Student")
            .FirstOrDefault(s => (string)s.Element("Surname") == surname);

        if (student != null)
        {
            Console.WriteLine($"Surname: {surname}");
            Console.WriteLine($"SchShip: {student.Element("SchShip").Value}");
            Console.WriteLine($"Marks: {student.Element("Grade1").Value}, {student.Element("Grade2").Value}, {student.Element("Grade3").Value}");
        }
        else
        {
            Console.WriteLine($"No student with surname: {surname}.");
        }
    }

    static void CountExcellentMaleStudents(int course, string faculty)
    {
        var doc = XDocument.Load("Student.xml");

        var excellentMales = doc.Descendants("Student")
            .Where(s => (string)s.Element("Gender") == "male" &&
                        (int)s.Element("Course") == course &&
                        (string)s.Element("Faculty") == faculty &&
                        (int)s.Element("Grade1") == 5 &&
                        (int)s.Element("Grade2") == 5 &&
                        (int)s.Element("Grade3") == 5)
            .Count();

        Console.WriteLine($"Number of boys excelling in {course} faculty {faculty}: {excellentMales}");
    }
}
