using System.Runtime.Serialization;

namespace Route.DAL.Entities.Order
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,
        [EnumMember(Value = "Payment Received")]

        PaymentReceipt,
        [EnumMember(Value = "Payment Failed")]

        PaymentFailed
    }
}
