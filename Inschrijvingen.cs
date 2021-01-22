using ConsoleTableExt;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace TeambuildingApp
{
    /// <summary>
    /// In deze klasse worden alle Inschrijvingen-instanties
    /// aangemaakt en staan alle methoden die functies uitvoeren 
    /// met Inschrijvingen-objecten.
    /// </summary>
    public class Inschrijvingen : IEqualityComparer<Tuple<Deelnemer, Activiteiten>>
    {
        #region  velden op klasse-niveau

        /// <summary>
        /// De lijst met alle 'Inschrijvingen'-instanties.
        /// </summary>
        public static List<Inschrijvingen> lijstInschrijvingen = new List<Inschrijvingen>();

        #endregion

        #region velden op instantie-niveau

        /// <summary>
        /// De inschrijving is een combinatie van een 'Deelnemer'-instantie
        /// en een 'Activiteiten'-instantie. (Tuple)
        /// </summary>
        public Tuple<Deelnemer, Activiteiten> Inschrijving { get; set; }

        #endregion

        #region constructor

        /// <summary>
        /// Deze constructor maakt een inschrijving aan door een tuple
        /// mee te geven die een combinatie is van een deelnemer en een 
        /// activiteit.
        /// </summary>
        /// <param name="inschrijving">Tuple die bestaat uit een 'Deelnemer'-instantie en een 'Activiteiten'-instantie.</param>
        public Inschrijvingen( Tuple<Deelnemer, Activiteiten> inschrijving )
        {
            Inschrijving = inschrijving;
        }

        #endregion

        #region methoden op instantie-niveau

        /// <summary>
        /// Methode geïmplementeerd via de IEqualityComparer interface.
        /// Gaat na of twee tuples (= inschrijvingen) gelijk zijn, dwz.
        /// dat dezelfde deelnemer gekoppeld is aan dezelfde activiteit.
        /// </summary>
        /// <param name="x">Inschrijving1</param>
        /// <param name="y">Inschrijving2</param>
        /// <returns>'True' geeft aan dat de 2 inschrijvingen gelijk zijn.</returns>
        public bool Equals( Tuple<Deelnemer, Activiteiten> x, Tuple<Deelnemer, Activiteiten> y )
        {
            if ( ReferenceEquals( x, y ) )
            {
                return true;
            }

            if ( ReferenceEquals( x, null ) || ReferenceEquals( y, null ) )
            {
                return false;
            }

            if ( x.Item1.Equals( y.Item2 ) && x.Item2.Equals( y.Item1 ) )
            {
                return true;
            }

            return x.Item1.Equals( y.Item1 ) && x.Item2.Equals( y.Item2 );
        }

        /// <summary>
        /// Methode geïmplementeerd via de IEqualityComparer interface.
        /// Geeft een HashCode terug van een inschrijving.
        /// </summary>
        /// <param name="inschrijving">Inschrijving-instantie meegeven.</param>
        /// <returns></returns>
        public int GetHashCode( [DisallowNull] Tuple<Deelnemer, Activiteiten> inschrijving )
        {
            return inschrijving.GetHashCode();
        }

        #endregion

        #region methoden op klasse-niveau

        /// <summary>
        /// Toont alle inschrijvingen in een op maat gemaakte tabel.
        /// Ik maak hierbij gebruik van het NuGet-package 'ConsoleTableExt' 
        /// door Hung Vo.
        /// </summary>
        public static void ToonTabelInschrijvingen()
        {
            ConsoleTableBuilder tableBuilder = ConsoleTableBuilder.From( InhoudDataTable2() );
            tableBuilder.ExportAndWriteLine();
        }

        private static DataTable InhoudDataTable2()
        {
            DataTable table = new DataTable();

            // Lijst inschrijvingen eerst sorteren op voornaam deelnemers.
            Inschrijvingen.lijstInschrijvingen.Sort( ( e1, e2 ) => e1.Inschrijving.Item1.Naam.CompareTo( e2.Inschrijving.Item1.Naam ) );

            // Kolommen invoeren
            table.Columns.Add( "Deelnemer (voornaam + naam)" );
            table.Columns.Add( "Naam activiteit" ); ;
            table.Columns.Add( "Datum activiteit" );

            // Daarna de rijen
            if ( lijstInschrijvingen.Count == 0 )
            {
                table.Rows.Add( "Nog geen inschrijvingen", "n.b.", "n.b." );
            }
            else
            {
                foreach ( Inschrijvingen item in lijstInschrijvingen )
                {
                    table.Rows.Add( item.Inschrijving.Item1.Naam, item.Inschrijving.Item2, $"{item.Inschrijving.Item2.DagVanActiviteit}" );
                }
            }

            return table;
        }

        /// <summary>
        /// Toont alle inschrijvingen voor een specifieke, ingegeven activiteit
        /// in een op maat gemaakte tabel. Ik maak hierbij gebruik van het 
        /// NuGet-package 'ConsoleTableExt' door Hung Vo.
        /// </summary>
        /// <param name="activiteit">Activiteit ingeven waarvan je de inschrijvingen wil zien.</param>
        public static void ToontabelInschrijvingenSpecifiekeActiviteit( string activiteit )
        {
            Console.WriteLine( $"Alle inschrijvingen voor '{activiteit}':" );

            // Kleinere lijst maken van enkel die inschrijvingen die gekoppeld zijn aan de gekozen activiteit.
            List<Inschrijvingen> inschrijvingen = lijstInschrijvingen.FindAll( e => e.Inschrijving.Item2.Naam.Equals( activiteit ) );

            // Deze keer heb ik eerst de inhoud van de tabel aangemaakt...
            DataTable InhoudDataTable4()
            {
                DataTable table = new DataTable();
                table.Columns.Add( "Deelnemer (voornaam + naam)" );
                table.Columns.Add( "Bedrijf deelnemer" );
                table.Columns.Add( "Datum Activiteit" );

                if ( inschrijvingen.Count == 0 )
                {
                    table.Rows.Add( "Nog geen inschrijvingen", "n.b.", "n.b." );
                }
                else
                {
                    foreach ( Inschrijvingen item in inschrijvingen )
                    {
                        table.Rows.Add( item.Inschrijving.Item1.Naam,
                                        $"werkt bij --> {item.Inschrijving.Item1.WerktHier()}",
                                        item.Inschrijving.Item2.DagVanActiviteit );
                    }
                }
                return table;
            }

            // ... en vervolgens gekoppeld aan een 'tablebuilder'.
            ConsoleTableBuilder tableBuilder = ConsoleTableBuilder.From( InhoudDataTable4() );
            tableBuilder.ExportAndWriteLine();

            Console.WriteLine();
        }

        /// <summary>
        /// Toont alle inschrijvingen, ingedeeld volgens activiteit,
        /// in een op maat gemaakte tabel. Ik maak hierbij gebruik van het 
        /// NuGet-package 'ConsoleTableExt' door Hung Vo.
        /// </summary>
        public static void ToontabelInschrijvingenPerActiviteit()
        {
            // Deellijst maken van inschrijvingen van een bepaalde activiteit
            List<string> reedsOpgesomd = new List<string>();

            // Aantal deellijsten is afhankelijk van het aantal verschillende activiteiten.
            for ( int i = 0; i < Activiteiten.lijstActMetBegeleider.Count; i++ )
            {
                if ( !reedsOpgesomd.Contains( Activiteiten.lijstActMetBegeleider[i].Item1.Naam ) )
                {
                    // Voorkomt dat sommige deellijsten meermaals worden weergegeven!
                    reedsOpgesomd.Add( Activiteiten.lijstActMetBegeleider[i].Item1.Naam );

                    Console.WriteLine( $"Alle inschrijvingen voor '{Activiteiten.lijstActMetBegeleider[i].Item1.Naam}':" );
                    List<Inschrijvingen> inschrijvingen = lijstInschrijvingen.FindAll( e => e.Inschrijving.Item2.Naam.Equals( Activiteiten.lijstActMetBegeleider[i].Item1.Naam ) );

                    ConsoleTableBuilder tableBuilder = ConsoleTableBuilder.From( InhoudDataTable3() );
                    tableBuilder.ExportAndWriteLine();

                    DataTable InhoudDataTable3()
                    {
                        DataTable table = new DataTable();
                        table.Columns.Add( "Deelnemer (voornaam + naam)" );
                        table.Columns.Add( "Bedrijf deelnemer" );
                        table.Columns.Add( "Datum Activiteit" );

                        if ( inschrijvingen.Count == 0 )
                        {
                            table.Rows.Add( "Nog geen inschrijvingen", "n.b.", "n.b." );
                        }
                        else
                        {
                            foreach ( Inschrijvingen item in inschrijvingen )
                            {
                                table.Rows.Add( item.Inschrijving.Item1.Naam,
                                                $"werkt bij --> {item.Inschrijving.Item1.WerktHier()}",
                                                item.Inschrijving.Item2.DagVanActiviteit );
                            }
                        }
                        return table;
                    }
                }
                Console.WriteLine();
            }
        }

        #endregion
    }
}
