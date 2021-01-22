using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TeambuildingApp
{
    /// <summary>
    /// Deze klasse erft van de Persoon-klasse. Hier worden ook 
    /// alle Deelnemer-instanties in aangemaakt en staan alle
    /// methoden die functies uitvoeren met deelnemer-objecten.
    /// </summary>
    public class Deelnemer : Persoon
    {
        #region velden op klasse-niveau

        /// <summary>
        /// Lijst met alle 'Deelnemer'-instanties. 
        /// (nog niet gekoppeld aan een activiteit).
        /// </summary>
        private static List<Deelnemer> lijstDeelnemer = new List<Deelnemer>();

        #endregion

        #region velden op instantie-niveau

        /// <summary>
        /// De activiteit waarvoor deze deelnemer is ingeschreven.
        /// Deze variabele wordt enkel gebruikt bij de automatisch gekoppelde deelnemers.
        /// </summary>
        private Activiteiten ingeschrevenVoorDezeActiviteit = new Activiteiten();

        /// <summary>
        /// Noodzakelijk om aan te geven via de constructor of de gekoppelde 
        /// activiteit automatisch (en willekeurig) wordt toegevoegd of dat 
        /// de activiteit zal worden gekoppeld via het inschrijvingsmenu.
        /// </summary>
        private bool heeftRandomActiviteit;

        #endregion

        #region constructor

        /// <summary>
        /// Maakt deelnemer aan (erft van Persoon-klasse) en er wordt
        /// onmiddellijk ook een inschrijving-instantie aangemaakt.
        /// </summary>
        /// <param name="achternaam">Achternaam van de deelnemer.</param>
        /// <param name="voornaam">Voornaam van de deelnemer.</param>
        /// <param name="bedrijf">Bedrijf waar de deelnemer werkt.</param>
        /// <param name="heeftrandomactiviteit">'True' als er een random-activiteit moet worden toegevoegd.</param>
        public Deelnemer( string achternaam, string voornaam, string bedrijf, bool heeftrandomactiviteit ) :
                                            base( achternaam, voornaam )
        {
            heeftRandomActiviteit = heeftrandomactiviteit;
            werktHier.Naam = bedrijf;

            // Het bedrijf van de deelnemer komt op de bedrijfslijst (indien nog niet op de lijst). 
            if ( !Bedrijf.lijstBedrijven.Contains( werktHier ) )
            {
                Bedrijf.lijstBedrijven.Add( werktHier );
            }

            // Deelnemer wordt aan de deelnemerslijst toegevoegd (indien nog niet op de lijst).
            if ( !lijstDeelnemer.Contains( this ) )
            {
                lijstDeelnemer.Add( this );
            }

            // Indien gevraagd, wordt er automatisch een willekeurige inschrijving gemaakt
            // en wordt deze inschrijving toegevoegd aan de inschrijvingslijst.
            if ( heeftRandomActiviteit )
            {
                ingeschrevenVoorDezeActiviteit = SetRandomActiviteit();
                Tuple<Deelnemer, Activiteiten> inschrijvingscombinatie = new Tuple<Deelnemer, Activiteiten>( this, ingeschrevenVoorDezeActiviteit );
                Inschrijvingen inschrijving = new Inschrijvingen( inschrijvingscombinatie );
                if ( !Inschrijvingen.lijstInschrijvingen.Contains( inschrijving ) )
                {
                    Inschrijvingen.lijstInschrijvingen.Add( inschrijving );
                    Inschrijvingen.lijstInschrijvingen = Inschrijvingen.lijstInschrijvingen.OrderBy( e => e.Inschrijving.Item2 ).ToList();
                }
            }
        }

        #endregion

        #region methoden op instantie-niveau

        /// <summary>
        /// Deze methode kiest een willekeurige activiteit uit de 
        /// activiteitenlijst.
        /// </summary>
        /// <returns>Geeft een willekeurige activiteit terug.</returns>
        private Activiteiten SetRandomActiviteit()
        {
            int j = randomGetalI.Next( 0, Activiteiten.lijstActiviteiten.Count );
            return (Activiteiten.lijstActiviteiten[j]);
        }

        #endregion

        #region methoden op klasse-niveau.

        /// <summary>
        /// Deze methode maakt een aangegeven aantal deelnemers aan en
        /// haalt de voornamen en achternamen willekeurig uit een tekstbestand.
        /// </summary>
        /// <param name="aantaldeelnemers"></param>
        public static void MaakAantalDeelnemers( int aantaldeelnemers )
        {
            for ( int i = 0; i < aantaldeelnemers; i++ )
            {
                string personenData = File.ReadAllText( personenDataFilePath );
                string[] personenDataApart = personenData.Split( ',' );
                int naamNr = randomGetalK.Next( 0, (personenDataApart.Length - 1) / 3 );
                string _voornaam = personenDataApart[3 * naamNr].Replace( Environment.NewLine, string.Empty ).Trim();
                string _achternaam = personenDataApart[(3 * naamNr) + 1].Trim();
                string _bedrijf = personenDataApart[(3 * naamNr) + 2].Trim();
                Deelnemer randomInschrijving = new Deelnemer( _achternaam, _voornaam, _bedrijf, true );
            }
        }

        #endregion
    }
}
