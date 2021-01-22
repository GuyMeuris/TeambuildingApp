using System;

namespace TeambuildingApp
{
    /// <summary>
    /// In deze klasse wordt de werking (= blauwdruk) van het 
    /// activiteiten-menu gecodeerd. Een 'WerkingMenu'-klasse 
    /// gaat altijd samen met een bijhorende 'InvullingMenu'-klasse.
    /// </summary>
    public class WerkingActiviteitenMenus
    {
        #region velden op instantie-niveau

        /// <summary>
        /// Is de keuze die de gebruiker maakt in het Activiteiten-keuzemenu.
        /// </summary>
        private int keuzeIndex;

        /// <summary>
        /// Alle mogelijke keuzes in het Activiteiten-keuzemenu.
        /// </summary>
        private string[] keuzes;

        /// <summary>
        /// De hoofding in het Activiteiten-keuzemenu.
        /// </summary>
        private string menuHoofding;

        /// <summary>
        /// De subhoofding in het Activiteiten-keuzemenu.
        /// </summary>
        private string menuSubHoofding;

        /// <summary>
        /// De hoofdtitel van het Activiteiten-keuzemenu.
        /// </summary>
        private string hoofdtitel;

        #endregion

        #region constructor

        /// <summary>
        /// Hier wordt de Activiteitenmenu-instantie aangemaakt. 
        /// </summary>
        /// <param name="menuhoofding">De hoofding in het Activiteiten-keuzemenu.</param>
        /// <param name="menusubhoofding">De subhoofding in het Activiteiten-keuzemenu.</param>
        /// <param name="keuzes">Alle mogelijke keuzes in het Activiteiten-keuzemenu.</param>
        /// <param name="hoofdtitel">De hoofdtitel van het Activiteiten-keuzemenu.</param>
        public WerkingActiviteitenMenus( string menuhoofding, string menusubhoofding, string[] keuzes, string hoofdtitel )
        {
            keuzeIndex = 0;
            this.keuzes = keuzes;
            menuHoofding = menuhoofding;
            menuSubHoofding = menusubhoofding;
            this.hoofdtitel = hoofdtitel;
        }

        #endregion

        #region methoden op instantie-niveau

        /// <summary>
        /// Deze methode activeert bepaalde toetsen van het toetsenbord
        /// die mogen worden gebruikt om een keuze te maken in het 
        /// Activiteiten-keuzemenu en deze te bevestigen.
        /// </summary>
        /// <returns>Geeft de keuze terug die de gebruiker maakt in het Activiteiten-keuzemenu.</returns>
        public int RunActiviteitenMenu()
        {
            //  gedrukte toetsen omzetten in 'keuzes'
            ConsoleKey gedrukteToets;
            do
            {
                Console.Clear();
                LaatActiviteitenMenuZien();

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
        /// Methode die het uitzicht van het Activiteiten-keuzemenu bepaalt:
        /// de exacte plaats van alle velden, eventueel 'extra' styling,
        /// hoe de 'knoppen' qua uiterlijk moeten reageren en wanneer en
        /// hoe deze worden weergegeven in de console.
        /// </summary>
        private void LaatActiviteitenMenuZien()
        {
            Console.WriteLine( hoofdtitel );
            Console.WriteLine( menuHoofding );
            Console.WriteLine();
            Console.WriteLine( Styling.InKleur( @"
            o                                                      (O)           (O)
          o  o                                                     ||     (O)    ||
          o o o                                .----.              ||     ||     ||
        o                                     /   O O\            /  \    ||    /  \
       o    ______          ______            '    O  '          :    :  /  \  :    :
       _ *o(_||___)________/___               \      /           |    | :    : |    |
     O(_)(       o  ______/    \             __`----'_____________\__/__|    |__\__/___
    > ^  `/------o-'            \                                        \__/ 
  D|_|___/                                                          ", "#00ffea" ) );
            Console.WriteLine( "\n" );
            Styling.Center( menuSubHoofding );
            System.Console.WriteLine( "\n" );

            // bij elk menu-item verandert er iets
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
