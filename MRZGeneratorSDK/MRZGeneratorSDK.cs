/*******************************************************
This program was created by the
Mehrdad Bahrami Advanced
Automatic Program Generator
© Copyright 2021

Project : MRZ Generator
Version : 1.0
Date    : 1/9/2021
Author  : Mehrdad Bahrami Hezveh
Comments: Nothing

*******************************************************/

using System;
using System.Text.RegularExpressions;

namespace MRZGeneratorSDK
{
    public class MRZGeneratorSDK
    {
        public static readonly string[] InputDataIsNotValid = { "input data is not valid", "input data is not valid", "input data is not valid" };
        private const string IRAN_HMD = "IRN";

        /// <summary>
        /// this function generate MRZ( Machine Redable Zone) According to the Doc 9303
        /// </summary>
        /// <param name="PassportType"> Two characters, the first of which shall be A, C or I, shall be used to designate the particular type of document.
        /// The second charactershall be as specified in Note k.</param>
        /// <param name="IssuingState"> The three-letter code specified in Doc 9303-3 shall be used.Spaces shall be replaced by filler characters(<)</param>
        /// <param name="DocumentNumber">As given by the issuing State or organization, 
        /// to uniquely identify the document from all other MROTDs issued by the State or organization.Spaces shall be replaced by filler characters(<).
        /// For additional details see Doc 9303-3.</param>
        /// <param name="BrithDate"> For details see Doc 9303-3 </param>
        /// <param name="Sexuality">F = female; M = male;< = nonspecified.</param>
        /// <param name="ExpiryDate">For details see Doc 9303-3.</param>
        /// <param name="NationalityState">For details see Doc 9303-3.</param>
        /// <param name="Name"></param>
        /// <param name="FamilyName"></param>
        /// <returns>3 Track MRZ Value</returns>
        public string[] MRZGenerator(
            string PassportType, string IssuingState, string DocumentNumber,
            string BrithDate, string Sexuality, string ExpiryDate, string NationalityState,
            string Name, string FamilyName)
        {

            string MRZ01SumOfCheckDigit = "";

            PassportType = PassportType.Replace(" ", "");
            PassportType = PassportType.ToUpper().Trim();

            IssuingState = IssuingState.Replace(" ", "");
            IssuingState = IssuingState.ToUpper().Trim();

            DocumentNumber = DocumentNumber.Replace(" ", "");
            DocumentNumber = DocumentNumber.ToUpper().Trim();

            BrithDate = BrithDate.Replace(" ", "");
            BrithDate = BrithDate.Replace("/", "");
            BrithDate = BrithDate.ToUpper().Trim();

            Sexuality = Sexuality.Replace(" ", "");
            Sexuality = Sexuality.ToUpper().Trim();

            ExpiryDate = ExpiryDate.Replace(" ", "");
            ExpiryDate = ExpiryDate.Replace("/", "");
            ExpiryDate = ExpiryDate.ToUpper().Trim();

            NationalityState = NationalityState.Replace(" ", "");
            NationalityState = NationalityState.ToUpper().Trim();

            Name = RemoveDuplicateSpace(Name);
            Name = Name.ToUpper().Trim();

            FamilyName = RemoveDuplicateSpace(FamilyName);
            FamilyName = FamilyName.ToUpper().Trim();

            string[] MRZ = { "", "", "" };

            #region MRZ 0

            if (String.IsNullOrEmpty(PassportType)) return InputDataIsNotValid;
            else if (PassportType.Length == 1 || PassportType.Length == 2)
            {
                char[] tmp = PassportType.ToCharArray();
                if (tmp[0] == 'A' || tmp[0] == 'C' || tmp[0] == 'I')
                {
                    if (PassportType.Length == 1) MRZ[0] += PassportType + "<";
                    if (PassportType.Length == 2) MRZ[0] += PassportType;  //+ "<";
                }
                else
                    return InputDataIsNotValid;

            }
            else if (PassportType.Length > 2) return InputDataIsNotValid;

            if (IssuingState.Length == 3) MRZ[0] += IssuingState;
            else if (String.IsNullOrEmpty(IssuingState)) MRZ[0] += IRAN_HMD;
            else return InputDataIsNotValid;

            if (DocumentNumber.Length == 9)
            {
                MRZ[0] += DocumentNumber; MRZ01SumOfCheckDigit += DocumentNumber;

                string CheckDigittmp = CheckDigits(DocumentNumber);
                if (CheckDigittmp != "-1") { MRZ[0] += CheckDigittmp; MRZ01SumOfCheckDigit += CheckDigittmp; }
                else return InputDataIsNotValid;
            }
            else
                return InputDataIsNotValid;

            while (MRZ[0].Length < 30) { MRZ[0] += "<"; MRZ01SumOfCheckDigit += "<"; }
            #endregion
            #region MRZ 1
            if (BrithDate.Length == 8)
            {
                BrithDate = BrithDate.Remove(4, 2);
                MRZ[1] += BrithDate;
                MRZ01SumOfCheckDigit += BrithDate;
                string CheckDigittmp = CheckDigits(BrithDate);
                if (CheckDigittmp != "-1") { MRZ[1] += CheckDigittmp; MRZ01SumOfCheckDigit += CheckDigittmp; }
                else return InputDataIsNotValid;
            }
            else
                return InputDataIsNotValid;

            if (Sexuality.Length == 1)
            {
                if (Sexuality == "M" || Sexuality == "F" || Sexuality == "<")
                {
                    MRZ[1] += Sexuality;
                }

            }
            else
                return InputDataIsNotValid;


            if (ExpiryDate.Length == 8)
            {
                ExpiryDate = ExpiryDate.Remove(4, 2);
                MRZ[1] += ExpiryDate;
                MRZ01SumOfCheckDigit += ExpiryDate;
                string CheckDigittmp = CheckDigits(ExpiryDate);
                if (CheckDigittmp != "-1") { MRZ[1] += CheckDigittmp; MRZ01SumOfCheckDigit += CheckDigittmp; }
                else return InputDataIsNotValid;
            }
            else
                return InputDataIsNotValid;

            if (NationalityState.Length == 3) MRZ[1] += IssuingState;
            else if (String.IsNullOrEmpty(NationalityState)) MRZ[1] += IRAN_HMD;
            else return InputDataIsNotValid;

            while (MRZ[1].Length < 29) { MRZ[1] += "<"; MRZ01SumOfCheckDigit += "<"; }





            string MRZ01CheckDigittmp = CheckDigits(MRZ01SumOfCheckDigit);
            if (MRZ01CheckDigittmp != "-1") MRZ[1] += MRZ01CheckDigittmp;
            else return InputDataIsNotValid;



            #endregion
            #region MRZ 2

            Name = Name.Replace(" ", "<");
            FamilyName = FamilyName.Replace(" ", "<");
            if ((Name + FamilyName).Length > 28)
            {
                Name = Name.Substring(0, 14);
                FamilyName = FamilyName.Substring(0, 14);
            }
            MRZ[2] = FamilyName + "<<" + Name;
            MRZ[2] = MRZ[2].Replace("<<<", "<<");
            while (MRZ[2].Length < 30) { MRZ[2] += "<"; }

            #endregion

            return MRZ;
        }
        /// <summary>
        /// Remove Duplicate Space in a String
        /// </summary>
        /// <param name="Input">Input String</param>
        /// <returns>Trimed String</returns>
        private string RemoveDuplicateSpace(string Input)
        {
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[ ]{2,}", options);
            return regex.Replace(Input, " ");
        }
        /// <summary>
        /// Check Digit According to 9303
        /// </summary>
        /// <param name="inputVariable"></param>
        /// <returns></returns>
        private string CheckDigits(string inputVariable)
        {
            string ReturnCheckDigit = "";
            inputVariable = inputVariable.ToUpper();
            char[] inputVariableArray = inputVariable.ToCharArray();
            const byte WeightingValue1 = 7, WeightingValue2 = 3, WeightingValue3 = 1;
            byte WeightingValue = 0;
            int SumOfdigit = 0;

            int WeightingCounter = 0;

            for (int i = 0; i < inputVariableArray.Length; i++)
            {
                switch (WeightingCounter)
                {
                    case 0: WeightingValue = WeightingValue1; break;
                    case 1: WeightingValue = WeightingValue2; break;
                    case 2: WeightingValue = WeightingValue3; break;
                    default: WeightingCounter = 0; WeightingValue = WeightingValue1; break;
                }
                SumOfdigit += GetCharacterValue(inputVariableArray[i]) * WeightingValue;
                if (++WeightingCounter > 2) WeightingCounter = 0;
            }
            ReturnCheckDigit = (SumOfdigit % 10).ToString();
            if (ReturnCheckDigit.Length != 1) return "-1";
            return ReturnCheckDigit;

        }
        private byte GetCharacterValue(char InputValue)
        {
            byte ReturnCharacterValue = 0;


            if (InputValue == '0' || InputValue == '1' || InputValue == '2' || InputValue == '3' || InputValue == '4' || InputValue == '5' ||
                InputValue == '6' || InputValue == '7' || InputValue == '8' || InputValue == '9' || InputValue == 'A' || InputValue == 'B' ||
                InputValue == 'C' || InputValue == 'D' || InputValue == 'E' || InputValue == 'F' || InputValue == 'G' || InputValue == 'H' ||
                InputValue == 'I' || InputValue == 'J' || InputValue == 'K' || InputValue == 'L' || InputValue == 'M' || InputValue == 'N' ||
                InputValue == 'O' || InputValue == 'P' || InputValue == 'Q' || InputValue == 'R' || InputValue == 'S' || InputValue == 'T' ||
                InputValue == 'U' || InputValue == 'V' || InputValue == 'W' || InputValue == 'X' || InputValue == 'Y' || InputValue == 'Z' ||
                InputValue == '<')
            {
                switch (InputValue)
                {
                    case '0': ReturnCharacterValue = 0; break;
                    case '1': ReturnCharacterValue = 1; break;
                    case '2': ReturnCharacterValue = 2; break;
                    case '3': ReturnCharacterValue = 3; break;
                    case '4': ReturnCharacterValue = 4; break;
                    case '5': ReturnCharacterValue = 5; break;
                    case '6': ReturnCharacterValue = 6; break;
                    case '7': ReturnCharacterValue = 7; break;
                    case '8': ReturnCharacterValue = 8; break;
                    case '9': ReturnCharacterValue = 9; break;
                    case 'A': ReturnCharacterValue = 10; break;
                    case 'B': ReturnCharacterValue = 11; break;
                    case 'C': ReturnCharacterValue = 12; break;
                    case 'D': ReturnCharacterValue = 13; break;
                    case 'E': ReturnCharacterValue = 14; break;
                    case 'F': ReturnCharacterValue = 15; break;
                    case 'G': ReturnCharacterValue = 16; break;
                    case 'H': ReturnCharacterValue = 17; break;
                    case 'I': ReturnCharacterValue = 18; break;
                    case 'J': ReturnCharacterValue = 19; break;
                    case 'K': ReturnCharacterValue = 20; break;
                    case 'L': ReturnCharacterValue = 21; break;
                    case 'M': ReturnCharacterValue = 22; break;
                    case 'N': ReturnCharacterValue = 23; break;
                    case 'O': ReturnCharacterValue = 24; break;
                    case 'P': ReturnCharacterValue = 25; break;
                    case 'Q': ReturnCharacterValue = 26; break;
                    case 'R': ReturnCharacterValue = 27; break;
                    case 'S': ReturnCharacterValue = 28; break;
                    case 'T': ReturnCharacterValue = 29; break;
                    case 'U': ReturnCharacterValue = 30; break;
                    case 'V': ReturnCharacterValue = 31; break;
                    case 'W': ReturnCharacterValue = 32; break;
                    case 'X': ReturnCharacterValue = 33; break;
                    case 'Y': ReturnCharacterValue = 34; break;
                    case 'Z': ReturnCharacterValue = 35; break;
                    case '<': ReturnCharacterValue = 0; break;
                    default: ReturnCharacterValue = 40; break;
                }
            }
            else
                ReturnCharacterValue = 40;

            return ReturnCharacterValue;


        }
    }
}