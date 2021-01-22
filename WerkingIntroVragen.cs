using System;

namespace TeambuildingApp
{
    /// <summary>
    /// In deze klasse wordt de werking (= blauwdruk) van de 
    /// app-introductievragen gecodeerd. Een 'WerkingMenu'-klasse 
    /// gaat altijd samen met een bijhorende 'InvullingMenu'-klasse.
    /// </summary>
    public class WerkingIntroVragen
    {
        #region velden op instantie-niveau

        /// <summary>
        /// Is de keuze die de gebruiker maakt bij de app-introductievragen.
        /// </summary>
        private int keuzeIndex;

        /// <summary>
        /// Alle mogelijke keuzes bij de app-introductievragen.
        /// </summary>
        private string[] keuzes;

        /// <summary>
        /// 1 van de vragen die worden gesteld bij de app-introductievragen.
        /// </summary>
        private string vraag;

        /// <summary>
        /// Een eventuele opmerking bij de app-introductievragen.
        /// </summary>
        private string opmerking;

        #endregion

        #region constructor

        /// <summary>
        /// Hier wordt een instantie gemaakt van het object 'app-introductievragen'.
        /// </summary>
        /// <param name="keuzes">Alle mogelijke keuzes bij de app-introductievragen.</param>
        /// <param name="vraag">1 van de vragen die worden gesteld bij de app-introductievragen.</param>
        /// <param name="opmerking">Een eventuele opmerking bij de app-introductievragen.</param>
        public WerkingIntroVragen( string[] keuzes, string vraag, string opmerking )
        {
            this.keuzes = keuzes;
            this.vraag = vraag;
            this.opmerking = opmerking;
            keuzeIndex = 0;

        }

        #endregion

        #region methoden op instantie-niveau

        /// <summary>
        /// Deze methode activeert bepaalde toetsen van het toetsenbord
        /// die mogen worden gebruikt om een keuze te maken bij de  
        /// app-introductievragen en deze te bevestigen.
        /// </summary>
        /// <returns>Geeft de keuze terug die de gebruiker maakt bij de  
        /// app-introductievragen.</returns>
        public int RunIntroVraag1()
        {
            //  gedrukte toetsen omzetten in 'keuzes'
            ConsoleKey gedrukteToets;
            do
            {
                Console.Clear();
                LaatIntroVraag1Zien();

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
        /// Methode die het uitzicht van de app-introductievragen bepaalt:
        /// de exacte plaats van alle velden, eventueel 'extra' styling,
        /// hoe de 'knoppen' qua uiterlijk moeten reageren en wanneer en
        /// hoe deze worden weergegeven in de console.
        /// </summary>
        private void LaatIntroVraag1Zien()
        {
            Console.SetCursorPosition( 0, 8 );
            Styling.Center( vraag );
            Console.WriteLine();
            Styling.Center( opmerking );
            Console.WriteLine();

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
