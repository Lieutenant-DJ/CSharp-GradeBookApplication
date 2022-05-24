using System;
using System.Collections.Generic;
using System.Text;
using GradeBook.Enums;
using System.Linq;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name) : base(name)
        {
            Type = GradeBookType.Ranked;
        }


        public override char GetLetterGrade(double averageGrade)
        {
            if (BelowMinAcceptableStudentAmount())
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

        public bool BelowMinAcceptableStudentAmount()
        {
            return (Students.Count < 5);
        }

        public override void CalculateStatistics()
        {
            if (BelowMinAcceptableStudentAmount())
            {
                throw new InvalidOperationException("Insufficient number of students; 5 student minimum required.");
            }

            base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (BelowMinAcceptableStudentAmount())
            {
                throw new InvalidOperationException("Insufficient number of students; 5 student minimum required.");
            }

            base.CalculateStudentStatistics(name);
        }
    }
}
