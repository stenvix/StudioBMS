using System;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace StudioBMS.Business.DTO.Models.ViewModels
{
    [DataContract]
    public class LiqPayCallbackViewModel
    {
        [DataMember] public int acq_id;

        [DataMember] public string action;

        [DataMember] public double agent_commission;

        [DataMember] public double amount;

        [DataMember] public double amount_bonus;

        [DataMember] public double amount_credit;

        [DataMember] public double amount_debit;

        [DataMember] public double commission_credit;

        [DataMember] public double commission_debit;

        [DataMember] public string create_date;

        [DataMember] public string currency;

        [DataMember] public string currency_credit;

        [DataMember] public string currency_debit;

        [DataMember] public string description;
        [DataMember] public string end_date;

        [DataMember] public string err_code;

        [DataMember] public string err_description;

        [DataMember] public string ip;

        [DataMember] public bool is_3ds;

        [DataMember] public string liqpay_order_id;

        [DataMember] public string mpi_eci;

        [DataMember] public string order_id;

        [DataMember] public int payment_id;

        [DataMember] public string paytype;

        [DataMember] public string public_key;

        [DataMember] public double receiver_commission;

        [DataMember] public double sender_bonus;

        [DataMember] public string sender_card_bank;

        [DataMember] public int sender_card_country;

        [DataMember] public string sender_card_mask2;

        [DataMember] public string sender_card_type;

        [DataMember] public double sender_commission;

        [DataMember] public string status;

        [DataMember] public int transaction_id;

        [DataMember] public string type;

        [DataMember] public int version;

        public Guid OrderId => Guid.Parse(order_id);

        public string Status => status;

        public double Amount
        {
            get => amount;
            set => amount = value;
        }

        public string PrivateKey { get; set; }

        private string DataString => JsonConvert.SerializeObject(this);
        public string Data => Convert.ToBase64String(Encoding.UTF8.GetBytes(DataString));

        private string SignatureString => $"{PrivateKey}{Data}{PrivateKey}";
        private byte[] SignatureSHA1 => SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(SignatureString));
        public string Signature => Convert.ToBase64String(SignatureSHA1);

        public string GetSignature(string data)
        {
            return Convert.ToBase64String(SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes($"{PrivateKey}{data}{PrivateKey}")));
        }
    }
}