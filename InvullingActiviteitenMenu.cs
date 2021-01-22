using System;

namespace TeambuildingApp
{
    /// <summary>
    /// In deze klasse krijgt het activiteiten-menu een concrete inhoud mee.
    /// Een 'InvullingMenu'-klasse gaat altijd samen met een bijhorende 
    /// 'WerkingMenu'-klasse.
    /// </summary>
    public class InvullingActiviteitenMenu
    {
        #region activiteitenmenu


        public static void StartActiviteitenMenu()  // deze public methode schermt de andere private methoden af.
        {
            ActiviteitenMenu();
        }

        /// <summary>
        /// Deze methode geeft de invulling weer van de activiteiten-menu-pagina.
        /// Dit is het eerste submenu van het centraal hoofdmenu.
        /// Van een gekleurde titel-banner tot en met de menukeuze met 4 opties.
        /// </summary>
        private static void ActiviteitenMenu()
        {
            Console.SetWindowSize( 90, 45 );

            string hoofdtitel = Styling.InKleur( Styling.ToAsciiArt( "Activiteitenmenu", "standard" ), "#00ffea" );
            string activiteitenMenuTitel = "'ActiveBonding' biedt een waaier aan van mogelijke teambuilding activiteiten!";
            string submenutitel = "Maak een keuze in het menu met de pijltjes-toetsen ↑ en ↓ en druk op 'ENTER'.";
            string[] hoofdmenuKeuzes = { "Alle activiteiten bekijken", "Aanmaken activiteit", "Verwijderen activiteit", "Terug naar hoofdmenu" };

            // Aanmaken 'WerkingMenu'-instantie (zie andere klasse)
            WerkingActiviteitenMenus subMenu1 = new WerkingActiviteitenMenus( activiteitenMenuTitel,
                                                                                submenutitel,
                                                                                    hoofdmenuKeuzes,
                                                                                        hoofdtitel );
            // Oproepen van de instantie-methode 'RunMenu'.
            int gekozenIndex = subMenu1.RunActiviteitenMenu();

            // Weergeven van de verschillende opties in het keuzemenu
            switch ( gekozenIndex )
            {
                case 0:
                    BekijkAlleActiviteiten();
                    break;
                case 1:
                    // Nieuwe activiteit aanmaken en inplannen
                    InvullingMaakActiviteitMenu.StartMaakActiviteitMenu();
                    break;
                case 2:
                    // Ingeplande activiteit verwijderen
                    InvullingVerwijderActiviteitMenu.StartVerwijderActiviteitMenu();
                    break;
                case 3:
                    // Naar hoofdmenu
                    InvullingHoofdmenu.StartHoofdmenu();
                    break;
            }
        }

        #endregion

        #region kalender activiteiten

        /// <summary>
        /// Deze methode maakt een pagina aan met een gekleurde
        /// titel-banner en een overzicht van alle activiteiten
        /// in een op maat gemaakte tabel.
        /// Er is een link terug naar het activiteiten-menu.
        /// </summary>
        private static void BekijkAlleActiviteiten()
        {
            Console.Clear();
            string hoofdtitel = Styling.InKleur( Styling.ToAsciiArt( "Onze activiteiten:", "standard" ), "#00ffea" );
            Console.WriteLine( hoofdtitel );
            Console.WriteLine();
            Styling.Center( "Deze chronologische kalender toont alle geplande activiteiten (volgende 3 maanden)." );
            Styling.Center( "U kan deze aanpassen in het activiteitenmenu." );
            Console.WriteLine();
            Activiteiten.ToonTabelActiviteiten();
            Console.WriteLine( "\n\n" );
            InvullingMaakActiviteitMenu.Afsluiting();
        }

        #endregion

    }
}
