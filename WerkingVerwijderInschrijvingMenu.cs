using System;
using System.Collections.Generic;

namespace TeambuildingApp
{
    /// <summary>
    /// In deze klasse wordt de werking (= blauwdruk) van de 
    /// VerwijderInschrijving-menu gecodeerd. Een 'WerkingMenu'-klasse 
    /// gaat altijd samen met een bijhorende 'InvullingMenu'-klasse.
    /// </summary>
    public class WerkingVerwijderInschrijvingMenu
    {
        #region velden op instantie-niveau

        /// <summary>
        /// Is de keuze die de gebruiker maakt in het VerwijderInschrijving-keuzemenu.
        /// </summary>
        private int keuzeIndex;

        /// <summary>
        /// De lijst 'lijstInschrijvingen' met alle geregistreerde inschrijvingen.
        /// </summary>
        private List<Inschrijvingen> lijstInschrijvingen;

        /// <summary>
        /// Vraag die gesteld wordt aan de gebruiker op de VerwijderInschrijving-pagina.
        /// </summary>
        private string vraag;

        /// <summary>
        /// Een eventuele opmerking op de VerwijderInschrijving-pagina.
        /// </summary>
        private string opmerking;

        #endregion

        #region constructor

        /// <summary>
        /// Hier wordt de VerwijderInschrijvingmenu-instantie aangemaakt.
        /// </summary>
        /// <param name="lijstinschrijvingen">De lijst 'lijstInschrijvingen' met alle geregistreerde inschrijvingen.</param>
        /// <param name="vraag">Vraag die gesteld wordt aan de gebruiker op de VerwijderInschrijving-pagina.</param>
        /// <param name="opmerking">Een eventuele opmerking op de VerwijderInschrijving-pagina.</param>
        public WerkingVerwijderInschrijvingMenu( List<Inschrijvingen> lijstinschrijvingen, string vraag, string opmerking )
        {
            lijstInschrijvingen = lijstinschrijvingen;
            this.vraag = vraag;
            this.opmerking = opmerking;
            keuzeIndex = 0;
        }

        #endregion

        #region methoden op instantie-niveau

        /// <summary>
        /// Deze methode activeert bepaalde toetsen van het toetsenbord
        /// die mogen worden gebruikt om een keuze te maken in het 
        /// VerwijderInschrijving-keuzemenu en deze te bevestigen.
        /// </summary>
        /// <returns>Geeft de keuze terug die de gebruiker maakt in het VerwijderInschrijving-keuzemenu.</returns>
        public int RunVerwijderInschrijving()
        {
            //  gedrukte toetsen omzetten in 'keuzes'
            ConsoleKey gedrukteToets;
            do
            {
                Console.Clear();
                LaatVerwijderInschrijvingZien();

                ConsoleKeyInfo toetsInfo = Console.ReadKey( true );
                gedrukteToets = toetsInfo.Key;

                // keuzeIndex wordt gekozen adhv ↑ en ↓ : 'cyclische' werking!
                if ( gedrukteToets == ConsoleKey.UpArrow )
                {
                    keuzeIndex--;

                    // Als je hoger gaat, begin je terug vanonder
                    if ( keuzeIndex == -1 )
                    {
                        keuzeIndex = lijstInschrijvingen.Count - 1;
                    }
                }
                else if ( gedrukteToets == ConsoleKey.DownArrow )
                {
                    keuzeIndex++;

                    // Als je lager gaat, begin je terug vanboven
                    if ( keuzeIndex == lijstInschrijvingen.Count )
                    {
                        keuzeIndex = 0;
                    }
                }

            } while ( gedrukteToets != ConsoleKey.Enter );

            return keuzeIndex;
        }

        /// <summary>
        /// Methode die het uitzicht van het VerwijderInschrijving-keuzemenu 
        /// bepaalt: de exacte plaats van alle velden, eventueel 'extra' 
        /// styling, hoe de 'knoppen' qua uiterlijk moeten reageren en 
        /// wanneer en hoe deze worden weergegeven in de console.
        /// </summary>
        private void LaatVerwijderInschrijvingZien()
        {
            Console.SetCursorPosition( 0, 8 );
            Styling.Center( vraag );
            Console.WriteLine();
            Styling.Center( opmerking );
            Console.WriteLine( "\n\n" );

            // bij elk menu-item verandert er iets
            for ( int i = 0; i < lijstInschrijvingen.Count; i++ )
            {
                string naamDeelnemer = lijstInschrijvingen[i].Inschrijving.Item1.Naam;
                string naamAct = lijstInschrijvingen[i].Inschrijving.Item2.Naam;
                string datumAct = lijstInschrijvingen[i].Inschrijving.Item2.DagVanActiviteit;

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
                Styling.Center( $"{prefix}  ### {naamDeelnemer} -->  {naamAct} op {datumAct}  ###\n" );
            }
            Console.ResetColor();
        }

        #endregion
    }
}
