using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using OpenNLP;
using OpenNLP.Tools.SentenceDetect;

namespace Hanslator
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
            reply = EmbelishUrl(reply);
            reply = AddHmm(reply);

            return reply;
        }

        /// <summary>
        /// In Hansenese, any reference to a URL must be proceeded by "see:" or "as:"
        /// </summary>
        /// <param name="sourceText">The text to Hanslate</param>
        /// <returns></returns>
        private static string EmbelishUrl(string sourceText)
        {
            string addition;
            var p = new Random().Next(1, 100);

            if (p < 75)
            {
                addition = "see: ";
            }
            else
            {
                addition = "as: ";
            }

            return Regex.Replace(sourceText, @"\b(?:https?://|www\.)\S+\b", match =>
            {
                return addition + Environment.NewLine + match.Value + Environment.NewLine;
            });

        }


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

        private static string AddHmmm(string sourceText)
        {
            if (! sourceText.StartsWith("Greetings"))
            {
                sourceText = "Hmmm... " + sourceText;
            }
            return sourceText;
        }

        //private List<string> SentenceParser(string input)
        //{
        //    var modelPath = "path/to/EnglishSD.nbin";
        //    var sentenceDetector = EnglishMaximumEntropySentenceDetector(modelPath);
        //    var sentences = sentenceDetector.SentenceDetect(input);
        //}
    }


}
