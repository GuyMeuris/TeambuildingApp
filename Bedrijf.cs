using Pastel;
using System;
using System.Collections.Generic;

namespace TeambuildingApp
{
    /// <summary>
    /// In deze 'struct' worden alle Bedrijf-instanties aangemaakt
    /// en staan alle methoden die functies uitvoeren met 
    /// bedrijf-objecten.
    /// </summary>
    public struct Bedrijf
    {
        #region velden op klasse-niveau
        /// <summary>
        /// Lijst van alle bedrijven met 'ActiveBonding' zelf altijd op plaats 1.
        /// </summary>
        public static List<Bedrijf> lijstBedrijven = new List<Bedrijf>();

        #endregion

        #region properties

        /// <summary>
        /// Naam van het bedrijf.
        /// </summary>
        public string Naam { get; set; }

        #endregion

        #region methoden op klasse-niveau

        /// <summary>
        /// Deze methode start de introductie van 'ActiveBonding' op.
        /// Er is een grote, gekleurde titel-banner en ook alle 
        /// werknemers worden in een tabel weergegeven.
        /// Na deze methode volgt het hoofdmenu.
        /// </summary>
        public static void StartActiveBondingIntro()
        {
            Console.SetWindowSize( 105, 40 );
            Console.Clear();

            Console.WriteLine();

            // De styling die ik hier heb toegepast is terug te vinden in de 'styling'-klasse
            Console.WriteLine( Styling.InKleur( Styling.ToAsciiArt( "activeBonding", "speed" ), "#00ffea" ) );

            Console.WriteLine( $"Ons bedrijf 'ActiveBonding' verzorgt teambuilding-evenementen voor bedrijven.\n".Pastel( "#00D8E1" ) );

            Styling.Center( "Wie is wie binnen ons bedrijf?\n" );

            // Tonen van alle werknemers van 'ActiveBonding'
            Begeleider.ToonFirmaCollegas();

            Console.WriteLine();

            Styling.Center( "Wij zullen u met plezier begeleiden bij 1 van onze teambuilding activiteiten!" );

            Console.WriteLine();

            Styling.Center( "Druk op een toets om verder te gaan" );
            Console.CursorVisible = false;
            Console.ReadKey( true );
        }

        /// <summary>
        /// Deze methode geeft de 'ActiveBonding'-bedrijfsinformatiepagina weer.
        /// Er is een grote, gekleurde titel-banner, alle werknemers worden 
        /// opnieuw in een tabel weergegeven en er zijn meer details over de firma.
        /// Er is een link terug naar het hoofdmenu.
        /// </summary>
        public static void IntroductieActiveBonding()
        {
            Console.SetWindowSize( 105, 45 );
            Console.Clear();

            string str = Styling.ToAsciiArt( Bedrijf.lijstBedrijven[0].Naam, "speed" );
            Console.WriteLine( Styling.InKleur( str, "#00ffea" ) );
            str = $"Ons bedrijf '{Bedrijf.lijstBedrijven[0]}' verzorgt teambuilding-evenementen voor bedrijven.\n";
            Console.WriteLine( Styling.InKleur( str, "#00D8E1" ) );
            Console.WriteLine( "\n\n" );

            Styling.Center( "Wie is ActiveBonding?" );
            Styling.Center( "---------------------" );
            Begeleider.ToonFirmaCollegas();
            Console.WriteLine();
            Styling.Center( $"{Bedrijf.lijstBedrijven[0]} is fictief opgericht in 2020 vanwege een dringende " +
                                                                $"nood aan applicatie-inhoud." );
            Styling.Center( "Ons bedrijf bestaat uit digitaal gefantaseerde medewerkers, de ene nog gekker dan de andere!" );
            Styling.Center( "U gaat hier onder andere lezen dat wij zeer actieve mensen zijn. Helaas, niets is minder waar!" );
            Styling.Center( "Net zoals onze 'fantastische' activiteiten, zijn ook onze identiteiten compleet nep." );
            Styling.Center( "Onze karakters dienen enkel als 'kannonenvoer' voor de functie van deze 'un-real-life' app." );
            Styling.Center( "Een app die intern is ontwikkeld door 1 van onze beste IT'ers ooit: 'Hij-wiens-naam-niet-wordt-getypt'! " );
            Styling.Center( "Wij hopen u bij deze alvast voldoende te hebben geïnformeerd en wensen u nog een fijn 2021..." );
            Console.WriteLine();
            Styling.Center( "... of in ons geval een fijn '11111100101'! " );
            Console.WriteLine();
            Styling.Center( "Druk op een toets om terug naar het hoofdmenu te gaan." );
            Console.ReadKey( true );
            InvullingHoofdmenu.StartHoofdmenu();
        }

        #endregion
    }
}
