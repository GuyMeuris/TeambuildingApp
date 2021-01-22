using System;

namespace TeambuildingApp
{
    /// <summary>
    /// In deze klasse krijgt het 'VerwijderBestaandeActiviteit'-menu 
    /// een concrete inhoud mee. Een 'InvullingMenu'-klasse gaat 
    /// altijd samen met een bijhorende 'WerkingMenu'-klasse.
    /// </summary>
    public class InvullingVerwijderActiviteitMenu
    {
        #region 'VerwijderActiviteit'-methoden

        public static void StartVerwijderActiviteitMenu()  // deze public methode schermt de andere private methoden af.
        {
            if ( Activiteiten.lijstActMetBegeleider.Count != 0 )
            {
                VerwijderActiviteit();
                InvullingMaakActiviteitMenu.Afsluiting();
            }
            // Indien er geen activiteiten te verwijderen zijn
            else
            {
                Console.Clear();
                Styling.Center( "Er zijn geen activiteiten om te verwijderen." );
                InvullingMaakActiviteitMenu.Afsluiting();
            }
        }

        /// <summary>
        /// Deze methode geeft de invulling weer van de 'VerwijderActiviteit'-pagina.
        /// Dit is een tweede echte toepassing (= menukeuze) in het activiteitenmenu.
        /// </summary>
        private static void VerwijderActiviteit()
        {
            string vraag = "Welke activiteit wenst u te verwijderen uit de lijst?";
            string opmerking = "(Maak een keuze met de pijltjes-toetsen ↑ en ↓ en druk op 'ENTER'.)";

            // Aanmaken 'WerkingMenu'-instantie (zie andere klasse)
            WerkingVerwijderActiviteitMenu verwijder = new WerkingVerwijderActiviteitMenu( Activiteiten.lijstActMetBegeleider, vraag, opmerking );

            // Oproepen van de instantie-methode 'RunMenu'.
            int gekozenIndex = verwijder.RunVerwijderActiviteit();

            // Aantal keuzemogelijkheden is afhankelijk van het aantal activiteiten
            // in de lijst 'ActiviteitenMetBegeleider', want dit zijn de 'ingeplande' 
            // activiteiten.
            for ( int i = 0; i < Activiteiten.lijstActMetBegeleider.Count; i++ )
            {
                if ( gekozenIndex == i )
                {
                    Activiteiten.lijstActMetBegeleider.RemoveAt( i );
                }
            }

            // Bevestiging van de verwijdering van de activiteit aan de gebruiker
            // en tevens de verwijdering van de activiteit uit de lijst
            // 'ActiviteitenMetBegeleider'.
            Styling.Center( $"De activiteit werd uit de lijst verwijderd." );

        }

        #endregion
    }
}
