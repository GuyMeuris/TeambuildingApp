using System;
using System.Globalization;
using System.Threading;

namespace TeambuildingApp
{
    /// <summary>
    /// In deze klasse wordt de werking (= blauwdruk) van het 
    /// MaakNieuweActiviteit-menu gecodeerd. Een 'WerkingMenu'-klasse 
    /// gaat altijd samen met een bijhorende 'InvullingMenu'-klasse.
    /// </summary>
    public class WerkingMaakActiviteitMenu
    {
        #region velden op instantie-niveau

        /// <summary>
        /// De velden die de gebruiker moet invullen in het MaakNieuweActiviteit-menu.
        /// </summary>
        private string[] velden;

        /// <summary>
        /// De hoofding in het MaakNieuweActiviteit-menu.
        /// </summary>
        private string menuHoofding;

        /// <summary>
        /// De subhoofding in het MaakNieuweActiviteit-menu.
        /// </summary>
        private string menuSubHoofding;

        /// <summary>
        /// De hoofdtitel van het MaakNieuweActiviteit-menu.
        /// </summary>
        private string hoofdtitel;

        #endregion

        #region constructor

        /// <summary>
        /// Hier wordt de MaakNieuweActiviteitmenu-instantie aangemaakt.
        /// </summary>
        /// <param name="menuhoofding">De hoofding in het MaakNieuweActiviteit-menu.</param>
        /// <param name="menusubhoofding">De subhoofding in het MaakNieuweActiviteit-menu.</param>
        /// <param name="velden">De velden die de gebruiker moet invullen in het MaakNieuweActiviteit-menu.</param>
        /// <param name="hoofdtitel">De hoofdtitel van het MaakNieuweActiviteit-menu.</param>
        public WerkingMaakActiviteitMenu( string menuhoofding, string menusubhoofding, string[] velden, string hoofdtitel )
        {
            this.velden = velden;
            menuHoofding = menuhoofding;
            menuSubHoofding = menusubhoofding;
            this.hoofdtitel = hoofdtitel;
        }

        #endregion

        #region methoden op instantie-niveau

        /// <summary>
        /// Deze methode doorloopt alle invul-velden stapsgewijs en slaat
        /// de input op als een 'string'. Er wordt tevens aan de nodige 
        /// foutenafhandeling gedaan. Alle strings worden zorgvuldig verwerkt
        /// om uiteindelijk een nieuwe activiteit mét begeleider te kunnen aanmaken.
        /// Deze activiteit wordt ook automatisch toegevoegd aan de 'lijstActiviteiten'
        /// en aan de lijst 'lijstActMetBegeleider'.
        /// </summary>
        public void RunMaakActiviteit()
        {
            Console.Clear();
            LaatMaakActiviteitTabelZien();

            #region locale variabelen

            string naamActiviteit;
            string plaatsActiviteit;
            string prijsAct;
            double prijsActiviteit = 0;
            DateTime start = DateTime.Now;
            DateTime dagEvent = new DateTime();
            string datumActiviteit;
            string ingegevenNaam;
            bool nieuweActIsAangemaakt = false;

            #endregion

            // Eerst de naam van de activiteit inlezen en opslaan
            // Deze moet gekozen worden uit de lijst van mogelijke activiteiten
            do
            {
                naamActiviteit = TextCapturingAtPosition( 50, 14 );
                if ( string.IsNullOrEmpty( naamActiviteit ) || naamActiviteit.Trim().Length == 0 )
                {
                    string foutmelding = "Naam activiteit is een verplicht veld.";
                    DisplayFoutVermelding( foutmelding, 50, 14 );
                }
                else if ( Activiteiten.mogelijkeActiviteiten.ContainsKey( naamActiviteit ) )
                {
                    break;
                }
                else
                {
                    string foutmelding = "Kies 1 van de activiteiten uit de lijst onderaan!";
                    DisplayFoutVermelding( foutmelding, 50, 14 );
                }
            } while ( !Activiteiten.mogelijkeActiviteiten.ContainsKey( naamActiviteit ) );

            // 'Indoor' of 'outdoor' wordt automatisch na 1 sec. ingevuld adh. van de activiteit
            bool indoorOutdoor = Activiteiten.mogelijkeActiviteiten[naamActiviteit];
            if ( indoorOutdoor )
            {
                Console.CursorVisible = false;
                Thread.Sleep( 1000 );
                Styling.WriteAt( "indoor", 50, 16 );
            }
            else
            {
                Console.CursorVisible = false;
                Thread.Sleep( 1000 );
                Styling.WriteAt( "outdoor", 50, 16 );
            }

            // Prijs van de activiteit moet tussen 2 specifieke bedragen liggen.
            do
            {
                prijsAct = TextCapturingAtPosition( 50, 18 );
                if ( !double.TryParse( prijsAct.Replace( " ", string.Empty ), result: out prijsActiviteit ) )
                {
                    string foutmelding = "U heeft geen geldig bedrag ingegeven. Denk aan de komma!";
                    DisplayFoutVermelding( foutmelding, 50, 18 );
                }
                else
                {
                    if ( prijsActiviteit < 25 )
                    {
                        string foutmelding = "Het bedrag is extreem laag!!! Denk aan de winstmarge!";
                        DisplayFoutVermelding( foutmelding, 50, 18 );
                    }
                    if ( prijsActiviteit > 1500 )
                    {
                        string foutmelding = "Het bedrag is extreem hoog!!! Dit is niet realistisch!";
                        DisplayFoutVermelding( foutmelding, 50, 18 );
                    }
                }
                prijsActiviteit = Math.Round( prijsActiviteit, 2 );
            } while ( !((prijsActiviteit > 25) && (prijsActiviteit < 1500)) );

            // Datum van de activiteit MOET binnen de 90 dagen worden ingepland.
            do
            {
                string format = "dd-MM-yyyy";
                datumActiviteit = TextCapturingAtPosition( 50, 20 );
                if ( !DateTime.TryParseExact( datumActiviteit.Replace( " ", string.Empty ),
                            format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dagEvent ) )
                {
                    string foutmelding = "U heeft geen geldige datum ingegeven. Juiste schrijfwijze!";
                    DisplayFoutVermelding( foutmelding, 50, 20 );
                }
                else
                {
                    if ( dagEvent < start )
                    {
                        string foutmelding = "Datum is in het verleden. Dit is niet realistisch!";
                        DisplayFoutVermelding( foutmelding, 50, 20 );
                    }
                    if ( dagEvent > start.AddDays( 90 ) )
                    {
                        string foutmelding = "Activiteit moet binnen de 3 maanden worden ingepland.";
                        DisplayFoutVermelding( foutmelding, 50, 20 );
                    }
                }
            } while ( !((dagEvent > start) && (dagEvent < start.AddDays( 90 ))) );

            // Een deel van het consolescherm weer 'zwart' maken om nieuwe tekst
            // op deze plaats te laten verschijnen.
            Console.SetCursorPosition( 0, 29 );
            for ( int i = 0; i < 21; i++ )
            {
                string legeZin = new string( ' ', 97 );
                Console.WriteLine( legeZin );
            }
            Console.SetCursorPosition( 0, 29 );

            // Toon lijst van mogelijke begeleiders bij de gekozen activiteit
            Begeleider.LijstBegeleiders();

            // Begeleider aanduiden uit de vertoonde lijst, met het juiste attest
            // indien nodig.
            do
            {
                ingegevenNaam = TextCapturingAtPosition( 50, 22 );
                Begeleider begeleider;
                if ( Begeleider.lijstBegeleider.Exists( e => e.Naam == ingegevenNaam ) )
                {
                    begeleider = Begeleider.lijstBegeleider.Find( e => e.Naam == ingegevenNaam );

                    if ( (naamActiviteit.Contains( "racen" )) && (!begeleider.attesten.Contains( "rijbewijs" )) )
                    {
                        string foutmelding = "U kan enkel een persoon aanduiden met het juiste attest!";
                        DisplayFoutVermelding( foutmelding, 50, 22 );
                    }
                    else if ( (Begeleider.alleAttesten.Exists( a => a == naamActiviteit ) && (!begeleider.attesten.Contains( naamActiviteit ))) )
                    {
                        string foutmelding = "U kan enkel een persoon aanduiden met het juiste attest!";
                        DisplayFoutVermelding( foutmelding, 50, 22 );
                    }
                    else
                    {
                        Activiteiten nieuweActiviteit = new( naamActiviteit, prijsActiviteit, indoorOutdoor, datumActiviteit, begeleider );
                        nieuweActIsAangemaakt = true;
                    }
                }
                else
                {
                    string foutmelding = "U kan enkel een persoon aanduiden uit de lijst!";
                    DisplayFoutVermelding( foutmelding, 50, 22 );
                }
            } while ( !nieuweActIsAangemaakt );
        }

        /// <summary>
        /// Methode die de tabel van de invulvelden bepaalt:
        /// de exacte plaats van alle velden, eventueel 'extra' 
        /// styling, een extra kader voor 'foutvermelding' en 
        /// hoe de lijst 'mogelijkeActiviteiten' wordt 
        /// weergegeven in de console.
        /// </summary>
        private void LaatMaakActiviteitTabelZien()
        {
            Console.WriteLine( hoofdtitel );
            Styling.Center( menuHoofding );
            Console.WriteLine( "\n\n" );
            Styling.Center( menuSubHoofding );
            System.Console.WriteLine( "\n" );
            int regelnr = 13;
            Styling.WriteAt( ("").PadRight( 75, '*' ), 10, regelnr );
            for ( int i = 0; i < velden.Length; i++ )
            {
                Styling.WriteAt( ($"*  {velden[i],-35}{" ",36}*"), 10, ++regelnr );
                Styling.WriteAt( ("").PadRight( 75, '*' ), 10, ++regelnr );
            }

            Styling.WriteAt( ("").PadRight( 75, '*' ), 10, 25 );
            Styling.WriteAt( ($"*  {"Foutmelding: ",-15}{" ",56}*"), 10, 26 );
            Styling.WriteAt( ("").PadRight( 75, '*' ), 10, 27 );
            Console.WriteLine();
            Activiteiten.ToonMogelijkeActiviteiten();
        }

        /// <summary>
        /// Deze methode gaat vanaf een bepaalde positie in de console
        /// input van de gebruiker opslaan. Aangezien de input in een 
        /// tabel staat, moet dit karakter per karakter gebeuren.
        /// Alle karakters worden vervolgens verenigd tot een string.
        /// </summary>
        /// <param name="positie">Beginpositie van de inputlocatie.</param>
        /// <param name="regelnr">De regelnummer van de inputlocatie.</param>
        /// <returns>Geeft de string terug die de gebruiker heeft ingegeven.</returns>
        private string TextCapturingAtPosition( int positie, int regelnr )
        {
            Console.SetCursorPosition( positie, regelnr );
            ConsoleKeyInfo gedrukteToets;
            Console.CursorVisible = true;
            string stringInput = string.Empty;
            do
            {
                gedrukteToets = Console.ReadKey();

                //backspace toets laten werken, bij foute ingave!
                if ( (gedrukteToets.Key == ConsoleKey.Backspace) && (stringInput.Length > 0) )
                {
                    stringInput = stringInput.Substring( 0, stringInput.Length - 1 );
                }
                else
                {
                    //elk getypt karakter toevoegen aan de string
                    stringInput += gedrukteToets.KeyChar;
                }
            } while ( gedrukteToets.Key != ConsoleKey.Enter );
            stringInput = stringInput.Substring( 0, stringInput.Length - 1 ).Trim();
            return stringInput;
        }

        #endregion

        #region methoden op klasse-niveau

        /// <summary>
        /// Deze methode geeft een ingegeven foutmelding 
        /// en een vermelding om de input opnieuw in te 
        /// geven op een meegegeven locatie bij foute ingave.
        /// </summary>
        /// <param name="foutmelding">De juiste reden wat er fout was aan de input.</param>
        /// <param name="positie">Beginpositie van de inputlocatie.</param>
        /// <param name="regelnr">De regelnummer van de inputlocatie.</param>
        private static void DisplayFoutVermelding( string foutmelding, int positie, int regelnr )
        {
            Console.CursorVisible = false;
            Styling.WriteAt( Styling.InKleur( foutmelding, "#ff6200" ), 26, 26 );
            Thread.Sleep( 3000 );
            string legeZin = new string( ' ', 58 );
            Styling.WriteAt( legeZin, 26, 26 );
            Styling.WriteAt( Styling.InKleur( "Probeer opnieuw in te geven.", "#ff6200" ), 26, 26 );
            Thread.Sleep( 3000 );
            Styling.WriteAt( legeZin, 26, 26 );
            Console.SetCursorPosition( positie, regelnr );
            string legeZin2 = new string( ' ', 33 );
            Styling.WriteAt( legeZin2, positie, regelnr );
            Console.SetCursorPosition( positie, regelnr );
            Console.CursorVisible = true;
        }

        #endregion
    }
}
