using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BarometerDomain;
using BarometerDomain.Repositories;
using BarometerDomain.Model;
using System.Data.Entity;

namespace BarometerStudent.Services
{
    public class ExcelReader
    {
        string FileName;
        string[,] StringTable;

        public ExcelReader(string File)
        {
            FileName = File;
            ToStrings(out StringTable);
        }

        public void ToStrings(out string[,] stringTable)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            try
            {
                Microsoft.Office.Interop.Excel.Workbook wb = excel.Workbooks.Open(FileName);

                // Get worksheet names
                foreach (Microsoft.Office.Interop.Excel.Worksheet sh in wb.Worksheets)
                    Console.WriteLine(sh.Name);

                // Get values from sheets SH1 and SH3 (in my file)
                Microsoft.Office.Interop.Excel.Worksheet val1 = wb.Sheets.get_Item(1);
                Console.WriteLine("{0}", val1);

                Microsoft.Office.Interop.Excel.Range range = val1.UsedRange;
                stringTable = new string[range.Rows.Count, range.Columns.Count];
                for (int row = 2; row < range.Rows.Count; row++)
                {
                    for (int column = 1; column < range.Columns.Count; column++)
                    {
                        var cell = (range.Cells[row, column] as Microsoft.Office.Interop.Excel.Range).Value2;
                        string cellString = Convert.ToString(cell);
                        System.Diagnostics.Debug.Write(cellString + " | ");
                        stringTable[row - 1, column - 1] = cellString;
                    }
                    System.Diagnostics.Debug.WriteLine("");
                }

                wb.Close();
                excel.Quit();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                excel.Quit();
                stringTable = null;
            }
        }

        public void ToDatabase()
        {
            Context db = new Context();
            StudentRepository studentRepo = new StudentRepository(db);
            GroupRepository groupRepo = new GroupRepository(db);
            List<Group> ChangedGroups = new List<Group>();
            int idColumn = 0;
            int fNameColumn = 1;
            int preFixColumn = 2;
            int lNameColumn = 3;
            int grpNameColumn = 4;
            for (int row = 0; row < StringTable.GetLength(0); row++)
            {
                string name = StringTable[row, fNameColumn];
                string prefix = StringTable[row, preFixColumn];
                string lastName = StringTable[row, lNameColumn];
                string grpName = StringTable[row, grpNameColumn];
                System.Diagnostics.Debug.WriteLine("processing" + name + " " + lastName + ", " + grpName);
                if (name != null && lastName != null && grpName != null)
                {
                    int id = Convert.ToInt32(StringTable[row, idColumn]);
                    bool newStudent;
                    Student student = TryFindStudent(id, studentRepo, name, prefix, lastName, out newStudent);
                    //zoek groep
                    Group group = TryFindGroup(grpName, groupRepo);
                    student.Groups.Add(group);
                    if (!newStudent)
                        studentRepo.Update(student);

                }
            }
            db.SaveChanges();
        }

        private Student TryFindStudent(int id, StudentRepository studentRepo, string prefix, string name, string lName, out bool newStudent)
        {
            Student student = studentRepo.ByNumber(id);
            newStudent = false;
            if (student == null) //als student null is, maak een nieuwe
            {
                //naam samenstellen
                if (!prefix.Equals(""))
                    name += " " + prefix;
                name += " " + lName;
                //maak student met naam, studentnummer en rol
                student = new Student() { Name = name, Number = id };
                student.RoleType = "Student";
                studentRepo.Insert(student);
                newStudent = true;
            }

            return student;
        }

        private Group TryFindGroup(string name, GroupRepository groupRepo)
        {
            Group group = groupRepo.ByName(name);
            if (group == null) //als de groep niet bestaat
            {
                group = new Group() { Name = name };
                groupRepo.Insert(group);
                groupRepo.Save();
            }
            
            return group;
        }
    }
}