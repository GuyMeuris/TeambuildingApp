using System;

namespace TeambuildingApp
{
    /// <summary>
    /// In deze klasse krijgt het inschrijvingen-menu een concrete inhoud mee.
    /// Een 'InvullingMenu'-klasse gaat altijd samen met een bijhorende 
    /// 'WerkingMenu'-klasse.
    /// </summary>
    public class InvullingInschrijvingenMenu
    {
        #region Inschrijvingsmenu

        public static void StartInschrijvingenMenu() // deze public methode schermt de andere private methoden af.
        {
            InschrijvingenMenu();
        }

        /// <summary>
        /// Deze methode geeft de invulling weer van de inschrijvingsmenu-pagina.
        /// Dit is het tweede submenu van het centraal hoofdmenu.
        /// Van een gekleurde titel-banner tot en met de menukeuze met 6 opties.
        /// </summary>
        private static void InschrijvingenMenu()
        {
            Console.SetWindowSize( 100, 45 );

            string hoofdtitel = Styling.InKleur( Styling.ToAsciiArt( "Inschrijvingsmenu", "standard" ), "#00ffea" );
            string activiteitenMenuTitel = "U kan hier alle aanpassingen doen ivm de inschrijvingen.";
            string submenutitel = "Maak een keuze met de pijltjes-toetsen ↑ en ↓ en druk op 'ENTER'.";
            string[] hoofdmenuKeuzes = { "Alle inschrijvingen bekijken",
                                            "Inschrijving aanmaken",
                                                "Inschrijving verwijderen",
                                                    "Bekijk alle inschrijvingen per activiteit",
                                                        "Zoek op naam activiteit",
                                                            "Terug naar hoofdmenu" };

            // Aanmaken 'WerkingMenu'-instantie (zie andere klasse)
            WerkingInschrijvingenMenu subMenu2 = new WerkingInschrijvingenMenu( activiteitenMenuTitel,
                                                                                    submenutitel,
                                                                                        hoofdmenuKeuzes,
                                                                                            hoofdtitel );
            // Oproepen van de instantie-methode 'RunMenu'.
            int gekozenIndex = subMenu2.RunInschrijvingenMenu();

            // Weergeven van de verschillende opties in het keuzemenu
            switch ( gekozenIndex )
            {
                case 0:
                    BekijkAlleInschrijvingen();
                    break;
                case 1:
                    // Nieuwe inschrijving aanmaken en registreren
                    InvullingMaakInschrijvingMenu.StartMaakInschrijvingMenu();
                    break;
                case 2:
                    // Geregistreerde inschrijving verwijderen
                    InvullingVerwijderInschrijvingMenu.StartVerwijderInschrijvingMenu();
                    break;
                case 3:
                    // Alle inschrijvingen bekijken, gesorteerd per ingeplande activiteit
                    BekijkAlleInschrijvingenPerActiviteit();
                    break;
                case 4:
                    // Alle inschrijvingen bekijken van een specifieke activiteit
                    InvullingZoekOpActiviteitMenu.StartZoekOpActiviteitMenu();
                    break;
                case 5:
                    // Naar hoofdmenu
                    InvullingHoofdmenu.StartHoofdmenu();
                    break;
            }
        }

        #endregion

        #region 'Bekijk inschrijvingen'-methoden

        /// <summary>
        /// Deze methode maakt een pagina aan met een gekleurde
        /// titel-banner en een overzicht van alle inschrijvingen
        /// in een op maat gemaakte tabel.
        /// Er is een link terug naar het inschrijvingsmenu.
        /// </summary>
        private static void BekijkAlleInschrijvingen()
        {
            Console.Clear();
            string hoofdtitel = Styling.InKleur( Styling.ToAsciiArt( "Alle inschrijvingen:", "standard" ), "#00ffea" );
            Console.WriteLine( hoofdtitel );
            Console.WriteLine();
            Styling.Center( "Dit overzicht toont (alfabetisch) alle personen die zich hebben ingeschreven." );
            Styling.Center( "U kan deze aanpassen in het inschrijvingsmenu." );
            Console.WriteLine();
            Inschrijvingen.ToonTabelInschrijvingen();
            Console.WriteLine( "\n\n" );
            // Afsluiten en teruggaan naar het inschrijvingsmenu
            Afsluiting();
        }

        /// <summary>
        /// Deze methode maakt een pagina aan met een gekleurde
        /// titel-banner en een overzicht van alle inschrijvingen,
        /// per activiteit gesorteerd, in een op maat gemaakte tabel.
        /// Er is een link terug naar het inschrijvingsmenu.
        /// </summary>
        private static void BekijkAlleInschrijvingenPerActiviteit()
        {
            Console.Clear();
            Console.SetWindowSize( 85, 45 );
            string hoofdtitel = Styling.InKleur( Styling.ToAsciiArt( "Inschrijvingen", "standard" ), "#00ffea" );
            Console.WriteLine( hoofdtitel );
            string hoofdtitel2 = Styling.InKleur( Styling.ToAsciiArt( "per activiteit:", "standard" ), "#00ffea" );
            Console.WriteLine( hoofdtitel2 );
            Console.WriteLine();
            Styling.Center( "Dit overzicht toont alle ingeschreven personen per activiteit." );
            Styling.Center( "U kan de inschrijvingen aanpassen in het inschrijvingsmenu." );
            Console.WriteLine();
            Inschrijvingen.ToontabelInschrijvingenPerActiviteit();
            Console.WriteLine( "\n\n" );
            // Afsluiten en teruggaan naar het inschrijvingsmenu
            Afsluiting();
        }

        #endregion

        #region Afsluiting

        /// <summary>
        /// Deze methode sluit het overzicht af en keert terug naar het inschrijvingsmenu.
        /// </summary>
        private static void Afsluiting()
        {
            Styling.Center( "Druk op een toets om terug te gaan naar het inschrijvingsmenu." );
            Console.ReadKey( true );
            StartInschrijvingenMenu();
        }

        #endregion

    }
}
