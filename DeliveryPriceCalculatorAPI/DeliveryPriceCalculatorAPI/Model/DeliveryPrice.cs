namespace DeliveryPriceCalculatorAPI.Model
{
    public class DeliveryPrice
    {
        public struct ItemResponse
        {
            [Newtonsoft.Json.JsonProperty("tariff_code")]
            public int TariffCode { get; set; }

            [Newtonsoft.Json.JsonProperty("delivery_sum")]
            public double DeliverySum { get; set; }
        }

        [Newtonsoft.Json.JsonProperty("tariff_codes")]
        public List<ItemResponse> TariffCodes { get; set; }

        public DeliveryPrice()
        {
            TariffCodes = new List<ItemResponse>();
        }

        public DeliveryPrice(string JSONResponse)
        {
            TariffCodes = Newtonsoft.Json.JsonConvert.DeserializeObject<DeliveryPrice>(JSONResponse).TariffCodes;
        }

        public double? GetDeliverySumByTarif(int TarifId)
        {
            return TariffCodes.Find
                    (
                        (x) => x.TariffCode == TarifId
                    ).DeliverySum;
        }
    }
}
