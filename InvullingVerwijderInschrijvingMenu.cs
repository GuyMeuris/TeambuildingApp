using System;

namespace TeambuildingApp
{
    /// <summary>
    /// In deze klasse krijgt het 'VerwijderBestaandeInschrijving'-
    /// menu een concrete inhoud mee. Een 'InvullingMenu'-klasse 
    /// gaat altijd samen met een bijhorende 'WerkingMenu'-klasse.
    /// </summary>
    public class InvullingVerwijderInschrijvingMenu
    {
        #region 'VerwijderInschrijving'-methode

        public static void StartVerwijderInschrijvingMenu()  // deze public methode schermt de andere private methoden af
        {
            if ( Activiteiten.lijstActMetBegeleider.Count != 0 )
            {
                VerwijderInschrijving();
                InvullingMaakInschrijvingMenu.Afsluiting();
            }
            // Indien er geen inschrijvingen te verwijderen zijn
            else
            {
                Console.Clear();
                Styling.Center( "Er zijn geen inschrijvingen om te verwijderen." );
                InvullingMaakInschrijvingMenu.Afsluiting();
            }
        }

        /// <summary>
        /// Deze methode geeft de invulling weer van de 'VerwijderInschrijving'-pagina.
        /// Dit is een tweede echte toepassing (= menukeuze) in het inschrijvingsmenu.
        /// </summary>
        private static void VerwijderInschrijving()
        {
            string vraag = "Welke inschrijving wenst u te verwijderen uit de lijst?";
            string opmerking = "(Maak een keuze met de pijltjes-toetsen ↑ en ↓ en druk op 'ENTER'.)";

            // Aanmaken 'WerkingMenu'-instantie (zie andere klasse)
            WerkingVerwijderInschrijvingMenu verwijder = new WerkingVerwijderInschrijvingMenu( Inschrijvingen.lijstInschrijvingen, vraag, opmerking );

            // Oproepen van de instantie-methode 'RunMenu'.
            int gekozenIndex = verwijder.RunVerwijderInschrijving();

            // Aantal keuzemogelijkheden is afhankelijk van het aantal inschrijvingen
            // in de lijst 'lijstInschrijvingen'.
            for ( int i = 0; i < Inschrijvingen.lijstInschrijvingen.Count; i++ )
            {
                if ( gekozenIndex == i )
                {
                    Inschrijvingen.lijstInschrijvingen.RemoveAt( i );
                }
            }
            Console.WriteLine( "\n" );

            // Bevestiging van de verwijdering van de inschrijving aan de gebruiker
            // en tevens de verwijdering van de inschrijving uit de lijst
            // 'lijstInschrijvingen'.
            Styling.Center( $"De inschrijving werd uit de lijst verwijderd.", "#ff0000" );
        }

        #endregion
    }
}
