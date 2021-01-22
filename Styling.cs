using Figgle;
using Pastel;
using System;

namespace TeambuildingApp
{
    /// <summary>
    /// In deze klasse staan alle methoden die verband houden
    /// met de 'styling en special formatting' van strings.
    /// </summary>
    public class Styling
    {
        #region Figgle 

        /// <summary>
        /// Deze methode 'pimpt' een string om uit 1 van de 10 mogelijke Ascii-Art FigLets.
        /// Er zijn er nog andere op: http://www.figlet.org/examples.html die kunnen worden bijgevoegd.
        /// 'Figgle' is een NuGet Package van Drew Noakes.
        /// </summary>
        /// <param name="str">De string die je wil bewerken.</param>
        /// <param name="font">De Ascii-Art font die je wil gebruiken. (Kies uit: "standard",
        /// "larry3D", "lean", "ghost", "nipples", "speed", "starwars", "univers", "weird" of
        /// "isometric1")</param>
        /// <returns>Geeft de ingegven string terug na bewerking.</returns>
        public static string ToAsciiArt( string str, string font )
        {
            string result = font == "standard" ? FiggleFonts.Standard.Render( str ) :
                            font == "larry3d" ? FiggleFonts.Larry3d.Render( str ) :
                            font == "lean" ? FiggleFonts.Lean.Render( str ) :
                            font == "ghost" ? FiggleFonts.Ghost.Render( str ) :
                            font == "nipples" ? FiggleFonts.Nipples.Render( str ) :
                            font == "speed" ? FiggleFonts.Speed.Render( str ) :
                            font == "starwars" ? FiggleFonts.Starwars.Render( str ) :
                            font == "univers" ? FiggleFonts.Univers.Render( str ) :
                            font == "weird" ? FiggleFonts.Weird.Render( str ) :
                            font == "isometric1" ? FiggleFonts.Isometric1.Render( str ) : "Er werd geen juiste font doorgegeven.";

            return result;
        }

        #endregion

        #region Pastel

        /// <summary>
        /// Deze methode 'kleurt' een string in de meegegeven hexcode-kleur.
        /// Er zijn nog mogelijkheden op: https://github.com/silkfire/Pastel die kunnen worden bijgevoegd.
        /// 'Pastel' is een NuGet Package van Gabriel Bider.
        /// </summary>
        /// <param name="str">De string die je wil bewerken.</param>
        /// <param name="hexcode">De kleur in Hexcode: vb. "#000000"</param>
        /// <returns>Geeft de ingegven string terug na bewerking.</returns>
        public static string InKleur( string str, string hexcode )
        {
            string strInKleur = str.Pastel( hexcode );
            return strInKleur;
        }

        #endregion

        #region 'Center'-methoden

        /// <summary>
        /// Deze methode plaats een meegegeven string in het midden van de console.
        /// </summary>
        /// <param name="str">De string die je wil bewerken.</param>
        public static void Center( string str )
        {
            Console.SetCursorPosition( Convert.ToInt32( Console.WindowWidth - str.Length ) / 2, Console.CursorTop );
            Console.WriteLine( str );
        }

        /// <summary>
        /// Deze methode plaats een meegegeven string in het midden van de console
        /// én in een meegegeven kleur. (Hexcode voor de kleur).
        /// </summary>
        /// <param name="str">De string die je wil bewerken.</param>
        /// <param name="kleur">De kleur in Hexcode: vb. "#000000"</param>
        public static void Center( string str, string kleur )
        {
            Console.SetCursorPosition( Convert.ToInt32( Console.WindowWidth - str.Length ) / 2, Console.CursorTop );
            Console.WriteLine( InKleur( str, kleur ) );
        }

        #endregion

        #region 'Titel'-methode

        /// <summary>
        /// Deze methode onderstreept de meegegeven string met een stippellijn
        /// bij het weergeven in de console. 
        /// </summary>
        /// <param name="str">De string die je wil bewerken.</param>
        public static void Titel( string str )
        {
            Console.WriteLine( str );
            Console.WriteLine( ("").PadRight( str.Length, '-' ) );
        }

        #endregion

        #region 'GeefWeerOpSpecifiekePositie'-methode

        /// <summary>
        /// Deze methode laat een meegegeven string beginnen op een
        /// specifieke plaats in de console. De plaats wordt tevens
        /// meegegeven door 'positie' en 'regelnummer'.
        /// </summary>
        /// <param name="s">De string die moet geplaatst worden ergens in de console.</param>
        /// <param name="positie">Het aantal 'karakters' bepaalt de positie van links gezien in de console.</param>
        /// <param name="regelnr">De regelnummer bepaalt op welke regel de string komt te staan in de console.</param>
        public static void WriteAt( string s, int positie, int regelnr )
        {
            try
            {
                Console.SetCursorPosition( positie, regelnr );
                Console.WriteLine( s );
            }
            // Foutmelding als de locatie buiten het schermbereik valt.
            catch ( ArgumentOutOfRangeException e )
            {
                Console.Clear();
                Console.WriteLine( "Locatie in de console valt buiten het schermbereik!" );
            }
        }

        #endregion
    }
}
