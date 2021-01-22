using System;

namespace TeambuildingApp
{
    /// <summary>
    /// In deze klasse krijgen de 3 app-introductievragen een concrete inhoud mee.
    /// Deze vragen zijn het eerste wat de gebruiker te zien krijgt, nog voor de 
    /// officiële app start en hebben als doel om de applicatie voor aanvang te 
    /// voorzien van enkele (dummy) gegevens. De 3 vragen met hun bijhorende 
    /// keuzemenu staan telkens op een aparte pagina.
    /// Een 'InvullingMenu'-klasse gaat altijd samen met een bijhorende 
    /// 'WerkingMenu'-klasse.
    /// </summary>
    public class InvullingIntroVragen
    {
        #region velden op klasse-niveau

        /// <summary>
        /// Deze variabele wordt gebruikt als globale variabele bij het aanmaken van 
        /// een ingegeven aantal activiteiten tijdens de app-introductievragen.
        /// </summary>
        private static int aantalActiviteiten;

        /// <summary>
        /// Deze variabele wordt gebruikt als globale variabele bij het aanmaken van 
        /// een ingegeven aantal begeleiders tijdens de app-introductievragen.
        /// </summary>
        private static int aantalBegeleiders;

        /// <summary>
        /// Deze variabele wordt gebruikt als globale variabele bij het aanmaken van 
        /// een ingegeven aantal deelnemers tijdens de app-introductievragen.
        /// </summary>
        private static int aantalDeelnemers;

        #endregion

        #region 'Intro'-methode

        /// <summary>
        /// Methode die uitleg verschaft over de app en de aanvangsvragen introduceert.
        /// </summary>
        public static void Intro()
        {
            Console.SetCursorPosition( 0, 3 );
            Styling.Center( "Deze app helpt het bedrijf 'ActiveBonding' teambuildings organiseren en" );
            Styling.Center( "werd gemaakt door Guy Meuris op 28/12/2020" );

            Console.SetCursorPosition( 0, 12 );
            Styling.Center( "Voor we beginnen met de app wil ik graag nog enkele vragen stellen" );
            Styling.Center( "om de app alvast enkele voorinstellingen te laten doen." );

            Console.SetCursorPosition( 0, 30 );
            Styling.Center( "Druk op een toets om verder te gaan." );
            Console.CursorVisible = false;
            Console.ReadKey( true );

            InvullingIntroVragen.StartIntroVragen();
        }
        #endregion

        #region 'app-introductievragen'-methoden

        public static void StartIntroVragen()   // deze public methode schermt de andere private methoden af.
        {
            IntroVraag1();
            AfsluitingVraag1();
            IntroVraag2();
            AfsluitingVraag2();
            IntroVraag3();
            AfsluitingVraag3();
        }

        /// <summary>
        /// Deze methode geeft de invulling weer van de eerste vraag (en dus
        /// de eerste pagina) van de app-introductievragen. Gebruiker kan hier het
        /// start-aantal begeleiders gaan bepalen.
        /// </summary>
        private static void IntroVraag1()    //  
        {
            string vraag = "Vraag 1/3: Uit hoeveel medewerkers mag de (fictieve) firma 'ActiveBonding' bestaan?";
            string opmerking = "(Maak een keuze met de pijltjes-toetsen ↑ en ↓ en druk op 'ENTER'.)";
            string[] introVragen1Keuzes = { "5", "6", "7", "8", "9", "10" };
            WerkingIntroVragen hoofdMenu = new WerkingIntroVragen( introVragen1Keuzes, vraag, opmerking );
            int gekozenIndex = hoofdMenu.RunIntroVraag1();

            switch ( gekozenIndex )
            {
                case 0:
                    aantalBegeleiders = 5;
                    break;
                case 1:
                    aantalBegeleiders = 6;
                    break;
                case 2:
                    aantalBegeleiders = 7;
                    break;
                case 3:
                    aantalBegeleiders = 8;
                    break;
                case 4:
                    aantalBegeleiders = 9;
                    break;
                case 5:
                    aantalBegeleiders = 10;
                    break;
            }
        }

        /// <summary>
        /// Deze methode sluit de eerste vraag bevestigd af en geeft
        /// het antwoord van de gebruiker weer. Het aantal begeleiders
        /// wordt opgeslagen en begeleiders worden automatisch aangemaakt.
        /// </summary>
        private static void AfsluitingVraag1()
        {
            Styling.Center( $"Uw antwoord is {aantalBegeleiders}." );
            Styling.Center( $"Er worden nu {aantalBegeleiders} medewerkers aangemaakt door de computer." );
            Begeleider.MaakAantalBegeleiders( aantalBegeleiders );
            Styling.Center( "In deze app zijn zij ook meteen een begeleider op 1 van de teambuilding activiteiten." );
            Console.WriteLine( "\n" );
            Styling.Center( "Druk op een toets om verder te gaan naar de volgende vraag." );
            Console.ReadKey( true );
        }

        /// <summary>
        /// Deze methode geeft de invulling weer van de tweede vraag (en dus
        /// de tweede pagina) van de app-introductievragen. Gebruiker kan hier het
        /// start-aantal activiteiten gaan bepalen.
        /// </summary>
        private static void IntroVraag2()
        {
            string vraag = "Vraag 2/3: Met hoeveel activiteiten wenst u de teambuilding app te beginnen?";
            string opmerking = "(Maak een keuze met de pijltjes-toetsen ↑ en ↓ en druk op 'ENTER'.)";
            string[] introVragen1Keuzes = { "3", "5", "8", "10" };
            WerkingIntroVragen hoofdMenu = new WerkingIntroVragen( introVragen1Keuzes, vraag, opmerking );
            int gekozenIndex = hoofdMenu.RunIntroVraag1();

            switch ( gekozenIndex )
            {
                case 0:
                    aantalActiviteiten = 3;
                    break;
                case 1:
                    aantalActiviteiten = 5;
                    break;
                case 2:
                    aantalActiviteiten = 8;
                    break;
                case 3:
                    aantalActiviteiten = 10;
                    break;
            }
        }

        /// <summary>
        /// Deze methode sluit de tweede vraag bevestigd af en geeft
        /// het antwoord van de gebruiker weer. Het aantal activiteiten
        /// wordt opgeslagen en activiteiten worden automatisch aangemaakt.
        /// </summary>
        private static void AfsluitingVraag2()
        {
            Styling.Center( $"Uw antwoord is {aantalActiviteiten}." );
            Styling.Center( $"Er worden nu {aantalActiviteiten} activiteiten aangemaakt door de computer." );
            Activiteiten.MaakAantalActiviteiten( aantalActiviteiten );
            Styling.Center( "U kan ze bekijken in het submenu 'activiteiten' van de app." );
            Console.WriteLine( "\n" );
            Styling.Center( "Druk op een toets om verder te gaan naar de volgende vraag." );
            Console.ReadKey( true );
        }

        /// <summary>
        /// Deze methode geeft de invulling weer van de derde vraag (en dus
        /// de derde pagina) van de app-introductievragen. Gebruiker kan hier het
        /// start-aantal deelnemers gaan bepalen.
        /// </summary>
        private static void IntroVraag3()
        {
            string vraag = "Vraag 3/3: Hoeveel deelnemers wenst u in deze teambuilding app demo-versie?";
            string opmerking = "(Maak een keuze met de pijltjes-toetsen ↑ en ↓ en druk op 'ENTER'.)";
            string[] introVragen1Keuzes = { "3", "5", "8", "10" };
            WerkingIntroVragen hoofdMenu = new WerkingIntroVragen( introVragen1Keuzes, vraag, opmerking );
            int gekozenIndex = hoofdMenu.RunIntroVraag1();

            switch ( gekozenIndex )
            {
                case 0:
                    aantalDeelnemers = 3;
                    break;
                case 1:
                    aantalDeelnemers = 5;
                    break;
                case 2:
                    aantalDeelnemers = 8;
                    break;
                case 3:
                    aantalDeelnemers = 10;
                    break;
            }
        }

        /// <summary>
        /// Deze methode sluit de derde vraag bevestigd af en geeft
        /// het antwoord van de gebruiker weer. Het aantal deelnemers
        /// wordt opgeslagen en deelnemers worden automatisch aangemaakt.
        /// </summary>
        private static void AfsluitingVraag3()
        {
            Styling.Center( $"Uw antwoord is {aantalDeelnemers}." );
            Styling.Center( $"Er worden nu {aantalDeelnemers} deelnemers aangemaakt door de computer." );
            Deelnemer.MaakAantalDeelnemers( aantalDeelnemers );
            Styling.Center( "U kan ze bekijken in het submenu 'inschrijvingen' van de app." );
            Console.WriteLine( "\n" );
            Styling.Center( "Druk op een toets om verder te gaan naar de volgende vraag." );
            Console.ReadKey( true );
        }

        #endregion
    }
}
