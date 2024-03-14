using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook:BaseGradeBook
    {
        public RankedGradeBook(string name) : base(name)
        {
            this.Type = Enums.GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if(Students.Count < 5)
            {
                throw new InvalidOperationException();
            }

            var sortedGrades = Students.OrderByDescending(s => s.AverageGrade).Select(s => s.AverageGrade).ToList();
            int top20Percent = (int)Math.Ceiling(Students.Count * 0.2);
            int top40Percent = (int)Math.Ceiling(Students.Count * 0.4);
            int top60Percent = (int)Math.Ceiling(Students.Count * 0.6);
            int top80Percent = (int)Math.Ceiling(Students.Count * 0.8);

            int index = sortedGrades.FindIndex(g => g == averageGrade);

            if (index < 0)
            {
                throw new InvalidOperationException("Student grade not found.");
            }

            if (index < top20Percent)
            {
                return 'A';
            }
            else if (index < top40Percent)
            {
                return 'B';
            }
            else if (index < top60Percent)
            {
                return 'C';
            }
            else if (index < top80Percent)
            {
                return 'D';
            }
            else
            {
                return 'F';
            }
        }

        public override void CalculateStatistics()
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students.");
            }else
            {
                base.CalculateStatistics();
            }
        }
    }
}
