using System;

namespace TeambuildingApp
{
    /// <summary>
    /// Deze klasse is de hoofdklasse voor begeleiders en deelnemers
    /// en bevat zelf niet de mogelijkheid om persoon-instanties aan 
    /// te maken. De methodes hier vermeld zijn gemeenschappelijke 
    /// functies voor zowel begeleiders als deelnemers.
    /// </summary>
    public abstract class Persoon : IComparable // Interface om de juiste 'vgl'-methode te kunnen implementeren
    {
        #region velden op klasse-niveau

        /// <summary>
        /// Aanmaken willekeurig getal bij alle personen
        /// </summary>
        public static Random randomGetalK = new Random();

        /// <summary>
        /// De directory (of 'bestandspad') naar een tekstbestand waarop
        /// 100 voornamen, achternamen en 20 bedrijfsnamen staan vermeld
        /// (Vorm: voornaam, achternaam, bedrijf\n) en dat gebruikt wordt 
        /// om willekeurige begeleiders en deelnemers te creëren tijdens
        /// de app-introductievragen.
        /// </summary>
        protected static string personenDataFilePath = "100personenData.txt";

        #endregion

        #region velden op instantie-niveau

        /// <summary>
        /// Achternaam van de begeleider/deelnemer.
        /// </summary>
        protected readonly string achternaam;

        /// <summary>
        /// Voornaam van de begeleider/deelnemer.
        /// </summary>
        protected readonly string voornaam;

        /// <summary>
        /// Bedrijf waar de begeleider/deelnemer werkzaam is.
        /// </summary>
        protected Bedrijf werktHier = new Bedrijf();

        /// <summary>
        /// Aanmaken willekeurig getal bij een specifiek persoon.
        /// </summary>
        protected Random randomGetalI = new Random();

        #endregion

        #region properties

        /// <summary>
        /// Leeftijd van de begeleider/deelnemer.
        /// </summary>
        public int Leeftijd { get; }

        /// <summary>
        /// Volledige naam van de begeleider/deelnemer.
        /// </summary>
        public string Naam { get; }

        #endregion

        #region constructors

        /// <summary>
        /// Deze constructor is aangemaakt om een oefening te maken in 'overerving'.
        /// </summary>
        /// <param name="achternaam">Achternaam van de begeleider/deelnemer.</param>
        /// <param name="voornaam">Voornaam van de begeleider/deelnemer.</param>
        public Persoon( string achternaam, string voornaam )
        {
            this.achternaam = achternaam;
            this.voornaam = voornaam;
            Naam = $"{voornaam} {achternaam}";

            // Leeftijd wordt in deze app voor ALLE personen ALTIJD willekeurig bepaald
            Leeftijd = SetRandomLeeftijd();
        }

        #endregion

        #region methoden op instantie-niveau

        /// <summary>
        /// 'String overriding'-methode om de bedrijfsnaam weer te geven.
        /// </summary>
        /// <returns></returns>
        public string WerktHier()
        {
            return werktHier.Naam;
        }

        /// <summary>
        /// Leeftijd van IEDERE persoon wordt hier willekeurig gekozen.
        /// Het getal ligt tussen de 18 en 65 (jaar).
        /// </summary>
        /// <returns></returns>
        private int SetRandomLeeftijd()
        {
            return randomGetalI.Next( 18, 66 );
        }

        /// <summary>
        /// 'Equals overriding'-methode om te kijken of twee Persoon-instanties
        /// hetzelfde zijn. Dit om te voorkomen dat er eenzelfde persoon zich
        /// 2x zou kunnen inschrijven of begeleider zijn.
        /// </summary>
        /// <param name="x">Andere Persoon-instantie om mee te vergelijken.</param>
        /// <returns>'True' wil zeggen dat de twee Persoon-instanties hetzelfde zijn.</returns>
        public override bool Equals( object x )
        {
            Persoon a = (Persoon)x;

            // Checken op combinatie 'naam/leeftijd', maar ook op 'hashcode'.
            if ( ((this.Naam == a.Naam) && (this.Leeftijd == a.Leeftijd))
                || (this.GetHashCode() == a.GetHashCode()) )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 'ICompare interface'-methode die helpt om Persoon-instanties op leeftijd
        /// en vervolgens op naam te sorteren.
        /// </summary>
        /// <param name="x">Andere Persoon-instantie om mee te vergelijken.</param>
        /// <returns>'-1' = komt eerst, '0' = gelijk, '1' = komt daarna in de rij.</returns>
        public int CompareTo( object x )
        {
            Persoon a = (Persoon)x;
            // Als de leeftijd hetzelfde is, wordt er alfabetisch gesorteerd
            if ( Leeftijd == a.Leeftijd )
            {
                return string.Compare( Naam, a.Naam );
            }
            else
            {
                // De jongste komt vooraan in de rij
                if ( Leeftijd > a.Leeftijd )
                {
                    return 1;
                }

                if ( Leeftijd < a.Leeftijd )
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
        }

        #endregion
    }
}
