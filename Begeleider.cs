using ConsoleTableExt;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace TeambuildingApp
{
    /// <summary>
    /// Deze klasse erft van de Persoon-klasse. Hier worden ook 
    /// alle Begeleider-instanties in aangemaakt en staan alle
    /// methoden die functies uitvoeren met begeleider-objecten.
    /// </summary>
    public class Begeleider : Persoon
    {
        #region velden op klasse-niveau

        /// <summary>
        /// Lijst met alle 'Begeleider'-instanties 
        /// (= werknemers van 'ActiveBonding') die
        /// de teambuilding activiteiten organiseren.
        /// </summary>
        public static List<Begeleider> lijstBegeleider = new List<Begeleider>();

        /// <summary>
        /// Lijst van alle attesten die een begeleider kan hebben, uitgezonderd
        /// het attest 'rijbewijs'. Niet elke begeleider heeft er evenveel of dezelfde.
        /// </summary>
        public static List<string> alleAttesten = new List<string> { "duiken", "skydiven", "muurklimmen", "vliegen", "zeilen" };

        #endregion

        #region velden op instantie-niveau

        /// <summary>
        /// Lijst van attesten die deze begeleider heeft.
        /// </summary>
        public List<string> attesten = new List<string> { };

        #endregion

        #region constructor

        /// <summary>
        /// Deze methode (die erft van 'persoon-klasse) maakt begeleiders aan en wordt 
        /// enkel opgeroepen tijdens de app-introductievragen voor het officiëel begin 
        /// van de app. Tijdens de app kunnen er GEEN begeleiders worden aangemaakt in deze oefening.
        /// </summary>
        /// <param name="achternaam">Achternaam van de begeleider.</param>
        /// <param name="voornaam">Voornaam van de begeleider.</param>
        public Begeleider( string achternaam, string voornaam ) : base( achternaam, voornaam )
        {
            // Begeleiders zijn altijd werknemers van 'ActiveBonding' in deze oefening.
            werktHier.Naam = "ActiveBonding";

            if ( Bedrijf.lijstBedrijven.IndexOf( werktHier ) != 0 ) // 'ActiveBonding' eerst op de lijst.
            {
                Bedrijf.lijstBedrijven.Insert( 0, werktHier );
            }

            if ( !lijstBegeleider.Contains( this ) )   //   Alle begeleiders komen op deze lijst
            {
                lijstBegeleider.Add( this );
            }

            // Iedere begeleider krijgt een willekeurig aantal attesten.
            // Ook de keuze van de attesten zelf is volledig willekeurig.
            SetRandomAttesten();
        }

        #endregion

        #region methoden op instantie-niveau

        /// <summary>
        /// Methode die een willekeurig aantal attesten aanmaakt, maar
        /// er voor zorgt dat de meeste begeleiders zeker een rijbewijs 
        /// gaan hebben. (Deze zal altijd eerst worden weergegeven.)
        /// </summary>
        private void SetRandomAttesten()
        {
            int aantalAttesten = randomGetalI.Next( 1, 5 );
            int j = randomGetalI.Next( 5 );
            if ( j < 4 )
            {
                attesten.Insert( 0, "rijbewijs" );
            }
            for ( int i = 0; i < aantalAttesten; i++ )
            {
                int k = randomGetalI.Next( 5 );
                if ( !attesten.Contains( alleAttesten[k] ) )
                {
                    attesten.Add( alleAttesten[k] );
                }
            }
        }

        /// <summary>
        /// Deze methode geeft weer of een begeleider al of niet attesten
        /// heeft en zo ja, dewelke dat zijn.
        /// </summary>
        public void GetAttesten()
        {
            string str = attesten.Count switch
            {
                0 => $"{voornaam} {achternaam} heeft geen attesten. ",
                1 => $"{voornaam} {achternaam} heeft volgend attest: ",
                _ => $"{voornaam} {achternaam} heeft volgende attesten: "
            };
            Console.Write( str );

            if ( attesten.Count != 0 )
            {
                for ( int i = 0; i <= attesten.Count - 1; i++ )
                {
                    Console.Write( $"{(i == 0 ? " " : "")}{attesten[i]}{(i == attesten.Count - 2 ? " en " : "")}" +
                                    $"{ (i < attesten.Count - 2 ? ", " : "")}" );
                }
                Console.WriteLine( "." );
            }
        }

        /// <summary>
        /// Deze 'string overriding'-methode somt alle attesten op 
        /// die deze begeleider heeft.
        /// </summary>
        /// <returns></returns>
        private string AttestenInfo()
        {
            string lijstAttesten = string.Join( ",", attesten );
            return lijstAttesten;
        }

        #endregion

        #region methoden op klasse-niveau

        /// <summary>
        /// Deze methode maakt een aangegeven aantal begeleiders aan en
        /// haalt de voornamen en achternamen willekeurig uit een tekstbestand.
        /// </summary>
        /// <param name="aantalbegeleiders">Aantal begeleiders dat moet worden aangemaakt.</param>
        public static void MaakAantalBegeleiders( int aantalbegeleiders )
        {
            for ( int i = 0; i < aantalbegeleiders; i++ )
            {
                string personenData = File.ReadAllText( personenDataFilePath );
                string[] personenDataApart = personenData.Split( ',' );
                int naamNr = randomGetalK.Next( 0, (personenDataApart.Length - 1) / 3 );
                string voornaam = personenDataApart[3 * naamNr].Replace( Environment.NewLine, string.Empty );
                string achternaam = personenDataApart[(3 * naamNr) + 1].Replace( " ", string.Empty );
                Begeleider collegaActiveBonding = new Begeleider( achternaam, voornaam );
            }
        }

        /// <summary>
        /// Deze methode geeft alle begeleiders weer en hun 
        /// individuele attesten. (Eenvoudige vorm)
        /// </summary>
        public static void LijstBegeleiders()
        {
            Console.WriteLine( "\nTer info: alle beschikbare begeleiders en hun attesten." );
            for ( int i = 0; i <= lijstBegeleider.Count - 1; i++ )
            {
                lijstBegeleider[i].GetAttesten();
            }
        }

        /// <summary>
        /// Deze methode geeft alle begeleiders weer en hun individuele 
        /// attesten in een op maat gemaakte tabel.
        /// Ik maak hierbij gebruik van het NuGet-package 'ConsoleTableExt' 
        /// door Hung Vo.
        /// </summary>
        public static void ToonFirmaCollegas()
        {
            ConsoleTableBuilder tableBuilder = ConsoleTableBuilder.From( InhoudDataTable() ).WithFormat( ConsoleTableBuilderFormat.Alternative );
            tableBuilder.ExportAndWriteLine();
        }
        private static DataTable InhoudDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add( "Naam" );
            table.Columns.Add( "Functie" );
            table.Columns.Add( "Leeftijd" );
            table.Columns.Add( "Attesten en brevetten" );

            for ( int i = 0; i < lijstBegeleider.Count; i++ )
            {
                if ( i == 0 )
                {
                    // De eerste begeleider zal de zaakvoerder zijn van het bedrijf
                    table.Rows.Add( lijstBegeleider[i].Naam, "Zaakvoerder, bezieler", lijstBegeleider[i].Leeftijd + " jaar",
                    lijstBegeleider[i].AttestenInfo() );
                }
                else if ( i == 1 )
                {
                    // De tweede begeleider krijgt de functie van marketing manager
                    table.Rows.Add( lijstBegeleider[i].Naam, "Marketing Manager", lijstBegeleider[i].Leeftijd + " jaar",
                    lijstBegeleider[i].AttestenInfo() );
                }
                else
                {
                    // Alle andere begeleiders krijgen de functie van teamleader
                    table.Rows.Add( lijstBegeleider[i].Naam, "Teamleader/Activity Coach", lijstBegeleider[i].Leeftijd + " jaar",
                    lijstBegeleider[i].AttestenInfo() );
                }
            }
            return table;
        }

        #endregion
    }
}
