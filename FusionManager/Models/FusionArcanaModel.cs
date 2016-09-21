using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FusionManager.Models
{
    public interface IFusionArcanaModel
    {
        Arcana GetDoubleFusionResultingArcana(Arcana first, Arcana second);
        List<Tuple<Arcana, Arcana>> GetDoubleFusionParametersByArcana(Arcana desiredResult);
        Arcana GetTripleFusionResultingArcana(Arcana first, Arcana second);
        List<Tuple<Arcana, Arcana>> GetTripleFusionParametersByArcana(Arcana desiredResult);
        bool OnlyAvailableThroughSpecialFusion(string name);
        bool CombinationResultsInSpecialFusion(string[] combination);
        string GetSpecialFusionResult(string[] combination);
        List<string[]> GetSpecialFusionCombination(string name);
    }

    public class FusionArcanaModel : IFusionArcanaModel
    {
        Arcana[,] doubleFusion;
        Arcana[,] tripleFusion;
        Dictionary<string, string[]> specialFusion;

        public FusionArcanaModel()
        {
            #region Combination Arrays

            doubleFusion = new Arcana[21, 21] {
                {Arcana.Fool, Arcana.Empress, Arcana.Devil, Arcana.Magician, Arcana.Temperance, Arcana.Justice, Arcana.Death, Arcana.Hierophant, Arcana.Strength, Arcana.Priestess, Arcana.Lovers, Arcana.Justice, Arcana.Magician, Arcana.Chariot, Arcana.Death, Arcana.Strength, Arcana.Hanged, Arcana.Lovers, Arcana.Emperor, Arcana.Hermit, Arcana.Star},
                {Arcana.None, Arcana.Magician, Arcana.Chariot, Arcana.Justice, Arcana.Hanged, Arcana.Priestess, Arcana.Fortune, Arcana.Priestess, Arcana.Emperor, Arcana.Devil, Arcana.Justice, Arcana.Hierophant, Arcana.Hermit, Arcana.Hanged, Arcana.Star, Arcana.Chariot, Arcana.Temperance, Arcana.Death, Arcana.Emperor, Arcana.Star, Arcana.Empress},
                {Arcana.None, Arcana.None, Arcana.Priestess, Arcana.Temperance, Arcana.Empress, Arcana.Star, Arcana.Emperor, Arcana.Hierophant, Arcana.Death, Arcana.Strength, Arcana.Magician, Arcana.Justice, Arcana.Lovers, Arcana.Strength, Arcana.Star, Arcana.Lovers, Arcana.Fortune, Arcana.Magician, Arcana.Hanged, Arcana.Chariot, Arcana.Hermit},
                {Arcana.None, Arcana.None, Arcana.None, Arcana.Empress, Arcana.Justice, Arcana.Magician, Arcana.Temperance, Arcana.Death, Arcana.Star, Arcana.Strength, Arcana.Hermit, Arcana.Chariot, Arcana.Devil, Arcana.Lovers, Arcana.Priestess, Arcana.Priestess, Arcana.Emperor, Arcana.Hierophant, Arcana.Priestess, Arcana.Chariot, Arcana.Fortune},
                {Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.Emperor,  Arcana.Hermit, Arcana.Fortune, Arcana.Strength, Arcana.Priestess, Arcana.Hierophant, Arcana.Star, Arcana.Star, Arcana.Strength, Arcana.Hierophant, Arcana.Devil, Arcana.Justice, Arcana.Lovers, Arcana.Hermit, Arcana.Temperance, Arcana.Magician, Arcana.Chariot},
                {Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.Hierophant, Arcana.Strength, Arcana.Star, Arcana.Hanged, Arcana.Lovers, Arcana.Strength, Arcana.Chariot, Arcana.Fortune, Arcana.Empress, Arcana.Chariot, Arcana.Emperor, Arcana.Devil, Arcana.Lovers, Arcana.Hermit, Arcana.Hanged, Arcana.Temperance},
                {Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.Lovers, Arcana.Devil, Arcana.Empress, Arcana.Chariot, Arcana.Justice, Arcana.Magician, Arcana.Death, Arcana.Emperor, Arcana.Hanged, Arcana.Empress, Arcana.Chariot, Arcana.Hierophant, Arcana.Magician, Arcana.Star, Arcana.Hanged},
                {Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.Chariot, Arcana.Magician, Arcana.Star, Arcana.Priestess, Arcana.Lovers, Arcana.Fortune, Arcana.Temperance, Arcana.Strength, Arcana.Magician, Arcana.Empress, Arcana.Emperor, Arcana.Justice, Arcana.Lovers, Arcana.None},
                {Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.Justice, Arcana.Fortune, Arcana.Priestess, Arcana.Emperor, Arcana.Lovers, Arcana.Hermit, Arcana.Lovers, Arcana.Hierophant, Arcana.Fortune, Arcana.Temperance, Arcana.Empress, Arcana.Devil, Arcana.None},
                {Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.Hermit, Arcana.Chariot, Arcana.Priestess, Arcana.Death, Arcana.Fortune, Arcana.Priestess, Arcana.Hanged, Arcana.Emperor, Arcana.Justice, Arcana.Hierophant, Arcana.Death, Arcana.Magician},
                {Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.Fortune, Arcana.Temperance, Arcana.Emperor, Arcana.Star, Arcana.Empress, Arcana.Hanged, Arcana.Temperance, Arcana.Devil, Arcana.Lovers, Arcana.Hanged, Arcana.Devil},
                {Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.Strength, Arcana.Star, Arcana.Devil, Arcana.Fortune, Arcana.Priestess, Arcana.Hermit, Arcana.Empress, Arcana.Hierophant, Arcana.Empress, Arcana.None},
                {Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.Hanged, Arcana.Justice, Arcana.Death, Arcana.Temperance, Arcana.Hierophant, Arcana.Fortune, Arcana.Strength, Arcana.Hierophant, Arcana.Priestess},
                {Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.Death, Arcana.Emperor, Arcana.Hermit, Arcana.Priestess, Arcana.Chariot, Arcana.Devil, Arcana.Justice, Arcana.None},
                {Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.Temperance, Arcana.Emperor, Arcana.Strength, Arcana.Hierophant, Arcana.Chariot, Arcana.Magician, Arcana.Death},
                {Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.Devil, Arcana.Death, Arcana.Strength, Arcana.Star, Arcana.Fortune, Arcana.Lovers},
                {Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.Tower, Arcana.Hanged, Arcana.Magician, Arcana.Hermit, Arcana.Hierophant},
                {Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.Star, Arcana.Fortune, Arcana.Empress, Arcana.Strength},
                {Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.Moon, Arcana.Empress, Arcana.Justice},
                {Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.Sun, Arcana.Emperor},
                {Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.Judgement}
            };

            tripleFusion = new Arcana[21, 21]{
                {Arcana.Fool,Arcana.Chariot,Arcana.Sun,Arcana.Magician,Arcana.Star,Arcana.Judgement,Arcana.Emperor,Arcana.Sun,Arcana.Hermit,Arcana.Lovers,Arcana.Emperor,Arcana.Justice,Arcana.Devil,Arcana.Temperance,Arcana.Hanged,Arcana.Moon,Arcana.Judgement,Arcana.Strength,Arcana.Fortune,Arcana.Tower,Arcana.Moon},
                {Arcana.None, Arcana.Magician,Arcana.Justice,Arcana.Hermit,Arcana.Moon,Arcana.Fortune,Arcana.Chariot,Arcana.Judgement,Arcana.Fool,Arcana.Strength,Arcana.Hanged,Arcana.Sun,Arcana.Hermit,Arcana.Emperor,Arcana.Tower,Arcana.Empress,Arcana.Temperance,Arcana.Fool,Arcana.Lovers,Arcana.Death,Arcana.Emperor},
                {Arcana.None, Arcana.None, Arcana.Priestess,Arcana.Justice,Arcana.Empress,Arcana.Death,Arcana.Fool,Arcana.Sun,Arcana.Magician,Arcana.Tower,Arcana.Hierophant,Arcana.Fortune,Arcana.Death,Arcana.Lovers,Arcana.Star,Arcana.Emperor,Arcana.Hanged,Arcana.Moon,Arcana.Devil,Arcana.Fool,Arcana.Strength},
                {Arcana.None, Arcana.None, Arcana.None, Arcana.Empress,Arcana.Hanged,Arcana.Tower,Arcana.Temperance,Arcana.Priestess,Arcana.Moon,Arcana.Judgement,Arcana.Strength,Arcana.Hierophant,Arcana.Star,Arcana.Fortune,Arcana.Devil,Arcana.Tower,Arcana.Chariot,Arcana.Sun,Arcana.Priestess,Arcana.Justice,Arcana.Fool},
                {Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.Emperor,Arcana.Lovers,Arcana.Devil,Arcana.Hermit,Arcana.Empress,Arcana.Chariot,Arcana.Sun,Arcana.Chariot,Arcana.Fortune,Arcana.Hierophant,Arcana.Justice,Arcana.Fool,Arcana.Hermit,Arcana.Temperance,Arcana.Tower,    Arcana.Moon,Arcana.Sun},
                {Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.Hierophant,Arcana.Moon,Arcana.Hanged,Arcana.Sun,Arcana.Star,Arcana.Strength,Arcana.Magician,Arcana.Tower,Arcana.Chariot,Arcana.Priestess,Arcana.Star,Arcana.Devil,Arcana.Fortune,Arcana.Strength,Arcana.Hermit,Arcana.Temperance},
                {Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.Lovers,Arcana.Hierophant,Arcana.Judgement,Arcana.Hanged,Arcana.Tower,Arcana.Hermit,Arcana.Sun,Arcana.Priestess,Arcana.Strength,Arcana.Sun,Arcana.Magician,Arcana.Hermit,Arcana.Death,Arcana.Star,Arcana.Devil},
                {Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.Chariot,Arcana.Fool,Arcana.Emperor,Arcana.Lovers,Arcana.Death,Arcana.Devil,Arcana.Magician,Arcana.Moon,Arcana.Temperance,Arcana.Emperor,Arcana.Empress,Arcana.Justice,Arcana.Strength,Arcana.None},
                {Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.Justice,Arcana.Strength,Arcana.Priestess,Arcana.Tower,Arcana.Lovers,Arcana.Temperance,Arcana.Emperor,Arcana.Fortune,Arcana.Moon,Arcana.Hierophant,Arcana.Hanged,Arcana.Hierophant,Arcana.None},
                {Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.Hermit,Arcana.Hanged,Arcana.Devil,Arcana.Fool,Arcana.Moon,Arcana.Sun,Arcana.Priestess,Arcana.Death,Arcana.Justice,Arcana.Empress,Arcana.Fortune,Arcana.Magician},
                {Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.Fortune,Arcana.Moon,Arcana.Justice,Arcana.Fool,Arcana.Death,Arcana.Fool,Arcana.Sun,Arcana.Devil,Arcana.Fool,Arcana.Magician,Arcana.Empress},
                {Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.Strength,Arcana.Empress,Arcana.Lovers,Arcana.Tower,Arcana.Hanged,Arcana.Fool,Arcana.Judgement,Arcana.Star,Arcana.Emperor,Arcana.None},
                {Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.Hanged,Arcana.Tower,Arcana.Hierophant,Arcana.Chariot,Arcana.Priestess,Arcana.Moon,Arcana.Temperance,Arcana.Temperance,Arcana.Hermit},
                {Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.Death,Arcana.Moon,Arcana.Strength,Arcana.Star,Arcana.Temperance,Arcana.Sun,Arcana.Empress,Arcana.None},
                {Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.Temperance,Arcana.Magician,Arcana.Lovers,Arcana.Chariot,Arcana.Death,Arcana.Empress,Arcana.Hierophant},
                {Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.Devil,Arcana.Fool,Arcana.Death,Arcana.Hierophant,Arcana.Fool,Arcana.Tower},
                {Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.Tower,Arcana.Priestess,Arcana.Judgement,Arcana.Devil,Arcana.Fortune},
                {Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.Star,Arcana.Magician,Arcana.Tower,Arcana.Hanged},
                {Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.Moon,Arcana.Judgement,Arcana.Priestess},
                {Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.Sun,Arcana.Lovers},
                {Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.None, Arcana.Judgement},
            };

            #endregion

            #region Special Fusions

            specialFusion = new Dictionary<string, string[]>();
            specialFusion.Add("Black Frost", new string[] { "Jack Frost", "Pyro Jack", "King Frost" });
            specialFusion.Add("Alice", new string[] { "Nebiros", "Belial" });
            specialFusion.Add("Pale Rider", new string[] { "White Rider", "Black Rider", "Red Rider" });
            specialFusion.Add("Norn", new string[] { "Clotho", "Lachesis", "Atropos" });
            specialFusion.Add("Michael", new string[] { "Uriel", "Rapael", "Gabriel" });
            specialFusion.Add("Shiva", new string[] { "Rangda", "Barong" });
            specialFusion.Add("Beelzebub", new string[] { "Astaroth", "Baal Zebul" });
            specialFusion.Add("Ardha", new string[] { "Shiva", "Parvati" });
            specialFusion.Add("Zeus", new string[] { "Warrior Zeus", "Seth" });
            specialFusion.Add("Lucifer", new string[] { "Metatron", "Ardha", "Zeus" });

            #endregion
        }

        #region Double Fusion

        public Arcana GetDoubleFusionResultingArcana(Arcana first, Arcana second)
        {
            return doubleFusion[(int)first, (int)second];
        }

        public List<Tuple<Arcana, Arcana>> GetDoubleFusionParametersByArcana(Arcana desiredResult)
        {
            List<Tuple<Arcana,Arcana>> combinations = new List<Tuple<Arcana, Arcana>>();

            var rowLowerLimit = doubleFusion.GetLowerBound(0);
            var rowUpperLimit = doubleFusion.GetUpperBound(0);

            var colLowerLimit = doubleFusion.GetLowerBound(1);
            var colUpperLimit = doubleFusion.GetUpperBound(1);

            for (int row = rowLowerLimit; row <= rowUpperLimit; row++)
            {
                for (int col = colLowerLimit; col <= colUpperLimit; col++)
                {
                    if (doubleFusion[row, col] == desiredResult)
                    {
                        Tuple<Arcana,Arcana> item = new Tuple<Arcana, Arcana>((Arcana)row, (Arcana)col);

                        if (!(item.Item1 == desiredResult || item.Item2 == desiredResult))
                        {
                            combinations.Add(item);
                        }
                    }
                }
            }

            return combinations;
        }

        #endregion

        #region Triple Fusion

        public Arcana GetTripleFusionResultingArcana(Arcana first, Arcana second)
        {
            return tripleFusion[(int)first, (int)second];
        }

        public List<Tuple<Arcana, Arcana>> GetTripleFusionParametersByArcana(Arcana desiredResult)
        {
            List<Tuple<Arcana, Arcana>> combinations = new List<Tuple<Arcana, Arcana>>();

            var rowLowerLimit = tripleFusion.GetLowerBound(0);
            var rowUpperLimit = tripleFusion.GetUpperBound(0);

            var colLowerLimit = tripleFusion.GetLowerBound(1);
            var colUpperLimit = tripleFusion.GetUpperBound(1);

            for (int row = rowLowerLimit; row <= rowUpperLimit; row++)
            {
                for (int col = colLowerLimit; col <= colUpperLimit; col++)
                {
                    if (tripleFusion[row, col] == desiredResult)
                    {
                        Tuple<Arcana, Arcana> item = new Tuple<Arcana, Arcana>((Arcana)row, (Arcana)col);

                        if (!(item.Item1 == desiredResult || item.Item2 == desiredResult))
                        {
                            combinations.Add(item);
                        }
                    }
                }
            }

            return combinations;
        }

        #endregion

        #region Special Fusion

        public bool OnlyAvailableThroughSpecialFusion(string name)
        {
            return specialFusion.ContainsKey(name);
        }

        public bool CombinationResultsInSpecialFusion(string[] combination)
        {
            bool result = false;

            List<string> searchTerms = new List<string>(combination);
            int matchesFound;

            foreach (var item in specialFusion)
            {
                List<string> fusionComponents = new List<string>(item.Value);

                matchesFound = 0;

                foreach (string term in searchTerms)
                {
                    if (fusionComponents.Contains(term))
                    {
                        matchesFound++;
                    }
                }

                if (matchesFound == searchTerms.Count && matchesFound == fusionComponents.Count)
                {
                    result = true;
                    return result;
                }
            }

            return result;
        }

        public string GetSpecialFusionResult(string[] combination)
        {
            string result = String.Empty;

            List<string> fusionCombination = new List<string>(combination);
            int matchesFound;
            bool fusionSuccess = false;

            foreach (var item in specialFusion)
            {
                List<string> fusionComponents = new List<string>(item.Value);

                matchesFound = 0;

                foreach (string component in fusionCombination)
                {
                    if (fusionComponents.Contains(component))
                    {
                        matchesFound++;
                    }
                }

                if (matchesFound == fusionCombination.Count && matchesFound == fusionComponents.Count)
                {
                    result = item.Key;
                    fusionSuccess = true;
                    return result;
                }
            }

            if (!fusionSuccess)
            {
                throw new ArgumentException("This combination does not result in a special fusion.");
            }

            return result;
        }

        public List<string[]> GetSpecialFusionCombination(string name)
        {
            List<string[]> result = new List<string[]>();

            try
            {
                result.Add(specialFusion[name]);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(String.Format("{0} is not a special fusion.", name));
            }

            return result;
        }

        #endregion
    }
}
