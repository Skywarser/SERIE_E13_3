/* Ce programme. . .
 * 
 * Auteur : Jayci Plamondon
 * Création : 31 octobre 2022
 */

using System;
using static System.Console;
using static System.Convert;
using static System.Math;

namespace SERIE_E13_3
{
    internal class Program
    {

        const int CodeRégionalMin = 100;
        const int CodeRégionalMax = 990;

        const double TauxRéductionJournée = 0.0;        // En pourcentage du tarif de base
        const double TauxRéductionSoirée = 0.15;        //
        const double TauxRéductionAutres = 0.25;        //

        const double MontantRéductionSamedi = 0.15;     // En dollars par minute d'appel
        const double MontantRéductionDimanche = 0.20;   // 
        const double MontantRéductionAutres = 0.0;      //
       
        static void Main()
        {
            new Program().Principale();
        }

        void Principale()
        {
            ObtenirParticipationPlanBoni();

            ObtenirNbAppelsClient();

            ObtenirInfoAppel();
                
        }

        bool ObtenirParticipationPlanBoni()
        {
            char participationPlanBoni;
            for(; ; )
            {       
                WriteLine("Participez-vous au plan boni (répondre O ou N) ?");
                participationPlanBoni = ToChar(ReadLine());

                /***/
                if (participationPlanBoni == 'O' || participationPlanBoni == 'N') break;
                /***/

                WriteLine("Invalide. Veuillez indiquer par O ou N.");
            }
            return participationPlanBoni == 'O';
        } // Fini

        int ObtenirNbAppelsClient()
        {
            int nbAppelsClient;
            for(; ; )
            {
                WriteLine("Indiquer votre nombre d'appels : ");
                nbAppelsClient = ToInt32(ReadLine());

                /***/
                if (0 < nbAppelsClient) break;
                /***/

                WriteLine("Invalide. Veuillez indiquer un nombre d'appel valide (doit être positif et non-nul)");
            }
            

            return nbAppelsClient;
        } // Fini


        void ObtenirInfoAppel()
        {
            int ObtenirCodeRégional()
            {
                int codeRégional;
                for(; ; )
                {
                    WriteLine("Indiquer le code régional (doit être situer entre" +
                               CodeRégionalMin + " et " + CodeRégionalMax + ")");
                    codeRégional = ToInt32(ReadLine());

                    /***/
                    if (CodeRégionalMin < codeRégional || codeRégional < CodeRégionalMax) break;
                    /***/

                    WriteLine("Invalide. Veuillez indiquer un code régional situé entre " +
                               CodeRégionalMin + " et " + CodeRégionalMax + '.');
                }
                return codeRégional;
            }

            int ObtenirHeureDébut()
            {
                int heureDébut;

                for(; ; )
                {
                    WriteLine("Indiquer l'heure de l'appel en format militaire (HHMM) : ");
                    heureDébut = ToInt32(ReadLine());

                    /***/
                    if (heureDébut >= 0000) break;
                    /***/

                    WriteLine("Invalide. Veuillez indiquer une heure sous format militaire HHMM (doit être " +
                              "situé entre 0000 et 2359).");
                }
                return heureDébut;
            }

            string ObtenirJourSemaine()
            {
                string jourDeLaSemaine;

                for (; ; )
                {
                    WriteLine("Indiquer le jour de la semaine de l'appel" +
                    " (trois premières lettres du jour) : ");
                    jourDeLaSemaine = ReadLine();

                    /***/
                    if (jourDeLaSemaine == "lun" || jourDeLaSemaine == "mar" || jourDeLaSemaine == "mer" ||
                        jourDeLaSemaine == "jeu" || jourDeLaSemaine == "ven") break;
                    /***/

                    WriteLine("Invalide. Veuillez indiquer le jour de la semaine par ses trois premières lettres.");
                }
                return jourDeLaSemaine;
            }

            int ObtenirDuréeAppel()
            {
                int duréeAppel;

                for(; ; )
                {
                    WriteLine("Indiquer la durée de l'appel (en minutes : ");
                    duréeAppel = ToInt32(ReadLine());

                    /***/
                    if (duréeAppel > 0) break;
                    /***/

                    WriteLine("Invalide. La durée de l'appel doit être positive et non-nulle.");
                }
                return duréeAppel;
            }
        } // Fini

        void DéterminerCoûtAppel(double p_tarifDeBase, int p_duréeAppel, double p_réductionTotale)
        {
            double coûtAppel;

            double DéterminerTarifBase(int p_codeRégional, int p_duréeAppel)
            {
                double tarifDeBase;

                if (207 >= p_codeRégional && p_codeRégional <= 506)
                    tarifDeBase = p_duréeAppel * 0.45;
                else if (603 >= p_codeRégional && p_codeRégional <= 802)
                    tarifDeBase = p_duréeAppel * 0.52;
                else if (418 >= p_codeRégional && p_codeRégional <= 518)
                    tarifDeBase = p_duréeAppel * 0.50;
                else if (613 >= p_codeRégional && p_codeRégional <= 819)
                    tarifDeBase = p_duréeAppel * 0.35;
                else if (p_codeRégional == 450)
                    tarifDeBase = p_duréeAppel * 0.42;
                else if (p_codeRégional == 514)
                    tarifDeBase = p_duréeAppel * 0.40;
                else
                    tarifDeBase = p_duréeAppel * 0.55;

                return tarifDeBase;
            }

            double DéterminerRéductionTotal(double p_réductionHeure, double p_réductionJour)
            {
                double réductionTotale;

                double DéterminerRéductionHeure(int p_heureDébut, double p_tarifDeBase)
                {
                    double réductionHeure;

                    if (0700 <= p_heureDébut && p_heureDébut <= 1659)
                        réductionHeure = p_tarifDeBase * (1 - TauxRéductionJournée);
                    else if (1700 <= p_heureDébut && p_heureDébut <= 2300)
                        réductionHeure = p_tarifDeBase * (TauxRéductionSoirée);
                    else
                        réductionHeure = p_tarifDeBase * (1 - TauxRéductionAutres);
                    return réductionHeure;
                }
                
                double DéterminerRéductionJour(int p_duréeAppel, string p_jourDeLaSemaine)
                {
                    double réductionJour;

                    if (p_jourDeLaSemaine == "sam")
                        réductionJour = p_duréeAppel * MontantRéductionSamedi;
                    else if (p_jourDeLaSemaine == "dim")
                        réductionJour = p_duréeAppel * MontantRéductionDimanche;
                    else
                        réductionJour = p_duréeAppel * MontantRéductionAutres;

                    return réductionJour;
                }

                réductionTotale = p_réductionHeure + p_réductionJour;

                return réductionTotale;
            }

            coûtAppel = (p_tarifDeBase * p_duréeAppel) - p_réductionTotale;
        }

        void AfficherSommaire()
        { }

    }
}   