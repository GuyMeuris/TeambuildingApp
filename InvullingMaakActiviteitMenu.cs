using System;

namespace TeambuildingApp
{
    /// <summary>
    /// In deze klasse krijgt het 'MaakEenNieuweActiviteitAan'-menu 
    /// een concrete inhoud mee. Een 'InvullingMenu'-klasse gaat 
    /// altijd samen met een bijhorende 'WerkingMenu'-klasse.
    /// </summary>
    public class InvullingMaakActiviteitMenu
    {
        #region 'maakActiviteit'-methoden

        public static void StartMaakActiviteitMenu() // deze public methode schermt de andere private methoden af.
        {
            StartMaakActiviteit();
            Afsluiting();
        }

        /// <summary>
        /// Deze methode geeft de invulling weer van de 'MaakNieuweActiviteit'-pagina.
        /// Van een gekleurde titel-banner tot en met een tabel met 5 invulvelden
        /// waarvan de antwoorden worden opgeslagen.
        /// Dit is een eerste echte toepassing (= menukeuze) in het activiteitenmenu.
        /// </summary>
        private static void StartMaakActiviteit()
        {
            Console.SetWindowSize( 100, 45 );

            string hoofdtitel = Styling.InKleur( Styling.ToAsciiArt( "Nieuwe activiteit", "standard" ), "#00ffea" );
            string activiteitenMenuTitel = "Maak hieronder een nieuwe teambuilding activiteit aan:";
            string submenutitel = "Vul de lege velden in en druk telkens daarna op 'ENTER'. De velden zijn verplicht.";
            string[] velden = { "Naam activiteit: ", "'Indoor' of 'outdoor': ", "Prijs activiteit (0,00): ",
                                    "Datum activiteit (dd-mm-jjjj): ", "Naam Begeleider (vrnaam famnaam): " };

            // Aanmaken 'WerkingMenu'-instantie (zie andere klasse)
            WerkingMaakActiviteitMenu activiteit =
                new WerkingMaakActiviteitMenu( activiteitenMenuTitel, submenutitel, velden, hoofdtitel );

            // Oproepen van de instantie-methode 'RunMenu'.
            activiteit.RunMaakActiviteit();

            // Een deel van het consolescherm weer 'zwart' maken om nieuwe tekst
            // op deze plaats te laten verschijnen.
            Console.CursorVisible = false;
            Console.SetCursorPosition( 0, 29 );
            for ( int i = 0; i < 15; i++ )
            {
                string legeZin = new string( ' ', 97 );
                Console.WriteLine( legeZin );
            }

            // Bevestiging van de antwoorden van de gebruiker en 
            // het aanmaken van een nieuwe activiteit, die eveneens
            // onmiddellijk wordt toegevoegd aan de activiteitenlijst 
            // én aan de 'ActiviteitMetBegeleider'-lijst.
            Console.SetCursorPosition( 0, 29 );
            Styling.Center( $"De activiteit werd aangemaakt." );
            Styling.Center( "U kan deze nu bekijken in het submenu 'activiteiten' van de app." );
            Console.WriteLine();
        }

        #endregion

        #region afsluiting

        /// <summary>
        /// Deze methode sluit de pagina af en keert terug naar het activiteitenmenu.
        /// </summary>
        public static void Afsluiting()
        {
            Styling.Center( "Druk op een toets om terug te gaan naar het activiteitenmenu." );
            Console.ReadKey( true );
            InvullingActiviteitenMenu.StartActiviteitenMenu();
        }

        #endregion


    }
}
