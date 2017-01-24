using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Globalization;

namespace App12
{
    class FindImage
    {
        static string[] keyterms = new string[] {  "tooth", "brush", "teeth", "dentist", // i <= 3 -> Toothbrush
                                                "lunch", "dinner", "breakfast", "eat", "food", "dessert", "taste", // 4 <= i && i <= 10 -> Fork & knife
                                                "movie", "film", "flick",   //  11 <= i && i <= 13 -> Movie ticket
                                                "walk", "run", "jog",   //  14 <= i && i <= 16 -> Man running
                                                "lift", "weights", "gym", "workout", "exercise",    //  17 <= i && i <= 21 -> Barbell
                                                "shop", "buy",  //  22 <= i && i <= 23 -> Shopping bag
                                                "bathroom", "potty", "restroom", "facilities",  //  24 <= i && i <= 27 -> Bathroom
                                                "shower", "bath",   //  28 <= i && i <= 29 -> Shower
                                                "appointment", "doctor", "checkup", "shot", "hospital", "nurse", "medicine",    //  30 <= i && i <= 36 -> Red cross
                                                "homework", "write", "draw", "work",    //  37 <= i && i <= 40 -> pencil
                                                "bus", "metro", //  41 <= i && <= 42 -> Front facing bus
                                                "school", "class", //   43 <= i && i <= 44 -> school house
                                                "swim", "pool"  //  45 - 46
                                                };

        static public string ParseForImage(string title)
        {
            
            for (int i = 0; i < keyterms.Length; i++)
            {
                CultureInfo culture = CultureInfo.CurrentCulture;

                if (culture.CompareInfo.IndexOf(title, keyterms[i], CompareOptions.IgnoreCase) >= 0)
                {
                    if (i <= 3)
                        return "toothbrush.png";
                    else if (i <= 10)
                        return "fork.png";
                    else if (i <= 13)
                        return "movie.png";
                    else if (i <= 16)
                        return "running.png";
                    else if (i <= 21)
                        return "barbell.png";
                    else if (i <= 23)
                        return "shopping-bag.png";
                    else if (i <= 27)
                        return "toilet.png";
                    else if (i <= 29)
                        return "shower.png";
                    else if (i <= 36)
                        return "red-cross.png";
                    else if (i <= 40)
                        return "pencil.png";
                    else if (i <= 42)
                        return "bus.png";
                    else if (i <= 44)
                        return "university.png";
                    else if (i <= 46)
                        return "swimming.png";


                }
            }
            return "alarm.png";
        }
    }
}