using System;

namespace TeambuildingApp
{
    /// <summary>
    /// In deze klasse wordt de werking (= blauwdruk) van het 
    /// hoofdmenu gecodeerd. Een 'WerkingMenu'-klasse 
    /// gaat altijd samen met een bijhorende 'InvullingMenu'-klasse.
    /// </summary>
    public class WerkingHoofdmenu
    {
        #region velden op instantie-niveau

        /// <summary>
        /// Is de keuze die de gebruiker maakt in het hoofdmenu-keuzemenu.
        /// </summary>
        private int keuzeIndex;

        /// <summary>
        /// Alle mogelijke keuzes in het hoofdmenu-keuzemenu.
        /// </summary>
        private string[] keuzes;

        /// <summary>
        /// De hoofding in het hoofdmenu-keuzemenu.
        /// </summary>
        private string menuHoofding;

        /// <summary>
        /// De subhoofding in het hoofdmenu-keuzemenu.
        /// </summary>
        private string menuSubHoofding;

        /// <summary>
        /// De hoofdtitel van het hoofdmenu-keuzemenu.
        /// </summary>
        private string hoofdtitel;

        /// <summary>
        /// De ondertitel van het hoofdmenu-keuzemenu.
        /// </summary>
        private string ondertitel;

        #endregion

        #region constructor

        /// <summary>
        /// Hier wordt de hoofdmenu-instantie aangemaakt.
        /// </summary>
        /// <param name="menuhoofding">De hoofding in het hoofdmenu-keuzemenu.</param>
        /// <param name="menusubhoofding">De subhoofding in het hoofdmenu-keuzemenu.</param>
        /// <param name="keuzes">Alle mogelijke keuzes in het hoofdmenu-keuzemenu.</param>
        /// <param name="hoofdtitel">De hoofdtitel van het hoofdmenu-keuzemenu.</param>
        /// <param name="ondertitel">De ondertitel van het hoofdmenu-keuzemenu.</param>
        public WerkingHoofdmenu( string menuhoofding, string menusubhoofding, string[] keuzes, string hoofdtitel, string ondertitel )
        {
            menuHoofding = menuhoofding;
            menuSubHoofding = menusubhoofding;
            this.keuzes = keuzes;
            this.hoofdtitel = hoofdtitel;
            this.ondertitel = ondertitel;
            keuzeIndex = 0;

        }

        #endregion

        #region methoden op instantie-niveau

        /// <summary>
        /// Deze methode activeert bepaalde toetsen van het toetsenbord
        /// die mogen worden gebruikt om een keuze te maken in het 
        /// hoofdmenu-keuzemenu en deze te bevestigen.
        /// </summary>
        /// <returns>Geeft de keuze terug die de gebruiker maakt in het hoofdmenu-keuzemenu.</returns>
        public int RunHoofdmenu()
        {
            //  gedrukte toetsen omzetten in 'keuzes'
            ConsoleKey gedrukteToets;
            do
            {
                Console.Clear();
                LaatHoofdmenuZien();

                ConsoleKeyInfo toetsInfo = Console.ReadKey( true );
                gedrukteToets = toetsInfo.Key;

                // keuzeIndex wordt gekozen adhv ↑ en ↓ : 'cyclische' werking!
                if ( gedrukteToets == ConsoleKey.UpArrow )
                {
                    keuzeIndex--;

                    // Als je hoger gaat, begin je terug vanonder
                    if ( keuzeIndex == -1 )
                    {
                        keuzeIndex = keuzes.Length - 1;
                    }
                }
                else if ( gedrukteToets == ConsoleKey.DownArrow )
                {
                    keuzeIndex++;

                    // Als je lager gaat, begin je terug vanboven
                    if ( keuzeIndex == keuzes.Length )
                    {
                        keuzeIndex = 0;
                    }
                }

            } while ( gedrukteToets != ConsoleKey.Enter );

            return keuzeIndex;
        }

        /// <summary>
        ///  Methode die het uitzicht van het hoofdmenu-keuzemenu bepaalt:
        /// de exacte plaats van alle velden, eventueel 'extra' styling,
        /// hoe de 'knoppen' qua uiterlijk moeten reageren en wanneer en
        /// hoe deze worden weergegeven in de console.
        /// </summary>
        private void LaatHoofdmenuZien()
        {
            Console.WriteLine( hoofdtitel );
            Console.WriteLine( ondertitel );
            Console.WriteLine();
            Styling.Center( menuHoofding );
            Console.WriteLine();
            Styling.Center( menuSubHoofding );
            System.Console.WriteLine( "\n\n\n" );

            // bij elk menu-item gebeurt er iets
            for ( int i = 0; i < keuzes.Length; i++ )
            {
                string huidigeKeuze = keuzes[i];

                // Verschijnt voor het gekozen menu-item
                string prefix;

                if ( i == keuzeIndex )
                {
                    // zo maken we de 'keuze' extra zichtbaar
                    prefix = "-->";
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    prefix = " ";
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Styling.Center( $"{prefix}  ###  {huidigeKeuze}  ###\n" );
            }
            Console.ResetColor();
        }

        #endregion  
    }
}
