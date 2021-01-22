using System;
using System.Text.RegularExpressions;
using System.Threading;

namespace TeambuildingApp
{
    /// <summary>
    /// In deze klasse wordt de werking (= blauwdruk) van het 
    /// ZoekOpActiviteit-menu gecodeerd. Een 'WerkingMenu'-klasse 
    /// gaat altijd samen met een bijhorende 'InvullingMenu'-klasse.
    /// </summary>
    public class WerkingZoekOpActiviteitMenu
    {
        #region velden op instantie-niveau

        /// <summary>
        /// Het veld dat de gebruiker moet invullen in het ZoekOpActiviteit-menu.
        /// </summary>
        private string veld;

        /// <summary>
        /// De hoofding in het ZoekOpActiviteit-menu.
        /// </summary>
        private string menuHoofding;

        /// <summary>
        /// De subhoofding in het ZoekOpActiviteit-menu.
        /// </summary>
        private string menuSubHoofding;

        /// <summary>
        /// De hoofdtitel van het ZoekOpActiviteit-menu.
        /// </summary>
        private string hoofdtitel;

        #endregion

        #region constructor

        /// <summary>
        /// Hier wordt de ZoekOpActiviteitmenu-instantie aangemaakt.
        /// </summary>
        /// <param name="menuhoofding">De hoofding in het ZoekOpActiviteit-menu.</param>
        /// <param name="menusubhoofding">De subhoofding in het ZoekOpActiviteit-menu.</param>
        /// <param name="veld">Het veld dat de gebruiker moet invullen in het ZoekOpActiviteit-menu.</param>
        /// <param name="hoofdtitel">De hoofdtitel van het ZoekOpActiviteit-menu.</param>
        public WerkingZoekOpActiviteitMenu( string menuhoofding, string menusubhoofding, string veld, string hoofdtitel )
        {
            this.veld = veld;
            menuHoofding = menuhoofding;
            menuSubHoofding = menusubhoofding;
            this.hoofdtitel = hoofdtitel;
        }

        #endregion

        #region methoden op instantie-niveau

        /// <summary>
        /// Deze methode slaat de input in het invulveld op als een 'string'. 
        /// Er wordt tevens aan de nodige foutenafhandeling gedaan. De 
        /// ingegeven string wordt gecontroleerd in de 'lijstActiviteiten',
        /// om uiteindelijk de overeenstemmende inschrijvingen te kunnen
        /// weergeven uit de lijst 'lijstInschrijvingen'.
        /// </summary>
        public string RunZoekOpActiviteit()
        {

            Console.Clear();
            LaatZoekOpActiviteitTabelZien();

            // locale variabele
            string naamActiviteit = String.Empty;

            // De naam van de activiteit inlezen, controleren en opslaan
            // Deze moet gekozen worden uit de lijst van mogelijke activiteiten
            do
            {
                string activiteit = TextCapturingAtPosition( 50, 14 );
                if ( string.IsNullOrEmpty( activiteit ) || activiteit.Trim().Length == 0 )
                {
                    string foutmelding = "Activiteit is een verplicht veld.";
                    DisplayFoutVermelding( foutmelding, 50, 14 );
                }
                else if ( !Regex.IsMatch( activiteit, @"^[a-zA-Z_\s]+$" ) )
                {
                    string foutmelding = "Activiteit kan enkel letters/underscore/spaties bevatten.";
                    DisplayFoutVermelding( foutmelding, 50, 14 );
                }
                else
                {
                    if ( Activiteiten.mogelijkeActiviteiten.ContainsKey( activiteit ) )
                    {
                        naamActiviteit = activiteit;
                        break;
                    }
                    else
                    {
                        string foutmelding = "Kies 1 van de activiteiten uit de lijst onderaan!";
                        DisplayFoutVermelding( foutmelding, 50, 14 );
                    }
                }
            } while ( naamActiviteit == "" );

            return naamActiviteit;
        }

        /// <summary>
        /// Methode die de layout van het invulveld bepaalt:
        /// de exacte plaats, eventueel 'extra' styling, 
        /// een extra kader voor 'foutvermelding' en 
        /// hoe de aangepaste lijst 'lijstInschrijvingen' 
        /// wordt weergegeven in de console.
        /// </summary>
        private void LaatZoekOpActiviteitTabelZien()
        {
            Console.WriteLine( hoofdtitel );
            Styling.Center( menuHoofding );
            Console.WriteLine( "\n\n" );
            Styling.Center( menuSubHoofding );
            System.Console.WriteLine( "\n" );
            int regelnr = 13;
            Styling.WriteAt( ("").PadRight( 75, '*' ), 10, regelnr );
            Styling.WriteAt( ($"*  {veld,-35}{" ",36}*"), 10, ++regelnr );
            Styling.WriteAt( ("").PadRight( 75, '*' ), 10, ++regelnr );
            Styling.WriteAt( ("").PadRight( 75, '*' ), 10, 17 );
            Styling.WriteAt( ($"*  {"Foutmelding: ",-15}{" ",56}*"), 10, 18 );
            Styling.WriteAt( ("").PadRight( 75, '*' ), 10, 19 );
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
            Styling.WriteAt( Styling.InKleur( foutmelding, "#ff6200" ), 26, 18 );
            Thread.Sleep( 3000 );
            string legeZin = new string( ' ', 58 );
            Styling.WriteAt( legeZin, 26, 18 );
            Styling.WriteAt( Styling.InKleur( "Probeer opnieuw in te geven.", "#ff6200" ), 26, 18 );
            Thread.Sleep( 3000 );
            Styling.WriteAt( legeZin, 26, 18 );
            Console.SetCursorPosition( positie, regelnr );
            string legeZin2 = new string( ' ', 33 );
            Styling.WriteAt( legeZin2, positie, regelnr );
            Console.SetCursorPosition( positie, regelnr );
            Console.CursorVisible = true;
        }

        #endregion
    }
}
