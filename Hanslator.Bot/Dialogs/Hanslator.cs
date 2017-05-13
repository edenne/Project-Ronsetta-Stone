using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Hanslator.Dialogs
{
    public static class Hanslation
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceText">The text to translate</param>
        /// <param name="persona">The persona of the hanslator:  
        ///     either RON which converts English->Hansenese or NOR which converts Hansenese->English</param>
        /// <returns></returns>
        public static string Hanslate(string sourceText, Persona persona = Persona.RON)
        {
            string reply;

            reply = ReplaceGreetings(sourceText);
            reply = AddSuch(sourceText);
            reply = ReplacePunctuation(reply);

            return reply;
        }

        /// <summary>
        /// In Hansenese, any reference to a URL must be proceeded by "see:" or 
        /// </summary>
        /// <param name="sourceText">The text to Hanslate</param>
        /// <returns></returns>
        //private static string SeeUrl(string sourceText)
        //{

        //}


        /// <summary>
        /// Hansenese requires "such" after "as" or "for"
        /// </summary>
        /// <param name="sourceText">Input to hanslate</param>
        /// <returns></returns>
        private static string AddSuch(string sourceText)
        {
            string reply;
            Regex r;

            r = new Regex(@"\bas\b");
            reply = r.Replace(sourceText, "as such");

            return reply;   
        }

        /// <summary>
        /// Periods, colons, and semi-colons should be replaced by ellipses.
        /// A simple comma is not sufficient; it must include "as such"
        /// </summary>
        /// <param name="input">The text to Hanslate.</param>
        /// <returns>The Hanslated text.</returns>
        private static string ReplacePunctuation(string input)
        {
            string reply;
            Regex r;

            r = new Regex("\\.");
            reply = r.Replace(input, "...");

            r = new Regex("\\:");
            reply = r.Replace(input, "...");

            r = new Regex("\\;");
            reply = r.Replace(input, "...");


            r = new Regex("\\,");
            reply = r.Replace(reply, ", for such,");

            return reply;
        }

        /// <summary>
        /// "Greetings" is the only proper greeting.  Furthermore, it should be followed by a dash rather than
        ///     the comma or colon usually used in English.
        /// </summary>
        /// <param name="input">The text to Hanslate.</param>
        /// <returns>Hanslated text.</returns>
        private static string ReplaceGreetings(string input)
        {
            string greetings = "Greetings -";
            string reply;
            Regex r;

            r = new Regex(@"\bHi\b");
            reply = r.Replace(input, greetings);

            r = new Regex(@"\bHello\b");
            reply = r.Replace(reply, greetings);

            return reply;
        }
    }


}


/// <summary>
/// The translator can respond in one of two personas
/// RON expects English and responds in Hansen
/// NOR expects Hansen and replies in English
/// </summary>
public enum Persona
{
    RON = 0,
    NOR = 1
}