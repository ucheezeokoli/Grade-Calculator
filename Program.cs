using System.Dynamic;
using System.IO.Pipes;
using System.Reflection.PortableExecutable;

Dictionary<string, List<List<double>>> students = new Dictionary<string, List<List<double>>>();

students
.Add(
    "Sophia",
    new List<List<double>>
    {
        new List<double> {90,86,87,98,100},
        new List<double> {0}
    }
    );
students
.Add(
    "Andrew",
    new List<List<double>>
    {
        new List<double> {92,89,81,96,90},
        new List<double> {0}
    }
    );
students
.Add(
    "Emma",
    new List<List<double>>
    {
        new List<double> {90,85,87,98,68},
        new List<double> {0}
    }
    );
students
.Add(
    "Logan",
    new List<List<double>>
    {
        new List<double> {90,95,87,88,96},
        new List<double> {0}
    }
    );

Console.WriteLine("Welcome to the grading program!");
Console.WriteLine
(
@"To view a student's grades enter the student's 'first name'.
To view all student's grades enter 'All'.
To add grades for a new student enter 'new'.
To end program enter 'End'."
);

string input = "";
while (input != "End")
{
    input = Console.ReadLine();

    if (students.ContainsKey(input))
    {
        Console.Write("Would you like to add any extra credit? for" + input + "? ");
        Console.WriteLine("Y/n");
        if (Console.ReadLine() == "Y")
        {
            AddExtraCreditScores(input);
        }
        CalculateGradesForIndividualStudent(input);

        Console.WriteLine(input + "\t\t" + students[input][1][0] + "\t" + getLetterGrade(input));
    }
    else if (input == "All")
    {
        Console.WriteLine("Student\t\tGrade\n");
        CalculateGradesForAllStudents();
    }
    else if (input == "new")
    {
        Console.WriteLine("Enter the first name of the new student");
        string firstName = Console.ReadLine();
        Console.WriteLine("Enter in 5 scores separated by a single space");
        string scoreInput = Console.ReadLine();
        string[] scoreList = scoreInput.Split(' ');
        students.Add(
            firstName,
            new List<List<double>>
            {
                new List<double> {},
                new List<double> {0}
            }
        );

        foreach (string score in scoreList)
        {
            double.TryParse(score, out double convertedScore);
            students[firstName][0].Add(convertedScore);
        }
    }
    else if (input == "End")
    {
        break;
    }
    else
    {
        Console.Error.WriteLine("Invalid entry");
    }
}

Console.WriteLine("Thank you for using the grading program. Goodbye!");

void AddExtraCreditScores(string student)
{
    Console.WriteLine("Enter extra credit scores separated by a single space");
    string extraCreditInput = Console.ReadLine();
    string[] extraCreditScores = extraCreditInput.Split(' ');

    foreach (string score in extraCreditScores)
    {
        double.TryParse(score, out double newScore);
        students[student][0].Add(newScore / 10);
    }
}

void CalculateGradesForAllStudents()
{
    foreach (KeyValuePair<string, List<List<double>>> student in students)
    {
        student.Value[1][0] = student
                        .Value[0]
                        .Sum() / 5;

        Console.WriteLine(student.Key + ":\t\t " + student.Value[1][0] + "\t" + getLetterGrade(student.Key));
    }
}

void CalculateGradesForIndividualStudent(string student)
{
    students[student][1][0] = students[student][0].Sum() / 5;
}

string getLetterGrade(string student)
{
    double score = students[student][1][0];
    if (score >= 97)
    {
        return "A+";
    }
    else if (score >= 93)
    {
        return "A";
    }
    else if (score >= 90)
    {
        return "A-";
    }
    else if (score >= 87)
    {
        return "B+";
    }
    else if (score >= 83)
    {
        return "B";
    }
    else if (score >= 80)
    {
        return "B-";
    }
    else if (score >= 77)
    {
        return "C+";
    }
    else if (score >= 73)
    {
        return "C";
    }
    else if (score >= 70)
    {
        return "C-";
    }
    else if (score >= 67)
    {
        return "D+";
    }
    else if (score >= 63)
    {
        return "D";
    }
    else if (score >= 60)
    {
        return "D-";
    }
    else
    {
        return "F";
    }
}
