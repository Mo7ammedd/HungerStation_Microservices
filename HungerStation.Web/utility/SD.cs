namespace HungerStation.Web.utility;

public class SD
{
    public static string CouponAPIBase { get; set; }

    public enum ApiType
    {
        GET,
        POST,
        PUT,
        DELETE
    }
}