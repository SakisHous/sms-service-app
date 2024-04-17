using AutoMapper;
using Events;
using SmsApp.DTO;

namespace SmsApp.Configuration;

public class CustomVendorResolver : IValueResolver<SmsRequest, SmsEvent, string>
{
    private readonly Dictionary<string, string> _vendors = new()
    {
        ["+30"] = "smsVendorGR",
        ["+357"] = "smsVendorCY"
    };

    public string Resolve(SmsRequest source, SmsEvent destination, string destMember, ResolutionContext context)
    {
        string key = source.RecipientCountryCode!.Trim();

        return _vendors.ContainsKey(key) ? _vendors[key] : "smsVendorRest";
    }
}
