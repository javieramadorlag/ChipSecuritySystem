using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
Date: 15/02/2024
Name: Javier Amador Lagunes
*/

namespace ChipSecuritySystem
{
    class Program
    {
        //Default colors given in the exercise (could be changed)
        static ColorChip DefaultChip = new ColorChip(Color.Blue, Color.Green);

        //Default chips given in the exercise (could be changed)
        static List<ColorChip> ChipsBag = new List<ColorChip>
        {
            new ColorChip(Color.Blue, Color.Yellow),
            new ColorChip(Color.Red, Color.Green),
            new ColorChip(Color.Yellow, Color.Red),
            new ColorChip(Color.Orange, Color.Purple)
        };

        private static void ProcessChips()
        {
            List<ColorChip> ProcessedChipsBag = new List<ColorChip>();
            List<ColorChip> CopyChipsBag = new List<ColorChip>(ChipsBag);  //Use a copy to manipulate chips
            int sizeChipBag = ChipsBag.Count;

            var pointerStartColor = DefaultChip.StartColor;
            var pointerEndColor = DefaultChip.EndColor;

            //Every chip has to be processed
            for (int i = 0; i < sizeChipBag; i++)
            {
                //Find the next chip
                int index = CopyChipsBag.FindIndex(c => c.StartColor == pointerStartColor);

                if (index >= 0)
                {
                    var chip = CopyChipsBag[index];

                    //If is the last chip
                    if (chip.EndColor == pointerEndColor)
                    {
                        pointerStartColor = chip.EndColor;
                        ProcessedChipsBag.Add(chip);
                        CopyChipsBag.RemoveAt(index);
                        break; // Exit the loop
                    }
                    else
                    {
                        pointerStartColor = chip.EndColor;
                        ProcessedChipsBag.Add(chip);
                        CopyChipsBag.RemoveAt(index);
                    }
                }
                else
                {
                    //Chip without use
                }
            }

            //If we have at least one chip and the start and end color matches
            if (ProcessedChipsBag.Count > 0 
                && ProcessedChipsBag.FirstOrDefault().StartColor == DefaultChip.StartColor 
                && ProcessedChipsBag.LastOrDefault().EndColor == DefaultChip.EndColor)
            {
                StringBuilder result = new StringBuilder();
                result.Append(DefaultChip.StartColor);
                foreach (var item in ProcessedChipsBag)
                {
                    result.Append(string.Format("[{0}]", item.ToString()));
                }
                result.Append(DefaultChip.EndColor);

                //Success
                Console.WriteLine(String.Format("The result that successfully links the {0} and {1} markers is:", DefaultChip.StartColor, DefaultChip.EndColor));
                Console.WriteLine(result);

                //Handle chips without using
                if (CopyChipsBag.Count > 0)
                {
                    StringBuilder chipsWithoutUse = new StringBuilder();
                    chipsWithoutUse.Append("\nIn this instance, the following chips are not used: ");
                    foreach (var item in CopyChipsBag)
                    {
                        chipsWithoutUse.Append(string.Format("[{0}]", item.ToString()));
                    }
                    Console.WriteLine(chipsWithoutUse);

                }
            }
            else
            {
                //Handle error
                Console.WriteLine(Constants.ErrorMessage);
            }
            
        }

        static void Main(string[] args)
        {
            //If we do not have chips
            if (ChipsBag.Count == 0)
            {
                Console.WriteLine(Constants.ErrorMessage);
                Console.ReadLine(); // Wait
                return;
            }
            else
            {
                Console.WriteLine("The bi-colored chips are: \n");
                foreach (var item in ChipsBag)
                {
                    Console.WriteLine(item.ToString());
                }
                Console.WriteLine();
                ProcessChips();
                Console.ReadLine(); // Wait
            }
        }
    }
}
