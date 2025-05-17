using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace Data_Table
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Students students = new Students();

            Console.WriteLine("\n--- Adding Students ---");
            students.AddStudent("Rania", 22, 'F', "Rabat", "0612345678", "rania@example.com");
            students.AddStudent("Youssef", 30, 'M', "Casablanca", "0654321987", "youssef@example.com");
            students.AddStudent("Rabie", 27, 'M', "Khouribga", "0718251436", "Rabie@example.com");
            students.AddStudent("Yassine", 24, 'M', "Khouribga", "0777452541", "Yassine@example.com");
            students.AddStudent("Zaid", 18, 'M', "Khouribga", "+393214521478", "Zaid@example.com");
            students.DisplayTable(students.GetOriginalTable());

            
            StudentView View = new StudentView(students.GetOriginalTable());
            SortDirection direction = SortDirection.None;

            Console.WriteLine("\n\n\n\n\nFilter By Gender = 'M'\n");
            View.FilterByGender('M');
            View.DisplayView(View.GetView());

            Console.WriteLine("\n\n\n\n\nFilter By Age = 18\n");

            View.FilterByAge(18);
            View.DisplayView(View.GetView());


            Console.WriteLine("\n\n\n\n\nFilter By City = 'Khouribga'\n");

            View.FilterByCity("Khouribga");
            View.DisplayView(View.GetView());

            Console.WriteLine("\n\n\n\n\nSort By Age Ascending\n");
            View.SortByAge(SortDirection.Ascending);
            View.DisplayView(View.GetView());


            Console.WriteLine("\n\n\n\n\nSort By Age Descending\n");

            View.SortByAge(SortDirection.Descending);
            View.DisplayView(View.GetView());


            Console.WriteLine("\n\n\n\n\nSort By Name Ascending\n");
            View.SortByName(SortDirection.Ascending);
            View.DisplayView(View.GetView());


            Console.WriteLine("\n\n\n\n\nSort By Name Descending\n");
            View.SortByName(SortDirection.Descending);
            View.DisplayView(View.GetView());


            Console.WriteLine("\n\n\n\n\nSort By Name Ascending and Age Descending\n");
            View.SortByName(SortDirection.Ascending);
            View.SortByAge(SortDirection.Descending);
            View.DisplayView(View.GetView());


            Console.WriteLine("\n\n\n\n\nSort By Name Descending and Age Ascending\n");
            View.SortByName(SortDirection.Descending);
            View.SortByAge(SortDirection.Ascending);
            View.DisplayView(View.GetView());


            Console.WriteLine("\n\n\n\n\nSort By Age Ascending Or Descending\n this ===> Ascending\n");
            View.SortByAge(true);
            View.DisplayView(View.GetView());


            Console.WriteLine("\n\n\n\n\nSort By Age Ascending Or Descending\n this ===> Descending\n");
            View.SortByAge(false, true);
            View.DisplayView(View.GetView());


            Console.WriteLine("\n\n\n\n\nSort By Name Ascending Or Descending\n this ===> Ascending\n");
            View.SortByName(true);
            View.DisplayView(View.GetView());


            Console.WriteLine("\n\n\n\n\nSort By Name Ascending Or Descending\n this ===> Descending\n");
            View.SortByName(false, true);
            View.DisplayView(View.GetView());


            //string CSVcontent = View.ExportToCsv();
            //string FilePath = "students.csv";
            //File.WriteAllText(FilePath, CSVcontent);
            //Console.WriteLine("Data has been successfully exported to " + FilePath);


            string CSVcontent = View.ExportToCsv();
            Console.WriteLine("\n\n\n\n\nExported CSV Content:\n");
            Console.WriteLine(CSVcontent);


            string RowFilter = View.GetCurrentFilter();
            Console.WriteLine("\n\n\n\n\nCurrent Row Filter:\n");
            Console.WriteLine(RowFilter);


            string SortExpression = View.GetCurrentSort();
            Console.WriteLine("\n\n\n\n\nCurrent Sort Filter:\n");
            Console.WriteLine(SortExpression);


            string GetCurrentSortColumn = View.GetCurrentSortColumn();
            Console.WriteLine("\n\n\n\n\nCurrent Sort Column:\n");
            Console.WriteLine(GetCurrentSortColumn);


            View.FilterByCities(new List<string> { "Khouribga", "Rabat" });
            Console.WriteLine("\n\n\n\n\nFilter By Cities = 'Khouribga' and 'Rabat'\n");
            View.DisplayView(View.GetView());


            View.FilterByAgeRange(18, 30);
            Console.WriteLine("\n\n\n\n\nFilter By Age Range = 18 and 30\n");
            View.DisplayView(View.GetView());


            View.FilterBy(row => Convert.ToInt32(row["Age"]) >= 30);
            Console.WriteLine("\n\n\n\n\nFilter By Age >= 30\n");
            View.DisplayView(View.GetView());
        }
    }
}

