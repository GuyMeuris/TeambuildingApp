using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;

namespace TeambuildingApp
{
    /// <summary>
    /// In deze klasse wordt de werking (= blauwdruk) van het 
    /// MaakNieuweInschrijving-menu gecodeerd. Een 'WerkingMenu'-klasse 
    /// gaat altijd samen met een bijhorende 'InvullingMenu'-klasse.
    /// </summary>
    public class WerkingMaakInschrijvingMenu
    {
        #region velden op instantie-niveau

        /// <summary>
        /// De velden die de gebruiker moet invullen in het MaakNieuweInschrijving-menu.
        /// </summary>
        private string[] velden;

        /// <summary>
        /// De hoofding in het MaakNieuweInschrijving-menu.
        /// </summary>
        private string menuHoofding;

        /// <summary>
        /// De subhoofding in het MaakNieuweInschrijving-menu.
        /// </summary>
        private string menuSubHoofding;

        /// <summary>
        /// De hoofdtitel van het MaakNieuweInschrijving-menu.
        /// </summary>
        private string hoofdtitel;

        #endregion

        #region constructor

        /// <summary>
        /// Hier wordt de MaakNieuweInschrijvingmenu-instantie aangemaakt.
        /// </summary>
        /// <param name="menuhoofding">De hoofding in het MaakNieuweInschrijving-menu.</param>
        /// <param name="menusubhoofding">De subhoofding in het MaakNieuweInschrijving-menu.</param>
        /// <param name="velden">De velden die de gebruiker moet invullen in het MaakNieuweInschrijving-menu.</param>
        /// <param name="hoofdtitel">De hoofdtitel van het MaakNieuweInschrijving-menu.</param>
        public WerkingMaakInschrijvingMenu( string menuhoofding, string menusubhoofding, string[] velden, string hoofdtitel )
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
        /// om uiteindelijk een nieuwe inschrijving te kunnen aanmaken.
        /// Deze inschrijving wordt ook automatisch toegevoegd aan de 'lijstInschrijvingen'.
        /// </summary>
        public void RunMaakInschrijving()
        {

            Console.Clear();
            LaatMaakInschrijvingTabelZien();

            #region locale variabelen

            string achternaamDeelnemer = String.Empty;
            string voornaamDeelnemer = String.Empty;
            string bedrijfDeelnemer = String.Empty;
            string activiteit;
            string dagActiviteit;
            DateTime dagEvent = new DateTime();
            Activiteiten ingegevenActiviteit = new Activiteiten();

            #endregion

            // De achternaam van de deelnemer die zich wenst in te schrijven, 
            // inlezen en opslaan. 
            do
            {
                string achternaam = TextCapturingAtPosition( 50, 14 );
                if ( string.IsNullOrEmpty( achternaam ) || achternaam.Trim().Length == 0 )
                {
                    string foutmelding = "Achternaam is een verplicht veld.";
                    DisplayFoutVermelding( foutmelding, 50, 14 );
                }
                else if ( !Regex.IsMatch( achternaam, @"^[a-zA-Z_\s]+$" ) )
                {
                    string foutmelding = "Achternaam kan enkel letters/spaties bevatten.";
                    DisplayFoutVermelding( foutmelding, 50, 14 );
                }
                else
                {
                    achternaamDeelnemer = achternaam;
                    break;
                }
            } while ( achternaamDeelnemer.Length == 0 );

            // De voornaam van de deelnemer die zich wenst in te schrijven, 
            // inlezen en opslaan. 
            do
            {
                string voornaam = TextCapturingAtPosition( 50, 16 );
                if ( string.IsNullOrEmpty( voornaam ) || voornaam.Trim().Length == 0 )
                {
                    string foutmelding = "Voornaam is een verplicht veld.";
                    DisplayFoutVermelding( foutmelding, 50, 16 );
                }
                else if ( !Regex.IsMatch( voornaam, @"^[a-zA-Z\s]+$" ) )
                {
                    string foutmelding = "Voornaam kan enkel letters/spaties bevatten.";
                    DisplayFoutVermelding( foutmelding, 50, 16 );
                }
                else
                {
                    voornaamDeelnemer = voornaam;
                    break;
                }
            } while ( voornaamDeelnemer.Length == 0 );

            // Het bedrijf, waar de deelnemer werkt die zich wenst in te schrijven, 
            // inlezen en opslaan. 
            do
            {
                string bedrijf = TextCapturingAtPosition( 50, 18 );
                if ( string.IsNullOrEmpty( bedrijf ) || bedrijf.Trim().Length == 0 )
                {
                    string foutmelding = "Bedrijf is een verplicht veld.";
                    DisplayFoutVermelding( foutmelding, 50, 18 );
                }
                else
                {
                    bedrijfDeelnemer = bedrijf;
                }
            } while ( bedrijfDeelnemer.Length == 0 );

            // Een deel van het consolescherm weer 'zwart' maken om nieuwe tekst
            // op deze plaats te laten verschijnen.
            Console.SetCursorPosition( 0, 29 );
            for ( int i = 0; i < 21; i++ )
            {
                string legeZin = new string( ' ', 97 );
                Console.WriteLine( legeZin );
            }
            Console.SetCursorPosition( 0, 29 );

            // Toon lijst van mogelijke activiteiten waarop de deelnemers 
            // zich kunnen inschrijven.
            Console.WriteLine( "Kies een activiteit uit de lijst:\n" );
            Activiteiten.ToonTabelActiviteiten();

            // Gekozen activiteit uit de getoonde lijst inlezen en opslaan.
            do
            {
                activiteit = TextCapturingAtPosition( 50, 20 );
                if ( Activiteiten.lijstActiviteiten.Exists( e => e.Naam == activiteit ) )
                {
                    break;
                }
                else
                {
                    string foutmelding = "U kan enkel een activiteit overnemen uit de lijst!";
                    DisplayFoutVermelding( foutmelding, 50, 20 );
                }
            } while ( !Activiteiten.lijstActiviteiten.Exists( e => e.Naam == activiteit ) );

            // Juiste datum van de gekozen activiteit inlezen en opslaan na ingave.
            // Zeker belangrijk wanneer de naam van de activiteit meerdere keren 
            // voorkomt op de lijst met erschillende data. Dit is de laatste variabele
            // die nodig is om een nieuwe inschrijving te kunnen aanmaken en deze
            // automatisch bij te voegen aan de lijst 'lijstInschrijvingen'.
            do
            {
                string format = "dd-MM-yyyy";
                dagActiviteit = TextCapturingAtPosition( 50, 22 );
                if ( !DateTime.TryParseExact( dagActiviteit.Replace( " ", string.Empty ),
                           format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dagEvent ) )
                {
                    string foutmelding = "U heeft geen geldige datum ingegeven. Juiste schrijfwijze!";
                    DisplayFoutVermelding( foutmelding, 50, 22 );
                }
                else
                {
                    if ( Activiteiten.lijstActiviteiten.Exists( e => e.DagVanActiviteit.Equals( dagActiviteit ) && e.Naam == activiteit ) )
                    {
                        ingegevenActiviteit = Activiteiten.lijstActiviteiten.Find
                                              ( e => e.Naam == activiteit && e.DagVanActiviteit.Equals( dagActiviteit ) );
                        Deelnemer nieuweDeelnemer = new Deelnemer( achternaamDeelnemer, voornaamDeelnemer, bedrijfDeelnemer, false );
                        Tuple<Deelnemer, Activiteiten> inschrijvingscombinatie = new Tuple<Deelnemer, Activiteiten>( nieuweDeelnemer, ingegevenActiviteit );
                        Inschrijvingen inschrijving = new Inschrijvingen( inschrijvingscombinatie );
                    }
                    else
                    {
                        string foutmelding = $"Enkel een datum bij activiteit '{activiteit}' uit de lijst!";
                        DisplayFoutVermelding( foutmelding, 50, 22 );
                    }
                }
            } while ( (ingegevenActiviteit.Naam != activiteit) && (ingegevenActiviteit.DagVanActiviteit != dagActiviteit) );

        }

        /// <summary>
        /// Methode die de tabel van de invulvelden bepaalt:
        /// de exacte plaats van alle velden, eventueel 'extra' 
        /// styling, een extra kader voor 'foutvermelding' en 
        /// hoe alle velden worden weergegeven in de console.
        /// </summary>
        private void LaatMaakInschrijvingTabelZien()
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
