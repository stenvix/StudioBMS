using System;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace StudioBMS.Business.DTO.Models.ViewModels
{
    [DataContract]
    public class LiqPayViewModel
    {
        [DataMember] public string action = "pay";

        [DataMember] public double amount;

        [DataMember] public string currency = "UAH";

        [DataMember] public string description;

        [DataMember] public string language = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;

        [DataMember] public string order_id;

        [DataMember] public string public_key = "i13236205459";

        [DataMember] public string sandbox = "1";

        [DataMember] public int version = 3;

        [DataMember] public string result_url = "";

        [DataMember] public string server_url = "";

        public int Version
        {
            get => version;
            set => version = value;
        }

        public double Amount
        {
            get => amount;
            set => amount = value;
        }

        public string PublicKey
        {
            get => public_key;
            set => public_key = value;
        }

        public string OrderId
        {
            get => order_id;
            set => order_id = value;
        }

        public string Language
        {
            get => language;
            set => language = value;
        }

        public string ServerUrl
        {
            get => server_url;
            set => server_url = value;
        }

        public string ResultUrl
        {
            get => result_url;
            set => result_url = value;
        }

        public string Description
        {
            get => description;
            set => description = value;
        }

        public string PrivateKey { get; set; }

        private string DataString =>  JsonConvert.SerializeObject(this);
        public string Data => Convert.ToBase64String(Encoding.UTF8.GetBytes(DataString));

        private string SignatureString => $"{PrivateKey}{Data}{PrivateKey}";
        private byte[] SignatureSHA1 => SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(SignatureString));
        public string Signature => Convert.ToBase64String(SignatureSHA1);


        public static LiqPayViewModel GetModel(OrderModel order)
        {
            return new LiqPayViewModel
            {
                OrderId = order.Id.ToString(),
                PrivateKey = "tuKOtMrz1arqJd2nv9UxtuZ5W9SpFgvdpP1P5MpL",
                Amount = order.Price
            };
        }
    }
}