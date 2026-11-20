namespace BankingTool.Model.Dto.BaseDto
{
    public class CreditCardNumber
    {
        private string _cardNumber;
        private string _maskedCardNumber;
        public string CardNumber
        {
            get
            {
                return _cardNumber;
            }
            set
            {
                _cardNumber = value;
            }
        }
        public string MaskedCardNumber
        {
            get
            {
                return _maskedCardNumber;
            }
            set
            {
                _maskedCardNumber = value;
            }
        }

        public CreditCardNumber(string cardNumber)
        {
            _cardNumber = cardNumber;
            _maskedCardNumber = ToMaskCardNumber(cardNumber);
        }

        public static implicit operator CreditCardNumber(string cardNumber)
        {
            return new CreditCardNumber(cardNumber);
        }

        public static implicit operator string(CreditCardNumber cardNumber)
        {
            return cardNumber.CardNumber;
        }

        public override string ToString()
        {
            return CardNumber;
        }
        public static string ToMaskCardNumber(string cardNumber)
        {
            return $"**** **** **** {cardNumber[^4..]}";
        }
    }
}
