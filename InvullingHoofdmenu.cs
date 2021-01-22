using System;

namespace TeambuildingApp
{
    /// <summary>
    /// In deze klasse krijgt het activiteiten-menu een concrete inhoud mee.
    /// Een 'InvullingMenu'-klasse gaat altijd samen met een bijhorende 
    /// 'WerkingMenu'-klasse.
    /// </summary>
    public class InvullingHoofdmenu
    {
        #region Hoofdmenu

        public static void StartHoofdmenu()  // deze public methode schermt de andere private methoden af.
        {
            Hoofdmenu();
        }

        /// <summary>
        /// Deze methode geeft de invulling weer van de Teambuilding hoofdmenu-pagina.
        /// Dit is de meest centrale pagina van de hele applicatie!
        /// Van een gekleurde titel-banner tot en met de menukeuze met 4 opties.
        /// </summary>
        private static void Hoofdmenu()
        {
            Console.SetWindowSize( 90, 35 );

            string logo1 = Styling.InKleur( Styling.ToAsciiArt( "Onze teambuilding", "standard" ), "#00ffea" );
            string logo2 = Styling.InKleur( Styling.ToAsciiArt( "  activiteiten...", "standard" ), "#00ffea" );

            string hoofdmenuTitel = "Welkom in het hoofdmenu van deze app!";
            string submenutitel = "Maak een keuze met de pijltjes-toetsen ↑ en ↓ en druk op 'ENTER'.";
            string[] hoofdmenuKeuzes = { "Overzicht teambuilding activiteiten", "Overzicht inschrijvingen",
                                          "Wie is ActiveBonding?", "Deze app verlaten" };

            // Aanmaken 'WerkingMenu'-instantie (zie andere klasse)
            WerkingHoofdmenu hoofdMenu = new WerkingHoofdmenu( hoofdmenuTitel, submenutitel, hoofdmenuKeuzes, logo1, logo2 );

            // Oproepen van de instantie-methode 'RunMenu'.
            int gekozenIndex = hoofdMenu.RunHoofdmenu();

            // Weergeven van de verschillende opties in het keuzemenu
            switch ( gekozenIndex )
            {
                case 0:
                    // ga naar het activiteitenmenu
                    InvullingActiviteitenMenu.StartActiviteitenMenu();
                    break;
                case 1:
                    // ga naar het inschrijvingenmenu
                    InvullingInschrijvingenMenu.StartInschrijvingenMenu();
                    break;
                case 2:
                    // ga naar de bedrijfsinformatie-pagina
                    Bedrijf.IntroductieActiveBonding();
                    break;
                case 3:
                    // verlaat de applicatie
                    ExitApp();
                    break;
            }
        }

        #endregion

        #region applicatie verlaten

        /// <summary>
        /// Deze methode sluit de applicatie af.
        /// </summary>
        private static void ExitApp()
        {
            Console.Clear();
            Console.WriteLine( "\n\n\n" );
            Styling.Center( "Wij hopen u graag snel terug te zien op onze app!" );
            Console.WriteLine( "\n\n\n" );
            Styling.Center( "Druk op een toets om de applicatie af te sluiten..." );
            Console.ReadKey( true );
            Environment.Exit( 0 );   // Het cijfer geeft de foutmelding aan bij afsluiten => 0 = geen fout.
        }

        #endregion

    }
}
