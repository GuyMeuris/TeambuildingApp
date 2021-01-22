
using System;

namespace TeambuildingApp
{
    /* Deze app werd gemaakt door Guy Meuris in januari 2021.
     * Het is een OOP oefening in het kader van mijn Syntra opleiding 'Software Developer'. 
     * De precieze opdracht is terug te vinden in het 'OpdrachtTeambuilding.txt'-bestand.
     * De 'TeambuildingApp' bestaat uit twee delen: eerst 3 introductievragen en vervolgens
     * de oficiële 'ActiveBonding teambuilding applicatie'.
     * De 3 vragen tijdens de introfase dienen om enkele dummy-gegevens in te voeren, zodat
     * de teambuilding applicatie reeds voorzien is van een startaantal begeleiders,
     * deelnemers, activiteiten en inschrijvingen. De gebruiker mag zelf kiezen tussen de
     * voorgestelde aantallen.
    */

    /// <summary>
    /// In deze hoofdklasse wordt er enkel verwezen naar 
    /// naar methodes uit andere klassen en staat er zo weinig
    /// mogelijk 'extra' code.
    /// </summary>
    internal class Program
    {
        private static void Main()
        {
            #region console-instellingen

            Type myType = typeof( Program );          // Namespace = console.titel, afhankelijk van de klasse waar het zich bevindt
            Console.Title = myType.Namespace;

            Console.SetWindowSize( 95, 35 );  // Console-grootte aanpassen

            Console.OutputEncoding = System.Text.Encoding.Unicode;  // Deze regel code zorgt voor een correcte weergave van het €-teken

            #endregion

            #region ApplicatieIntro

            InvullingIntroVragen.Intro();

            #endregion

            #region BedrijfsIntro

            Bedrijf.StartActiveBondingIntro();

            #endregion

            #region Hoofdmenu + submenu's

            Console.Clear();
            InvullingHoofdmenu.StartHoofdmenu();

            #endregion
        }
    }
}
