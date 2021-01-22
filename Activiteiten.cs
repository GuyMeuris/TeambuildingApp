using ConsoleTableExt;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace TeambuildingApp
{
    /// <summary>
    /// Klasse van de teambuildingsactiviteiten.
    /// </summary>
    public class Activiteiten : IComparable    // Interface om de juiste 'vgl'-methode te kunnen implementeren
    {
        #region velden op klasse-niveau

        public static List<Activiteiten> lijstActiviteiten = new List<Activiteiten>();

        /// <summary>
        /// Lijst van alle activiteiten die gekoppeld zijn aan een begeleider (in een tuple).
        /// </summary>
        public static List<Tuple<Activiteiten, Begeleider>> lijstActMetBegeleider = new List<Tuple<Activiteiten, Begeleider>>();

        /// <summary>
        /// Lijst van alle mogelijke activiteiten waaruit kan worden gekozen.
        /// 'True' = indoor, 'false' = outdoor.
        /// </summary>
        public static Dictionary<string, bool> mogelijkeActiviteiten = new Dictionary<string, bool>
        {
            { "Ferrari_racen", false },
            { "duiken",false },
            { "skydiven", false},
            { "muurklimmen", true },
            { "vliegen", false},
            { "zeilen", false },
            { "moto_racen", false},
            { "nachtzwemmen", false },
            { "oriëntatiewandelen", false},
            { "tennistornooi", false },
            { "paintball", false},
            { "rondvaart Schelde", false },
            { "BakeOff wedstrijd", true},
            { "Escape Room", true},
            { "real life Sudoku puzzelgame", true },
            { "museumbezoek", true},
            { "stadsverkenning", false },
            { "trampolinepark", true},
            { "luchtkastelenparadijs", false }
        };

        #endregion

        #region velden op instantie-niveau

        /// <summary>
        /// Willekeurig getal aanmaken.
        /// </summary>
        private Random randomGetalI = new Random();

        /// <summary>
        /// Prijs van de activiteit.
        /// </summary>
        private double prijs;

        /// <summary>
        /// Plaats van de activiteit:
        /// 'True' = indoor, 'false' = outdoor.
        /// </summary>
        private bool indoorActiviteit;

        /// <summary>
        /// Deze begeleider (= altijd iemand van 'ActiveBonding met 
        /// het juiste attest indien noodzakelijk!) leidt de activiteit.
        /// </summary>
        private Begeleider begeleidtActiviteit;

        #endregion

        #region properties

        /// <summary>
        /// Naam van de activiteit.
        /// </summary>
        public string Naam { get; }

        /// <summary>
        /// Datum waarop de activiteit wordt georganiseerd.
        /// </summary>
        public string DagVanActiviteit { get; set; }

        #endregion

        #region constructors

        /// <summary>
        /// Deze lege constructor is nodig om de activiteit te kunnen linken aan inschrijvingen.
        /// </summary>
        public Activiteiten() { }

        /// <summary>
        /// Deze constructor wordt gebruikt voor de automatisch gegenereerde activiteit-instanties.
        /// </summary>
        /// <param name="naam">Naam van de activiteit.</param>
        /// <param name="prijs">Prijs van de activiteit.</param>
        /// <param name="indoorActiviteit">'True' = indoor, 'false' = outdoor activiteit.</param>
        public Activiteiten( string naam, double prijs, bool indoorActiviteit )
        {
            Naam = naam.ToLower();
            this.prijs = Math.Round( prijs, 2 );
            this.indoorActiviteit = indoorActiviteit;

            DagVanActiviteit = RandomDag();

            if ( lijstActiviteiten.BinarySearch( this ) == -1 )
            {
                lijstActiviteiten.Add( this );
                lijstActiviteiten.Sort();
            }

            begeleidtActiviteit = HeeftRandomBegeleider();
            Tuple<Activiteiten, Begeleider> activiteitCombi = new Tuple<Activiteiten, Begeleider>( this, begeleidtActiviteit );
            if ( !lijstActMetBegeleider.Exists( e => e.Item1.Equals( activiteitCombi.Item1 ) && e.Item2.Equals( activiteitCombi.Item2 ) ) )
            {
                lijstActMetBegeleider.Add( activiteitCombi );
                lijstActMetBegeleider = lijstActMetBegeleider.OrderBy( e => e.Item1 ).ToList();
            }
        }

        /// <summary>
        /// Deze constructor wordt gebruikt voor de activiteiten-instanties die worden aangemaakt
        /// via het activiteiten-menu.
        /// </summary>
        /// /// <param name="naam">Naam van de activiteit.</param>
        /// <param name="prijs">Prijs van de activiteit.</param>
        /// <param name="indoorActiviteit">'True' = indoor, 'false' = outdoor activiteit.</param>
        /// <param name="datumActiviteit">Datum wanneer de activiteit wordt georganiseerd</param>
        /// <param name="begeleider">Begeleider die de activiteit leidt.</param>
        public Activiteiten( string naam, double prijs, bool indoorActiviteit, string datumActiviteit, Begeleider begeleider )
        {
            Naam = naam.ToLower();
            this.prijs = Math.Round( prijs, 2 );
            this.indoorActiviteit = indoorActiviteit;
            DagVanActiviteit = datumActiviteit;

            if ( lijstActiviteiten.BinarySearch( this ) == -1 )
            {
                lijstActiviteiten.Add( this );
                lijstActiviteiten.Sort();
            }

            begeleidtActiviteit = begeleider;
            Tuple<Activiteiten, Begeleider> activiteitCombi = new Tuple<Activiteiten, Begeleider>( this, begeleidtActiviteit );
            if ( !lijstActMetBegeleider.Exists( e => e.Item1.Equals( activiteitCombi.Item1 ) && e.Item2.Equals( activiteitCombi.Item2 ) ) )
            {
                lijstActMetBegeleider.Add( activiteitCombi );
                lijstActMetBegeleider = lijstActMetBegeleider.OrderBy( e => e.Item1 ).ToList();
            }
        }

        #endregion

        #region methoden instantie-niveau

        /// <summary>
        /// 'String overriding' om makkelijker de waarde te kunnen weergeven.
        /// </summary>
        /// <returns>Geeft de naam van de activiteit terug.</returns>
        public override string ToString()
        {
            return Naam;
        }

        /// <summary>
        /// 'Equals overriding' om 2 activiteiten-instanties met elkaar te kunnen vergelijken
        /// om te controleren of ze dezelfde activiteit (op dezelfde dag) zijn.
        /// </summary>
        /// <param name="x"></param>
        /// <returns>'True' geeft aan dat de 2 activiteiten dezelfde zijn.</returns>
        public override bool Equals( object x )
        {
            Activiteiten a = (Activiteiten)x;

            if ( (this.Naam == a.Naam) && (this.DagVanActiviteit == a.DagVanActiviteit) )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 'ICompare interface'-methode die helpt om activiteiten-instanties alfabetisch
        /// en vervolgens chronologisch te sorteren. 
        /// </summary>
        /// <param name="act"></param>
        /// <returns>'-1' = komt eerst, '0' = gelijk, '1' = komt daarna in de rij.</returns>
        public int CompareTo( object act )
        {
            Activiteiten a = (Activiteiten)act;
            if ( this.DagVanActiviteit == a.DagVanActiviteit )
            {
                return String.Compare( this.Naam, a.Naam ); // alfabetisch sorteren met string-methode
            }
            else  // eerst sorteren op jaartal, ...
            {
                if ( Int32.Parse( this.DagVanActiviteit[^4..] ) > Int32.Parse( a.DagVanActiviteit[^4..] ) )
                {
                    return 1;
                }

                if ( Int32.Parse( this.DagVanActiviteit[^4..] ) < Int32.Parse( a.DagVanActiviteit[^4..] ) )
                {
                    return -1;
                }
                else      //  ...vervolgens op maand, ...  
                {
                    if ( Int32.Parse( this.DagVanActiviteit[3..5] ) > Int32.Parse( a.DagVanActiviteit[3..5] ) )
                    {
                        return 1;
                    }

                    if ( Int32.Parse( this.DagVanActiviteit[3..5] ) < Int32.Parse( a.DagVanActiviteit[3..5] ) )
                    {
                        return -1;
                    }
                    else     //  ...daarna op dag.
                    {
                        if ( Int32.Parse( this.DagVanActiviteit[..2] ) > Int32.Parse( a.DagVanActiviteit[..2] ) )
                        {
                            return 1;
                        }

                        if ( Int32.Parse( this.DagVanActiviteit[..2] ) < Int32.Parse( a.DagVanActiviteit[..2] ) )
                        {
                            return -1;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Geeft in een volledig woord ipv een bool-waarde aan of 
        /// de activiteit binnen of buiten is.
        /// </summary>
        /// <returns></returns>
        private string IndoorOutdoor()
        {
            if ( indoorActiviteit )
            {
                return "indoor";
            }
            else
            {
                return "outdoor";
            }
        }

        /// <summary>
        /// Deze methode zorgt voor een willekeurig gekozen begeleider (mét juiste attest
        /// indien noodzakelijk!) bij de automatisch gegenereerde activiteiten.
        /// </summary>
        /// <returns>Geeft de juiste begeleider terug.</returns>
        private Begeleider HeeftRandomBegeleider()
        {

            Begeleider begeleider;
            List<Begeleider> resultaat = new List<Begeleider>();
            if ( Begeleider.alleAttesten.Exists( e => e == this.Naam ) ) // als er een attest nodig is
            {
                // Alle begeleiders met het gevraagde attest in nieuwe lijst steken
                resultaat = Begeleider.lijstBegeleider.FindAll( e => e.attesten.Contains( this.Naam ) );
                if ( resultaat.Count == 0 )
                {
                    int j = randomGetalI.Next( Begeleider.lijstBegeleider.Count );
                    begeleider = Begeleider.lijstBegeleider[j];
                }
                else
                {
                    int j = randomGetalI.Next( resultaat.Count - 1 );
                    begeleider = resultaat[j];
                }
            }
            else  // als er een rijbewijs nodig is. Deze naam van het attest is uitzonderlijk NIET gelijk
                  // aan de naam van de activiteit. Dan lossen we het zo op. 
            {
                int index = Naam.IndexOf( "_" );
                if ( (index != -1) && (Naam.Substring( index + 1 ) == "racen") )
                {
                    resultaat = Begeleider.lijstBegeleider.FindAll( e => e.attesten.Contains( "rijbewijs" ) );
                    int j = randomGetalI.Next( resultaat.Count );
                    begeleider = resultaat[j];
                }
                else   // als er geen attest nodig is...
                {
                    int j = randomGetalI.Next( Begeleider.lijstBegeleider.Count );
                    begeleider = Begeleider.lijstBegeleider[j];
                }
            }

            return begeleider;
        }

        /// <summary>
        /// Deze methode zorgt voor een willekeurig gekozen datum 
        /// bij de automatisch gegenereerde activiteiten.
        /// </summary>
        /// <returns>Geeft een datum terug tussen de 10 en 90 dagen vanaf nu.</returns>
        private string RandomDag()
        {
            DateTime start = DateTime.Now;
            DateTime dagEvent = new DateTime();
            dagEvent = start.AddDays( randomGetalI.Next( 10, 91 ) );
            DagVanActiviteit = dagEvent.ToString( "dd-MM-yyyy" );
            return DagVanActiviteit;
        }

        #endregion

        #region methoden op klasse-niveau

        /// <summary>
        /// Deze methode genereert automatisch het meegegeven aantal activiteiten.
        /// </summary>
        /// <param name="aantalactiviteiten"></param>
        public static void MaakAantalActiviteiten( int aantalactiviteiten )
        {
            for ( int i = 0; i < 4; i++ )  // eerste 4 activiteiten vereisen een 'attest' als begeleider.
            {
                List<string> keyLijst = new List<string>( mogelijkeActiviteiten.Keys.Where
                    ( e => e == "Ferrari-racen" || e == "Duiken" || e == "vliegen" || e == "muurklimmen" || e == "skydiven" || e == "zeilen" ) );
                string _naam = keyLijst[Persoon.randomGetalK.Next( keyLijst.Count )];
                double _prijs = Math.Round( (Math.Round( (Persoon.randomGetalK.NextDouble() * 150 + 60), 1 ) + 0.05), 2 );
                bool _indoorOutdoor = mogelijkeActiviteiten.GetValueOrDefault( _naam );
                Activiteiten maakRandomActiviteit = new Activiteiten( _naam, _prijs, _indoorOutdoor );
            }

            for ( int i = 4; i < aantalactiviteiten; i++ )  // de volgende zijn totaal willekeurig.
            {
                List<string> keyLijst = new List<string>( mogelijkeActiviteiten.Keys );
                string _naam = keyLijst[Persoon.randomGetalK.Next( keyLijst.Count )];
                double _prijs = Math.Round( (Math.Round( (Persoon.randomGetalK.NextDouble() * 150 + 60), 1 ) + 0.05), 2 );
                bool _indoorOutdoor = mogelijkeActiviteiten.GetValueOrDefault( _naam );
                Activiteiten maakRandomActiviteit = new Activiteiten( _naam, _prijs, _indoorOutdoor );
            }

        }

        /// <summary>
        /// Deze methode toont alle mogelijke teambuilding activiteiten van ActiveBonding,
        /// met vermelding van 'indoor' of 'outdoor' als ze binnen of buiten zijn.
        /// </summary>
        public static void ToonMogelijkeActiviteiten()
        {
            Styling.Center( "Alle mogelijke teambuilding activiteiten van ActiveBonding: \n" );
            foreach ( KeyValuePair<string, bool> e in mogelijkeActiviteiten.OrderBy( e => e.Key ) )
            {
                if ( e.Value == true )
                {
                    Console.WriteLine( $"{" ",20}{e.Key,-30} --> indooractiviteit" );
                }
                else
                {
                    Console.WriteLine( $"{" ",20}{e.Key,-30} --> outdooractiviteit" );
                }
            }
        }

        /// <summary>
        /// Deze methode toont in een automatisch gegenereerde tabel alle activiteiten
        /// die zijn gepland én gekoppeld aan een begeleider.
        /// Ik maak hierbij gebruik van het NuGet-package 'ConsoleTableExt' door Hung Vo.
        /// </summary>
        public static void ToonTabelActiviteiten()
        {
            // Bouwen van de tabel ahv. de inhoud 'InhoudDataTable()'
            ConsoleTableBuilder tableBuilder = ConsoleTableBuilder.From( InhoudDataTable() );
            tableBuilder.ExportAndWriteLine();
        }

        /// <summary>
        /// Hier wordt de inhoud van de tabel aangemaakt: eerst de kolommen,
        /// vervolgens de rijen.
        /// </summary>
        /// <returns>Er wordt aangemaakte 'tabel-inhoud' voor 1 tabel teruggegeven.</returns>
        private static DataTable InhoudDataTable()
        {
            DataTable table = new DataTable();
            // Eerst de kolommen aanmaken
            table.Columns.Add( "Naam" );
            table.Columns.Add( "Indoor/Outdoor" );
            table.Columns.Add( "Prijs" );
            table.Columns.Add( "Datum" );
            table.Columns.Add( "Begeleider" );

            // Vervolgens de rijen
            if ( lijstActMetBegeleider.Count == 0 )
            {
                table.Rows.Add( "Nog geen activiteit ingegeven", "n.b.", "n.b.", "n.b.", "n.b." );
            }
            else
            {
                foreach ( Tuple<Activiteiten, Begeleider> e in lijstActMetBegeleider )
                {
                    table.Rows.Add( e.Item1.Naam, e.Item1.IndoorOutdoor(), e.Item1.prijs, e.Item1.DagVanActiviteit, e.Item2.Naam );
                }
            }
            return table;
        }

        #endregion
    }
}
