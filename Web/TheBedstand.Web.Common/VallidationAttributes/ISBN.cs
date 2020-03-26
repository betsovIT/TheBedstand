namespace TheBedstand.Web.Common
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ISBN : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var isbn = value as string;

            if (isbn == null)
            {
                return false;
            }
            else if (isbn.Length == 10)
            {
                var nonCheckDigitMultiplicationSum = 0;

                for (int i = 0; i < 8; i++)
                {
                    nonCheckDigitMultiplicationSum += int.Parse(isbn[i].ToString()) * (10 - i);
                }

                int providedCheckDigit;

                if (isbn[10] == 'X' || isbn[9] == 'x')
                {
                    providedCheckDigit = 10;
                }
                else
                {
                    providedCheckDigit = int.Parse(isbn[9].ToString());
                }

                var modularDivisionResult = (nonCheckDigitMultiplicationSum + providedCheckDigit) % 11;

                if (modularDivisionResult == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (isbn.Length == 13)
            {
                var digitsMultiplicationSum = 0;

                int multiplicator = 0;

                for (int i = 0; i < 12; i++)
                {
                    if (i % 2 == 0)
                    {
                        multiplicator = 1;
                    }
                    else
                    {
                        multiplicator = 3;
                    }

                    digitsMultiplicationSum += int.Parse(isbn[i].ToString()) * multiplicator;
                }

                if (digitsMultiplicationSum % 10 == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
