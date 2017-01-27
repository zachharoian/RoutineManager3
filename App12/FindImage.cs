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
                                                "lift", "barbell", "gym", "workout", "exercise",    //  17 <= i && i <= 21 -> Barbell
                                                "shop", "buy",  //  22 <= i && i <= 23 -> Shopping bag
                                                "bathroom", "potty", "restroom", "facilities",  //  24 <= i && i <= 27 -> Bathroom
                                                "shower", "bath",   //  28 <= i && i <= 29 -> Shower
                                                "appointment", "doctor", "checkup", "shot", "hospital", "nurse", "dr.",    //  30 <= i && i <= 36 -> Red cross
                                                "homework", "write", "draw", "study",    //  37 <= i && i <= 40 -> pencil
                                                "bus", "metro", //  41 <= i && <= 42 -> Front facing bus
                                                "school", "class", //   43 <= i && i <= 44 -> school house
                                                "swim", "pool",  //  45 - 46

                                                //  Second Pack
                                                "guitar",   // 47
                                                "airplane", "flight", // 49
                                                "football", //  50
                                                "ballet", "ballerina",  //  52
                                                "baseball", //  53
                                                "bike", "bicycle",  //  55
                                                "sail", "boat", //  57
                                                "bowl", //  58
                                                "birth",    //  59
                                                "pack", "suitcase", "work", //  62
                                                "funeral",  //  63
                                                "coffee",   //  64
                                                "dress", "cloth", "jacket", "coat", //  68
                                                "haircut",  //  69
                                                "bed", //   70
                                                "drum", //  71
                                                "zoo", "animals",   //  73
                                                "gas", "oil",   //  75
                                                "bible", "church", "faith", "jesus",    //  79
                                                "flight", "airplane", "international", //    82
                                                "iron", "laundry",  //  84
                                                "science", "laboratory", "microscope",    //  87
                                                "earth", "leaf", "green", //    90
                                                "court", "judge", "jury",   //  93
                                                "scooba", "diving", "dive", //  96
                                                "medicine", "pill", "vitamin",  //  99
                                                "sing", "vocal", // 101
                                                "music", // 102
                                                "scale", "weight",  //  104
                                                "eye", "vision", "optometrist", //  107
                                                "vacation", "beach",    //  109
                                                "piano",    //  110
                                                "save", "savings", "piggy bank", "money", //    114
                                                "ping", "pong", "table tennis", "table-tennis", //  118
                                                "pizza", // 119
                                                "pray", //  120
                                                "buddha", //    121
                                                "shoe", //  122
                                                "archer", //    123
                                                "tea", //   124
                                                "floss",    //  125
                                                "theat", "drama", "play", //    128
                                                "drum", "bongo", "conga", //    131
                                                "violin", "cello", //   133
                                                "wash", "dry"   //  135
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
                        return "shapes.png";
                    else if (i <= 16)
                        return "running.png";
                    else if (i <= 21)
                        return "barbell.png";
                    else if (i <= 23)
                        return "shopping-cart.png";
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

                    //  Pack 2
                    else if (i <= 47)
                        return "guitar.png";
                    else if (i <= 49)
                        return "airplane.png";
                    else if (i <= 50)
                        return "american-football.png";
                    else if (i <= 52)
                        return "ballerina.png";
                    else if (i <= 53)
                        return "bat.png";
                    else if (i <= 55)
                        return "bicycle.png";
                    else if (i <= 57)
                        return "boat.png";
                    else if (i <= 58)
                        return "bowling-ball.png";
                    else if (i <= 59)
                        return "birthday-cake-with-candle.png";
                    else if (i <= 62)
                        return "business.png";
                    else if (i <= 63)
                        return "christian-coffin.png";
                    else if (i <= 64)
                        return "coffee.png";
                    else if (i <= 68)
                        return "cold.png";
                    else if (i <= 69)
                        return "cut.png";
                    else if (i <= 70)
                        return "double-bed.png";
                    else if (i <= 71)
                        return "drumsticks.png";
                    else if (i <= 73)
                        return "elephant-facing-left.png";
                    else if (i <= 75)
                        return "gasoline-pump.png";
                    else if (i <= 79)
                        return "holy-bible.png";
                    else if (i <= 82)
                        return "international-flights.png";
                    else if (i <= 84)
                        return "iron-electric-heat.png";
                    else if (i <= 87)
                        return "laboratory-microscope.png";
                    else if (i <= 90)
                        return "leaf-black-shape.png";
                    else if (i <= 93)
                        return "legal-hammer-black-silhouette.png";
                    else if (i <= 96)
                        return "mask.png";
                    else if (i <= 99)
                        return "medicines.png";
                    else if (i <= 101)
                        return "microphone.png";
                    else if (i <= 102)
                        return "musical-note.png";
                    else if (i <= 104)
                        return "old-scale.png";
                    else if (i <= 107)
                        return "ophthalmology.png";
                    else if (i <= 109)
                        return "palm-tree.png";
                    else if (i <= 110)
                        return "piano.png";
                    else if (i <= 114)
                        return "pig-money-safe.png";
                    else if (i <= 118)
                        return "ping-pong.png";
                    else if (i <= 119)
                        return "pizza-triangle-outline.png";
                    else if (i <= 120)
                        return "praying-hands.png";
                    else if (i <= 121)
                        return "sitting-buddha.png";
                    else if (i <= 122)
                        return "sport-shoe.png";
                    else if (i <= 123)
                        return "target.png";
                    else if (i <= 124)
                        return "teapot-facing-left.png";
                    else if (i <= 125)
                        return "teeth.png";
                    else if (i <= 128)
                        return "theater-masks.png";
                    else if (i <= 131)
                        return "timpani.png";
                    else if (i <= 133)
                        return "violin.png";
                    else if (i <= 135)
                        return "washer-machine.png";
                }
            }
            return "alarm.png";
        }
    }
}