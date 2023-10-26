using System.Collections.Generic;
using System;
using System.Linq;

namespace Dewey_Decimal_Training_App
{
    public class DeweyDecimalGenerator
    {
        List<string> callNumbers;
        List<string> sortedCallNumbers;
        int userPoints;

        public List<string> CallNumbers { get => callNumbers; set => callNumbers = value; }
        public List<string> SortedCallNumbers { get => sortedCallNumbers; set => sortedCallNumbers = value; }
        public int UserPoints { get => userPoints; set => userPoints = value; }

        public List<string> GenerateRandomCallNumbers(int count)
        {
            Random random = new Random();
            List<string> numbers = new List<string>();

            for (int i = 0; i < count; i++)
            {
                string number = $"{random.Next(1000):000}.{random.Next(100):00} {GenerateRandomAuthorInitials()}";
                numbers.Add(number);
            }
            return numbers;
        }

        private string GenerateRandomAuthorInitials()
        {
            Random random = new Random();
            string initials = "";
            for (int i = 0; i < 3; i++)
            {
                initials += (char)('A' + random.Next(26));
            }
            return initials;
        }

        public string GenerateCallNumbers()
        {
            CallNumbers = GenerateRandomCallNumbers(10);
            SortedCallNumbers = CallNumbers.OrderBy(cn => cn).ToList();

            return DisplayCallNumbers(CallNumbers);
        }

        public string DisplayCallNumbers(List<string> numbers)
        {
            return string.Join(Environment.NewLine, numbers);
        }
    }
}