using System;
using System.Collections.Generic;
using System.Text;
using GradeBook.Enums;
using System.Linq;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        const int MINSTUDENTS = 5;
        public RankedGradeBook(string name) : base(name)
        {
            Type = GradeBookType.Ranked;
        }


        public override char GetLetterGrade(double averageGrade)
        {
            if (MinAcceptableStudentAmountCheck())
            {
                throw new InvalidOperationException("Insufficient number of students; 5 student minimum required.");
            }

            var higherGrades = Students.Count(grades => grades.AverageGrade >= averageGrade);
            var percentile = (Students.Count - higherGrades) / Students.Count * 100;

            switch (averageGrade)
            {
                case var d when d >= 80.0:
                    return 'A';

                case var d when d >= 60.0:
                    return 'B';

                case var d when d >= 40.0:
                    return 'C';

                case var d when d >= 20.0:
                    return 'D';

                default:
                    return 'F';
            }
        }

        public bool MinAcceptableStudentAmountCheck()
        {
            return (Students.Count < MINSTUDENTS);
        }

        public override void CalculateStatistics()
        {
            if (MinAcceptableStudentAmountCheck())
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }
            base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (MinAcceptableStudentAmountCheck())
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }
            base.CalculateStudentStatistics(name);
        }
    }
}
