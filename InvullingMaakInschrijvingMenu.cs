using System;

namespace TeambuildingApp
{
    /// <summary>
    /// In deze klasse krijgt het 'MaakEenNieuweInschrijvingAan'-menu 
    /// een concrete inhoud mee. Een 'InvullingMenu'-klasse gaat 
    /// altijd samen met een bijhorende 'WerkingMenu'-klasse.
    /// </summary>
    public class InvullingMaakInschrijvingMenu
    {
        #region 'maakActiviteit'-methoden

        public static void StartMaakInschrijvingMenu() // deze public methode schermt de andere private methoden af.
        {
            StartMaakInschrijving();
            Afsluiting();
        }

        /// <summary>
        /// Deze methode geeft de invulling weer van de 'MaakNieuweInschrijving'-pagina.
        /// Van een gekleurde titel-banner tot en met een tabel met 5 invulvelden
        /// waarvan de antwoorden worden opgeslagen.
        /// Dit is een eerste echte toepassing (= menukeuze) in het inschrijvingsmenu.
        /// </summary>
        private static void StartMaakInschrijving()
        {
            Console.SetWindowSize( 100, 45 );

            string hoofdtitel = Styling.InKleur( Styling.ToAsciiArt( "Nieuwe Inschrijving", "standard" ), "#00ffea" );
            string activiteitenMenuTitel = "Maak hieronder een nieuwe teambuilding inschrijving aan:";
            string submenutitel = "Vul de lege velden in en druk telkens daarna op 'ENTER'. De velden zijn verplicht.";
            string[] velden = { "Achternaam deelnemer: ", "Voornaam deelnemer: ", "Bedrijfsnaam deelnemer: ",
                                    "Naam activiteit: ", "Datum activiteit (dd-mm-jjjj): " };

            // Aanmaken 'WerkingMenu'-instantie (zie andere klasse)
            WerkingMaakInschrijvingMenu inschrijving =
                new WerkingMaakInschrijvingMenu( activiteitenMenuTitel, submenutitel, velden, hoofdtitel );

            // Oproepen van de instantie-methode 'RunMenu'.
            inschrijving.RunMaakInschrijving();

            // Een deel van het consolescherm weer 'zwart' maken om nieuwe tekst
            // op deze plaats te laten verschijnen.
            Console.CursorVisible = false;
            Console.SetCursorPosition( 0, 29 );
            for ( int i = 0; i < 25; i++ )
            {
                string legeZin = new string( ' ', 97 );
                Console.WriteLine( legeZin );
            }

            // Bevestiging van de antwoorden van de gebruiker en 
            // het aanmaken van een nieuwe inschrijving, die eveneens
            // onmiddellijk wordt toegevoegd aan de inschrijvingslijst.
            Console.SetCursorPosition( 0, 29 );
            Styling.Center( $"De inschrijving werd aangemaakt." );
            Styling.Center( "U kan deze nu bekijken in het submenu 'inschrijvingen' van de app." );
            Console.WriteLine();
        }

        #endregion

        #region afsluiting

        /// <summary>
        /// Deze methode sluit de pagina af en keert terug naar het inschrijvingsmenu.
        /// </summary>
        public static void Afsluiting()
        {
            Styling.Center( "Druk op een toets om terug te gaan naar het inschrijvingsmenu." );
            Console.ReadKey( true );
            InvullingInschrijvingenMenu.StartInschrijvingenMenu();
        }

        #endregion
    }
}
