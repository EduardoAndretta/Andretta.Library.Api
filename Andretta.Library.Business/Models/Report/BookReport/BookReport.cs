namespace Andretta.Library.Business.Models
{
    public class BookReport
    {
        public HighestQuantityStockBook? HighestQuantityStockBook { get; init; }
        public LowestQuantityStockBook? LowestQuantityStockBook { get; init; }
        public HighestUnitPriceStockBook? HighestUnitPriceStockBook { get; init; }
        public LowestUnitPriceStockBook? LowestUnitPriceStockBook { get; init; }
        public HighestTotalPriceBook? HighestTotalPriceBook { get; init; }
        public LowestTotalPriceBook? LowestTotalPriceBook { get; init; }
        public HighestPublishDateBook? HighestPublishDateBook { get; init; }
        public LowestPublishDateBook? LowestPublishDateBook { get; init; }
    }
}
