using System;

namespace TeambuildingApp
{
    /// <summary>
    /// In deze klasse wordt de werking (= blauwdruk) van het 
    /// inschrijvingsmenu gecodeerd. Een 'WerkingMenu'-klasse 
    /// gaat altijd samen met een bijhorende 'InvullingMenu'-klasse.
    /// </summary>
    public class WerkingInschrijvingenMenu
    {
        #region velden op instantie-niveau

        /// <summary>
        /// Is de keuze die de gebruiker maakt in het Inschrijvingen-keuzemenu.
        /// </summary>
        private int keuzeIndex;

        /// <summary>
        /// Alle mogelijke keuzes in het Inschrijvingen-keuzemenu.
        /// </summary>
        private string[] keuzes;

        /// <summary>
        /// De hoofding in het Inschrijvingen-keuzemenu.
        /// </summary>
        private string menuHoofding;

        /// <summary>
        /// De subhoofding in het Inschrijvingen-keuzemenu.
        /// </summary>
        private string menuSubHoofding;

        /// <summary>
        /// De hoofdtitel van het Inschrijvingen-keuzemenu.
        /// </summary>
        private string hoofdtitel;

        #endregion

        #region constructor

        /// <summary>
        /// Hier wordt de Inschrijvingsmenu-instantie aangemaakt.
        /// </summary>
        /// <param name="menuhoofding">De hoofding in het Inschrijvingen-keuzemenu.</param>
        /// <param name="menusubhoofding">De subhoofding in het Inschrijvingen-keuzemenu.</param>
        /// <param name="keuzes">Alle mogelijke keuzes in het Inschrijvingen-keuzemenu.</param>
        /// <param name="hoofdtitel">De hoofdtitel van het Inschrijvingen-keuzemenu.</param>
        public WerkingInschrijvingenMenu( string menuhoofding, string menusubhoofding, string[] keuzes, string hoofdtitel )
        {
            keuzeIndex = 0;
            this.keuzes = keuzes;
            this.menuHoofding = menuhoofding;
            this.menuSubHoofding = menusubhoofding;
            this.hoofdtitel = hoofdtitel;
        }

        #endregion

        #region methoden op instantie-niveau

        /// <summary>
        /// Deze methode activeert bepaalde toetsen van het toetsenbord
        /// die mogen worden gebruikt om een keuze te maken in het 
        /// Inschrijvingen-keuzemenu en deze te bevestigen.
        /// </summary>
        /// <returns>Geeft de keuze terug die de gebruiker maakt in het Inschrijvingen-keuzemenu.</returns>
        public int RunInschrijvingenMenu()
        {
            //  gedrukte toetsen omzetten in 'keuzes'
            ConsoleKey gedrukteToets;
            do
            {
                Console.Clear();
                LaatInschrijvingenMenuZien();

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
        /// Methode die het uitzicht van het Inschrijvingen-keuzemenu bepaalt:
        /// de exacte plaats van alle velden, eventueel 'extra' styling,
        /// hoe de 'knoppen' qua uiterlijk moeten reageren en wanneer en
        /// hoe deze worden weergegeven in de console.
        /// </summary>
        private void LaatInschrijvingenMenuZien()
        {
            Console.WriteLine( hoofdtitel );
            Styling.Center( menuHoofding );
            Console.WriteLine( Styling.InKleur( @"
  ______________________________                       ____________________
 / \                             \.                   /                    \                
|   |    * Wiebe Nik             |.                   |     Proficiat,     |
 \_ |    * Nie Mandal            |.                   |       U bent       |
    |    * Tis Marnenaam         |.                   |    ingeschreven!   |
    |    * Kaner Nogbij          |.                   \____________________/     
    |    * Dee sisverdeshau      |.                            !  !
    |    * Wadde nonzinhie       |.                            L_ !
    |    * Wiele Esdahierna      |.                           / _)!
    |    * ???                   |.                          / /__L
    |   _________________________|___                  _____/ (____)
    |  /                            /.                        (____)
    \_/____________________________/.                  _____  (____)
                                                            \_(____)
                                                               !  !
                                                               !  !
                                                               \__/   ", "#00ffea" ) );
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
