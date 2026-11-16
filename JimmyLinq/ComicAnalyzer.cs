using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace JimmyLinq
{
    internal static class ComicAnalyzer
    {
        private static PriceRange CalculatePriceRange(Comic comic)
        {
            if (Comic.Prices[comic.Issue] < 100) return PriceRange.Cheap;
            else return PriceRange.Expensive;
        }

        public static IEnumerable<IGrouping<PriceRange, Comic>> GroupComicsByPrice(IEnumerable<Comic> comics, IReadOnlyDictionary<int, decimal> prices)
        {
            return from comic in comics
                   orderby prices[comic.Issue]
                   group comic by CalculatePriceRange(comic) into comicsGroup
                   select comicsGroup;
        }
        public static IEnumerable<string> GetReviews(IEnumerable<Comic> comics, IEnumerable<Review> reviews)
        {
            return from comic in comics
                   join review in reviews
                   on comic.Issue equals review.Issue
                   orderby comic.Issue
                   select $"{review.Critic} rated #{comic.Issue} '{comic.Name}' {review.Score:0.00}";
        }
    }
}
