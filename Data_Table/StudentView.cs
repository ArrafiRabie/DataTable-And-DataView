using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Table
{
    internal enum SortDirection
    {
        None,
        Ascending,
        Descending
    }

    internal class StudentView
    {
        private DataView View;
        public event Action OnviewUpdate;

        public StudentView(DataTable Table) 
        {
            View = new DataView(Table);
        }

        public void FilterByGender(char gender)
        {
            View.RowFilter = $"gender = '{gender}'";
        }

        public void FilterByAge(int age)
        { 
            View.RowFilter = $"age = '{age}'";
        }

        public void FilterByCity(string city)
        {
            View.RowFilter = $"Address = '{city.Replace("'","''")}'";
        }

        private string GetSortExpression(string Column, SortDirection direction)
        {
            if (direction == SortDirection.Ascending)
                return View.Sort = $"{Column} ASC";
            else if (direction == SortDirection.Descending)
                return View.Sort = $"{Column} DESC";
            else
                return View.Sort = $"{Column}";
        }

        public void SortByAge(SortDirection direction)
        {
            View.Sort = GetSortExpression("Age", direction);
        }

        public void SortByName(SortDirection direction)
        {
            View.Sort = GetSortExpression("Name", direction);
        }

        public void SortByAge(bool ASC = false, bool DESC = false)
        {
            SortByAge(ConvertToDirection(ASC,DESC));
        }

        public void SortByName(bool ASC = false, bool DESC = false)
        {
            SortByName(ConvertToDirection(ASC,DESC));
        }

        public DataView GetView() 
        {
            return View;
        }

        private SortDirection ConvertToDirection(bool ASC, bool DESC)
        {
            if (ASC && DESC)
                throw new ArgumentException("Cannot sort by both ASC and DESC at the same time.");
            if (ASC)
                return SortDirection.Ascending;
            else if (DESC)
                return SortDirection.Descending;
            else
                return SortDirection.None;
        }

        public void SearchByName(string name)
        {
            View.RowFilter = $"Name LIKE '%{name.Replace("'", "''")}%'";
        }

        public void SearchByPhone(string phone)
        {
            View.RowFilter = $"Phone LIKE '%{phone.Replace("'", "''")}%'";
        }

        public void SearchByEmail(string email)
        {
            View.RowFilter = $"Email LIKE '%{email.Replace("'", "''")}%'";
        }

        public void ResetFilter()
        {
            View.RowFilter = string.Empty;
        }

        public void ResetSort()
        {
            View.Sort = string.Empty;
        }

        public void AddCustomFilter(string filter)
        {
            View.RowFilter = filter;
        }

        public int GetCount()
        {
            return View.Count;
        }

        public double GetAverageAge()
        {
            if (View.Count == 0)
                return 0;

            return View.Cast<DataRowView>()
              .Average(r => Convert.ToDouble(r["Age"]));
        }

        public string ExportToCsv()
        {
            StringBuilder csv = new StringBuilder();

            // Header
            csv.AppendLine(string.Join(",", View.Table.Columns.Cast<DataColumn>().Select(c => c.ColumnName)));

            // Rows
            foreach (DataRowView rowView in View)
            {
                var values = rowView.Row.ItemArray.Select(v => v.ToString());
                csv.AppendLine(string.Join(",", values));
            }

            return csv.ToString();
        }

        public string GetCurrentFilter()
        {
            return View.RowFilter;
        }

        public string GetCurrentSort()
        {
            return View.Sort;
        }

        public string GetCurrentSortColumn()
        {
            return View.Sort.Split(' ')[0];
        }

        public void FilterByCities(List<string>Cities)
        {
            if (Cities == null || Cities.Count == 0)
            {
                View.RowFilter = string.Empty;
                return;
            }
            string InClause = string.Join(",", Cities.Select(c => $"'{c.Replace("'", "''")}'"));
            View.RowFilter = $"Address IN ({InClause})";
        }

        public void FilterByAgeRange(int min, int max)
        {
            if (min > max)
            {
                View.RowFilter = "1 = 0";
            }
            else
                View.RowFilter = $"Age >= {min} AND Age <= {max}";
        }

        public void FilterBy(Func<DataRow,bool> predicate)
        {
            try
            {
                var filteredTable = View.Table.AsEnumerable()
                    .Where(predicate)
                    .CopyToDataTable();

                View = new DataView(filteredTable);
            }
            catch (InvalidOperationException)
            {
                // No rows matched the predicate
                View = new DataView(View.Table.Clone()); // return empty table with same schema
            }
        }

        public void DisplayView(DataView view)
        {
            if (view == null || view.Count == 0)
            {
                Console.WriteLine("No data to display.");
                return;
            }

            foreach (DataColumn column in view.Table.Columns)
                Console.Write($"{column.ColumnName,-13} | ");
            Console.WriteLine("\n" + new string('-', 111));

            foreach (DataRowView rowView in view)
            {
                foreach (var item in rowView.Row.ItemArray)
                    Console.Write($"{item,-13} | ");
                Console.WriteLine();
            }
        }

    }
}
