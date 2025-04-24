namespace BankingTool.Model.Dto.RequestDtos
{
    public class RequestId(bool allowNull = false)
    {
        private int? _id;
        private bool _allowNull = allowNull;

        public int? Id
        {
            get => _id;
            set
            {
                if (!_allowNull && value == null)
                {
                    throw new InvalidOperationException("Id cannot be null.");
                }
                _id = value;
            }
        }
    }
}
