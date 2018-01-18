using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TimSort
{
    class Files  //Class Files
    {
       public String File_Path = "C:\\Users\\Waqas Ahmed\\Desktop\\DATA.txt";              //Reading Path
       public String Save_Path = "C:\\Users\\Waqas Ahmed\\Desktop\\Sorted Data.txt";       //Saving Path

        public int[] ReadFile()      //File Reading Method retuns Int Array.
        {
            try
            {
                StreamReader Read = new StreamReader(File_Path);    //Object Created
                String Integer = Read.ReadToEnd();                  //Saved in a String
                String[] Number = Integer.Split('\n');              //Converting to String Array using split function
                int[] Array = new int[Number.Length];               //Declared int Array 
                for (int i = 0; i < Number.Length; i++)             //Converting String array into int array
                {
                    String Temp = Number[i];
                    Int32.TryParse(Temp, out Array[i]);
                }
                return Array;
            }
            catch
            {
                Console.WriteLine("File not Found!!");         //Exception Handeling
            }
            
            return null;
        }

        public void WriteFile(int[] a)
        {
            try
            {
                FileStream Data = new FileStream(Save_Path, FileMode.Create, FileAccess.Write);     //Making a File
                StreamWriter Input = new StreamWriter(Data);                                        //Object Created
                for (int i = 0; i < a.Length; i++)
                {
                    Input.WriteLine("{0}", a[i]);                                                   //Writing File
                }
                Input.Close();                                                                     //File Closed
            }
            catch { Console.WriteLine("File Path Incorrect!!"); }                                  //Exception Handeling
        }
    }
    class TimSort
    {
       public int RUN = 32;
        //Size of Run

       public int[] Insertion(int[] insertion, int Start, int End)    //Insertion sort for Storting Intervals
       {
           int temp, min;
           for (int i = Start + 1; i < End; i++)
           {
               temp = insertion[i];
               min = i;
               while (min > Start && insertion[min - 1] >= temp)
               {
                   insertion[min] = insertion[min - 1];
                   min -= 1;
               }
               insertion[min] = temp;
           }
           return insertion;      //Return Array
       }

       public void Merger(int[] Array, int l, int m, int r)     //Merge Function
       {

          int LLength = m - l + 1, RLength = r - m;             //Dividing Array in Half
          int[] Left = new int[LLength];                        //Initailizing Array of Half Length
          int[] Right = new int[RLength];

          for (int i = 0; i < LLength; i++)                     //Copying Data in new Array. Left Side
               Left[i] = Array[l + i];

          for (int i = 0; i < RLength; i++)                     //Copying Data in new Array. Right Side
               Right[i] = Array[m + 1 + i];
 
          int L_Marker = 0;                                     //Pointer for Left Array
          int R_Marker = 0;                                     //Pointer for Right Array
          int O_Marker = l;                                     //Original Array Pointer

          while (L_Marker < LLength && R_Marker < RLength)      //Repeat until Pointer Don't Exceed Array Lenght
          {
              if (Left[L_Marker] <= Right[R_Marker])            //If Left Array has a Small Element
             {
                 Array[O_Marker] = Left[L_Marker];              //Copy element into Original Array
                 L_Marker++;
             }
             else
             {
                 Array[O_Marker] = Right[R_Marker];            //Copy Right Array Element
                 R_Marker++;
             }
              O_Marker++;
          }

          while (L_Marker < LLength)                          //Copy rest of the Left Array elements. If any Left
         {
             Array[O_Marker] = Left[L_Marker];
             O_Marker++;
             L_Marker++;
         }

          while (R_Marker < RLength)                         //Copy rest of the Right Array elements. If any Left
         {
             Array[O_Marker] = Right[R_Marker];
             O_Marker++;
             R_Marker++;
         }
       }

       public void TIMSORT(int[] Array, int n)              //Timsort Method 
       {
           for (int i = 0; i < n; i += RUN)                 //Calling Insertion Sort on RUN intervals
           {
               Insertion(Array, i, min((i + 31), (n)));
           }

          for (int size = RUN; size < n; size = 2*size)
           {
             for (int left = 0; left < n; left += 2*size)
             {
               int mid = left + size - 1;
               int right = min((left + 2*size - 1), (n-1));
               Merger(Array, left, mid, right);
             }
           }
         }

         public void Show(int[] arr)                         //For printing Array
         {
           foreach(int x in arr)
              Console.Write("{0} ",x);
         }

         public int min(int a, int b)                       //For Minimum Value
         {
           if (a > b)
             return b;
           else
             return a;
        }
}
    class Program
    {
        static void Main(string[] args)
        {
            TimSort Sort = new TimSort();
            Files IO = new Files();

            int[] Number_List = IO.ReadFile();
            Console.WriteLine("\nReading File From: {0}",IO.File_Path);
            Sort.TIMSORT(Number_List, Number_List.Length);

            Console.WriteLine("\nAfter Sorting Array:");
            Sort.Show(Number_List);

            IO.WriteFile(Number_List);
            Console.WriteLine("\n\nData Saved at: {0}",IO.Save_Path);

             Console.ReadKey();
 
        }
    }
}
