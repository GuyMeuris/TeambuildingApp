using System;

namespace TeambuildingApp
{
    /// <summary>
    /// In deze klasse krijgt het 'ZoekInschrijvingenOpActiviteit'
    /// -menu een concrete inhoud mee. Een 'InvullingMenu'-klasse 
    /// gaat altijd samen met een bijhorende 'WerkingMenu'-klasse.
    /// </summary>

    public class InvullingZoekOpActiviteitMenu
    {
        #region 'ZoekOpActiviteit'-methoden

        public static void StartZoekOpActiviteitMenu() // deze public methode schermt de andere private methoden af.
        {
            StartZoekOpActiviteit();
            InvullingMaakInschrijvingMenu.Afsluiting();
        }

        /// <summary>
        /// Deze methode geeft de invulling weer van de 'ZoekOpActiviteit'-pagina.
        /// Van een gekleurde titel-banner tot en met een tabel met 1 invulveld
        /// waarvan het antwoord wordt opgeslagen.
        /// Dit is een derde echte toepassing (= menukeuze) in het inschrijvingsmenu.
        /// </summary>
        private static void StartZoekOpActiviteit()
        {
            Console.SetWindowSize( 100, 45 );

            string hoofdtitel = Styling.InKleur( Styling.ToAsciiArt( "Zoek activiteit", "standard" ), "#00ffea" );
            string activiteitenMenuTitel = "Vul een activiteit in uit de lijst waarvan je de inschrijvingen wil zien:";
            string submenutitel = "Vul het lege veld in en druk daarna op 'ENTER'.";
            string veld = "Naam activiteit";

            // Aanmaken 'WerkingMenu'-instantie (zie andere klasse)
            WerkingZoekOpActiviteitMenu activiteit =
                new WerkingZoekOpActiviteitMenu( activiteitenMenuTitel, submenutitel, veld, hoofdtitel );

            // Oproepen van de instantie-methode 'RunMenu'.
            string resultaat = activiteit.RunZoekOpActiviteit();

            // Een deel van het consolescherm weer 'zwart' maken om nieuwe tekst
            // op deze plaats te laten verschijnen.
            Console.CursorVisible = false;
            Console.SetCursorPosition( 0, 21 );
            for ( int i = 0; i < 25; i++ )
            {
                string legeZin = new string( ' ', 97 );
                Console.WriteLine( legeZin );
            }
            Console.SetCursorPosition( 0, 21 );

            // Tonen van alle inschrijvingen (ook als er geen zijn) 
            // voor de ingegeven activiteit in een op maat gemaakte tabel.
            Inschrijvingen.ToontabelInschrijvingenSpecifiekeActiviteit( resultaat );
        }

        #endregion
    }
}
