using System;
using System.Collections.Generic;

namespace TeambuildingApp
{
    /// <summary>
    /// In deze klasse wordt de werking (= blauwdruk) van de 
    /// VerwijderActiviteit-menu gecodeerd. Een 'WerkingMenu'-klasse 
    /// gaat altijd samen met een bijhorende 'InvullingMenu'-klasse.
    /// </summary>
    public class WerkingVerwijderActiviteitMenu
    {
        #region velden op instantie-niveau

        /// <summary>
        /// Is de keuze die de gebruiker maakt in het VerwijderActiviteit-keuzemenu.
        /// </summary>
        private int keuzeIndex;

        /// <summary>
        /// De lijst 'lijstActMetBegeleider' met alle mogelijke activiteiten.
        /// </summary>
        private List<Tuple<Activiteiten, Begeleider>> lijstActMetBegeleider = new List<Tuple<Activiteiten, Begeleider>>();

        /// <summary>
        /// Vraag die gesteld wordt aan de gebruiker op de VerwijderActiviteit-pagina.
        /// </summary>
        private string vraag;

        /// <summary>
        /// Een eventuele opmerking op de VerwijderActiviteit-pagina.
        /// </summary>
        private string opmerking;

        #endregion

        #region constructor

        /// <summary>
        /// Hier wordt de VerwijderActiviteitmenu-instantie aangemaakt.
        /// </summary>
        /// <param name="lijstactmetbegeleider">De lijst 'lijstActMetBegeleider' met alle mogelijke activiteiten.</param>
        /// <param name="vraag">Vraag die gesteld wordt aan de gebruiker in het VerwijderActiviteit-keuzemenu.</param>
        /// <param name="opmerking">Een eventuele opmerking in het VerwijderActiviteit-keuzemenu.</param>
        public WerkingVerwijderActiviteitMenu( List<Tuple<Activiteiten, Begeleider>> lijstactmetbegeleider, string vraag, string opmerking )
        {
            lijstActMetBegeleider = lijstactmetbegeleider;
            this.vraag = vraag;
            this.opmerking = opmerking;
            keuzeIndex = 0;
        }

        #endregion

        #region methoden op instantie-niveau

        /// <summary>
        /// Deze methode activeert bepaalde toetsen van het toetsenbord
        /// die mogen worden gebruikt om een keuze te maken in het 
        /// VerwijderActiviteit-keuzemenu en deze te bevestigen.
        /// </summary>
        /// <returns>Geeft de keuze terug die de gebruiker maakt in het VerwijderActiviteit-keuzemenu.</returns>
        public int RunVerwijderActiviteit()
        {
            //  gedrukte toetsen omzetten in 'keuzes'
            ConsoleKey gedrukteToets;
            do
            {
                Console.Clear();
                LaatVerwijderActiviteitZien();

                ConsoleKeyInfo toetsInfo = Console.ReadKey( true );
                gedrukteToets = toetsInfo.Key;

                // keuzeIndex wordt gekozen adhv ↑ en ↓ : 'cyclische' werking!
                if ( gedrukteToets == ConsoleKey.UpArrow )
                {
                    keuzeIndex--;

                    // Als je hoger gaat, begin je terug vanonder
                    if ( keuzeIndex == -1 )
                    {
                        keuzeIndex = lijstActMetBegeleider.Count - 1;
                    }
                }
                else if ( gedrukteToets == ConsoleKey.DownArrow )
                {
                    keuzeIndex++;

                    // Als je lager gaat, begin je terug vanboven
                    if ( keuzeIndex == lijstActMetBegeleider.Count )
                    {
                        keuzeIndex = 0;
                    }
                }

            } while ( gedrukteToets != ConsoleKey.Enter );

            return keuzeIndex;
        }

        /// <summary>
        /// Methode die het uitzicht van het VerwijderActiviteit-keuzemenu 
        /// bepaalt: de exacte plaats van alle velden, eventueel 'extra' 
        /// styling, hoe de 'knoppen' qua uiterlijk moeten reageren en 
        /// wanneer en hoe deze worden weergegeven in de console.
        /// </summary>
        private void LaatVerwijderActiviteitZien()
        {
            Console.SetCursorPosition( 0, 8 );
            Styling.Center( vraag );
            Console.WriteLine();
            Styling.Center( opmerking );
            Console.WriteLine( "\n\n" );

            // bij elk menu-item verandert er iets
            for ( int i = 0; i < lijstActMetBegeleider.Count; i++ )
            {
                string naamAct = lijstActMetBegeleider[i].Item1.ToString();
                string datumAct = lijstActMetBegeleider[i].Item1.DagVanActiviteit;

                // Verschijnt voor het gekozen menu-item
                string prefix;

                if ( i == keuzeIndex )
                {
                    // zo maken we de 'keuze' extra zichtbaar
                    prefix = "Verwijder -->";
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    prefix = " ";
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Styling.Center( $"{prefix}  ###  {naamAct} op {datumAct}  ###\n" );
            }
            Console.ResetColor();
        }

        #endregion
    }
}
